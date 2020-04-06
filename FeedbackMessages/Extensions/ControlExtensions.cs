
using System;
using System.Web;
using System.Web.UI;

namespace FeedbackMessages.Extensions
{
    /// <summary>
    /// Control extensions for message store.
    /// </summary>
    public static class ControlExtensions
    {

        /// <summary>
        /// Sets information message to <see cref="MessageStore"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void InfoMessage(this Control control, Object message)
        {
            var feedbackMessage = FeedbackMessage.Info(message, control);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets success message to <see cref="MessageStore"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void SuccessMessage(this Control control, Object message)
        {
            var feedbackMessage = FeedbackMessage.Success(message, control);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets warning message to <see cref="MessageStore"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void WarnMessage(this Control control, Object message)
        {
            var feedbackMessage = FeedbackMessage.Warn(message, control);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }

        /// <summary>
        /// Sets error message to <see cref="MessageStore"/>.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="message"></param>
        public static void ErrorMessage(this Control control, Object message)
        {
            var feedbackMessage = FeedbackMessage.Error(message, control);

            FeedbackMessageStore.Current.AddMessage(feedbackMessage);
        }
    }
}
