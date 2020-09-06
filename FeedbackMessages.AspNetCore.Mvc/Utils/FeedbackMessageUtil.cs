using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeedbackMessages.Utils.Mvc
{
    public static class FeedbackMessageUtil
    {

        /// <summary>
        /// Extracts error messages from <see cref="ModelStateDictionary"/> as <see cref="FeedbackMessage"/>.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable<FeedbackMessage> GetErrorsAsFeedbackMessage(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {

                List<FeedbackMessage> errorMessages = new List<FeedbackMessage>();
                VisitModelState(modelState.Root, entry =>
                {
                    foreach (ModelError error in entry.Errors)
                    {
                        if (String.IsNullOrEmpty(error.ErrorMessage))
                        {
                            continue;
                        }

                        var feedbackMessage = FeedbackMessage.Error(error.ErrorMessage);
                        errorMessages.Add(feedbackMessage);
                    }
                });

                return errorMessages;
            }

            return Enumerable.Empty<FeedbackMessage>();
        }

        /// <summary>
        /// Adds validation error messages to <see cref="FeedbackMessageStore"/>. 
        /// </summary>
        /// <param name="viewContext"></param>
        public static void AppendValidationErrorsToStore(ViewContext viewContext)
        {
            var viewData = viewContext.ViewData;
            if (!viewContext.ClientValidationEnabled && viewData.ModelState.IsValid)
            {
                return;
            }

            var messageStore = FeedbackMessageStore.Current;
            ModelStateDictionary modelStates = viewData.ModelState;
            foreach (var errorMessage in GetErrorsAsFeedbackMessage(modelStates))
            {
                messageStore.AddMessage(errorMessage);
            }
        }

        private static void VisitModelState(ModelStateEntry modelStateEntry, Action<ModelStateEntry> action)
        {
            if (modelStateEntry.Children != null)
            {
                foreach (ModelStateEntry child in modelStateEntry.Children)
                {
                    VisitModelState(child, action);
                }
            }

            action.Invoke(modelStateEntry);
        }

    }
}
