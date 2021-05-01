using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;



namespace FeedbackMessages
{

    /// <summary>
    /// Message store that manage <see cref="FeedbackMessage"/>.
    /// </summary>
    [Serializable]
    public class FeedbackMessageStore : ICollection<FeedbackMessage>
    {

        /// <summary>
        /// Store holder
        /// </summary>
        private static IFeedbackMessageStoreHolder holder;

        /// <summary>
        /// Gets message sotre from current http context.
        /// </summary>
        public static FeedbackMessageStore Current => holder.Current;



        /// <summary>
        /// Initialize FeedbackMessageStore.
        /// </summary>
        /// <param name="holder"></param>
        public static void Initialize(IFeedbackMessageStoreHolder holder)
        {
            FeedbackMessageStore.holder = holder;
        }

        /// <summary>
        /// Loads message store.
        /// </summary>
        public static void Load()
        {
            holder.LoadFeedbackMessageStore();
        }

        /// <summary>
        /// Flashs message sotre.
        /// </summary>
        public static void Flash()
        {
            holder.FlashFeedbackMessageStore();
        }

        private readonly object createLock = new object();

        /// <summary>
        /// Temporary objects in current request.
        /// </summary>
        [NonSerialized]
        private readonly IDictionary<Object, Object> items = new Dictionary<Object, Object>();

        /// <summary>
        /// Feedback messages for each level.
        /// </summary>
        private Dictionary<FeedbackMessage.FeedbackMessageLevel, List<FeedbackMessage>> messagesHolder = new Dictionary<FeedbackMessage.FeedbackMessageLevel, List<FeedbackMessage>>();

        /// <summary>
        /// Temporary objects in current request.
        /// </summary>
        public IDictionary<Object, Object> Items => items;


        /// <summary>
        /// Readonly feedback messages for each level.
        /// </summary>
        public IDictionary<FeedbackMessage.FeedbackMessageLevel, ReadOnlyCollection<FeedbackMessage>> Messages
        {
            get
            {
                var returnMessages = new Dictionary<FeedbackMessage.FeedbackMessageLevel, ReadOnlyCollection<FeedbackMessage>>();

                foreach (var kv in messagesHolder)
                {
                    returnMessages.Add(kv.Key, GetOrNewFeedbackMessages(kv.Key).AsReadOnly());
                }
                return returnMessages;
            }
        }

        /// <summary>
        /// Gets messages count.
        /// </summary>
        public int Count
        {
            get
            {
                int cnt = 0;
                foreach (var entry in Messages)
                {
                    cnt += entry.Value.Count;
                }

                return cnt;
            }
        }

        /// <summary>
        /// Gets readonly status.
        /// </summary>
        public bool IsReadOnly => false;




        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessageStore()
        {
        }


        private List<FeedbackMessage> GetOrNewFeedbackMessages(FeedbackMessage.FeedbackMessageLevel level)
        {
            if (!messagesHolder.ContainsKey(level))
            {
                lock (createLock)
                {
                    if (!messagesHolder.ContainsKey(level))
                    {
                        var messageList = new List<FeedbackMessage>();
                        messagesHolder[level] = messageList;
                        return messageList;
                    }
                }
            }

            return messagesHolder[level];
        }

        /// <summary>
        /// Gets feedback messages as readonly.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public IList<FeedbackMessage> GetFeedbackMessages(FeedbackMessage.FeedbackMessageLevel level)
        {
            var messages = GetOrNewFeedbackMessages(level);
            return messages.AsReadOnly();
        }

        /// <summary>
        /// Gets feedback messages as readonly.
        /// </summary>
        /// <returns></returns>
        public IList<FeedbackMessage> GetFeedbackMessages()
        {
            var messages = new List<FeedbackMessage>();
            if (Messages.Keys.Count == 0)
            {
                return messages.AsReadOnly();
            }

            foreach (var entry in Messages)
            {
                messages.AddRange(entry.Value);
            }

            return messages.AsReadOnly();
        }

        /// <summary>
        /// Adds feedback message.
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(FeedbackMessage message)
        {
            if (message == null || message.Message == null || message.IsRendered)
            {
                return;
            }

            var messageList = GetOrNewFeedbackMessages(message.Level);
            messageList.Add(message);
        }

        /// <summary>
        /// Adds feedback messages.
        /// </summary>
        /// <param name="messages"></param>
        public void AddMessages(IEnumerable<FeedbackMessage> messages)
        {
            foreach (FeedbackMessage message in messages)
            {
                AddMessage(message);
            }
        }

        /// <summary>
        /// Cleans rendered messages.
        /// </summary>
        public void CleanRendered()
        {
            foreach (var messageList in messagesHolder.Values)
            {
                var unrenderdMessages = messageList.Where(msg => !msg.IsRendered).ToList();

                messageList.Clear();
                messageList.AddRange(unrenderdMessages);
            }
        }

        /// <summary>
        /// Whethers exists unrendered messages or not.
        /// </summary>
        /// <returns></returns>
        public bool HasUnrenderedMessage()
        {
            foreach (var messageList in messagesHolder.Values)
            {
                var hasUnrenderd = messageList.Any(msg => !msg.IsRendered);

                if (hasUnrenderd)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clear messages.
        /// </summary>
        /// <param name="level"></param>
        public void Clear(FeedbackMessage.FeedbackMessageLevel level)
        {
            GetOrNewFeedbackMessages(level).Clear();
        }

        /// <summary>
        /// Gets messages as enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<FeedbackMessage> GetEnumerator()
        {
            return GetFeedbackMessages().GetEnumerator();
        }

        /// <summary>
        /// Gets messages as enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetFeedbackMessages().GetEnumerator();
        }

        /// <summary>
        /// Adds message. Delegates to <see cref="AddMessage(FeedbackMessage)"/>.
        /// </summary>
        /// <param name="item"></param>
        public void Add(FeedbackMessage item)
        {
            AddMessage(item);
        }

        /// <summary>
        /// Clear messages.
        /// </summary>
        public void Clear()
        {
            messagesHolder.Clear();
        }

        /// <summary>
        /// Whether contains message or not.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(FeedbackMessage item)
        {
            foreach (var entry in messagesHolder)
            {

                if (entry.Value.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Copys to array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(FeedbackMessage[] array, int arrayIndex)
        {
            int index = arrayIndex;
            foreach (var item in this)
            {
                array[index] = item;
                index++;
            }
        }

        /// <summary>
        /// Removes message.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(FeedbackMessage item)
        {
            foreach (var entry in messagesHolder)
            {
                if (entry.Value.Remove(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
