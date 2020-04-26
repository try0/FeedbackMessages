using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackMessages.Components
{

    public class FeedbackMessagePanel : ComponentBase
    {
        /// <summary>
        /// Message renderer
        /// </summary>
        public FeedbackMessageRenderer MessageRenderer { get; set; } = FeedbackMessageSettings.Instance.MessageRenderer;

        /// <summary>
        /// Constructor.
        /// </summary>
        public FeedbackMessagePanel()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "div");
            builder.AddMarkupContent(0, MessageRenderer.RenderMessages().ToString());
            builder.CloseElement();
        }
    }
}
