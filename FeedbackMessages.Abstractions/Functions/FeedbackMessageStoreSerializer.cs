using System;
using System.Text.Json;

namespace FeedbackMessages
{
    /// <summary>
    /// <see cref="FeedbackMessageStore"/> converter for to persisted in session.
    /// </summary>
    public class FeedbackMessageStoreSerializer : IFeedbackMessageStoreSerializer
    {
        /// <summary>
        /// <see cref="FeedbackMessageStore"/> deserializer
        /// </summary>
        public Func<string, FeedbackMessageStore> Deserializer { get; set; } = serial =>
        {
            return JsonSerializer.Deserialize<FeedbackMessageStore>(serial);
        };

        /// <summary>
        /// <see cref="FeedbackMessageStore"/> serializer
        /// </summary>
        public Func<FeedbackMessageStore, string> Serializer { get; set; } = store =>
        {
            return JsonSerializer.Serialize<FeedbackMessageStore>(store);
        };

        /// <summary>
        /// Deserialize string as store.
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public FeedbackMessageStore Deserialize(string serial)
        {
            return Deserializer.Invoke(serial);
        }

        /// <summary>
        /// Serialize store as string.
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public string Serialize(FeedbackMessageStore store)
        {
            return Serializer.Invoke(store);
        }
    }
}
