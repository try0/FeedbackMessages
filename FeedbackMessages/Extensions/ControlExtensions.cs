using FeedbackMessages.Utils;
using System;
using System.Web.UI;

namespace FeedbackMessages.Extensions
{
    /// <summary>
    /// Control extensions for message store.
    /// </summary>
    public static class ControlExtensions
    {

        /// <summary>
        /// Sets information message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void InfoMessage(this Control control, Object message)
        {
            var feedbackMessage = FeedbackMessage.Info(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets success message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void SuccessMessage(this Control control, Object message)
        {
            var feedbackMessage = FeedbackMessage.Success(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets warning message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void WarnMessage(this Control control, Object message)
        {
            var feedbackMessage = FeedbackMessage.Warn(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets error message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void ErrorMessage(this Control control, Object message)
        {
            var feedbackMessage = FeedbackMessage.Error(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }


        /// <summary>
        /// Appends javascript for display messages.
        /// </summary>
        /// <param name="control"></param>
        public static void AppendFeedbackMessageScripts(this Control control, FeedbackMessageRenderOption option = default)
        {
            control.Page.PreRenderComplete += (object sender, System.EventArgs e) =>
            {
                Page page = (Page)sender;

                FeedbackMessageUtil.AppendValidationErrorsToStore(page, option);

                page.ClientScript.RegisterStartupScript(page.GetType(), "FeedbackMessages.OnReady", FeedbackMessageSettings.Instance.ScriptBuilder.GetDomReadyScript(), true);
            };


        }

        /// <summary>
        /// Adds validation error messages to <see cref="FeedbackMessageStore"/>. 
        /// </summary>
        /// <param name="page"></param>
        public static void AppendValidationErrorsToStore(this Page page)
        {
            FeedbackMessageUtil.AppendValidationErrorsToStore(page);
        }

        /// <summary>
        /// Adds validation error messages to <see cref="FeedbackMessageStore"/>. 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="option"></param>
        public static void AppendValidationErrorsToStore(this Page page, FeedbackMessageRenderOption option)
        {
            FeedbackMessageUtil.AppendValidationErrorsToStore(page, option);
        }

        /// <summary>
        /// Adds validation error messages to <see cref="FeedbackMessageStore"/>. 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="validationGroup"></param>
        public static void AppendValidationErrorsToStore(this Page page, string validationGroup)
        {
            FeedbackMessageUtil.AppendValidationErrorsToStore(page, validationGroup);
        }

    }
}
