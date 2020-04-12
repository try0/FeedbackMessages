using System;
using System.Web.Mvc;

namespace FeedbackMessages.Extensions
{
    /// <summary>
    /// Controller extensions for message store.
    /// </summary>
    public static class ControllerExtensions
    {

        /// <summary>
        /// Sets information message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        public static void InfoMessage(this Controller controller, Object message)
        {
            var feedbackMessage = FeedbackMessage.Info(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets success message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        public static void SuccessMessage(this Controller controller, Object message)
        {
            var feedbackMessage = FeedbackMessage.Success(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets warning message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        public static void WarnMessage(this Controller controller, Object message)
        {
            var feedbackMessage = FeedbackMessage.Warn(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets error message to <see cref="FeedbackMessageStore"/>.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="message"></param>
        public static void ErrorMessage(this Controller controller, Object message)
        {
            var feedbackMessage = FeedbackMessage.Error(message);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }
    }
}
