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
        private RestResponse _response;
        private JObject _latestBill;
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

        
        [Given(@"a group places an order as follow")]
        public async Task GivenAGroupOrdersBeforeTime(Table table)
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

            var request = new RestRequest("order/book", Method.Post);
            request.AddJsonBody(new
            {
                people,
                starters,
                mains,
                drinks,
                hour
            });

            _response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.Created, _response.StatusCode);

            if(_response.Content != null)
            {
                var jsonResponse = JObject.Parse(_response.Content);
                if(jsonResponse["orderId"] != null && int.TryParse((string?)jsonResponse["orderId"], out int orderResult))
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


        [Given(@"a group add an order as follow")]
        public async Task GivenMorePeopleJoin(Table table)
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

            var request = new RestRequest($"order/add/{_orderId}", Method.Post);
            request.AddJsonBody(new
            {
                people,
                starters,
                mains,
                drinks,
                hour
            });

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

        [Given(@"a group each order as follow")]
        public async Task GivenAGroupOrdersEachBeforeTime(Table table)
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

            var request = new RestRequest("order/book", Method.Post);
            //If order is requested individually, store numbers of starters, mains, and drink of each person level in memory context
            _eachIndividualOrder = new Dictionary<string, object> 
            { 
                {"starters", starters },
                {"mains", mains }, 
                {"drinks", drinks }, 
                {"hour", hour } 
            };
            //Order is multipled by number of people 
            request.AddJsonBody(new
            {
                people,
                starters = starters * people,
                mains = mains * people,
                drinks = drinks * people,
                hour
            }); 

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

        [Given(@"order is cancelled as follow")]
        public async Task GivenOneMemberCancelsTheirOrder(Table table)
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

            var request = new RestRequest($"order/delete/{_orderId}", Method.Delete);
            request.AddJsonBody(new
            {
                people,
                starters, 
                mains,
                drinks,
                hour = hour
            });

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

        // REDUNDANT BECAUSE THE STEP IS NOT SPECIFIC ENOUGH 
        //[Given(@"(.*) members cancel their order")]
        //public async Task GivenOneMemberCancelsTheirOrder(int people)
        //{
        //    var request = new RestRequest($"order/delete/{_orderId}", Method.Delete);
        //    _eachIndividualOrder["starters"] = (int)_eachIndividualOrder["starters"] * people;
        //    _eachIndividualOrder["mains"] = (int)_eachIndividualOrder["mains"] * people;
        //    _eachIndividualOrder["drinks"] = (int)_eachIndividualOrder["drinks"] * people;
        //    request.AddJsonBody(new {
        //        people,
        //        starters = (int)_eachIndividualOrder["starters"],
        //        mains = (int)_eachIndividualOrder["mains"],
        //        drinks = (int)_eachIndividualOrder["drinks"],
        //        hour = (string)_eachIndividualOrder["hour"]
        //    });

        //    _response = await _client.ExecuteAsync(request);
        //    Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

        //    if (_response.Content != null)
        //    {
        //        var jsonResponse = JObject.Parse(_response.Content);
        //        if (jsonResponse["orderId"] != null && int.TryParse((string?)jsonResponse["orderId"], out int orderResult))
        //        {
        //            _orderId = orderResult;
        //        }
        //        else
        //        {
        //            Assert.Fail("No order is found.");
        //        }
        //    }
        //    else
        //    {
        //        Assert.Fail("Failed to delete orders");
        //    }
        //}



        [When(@"the bill is requested")]
        public async Task WhenTheBillIsRequested()
        {
            if (_orderId == 0) { Assert.Fail("No order has been booked yet"); }
            var request = new RestRequest($"order/checkout/bill/{_orderId}", Method.Get);
            _response = await _client.ExecuteAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            if(_response.Content != null) { _latestBill = JObject.Parse(_response.Content); }
            
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



    }
}
