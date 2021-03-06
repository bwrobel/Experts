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
    public partial class AdministrationController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AdministrationController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected AdministrationController(Dummy d) { }

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
        public System.Web.Mvc.ActionResult EventReactionForm()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EventReactionForm);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult SystemBreakdownReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SystemBreakdownReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UserFailureReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserFailureReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ExpertQualificationsChangedReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExpertQualificationsChangedReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ThreadIssueReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ThreadIssueReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult CashPaymentReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CashPaymentReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ExpertPublicDataChangedReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExpertPublicDataChangedReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult NoAnswerReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NoAnswerReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult ExpertPriceProposalReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExpertPriceProposalReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult NewAdditionalServiceReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NewAdditionalServiceReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult BecomePartnerRequestReaction()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BecomePartnerRequestReaction);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult CreateCategoryAttribute()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CreateCategoryAttribute);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult CreateChildCategoryAttribute()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CreateChildCategoryAttribute);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult EditCategoryAttribute()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditCategoryAttribute);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult DeleteCategoryAttribute()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteCategoryAttribute);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult DeleteCategoryAttributeOption()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteCategoryAttributeOption);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult AddCategoryAttributeOption()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddCategoryAttributeOption);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult UpdateCategoryAttributeOption()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCategoryAttributeOption);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public AdministrationController Actions { get { return MVC.Administration; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Administration";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Administration";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string ModeratorMenu = "ModeratorMenu";
            public readonly string EventsLog = "EventsLog";
            public readonly string EventReactionForm = "EventReactionForm";
            public readonly string SystemBreakdownReaction = "SystemBreakdownReaction";
            public readonly string UserFailureReaction = "UserFailureReaction";
            public readonly string ExpertQualificationsChangedReaction = "ExpertQualificationsChangedReaction";
            public readonly string ThreadIssueReaction = "ThreadIssueReaction";
            public readonly string CashPaymentReaction = "CashPaymentReaction";
            public readonly string ExpertPublicDataChangedReaction = "ExpertPublicDataChangedReaction";
            public readonly string NoAnswerReaction = "NoAnswerReaction";
            public readonly string ExpertPriceProposalReaction = "ExpertPriceProposalReaction";
            public readonly string NewAdditionalServiceReaction = "NewAdditionalServiceReaction";
            public readonly string BecomePartnerRequestReaction = "BecomePartnerRequestReaction";
            public readonly string UserList = "UserList";
            public readonly string CategoryList = "CategoryList";
            public readonly string CreateCategoryAttribute = "CreateCategoryAttribute";
            public readonly string CreateChildCategoryAttribute = "CreateChildCategoryAttribute";
            public readonly string EditCategoryAttribute = "EditCategoryAttribute";
            public readonly string DeleteCategoryAttribute = "DeleteCategoryAttribute";
            public readonly string DeleteCategoryAttributeOption = "DeleteCategoryAttributeOption";
            public readonly string AddCategoryAttributeOption = "AddCategoryAttributeOption";
            public readonly string UpdateCategoryAttributeOption = "UpdateCategoryAttributeOption";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string ModeratorMenu = "ModeratorMenu";
            public const string EventsLog = "EventsLog";
            public const string EventReactionForm = "EventReactionForm";
            public const string SystemBreakdownReaction = "SystemBreakdownReaction";
            public const string UserFailureReaction = "UserFailureReaction";
            public const string ExpertQualificationsChangedReaction = "ExpertQualificationsChangedReaction";
            public const string ThreadIssueReaction = "ThreadIssueReaction";
            public const string CashPaymentReaction = "CashPaymentReaction";
            public const string ExpertPublicDataChangedReaction = "ExpertPublicDataChangedReaction";
            public const string NoAnswerReaction = "NoAnswerReaction";
            public const string ExpertPriceProposalReaction = "ExpertPriceProposalReaction";
            public const string NewAdditionalServiceReaction = "NewAdditionalServiceReaction";
            public const string BecomePartnerRequestReaction = "BecomePartnerRequestReaction";
            public const string UserList = "UserList";
            public const string CategoryList = "CategoryList";
            public const string CreateCategoryAttribute = "CreateCategoryAttribute";
            public const string CreateChildCategoryAttribute = "CreateChildCategoryAttribute";
            public const string EditCategoryAttribute = "EditCategoryAttribute";
            public const string DeleteCategoryAttribute = "DeleteCategoryAttribute";
            public const string DeleteCategoryAttributeOption = "DeleteCategoryAttributeOption";
            public const string AddCategoryAttributeOption = "AddCategoryAttributeOption";
            public const string UpdateCategoryAttributeOption = "UpdateCategoryAttributeOption";
        }


        static readonly ActionParamsClass_EventsLog s_params_EventsLog = new ActionParamsClass_EventsLog();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EventsLog EventsLogParams { get { return s_params_EventsLog; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EventsLog
        {
            public readonly string page = "page";
            public readonly string threadId = "threadId";
        }
        static readonly ActionParamsClass_EventReactionForm s_params_EventReactionForm = new ActionParamsClass_EventReactionForm();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EventReactionForm EventReactionFormParams { get { return s_params_EventReactionForm; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EventReactionForm
        {
            public readonly string eventId = "eventId";
        }
        static readonly ActionParamsClass_SystemBreakdownReaction s_params_SystemBreakdownReaction = new ActionParamsClass_SystemBreakdownReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SystemBreakdownReaction SystemBreakdownReactionParams { get { return s_params_SystemBreakdownReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SystemBreakdownReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_UserFailureReaction s_params_UserFailureReaction = new ActionParamsClass_UserFailureReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UserFailureReaction UserFailureReactionParams { get { return s_params_UserFailureReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UserFailureReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ExpertQualificationsChangedReaction s_params_ExpertQualificationsChangedReaction = new ActionParamsClass_ExpertQualificationsChangedReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExpertQualificationsChangedReaction ExpertQualificationsChangedReactionParams { get { return s_params_ExpertQualificationsChangedReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExpertQualificationsChangedReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ThreadIssueReaction s_params_ThreadIssueReaction = new ActionParamsClass_ThreadIssueReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ThreadIssueReaction ThreadIssueReactionParams { get { return s_params_ThreadIssueReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ThreadIssueReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_CashPaymentReaction s_params_CashPaymentReaction = new ActionParamsClass_CashPaymentReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CashPaymentReaction CashPaymentReactionParams { get { return s_params_CashPaymentReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CashPaymentReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ExpertPublicDataChangedReaction s_params_ExpertPublicDataChangedReaction = new ActionParamsClass_ExpertPublicDataChangedReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExpertPublicDataChangedReaction ExpertPublicDataChangedReactionParams { get { return s_params_ExpertPublicDataChangedReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExpertPublicDataChangedReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_NoAnswerReaction s_params_NoAnswerReaction = new ActionParamsClass_NoAnswerReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NoAnswerReaction NoAnswerReactionParams { get { return s_params_NoAnswerReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NoAnswerReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_ExpertPriceProposalReaction s_params_ExpertPriceProposalReaction = new ActionParamsClass_ExpertPriceProposalReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ExpertPriceProposalReaction ExpertPriceProposalReactionParams { get { return s_params_ExpertPriceProposalReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ExpertPriceProposalReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_NewAdditionalServiceReaction s_params_NewAdditionalServiceReaction = new ActionParamsClass_NewAdditionalServiceReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NewAdditionalServiceReaction NewAdditionalServiceReactionParams { get { return s_params_NewAdditionalServiceReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NewAdditionalServiceReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_BecomePartnerRequestReaction s_params_BecomePartnerRequestReaction = new ActionParamsClass_BecomePartnerRequestReaction();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_BecomePartnerRequestReaction BecomePartnerRequestReactionParams { get { return s_params_BecomePartnerRequestReaction; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_BecomePartnerRequestReaction
        {
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_UserList s_params_UserList = new ActionParamsClass_UserList();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UserList UserListParams { get { return s_params_UserList; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UserList
        {
            public readonly string sortOptions = "sortOptions";
            public readonly string page = "page";
        }
        static readonly ActionParamsClass_CreateCategoryAttribute s_params_CreateCategoryAttribute = new ActionParamsClass_CreateCategoryAttribute();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CreateCategoryAttribute CreateCategoryAttributeParams { get { return s_params_CreateCategoryAttribute; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CreateCategoryAttribute
        {
            public readonly string categoryId = "categoryId";
            public readonly string form = "form";
        }
        static readonly ActionParamsClass_CreateChildCategoryAttribute s_params_CreateChildCategoryAttribute = new ActionParamsClass_CreateChildCategoryAttribute();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_CreateChildCategoryAttribute CreateChildCategoryAttributeParams { get { return s_params_CreateChildCategoryAttribute; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_CreateChildCategoryAttribute
        {
            public readonly string parentCategoryAttributeId = "parentCategoryAttributeId";
        }
        static readonly ActionParamsClass_EditCategoryAttribute s_params_EditCategoryAttribute = new ActionParamsClass_EditCategoryAttribute();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditCategoryAttribute EditCategoryAttributeParams { get { return s_params_EditCategoryAttribute; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditCategoryAttribute
        {
            public readonly string attributeId = "attributeId";
            public readonly string parentAttributeId = "parentAttributeId";
            public readonly string form = "form";
        }
        static readonly ActionParamsClass_DeleteCategoryAttribute s_params_DeleteCategoryAttribute = new ActionParamsClass_DeleteCategoryAttribute();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteCategoryAttribute DeleteCategoryAttributeParams { get { return s_params_DeleteCategoryAttribute; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteCategoryAttribute
        {
            public readonly string attributeId = "attributeId";
        }
        static readonly ActionParamsClass_DeleteCategoryAttributeOption s_params_DeleteCategoryAttributeOption = new ActionParamsClass_DeleteCategoryAttributeOption();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_DeleteCategoryAttributeOption DeleteCategoryAttributeOptionParams { get { return s_params_DeleteCategoryAttributeOption; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_DeleteCategoryAttributeOption
        {
            public readonly string optionId = "optionId";
        }
        static readonly ActionParamsClass_AddCategoryAttributeOption s_params_AddCategoryAttributeOption = new ActionParamsClass_AddCategoryAttributeOption();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_AddCategoryAttributeOption AddCategoryAttributeOptionParams { get { return s_params_AddCategoryAttributeOption; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_AddCategoryAttributeOption
        {
            public readonly string categoryAttributeId = "categoryAttributeId";
            public readonly string option = "option";
        }
        static readonly ActionParamsClass_UpdateCategoryAttributeOption s_params_UpdateCategoryAttributeOption = new ActionParamsClass_UpdateCategoryAttributeOption();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_UpdateCategoryAttributeOption UpdateCategoryAttributeOptionParams { get { return s_params_UpdateCategoryAttributeOption; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_UpdateCategoryAttributeOption
        {
            public readonly string categoryAttributeId = "categoryAttributeId";
            public readonly string updatedOption = "updatedOption";
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
                public readonly string _CategoryAttributeOptionsEditor = "_CategoryAttributeOptionsEditor";
                public readonly string _CategoryAttributeOptionsList = "_CategoryAttributeOptionsList";
                public readonly string _SpecificEvents = "_SpecificEvents";
                public readonly string _TopMenuModerator = "_TopMenuModerator";
                public readonly string CategoryList = "CategoryList";
                public readonly string CreateCategoryAttribute = "CreateCategoryAttribute";
                public readonly string EditCategoryAttribute = "EditCategoryAttribute";
                public readonly string EventsLog = "EventsLog";
                public readonly string UserList = "UserList";
            }
            public readonly string _CategoryAttributeOptionsEditor = "~/Views/Administration/_CategoryAttributeOptionsEditor.cshtml";
            public readonly string _CategoryAttributeOptionsList = "~/Views/Administration/_CategoryAttributeOptionsList.cshtml";
            public readonly string _SpecificEvents = "~/Views/Administration/_SpecificEvents.cshtml";
            public readonly string _TopMenuModerator = "~/Views/Administration/_TopMenuModerator.cshtml";
            public readonly string CategoryList = "~/Views/Administration/CategoryList.cshtml";
            public readonly string CreateCategoryAttribute = "~/Views/Administration/CreateCategoryAttribute.cshtml";
            public readonly string EditCategoryAttribute = "~/Views/Administration/EditCategoryAttribute.cshtml";
            public readonly string EventsLog = "~/Views/Administration/EventsLog.cshtml";
            public readonly string UserList = "~/Views/Administration/UserList.cshtml";
            static readonly _EventsClass s_Events = new _EventsClass();
            public _EventsClass Events { get { return s_Events; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _EventsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string BecomePartnerRequestReaction = "BecomePartnerRequestReaction";
                    public readonly string CashPaymentReaction = "CashPaymentReaction";
                    public readonly string ExpertPriceProposalReaction = "ExpertPriceProposalReaction";
                    public readonly string ExpertQualificationsChangedReaction = "ExpertQualificationsChangedReaction";
                    public readonly string NewAdditionalServiceReactionModel = "NewAdditionalServiceReactionModel";
                    public readonly string NoAnswerReaction = "NoAnswerReaction";
                    public readonly string SystemBreakdownReaction = "SystemBreakdownReaction";
                    public readonly string ThreadIssueReaction = "ThreadIssueReaction";
                    public readonly string UserFailureReaction = "UserFailureReaction";
                }
                public readonly string BecomePartnerRequestReaction = "~/Views/Administration/Events/BecomePartnerRequestReaction.cshtml";
                public readonly string CashPaymentReaction = "~/Views/Administration/Events/CashPaymentReaction.cshtml";
                public readonly string ExpertPriceProposalReaction = "~/Views/Administration/Events/ExpertPriceProposalReaction.cshtml";
                public readonly string ExpertQualificationsChangedReaction = "~/Views/Administration/Events/ExpertQualificationsChangedReaction.cshtml";
                public readonly string NewAdditionalServiceReactionModel = "~/Views/Administration/Events/NewAdditionalServiceReactionModel.cshtml";
                public readonly string NoAnswerReaction = "~/Views/Administration/Events/NoAnswerReaction.cshtml";
                public readonly string SystemBreakdownReaction = "~/Views/Administration/Events/SystemBreakdownReaction.cshtml";
                public readonly string ThreadIssueReaction = "~/Views/Administration/Events/ThreadIssueReaction.cshtml";
                public readonly string UserFailureReaction = "~/Views/Administration/Events/UserFailureReaction.cshtml";
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_AdministrationController : Experts.Web.Controllers.AdministrationController
    {
        public T4MVC_AdministrationController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult ModeratorMenu()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ModeratorMenu);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EventsLog(int? page, int? threadId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EventsLog);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "threadId", threadId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EventReactionForm(int eventId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EventReactionForm);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "eventId", eventId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult SystemBreakdownReaction(Experts.Web.Models.Events.SystemBreakdownReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SystemBreakdownReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UserFailureReaction(Experts.Web.Models.Events.UserFailureReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserFailureReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ExpertQualificationsChangedReaction(Experts.Web.Models.Events.ExpertQualificationsChangedReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExpertQualificationsChangedReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ThreadIssueReaction(Experts.Web.Models.Events.ThreadIssueReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ThreadIssueReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CashPaymentReaction(Experts.Web.Models.Events.CashPaymentReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CashPaymentReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ExpertPublicDataChangedReaction(Experts.Web.Models.Events.CashPaymentReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExpertPublicDataChangedReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult NoAnswerReaction(Experts.Web.Models.Events.NoAnswerReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NoAnswerReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult ExpertPriceProposalReaction(Experts.Web.Models.Events.ExpertPriceProposalReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ExpertPriceProposalReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult NewAdditionalServiceReaction(Experts.Web.Models.Events.NewAdditionalServiceReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NewAdditionalServiceReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult BecomePartnerRequestReaction(Experts.Web.Models.Events.BecomePartnerRequestReactionModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.BecomePartnerRequestReaction);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UserList(MvcContrib.UI.Grid.GridSortOptions sortOptions, int? page)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UserList);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "sortOptions", sortOptions);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CategoryList()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CategoryList);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreateCategoryAttribute(int categoryId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CreateCategoryAttribute);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryId", categoryId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreateChildCategoryAttribute(int parentCategoryAttributeId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CreateChildCategoryAttribute);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "parentCategoryAttributeId", parentCategoryAttributeId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreateCategoryAttribute(Experts.Web.Models.Forms.CategoryAttributeForm form)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.CreateCategoryAttribute);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "form", form);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EditCategoryAttribute(int attributeId, int? parentAttributeId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditCategoryAttribute);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "attributeId", attributeId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "parentAttributeId", parentAttributeId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult EditCategoryAttribute(Experts.Web.Models.Forms.CategoryAttributeForm form)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditCategoryAttribute);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "form", form);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DeleteCategoryAttribute(int attributeId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteCategoryAttribute);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "attributeId", attributeId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DeleteCategoryAttributeOption(int optionId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.DeleteCategoryAttributeOption);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "optionId", optionId);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult AddCategoryAttributeOption(int categoryAttributeId, Experts.Core.Entities.CategoryAttributeOption option)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AddCategoryAttributeOption);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryAttributeId", categoryAttributeId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "option", option);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult UpdateCategoryAttributeOption(int categoryAttributeId, Experts.Core.Entities.CategoryAttributeOption updatedOption)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.UpdateCategoryAttributeOption);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "categoryAttributeId", categoryAttributeId);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "updatedOption", updatedOption);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
