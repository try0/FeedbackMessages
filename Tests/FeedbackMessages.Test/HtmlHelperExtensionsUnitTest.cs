using AngleSharp.Html.Parser;
using FeedbackMessages.Extensions;
using FeedbackMessages.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;

namespace FeedbackMessages.AspNetCore.Test
{
    [TestClass]
    public class HtmlHelperExtensionsUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestRenderPanel()
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

        [TestMethod]
        public void TestRenderScript()
        {
            InitializeHttpContext();
            FeedbackMessageSettings.Initializer
                .SetScriptBuilderInstance(new FeedbackMessageScriptBuilder(msg => $"alert('{msg.ToString()}');"))
                .Initialize();


            WarnMessage("Warning");
            var helper = new HtmlHelper(new ViewContext(), new Mock<IViewDataContainer>().Object);

            var content = helper.FeedbackMessageScript();

            var script = content.ToHtmlString();

            Assert.IsTrue(script.Contains("document.addEventListener(\"DOMContentLoaded\", function(){alert('Warning');});"));

        }
    }
}
