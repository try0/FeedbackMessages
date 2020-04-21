using System.Collections.Generic;
using System.Text;

using static FeedbackMessages.FeedbackMessage;

namespace FeedbackMessages
{
    /// <summary>
    /// Message renderer.
    /// </summary>
    public class FeedbackMessageRenderer
    {
        /// <summary>
        /// Outer tag
        /// </summary>
        public string OuterTagName { get; set; } = "ul";

        /// <summary>
        /// Inner tag
        /// </summary>
        public string InnerTagName { get; set; } = "li";

        /// <summary>
        /// Outer tag attributes
        /// </summary>
        private FeedbackMessageAttributeCollection OuterTagAttributes { get; set; } = new FeedbackMessageAttributeCollection();

        /// <summary>
        /// Inner tag attributes
        /// </summary>
        private FeedbackMessageAttributeCollection InnerTagAttributes { get; set; } = new FeedbackMessageAttributeCollection();


        /// <summary>
        /// 
        /// </summary>
        private IDictionary<FeedbackMessage.FeedbackMessageLevel, FeedbackMessageAttributeCollection> PerLevelOuterTagAttributes { get; set; } = new Dictionary<FeedbackMessage.FeedbackMessageLevel, FeedbackMessageAttributeCollection>();

        /// <summary>
        /// 
        /// </summary>
        private IDictionary<FeedbackMessage.FeedbackMessageLevel, FeedbackMessageAttributeCollection> PerLevelInnerTagAttributes { get; set; } = new Dictionary<FeedbackMessage.FeedbackMessageLevel, FeedbackMessageAttributeCollection>();


        /// <summary>
        /// Wheter escape message or not
        /// </summary>
        public bool EscapeMessage { get; set; } = false;

        /// <summary>
        /// Converter that convert <see cref="FeedbackMessage"/> to string.
        /// </summary>
        public IFeedbackMessageConverter<string> StringConverter { get; set; } = new FeedbackMessageStringConverter(msg => msg.ToString());

        /// <summary>
        /// Appends attribute value to outer tag.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public FeedbackMessageRenderer AppendOuterAttributeValue(string key, string attrValue)
        {
            OuterTagAttributes.AppendAttribute(key, attrValue);
            return this;
        }

        /// <summary>
        /// Appends attribute value to outer tag.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="key"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public FeedbackMessageRenderer AppendOuterAttributeValue(FeedbackMessageLevel level, string key, string attrValue)
        {

            FeedbackMessageAttributeCollection attrCollection;
            if (PerLevelOuterTagAttributes.ContainsKey(level))
            {
                attrCollection = PerLevelOuterTagAttributes[level];
            }
            else
            {
                attrCollection = new FeedbackMessageAttributeCollection();
                PerLevelOuterTagAttributes[level] = attrCollection;
            }

            attrCollection.AppendAttribute(key, attrValue);
            return this;
        }

        /// <summary>
        /// Appends attribute value to inner tag.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public FeedbackMessageRenderer AppendInnerAttributeValue(string key, string attrValue)
        {
            InnerTagAttributes.AppendAttribute(key, attrValue);
            return this;
        }

        /// <summary>
        /// Appends attribute value to inner tag.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="key"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        public FeedbackMessageRenderer AppendInnerAttributeValue(FeedbackMessageLevel level, string key, string attrValue)
        {

            FeedbackMessageAttributeCollection attrCollection;
            if (PerLevelInnerTagAttributes.ContainsKey(level))
            {
                attrCollection = PerLevelInnerTagAttributes[level];
            }
            else
            {
                attrCollection = new FeedbackMessageAttributeCollection();
                PerLevelInnerTagAttributes[level] = attrCollection;
            }

            attrCollection.AppendAttribute(key, attrValue);
            return this;
        }


        /// <summary>
        /// Render messages tag.
        /// </summary>
        /// <returns></returns>
        public StringBuilder RenderMessages()
        {

            StringBuilder output = new StringBuilder();



            var messageStore = FeedbackMessageStore.Current;
            foreach (var messages in messageStore.Messages)
            {

                if (messages.Value.Count == 0)
                {
                    continue;
                }



                FeedbackMessageAttributeCollection outerAttrs;
                if (PerLevelOuterTagAttributes.ContainsKey(messages.Key))
                {
                    outerAttrs = OuterTagAttributes.Merge(PerLevelOuterTagAttributes[messages.Key]);
                }
                else
                {
                    outerAttrs = new FeedbackMessageAttributeCollection(OuterTagAttributes);
                }

                outerAttrs.AppendAttribute("class", $"feedback-{ messages.Key.ToString().ToLower() }");


                FeedbackMessageAttributeCollection innerAttrs;
                if (PerLevelInnerTagAttributes.ContainsKey(messages.Key))
                {
                    innerAttrs = InnerTagAttributes.Merge(PerLevelInnerTagAttributes[messages.Key]);
                }
                else
                {
                    innerAttrs = new FeedbackMessageAttributeCollection(InnerTagAttributes);
                }


                output.Append($"<{OuterTagName} {outerAttrs.Build().ToString()}>");
                messages.Value.ForEach(msg =>
                {
                    output.Append($"<{InnerTagName} {innerAttrs.Build().ToString()}>");

                    string message = StringConverter.Convert(msg);
                    output.Append(EscapeMessage ? System.Web.HttpUtility.HtmlEncode(message) : message);
                    output.Append($"</{InnerTagName}>");
                    msg.MarkRendered();
                });
                output.Append($"</{OuterTagName}>");

            }

            return output;
        }




    }
}
