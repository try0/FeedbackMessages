using System;

namespace FeedbackMessages
{
    /// <summary>
    /// <see cref="FeedbackMessage"/> converter that convert <see cref="FeedbackMessage"/> to string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FeedbackMessageStringConverter : IFeedbackMessageConverter<string>
    {
        /// <summary>
        /// String factory.
        /// </summary>
        public Func<FeedbackMessage, string> StringFactory { get; set; } = msg => msg.ToString();

        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessageStringConverter()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stringFactory"></param>
        public FeedbackMessageStringConverter(Func<FeedbackMessage, string> stringFactory)
        {
            this.StringFactory = stringFactory;
        }

        /// <summary>
        /// Converts FeedbackMessage to string.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string Convert(FeedbackMessage message)
        {
            return StringFactory.Invoke(message);
        }
    }
}
