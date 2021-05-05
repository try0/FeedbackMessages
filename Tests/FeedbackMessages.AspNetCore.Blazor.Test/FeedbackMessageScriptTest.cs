using Bunit;
using FeedbackMessages.Components;
using FeedbackMessages.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackMessages.AspNetCore.Blazor.Test
{
    /// <summary>
    /// <see cref="FeedbackMessageScript"/> tests.
    /// </summary>
    [TestClass]
    public class FeedbackMessageScriptTest : FeedbackMessagesUnitTestBase
    {
        [TestMethod]
        public void TestRenderScript()
        {
            InitializeHttpContext();
            InitializeSettings();
            using var ctx = new Bunit.TestContext();

            string renderMessage = "Info message";
            InfoMessage(renderMessage);

            var renderedComponent = ctx.RenderComponent<FeedbackMessageScript>();

            // information message
            var scriptElement = renderedComponent.Find("#fms");
            Assert.AreEqual("script", scriptElement.TagName.ToLower());
            Assert.AreEqual(0, scriptElement.ChildElementCount);
            Assert.IsTrue(scriptElement.InnerHtml.Contains("function renderFeedbackMessage()"));

            var attr = scriptElement.Attributes["data-fmscript"];
            Assert.AreEqual("alert('Info message');", attr.Value);

            ctx.JSInterop.VerifyInvoke("renderFeedbackMessage");
        }

        /// <summary>
        /// <see cref="FeedbackMessageScript.RefreshRender"/>
        /// </summary>
        [TestMethod]
        public void TestRefreshRender()
        {
            InitializeHttpContext();
            InitializeSettings();
            using var ctx = new Bunit.TestContext();

            string renderMessage = "Info message";
            InfoMessage(renderMessage);

            var renderedComponent = ctx.RenderComponent<FeedbackMessageScript>();

            // information message
            {
                var scriptElement = renderedComponent.Find("#fms");
                Assert.AreEqual("script", scriptElement.TagName.ToLower());
                Assert.AreEqual(0, scriptElement.ChildElementCount);
                Assert.IsTrue(scriptElement.InnerHtml.Contains("function renderFeedbackMessage()"));

                var attr = scriptElement.Attributes["data-fmscript"];
                Assert.AreEqual("alert('Info message');", attr.Value);

                ctx.JSInterop.VerifyInvoke("renderFeedbackMessage");
            }
           
            // refresh component
            string warnMessage = "Warn message";
            WarnMessage(warnMessage);
            renderedComponent.InvokeAsync(() => renderedComponent.Instance.RefreshRender());

            // warning message
            {
                var scriptElement = renderedComponent.Find("#fms");
                Assert.AreEqual("script", scriptElement.TagName.ToLower());
                Assert.AreEqual(0, scriptElement.ChildElementCount);
                Assert.IsTrue(scriptElement.InnerHtml.Contains("function renderFeedbackMessage()"));

                var attr = scriptElement.Attributes["data-fmscript"];
                Assert.AreEqual("alert('Warn message');", attr.Value);

                ctx.JSInterop.VerifyInvoke("renderFeedbackMessage", 2);
            }
           
        }
    }
}
