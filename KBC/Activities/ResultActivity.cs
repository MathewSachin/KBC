using Android.App;
using Android.OS;
using Android.Widget;

namespace KBC
{
    [Activity(Label = "KBC", Icon = "@drawable/icon")]
    public class ResultActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Result);

            var answered = Intent.Extras.GetInt("Answered");

            var earned = answered == Question.Amounts.Length ? Question.Amounts[answered - 1] : "0";
            var message = answered == Question.Amounts.Length ? "Congratulation" : "Better Luck Next Time";

            var msgView = FindViewById<TextView>(Resource.Id.resultMessageView);
            msgView.Text = message;

            var cashView = FindViewById<TextView>(Resource.Id.resultCashView);
            cashView.Text = "₹" + earned;
        }
    }
}

