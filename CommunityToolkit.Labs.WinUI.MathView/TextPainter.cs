using CSharpMath.Rendering.FrontEnd;
using Microsoft.Graphics.Canvas;

namespace CommunityToolkit.Labs.WinUI.MathView;

public sealed class TextPainter : CSharpMath.Rendering.FrontEnd.TextPainter<CanvasDrawingSession, Windows.UI.Color>, IWin2DPainter
{
    public float Width { get; set; }
    public float Height { get; set; }

    public override Windows.UI.Color UnwrapColor(System.Drawing.Color color) => color.ToWindowsColor();
    public override System.Drawing.Color WrapColor(Windows.UI.Color color) => color.ToSystemColor();

    public override ICanvas WrapCanvas(CanvasDrawingSession session) => new Canvas(session, Width, Height);

    public void Draw(CanvasDrawingSession session, float x, float y) => Draw(session, new System.Drawing.PointF(x, y), Width);
}
