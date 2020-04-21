using FeedbackMessages.Test;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackMessages.AspNetCore.Test
{
    [TestClass]
    public class FeedbackMessageScriptTagHelperUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestRenderPanel()
        {
            InitializeHttpContext();
            FeedbackMessageSettings.CreateInitializer()
                .SetScriptBuilderInstance(new FeedbackMessageScriptBuilder(msg => $"alert('{msg.ToString()}');"))
                .Initialize();


            InfoMessage("InfoMessage");
            ErrorMessage("ErrorMessage");


            var tagHelper = new FeedbackMessageScriptTagHelper();

            var tagHelperContext = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), Guid.NewGuid().ToString("N"));
            var tagHelperOutput = new TagHelperOutput("feedback-message-script", new TagHelperAttributeList(), (result, encoder) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                tagHelperContent.SetHtmlContent(string.Empty);
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });
            tagHelper.Process(tagHelperContext, tagHelperOutput);


            // render, parse
            var script = tagHelperOutput.Content.GetContent();

            Assert.IsTrue(script.Contains("document.addEventListener(\"DOMContentLoaded\", function(){alert('InfoMessage');alert('ErrorMessage');});"));

        }
    }
}
