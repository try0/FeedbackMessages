using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackMessages.Components
{

    public class FeedbackMessageScript : ComponentBase
    {

        public FeedbackMessageScriptBuilder ScriptBuilder { get; set; } = FeedbackMessageSettings.Instance.ScriptBuilder;

        public FeedbackMessageScript()
        {
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenElement(0, "script");
            builder.AddMarkupContent(0, ScriptBuilder.GetDomReadyScript());
            builder.CloseElement();
        }
    }
}
