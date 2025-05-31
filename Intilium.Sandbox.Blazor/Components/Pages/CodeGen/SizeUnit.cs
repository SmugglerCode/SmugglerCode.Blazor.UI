namespace Intilium.Sandbox.Blazor.Components.Pages.CodeGen;

public enum SizeUnit
{
    Px = 0, // default on screens
    Em = 1, // relative to the parents
    Rem = 2, // relative to the root (body)
    Fr = 3, // fractional, css grid
    Percent = 4, // relative to the parent
    Pt = 5, // point 1/72 inch
    Mm = 6, // millimeter, rarely used
    Cm = 7, // centimeter, rarely used
    In = 8, // inch, rarely used
    Vw = 9, // vw viewport width
    Vh = 10, // vh viewport height
    Vmin = 11, // smallest of vw/vh
    Vmax = 12, // biggest of vw
    Auto = 13 // automatic size
}