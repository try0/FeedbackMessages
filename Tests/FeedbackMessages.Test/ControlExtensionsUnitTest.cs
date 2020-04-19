using FeedbackMessages.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.UI.WebControls;
using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Test
{
    [TestClass]
    public class ControlExtensionsUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestAddMessages()
        {

            InitializeHttpContext();

            var ctl = new Button();

            ctl.InfoMessage("情報メッセージ1");
            ctl.InfoMessage("情報メッセージ2");

            ctl.SuccessMessage("success");
            ctl.WarnMessage("warn");
            ctl.ErrorMessage("error");


            var store = FeedbackMessageStore.Current;
            Assert.AreEqual(store.Count, 5);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.INFO).Count, 2);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.SUCCESS).Count, 1);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.WARN).Count, 1);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.ERROR).Count, 1);

        }
    }
}
