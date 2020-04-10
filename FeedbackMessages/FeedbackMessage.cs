using System;
using System.Collections.Generic;
using System.Web.UI;

namespace FeedbackMessages
{
    /// <summary>
    /// The message that feedback to web client, users. Message object wrapper. 
    /// </summary>
    [Serializable]
    public class FeedbackMessage 
    {

        /// <summary>
        /// Message levels.
        /// </summary>
        [Serializable]
        public enum FeedbackMessageLevel
        {
            /// <summary>
            /// Information level
            /// </summary>
            INFO,

            /// <summary>
            /// Success level
            /// </summary>
            SUCCESS,

            /// <summary>
            /// Warning level
            /// </summary>
            WARN,

            /// <summary>
            /// Error level
            /// </summary>
            ERROR
        }

        /// <summary>
        /// Creates information feedback message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static FeedbackMessage Info(Object message, Control reporter = null)
        {
            return new FeedbackMessage()
            {
                Level = FeedbackMessageLevel.INFO,
                Message = message,
                Reporter = reporter
            };
        }

        /// <summary>
        /// Creates success feedback message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static FeedbackMessage Success(Object message, Control reporter = null)
        {
            return new FeedbackMessage()
            {
                Level = FeedbackMessageLevel.SUCCESS,
                Message = message,
                Reporter = reporter
            };
        }

        /// <summary>
        /// Creates warning feedback message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static FeedbackMessage Warn(Object message, Control reporter = null)
        {
            return new FeedbackMessage()
            {
                Level = FeedbackMessageLevel.WARN,
                Message = message,
                Reporter = reporter
            };
        }

        /// <summary>
        /// Creates error feedback message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="reporter"></param>
        /// <returns></returns>
        public static FeedbackMessage Error(Object message, Control reporter = null)
        {
            return new FeedbackMessage()
            {
                Level = FeedbackMessageLevel.ERROR,
                Message = message,
                Reporter = reporter
            };
        }

        /// <summary>
        /// The message level
        /// </summary>
        public FeedbackMessageLevel Level { get; set; }

        /// <summary>
        /// The message body
        /// </summary>
        public Object Message { get; set; }

        [NonSerialized]
        private Control control;

        /// <summary>
        /// The message reporter
        /// </summary>
        public Control Reporter
        {
            get { return control; }
            set { control = value; }
        }

        /// <summary>
        /// Wheter message is rendered or not
        /// </summary>
        public bool IsRendered { get; private set; }

        /// <summary>
        /// Change state to rendered.
        /// </summary>
        public void MarkRendered()
        {
            IsRendered = true;
        }

        /// <summary>
        /// <see cref="FeedbackMessage.Message.ToString()"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Message.ToString();
        }

        public override bool Equals(object obj)
        {
            var message = obj as FeedbackMessage;
            return message != null &&
                   Level == message.Level &&
                   EqualityComparer<object>.Default.Equals(Message, message.Message);
        }

        public override int GetHashCode()
        {
            var hashCode = -1024348279;
            hashCode = hashCode * -1521134295 + Level.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(Message);
            return hashCode;
        }

    }
}
