using CSharpMath.Rendering.FrontEnd;
using Microsoft.Graphics.Canvas;

namespace CommunityToolkit.Labs.WinUI.MathView;

public sealed class MathPainter : CSharpMath.Rendering.FrontEnd.MathPainter<CanvasDrawingSession, Windows.UI.Color>, IWin2DPainter
{
    public float Width { get; set; }
    public float Height { get; set; }

    public override Windows.UI.Color UnwrapColor(System.Drawing.Color color) => color.ToWindowsColor();
    public override System.Drawing.Color WrapColor(Windows.UI.Color color) => color.ToSystemColor();

    public override ICanvas WrapCanvas(CanvasDrawingSession session) => new Canvas(session, Width, Height);
}
