using System.Drawing;
using System.Numerics;
using Microsoft.Graphics.Canvas.Geometry;

namespace CommunityToolkit.Labs.WinUI.MathView;

/// <summary>
/// Win2D implementation of a path. Gets drawn to the parent Canvas on dispose.
/// </summary>
internal sealed class Path : CSharpMath.Rendering.FrontEnd.Path
{
    private readonly Canvas _canvas;
    private readonly CanvasPathBuilder _pathBuilder;
    private bool _isOpen = false;

    public override Color? Foreground { get; set; }

    public Path(Canvas canvas)
    {
        _canvas = canvas;
        _pathBuilder = new CanvasPathBuilder(canvas._session.Device);
    }

    public override void Curve3(float x1, float y1, float x2, float y2)
    {
        if (!_isOpen) OpenContour(x1, y1);
        _pathBuilder.AddQuadraticBezier(new Vector2(x1, y1), new Vector2(x2, y2));
    }

    public override void Curve4(float x1, float y1, float x2, float y2, float x3, float y3)
    {
        if (!_isOpen) OpenContour(x1, y1);
        _pathBuilder.AddCubicBezier(new Vector2(x1, y1), new Vector2(x2, y2), new Vector2(x3, y3));
    }

    public override void LineTo(float x1, float y1)
    {
        if (!_isOpen) OpenContour(x1, y1);
        _pathBuilder.AddLine(new Vector2(x1, y1));
    }

    public override void MoveTo(float x0, float y0)
    {
        if (_isOpen) CloseContour();
        OpenContour(x0, y0);
    }
    public override void CloseContour()
    {
        if (!_isOpen) return;
        _isOpen = false;
        _pathBuilder.EndFigure(CanvasFigureLoop.Closed);
    }

    private void OpenContour(float x1, float y1)
    {
        _isOpen = true;
        _pathBuilder.BeginFigure(new Vector2(x1, y1));
    }

    public override void Dispose()
    {
        CanvasGeometry geometry = CanvasGeometry.CreatePath(_pathBuilder);
        _canvas._session.FillGeometry(geometry,
            Foreground?.ToWindowsColor() ?? _canvas.CurrentWindowsColor);
        _pathBuilder.Dispose();
    }
}
