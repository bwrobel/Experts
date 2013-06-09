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
    public partial class DevelopmentController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DevelopmentController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected DevelopmentController(Dummy d) { }

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


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public DevelopmentController Actions { get { return MVC.Development; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Development";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Development";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Exception = "Exception";
            public readonly string RefreshDataFresh = "RefreshDataFresh";
            public readonly string LastError = "LastError";
            public readonly string GenerateData = "GenerateData";
            public readonly string GeneratePartnerScript = "GeneratePartnerScript";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Exception = "Exception";
            public const string RefreshDataFresh = "RefreshDataFresh";
            public const string LastError = "LastError";
            public const string GenerateData = "GenerateData";
            public const string GeneratePartnerScript = "GeneratePartnerScript";
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
                public readonly string PartnerScript = "PartnerScript";
            }
            public readonly string PartnerScript = "~/Views/Development/PartnerScript.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_DevelopmentController : Experts.Web.Controllers.DevelopmentController
    {
        public T4MVC_DevelopmentController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult Exception()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Exception);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult RefreshDataFresh()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RefreshDataFresh);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult LastError()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.LastError);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GenerateData()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GenerateData);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GeneratePartnerScript()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GeneratePartnerScript);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591