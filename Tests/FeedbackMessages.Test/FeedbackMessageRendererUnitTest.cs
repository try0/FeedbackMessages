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
        public void TestDefaultProperties()
        {
            var renderer = new FeedbackMessageRenderer();

            Assert.IsNotNull(renderer.OuterTagName);
            Assert.IsNotNull(renderer.InnerTagName);

            Assert.AreEqual("ul", renderer.OuterTagName);
            Assert.AreEqual("li", renderer.InnerTagName);

            Assert.IsFalse(renderer.EscapeMessage);

            Assert.IsNotNull(renderer.StringConverter);
        }


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
            Assert.AreEqual("div", outer.NodeName.ToLower());
            Assert.IsTrue(outer.Attributes["class"].Value.Contains("feedback-info"));



            var inner = htmlDoc.GetElementById("inner");
            Assert.IsNotNull(inner);
            Assert.AreEqual("div", inner.NodeName.ToLower());
            Assert.AreEqual("TestMessage", inner.TextContent);
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
            Assert.AreEqual("div", outer.NodeName.ToLower());
            Assert.IsTrue(outer.Attributes["class"].Value.Contains("feedback-info"));


            Assert.AreEqual(2, outer.ChildElementCount);

            var mes1 = outer.FirstChild;

            Assert.IsNotNull(mes1);
            Assert.AreEqual("div", mes1.NodeName.ToLower());
            Assert.AreEqual("TestMessage1", mes1.TextContent);

            var mes2 = mes1.NextSibling;
            Assert.IsNotNull(mes2);
            Assert.AreEqual("div", mes2.NodeName.ToLower());
            Assert.AreEqual("TestMessage2", mes2.TextContent);
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
                Assert.AreEqual("div", outer.NodeName.ToLower());
                Assert.IsTrue(outer.Attributes["class"].Value.Contains("feedback-info"));


                Assert.AreEqual(2, outer.ChildElementCount);

                var mes1 = outer.FirstChild;

                Assert.IsNotNull(mes1);
                Assert.AreEqual("span", mes1.NodeName.ToLower());
                Assert.AreEqual("InfoTestMessage1", mes1.TextContent);

                var mes2 = mes1.NextSibling;
                Assert.IsNotNull(mes2);
                Assert.AreEqual("span", mes2.NodeName.ToLower());
                Assert.AreEqual("InfoTestMessage2", mes2.TextContent);
            }

            // error
            {
                var outer = htmlDoc.GetElementById("outer-error");
                Assert.IsNotNull(outer);
                Assert.AreEqual("div", outer.NodeName.ToLower());
                Assert.IsTrue(outer.Attributes["class"].Value.Contains("feedback-error"));


                Assert.AreEqual(3, outer.ChildElementCount);

                var mes1 = outer.FirstChild;

                Assert.IsNotNull(mes1);
                Assert.AreEqual("span", mes1.NodeName.ToLower());
                Assert.AreEqual("ErrorTestMessage1", mes1.TextContent);

                var mes2 = mes1.NextSibling;
                Assert.IsNotNull(mes2);
                Assert.AreEqual("span", mes2.NodeName.ToLower());
                Assert.AreEqual("ErrorTestMessage2", mes2.TextContent);

                var mes3 = mes2.NextSibling;
                Assert.IsNotNull(mes3);
                Assert.AreEqual("span", mes3.NodeName.ToLower());
                Assert.AreEqual("ErrorTestMessage3", mes3.TextContent);

            }


        }
    }
}
