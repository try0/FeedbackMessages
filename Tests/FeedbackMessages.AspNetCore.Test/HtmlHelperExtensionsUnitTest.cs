﻿using AngleSharp.Html.Parser;
using FeedbackMessages.Extensions;
using FeedbackMessages.Test;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Text.Encodings.Web;

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
            var helper = new HtmlHelper(
                new Mock<IHtmlGenerator>().Object,
                new Mock<ICompositeViewEngine>().Object,
                new Mock<IModelMetadataProvider>().Object,
                new Mock<IViewBufferScope>().Object,
                NullHtmlEncoder.Default,
                new Mock<UrlEncoder>().Object);

            var content = helper.FeedbackMessagePanel();


            var writer = new StringWriter();
            content.WriteTo(writer, NullHtmlEncoder.Default);

            var htmlString = writer.ToString();

            var parser = new HtmlParser();
            var htmlDoc = parser.ParseDocument(htmlString);

            var warnArea = htmlDoc.GetElementsByClassName("feedback-warn")[0];

            Assert.AreEqual(1, warnArea.ChildElementCount);

            var warnMessage = warnArea.FirstChild;

            Assert.AreEqual("Warning", warnMessage.TextContent);
        }

        [TestMethod]
        public void TestRenderScript()
        {
            InitializeHttpContext();
            FeedbackMessageSettings.CreateInitializer()
                .SetScriptBuilderInstance(new FeedbackMessageScriptBuilder(msg => $"alert('{msg.ToString()}');"))
                .Initialize();


            WarnMessage("Warning");
            var helper = new HtmlHelper(
                new Mock<IHtmlGenerator>().Object,
                new Mock<ICompositeViewEngine>().Object,
                new Mock<IModelMetadataProvider>().Object,
                new Mock<IViewBufferScope>().Object,
                NullHtmlEncoder.Default,
                new Mock<UrlEncoder>().Object);

            var content = helper.FeedbackMessageScript();


            var writer = new StringWriter();
            content.WriteTo(writer, NullHtmlEncoder.Default);

            var script = writer.ToString();

            Assert.IsTrue(script.Contains("document.addEventListener(\"DOMContentLoaded\", function(){alert('Warning');});"));

        }
    }
}
