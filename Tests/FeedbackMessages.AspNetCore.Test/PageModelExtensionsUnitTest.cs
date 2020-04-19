using FeedbackMessages.Extensions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages.Test
{


    [TestClass]
    public class PageModelExtensionsUnitTest : FeedbackMessagesUnitTestBase
    {

        public class MockPageModel : PageModel {
        }

        [TestMethod]
        public void TestAddMessages()
        {

            InitializeHttpContext();

            PageModel model = new MockPageModel();

            model.InfoMessage("情報メッセージ1");
            model.InfoMessage("情報メッセージ2");

            model.SuccessMessage("success");
            model.WarnMessage("warn");
            model.ErrorMessage("error");


            var store = FeedbackMessageStore.Current;
            Assert.AreEqual(store.Count, 5);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.INFO).Count, 2);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.SUCCESS).Count, 1);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.WARN).Count, 1);
            Assert.AreEqual(store.GetFeedbackMessages(FeedbackMessageLevel.ERROR).Count, 1);

        }
    }
}
