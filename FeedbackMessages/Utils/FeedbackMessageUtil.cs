using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using System.Web.UI;

namespace FeedbackMessages.Utils
{
    /// <summary>
    /// Utilities 
    /// </summary>
    public static class FeedbackMessageUtil
    {

        /// <summary>
        /// Adds validation error messages to <see cref="FeedbackMessageStore"/>. 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="option"></param>
        public static void AppendValidationErrorsToStore(Page page, FeedbackMessageRenderOption option)
        {
            List<FeedbackMessage> errorMessages = new List<FeedbackMessage>();

            if (option.ShowValidationErrors)
            {
                ValidatorCollection validators = page.GetValidators(option.ValidationGroup);

                foreach (IValidator validator in validators)
                {
                    if (validator.IsValid)
                    {
                        continue;
                    }

                    if (String.IsNullOrEmpty(validator.ErrorMessage))
                    {
                        continue;
                    }

                    var feedbackMessage = FeedbackMessage.Error(String.Copy(validator.ErrorMessage));
                    errorMessages.Add(feedbackMessage);
                }
            }

            if (option.ShowModelStateErrors)
            {
                ModelStateDictionary modelState = page.ModelState;
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
                            errorMessages.Add(feedbackMessage);
                        }
                    }
                }
            }

            FeedbackMessageStore.Current.AddMessages(errorMessages);
        }

        /// <summary>
        ///  Adds validation error messages to <see cref="FeedbackMessageStore"/>. 
        /// </summary>
        /// <param name="page"></param>
        public static void AppendValidationErrorsToStore(Page page)
        {
            AppendValidationErrorsToStore(page, new FeedbackMessageRenderOption()
            {
                ShowValidationErrors = true,
                ShowModelStateErrors = true
            });
        }

        /// <summary>
        ///  Adds validation error messages to <see cref="FeedbackMessageStore"/>. 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="validationGroup"></param>
        public static void AppendValidationErrorsToStore(Page page, string validationGroup)
        {
            AppendValidationErrorsToStore(page, new FeedbackMessageRenderOption()
            {
                ShowValidationErrors = true,
                ShowModelStateErrors = true,
                ValidationGroup = validationGroup
            });
        }
    }
}
