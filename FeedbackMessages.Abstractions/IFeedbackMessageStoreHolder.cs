namespace FeedbackMessages
{
    /// <summary>
    /// Message store provider.
    /// </summary>
    public interface IFeedbackMessageStoreHolder
    {
        /// <summary>
        /// Gets message store in current request.
        /// </summary>
        FeedbackMessageStore Current { get; }

        /// <summary>
        /// Loads message store.
        /// </summary>
        void LoadFeedbackMessageStore();

        /// <summary>
        /// Flash message store.
        /// </summary>
        void FlashFeedbackMessageStore();
    }
}
