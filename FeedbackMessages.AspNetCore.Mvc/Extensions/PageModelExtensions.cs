using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace FeedbackMessages.Extensions
{
    /// <summary>
    /// RazorPages model extensions for message store.
    /// </summary>
    public static class PageModelExtensions
    {

        /// <summary>
        /// Sets information message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="pageModel"></param>
        /// <param name="message"></param>
        public static void InfoMessage(this PageModel pageModel, Object message)
        {
            var feedbackMessage = FeedbackMessage.Info(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets success message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="pageModel"></param>
        /// <param name="message"></param>
        public static void SuccessMessage(this PageModel pageModel, Object message)
        {
            var feedbackMessage = FeedbackMessage.Success(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets warning message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="pageModel"></param>
        /// <param name="message"></param>
        public static void WarnMessage(this PageModel pageModel, Object message)
        {
            var feedbackMessage = FeedbackMessage.Warn(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets error message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="pageModel"></param>
        /// <param name="message"></param>
        public static void ErrorMessage(this PageModel pageModel, Object message)
        {
            var feedbackMessage = FeedbackMessage.Error(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }
    }
}
