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
        /// Extracts error messages from <see cref="ValidatorCollection"/> as <see cref="FeedbackMessage"/>.
        /// </summary>
        /// <param name="validators"></param>
        /// <returns></returns>
        public static IEnumerable<FeedbackMessage> GetErrorsAsFeedbackMessage(ValidatorCollection validators)
        {
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
                yield return feedbackMessage;
            }
        }

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
        /// <param name="page"></param>
        /// <param name="option"></param>
        public static void AppendValidationErrorsToStore(Page page, FeedbackMessageRenderOption option)
        {
            var messageStore = FeedbackMessageStore.Current;

            if (option.ShowValidationErrors)
            {
                ValidatorCollection validators = page.GetValidators(option.ValidationGroup);

                foreach (FeedbackMessage errorMessage in GetErrorsAsFeedbackMessage(validators))
                {
                    messageStore.Add(errorMessage);
                }
            }

            if (option.ShowModelStateErrors)
            {
                ModelStateDictionary modelState = page.ModelState;
                if (!modelState.IsValid)
                {
                    foreach (FeedbackMessage errorMessage in GetErrorsAsFeedbackMessage(modelState))
                    {
                        messageStore.Add(errorMessage);
                    }
                }
            }

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
