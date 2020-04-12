using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FeedbackMessages.Components
{
    /// <summary>
    /// Render feedback messages. 
    /// </summary>
    [ToolboxData("<{0}:FeedbackMessagePanel runat=server></{0}:FeedbackMessagePanel>")]
    public class FeedbackMessagePanel : WebControl
    {

        /// <summary>
        /// Message renderer
        /// </summary>
        public FeedbackMessageRenderer MessageRenderer { get; set; }

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

            if (MessageRenderer == null)
            {
                MessageRenderer = new FeedbackMessageRenderer();
            }

            StringBuilder messagesArea = MessageRenderer.RenderMessages();

            output.Write(messagesArea);

        }
    }
}
