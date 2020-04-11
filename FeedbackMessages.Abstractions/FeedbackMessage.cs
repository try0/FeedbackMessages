using System;
using System.Collections.Generic;


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
        /// <returns></returns>
        public static FeedbackMessage Info(Object message)
        {
            return new FeedbackMessage()
            {
                Level = FeedbackMessageLevel.INFO,
                Message = message
            };
        }

        /// <summary>
        /// Creates success feedback message.
        /// </summary>
        /// <param name="message"></param> 
        /// <returns></returns>
        public static FeedbackMessage Success(Object message)
        {
            return new FeedbackMessage()
            {
                Level = FeedbackMessageLevel.SUCCESS,
                Message = message
            };
        }

        /// <summary>
        /// Creates warning feedback message.
        /// </summary>
        /// <param name="message"></param> 
        /// <returns></returns>
        public static FeedbackMessage Warn(Object message)
        {
            return new FeedbackMessage()
            {
                Level = FeedbackMessageLevel.WARN,
                Message = message
            };
        }

        /// <summary>
        /// Creates error feedback message.
        /// </summary>
        /// <param name="message"></param> 
        /// <returns></returns>
        public static FeedbackMessage Error(Object message)
        {
            return new FeedbackMessage()
            {
                Level = FeedbackMessageLevel.ERROR,
                Message = message
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



        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessage()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessage(FeedbackMessageLevel level, Object message)
        {
            this.Level = level;
            this.Message = message;
        }


        /// <summary>
        /// Wheter message is rendered or not
        /// </summary>
        public bool IsRendered { get; private set; }

        /// <summary>
        /// Change state to rendered.
        /// </summary>
        public virtual void MarkRendered()
        {
            IsRendered = true;
        }

        /// <summary>
        /// <see cref="FeedbackMessage.Message"/>.ToString()
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
