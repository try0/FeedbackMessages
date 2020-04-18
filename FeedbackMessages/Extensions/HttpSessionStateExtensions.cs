using System.Web.SessionState;

namespace FeedbackMessages.Extensions
{
    public static class HttpSessionStateExtensions
    {
        public static void SetStore(this HttpSessionState session, string key, FeedbackMessageStore value)
        {
            if (value == null)
            {
                return;
            }

            string str = FeedbackMessageSettings.Instance.StoreSerializer.Serialize(value);
            session[key] = str;
        }
        public static FeedbackMessageStore GetStore(this HttpSessionState session, string key)
        {
            var str = session[key] as string;
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            FeedbackMessageStore obj = FeedbackMessageSettings.Instance.StoreSerializer.Deserialize(str);
            return obj;
        }
    }
}
