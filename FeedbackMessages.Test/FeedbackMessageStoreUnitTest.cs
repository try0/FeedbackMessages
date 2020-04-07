using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;

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
        /// <see cref="FeedbackMessageStore.Load"/> test
        /// </summary>
        [TestMethod]
        public void TestLoad()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            HttpContext.Current.Session[FeedbackMessageStore.ITEM_KEY] = store;


            FeedbackMessageStore.Load();

            var loadedStore = HttpContext.Current.Items[FeedbackMessageStore.ITEM_KEY];

            Assert.IsNotNull(loadedStore);
            Assert.IsTrue(store == loadedStore);
        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.Flash"/> test
        /// </summary>
        [TestMethod]
        public void TestFlashEmptyStore()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();

            HttpContext.Current.Items[FeedbackMessageStore.ITEM_KEY] = store;

            FeedbackMessageStore.Flash();

            var flashedStore = HttpContext.Current.Session[FeedbackMessageStore.ITEM_KEY];

            Assert.IsNull(flashedStore);

        }

        /// <summary>
        /// <see cref="FeedbackMessageStore.Flash"/> test
        /// </summary>
        [TestMethod]
        public void TestFlashExistsUnrenderedMessages()
        {
            InitializeHttpContext();

            var store = new FeedbackMessageStore();
            store.AddMessage(FeedbackMessage.Info("test message."));

            HttpContext.Current.Items[FeedbackMessageStore.ITEM_KEY] = store;

            FeedbackMessageStore.Flash();

            var flashedStore = HttpContext.Current.Session[FeedbackMessageStore.ITEM_KEY];

            Assert.IsNotNull(flashedStore);
            Assert.IsTrue(store == flashedStore);
        }

    }
}
