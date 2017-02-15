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

            var lifelinesUsed = 0;

            var fifty50Used = Intent.Extras.GetBoolean("fifty50Used");
            if (fifty50Used)
                ++lifelinesUsed;

            var doubleTipUsed = Intent.Extras.GetBoolean("doubleTipUsed");
            if (doubleTipUsed)
                ++lifelinesUsed;

            var audiencePollUsed = Intent.Extras.GetBoolean("audiencePollUsed");
            if (audiencePollUsed)
                ++lifelinesUsed;

            var changeQuestionUsed = Intent.Extras.GetBoolean("changeQuestionUsed");
            if (changeQuestionUsed)
                ++lifelinesUsed;

            if (lifelinesUsed > 0)
            {
                var lifelinesView = FindViewById<TextView>(Resource.Id.lifelinesUsedView);

                lifelinesView.Text = "Lifelines Used:\n\n";

                if (fifty50Used)
                    lifelinesView.Text += "50 50\n";

                if (doubleTipUsed)
                    lifelinesView.Text += "Double Tip\n";

                if (audiencePollUsed)
                    lifelinesView.Text += "Audience Poll\n";

                if (changeQuestionUsed)
                    lifelinesView.Text += "Change Question\n";
            }
            
            var message = answered > Question.SafeLevels[0] ? "Congratulation" : "Better Luck Next Time";

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

