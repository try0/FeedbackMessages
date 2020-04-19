using FeedbackMessages.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc; 
using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Test
{
    public class MockController : Controller
    {
    }


    [TestClass]
    public class ControllerExtensionsUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestAddMessages()
        {

            InitializeHttpContext();

            var controller = new MockController();

            controller.InfoMessage("情報メッセージ1");
            controller.InfoMessage("情報メッセージ2");

            controller.SuccessMessage("success");
            controller.WarnMessage("warn");
            controller.ErrorMessage("error");


            var store = FeedbackMessageStore.Current;
            Assert.AreEqual(store.Count, 5);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.INFO).Count, 2);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.SUCCESS).Count, 1);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.WARN).Count, 1);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.ERROR).Count, 1);

        }
    }
}
