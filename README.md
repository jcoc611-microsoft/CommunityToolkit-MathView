# Math View

MathView is a prototype WinUI 3 control that uses [CSharpMath](https://github.com/verybadcat/CSharpMath/) and [Win2D](https://github.com/microsoft/Win2D) to render LaTeX.

## Sample usage
Namespace:
```xml
xmlns:math="using:CommunityToolkit.Labs.WinUI.MathView"
```

Xaml:
```xml
<math:MathView LaTeX="x = -b \pm \frac{\sqrt{b^2-4ac}}{2a}" />
```

There is a sample app in the repo if you want an end-to-end demo.
