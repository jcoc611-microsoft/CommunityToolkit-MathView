namespace CommunityToolkit.Labs.WinUI.MathView;

internal static class Extensions
{
    internal static Windows.UI.Color ToWindowsColor(this System.Drawing.Color color) => Windows.UI.Color.FromArgb(color.A, color.R, color.G, color.B);
    internal static System.Drawing.Color ToSystemColor(this Windows.UI.Color color) => System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
}
