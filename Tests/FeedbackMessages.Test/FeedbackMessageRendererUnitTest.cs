using AngleSharp.Html.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeedbackMessages.Test
{
    /// <summary>
    /// <see cref="FeedbackMessageRenderer"/> tests.
    /// </summary>
    [TestClass]
    public class FeedbackMessageRendererUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestAppendAttributeValue()
        {

            InitializeHttpContext();

            InfoMessage("TestMessage");


            var renderer = new FeedbackMessageRenderer();
            renderer.OuterTagName = "div";
            renderer.InnerTagName = "div";

            renderer.AppendOuterAttributeValue("id", "outer");
            renderer.AppendInnerAttributeValue("id", "inner");

            {

                var htmlString = renderer.RenderMessages().ToString();
                var htmlParser = new HtmlParser();

                var htmlDoc = htmlParser.ParseDocument(htmlString);
                var outer = htmlDoc.GetElementById("outer");
                Assert.IsNotNull(outer);
                var inner = htmlDoc.GetElementById("inner");
                Assert.IsNotNull(inner);
            }



            renderer.AppendOuterAttributeValue("class", "outer");
            renderer.AppendInnerAttributeValue("class", "inner");

            {
                var htmlString = renderer.RenderMessages().ToString();
                var htmlParser = new HtmlParser();

                var htmlDoc = htmlParser.ParseDocument(htmlString);
                var outer = htmlDoc.GetElementById("outer");
                Assert.IsNotNull(outer);
                Assert.IsTrue(outer.Attributes["class"].Value.Contains("outer"));
                var inner = htmlDoc.GetElementById("inner");
                Assert.IsNotNull(inner);
                Assert.IsTrue(inner.Attributes["class"].Value.Contains("inner"));
            }
        }

        [TestMethod]
        public void TestRenderMessages()
        {

            InitializeHttpContext();

            InfoMessage("TestMessage");


            var renderer = new FeedbackMessageRenderer();
            renderer.OuterTagName = "div";
            renderer.InnerTagName = "div";

            renderer.AppendOuterAttributeValue("id", "outer");
            renderer.AppendInnerAttributeValue("id", "inner");



            var htmlString = renderer.RenderMessages().ToString();

            var htmlParser = new HtmlParser();
            var htmlDoc = htmlParser.ParseDocument(htmlString);


            var outer = htmlDoc.GetElementById("outer");
            Assert.IsNotNull(outer);
            Assert.AreEqual(outer.NodeName.ToLower(), "div");
            Assert.IsTrue(outer.Attributes["class"].Value.Contains("feedback-info"));



            var inner = htmlDoc.GetElementById("inner");
            Assert.IsNotNull(inner);
            Assert.AreEqual(inner.NodeName.ToLower(), "div");
            Assert.AreEqual(inner.TextContent, "TestMessage");
        }

        [TestMethod]
        public void TestRenderMessagesMultiMessage()
        {

            InitializeHttpContext();

            InfoMessage("TestMessage1");
            InfoMessage("TestMessage2");

            var renderer = new FeedbackMessageRenderer();
            renderer.OuterTagName = "div";
            renderer.InnerTagName = "div";

            renderer.AppendOuterAttributeValue("id", "outer");



            var htmlString = renderer.RenderMessages().ToString();

            var htmlParser = new HtmlParser();
            var htmlDoc = htmlParser.ParseDocument(htmlString);


            var outer = htmlDoc.GetElementById("outer");
            Assert.IsNotNull(outer);
            Assert.AreEqual(outer.NodeName.ToLower(), "div");
            Assert.IsTrue(outer.Attributes["class"].Value.Contains("feedback-info"));


            Assert.AreEqual(outer.ChildElementCount, 2);

            var mes1 = outer.FirstChild;

            Assert.IsNotNull(mes1);
            Assert.AreEqual(mes1.NodeName.ToLower(), "div");
            Assert.AreEqual(mes1.TextContent, "TestMessage1");

            var mes2 = mes1.NextSibling;
            Assert.IsNotNull(mes2);
            Assert.AreEqual(mes2.NodeName.ToLower(), "div");
            Assert.AreEqual(mes2.TextContent, "TestMessage2");
        }

        [TestMethod]
        public void TestRenderMessagesMultiLevelMessage()
        {

            InitializeHttpContext();

            InfoMessage("InfoTestMessage1");
            InfoMessage("InfoTestMessage2");

            ErrorMessage("ErrorTestMessage1");
            ErrorMessage("ErrorTestMessage2");
            ErrorMessage("ErrorTestMessage3");

            var renderer = new FeedbackMessageRenderer();
            renderer.OuterTagName = "div";
            renderer.InnerTagName = "span";

            renderer.AppendOuterAttributeValue(FeedbackMessage.FeedbackMessageLevel.INFO, "id", "outer-info");
            renderer.AppendOuterAttributeValue(FeedbackMessage.FeedbackMessageLevel.ERROR, "id", "outer-error");



            var htmlString = renderer.RenderMessages().ToString();

            var htmlParser = new HtmlParser();
            var htmlDoc = htmlParser.ParseDocument(htmlString);


            // info 
            {
                var outer = htmlDoc.GetElementById("outer-info");
                Assert.IsNotNull(outer);
                Assert.AreEqual(outer.NodeName.ToLower(), "div");
                Assert.IsTrue(outer.Attributes["class"].Value.Contains("feedback-info"));


                Assert.AreEqual(outer.ChildElementCount, 2);

                var mes1 = outer.FirstChild;

                Assert.IsNotNull(mes1);
                Assert.AreEqual(mes1.NodeName.ToLower(), "span");
                Assert.AreEqual(mes1.TextContent, "InfoTestMessage1");

                var mes2 = mes1.NextSibling;
                Assert.IsNotNull(mes2);
                Assert.AreEqual(mes2.NodeName.ToLower(), "span");
                Assert.AreEqual(mes2.TextContent, "InfoTestMessage2");
            }

            // error
            {
                var outer = htmlDoc.GetElementById("outer-error");
                Assert.IsNotNull(outer);
                Assert.AreEqual(outer.NodeName.ToLower(), "div");
                Assert.IsTrue(outer.Attributes["class"].Value.Contains("feedback-error"));


                Assert.AreEqual(outer.ChildElementCount, 3);

                var mes1 = outer.FirstChild;

                Assert.IsNotNull(mes1);
                Assert.AreEqual(mes1.NodeName.ToLower(), "span");
                Assert.AreEqual(mes1.TextContent, "ErrorTestMessage1");

                var mes2 = mes1.NextSibling;
                Assert.IsNotNull(mes2);
                Assert.AreEqual(mes2.NodeName.ToLower(), "span");
                Assert.AreEqual(mes2.TextContent, "ErrorTestMessage2");

                var mes3 = mes2.NextSibling;
                Assert.IsNotNull(mes3);
                Assert.AreEqual(mes3.NodeName.ToLower(), "span");
                Assert.AreEqual(mes3.TextContent, "ErrorTestMessage3");

            }


        }
    }
}
