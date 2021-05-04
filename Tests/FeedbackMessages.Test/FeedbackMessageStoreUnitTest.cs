using FeedbackMessages.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Web;
using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Test
{
    /// <summary>
    /// <see cref="FeedbackMessageStore"/> Unit Tests.
    /// </summary>
    [TestClass]
    public class FeedbackMessageStoreUnitTest : FeedbackMessagesUnitTestBase
    {
        public FeedbackMessageStoreUnitTest()
        {
        }


        /// <summary>
        /// <see cref="FeedbackMessageStore.Load"/> test.
        /// </summary>
        [TestMethod]
        public void TestLoad()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            HttpContext.Current.Session[FeedbackMessageStoreHolder.ITEM_KEY] = store;


            FeedbackMessageStore.Load();

            var loadedStore = HttpContext.Current.Items[FeedbackMessageStoreHolder.ITEM_KEY] as FeedbackMessageStore;

            Assert.IsNotNull(loadedStore);
            Assert.AreEqual(0, loadedStore.Count);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.Flash"/> test.
        /// </summary>
        [TestMethod]
        public void TestFlashEmptyStore()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            HttpContext.Current.Items[FeedbackMessageStoreHolder.ITEM_KEY] = store;

            FeedbackMessageStore.Flash();

            var flashedStore = HttpContext.Current.Session[FeedbackMessageStoreHolder.ITEM_KEY];

            Assert.IsNull(flashedStore);

        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.Flash"/> test.
        /// </summary>
        [TestMethod]
        public void TestFlashExistsUnrenderedMessages()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();
            store.AddMessage(FeedbackMessage.Info("test message."));

            HttpContext.Current.Items[FeedbackMessageStoreHolder.ITEM_KEY] = store;

            FeedbackMessageStore.Flash();

            var flashedStore = HttpContext.Current.Session.GetStore(FeedbackMessageStoreHolder.ITEM_KEY);

            Assert.IsNotNull(flashedStore);
            Assert.AreEqual(1, flashedStore.Count);

            Assert.IsTrue(flashedStore.HasUnrenderedMessage());

            var message = flashedStore.GetFeedbackMessages()[0];
            Assert.AreEqual(FeedbackMessageLevel.INFO, message.Level);
            Assert.AreEqual("test message.", message.ToString());
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.AddMessage(FeedbackMessage)"/> test.
        /// </summary>
        [TestMethod]
        public void TestAddMessage()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            var feedbackMessage = FeedbackMessage.Info("Test info message.");

            store.AddMessage(feedbackMessage);

            IList<FeedbackMessage> infoMessages = store.GetFeedbackMessages(FeedbackMessageLevel.INFO);
            Assert.AreEqual(1, infoMessages.Count);
            Assert.IsTrue(infoMessages.Contains(feedbackMessage));

            IList<FeedbackMessage> errorMessages = store.GetFeedbackMessages(FeedbackMessageLevel.ERROR);
            Assert.AreEqual(0, errorMessages.Count);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.AddMessage(FeedbackMessage)"/> test.
        /// </summary>
        [TestMethod]
        public void TestAddDuplicaateMessages()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            var info1 = FeedbackMessage.Info("Test message.");
            var info2 = FeedbackMessage.Info("Test message.");

            store.AddMessage(info1);
            store.AddMessage(info2);

            Assert.AreEqual(1, store.Count);

            var warn = FeedbackMessage.Warn("Test message.");

            store.AddMessage(warn);
            Assert.AreEqual(2, store.Count);

        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.AddMessages(IEnumerable{FeedbackMessage})"/> test.
        /// </summary>
        [TestMethod]
        public void TestAddMessages()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            List<FeedbackMessage> messages = new List<FeedbackMessage>();

            var info = FeedbackMessage.Info("Test info message.");
            var warn = FeedbackMessage.Warn("Test warn message.");

            messages.Add(info);
            messages.Add(warn);

            store.AddMessages(messages);

            IList<FeedbackMessage> infoMessages = store.GetFeedbackMessages(FeedbackMessageLevel.INFO);
            Assert.AreEqual(1, infoMessages.Count);
            Assert.IsTrue(infoMessages.Contains(info));

            IList<FeedbackMessage> successMessages = store.GetFeedbackMessages(FeedbackMessageLevel.SUCCESS);
            Assert.AreEqual(0, successMessages.Count);

            IList<FeedbackMessage> warnMessages = store.GetFeedbackMessages(FeedbackMessageLevel.WARN);
            Assert.AreEqual(1, warnMessages.Count);
            Assert.IsTrue(warnMessages.Contains(warn));

            IList<FeedbackMessage> errorMessages = store.GetFeedbackMessages(FeedbackMessageLevel.ERROR);
            Assert.AreEqual(0, errorMessages.Count);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.HasUnrenderedMessage"/> test.
        /// </summary>
        [TestMethod]
        public void TestHasUnrenderedMessage()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            Assert.IsFalse(store.HasUnrenderedMessage());

            var message = FeedbackMessage.Warn("Test warn message");
            store.AddMessage(message);

            Assert.IsTrue(store.HasUnrenderedMessage());

            message.MarkRendered();

            Assert.IsFalse(store.HasUnrenderedMessage());

        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.CleanRendered"/> test.
        /// </summary>
        [TestMethod]
        public void TestCleanRendered()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            Assert.IsFalse(store.HasUnrenderedMessage());


            var infoMessage1 = FeedbackMessage.Info("Test warn message1");
            store.AddMessage(infoMessage1);
            var infoMessage2 = FeedbackMessage.Info("Test warn message2");
            store.AddMessage(infoMessage2);
            var infoMessage3 = FeedbackMessage.Info("Test warn message3");
            store.AddMessage(infoMessage3);

            store.CleanRendered();

            var infoMessages = store.GetFeedbackMessages(FeedbackMessageLevel.INFO);

            Assert.AreEqual(3, infoMessages.Count);

            infoMessage1.MarkRendered();
            infoMessage2.MarkRendered();
            store.CleanRendered();

            var infoMessagesCleanedRendered = store.GetFeedbackMessages(FeedbackMessageLevel.INFO);

            Assert.AreEqual(1, infoMessagesCleanedRendered.Count);
            Assert.IsTrue(infoMessagesCleanedRendered.Contains(infoMessage3));

            infoMessage3.MarkRendered();
            store.CleanRendered();

            Assert.IsFalse(store.HasUnrenderedMessage());

        }


        [TestMethod]
        public void TestSerializeAndDeserializeStore()
        {

            var store = new FeedbackMessageStore();
            store.Add(FeedbackMessage.Info("情報メッセージ"));

            var serializer = new FeedbackMessageStoreSerializer();

            var str = serializer.Serialize(store);


            var deserializeStore = serializer.Deserialize(str);

            Assert.IsTrue(deserializeStore.HasUnrenderedMessage());
            Assert.AreEqual("情報メッセージ", deserializeStore.GetFeedbackMessages()[0].ToString());

            Assert.AreEqual(1, deserializeStore.Count);

        }

        [TestMethod]
        public void TestRemove()
        {


            var store = new FeedbackMessageStore();

            var message = FeedbackMessage.Info("Test");
            store.AddMessage(message);
            store.AddMessage(FeedbackMessage.Info("Test2"));
            Assert.AreEqual(2, store.Count);

            store.Remove(message);
            Assert.AreEqual(1, store.Count);

        }

        [TestMethod]
        public void TestContains()
        {


            var store = new FeedbackMessageStore();

            var message = FeedbackMessage.Info("Test");
            store.AddMessage(message);
            store.AddMessage(FeedbackMessage.Info("Test2"));
            Assert.AreEqual(2, store.Count);

            Assert.IsTrue(store.Contains(message));
            Assert.IsFalse(store.Contains(FeedbackMessage.Warn("Test")));

        }

        [TestMethod]
        public void TestClear()
        {


            var store = new FeedbackMessageStore();

            var message = FeedbackMessage.Info("Test");
            store.AddMessage(message);
            store.AddMessage(FeedbackMessage.Info("Test2"));
            Assert.AreEqual(2, store.Count);

            store.Clear();
            Assert.AreEqual(0, store.Count);

        }

        [TestMethod]
        public void TestClearLevel()
        {
            var store = new FeedbackMessageStore();

            var message = FeedbackMessage.Info("Test");
            store.AddMessage(message);
            store.AddMessage(FeedbackMessage.Info("Test2"));
            store.AddMessage(FeedbackMessage.Warn("Test2"));
            Assert.AreEqual(3, store.Count);

            store.Clear(FeedbackMessageLevel.INFO);
            Assert.AreEqual(1, store.Count);

            store.Clear(FeedbackMessageLevel.WARN);
            Assert.AreEqual(0, store.Count);

        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.AddMessages(IEnumerable{FeedbackMessage})"/> test.
        /// </summary>
        [TestMethod]
        public void TestMessageAppendedEvent()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            var feedbackMessage = FeedbackMessage.Info("test raise appended event.");
            int countRaiseEvent = 0;

            EventHandler<MessageAppendedEventArgs> handler = (sender, args) =>
            {
                Assert.AreEqual(store, sender);
                Assert.AreEqual(feedbackMessage, args.Message);
                countRaiseEvent++;
            };

            // register
            store.OnMessageAppeded += handler;


            store.AddMessage(feedbackMessage);
            Assert.AreEqual(1, countRaiseEvent);

            // unregister
            store.OnMessageAppeded -= handler;
            store.AddMessage(FeedbackMessage.Warn("test unregister event"));
            Assert.AreEqual(1, countRaiseEvent);

        }
    }


}
