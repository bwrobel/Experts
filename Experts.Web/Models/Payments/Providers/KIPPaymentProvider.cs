using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace Experts.Web.Models.Payments.Providers
{
    public class KIPPaymentProvider : PaymentProvider
    {
        public override string ProviderFormPartialView
        {
            get { return MVC.Payment.Views._KIPProviderForm; }
        }

        public override string ChannelSelectPartialView
        {
            get { return MVC.Payment.Views._KIPProviderChannelSelect; }
        }

        public override string TermsAndConditionsText
        {
            get { return Resources.Payment.KipProviderTermsAndConditions; }
        }

        public override bool AuthenticateResult(HttpRequestBase request)
        {
            //TODO: Dodać weryfikację MD5
            return request.ServerVariables["REMOTE_ADDR"] == "195.149.229.109";
        }

        public override PaymentResult ParseResult(NameValueCollection parameters)
        {
            var result = new PaymentResult
                             {
                                 ResponseResult = new ContentResult {Content = "TRUE"},
                                 IsSuccessfull = parameters["tr_status"] == "TRUE",
                                 PaymentId = int.Parse(parameters["tr_crc"]),
                                 ProviderPaymentId = parameters["tr_id"]
                             };

            return result;
        }

        public override string RequestUrl
        {
            get { return "https://secure.transferuj.pl"; }
        }

        public string SellerId { get { return ConfigurationManager.AppSettings["Payments.KIPSellerId"]; } }
    }
}