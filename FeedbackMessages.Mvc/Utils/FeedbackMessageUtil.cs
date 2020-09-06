using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;

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
                foreach (KeyValuePair<string, ModelState> pair in modelState)
                {
                    foreach (ModelError error in pair.Value.Errors)
                    {
                        if (String.IsNullOrEmpty(error.ErrorMessage))
                        {
                            continue;
                        }

                        var feedbackMessage = FeedbackMessage.Error(error.ErrorMessage);
                        yield return feedbackMessage;
                    }
                }
            }
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
    }
}
