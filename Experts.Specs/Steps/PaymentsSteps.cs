using TechTalk.SpecFlow;

namespace Experts.Specs.Steps
{
    [Binding]
    public class PaymentsSteps
    {
        [When(@"wypełnię formularz płatności")]
        public void WhenIFillPaymentForm()
        {
            var shared = new SharedSteps();

            shared.WhenITypeIntoField("Imię", "T4JNY_c0de#");
            shared.WhenITypeIntoField("Nazwisko", "test");
            shared.WhenITypeIntoField("Adres e-mail", "test@asknuts.com");
            //shared.WhenIClickCheckboxById("PaymentForm_Policy"); TODO: zrobić tak żeby działało zarówno dla zalogowanego jak i niezalogowanego
        }
    }
}
