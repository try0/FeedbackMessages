namespace FeedbackMessages
{
    /// <summary>
    /// <see cref="FeedbackMessageStore"/> converter for to persisted in session.
    /// </summary>
    public interface IFeedbackMessageStoreSerializer
    {
        /// <summary>
        /// Deserialize string as store.
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        FeedbackMessageStore Deserialize(string serial);

        /// <summary>
        /// Serialize store as string.
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        string Serialize(FeedbackMessageStore store);
    }
}
