using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using System.Threading;
using Experts.Web.Filters;

namespace Experts.Web.Utils
{
    public static class AutoMetadata
    {
        private const string TitlePostfix = "_Title";
        private const string DescriptionPostfix = "_Description";

        public static string GetTitle(string controllerName, string actionName)
        {
            var resourceName = string.Format("{0}_{1}{2}", controllerName, actionName, TitlePostfix);

            Resources.Metadata.ResourceManager.ReleaseAllResources();
            return Resources.Metadata.ResourceManager.GetString(resourceName);
        }

        public static string GetDescription(string controllerName, string actionName)
        {
            var resourceName = string.Format("{0}_{1}{2}", controllerName, actionName, DescriptionPostfix);
            return Resources.Metadata.ResourceManager.GetString(resourceName);
        }

        public static void ValidateMetadataResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var controllers = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Controller)) && !t.Name.StartsWith("T4MVC_"));

            var expectedMetadata = new List<string>();

            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", string.Empty);
                var actions = controller.GetMethods()
                       .Where(m => m.ReturnType.IsSubclassOf(typeof(ActionResult)) || m.ReturnType == typeof(ActionResult))
                       .Where(m => !m.GetCustomAttributes(typeof(NonActionAttribute), true).Any())
                       .Where(m => m.IsPublic);

                foreach (var action in actions)
                {
                    var isCustomMetadata = action.GetCustomAttributes(typeof (CustomMetadataAttribute), true).Any();
                    var isPartial = action.GetCustomAttributes(typeof (DefaultRoutingAttribute), true).Any();
                    var isNotPublic = action.GetCustomAttributes(typeof (AuthorizeRolesAttribute), true).Any()
                                      || action.GetCustomAttributes(typeof(AuthorizeSignedUpUserAttribute), true).Any()
                                      || action.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any()
                                      || controller.GetCustomAttributes(typeof (AuthorizeRolesAttribute), true).Any()
                                      || controller.GetCustomAttributes(typeof(AuthorizeSignedUpUserAttribute), true).Any()
                                      || controller.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any();

                    if (!isCustomMetadata && !isPartial)
                    {
                        var titleReourceName = string.Format("{0}_{1}{2}", controllerName, action.Name, TitlePostfix);
                        var descriptionResourceName = string.Format("{0}_{1}{2}", controllerName, action.Name, DescriptionPostfix);
                        var metadataDescription = Resources.Metadata.ResourceManager.GetString(descriptionResourceName);

                        if (!expectedMetadata.Contains(titleReourceName))
                            expectedMetadata.Add(titleReourceName);

                        if (!expectedMetadata.Contains(descriptionResourceName) && !isNotPublic)
                            expectedMetadata.Add(descriptionResourceName);

                        if (metadataDescription!= null && metadataDescription.Length > 155)
                            throw new Exception("Description should not be longer than 155 words: " + descriptionResourceName);
                    }
                }
            }


            var resourceSet = Resources.Metadata.ResourceManager.GetResourceSet(Thread.CurrentThread.CurrentUICulture, false, false);
            var resourcesDictionary = resourceSet.Cast<DictionaryEntry>().ToDictionary(entry => (string)entry.Key, entry => (string)entry.Value);
            resourceSet.Close();

            var missingMetadata = expectedMetadata.Where(m =>!resourcesDictionary.ContainsKey(m)).ToList();
            if (missingMetadata.Any())
            {
                var missingMetadataInfo = new StringBuilder();
                foreach (var missingRoute in missingMetadata)
                    missingMetadataInfo.AppendLine(missingRoute);

                throw new Exception("Missing metadata: " + Environment.NewLine + missingMetadataInfo);
            }

            var unnecessaryMeta = new List<string>();

            foreach (var entry in resourcesDictionary)
                if (!expectedMetadata.Contains(entry.Key))
                    unnecessaryMeta.Add(entry.Key);

            if(unnecessaryMeta.Any())
                throw new Exception("Unnecessary resource keys: " + Environment.NewLine + String.Join(Environment.NewLine, unnecessaryMeta));

            var duplicatedMeta = new List<string>();

            foreach (var entry in resourcesDictionary)
                if (resourcesDictionary.Count(kvp => entry.Key == kvp.Key) > 1)
                    duplicatedMeta.Add(entry.Key);

            if(duplicatedMeta.Any())
                throw new Exception("Unnecessary resource keys: " + Environment.NewLine + String.Join(Environment.NewLine, unnecessaryMeta));

        }

    }
}