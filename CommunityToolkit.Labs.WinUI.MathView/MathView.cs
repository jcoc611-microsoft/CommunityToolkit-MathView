using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace CommunityToolkit.Labs.WinUI.MathView;

/// <summary>
/// Displays a mathematical expression using Win2D.
/// </summary>
public sealed partial class MathView : Panel
{
    private readonly IWin2DPainter _painter;
    private readonly CanvasControl _canvasControl;

    /// <summary>
    /// Identifies the <see cref="LaTeX"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty LaTeXProperty = DependencyProperty.Register(
        nameof(LaTeX), typeof(string), typeof(MathView), new PropertyMetadata(null, LaTeXChanged));

    /// <summary>
    /// Gets or sets the LaTeX string to render.
    /// </summary>
    public string? LaTeX
    {
        get => (string?)GetValue(LaTeXProperty);
        set => SetValue(LaTeXProperty, value);
    }

    public MathView()
    {
        _painter = new MathPainter();

        _canvasControl = new CanvasControl();
        _canvasControl.Draw += canvas_Draw;
        this.Children.Add(_canvasControl);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        bool hasWidth = double.IsFinite(availableSize.Width);
        bool hasHeight = double.IsFinite(availableSize.Height);

        if (hasWidth && hasHeight)
        {
            return availableSize;
        }
        else
        {
            System.Drawing.RectangleF rc = _painter.Measure(hasWidth ? (float)availableSize.Width : 1000.0f);
            return new Size(rc.Width, rc.Height);
        }
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        _canvasControl.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
        return finalSize;
    }

    private void canvas_Draw(CanvasControl control, CanvasDrawEventArgs args)
    {
        CanvasDrawingSession session = args.DrawingSession;
        session.Clear(Windows.UI.Color.FromArgb(0, 255, 255, 255));
        session.Units = CanvasUnits.Pixels;

        _painter.Width = (float)control.ActualWidth;
        _painter.Height = (float)control.ActualHeight;

        System.Drawing.RectangleF rc = _painter.Measure(_painter.Width);
        session.Transform = System.Numerics.Matrix3x2.CreateTranslation(rc.Left, rc.Top);
        _painter.Draw(session, 0.0f, 0.0f);
    }

    private static void LaTeXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((MathView)d)._painter.LaTeX = (string?)e.NewValue;
    }
}
