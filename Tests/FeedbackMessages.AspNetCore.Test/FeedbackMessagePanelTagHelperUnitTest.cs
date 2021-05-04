using AngleSharp.Html.Parser;
using FeedbackMessages.Test;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FeedbackMessages.AspNetCore.Test
{
    [TestClass]
    public class FeedbackMessagePanelTagHelperUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestRenderPanel()
        {
            InitializeHttpContext();

            InfoMessage("InfoMessage");
            ErrorMessage("ErrorMessage");


            var tagHelper = new FeedbackMessagePanelTagHelper();

            var tagHelperContext = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("N"));
            var tagHelperOutput = new TagHelperOutput("feedback-message-panel", new TagHelperAttributeList(), (result, encoder) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                tagHelperContent.SetHtmlContent(string.Empty);
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });
            tagHelper.Process(tagHelperContext, tagHelperOutput);


            // render, parse
            var htmlString = tagHelperOutput.Content.GetContent();
            var parser = new HtmlParser();
            var htmlDoc = parser.ParseDocument(htmlString);

            // information message
            var infoArea = htmlDoc.GetElementsByClassName("feedback-info")[0];
            Assert.AreEqual("ul", infoArea.TagName.ToLower());
            Assert.AreEqual(1, infoArea.ChildElementCount);

            var infoMessage = infoArea.FirstChild;
            Assert.AreEqual("li", infoMessage.NodeName.ToLower());
            Assert.AreEqual("InfoMessage", infoMessage.TextContent);


            // error message
            var errorArea = htmlDoc.GetElementsByClassName("feedback-error")[0];
            Assert.AreEqual("ul", infoArea.TagName.ToLower());
            Assert.AreEqual(1, infoArea.ChildElementCount);

            var errorMessage = errorArea.FirstChild;
            Assert.AreEqual("li", errorMessage.NodeName.ToLower());
            Assert.AreEqual("ErrorMessage", errorMessage.TextContent);
        }
    }
}
