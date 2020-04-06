using System.Collections.Generic;
using System.Text;
using System.Web;

namespace FeedbackMessages
{
    public class FeedbackMessageRenderer
    {
        /// <summary>
        /// Per-Level tag
        /// </summary>
        public string OuterTagName { get; set; } = "ul";

        /// <summary>
        /// Per-Message tag
        /// </summary>
        public string InnerTagName { get; set; } = "li";

        /// <summary>
        /// Per-Level attributes
        /// </summary>
        private IDictionary<string, string> OuterTagAttributes { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Per-Message attributes
        /// </summary>
        private IDictionary<string, string> InnerTagAttributes { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Wheter escape message or not
        /// </summary>
        public bool EscapeMessage { get; set; } = false;


        private void AppendAttributeValue(IDictionary<string, string> attrs, string key, string value)
        {

            if (attrs.ContainsKey(key))
            {
                var attr = attrs[key];

                attrs[key] = attr + " " + value;
            }
            else
            {
                attrs[key] = value;
            }
        }

        public FeedbackMessageRenderer AppendOuterAttributeValue(string key, string attrValue)
        {
            AppendAttributeValue(OuterTagAttributes, key, attrValue);
            return this;
        }

        public FeedbackMessageRenderer AppendInnerAttributeValue(string key, string attrValue)
        {
            AppendAttributeValue(InnerTagAttributes, key, attrValue);
            return this;
        }




        public StringBuilder RenderMessages()
        {

            StringBuilder output = new StringBuilder();


            string outerAttrClass = OuterTagAttributes["class"];
            if (outerAttrClass == null)
            {
                outerAttrClass = "";
            }

            StringBuilder outerAttrBuilder = new StringBuilder(); 
            foreach (var attrEntry in OuterTagAttributes)
            {

                if (attrEntry.Key.Equals("class"))
                {
                    continue;
                }

                outerAttrBuilder.Append(attrEntry.Key).Append("=\"").Append(attrEntry.Value).Append("\" ");
            }

            StringBuilder innerAttrBuilder = new StringBuilder();
            foreach (var attrEntry in InnerTagAttributes)
            {

                if (attrEntry.Key.Equals("class"))
                {
                    continue;
                }

                innerAttrBuilder.Append(attrEntry.Key).Append("=\"").Append(attrEntry.Value).Append("\" ");
            }

            var messageStore = FeedbackMessageStore.Current;
            foreach (var messages in messageStore.Messages)
            {

                if (messages.Value.Count == 0)
                {
                    continue;
                }

 
                output.Append($"<{OuterTagName} class=\"feedback-{ messages.Key.ToString().ToLower() } {outerAttrClass}\" {outerAttrBuilder.ToString()}>");
                messages.Value.ForEach(msg =>
                {
                    output.Append($"<{InnerTagName} {innerAttrBuilder.ToString()}>");

                    string message = msg.Message.ToString();
                    output.Append(EscapeMessage ? HttpUtility.HtmlEncode(message) : message);
                    output.Append($"</{InnerTagName}>");
                    msg.MarkRender();
                });
                output.Append($"</{OuterTagName}>");

            }

            return output;
        }




    }
}
