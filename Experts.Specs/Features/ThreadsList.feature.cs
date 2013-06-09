﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.296
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Experts.Specs.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Przeglądanie listy pytań")]
    public partial class PrzegladanieListyPytanFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ThreadsList.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pl-PL"), "Przeglądanie listy pytań", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
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
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Przejście do szczegółów pytania z listy moje-pytania")]
        public virtual void PrzejscieDoSzczegolowPytaniaZListyMoje_Pytania()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Przejście do szczegółów pytania z listy moje-pytania", ((string[])(null)));
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 5
 testRunner.When("przejdę do /moje-pytania", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 6
 testRunner.And("kliknę przycisk Pytanie testowe bez eksperta", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 7
 testRunner.Then("zobaczę tekst Pytanie oczekuje na odpowiedź eksperta", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Przejście do szczegółów pytania z listy moje-odpowiedzi")]
        public virtual void PrzejscieDoSzczegolowPytaniaZListyMoje_Odpowiedzi()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Przejście do szczegółów pytania z listy moje-odpowiedzi", ((string[])(null)));
#line 9
this.ScenarioSetup(scenarioInfo);
#line 10
 testRunner.Given("że jestem zalogowany jako ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 11
 testRunner.When("przejdę do /moje-odpowiedzi", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 12
 testRunner.And("kliknę przycisk Pytanie testowe z ekspertem", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 13
 testRunner.Then("zobaczę tekst Odpowiadasz na to pytanie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Przejście do szczegółów nieprzejetego pytania z listy pytania-uzytkownikow")]
        public virtual void PrzejscieDoSzczegolowNieprzejetegoPytaniaZListyPytania_Uzytkownikow()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Przejście do szczegółów nieprzejetego pytania z listy pytania-uzytkownikow", ((string[])(null)));
#line 15
this.ScenarioSetup(scenarioInfo);
#line 16
 testRunner.Given("że jestem zalogowany jako ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 17
 testRunner.When("przejdę do /pytania-uzytkownikow", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 18
 testRunner.And("kliknę przycisk Pytanie testowe bez eksperta", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 19
 testRunner.Then("zobaczę tekst Pytanie oczekuje na odpowiedź eksperta", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Niewyświetlanie nieopłaconych pytań")]
        public virtual void NiewyswietlanieNieoplaconychPytan()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Niewyświetlanie nieopłaconych pytań", ((string[])(null)));
#line 21
this.ScenarioSetup(scenarioInfo);
#line 22
 testRunner.Given("że jestem zalogowany jako ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 23
 testRunner.When("przejdę do /pytania-uzytkownikow", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 24
 testRunner.Then("nie zobaczę tekstu Nieopłacone pytanie testowe", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
