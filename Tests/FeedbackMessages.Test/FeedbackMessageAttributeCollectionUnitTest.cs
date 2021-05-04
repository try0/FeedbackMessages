using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeedbackMessages.Test
{
    [TestClass]
    public class FeedbackMessageAttributeCollectionUnitTest : FeedbackMessagesUnitTestBase
    {

        [TestMethod]
        public void TestAddAttributeValue()
        {
            var attributes = new FeedbackMessageAttributeCollection();

            attributes.AppendAttribute("class", "test1");

            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual(1, attributes["class"].Count);
        }


        [TestMethod]
        public void TestAddAttributeMultiValue()
        {
            var attributes = new FeedbackMessageAttributeCollection();

            attributes.AppendAttribute("class", "test1");

            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual(1, attributes["class"].Count);

            attributes.AppendAttribute("class", "test2");
            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual(2, attributes["class"].Count);

        }


        [TestMethod]
        public void TestAddAttributeDuplicateValue()
        {
            var attributes = new FeedbackMessageAttributeCollection();

            attributes.AppendAttribute("class", "test1");

            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual(1, attributes["class"].Count);

            attributes.AppendAttribute("class", "test1");
            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual(1, attributes["class"].Count);

        }

        [TestMethod]
        public void TestAddMultiAttributeValue()
        {
            var attributes = new FeedbackMessageAttributeCollection();

            attributes.AppendAttribute("class", "test1");

            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual(1, attributes["class"].Count);

            attributes.AppendAttribute("data-test", "test1");
            Assert.AreEqual(2, attributes.Count);
            Assert.AreEqual(1, attributes["data-test"].Count);

        }

        [TestMethod]
        public void TestMergeExistsSameAttribute()
        {
            var attributes1 = new FeedbackMessageAttributeCollection();
            attributes1.AppendAttribute("class", "test1");

            var attributes2 = new FeedbackMessageAttributeCollection();
            attributes1.AppendAttribute("class", "test2");

            var mergedAttributes = attributes1.Merge(attributes2);

            Assert.AreEqual(1, mergedAttributes.Count);
            Assert.AreEqual(2, mergedAttributes["class"].Count);

        }

        [TestMethod]
        public void TestMergeExistsSameValue()
        {
            var attributes1 = new FeedbackMessageAttributeCollection();
            attributes1.AppendAttribute("class", "test1");

            var attributes2 = new FeedbackMessageAttributeCollection();
            attributes1.AppendAttribute("name", "test1");

            var mergedAttributes = attributes1.Merge(attributes2);

            Assert.AreEqual(2, mergedAttributes.Count);
            Assert.AreEqual(1, mergedAttributes["class"].Count);
            Assert.AreEqual(1, mergedAttributes["name"].Count);
        }

        [TestMethod]
        public void TestMergeExistsSameValueInSameAttribute()
        {
            var attributes1 = new FeedbackMessageAttributeCollection();
            attributes1.AppendAttribute("class", "test1");

            var attributes2 = new FeedbackMessageAttributeCollection();
            attributes1.AppendAttribute("class", "test1");

            var mergedAttributes = attributes1.Merge(attributes2);

            Assert.AreEqual(1, mergedAttributes.Count);
            Assert.AreEqual(1, mergedAttributes["class"].Count);

        }

        [TestMethod]
        public void TestBuild()
        {

            var attributes = new FeedbackMessageAttributeCollection();
            attributes.AppendAttribute("class", "val1");
            attributes.AppendAttribute("class", "val2");
            attributes.AppendAttribute("class", "val3");
            attributes.AppendAttribute("class", "val4");

            var str = attributes.Build().ToString();

            Assert.AreEqual("class=\"val1 val2 val3 val4\" ", str);

        }

        [TestMethod]
        public void TestBuildMultiAttribute()
        {

            var attributes = new FeedbackMessageAttributeCollection();
            attributes.AppendAttribute("class", "val1");
            attributes.AppendAttribute("class", "val2");
            attributes.AppendAttribute("class", "val3");
            attributes.AppendAttribute("class", "val4");

            attributes.AppendAttribute("name", "nameval");

            var str = attributes.Build().ToString();

            Assert.AreEqual("class=\"val1 val2 val3 val4\" name=\"nameval\" ", str);

        }

    }
}
