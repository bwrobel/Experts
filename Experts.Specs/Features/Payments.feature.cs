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
    [NUnit.Framework.DescriptionAttribute("Płatności")]
    public partial class PlatnosciFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Payments.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pl-PL"), "Płatności", "", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("Poprawne opłacenie pytania")]
        public virtual void PoprawneOplaceniePytania()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Poprawne opłacenie pytania", ((string[])(null)));
#line 3
this.ScenarioSetup(scenarioInfo);
#line 4
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 5
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 6
 testRunner.When("wybiorę kategorię pytania Dom oraz wpiszę treść Pytanie testowe", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 7
 testRunner.And("kliknę przycisk Zadaj pytanie ekspertowi!", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 8
 testRunner.And("kliknę przycisk Pomiń ten krok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 9
 testRunner.And("wpiszę do pola Imię tekst Bolek", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 10
 testRunner.And("wpiszę do pola Nazwisko tekst Lolek", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 11
 testRunner.And("wpiszę do pola Adres e-mail tekst bolek@lolek.pl", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 12
 testRunner.And("kliknę przycisk Uzyskaj odpowiedź", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 13
 testRunner.Then("znajdę się na zewnętrznej stronie https://secure.transferuj.pl/", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Błędne opłacenie pytania: brak danych")]
        public virtual void BledneOplaceniePytaniaBrakDanych()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Błędne opłacenie pytania: brak danych", ((string[])(null)));
#line 15
this.ScenarioSetup(scenarioInfo);
#line 16
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 17
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 18
 testRunner.When("wybiorę kategorię pytania Dom oraz wpiszę treść Pytanie testowe", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 19
 testRunner.And("kliknę przycisk Zadaj pytanie ekspertowi!", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 20
 testRunner.And("kliknę przycisk Pomiń ten krok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 21
 testRunner.And("kliknę przycisk Uzyskaj odpowiedź", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 22
 testRunner.Then("zobaczę tekst Ekspert proponuje wartość odpowiedzi", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Strona bilansu")]
        public virtual void StronaBilansu()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Strona bilansu", ((string[])(null)));
#line 24
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("że jestem zalogowany jako ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 26
 testRunner.When("kliknę przycisk Moje konto", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 27
 testRunner.And("kliknę przycisk Moje płatności", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 28
 testRunner.Then("znajdę się na stronie /bilans", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Szczegóły bilansu: pytający")]
        public virtual void SzczegolyBilansuPytajacy()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Szczegóły bilansu: pytający", ((string[])(null)));
#line 30
this.ScenarioSetup(scenarioInfo);
#line 31
 testRunner.Given("że użytkownik zadał pytanie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 32
 testRunner.When("przejdę do /bilans", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 33
 testRunner.Then("zobaczę tekst Wpłata na poczet pytania \'Pytanie testowe...\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Szczegóły bilansu: ekspert")]
        public virtual void SzczegolyBilansuEkspert()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Szczegóły bilansu: ekspert", ((string[])(null)));
#line 35
this.ScenarioSetup(scenarioInfo);
#line 36
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 37
 testRunner.When("przejdę do /moje-pytania", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 38
 testRunner.And("kliknę przycisk Pytanie testowe z ekspertem", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 39
 testRunner.And("kliknę przycisk Zaakceptuj odpowiedź", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 40
 testRunner.And("kliknę przycisk Zaakceptuj bez oceny", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 41
 testRunner.And("odświeżę stronę", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 42
 testRunner.And("kliknę przycisk Wyloguj się", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 43
 testRunner.And("zaloguję się jako ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 44
 testRunner.And("przejdę do /bilans", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 45
 testRunner.Then("zobaczę tekst Wynagrodzenie za pytanie \'Pytanie testowe z ekspertem...\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wypłata środków poprawna: ekspert")]
        public virtual void WyplataSrodkowPoprawnaEkspert()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wypłata środków poprawna: ekspert", ((string[])(null)));
#line 47
this.ScenarioSetup(scenarioInfo);
#line 48
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 49
 testRunner.When("przejdę do /moje-pytania", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 50
 testRunner.And("kliknę przycisk Pytanie testowe z ekspertem", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 51
 testRunner.And("kliknę przycisk Zaakceptuj odpowiedź", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 52
 testRunner.And("kliknę przycisk Zaakceptuj bez oceny", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 53
 testRunner.And("odświeżę stronę", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 54
 testRunner.And("kliknę przycisk Wyloguj się", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 55
 testRunner.And("zaloguję się jako ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 56
 testRunner.And("przejdę do /bilans", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 57
 testRunner.And("kliknę przycisk Wypłać środki", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 58
 testRunner.And("kliknę przycisk Zleć wypłatę środków", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 59
 testRunner.Then("zobaczę tekst Przyjęto zlecenie wypłaty", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line 60
 testRunner.And("zobaczę tekst Zlecenie wypłaty środków na życzenie klienta", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wypłata środków brak konta: ekspert")]
        public virtual void WyplataSrodkowBrakKontaEkspert()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wypłata środków brak konta: ekspert", ((string[])(null)));
#line 62
this.ScenarioSetup(scenarioInfo);
#line 63
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 64
 testRunner.When("przejdę do /moje-pytania", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 65
 testRunner.And("kliknę przycisk Pytanie testowe z ekspertem", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 66
 testRunner.And("kliknę przycisk Zaakceptuj odpowiedź", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 67
 testRunner.And("kliknę przycisk Zaakceptuj bez oceny", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 68
 testRunner.And("odświeżę stronę", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 69
 testRunner.And("kliknę przycisk Wyloguj się", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 70
 testRunner.And("zaloguję się jako ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 71
 testRunner.And("przejdę do /bilans", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 72
 testRunner.And("kliknę przycisk Wypłać środki", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 73
 testRunner.And("usunę z pola Numer konta bankowego do wpłaty jego zawartość", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 74
 testRunner.And("kliknę przycisk Zleć wypłatę środków", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 75
 testRunner.Then("zobaczę tekst Podaj numer konta bankowego", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Wypłata środków brak wartości: ekspert")]
        public virtual void WyplataSrodkowBrakWartosciEkspert()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Wypłata środków brak wartości: ekspert", ((string[])(null)));
#line 77
this.ScenarioSetup(scenarioInfo);
#line 78
 testRunner.Given("że jestem zalogowany jako użytkownik", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 79
 testRunner.When("przejdę do /moje-pytania", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 80
 testRunner.And("kliknę przycisk Pytanie testowe z ekspertem", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 81
 testRunner.And("kliknę przycisk Zaakceptuj odpowiedź", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 82
 testRunner.And("kliknę przycisk Zaakceptuj bez oceny", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 83
 testRunner.And("odświeżę stronę", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 84
 testRunner.And("kliknę przycisk Wyloguj się", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 85
 testRunner.And("zaloguję się jako ekspert", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 86
 testRunner.And("przejdę do /bilans", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 87
 testRunner.And("kliknę przycisk Wypłać środki", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 88
 testRunner.And("usunę z pola Wartość jego zawartość", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 89
 testRunner.And("kliknę przycisk Zleć wypłatę środków", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 90
 testRunner.Then("zobaczę tekst Podaj wartość przelewu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Autouzupełnianie formularza płatności z ostatniej wizyty")]
        public virtual void AutouzupelnianieFormularzaPlatnosciZOstatniejWizyty()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Autouzupełnianie formularza płatności z ostatniej wizyty", ((string[])(null)));
#line 92
this.ScenarioSetup(scenarioInfo);
#line 93
 testRunner.Given("że użytkownik zadał pytanie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Zakładając ");
#line 94
 testRunner.When("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 95
 testRunner.When("wybiorę kategorię pytania Dom oraz wpiszę treść Pytanie testowe nr 2", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 96
 testRunner.And("kliknę przycisk Zadaj pytanie ekspertowi!", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 97
 testRunner.And("kliknę przycisk Pomiń ten krok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 98
 testRunner.And("wpiszę do pola Nazwisko tekst ZMIENIONE NAZWISKO", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 99
 testRunner.And("kliknę przycisk Uzyskaj odpowiedź", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 100
 testRunner.And("przejdę do /", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 101
 testRunner.When("wybiorę kategorię pytania Dom oraz wpiszę treść Pytanie testowe nr 3", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Jeśli ");
#line 102
 testRunner.And("kliknę przycisk Zadaj pytanie ekspertowi!", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 103
 testRunner.And("kliknę przycisk Pomiń ten krok", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Oraz ");
#line 104
 testRunner.Then("zobaczę w elemencie tekst ZMIENIONE NAZWISKO", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Wtedy ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion