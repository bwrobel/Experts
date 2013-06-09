// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Experts.Web.Controllers
{
    public partial class StaticPagesController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public StaticPagesController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected StaticPagesController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult AdCampaignHome()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AdCampaignHome);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public StaticPagesController Actions { get { return MVC.StaticPages; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "StaticPages";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "StaticPages";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Home = "Home";
            public readonly string AdCampaignHome = "AdCampaignHome";
            public readonly string TopMenu = "TopMenu";
            public readonly string MeetAsknuts = "MeetAsknuts";
            public readonly string PolicyPrivate = "PolicyPrivate";
            public readonly string PolicyWeb = "PolicyWeb";
            public readonly string PageNotFound = "PageNotFound";
            public readonly string CategoryDescription = "CategoryDescription";
            public readonly string About = "About";
            public readonly string _HelpQuestionStandard = "_HelpQuestionStandard";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Home = "Home";
            public const string AdCampaignHome = "AdCampaignHome";
            public const string TopMenu = "TopMenu";
            public const string MeetAsknuts = "MeetAsknuts";
            public const string PolicyPrivate = "PolicyPrivate";
            public const string PolicyWeb = "PolicyWeb";
            public const string PageNotFound = "PageNotFound";
            public const string CategoryDescription = "CategoryDescription";
            public const string About = "About";
            public const string _HelpQuestionStandard = "_HelpQuestionStandard";
        }


        static readonly ActionParamsClass_AdCampaignHome s_params_AdCampaignHome = new ActionParamsClass_AdCampaignHome();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AdCampaignHome AdCampaignHomeParams { get { return s_params_AdCampaignHome; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AdCampaignHome
        {
            public readonly string code = "code";
        }
        static readonly ActionParamsClass_MeetAsknuts s_params_MeetAsknuts = new ActionParamsClass_MeetAsknuts();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_MeetAsknuts MeetAsknutsParams { get { return s_params_MeetAsknuts; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_MeetAsknuts
        {
            public readonly string anhor = "anhor";
        }
        static readonly ActionParamsClass_CategoryDescription s_params_CategoryDescription = new ActionParamsClass_CategoryDescription();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CategoryDescription CategoryDescriptionParams { get { return s_params_CategoryDescription; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CategoryDescription
        {
            public readonly string categoryId = "categoryId";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _HelpQuestionStandard = "_HelpQuestionStandard";
                public readonly string _MeetAskNutsHint = "_MeetAskNutsHint";
                public readonly string About = "About";
                public readonly string AdCampaignHome = "AdCampaignHome";
                public readonly string ExpertFAQ = "ExpertFAQ";
                public readonly string Home = "Home";
                public readonly string MeetAsknuts = "MeetAsknuts";
                public readonly string PolicyPrivate = "PolicyPrivate";
                public readonly string PolicyWeb = "PolicyWeb";
            }
            public readonly string _HelpQuestionStandard = "~/Views/StaticPages/_HelpQuestionStandard.cshtml";
            public readonly string _MeetAskNutsHint = "~/Views/StaticPages/_MeetAskNutsHint.cshtml";
            public readonly string About = "~/Views/StaticPages/About.cshtml";
            public readonly string AdCampaignHome = "~/Views/StaticPages/AdCampaignHome.cshtml";
            public readonly string ExpertFAQ = "~/Views/StaticPages/ExpertFAQ.cshtml";
            public readonly string Home = "~/Views/StaticPages/Home.cshtml";
            public readonly string MeetAsknuts = "~/Views/StaticPages/MeetAsknuts.cshtml";
            public readonly string PolicyPrivate = "~/Views/StaticPages/PolicyPrivate.cshtml";
            public readonly string PolicyWeb = "~/Views/StaticPages/PolicyWeb.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_StaticPagesController : Experts.Web.Controllers.StaticPagesController
    {
        public T4MVC_StaticPagesController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Home()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Home);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AdCampaignHome(string code)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AdCampaignHome);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "code", code);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult TopMenu()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.TopMenu);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult MeetAsknuts(string anhor)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MeetAsknuts);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "anhor", anhor);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult PolicyPrivate()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PolicyPrivate);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult PolicyWeb()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PolicyWeb);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult PageNotFound()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PageNotFound);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CategoryDescription(int? categoryId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CategoryDescription);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryId", categoryId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult About()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.About);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult _HelpQuestionStandard()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._HelpQuestionStandard);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
