using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;

namespace KBC
{
    [Activity(Label = "Money Tree | KBC", Icon = "@drawable/icon")]
    public class MoneyTreeActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.MoneyTree);
            
            var answered = Intent.Extras.GetInt("Answered");

            var layout = FindViewById<LinearLayout>(Resource.Id.moneyTree);

            for (int i = Question.Amounts.Length - 1; i >= 0; --i)
            {
                var b = new TextView(this)
                {
                    Text = $"{(i + 1).ToString().PadLeft(2, ' ')}.\t\t ₹{Question.Amounts[i]}",
                    TextAlignment = TextAlignment.ViewEnd,
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent),
                    TextSize = 20
                };

                if (i == answered - 1)
                    b.SetBackgroundColor(Color.Orange);

                if (Question.SafeLevels.Contains(i))
                    b.SetTextColor(Color.Yellow);

                layout.AddView(b);
            }
        }
    }
}

