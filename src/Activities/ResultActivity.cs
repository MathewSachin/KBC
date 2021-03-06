﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

namespace KBC
{
    [Activity(Label = "KBC")]
    public class ResultActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Result);

            var answered = Intent.Extras.GetInt("Answered");
            var resultType = (ResultType)Intent.Extras.GetInt(nameof(ResultType));

            var lifelinesUsed = 0;

            var fifty50Used = Intent.Extras.GetBoolean("fifty50Used");
            if (fifty50Used)
                ++lifelinesUsed;

            var doubleDipUsed = Intent.Extras.GetBoolean("doubleDipUsed");
            if (doubleDipUsed)
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

                if (doubleDipUsed)
                    lifelinesView.Text += "Double Dip\n";

                if (audiencePollUsed)
                    lifelinesView.Text += "Audience Poll\n";

                if (changeQuestionUsed)
                    lifelinesView.Text += "Change Question\n";
            }
            
            var msgView = FindViewById<TextView>(Resource.Id.resultMessageView);
            msgView.Text = GetMessage(answered, resultType);

            var cashView = FindViewById<TextView>(Resource.Id.resultCashView);
            cashView.Text = "₹" + GetAmount(answered, resultType == ResultType.Quit);

            var playAgainButton = FindViewById<Button>(Resource.Id.playAgainButton);
            playAgainButton.SetColor(Extensions.OptionDefaultColor);
            playAgainButton.Click += PlayAgain;
        }

        void PlayAgain(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(GameActivity));
            StartActivity(i);

            Finish();
        }

        string GetMessage(int Answered, ResultType ResultType)
        {
            switch (ResultType)
            {
                case ResultType.Quit:
                    return "You Quit";

                case ResultType.TimeOut:
                    return "Timeout";

                case ResultType.Win:
                    return "Congratulations";

                default:
                case ResultType.WrongAnswer:
                    return "Wrong Answer";
            }
        }

        string GetAmount(int Answered, bool Quit)
        {
            var level = Answered - 1;

            if (Quit)
                return level == -1 ? "0" : Question.Amounts[level];
            
            for (int i = Question.SafeLevels.Length - 1; i >= 0; --i)
                if (level >= Question.SafeLevels[i])
                    return Question.Amounts[Question.SafeLevels[i]];
                        
            return "0";
        }
    }
}

