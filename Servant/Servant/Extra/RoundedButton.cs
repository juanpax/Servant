using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Servant.Extra
{
    public class RoundedButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
