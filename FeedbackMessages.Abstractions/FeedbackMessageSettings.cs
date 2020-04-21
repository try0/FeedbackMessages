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
        /// Creates setting initializer.
        /// </summary>
        public static FeedbackMessageSettingsInitializer CreateInitializer() => new FeedbackMessageSettingsInitializer();

        /// <summary>
        /// <see cref="FeedbackMessageSettings"/> initializer.
        /// </summary>
        public class FeedbackMessageSettingsInitializer
        {

            Func<FeedbackMessageRenderer> messageRendererFactory;
            Func<FeedbackMessageScriptBuilder> scriptBuilderFactory;
            Func<IFeedbackMessageStoreSerializer> storeSerializerFactory;
            Func<FeedbackMessageConfig> configFactory;

            /// <summary>
            /// Sets default message renderer.
            /// </summary>
            /// <param name="messageRenderer"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetMessageRendererInstance(FeedbackMessageRenderer messageRenderer)
            {
                this.messageRendererFactory = () => messageRenderer;
                return this;
            }

            /// <summary>
            /// Sets factory that create default message renderer.
            /// </summary>
            /// <param name="factory"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetMessageRendererFactory(Func<FeedbackMessageRenderer> factory)
            {
                this.messageRendererFactory = factory;
                return this;
            }

            /// <summary>
            /// Sets default script builder.
            /// </summary>
            /// <param name="scriptBuilder"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetScriptBuilderInstance(FeedbackMessageScriptBuilder scriptBuilder)
            {
                this.scriptBuilderFactory = () => scriptBuilder;
                return this;
            }

            /// <summary>
            /// Sets factory that create default script builder.
            /// </summary>
            /// <param name="factory"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetScriptBuilderFactory(Func<FeedbackMessageScriptBuilder> factory)
            {
                this.scriptBuilderFactory = factory;
                return this;
            }

            public FeedbackMessageSettingsInitializer SetStoreSerializerInstance(IFeedbackMessageStoreSerializer storeSerializer)
            {
                this.storeSerializerFactory = () => storeSerializer;
                return this;
            }

            public FeedbackMessageSettingsInitializer SetStoreSerializerFactory(Func<IFeedbackMessageStoreSerializer> factory)
            {
                this.storeSerializerFactory = factory;
                return this;
            }

            /// <summary>
            /// Sets default congig.
            /// </summary>
            /// <param name="config"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetConfigInstance(FeedbackMessageConfig config)
            {
                this.configFactory = () => config;
                return this;
            }

            /// <summary>
            /// Sets factory that create default config.
            /// </summary>
            /// <param name="factory"></param>
            /// <returns></returns>
            public FeedbackMessageSettingsInitializer SetConfigFactory(Func<FeedbackMessageConfig> factory)
            {
                this.configFactory = factory;
                return this;
            }

            /// <summary>
            /// Initializes <see cref="FeedbackMessageSettings"/>.
            /// </summary>
            /// <returns></returns>
            public FeedbackMessageSettings Initialize()
            {

                var settings = new FeedbackMessageSettings();

                if (messageRendererFactory != null)
                {
                    settings.messageRendererFactory = messageRendererFactory;
                }

                if (scriptBuilderFactory != null)
                {
                    settings.scriptBuilderFactory = scriptBuilderFactory;
                }

                if (storeSerializerFactory != null)
                {
                    settings.storeSerializerFactory = storeSerializerFactory;
                }

                if (configFactory != null)
                {
                    settings.configFactory = configFactory;
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


        Func<FeedbackMessageRenderer> messageRendererFactory = () => new FeedbackMessageRenderer();
        Func<FeedbackMessageScriptBuilder> scriptBuilderFactory = () => new FeedbackMessageScriptBuilder(msg => throw new System.Exception("FeedbackMessageSettings.ScriptBuilder: must set your instance."));
        Func<IFeedbackMessageStoreSerializer> storeSerializerFactory = () => new FeedbackMessageStoreSerializer();
        Func<FeedbackMessageConfig> configFactory = () => new FeedbackMessageConfig();

        /// <summary>
        /// Message renderer
        /// </summary>
        public FeedbackMessageRenderer MessageRenderer => messageRendererFactory.Invoke();

        /// <summary>
        /// Script builder
        /// </summary>
        public FeedbackMessageScriptBuilder ScriptBuilder => scriptBuilderFactory.Invoke();

        /// <summary>
        /// <see cref="FeedbackMessageStore"/> serializer.
        /// </summary>
        public IFeedbackMessageStoreSerializer StoreSerializer => storeSerializerFactory();

        /// <summary>
        /// Config values
        /// </summary>
        public FeedbackMessageConfig Config => configFactory.Invoke();
    }
}
