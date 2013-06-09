using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Core.Repositories;
using Experts.Web.Filters;
using Experts.Web.Helpers;
using Experts.Web.Models.Catalog;
using Experts.Web.Models.Forms;
using Experts.Web.Models.Shared;
using Experts.Web.Models.Threads;
using MvcContrib.Sorting;
using MvcContrib.UI.Grid;
using MvcSiteMapProvider.Web;

namespace Experts.Web.Controllers
{
    public partial class CatalogController : BaseController
    {
        [DefaultRouting]
        [HttpPost]
        public virtual ActionResult ChildCategoryAttributes(
            [Bind(Prefix = "AttributeValues")] AttributeValueModel[] attributeValues, int attributeId)
        {
            var helper = new CategoryAttributeHelper();
            var categoryAttributeValues =
                helper.GetCategoryAttributeValues(attributeValues.Where(cv => cv.AttributeId == attributeId));
            var allSubAttributes = categoryAttributeValues.SelectMany(cav => cav.Attribute.ChildAttributes);
            var selectedSubAttributes =
                allSubAttributes.Where(
                    sa => categoryAttributeValues.Any(cav => cav.SelectedOptions.Intersect(sa.ParentOptions).Any()));

            var attributes =
                selectedSubAttributes.Where(
                    ca => ca.Type == CategoryAttributeType.MultiSelect || ca.Type == CategoryAttributeType.SingleSelect)
                    .ToList();

            var model = new KeywordProcessFormModel
                            {CategoryAttributes = attributes, CategoryAttributeValues = attributeValues};

            return PartialView(MVC.Catalog.Views._KeywordCategoryAttributes, model);
        }

        [AuthorizeRoles(Role.Moderator)]
        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult IsKeywordUnique(string keywordPhrase)
        {
            var isUnique = Repository.SEOKeyword.Find(k => k.Phrase == keywordPhrase) == null;
            return Content(isUnique.ToString());
        }

        [AuthorizeRoles(Role.Moderator)]
        [HttpPost]
        public virtual ActionResult AddKeyword(string keywordPhrase)
        {
            var keyword = new SEOKeyword
                              {
                                  Phrase = keywordPhrase,
                                  Source = SEOKeywordSource.Manual
                              };

            Repository.SEOKeyword.Add(keyword);

            return ProcessKeyword(new KeywordProcessForm {SeoKeywordPhrase = keywordPhrase, SeoKeywordId = keyword.Id});
        }


        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult ProcessKeyword(KeywordProcessForm form = null)
        {
            var count = Repository.SEOKeyword.Count(q => q.ByStatus(SEOKeywordStatus.New));
            var keyword = form.SeoKeywordId > 0
                              ? Repository.SEOKeyword.Get(form.SeoKeywordId)
                              : Repository.SEOKeyword.GetToModerate();

            if (keyword == null)
                return View(MVC.Catalog.Views.NoNewKeyword);

            var model = new KeywordProcessFormModel(keyword);

            model.AvailableCategories =
                Repository.Category.All().Select(c => new SelectListItem {Text = c.Name, Value = c.Id.ToString()});
            model.KeywordProcessForm.SeoKeywordType = keyword.Type;

            if (keyword.Category != null)
                model.KeywordProcessForm.CategoryId = keyword.Category.Id;

            model.NumberOfKeywordsToModerate = count;

            if (form.SeoKeywordId > 0)
                model.KeywordProcessForm = form;

            return View(MVC.Catalog.Views.ProcessKeyword, model);
        }

        [HttpPost]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult SetKeywordAttributes([Bind(Prefix = "KeywordProcessForm")] KeywordProcessForm form)
        {
            if (!ModelState.IsValid)
                return ProcessKeyword(form);

            var keyword = Repository.SEOKeyword.Get(form.SeoKeywordId);

            var category = Repository.Category.Get(form.CategoryId);
            var attributes =
                category.Attributes.Where(
                    ca => ca.Type == CategoryAttributeType.MultiSelect || ca.Type == CategoryAttributeType.SingleSelect)
                    .ToList();
            var values = CategoryAttributeHelper.GetCategoryAttributeValueModel(keyword.CategoryAttributes);

            var model = new KeywordProcessFormModel(keyword)
                            {
                                KeywordProcessForm = form,
                                CategoryAttributes = attributes,
                                CategoryAttributeValues = values
                            };

            return View(model);
        }

        [HttpPost]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult StoreKeyword([Bind(Prefix = "KeywordProcessForm")] KeywordProcessForm form)
        {
            var keyword = Repository.SEOKeyword.Get(form.SeoKeywordId);
            var category = Repository.Category.Get(form.CategoryId);

            keyword.Status = SEOKeywordStatus.Processed;
            keyword.Type = form.SeoKeywordType;
            keyword.Category = category;
            keyword.Phrase = form.SeoKeywordPhrase;

            var helper = new CategoryAttributeHelper();
            keyword.CategoryAttributes.Clear();
            keyword.CategoryAttributes.AddRange(helper.GetCategoryAttributeValues(form.AttributeValues));

            Repository.SEOKeyword.Update(keyword);

            Flash.Success(string.Format(Resources.Catalog.LandingPageCreated, keyword.Phrase));

            return RedirectToAction(MVC.Catalog.ProcessKeyword());
        }

        [DefaultRouting]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult BanKeyword(int id)
        {
            var keyword = Repository.SEOKeyword.Get(id);
            keyword.Status = SEOKeywordStatus.Blocked;

            Repository.SEOKeyword.Update(keyword);
            Flash.Warning(string.Format(Resources.Catalog.KeyPhraseBanned, keyword.Phrase));

            return RedirectToAction(MVC.Catalog.ProcessKeyword());
        }

        [DefaultRouting]
        [AuthorizeRoles(Role.Moderator)]
        public virtual ActionResult SetKeywordAsUndefined(int id)
        {
            var keyword = Repository.SEOKeyword.Get(id);
            keyword.Status = SEOKeywordStatus.Undefined;

            Repository.SEOKeyword.Update(keyword);
            Flash.Info(string.Format(Resources.Catalog.KeyPhraseSetAsUndefined, keyword.Phrase));

            return RedirectToAction(MVC.Catalog.ProcessKeyword());
        }

        private void PresetSeoKeyword(SEOKeyword currentKeyword)
        {
            if (RequestSeoKeyword != null)
            {
                RequestSeoKeyword.CategoryAttributes.AddRange(currentKeyword.CategoryAttributes);
                RequestSeoKeyword.Category = currentKeyword.Category;
                RequestSeoKeyword.Type = currentKeyword.Type;

                Repository.SEOKeyword.Update(RequestSeoKeyword);
            }
        }

        public virtual ActionResult PhraseDetails(int keywordId, string title)
        {
            var keyword = Repository.SEOKeyword.Get(keywordId);
            PresetSeoKeyword(keyword);

            var threads = Repository.Thread.FindClosestMatches(keyword.Category, keyword.CategoryAttributes,
                                                               PagerHelper.SimiliarThreadsListSize,
                                                               t =>
                                                               t.BySanitizationStatus(
                                                                   ThreadSanitizationStatus.Sanitized).ByInnerStatus(false));

            var form = new ThreadForm()
                           {
                               CategoryId = keyword.Category.Id,
                               AttributeValues =
                                   CategoryAttributeHelper.GetCategoryAttributeValueModel(keyword.CategoryAttributes),
                               SeoKeywordId = keywordId
                           };

            form.GenerateTemporaryAttachmentFolder();

            var seo = new SeoDetails()
                          {
                              Id = keyword.Id,
                              Phrase = keyword.Phrase,
                              Category = keyword.Category,
                              Type = keyword.Type,
                              Status = keyword.Status
                          };

            var categories = new List<Category>();
            categories.Add(keyword.Category);

            var model = new ThreadDetailsSeo(Repository.Category.All())
                            {
                                SeoDetails = seo,
                                Threads = threads,
                                ThreadForm = form,
                                AvailableCategories = categories
                            };

            ViewBag.PageTitle = model.SeoDetails.Phrase;

            return View(model);
        }

        public virtual ActionResult ExpertListSeo(int keywordId, string title)
        {
            var keyword = Repository.SEOKeyword.Get(keywordId);
            PresetSeoKeyword(keyword);

            var experts = Repository.Expert.FindClosestMatches(keyword.Category.Id, keyword.CategoryAttributes,
                                                               PagerHelper.SeoSimiliarExpertsLimit);

            var form = new ThreadForm()
                           {
                               CategoryId = keyword.Category.Id,
                               AttributeValues =
                                   CategoryAttributeHelper.GetCategoryAttributeValueModel(keyword.CategoryAttributes),
                               SeoKeywordId = keywordId
                           };

            form.GenerateTemporaryAttachmentFolder();

            var seo = new SeoDetails()
                          {
                              Id = keyword.Id,
                              Phrase = keyword.Phrase,
                              Category = keyword.Category,
                              Type = keyword.Type,
                              Status = keyword.Status
                          };

            var categories = new List<Category>();
            categories.Add(keyword.Category);

            var model = new ThreadDetailsSeo(Repository.Category.All())
                            {
                                SeoDetails = seo,
                                Experts = experts,
                                ThreadForm = form,
                                AvailableCategories = categories
                            };

            ViewBag.PageTitle = model.SeoDetails.Phrase;

            return View(model);
        }

        public virtual ActionResult QuestionListSeo(int keywordId, string title)
        {
            var keyword = Repository.SEOKeyword.Get(keywordId);
            PresetSeoKeyword(keyword);

            var threads = Repository.Thread.FindClosestMatches(keyword.Category,
                                                               keyword.CategoryAttributes,
                                                               PagerHelper.SeoSimiliarThreadsLimit,
                                                               t => t.BySanitizationStatus(ThreadSanitizationStatus.Sanitized).ByInnerStatus(false));

            var experts = Repository.Expert.FindClosestMatches(keyword.Category.Id,
                                                               keyword.CategoryAttributes,
                                                               PagerHelper.SimiliarExpertsListSize);

            var form = new ThreadForm()
                           {
                               Content = keyword.Phrase,
                               CategoryId = keyword.Category.Id,
                               AttributeValues =
                                   CategoryAttributeHelper.GetCategoryAttributeValueModel(keyword.CategoryAttributes),
                               SeoKeywordId = keywordId
                           };

            form.GenerateTemporaryAttachmentFolder();

            var seo = new SeoDetails()
                          {
                              Id = keyword.Id,
                              Phrase = keyword.Phrase,
                              Category = keyword.Category,
                              Type = keyword.Type,
                              Status = keyword.Status
                          };

            var categories = new List<Category>();
            categories.Add(keyword.Category);

            var model = new ThreadDetailsSeo()
                            {
                                SeoDetails = seo,
                                ThreadForm = form,
                                Threads = threads,
                                Experts = experts,
                                AvailableCategories = categories
                            };

            ViewBag.PageTitle = model.SeoDetails.Phrase;

            return View(model);
        }

        public virtual ActionResult KeywordListExperts(int? page = null)
        {
            var keywords = Repository.SEOKeyword.Find(PagerHelper.KeywordTilesSize, page ?? 1, t => t.ByType(SEOKeywordType.Expert).ByStatus(SEOKeywordStatus.Processed)).ToList();

            if (Request.IsAjaxRequest())
                return PartialView(MVC.Thread.Views._KeywordTilesItems, keywords);

            return View(keywords);
        }

        public virtual ActionResult KeywordListPhrases(int? page = null)
        {
            var keywords = Repository.SEOKeyword.Find(PagerHelper.KeywordTilesSize, page ?? 1, t => t.ByType(SEOKeywordType.Phrase).ByStatus(SEOKeywordStatus.Processed)).ToList();

            if (Request.IsAjaxRequest())
                return PartialView(MVC.Thread.Views._KeywordTilesItems, keywords);

            return View(keywords);
        }

        public virtual ActionResult KeywordListQuestions(int? page = null)
        {
            var keywords = Repository.SEOKeyword.Find(PagerHelper.KeywordTilesSize, page ?? 1, t => t.ByType(SEOKeywordType.Question).ByStatus(SEOKeywordStatus.Processed)).ToList();

            if (Request.IsAjaxRequest())
                return PartialView(MVC.Thread.Views._KeywordTilesItems, keywords);

            return View(keywords);
        }

        public virtual ActionResult SiteMapXml()
        {
            return new XmlSiteMapResult();
        }
    }
}
