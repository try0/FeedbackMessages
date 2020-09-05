using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FeedbackMessages.Utils;

namespace FeedbackMessages.Components
{
    /// <summary>
    /// Render feedback messages. 
    /// </summary>
    [ToolboxData("<{0}:FeedbackMessagePanel runat=server></{0}:FeedbackMessagePanel>")]
    public class FeedbackMessagePanel : WebControl
    {

        /// <summary>
        /// Whether to render validation error messages.
        /// </summary>
        [
        Category("Behavior"),
        Themeable(false),
        DefaultValue(true),
        Description(" Whether to render validation error messages.")
        ]
        public bool ShowValidationErrors
        {
            get
            {
                object o = ViewState["ShowValidationErrors"];
                return ((o == null) ? true : (bool)o);
            }
            set
            {
                ViewState["ShowValidationErrors"] = value;
            }
        }

        /// <summary>
        /// Whether to render model state error messages.
        /// </summary>
        [
        Category("Behavior"),
        Themeable(false),
        DefaultValue(true),
        Description("Whether to render model state error messages.")
        ]
        public bool ShowModelStateErrors
        {
            get
            {
                object o = ViewState["ShowModelStateErrors"];
                return ((o == null) ? true : (bool)o);
            }
            set
            {
                ViewState["ShowModelStateErrors"] = value;
            }
        }

        /// <summary>
        /// Validation group name.
        /// </summary>
        [
        Category("Behavior"),
        Themeable(false),
        DefaultValue(""),
        Description("Validation group.")
        ]
        public string ValidationGroup
        {
            get
            {
                string s = (string)ViewState["ValidationGroup"];
                return ((s == null) ? string.Empty : s);
            }
            set
            {
                ViewState["ValidationGroup"] = value;
            }
        }

        /// <summary>
        /// Message renderer
        /// </summary>
        public FeedbackMessageRenderer MessageRenderer { get; set; } = FeedbackMessageSettings.Instance.MessageRenderer;

        /// <summary>
        /// Tag key
        /// </summary>
        protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

        /// <summary>
        /// Renders contents.
        /// </summary>
        /// <param name="output"></param>
        protected override void RenderContents(HtmlTextWriter output)
        {

            AddValidationErrors();

            StringBuilder messagesArea = MessageRenderer.RenderMessages();

            output.Write(messagesArea);

        }

        /// <summary>
        /// Adds validation error messages to <see cref="FeedbackMessageStore"/>. 
        /// </summary>
        private void AddValidationErrors()
        {
            var renderOption = new FeedbackMessageRenderOption()
            {
                ShowModelStateErrors = this.ShowModelStateErrors,
                ShowValidationErrors = this.ShowValidationErrors,
                ValidationGroup = this.ValidationGroup
            };

            FeedbackMessageUtil.AppendValidationErrorsToStore(Page, renderOption);

        }
    }
}
