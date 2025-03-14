﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ArgusTestCheckoutSystem.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Checkout")]
    public partial class CheckoutFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "Checkout.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Checkout", @"Assumptions of the api service:

1. order/book:
- receive request body {
     people (int),
     starters (int),
     mains (int),
     drinks (int),
     hour (string) 
} as JSON Body then return {orderId: <some id>, people } with status code 201
- the backend api service create a new Order object stored in memory / db

2. order/add/{_orderId}:
- receive request body {
     people (int),
     starters (int),
     mains (int),
     drinks (int),
     hour (string) 
} as JSON Body then return {orderId: <some id>, people } with status code 200
- the backend api service query that Order object using orderId and add count of people, starters, mains, drinks, drinks 

3. order/delete/{_orderId}:
- receive request body {
     people (int),
     starters (int),
     mains (int),
     drinks (int),
     hour (string) 
} as JSON Body then return {orderId: <some id>, people } with status code 200
- the backend api service query that Order object using orderId and deduct count of people, starters, mains, drinks, drinks 

4. checkout/bill/{_orderId}:
- query the order using orderId 
- calculate the bill in backend: (count of starters*4 + count of mains*7) * 1.1 + count of drinks*2.5 + count of drinks with discount*2.5*0.7
- return { ""totalAmount"": totalAmount, ""people"": people remaining }

5. If an invalid orderId is provided, the API returns 404 Not Found.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("0000 Order with Drink Discount")]
        public virtual void _0000OrderWithDrinkDiscount()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("0000 Order with Drink Discount", "Group of order with 4 people, 4 starters, 4 mains, and 4 drinks before 19:00", tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 43
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table1.AddRow(new string[] {
                            "4",
                            "4",
                            "4",
                            "4",
                            "18:59"});
#line 45
 testRunner.Given("a group places an order as follow", ((string)(null)), table1, "Given ");
#line hidden
#line 48
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 49
 testRunner.Then("the total amount should be \"£55.40\" and 4 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("0001 Order without Drink Discount")]
        public virtual void _0001OrderWithoutDrinkDiscount()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("0001 Order without Drink Discount", "Group of order with 4 people, 4 starters, 4 mains, and 4 drinks on 19:00", tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 51
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table2.AddRow(new string[] {
                            "4",
                            "4",
                            "4",
                            "4",
                            "19:00"});
#line 53
 testRunner.Given("a group places an order as follow", ((string)(null)), table2, "Given ");
#line hidden
#line 56
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 57
 testRunner.Then("the total amount should be \"£58.40\" and 4 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("0100 Order with Drink Discount then Add Order Without Drink Discount")]
        public virtual void _0100OrderWithDrinkDiscountThenAddOrderWithoutDrinkDiscount()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("0100 Order with Drink Discount then Add Order Without Drink Discount", "Group of order with 2 people, 1 starters, 2 mains, and 2 drinks before 19:00 \r\nth" +
                    "en another group of order with 2 people, 0 starters, 2 mains, and 2 drinks order" +
                    " at 20:00", tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 59
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table3.AddRow(new string[] {
                            "2",
                            "1",
                            "2",
                            "2",
                            "18:00"});
#line 62
 testRunner.Given("a group places an order as follow", ((string)(null)), table3, "Given ");
#line hidden
#line 65
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 66
 testRunner.Then("the total amount should be \"£23.30\" and 2 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table4.AddRow(new string[] {
                            "2",
                            "0",
                            "2",
                            "2",
                            "20:00"});
#line 67
 testRunner.Given("a group add an order as follow", ((string)(null)), table4, "Given ");
#line hidden
#line 70
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 71
 testRunner.Then("the total amount should be \"£43.70\" and 4 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("0200 Each Order before 19:00 then One Person Left before 19:00")]
        public virtual void _0200EachOrderBefore1900ThenOnePersonLeftBefore1900()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("0200 Each Order before 19:00 then One Person Left before 19:00", "4 people each order with 1 starters, 1 mains, and 1 drinks before 19:00 \r\n1 peopl" +
                    "e cancel order before 19:00 ", tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 73
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table5.AddRow(new string[] {
                            "4",
                            "1",
                            "1",
                            "1",
                            "18:00"});
#line 76
 testRunner.Given("a group each order as follow", ((string)(null)), table5, "Given ");
#line hidden
#line 79
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 80
 testRunner.Then("the total amount should be \"£55.40\" and 4 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table6.AddRow(new string[] {
                            "1",
                            "1",
                            "1",
                            "1",
                            "18:59"});
#line 81
 testRunner.Given("order is cancelled as follow", ((string)(null)), table6, "Given ");
#line hidden
#line 84
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 85
 testRunner.Then("the total amount should be \"£41.55\" and 3 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("0201 Each Order after 19:00 then One Person Left after 19:00")]
        public virtual void _0201EachOrderAfter1900ThenOnePersonLeftAfter1900()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("0201 Each Order after 19:00 then One Person Left after 19:00", "4 people each order with 1 starters, 1 mains, and 1 drinks after 19:00 \r\n1 people" +
                    " cancel order after 19:00 ", tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 87
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table7.AddRow(new string[] {
                            "4",
                            "1",
                            "1",
                            "1",
                            "19:01"});
#line 90
 testRunner.Given("a group each order as follow", ((string)(null)), table7, "Given ");
#line hidden
#line 93
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 94
 testRunner.Then("the total amount should be \"£58.40\" and 4 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table8.AddRow(new string[] {
                            "1",
                            "1",
                            "1",
                            "1",
                            "20:00"});
#line 95
 testRunner.Given("order is cancelled as follow", ((string)(null)), table8, "Given ");
#line hidden
#line 98
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 99
 testRunner.Then("the total amount should be \"£43.80\" and 3 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("0202 Each Order before 19:00 then One Person Left after 19:00")]
        public virtual void _0202EachOrderBefore1900ThenOnePersonLeftAfter1900()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("0202 Each Order before 19:00 then One Person Left after 19:00", "4 people each order with 1 starters, 1 mains, and 1 drinks before 19:00 \r\n1 peopl" +
                    "e cancel order after 19:00 ", tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 101
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table9.AddRow(new string[] {
                            "4",
                            "1",
                            "1",
                            "1",
                            "18:59"});
#line 104
 testRunner.Given("a group each order as follow", ((string)(null)), table9, "Given ");
#line hidden
#line 107
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 108
 testRunner.Then("the total amount should be \"£55.40\" and 4 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "People",
                            "Starters",
                            "Mains",
                            "Drinks",
                            "Hour"});
                table10.AddRow(new string[] {
                            "1",
                            "1",
                            "1",
                            "1",
                            "20:00"});
#line 109
 testRunner.Given("order is cancelled as follow", ((string)(null)), table10, "Given ");
#line hidden
#line 112
 testRunner.When("the bill is requested", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 113
 testRunner.Then("the total amount should be \"£43.30\" and 3 people remain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
