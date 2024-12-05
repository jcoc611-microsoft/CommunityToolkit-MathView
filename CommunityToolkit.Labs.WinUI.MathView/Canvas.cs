using CSharpMath.Rendering.FrontEnd;
using Microsoft.Graphics.Canvas;
using System.Collections.Generic;
using System.Numerics;

namespace CommunityToolkit.Labs.WinUI.MathView;

internal sealed class Canvas : ICanvas
{
    internal readonly CanvasDrawingSession _session;
    private readonly Stack<Canvas.Layer> _layers = new();

    public float Width { get; init; }
    public float Height { get; init; }

    public System.Drawing.Color DefaultColor { get; set; }
    public System.Drawing.Color? CurrentColor { get; set; }
    public PaintStyle CurrentStyle { get; set; }

    internal Windows.UI.Color CurrentWindowsColor => (CurrentColor ?? DefaultColor).ToWindowsColor();

    public Canvas(CanvasDrawingSession session, float width, float height)
    {
        _session = session;
        Width = width;
        Height = height;
    }

    public void DrawLine(float x1, float y1, float x2, float y2, float lineThickness)
        => _session.DrawLine(x1, y1, x2, y2, CurrentWindowsColor, lineThickness);
    public void StrokeRect(float left, float top, float width, float height)
        => _session.DrawRectangle(left, top, width, height, CurrentWindowsColor, strokeWidth: 1);
    public void FillRect(float left, float top, float width, float height)
        => _session.FillRectangle(left, top, width, height, CurrentWindowsColor);

    public void Save() => _layers.Push(new Layer(_session.CreateLayer(1.0f), _session.Transform));
    public void Restore()
    {
        Layer layer = _layers.Pop();
        _session.Transform = layer.TransformPre;
        layer.ActiveLayer.Dispose();
    }

    public void Scale(float sx, float sy)
        => _session.Transform = _session.Transform * System.Numerics.Matrix3x2.CreateScale(sx, sy);
    public void Translate(float dx, float dy)
        => _session.Transform = _session.Transform * System.Numerics.Matrix3x2.CreateTranslation(dx, -dy);

    public CSharpMath.Rendering.FrontEnd.Path StartNewPath() => new Path(this);

    readonly struct Layer
    {
        public readonly CanvasActiveLayer ActiveLayer;
        public readonly Matrix3x2 TransformPre;

        public Layer(CanvasActiveLayer activeLayer, Matrix3x2 transformPre)
        {
            ActiveLayer = activeLayer;
            TransformPre = transformPre;
        }
    }
}
