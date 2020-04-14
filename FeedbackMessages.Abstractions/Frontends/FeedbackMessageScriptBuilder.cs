using System;
using System.Text;

namespace FeedbackMessages.Frontends
{
    /// <summary>
    /// This class provides js integration functionality. Used to display the message using javascript.
    /// </summary>
    public class FeedbackMessageScriptBuilder
    {

        /// <summary>
        /// <see cref="Func{T, TResult}"/> wrapper.
        /// </summary>
        private class Converter : IFeedbackMessageConverter<string>
        {
            public Func<FeedbackMessage, string> func;

            public string Convert(FeedbackMessage message)
            {
                return func.Invoke(message);
            }
        }

        /// <summary>
        /// Converter that convert <see cref="FeedbackMessage"/> to JavaScript.
        /// </summary>
        private IFeedbackMessageConverter<string> ScriptFactory { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessageScriptBuilder()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="scriptFactory"></param>
        public FeedbackMessageScriptBuilder(Func<FeedbackMessage, string> scriptFactory)
        {
            var converter = new Converter();
            converter.func = scriptFactory;
            this.ScriptFactory = converter;
        }
        /// <summary>
        /// Gets script for display unrenderd messages.
        /// </summary>
        /// <returns></returns>
        public virtual string GetScripts()
        {

            StringBuilder builder = new StringBuilder();
            foreach (var message in FeedbackMessageStore.Current.GetFeedbackMessages())
            {

                string script = ScriptFactory.Convert(message);
                builder.Append(script);
                if (!script.EndsWith(";"))
                {
                    builder.Append(";");
                }

                message.MarkRendered();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets script for display unrenderd messages.
        /// </summary>
        /// <returns></returns>
        public virtual string GetDomReadyScript()
        {
            var script =
                "document.addEventListener(\"DOMContentLoaded\", function(){"
                + GetScripts()
                + "});";

            return script;
        }
    }
}
