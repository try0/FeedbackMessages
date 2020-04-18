
using Microsoft.AspNetCore.Http;

namespace FeedbackMessages.Extensions
{
    public static class ISessionExtensions
    {
        public static void SetStore(this ISession session, string key, FeedbackMessageStore value)
        {
            if (value == null)
            {
                return;
            }

            string str = FeedbackMessageSettings.Instance.StoreSerializer.Serialize(value);
            session.SetString(key, str);
        }
        public static FeedbackMessageStore GetStore(this ISession session, string key)
        {
            var str = session.GetString(key);
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            FeedbackMessageStore obj = FeedbackMessageSettings.Instance.StoreSerializer.Deserialize(str);
            return obj;
        }
    }
}
