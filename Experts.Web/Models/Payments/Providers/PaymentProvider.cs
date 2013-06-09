using System.Collections.Specialized;
using System.Web;

namespace Experts.Web.Models.Payments.Providers
{
    public abstract class PaymentProvider
    {
        public abstract string ProviderFormPartialView { get; }
        public abstract bool AuthenticateResult(HttpRequestBase request);
        public abstract PaymentResult ParseResult(NameValueCollection parameters);
        public abstract string RequestUrl { get; }
        public abstract string ChannelSelectPartialView { get; } // can be set to null if no channel select allowed
        public abstract string TermsAndConditionsText { get;  }
    }
}