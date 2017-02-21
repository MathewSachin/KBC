using Android.Graphics;
using Android.Widget;
using System;

namespace KBC
{
    static class Extensions
    {
        public static readonly Color OptionDefaultColor = Color.ParseColor("#424242"),
            OptionIndeterminateColor = Color.Gold,
            OptionCorrectColor = Color.ParseColor("#43a047"),
            OptionWrongColor = Color.ParseColor("#e53935");

        public static void SetColor(this TextView b, Color c)
        {
            b.Background.SetColorFilter(c, PorterDuff.Mode.SrcIn);
        }

        public static Random Random { get; } = new Random(DateTime.Now.Millisecond);
    }
}