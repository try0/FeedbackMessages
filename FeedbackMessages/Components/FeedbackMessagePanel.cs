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

        public FeedbackMessageRenderer MessageRenderer { get; set; }

        protected override string TagName => "div";

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
