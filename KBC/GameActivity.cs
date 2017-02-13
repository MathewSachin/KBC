using Android.App;
using Android.OS;
using Android.Views;
using Android.Content;
using Android.Widget;
using System;
using Android.Graphics;
using System.Threading;
using System.Collections.Generic;

namespace KBC
{
    [Activity(Label = "KBC", Icon = "@drawable/icon")]
    public class GameActivity : Activity
    {
        Random r;
        int answered, correctOption;
        TextView questionView, cashView;
        Button optionA, optionB, optionC, optionD;
        bool doubleTip;

        bool fifty50Used, doubleTipUsed, audiencePollUsed;
        Button fifty50Button, doubleTipButton, audiencePollButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Game);

            r = new Random(DateTime.Now.Millisecond);

            var moneyTreeButton = FindViewById<Button>(Resource.Id.moneyTreeButton);
            moneyTreeButton.Click += ViewMoneyTree;

            questionView = FindViewById<TextView>(Resource.Id.questionView);
            cashView = FindViewById<TextView>(Resource.Id.cashView);

            optionA = FindViewById<Button>(Resource.Id.optionA);
            optionB = FindViewById<Button>(Resource.Id.optionB);
            optionC = FindViewById<Button>(Resource.Id.optionC);
            optionD = FindViewById<Button>(Resource.Id.optionD);

            optionA.Click += (s, e) => OptionClick(optionA, 1);
            optionB.Click += (s, e) => OptionClick(optionB, 2);
            optionC.Click += (s, e) => OptionClick(optionC, 3);
            optionD.Click += (s, e) => OptionClick(optionD, 4);

            optionA.Background.SetColorFilter(Color.Gray, PorterDuff.Mode.SrcIn);
            optionB.Background.SetColorFilter(Color.Gray, PorterDuff.Mode.SrcIn);
            optionC.Background.SetColorFilter(Color.Gray, PorterDuff.Mode.SrcIn);
            optionD.Background.SetColorFilter(Color.Gray, PorterDuff.Mode.SrcIn);

            fifty50Button = FindViewById<Button>(Resource.Id.fifty50Button);
            fifty50Button.Click += Fifty50;

            doubleTipButton = FindViewById<Button>(Resource.Id.doubleTipButton);
            doubleTipButton.Click += DoubleTip;

            audiencePollButton = FindViewById<Button>(Resource.Id.audiencePollButton);

            ShowQuestion();
        }

        void DoubleTip(object sender, EventArgs e)
        {
            doubleTip = true;

            doubleTipUsed = true;
            doubleTipButton.SetTextColor(Color.Red);

            LifelineState(false);
        }

        void LifelineState(bool Enabled)
        {
            if (!Enabled)
                fifty50Button.Enabled = doubleTipButton.Enabled = audiencePollButton.Enabled = false;
            else
            {
                if (!fifty50Used)
                    fifty50Button.Enabled = true;

                if (!doubleTipUsed)
                    doubleTipButton.Enabled = true;

                if (!audiencePollUsed)
                    audiencePollButton.Enabled = true;
            }
        }

        void Fifty50(object sender, EventArgs e)
        {
            var list = new List<Button>();

            if (correctOption != 1)
                list.Add(optionA);
            if (correctOption != 2)
                list.Add(optionB);
            if (correctOption != 3)
                list.Add(optionC);
            if (correctOption != 4)
                list.Add(optionD);

            list.RemoveAt(r.Next(3));

            foreach (var b in list)
                b.Enabled = false;
            
            fifty50Used = true;
            fifty50Button.SetTextColor(Color.Red);

            LifelineState(false);
        }

        void ViewMoneyTree(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(MoneyTreeActivity));
            i.PutExtra("Answered", answered);
            StartActivity(i);
        }

        void ShowQuestion()
        {
            var q = Question.Questions[r.Next(Question.Questions.Length)];

            questionView.Text = q.Statement;

            optionA.Text = q.OptionA;
            optionB.Text = q.OptionB;
            optionC.Text = q.OptionC;
            optionD.Text = q.OptionD;

            correctOption = q.CorrectOption;
        }

        void AfterCorrectAnswer(Button b)
        {
            RunOnUiThread(() => b.Background.SetColorFilter(Color.Green, PorterDuff.Mode.SrcIn));

            Thread.Sleep(2000);

            RunOnUiThread(() =>
            {
                ResetColor(optionA);
                ResetColor(optionB);
                ResetColor(optionC);
                ResetColor(optionD);

                cashView.Text = $"Cash: ₹{Question.Amounts[answered]}";

                ++answered;

                ShowQuestion();

                OptionsState(true);
                optionA.Enabled = optionB.Enabled = optionC.Enabled = optionD.Enabled = true;
                doubleTip = false;
                
                LifelineState(true);
            });
        }

        void ResetColor(Button b)
        {
            b.Background.SetColorFilter(Color.Gray, PorterDuff.Mode.SrcIn);
        }
        
        void OptionsState(bool Clickable)
        {
            optionA.Clickable = optionB.Clickable = optionC.Clickable = optionD.Clickable = Clickable;
        }

        void OptionClick(Button b, int Index)
        {
            OptionsState(false);

            b.Background.SetColorFilter(Color.Gold, PorterDuff.Mode.SrcIn);

            new Thread(() =>
            {
                Thread.Sleep(1000);

                if (Index == correctOption)
                    AfterCorrectAnswer(b);
                else if (doubleTip)
                {
                    RunOnUiThread(() =>
                    {
                        b.Background.SetColorFilter(Color.Red, PorterDuff.Mode.SrcIn);

                        OptionsState(true);

                        b.Clickable = false;
                        doubleTip = false;
                    });
                }
                else RunOnUiThread(() => b.Background.SetColorFilter(Color.Red, PorterDuff.Mode.SrcIn));
            }).Start();
        }
    }
}

