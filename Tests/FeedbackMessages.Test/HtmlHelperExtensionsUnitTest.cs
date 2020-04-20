using AngleSharp.Html.Parser;
using FeedbackMessages.Extensions;
using FeedbackMessages.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Text.Encodings.Web;
using System.Web.Mvc;

namespace FeedbackMessages.AspNetCore.Test
{
    [TestClass]
    public class HtmlHelperExtensionsUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestRender()
        {
            InitializeHttpContext();

            WarnMessage("Warning");
            var helper = new HtmlHelper(new ViewContext(), new Mock<IViewDataContainer>().Object);

            var content = helper.FeedbackMessagePanel();


            var htmlString = content.ToHtmlString();
            var parser = new HtmlParser();
            var htmlDoc = parser.ParseDocument(htmlString);

            var warnArea = htmlDoc.GetElementsByClassName("feedback-warn")[0];
            Assert.AreEqual(warnArea.ChildElementCount, 1);

            var warnMessage = warnArea.FirstChild;
            Assert.AreEqual(warnMessage.TextContent, "Warning");
        }
    }
}
