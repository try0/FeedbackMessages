using FeedbackMessages.Utils.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeedbackMessages.Components
{

    public class FeedbackMessageScript : ComponentBase
    {
        private EditContext previousEditContext;
        private readonly EventHandler<ValidationStateChangedEventArgs> validationStateChangedHandler;

        /// <summary>
        /// Whether to render validation error messages.
        /// </summary>
        [Parameter]
        public bool ShowValidationErrors { get; set; } = false;

        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

        [CascadingParameter]
        EditContext CurrentEditContext { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        public FeedbackMessageScriptBuilder ScriptBuilder { get; set; } = FeedbackMessageSettings.Instance.ScriptBuilder;

        public FeedbackMessageScript()
        {
            validationStateChangedHandler = (sender, eventArgs) =>
            {
                if (ShowValidationErrors)
                {
                    if (ShowValidationErrors && CurrentEditContext != null)
                    {
                        FeedbackMessageUtil.AppendValidationErrorsToStore(CurrentEditContext);
                    }
                }
            };
        }

        /// <summary>
        /// Refresh this component. Delegate processing to StateHasChanged().
        /// </summary>
        public void RefreshRender()
        {
            base.StateHasChanged();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (CurrentEditContext != previousEditContext)
            {
                DetachValidationStateChangedListener();
                CurrentEditContext.OnValidationStateChanged += validationStateChangedHandler;
                previousEditContext = CurrentEditContext;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);


            builder.OpenElement(0, "script");

            var attributes = new Dictionary<string, object>();
            attributes["id"] = "fms";
            attributes["data-fmscript"] = ScriptBuilder.GetScripts();

            if (AdditionalAttributes != null)
            {
                foreach (var kv in AdditionalAttributes)
                {
                    attributes.Add(kv.Key, kv.Value);
                }
            }

            builder.AddMultipleAttributes(1, attributes);
            var renderFeedbackMessageFunction =
                @"
                function renderFeedbackMessage() {
                    var script = document.createElement('script');
                    var fms = document.getElementById('fms');
                    script.innerHTML = fms.getAttribute('data-fmscript');
                    document.head.appendChild(script);
                }";

            builder.AddMarkupContent(1, renderFeedbackMessageFunction);
            builder.CloseElement();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);


            JSRuntime.InvokeVoidAsync("renderFeedbackMessage");

            FeedbackMessageStore.Current.CleanRendered();
            FeedbackMessageStore.Flash();
        }

        private void DetachValidationStateChangedListener()
        {
            if (previousEditContext != null)
            {
                previousEditContext.OnValidationStateChanged -= validationStateChangedHandler;
            }
        }

    }
}
