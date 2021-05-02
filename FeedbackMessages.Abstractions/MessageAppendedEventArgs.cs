using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackMessages
{
    /// <summary>
    /// Event args on appended message.
    /// </summary>
    public class MessageAppendedEventArgs : EventArgs
    {
        public FeedbackMessage Message { get; set; }

        public MessageAppendedEventArgs()
        {
        }
        public MessageAppendedEventArgs(FeedbackMessage message)
        {
            this.Message = message;
        }

    }
}
