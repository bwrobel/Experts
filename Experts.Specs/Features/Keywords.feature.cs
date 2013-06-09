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
    [NUnit.Framework.DescriptionAttribute("Słowa kluczowe")]
    public partial class SlowaKluczoweFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Keywords.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pl-PL"), "Słowa kluczowe", "", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Wyświetlenie listy popularnych tematów pytań")]
        public virtual void WyswietlenieListyPopularnychTematowPytan()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wyświetlenie listy popularnych tematów pytań", ((string[])(null)));
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 5
 testRunner.When("kliknę przycisk Popularne tematy pytań", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 6
 testRunner.Then("zobaczę tekst pytanie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 7
 testRunner.And("zobaczę tekst Kategoria", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wyświetlenie listy najnowszych odpowiedzi")]
        public virtual void WyswietlenieListyNajnowszychOdpowiedzi()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wyświetlenie listy najnowszych odpowiedzi", ((string[])(null)));
#line 9
this.ScenarioSetup(scenarioInfo);
#line 10
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 11
 testRunner.When("kliknę przycisk Katalog odpowiedzi", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 12
 testRunner.Then("zobaczę tekst Zamknięte pytanie testowe sanitized", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 13
 testRunner.And("zobaczę tekst Nauka", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 14
 testRunner.And("zobaczę tekst Zobacz odpowiedź", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wyświetlenie listy wyszukiwanych zwrotów")]
        public virtual void WyswietlenieListyWyszukiwanychZwrotow()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wyświetlenie listy wyszukiwanych zwrotów", ((string[])(null)));
#line 16
this.ScenarioSetup(scenarioInfo);
#line 17
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 18
 testRunner.When("kliknę przycisk Popularne frazy", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 19
 testRunner.Then("zobaczę tekst fraza", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 20
 testRunner.And("zobaczę tekst Kategoria", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wyświetlenie listy popularnych ekspertów")]
        public virtual void WyswietlenieListyPopularnychEkspertow()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wyświetlenie listy popularnych ekspertów", ((string[])(null)));
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 24
 testRunner.When("kliknę przycisk Popularni eksperci", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 25
 testRunner.Then("zobaczę tekst ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 26
 testRunner.And("zobaczę tekst Kategoria", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wyświetlenie szczegółów popularnego tematu")]
        public virtual void WyswietlenieSzczegolowPopularnegoTematu()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wyświetlenie szczegółów popularnego tematu", ((string[])(null)));
#line 28
this.ScenarioSetup(scenarioInfo);
#line 29
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 30
 testRunner.When("kliknę przycisk Popularne tematy pytań", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 31
 testRunner.And("kliknę przycisk pytanie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 32
 testRunner.Then("zobaczę tekst Wyszukany przez Ciebie temat związany jest z jedną z dziedzin w któ" +
                    "rej specjalizują się nasi eksperci.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 33
 testRunner.And("zobaczę tekst Eksperci z kategorii", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 34
 testRunner.And("zobaczę tekst Podobne pytania", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 35
 testRunner.And("zobaczę tekst Wpisz swoje pytanie w pole poniżej.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wyświetlenie szczegółów najnowszej odpowiedzi")]
        public virtual void WyswietlenieSzczegolowNajnowszejOdpowiedzi()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wyświetlenie szczegółów najnowszej odpowiedzi", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 38
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 39
 testRunner.When("kliknę przycisk Katalog odpowiedzi", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 40
 testRunner.And("kliknę przycisk Zamknięte pytanie testowe sanitized", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 41
 testRunner.Then("zobaczę tekst Odpowiedź testowa sanitized", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 42
 testRunner.And("zobaczę tekst Zadaj swoje pytanie ekspertowi test-expert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wyświetlenie szczegółów wyszukiwanego zwrotu")]
        public virtual void WyswietlenieSzczegolowWyszukiwanegoZwrotu()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wyświetlenie szczegółów wyszukiwanego zwrotu", ((string[])(null)));
#line 44
this.ScenarioSetup(scenarioInfo);
#line 45
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 46
 testRunner.When("kliknę przycisk Popularne frazy", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 47
 testRunner.And("kliknę przycisk fraza", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 48
 testRunner.Then("zobaczę tekst Fraza: fraza", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 49
 testRunner.And("zobaczę tekst Wyszukiwany zwrot z kategorii Nauka", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 50
 testRunner.And("zobaczę tekst Wyszukana przez Ciebie fraza związana jest z jedną z dziedzin w któ" +
                    "rej specjalizują się nasi eksperci.", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 51
 testRunner.And("zobaczę tekst Eksperci z kategorii", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 52
 testRunner.And("zobaczę tekst Podobne odpowiedzi", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wyświetlenie szczegółów popularnych ekspertów")]
        public virtual void WyswietlenieSzczegolowPopularnychEkspertow()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wyświetlenie szczegółów popularnych ekspertów", ((string[])(null)));
#line 54
this.ScenarioSetup(scenarioInfo);
#line 55
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 56
 testRunner.When("kliknę przycisk Popularni eksperci", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 57
 testRunner.And("kliknę przycisk ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 58
 testRunner.Then("zobaczę tekst Wyszukana przez Ciebie fraza związana jest z jedną z dziedzin w któ" +
                    "rej specjalizują się nasi eksperci", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 59
 testRunner.And("zobaczę tekst Eksperci z kategorii", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 60
 testRunner.And("zobaczę tekst Rekomendowani eksperci", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 61
 testRunner.And("zobaczę tekst Zapytaj mnie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Przekierowanie do wizytówki eksperta z popularnych ekspertów")]
        public virtual void PrzekierowanieDoWizytowkiEkspertaZPopularnychEkspertow()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Przekierowanie do wizytówki eksperta z popularnych ekspertów", ((string[])(null)));
#line 63
this.ScenarioSetup(scenarioInfo);
#line 64
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 65
 testRunner.When("kliknę przycisk Popularni eksperci", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 66
 testRunner.And("kliknę przycisk ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 67
 testRunner.And("kliknę przycisk test-expert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 68
 testRunner.Then("zobaczę tekst zweryfikowany ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 69
 testRunner.And("zobaczę tekst Zadaj mi swoje pytanie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 70
 testRunner.And("zobaczę tekst Jestem ekspertem z dziedziny", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Przekierowanie do wizytówki eksperta z popularnych tematów pytań")]
        public virtual void PrzekierowanieDoWizytowkiEkspertaZPopularnychTematowPytan()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Przekierowanie do wizytówki eksperta z popularnych tematów pytań", ((string[])(null)));
#line 72
this.ScenarioSetup(scenarioInfo);
#line 73
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 74
 testRunner.When("kliknę przycisk Popularne tematy pytań", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 75
 testRunner.And("kliknę przycisk pytanie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 76
 testRunner.And("kliknę przycisk test-expert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 77
 testRunner.Then("zobaczę tekst zweryfikowany ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
