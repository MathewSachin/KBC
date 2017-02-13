using Android.Graphics;
using Android.Widget;

namespace KBC
{
    static class Extensions
    {
        public static void SetColor(this Button b, Color c)
        {
            b.Background.SetColorFilter(c, PorterDuff.Mode.SrcIn);
        }
    }
}