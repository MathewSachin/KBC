using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

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
            
            var message = answered >= 4 ? "Congratulation" : "Better Luck Next Time";

            var msgView = FindViewById<TextView>(Resource.Id.resultMessageView);
            msgView.Text = message;

            var cashView = FindViewById<TextView>(Resource.Id.resultCashView);
            cashView.Text = "₹" + GetAmount(answered);

            var playAgainButton = FindViewById<Button>(Resource.Id.playAgainButton);
            playAgainButton.Click += PlayAgain;
        }

        void PlayAgain(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(GameActivity));
            StartActivity(i);

            Finish();
        }

        string GetAmount(int Answered)
        {
            int level = Answered - 1;

            for (int i = Question.SafeLevels.Length - 1; i >= 0; --i)
                if (level >= Question.SafeLevels[i])
                    return Question.Amounts[Question.SafeLevels[i]];
                        
            return "0";
        }
    }
}

