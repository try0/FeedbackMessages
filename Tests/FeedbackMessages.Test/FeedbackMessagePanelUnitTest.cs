using AngleSharp.Html.Parser;
using FeedbackMessages.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Web.UI;

namespace FeedbackMessages.Test
{
    /// <summary>
    /// <see cref="FeedbackMessageRenderer"/> tests.
    /// </summary>
    [TestClass]
    public class FeedbackMessagePanelUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestRenderPanel()
        {

            InitializeHttpContext();

            InfoMessage("Test message");

            var panel = new FeedbackMessagePanel();
            panel.ID = "fm";
            panel.Page = new Page();


            var writer = new StringWriter();
            var output = new HtmlTextWriter(writer);
            panel.RenderControl(output);

            var htmlString = writer.GetStringBuilder().ToString();

            var parser = new HtmlParser();
            var htmlDoc = parser.ParseDocument(htmlString);

            var panelElement = htmlDoc.GetElementById(panel.ClientID);
            Assert.AreEqual("div", panelElement.TagName.ToLower());

            var infoArea = panelElement.GetElementsByClassName("feedback-info")[0];
            var mes1 = infoArea.FirstChild;
            Assert.AreEqual("Test message", mes1.TextContent);

        }

        [TestMethod]
        public void TestRenderPanelWithCustomRenderer()
        {

            InitializeHttpContext();

            InfoMessage("Test message");

            var panel = new FeedbackMessagePanel();
            panel.ID = "fm";
            panel.Page = new Page();

            var renderer = new FeedbackMessageRenderer();
            renderer.OuterTagName = "div";
            renderer.InnerTagName = "p";
            panel.MessageRenderer = renderer;


            var writer = new StringWriter();
            var output = new HtmlTextWriter(writer);
            panel.RenderControl(output);

            var htmlString = writer.GetStringBuilder().ToString();

            var parser = new HtmlParser();
            var htmlDoc = parser.ParseDocument(htmlString);

            var panelElement = htmlDoc.GetElementById(panel.ClientID);
            Assert.AreEqual("div", panelElement.TagName.ToLower());

            var infoArea = panelElement.GetElementsByClassName("feedback-info")[0];
            var mes1 = infoArea.FirstChild;
            Assert.AreEqual("p", mes1.NodeName.ToLower());
            Assert.AreEqual("Test message", mes1.TextContent);

        }

    }
}
