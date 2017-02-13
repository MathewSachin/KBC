using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;

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
                var b = new Button(this)
                {
                    Text = "₹" + Question.Amounts[i]                    
                };

                if (i < answered)
                    b.SetColor(Color.Gold);

                layout.AddView(b);
            }
        }
    }
}

