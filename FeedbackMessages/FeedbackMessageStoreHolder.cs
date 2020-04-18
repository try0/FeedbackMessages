using FeedbackMessages.Extensions;
using System.Web;
using System.Web.SessionState;

namespace FeedbackMessages
{
    /// <summary>
    /// <see cref="FeedbackMessageStore"/> provider.
    /// </summary>
    public class FeedbackMessageStoreHolder : IFeedbackMessageStoreHolder
    {
        /// <summary>
        /// Default instance.
        /// </summary>
        public static readonly FeedbackMessageStoreHolder Instance = new FeedbackMessageStoreHolder();

        /// <summary>
        /// MessageStore key string that for use add store to session or request items.
        /// </summary>
        public static readonly string ITEM_KEY = typeof(FeedbackMessageStore).ToString();

        private static readonly string META_DATA_SESSION_KEY = "SESSION";

        /// <summary>
        /// Gets message store in current request.
        /// </summary>
        public FeedbackMessageStore Current
        {
            get
            {
                var store = HttpContext.Current.Items[ITEM_KEY];

                if (store != null)
                {
                    return (FeedbackMessageStore)store;
                }

                store = new FeedbackMessageStore();
                HttpContext.Current.Items[ITEM_KEY] = store;
                return (FeedbackMessageStore)store;
            }
        }

        /// <summary>
        /// Flashs <see cref="FeedbackMessageStore"/> to session.
        /// </summary>
        public void FlashFeedbackMessageStore()
        {
            var messageStore = Current;
            messageStore.CleanRendered();

            if (messageStore.Items.ContainsKey(META_DATA_SESSION_KEY))
            {
                HttpSessionState session = (HttpSessionState)messageStore.Items[META_DATA_SESSION_KEY];

                if (messageStore.HasUnrenderedMessage())
                {
                    session[ITEM_KEY] = messageStore;
                }
                else
                {
                    messageStore.Items.Remove(session);
                    HttpContext.Current.Session[ITEM_KEY] = null;
                }

                return;
            }

            if (!ExistsSession())
            {
                return;
            }


            if (messageStore.HasUnrenderedMessage())
            {
                HttpContext.Current.Session.SetStore(ITEM_KEY, messageStore);
            }
            else
            {
                messageStore.Items[META_DATA_SESSION_KEY] = null;
                HttpContext.Current.Session[ITEM_KEY] = null;
            }
        }

        /// <summary>
        /// Loads <see cref="FeedbackMessageStore"/> from session.
        /// </summary>
        public void LoadFeedbackMessageStore()
        {
            FeedbackMessageStore messageStore = (FeedbackMessageStore)HttpContext.Current.Items[ITEM_KEY];
            if (messageStore != null)
            {
                return;
            }

            if (!ExistsSession())
            {
                HttpContext.Current.Items[ITEM_KEY] = new FeedbackMessageStore();
                return;
            }

            messageStore = HttpContext.Current.Session.GetStore(ITEM_KEY);
            if (messageStore == null)
            {
                messageStore = new FeedbackMessageStore();
            }

            messageStore.Items[META_DATA_SESSION_KEY] = HttpContext.Current.Session;

            HttpContext.Current.Items[ITEM_KEY] = messageStore;
        }


        /// <summary>
        /// Whether exists session or not.
        /// </summary>
        /// <returns></returns>
        private static bool ExistsSession()
        {
            var context = HttpContext.Current;
            return context != null && context.Session != null;
        }


    }
}
