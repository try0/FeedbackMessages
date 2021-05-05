using Bunit;
using FeedbackMessages.Components;
using FeedbackMessages.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeedbackMessages.AspNetCore.Blazor.Test
{
    /// <summary>
    /// <see cref="FeedbackMessagePanel"/> tests.
    /// </summary>
    [TestClass]
    public class FeedbackMessagePanelTest : FeedbackMessagesUnitTestBase
    {
        [TestMethod]
        public void TestRenderPanel()
        {
            InitializeHttpContext();
            using var ctx = new Bunit.TestContext();

            string renderMessage = "Info message";
            InfoMessage(renderMessage);

            var renderedComponent = ctx.RenderComponent<FeedbackMessagePanel>();
            
            // information message
            var infoArea = renderedComponent.Find(".feedback-info");
            Assert.AreEqual("ul", infoArea.TagName.ToLower());
            Assert.AreEqual(1, infoArea.ChildElementCount);
            Assert.AreEqual(renderMessage, infoArea.Children[0].InnerHtml);
        }

        /// <summary>
        /// <see cref="FeedbackMessagePanel.RefreshRender"/>
        /// </summary>
        [TestMethod]
        public void TestRefreshRender()
        {
            InitializeHttpContext();
            using var ctx = new Bunit.TestContext();

            string infoMessage = "Info message";
            InfoMessage(infoMessage);

            var renderedComponent = ctx.RenderComponent<FeedbackMessagePanel>();

            // information message
            var infoArea = renderedComponent.Find(".feedback-info");

            Assert.AreEqual("ul", infoArea.TagName.ToLower());
            Assert.AreEqual(1, infoArea.ChildElementCount);
            Assert.AreEqual(infoMessage, infoArea.Children[0].InnerHtml);

            // refresh component
            string warnMessage =  "Warn message";
            WarnMessage(warnMessage);
            renderedComponent.InvokeAsync(() => renderedComponent.Instance.RefreshRender());

            // warning message
            var warnArea = renderedComponent.Find(".feedback-warn");

            Assert.AreEqual("ul", warnArea.TagName.ToLower());
            Assert.AreEqual(1, warnArea.ChildElementCount);
            Assert.AreEqual(warnMessage, warnArea.Children[0].InnerHtml);
        }
    }
}
