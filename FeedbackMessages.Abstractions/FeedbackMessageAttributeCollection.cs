using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackMessages
{
    /// <summary>
    /// Attribute values.
    /// </summary>
    public class FeedbackMessageAttributeCollection : Dictionary<string, ISet<string>>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessageAttributeCollection()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="attrCollection"></param>
        public FeedbackMessageAttributeCollection(FeedbackMessageAttributeCollection attrCollection) : base(attrCollection)
        {
        }



        /// <summary>
        /// Appends attribute value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AppendAttribute(string key, string value)
        {
            if (this.ContainsKey(key))
            {
                var attr = this[key];

                attr.Add(value);
            }
            else
            {
                var set = new HashSet<string>();

                set.Add(value);

                this[key] = set;
            }
        }

        /// <summary>
        /// Merges attributes. Create new one.
        /// </summary>
        /// <param name="attrCollection"></param>
        /// <returns></returns>
        public FeedbackMessageAttributeCollection Merge(FeedbackMessageAttributeCollection attrCollection)
        {

            var newAttrCollection = new FeedbackMessageAttributeCollection(attrCollection);

            foreach (var attrEntry in this)
            {

                if (newAttrCollection.ContainsKey(attrEntry.Key))
                {

                    var set = newAttrCollection[attrEntry.Key];
                    foreach (var attr in attrEntry.Value)
                    {

                        set.Add(attr);
                    }
                }
                else
                {
                    newAttrCollection[attrEntry.Key] = new HashSet<string>(attrEntry.Value);
                }
            }

            return newAttrCollection;

        }

        /// <summary>
        /// Build attributes as StringBuilder.
        /// </summary>
        /// <returns></returns>
        public StringBuilder Build()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var attrEntry in this)
            {
                builder.Append(attrEntry.Key).Append("=\"").Append(String.Join(" ", attrEntry.Value)).Append("\" ");
            }

            return builder;
        }
    }
}
