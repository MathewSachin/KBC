using Android.Graphics;
using Android.Widget;
using System;

namespace KBC
{
    static class Extensions
    {
        public static void SetColor(this TextView b, Color c)
        {
            b.Background.SetColorFilter(c, PorterDuff.Mode.SrcIn);
        }

        public static Random Random { get; } = new Random(DateTime.Now.Millisecond);
    }
}