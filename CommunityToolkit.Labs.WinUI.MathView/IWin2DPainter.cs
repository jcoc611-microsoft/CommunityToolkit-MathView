using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Drawing;

namespace CommunityToolkit.Labs.WinUI.MathView;

internal interface IWin2DPainter
{
    float Width { get; set;  }
    float Height { get; set; }
    string? LaTeX { get; set; }

    void Draw(CanvasDrawingSession canvas, float x, float y);
    RectangleF Measure(float textPainterCanvasWidth);
}
