﻿using Android.App;
using Android.OS;
using Android.Views;
using Android.Content;
using Android.Widget;
using System;
using Android.Graphics;
using System.Threading;
using System.Collections.Generic;
using Android.Media;

namespace KBC
{
    [Activity(Label = "KBC", Icon = "@drawable/icon")]
    public class GameActivity : Activity
    {
        Random r;
        int answered, currentQuestion, correctOption;
        TextView questionView, cashView;
        Button optionA, optionB, optionC, optionD;
        bool doubleTip;

        Button[] options;

        bool fifty50Used, doubleTipUsed, audiencePollUsed;
        Button fifty50Button, doubleTipButton, audiencePollButton;

        MediaPlayer questionAskedMediaPlayer, correctAnswerMediaPlayer;

        void Init()
        {
            r = new Random(DateTime.Now.Millisecond);

            var moneyTreeButton = FindViewById<Button>(Resource.Id.moneyTreeButton);
            moneyTreeButton.Click += ViewMoneyTree;

            questionView = FindViewById<TextView>(Resource.Id.questionView);
            cashView = FindViewById<TextView>(Resource.Id.cashView);

            optionA = FindViewById<Button>(Resource.Id.optionA);
            optionB = FindViewById<Button>(Resource.Id.optionB);
            optionC = FindViewById<Button>(Resource.Id.optionC);
            optionD = FindViewById<Button>(Resource.Id.optionD);

            options = new[] { optionA, optionB, optionC, optionD };

            optionA.Click += (s, e) => OptionClick(optionA, 1);
            optionB.Click += (s, e) => OptionClick(optionB, 2);
            optionC.Click += (s, e) => OptionClick(optionC, 3);
            optionD.Click += (s, e) => OptionClick(optionD, 4);

            optionA.SetColor(Color.Gray);
            optionB.SetColor(Color.Gray);
            optionC.SetColor(Color.Gray);
            optionD.SetColor(Color.Gray);

            fifty50Button = FindViewById<Button>(Resource.Id.fifty50Button);
            fifty50Button.Click += Fifty50;

            doubleTipButton = FindViewById<Button>(Resource.Id.doubleTipButton);
            doubleTipButton.Click += DoubleTip;

            audiencePollButton = FindViewById<Button>(Resource.Id.audiencePollButton);
            audiencePollButton.Click += AudiencePoll;

            questionAskedMediaPlayer = MediaPlayer.Create(this, Resource.Raw.Question);
            correctAnswerMediaPlayer = MediaPlayer.Create(this, Resource.Raw.Correct);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Game);

            Init();

            if (bundle == null)
                ShowQuestion();
            else
            {
                currentQuestion = bundle.GetInt(nameof(currentQuestion));
                answered = bundle.GetInt(nameof(answered));

                ShowQuestion(currentQuestion);

                UpdateCashView();

                fifty50Used = bundle.GetBoolean(nameof(fifty50Used));
                doubleTipUsed = bundle.GetBoolean(nameof(doubleTipUsed));
                audiencePollUsed = bundle.GetBoolean(nameof(audiencePollUsed));

                if (fifty50Used)
                {
                    fifty50Button.Enabled = false;
                    fifty50Button.SetColor(Color.Red);
                }

                if (doubleTipUsed)
                {
                    doubleTipButton.Enabled = false;
                    doubleTipButton.SetColor(Color.Red);
                }

                if (audiencePollUsed)
                {
                    audiencePollButton.Enabled = false;
                    audiencePollButton.SetColor(Color.Red);
                }
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(nameof(currentQuestion), currentQuestion);
            outState.PutInt(nameof(answered), answered);

            outState.PutBoolean(nameof(fifty50Used), fifty50Used);
            outState.PutBoolean(nameof(doubleTipUsed), doubleTipUsed);
            outState.PutBoolean(nameof(audiencePollUsed), audiencePollUsed);
            
            base.OnSaveInstanceState(outState);
        }

        void AudiencePoll(object sender, EventArgs e)
        {
            audiencePollUsed = true;
            audiencePollButton.SetColor(Color.Red);

            Toast.MakeText(this, options[correctOption - 1].Text, ToastLength.Short).Show();

            LifelineState(false);
        }

        void DoubleTip(object sender, EventArgs e)
        {
            doubleTip = true;

            doubleTipUsed = true;
            doubleTipButton.SetColor(Color.Red);

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
            fifty50Button.SetColor(Color.Red);

            LifelineState(false);
        }

        void ViewMoneyTree(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(MoneyTreeActivity));
            i.PutExtra("Answered", answered);
            StartActivity(i);
        }

        void ShowQuestion(int Index = -1)
        {
            if (answered == 0)
                questionAskedMediaPlayer.Start();

            if (Index == -1)
                Index = r.Next(Question.Questions.Length);

            currentQuestion = Index;

            var q = Question.Questions[Index];

            questionView.Text = q.Statement;

            optionA.Text = "A. " + q.OptionA;
            optionB.Text = "B. " + q.OptionB;
            optionC.Text = "C. " + q.OptionC;
            optionD.Text = "D. " + q.OptionD;

            correctOption = q.CorrectOption;
        }

        void UpdateCashView()
        {
            cashView.Text = $"Cash: ₹{Question.Amounts[answered - 1]}";
        }

        void ResultView()
        {
            var i = new Intent(this, typeof(ResultActivity));
            i.PutExtra("Answered", answered);
            StartActivity(i);

            Finish();
        }

        void AfterCorrectAnswer(Button b)
        {
            correctAnswerMediaPlayer.Start();

            RunOnUiThread(() => b.SetColor(Color.Green));

            // Money tree gets updated
            ++answered;

            Thread.Sleep(3000);

            RunOnUiThread(() =>
            {
                ResetColor(optionA, optionB, optionC, optionD);

                UpdateCashView();
                
                if (answered == Question.Amounts.Length)
                    ResultView();
                else
                {
                    ShowQuestion();

                    OptionsState(true);
                    optionA.Enabled = optionB.Enabled = optionC.Enabled = optionD.Enabled = true;
                    doubleTip = false;

                    LifelineState(true);
                }
            });
        }

        void ResetColor(params Button[] Buttons)
        {
            foreach (var b in Buttons)
                b.SetColor(Color.Gray);
        }
        
        void OptionsState(bool Clickable)
        {
            optionA.Clickable = optionB.Clickable = optionC.Clickable = optionD.Clickable = Clickable;
        }

        void OptionClick(Button b, int Index)
        {
            OptionsState(false);
            LifelineState(false);

            b.SetColor(Color.Gold);

            new Thread(() =>
            {
                Thread.Sleep(1000);

                if (Index == correctOption)
                    AfterCorrectAnswer(b);
                else if (doubleTip)
                {
                    RunOnUiThread(() =>
                    {
                        b.SetColor(Color.Red);

                        OptionsState(true);

                        b.Clickable = false;
                        doubleTip = false;
                    });
                }
                else
                {
                    RunOnUiThread(() => b.SetColor(Color.Red));

                    Thread.Sleep(1000);

                    RunOnUiThread(ResultView);
                }
            }).Start();
        }        
    }
}
