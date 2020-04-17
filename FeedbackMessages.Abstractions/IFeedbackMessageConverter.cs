namespace FeedbackMessages
{
    /// <summary>
    /// <see cref="FeedbackMessage"/> converter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFeedbackMessageConverter<out T>
    {
        /// <summary>
        /// Converts message to type parameter instance.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        T Convert(FeedbackMessage message);

    }
}
