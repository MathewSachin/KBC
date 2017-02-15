using Android.App;
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
        int answered, currentQuestion, correctOption;
        TextView questionView, cashView;
        Button optionA, optionB, optionC, optionD;
        bool doubleTip;
        
        Button[] options;

        bool fifty50Used, doubleTipUsed, audiencePollUsed, changeQuestionUsed;
        Button fifty50Button, doubleTipButton, audiencePollButton, changeQuestionButton;

        MediaPlayer questionAskedMediaPlayer, correctAnswerMediaPlayer;

        List<int> asked;

        void Init()
        {
            asked = new List<int>();

            var moneyTreeButton = FindViewById<Button>(Resource.Id.moneyTreeButton);
            moneyTreeButton.Click += ViewMoneyTree;

            questionView = FindViewById<TextView>(Resource.Id.questionView);
            cashView = FindViewById<TextView>(Resource.Id.cashView);

            cashView.SetBackgroundColor(Color.Orange);
            
            optionA = FindViewById<Button>(Resource.Id.optionA);
            optionB = FindViewById<Button>(Resource.Id.optionB);
            optionC = FindViewById<Button>(Resource.Id.optionC);
            optionD = FindViewById<Button>(Resource.Id.optionD);

            options = new[] { optionA, optionB, optionC, optionD };

            for (int i = 0; i < 4; ++i)
            {
                options[i].Click += (s, e) => OptionClick(options[i], i + 1);
                options[i].SetColor(Color.Gray);
            }

            fifty50Button = FindViewById<Button>(Resource.Id.fifty50Button);
            fifty50Button.Click += Fifty50;

            doubleTipButton = FindViewById<Button>(Resource.Id.doubleTipButton);
            doubleTipButton.Click += DoubleTip;

            audiencePollButton = FindViewById<Button>(Resource.Id.audiencePollButton);
            audiencePollButton.Click += AudiencePoll;

            changeQuestionButton = FindViewById<Button>(Resource.Id.changeQuestionButton);
            changeQuestionButton.Click += ChangeQuestion;

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

                asked.AddRange(bundle.GetIntArray(nameof(asked)));

                ShowQuestion(currentQuestion);
                
                fifty50Used = bundle.GetBoolean(nameof(fifty50Used));
                doubleTipUsed = bundle.GetBoolean(nameof(doubleTipUsed));
                audiencePollUsed = bundle.GetBoolean(nameof(audiencePollUsed));
                changeQuestionUsed = bundle.GetBoolean(nameof(changeQuestionUsed));

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

                if (changeQuestionUsed)
                {
                    changeQuestionButton.Enabled = false;
                    changeQuestionButton.SetColor(Color.Red);
                }
            }
        }

        public override void OnBackPressed()
        {
            var builder = new AlertDialog.Builder(this);

            builder.SetTitle("Quit")
                .SetMessage("Are you sure?")
                .SetPositiveButton("Yes", (s, e) => ResultView(true))
                .SetNegativeButton("No", (s, e) => { })
                .Create()
                .Show();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(nameof(currentQuestion), currentQuestion);
            outState.PutInt(nameof(answered), answered);

            outState.PutBoolean(nameof(fifty50Used), fifty50Used);
            outState.PutBoolean(nameof(doubleTipUsed), doubleTipUsed);
            outState.PutBoolean(nameof(audiencePollUsed), audiencePollUsed);
            outState.PutBoolean(nameof(changeQuestionUsed), changeQuestionUsed);

            outState.PutIntArray(nameof(asked), asked.ToArray());
            
            base.OnSaveInstanceState(outState);
        }

        void ChangeQuestion(object sender, EventArgs e)
        {
            ShowQuestion();

            changeQuestionUsed = true;
            changeQuestionButton.SetColor(Color.Red);

            LifelineState(true);
        }

        void AudiencePoll(object sender, EventArgs e)
        {
            var frag = AudiencePollFragment.Create(correctOption, () => OptionClick(options[correctOption - 1], correctOption));
            frag.Show(FragmentManager, "AudiencePoll");

            audiencePollUsed = true;
            audiencePollButton.SetColor(Color.Red);

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
                fifty50Button.Enabled = doubleTipButton.Enabled = audiencePollButton.Enabled = changeQuestionButton.Enabled = false;
            else
            {
                fifty50Button.Enabled = !fifty50Used;

                doubleTipButton.Enabled = !doubleTipUsed;

                audiencePollButton.Enabled = !audiencePollUsed;
                
                changeQuestionButton.Enabled = !changeQuestionUsed;
            }
        }

        void Fifty50(object sender, EventArgs e)
        {
            var list = new List<Button>();

            for (int i = 1; i <= 4; ++i)
                if (correctOption != i)
                    list.Add(options[i - 1]);
            
            list.RemoveAt(Extensions.Random.Next(3));

            foreach (var b in list)
            {
                b.Clickable = false;
                b.Text = "";
            }
            
            fifty50Used = true;
            fifty50Button.SetColor(Color.Red);

            LifelineState(false);
        }

        void ViewMoneyTree(object sender, EventArgs e)
        {
            var frag = MoneyTreeFragment.Create(answered);
            frag.Show(FragmentManager, "MoneyTree" + answered);
        }

        void ShowQuestion(int Index = -1)
        {
            if (Index == -1)
            {
                do Index = Extensions.Random.Next(Question.Questions.Length);
                while (asked.Contains(Index));

                if (answered == 0)
                    questionAskedMediaPlayer.Start();
            }

            UpdateCashView();

            currentQuestion = Index;
            asked.Add(Index);

            var q = Question.Questions[Index];

            questionView.Text = $"{answered + 1}. {q.Statement}";

            optionA.Text = "A. " + q.OptionA;
            optionB.Text = "B. " + q.OptionB;
            optionC.Text = "C. " + q.OptionC;
            optionD.Text = "D. " + q.OptionD;

            correctOption = q.CorrectOption;
        }

        void UpdateCashView()
        {
            cashView.Text = $"₹{Question.Amounts[answered]}";
        }

        void ResultView(bool Quit = false)
        {
            var i = new Intent(this, typeof(ResultActivity));
            i.PutExtra("Answered", answered);
            i.PutExtra(nameof(Quit), Quit);

            i.PutExtra(nameof(fifty50Used), fifty50Used);
            i.PutExtra(nameof(doubleTipUsed), doubleTipUsed);
            i.PutExtra(nameof(audiencePollUsed), audiencePollUsed);
            i.PutExtra(nameof(changeQuestionUsed), changeQuestionUsed);

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
                                
                if (answered == Question.Amounts.Length)
                    ResultView();
                else
                {
                    ShowQuestion();

                    foreach (var option in options)
                    {
                        option.Enabled = true;
                        option.Clickable = true;
                    }

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
        
        void OptionClick(Button b, int Index)
        {
            foreach (var option in options)
                option.Clickable = false;
            
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
                        
                        foreach (var option in options)
                            option.Clickable = true;

                        b.Clickable = false;
                        doubleTip = false;
                    });
                }
                else
                {
                    RunOnUiThread(() => b.SetColor(Color.Red));

                    Thread.Sleep(500);

                    RunOnUiThread(() => options[correctOption - 1].SetColor(Color.Green));

                    Thread.Sleep(1000);

                    RunOnUiThread(() => ResultView());
                }
            }).Start();
        }        
    }
}

