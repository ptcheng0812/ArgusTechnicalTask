using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ArgusTestCheckoutSystem.Support;
using System.Diagnostics;
using System.Configuration;

namespace ArgusTestCheckoutSystem.StepDefinitions
{
    [Binding]
    public class CheckoutSteps
    {
        private readonly RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private JObject _latestBill;
        private object _order;
        private Dictionary<string, object> _eachIndividualOrder;
        private int _orderId = 0;
        private ScenarioContext _scenarioContext;

        public CheckoutSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _client = new RestClient("http://localhost:5283/api");

            //If getting base api endpoint url from app.config
            //string baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"] ?? throw new Exception("API URL not found in app.config");
            //var _client = new RestClient(baseUrl);
        }


        [Given(@"a group has an order as follow is set")]
        public void GivenAGroupHasAnOrderAsFollowIsSet(Table table)
        {
            var row = table.Rows[0];
            if (row["People"] == null || row["Starters"] == null || row["Mains"] == null ||
                row["Drinks"] == null || row["Hour"] == null)
            {
                Assert.Fail("Please provide correct key columns. Availables: People | Starters | Mains | Drinks | Hour ");
            }
            int people = int.Parse(row["People"]);
            int starters = int.Parse(row["Starters"]);
            int mains = int.Parse(row["Mains"]);
            int drinks = int.Parse(row["Drinks"]);
            string hour = row["Hour"];

            _order = new
            {
                people,
                starters,
                mains,
                drinks,
                hour
            };


        }

        [When(@"the order is booked via endpoint ""(.*)""")]
        public async Task WhenTheOrderIsBookedViaEndpoint(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(_order);

            _response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Created, _response.StatusCode);

            if (_response.Content != null)
            {
                var jsonResponse = JObject.Parse(_response.Content);
                if (jsonResponse["orderId"] != null && int.TryParse((string?)jsonResponse["orderId"], out int orderResult))
                {
                    _orderId = orderResult;
                }
                else
                {
                    Assert.Fail("No order is found.");
                }
            }
            else
            {
                Assert.Fail("Failed to make this order");
            }
        }


        [When(@"the order is added via endpoint ""(.*)""")]
        public async Task WhenTheOrderIsAddedViaEndpoint(string endpoint)
        {
            var request = new RestRequest($"{endpoint}/{_orderId}", Method.Post);
            request.AddJsonBody(_order);

            _response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            if (_response.Content != null)
            {
                var jsonResponse = JObject.Parse(_response.Content);
                if (jsonResponse["orderId"] != null && int.TryParse((string?)jsonResponse["orderId"], out int orderResult))
                {
                    _orderId = orderResult;
                }
                else
                {
                    Assert.Fail("No order is found.");
                }
            }
            else
            {
                Assert.Fail("Failed to make this order");
            }
        }

        [Given(@"a group has each order as follow is set")]
        public void GivenAGroupHasEachOrderAsFollowIsSet(Table table)
        {
            var row = table.Rows[0];
            if (row["People"] == null || row["Starters"] == null || row["Mains"] == null ||
                row["Drinks"] == null || row["Hour"] == null)
            {
                Assert.Fail("Please provide correct key columns. Availables: People | Starters | Mains | Drinks | Hour ");
            }
            int people = int.Parse(row["People"]);
            int starters = int.Parse(row["Starters"]);
            int mains = int.Parse(row["Mains"]);
            int drinks = int.Parse(row["Drinks"]);
            string hour = row["Hour"];

            //If order is requested individually, store numbers of starters, mains, and drink of each person level in memory context
            _eachIndividualOrder = new Dictionary<string, object>
            {
                {"starters", starters },
                {"mains", mains },
                {"drinks", drinks },
                {"hour", hour }
            };
            //Order is multipled by number of people 
            _order = new
            {
                people,
                starters = starters * people,
                mains = mains * people,
                drinks = drinks * people,
                hour
            };

        }

        [When(@"the order is cancelled via endpoint ""(.*)""")]
        public async Task WhenTheOrderIsCancelledViaEndpoint(string endpoint)
        {
            var request = new RestRequest($"{endpoint}/{_orderId}", Method.Delete);
            request.AddJsonBody(_order);
            _response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            if (_response.Content != null)
            {
                var jsonResponse = JObject.Parse(_response.Content);
                if (jsonResponse["orderId"] != null && int.TryParse((string?)jsonResponse["orderId"], out int orderResult))
                {
                    _orderId = orderResult;
                }
                else
                {
                    Assert.Fail("No order is found.");
                }
            }
            else
            {
                Assert.Fail("Failed to delete orders");
            }
        }


        [When(@"the bill is requested via endpoint ""(.*)""")]
        public async Task WhenTheBillIsRequestedViaEndpoint(string endpoint)
        {
            if (_orderId == 0) { Assert.Fail("No order has been booked yet"); }
            var request = new RestRequest($"{endpoint}/{_orderId}", Method.Get);
            _response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            if(_response.Content != null) { _latestBill = JObject.Parse(_response.Content); }
        }

        [When(@"the bill is requested via endpoint ""(.*)"" but allow error")]
        public async Task WhenTheBillIsRequestedViaEndpointButAllowError(string endpoint)
        {
            var request = new RestRequest($"{endpoint}/{_orderId}", Method.Get);
            _response = await _client.ExecuteAsync(request);
        }

        [Then(@"the total amount should be ""£(.*)"" and (.*) people remain")]
        public void ThenTheTotalAmountShouldBe(double expectedTotal, int people)
        {
            if (_latestBill["totalAmount"] != null && _latestBill["people"] !=null)
            {
                float actualTotal = Helpers.ConvertPoundToFloat((string?)_latestBill["totalAmount"] ?? "0.00");
                int actualPeople = _latestBill["people"]?.ToObject<int?>() ?? 0;
                Assert.AreEqual((float)Math.Round(expectedTotal, 2), (float)Math.Round(actualTotal, 2));
                Assert.AreEqual(people, actualPeople);

            }
            else
            {
                Assert.Fail("Failed to retrieve total amount or number of people.");
            }
        }

        [Then(@"the service returns message ""(.*)""")]
        public void ThenTheServiceReturnsMessage(string message)
        {
            if(_response.Content != null)
            {
                if (JObject.Parse(_response.Content)["message"] != null)
                {
                    Assert.AreEqual(message, JObject.Parse(_response.Content)["message"]?.ToString());
                }
                else
                {
                    Assert.Fail("The response has no message");
                }
            }
            else
            {
                Assert.Fail("The response has no content");
            }
        }


    }
}
