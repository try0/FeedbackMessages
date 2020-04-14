using FeedbackMessages.Frontends;
using System;

namespace FeedbackMessages
{
    public class FeedbackMessageSettings
    {
        // TODO
        public class FeedbackMessageConfig
        {


        }

        /// <summary>
        /// Gets setting initializer.
        /// </summary>
        public static FeedbackMessageSettingsInitializer Initializer => new FeedbackMessageSettingsInitializer();

        /// <summary>
        /// <see cref="FeedbackMessageSettings"/> initializer.
        /// </summary>
        public class FeedbackMessageSettingsInitializer
        {

            FeedbackMessageRenderer messageRenderer;
            FeedbackMessageScriptBuilder scriptBuilder;
            FeedbackMessageConfig config;

            /// <summary>
            /// Sets default message renderer.
            /// </summary>
            /// <param name="messageRenderer"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetMessageRenderer(FeedbackMessageRenderer messageRenderer)
            {
                this.messageRenderer = messageRenderer;
                return this;
            }

            /// <summary>
            /// Sets factory that create default message renderer.
            /// </summary>
            /// <param name="factory"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetMessageRenderer(Func<FeedbackMessageRenderer> factory)
            {
                this.messageRenderer = factory.Invoke();
                return this;
            }

            /// <summary>
            /// Sets default script builder.
            /// </summary>
            /// <param name="scriptBuilder"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetScriptBuilder(FeedbackMessageScriptBuilder scriptBuilder)
            {
                this.scriptBuilder = scriptBuilder;
                return this;
            }

            /// <summary>
            /// Sets factory that create default script builder.
            /// </summary>
            /// <param name="factory"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetScriptBuilder(Func<FeedbackMessageScriptBuilder> factory)
            {
                this.scriptBuilder = factory.Invoke();
                return this;
            }

            /// <summary>
            /// Sets default congig.
            /// </summary>
            /// <param name="config"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetConfig(FeedbackMessageConfig config)
            {
                this.config = config;
                return this;
            }

            /// <summary>
            /// Sets factory that create default config.
            /// </summary>
            /// <param name="factory"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetConfig(Func<FeedbackMessageConfig> factory)
            {
                this.config = factory.Invoke();
                return this;
            }

            /// <summary>
            /// Initializes <see cref="FeedbackMessageSettings"/>.
            /// </summary>
            /// <returns></returns>
            public FeedbackMessageSettings Initialize()
            {

                var settings = new FeedbackMessageSettings();

                if (messageRenderer != null)
                {
                    settings.MessageRenderer = messageRenderer;
                }

                if (scriptBuilder != null)
                {
                    settings.ScriptBuilder = scriptBuilder;
                }

                if (config != null)
                {
                    settings.Config = config;
                }

                FeedbackMessageSettings.Instance = settings;

                return settings;
            }
        }


        /// <summary>
        /// Setting values.
        /// </summary>
        public static FeedbackMessageSettings Instance { get; private set; } = CreateDefaultSettings();

        /// <summary>
        /// new default instance.
        /// </summary>
        /// <returns></returns>
        private static FeedbackMessageSettings CreateDefaultSettings()
        {
            var settings = new FeedbackMessageSettings();

            return settings;
        }

        /// <summary>
        /// Message renderer
        /// </summary>
        public FeedbackMessageRenderer MessageRenderer { get; protected set; } = new FeedbackMessageRenderer();

        /// <summary>
        /// Script builder
        /// </summary>
        public FeedbackMessageScriptBuilder ScriptBuilder { get; protected set; } = new FeedbackMessageScriptBuilder(msg => throw new System.Exception("FeedbackMessageSettings.ScriptBuilder: must set your instance."));

        /// <summary>
        /// Config values
        /// </summary>
        public FeedbackMessageConfig Config { get; protected set; } = new FeedbackMessageConfig();
    }
}
