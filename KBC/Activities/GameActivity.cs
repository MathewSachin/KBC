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
        bool doubleTip;
        
        Button[] options;

        bool fifty50Used, doubleTipUsed, audiencePollUsed, changeQuestionUsed;
        Button fifty50Button, doubleTipButton, audiencePollButton, changeQuestionButton;

        MediaPlayer correctAnswerMediaPlayer;

        List<int> askedEasy, askedMedium;

        readonly Color OptionDefaultColor = Color.Gray,
            OptionIndeterminateColor = Color.Gold,
            OptionCorrectColor = Color.ParseColor("#43a047"),
            OptionWrongColor = Color.ParseColor("#e53935");
        
        void Init()
        {
            askedEasy = new List<int>();
            askedMedium = new List<int>();

            var moneyTreeButton = FindViewById<Button>(Resource.Id.moneyTreeButton);
            moneyTreeButton.Click += ViewMoneyTree;

            questionView = FindViewById<TextView>(Resource.Id.questionView);
            
            options = new[] 
            {
                FindViewById<Button>(Resource.Id.optionA),
                FindViewById<Button>(Resource.Id.optionB),
                FindViewById<Button>(Resource.Id.optionC),
                FindViewById<Button>(Resource.Id.optionD)
            };

            options[0].Click += (s, e) => OptionClick(1);
            options[1].Click += (s, e) => OptionClick(2);
            options[2].Click += (s, e) => OptionClick(3);
            options[3].Click += (s, e) => OptionClick(4);

            foreach (var option in options)
                option.SetColor(OptionDefaultColor);

            cashView = FindViewById<TextView>(Resource.Id.cashView);
            cashView.SetBackgroundColor(Color.Orange);

            fifty50Button = FindViewById<Button>(Resource.Id.fifty50Button);
            fifty50Button.Click += Fifty50;

            doubleTipButton = FindViewById<Button>(Resource.Id.doubleTipButton);
            doubleTipButton.Click += DoubleTip;

            audiencePollButton = FindViewById<Button>(Resource.Id.audiencePollButton);
            audiencePollButton.Click += AudiencePoll;

            changeQuestionButton = FindViewById<Button>(Resource.Id.changeQuestionButton);
            changeQuestionButton.Click += ChangeQuestion;
            
            correctAnswerMediaPlayer = MediaPlayer.Create(this, Resource.Raw.Correct);
            correctAnswerMediaPlayer.SetVolume(0.5f, 0.5f);
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

                askedEasy.AddRange(bundle.GetIntArray(nameof(askedEasy)));
                askedMedium.AddRange(bundle.GetIntArray(nameof(askedMedium)));

                ShowQuestion(currentQuestion);
                
                fifty50Used = bundle.GetBoolean(nameof(fifty50Used));
                doubleTipUsed = bundle.GetBoolean(nameof(doubleTipUsed));
                audiencePollUsed = bundle.GetBoolean(nameof(audiencePollUsed));
                changeQuestionUsed = bundle.GetBoolean(nameof(changeQuestionUsed));

                if (fifty50Used)
                {
                    fifty50Button.Enabled = false;
                    fifty50Button.SetColor(OptionWrongColor);
                }

                if (doubleTipUsed)
                {
                    doubleTipButton.Enabled = false;
                    doubleTipButton.SetColor(OptionWrongColor);
                }

                if (audiencePollUsed)
                {
                    audiencePollButton.Enabled = false;
                    audiencePollButton.SetColor(OptionWrongColor);
                }

                if (changeQuestionUsed)
                {
                    changeQuestionButton.Enabled = false;
                    changeQuestionButton.SetColor(OptionWrongColor);
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

            outState.PutIntArray(nameof(askedEasy), askedEasy.ToArray());
            outState.PutIntArray(nameof(askedMedium), askedMedium.ToArray());

            base.OnSaveInstanceState(outState);
        }

        void ChangeQuestion(object sender, EventArgs e)
        {
            ShowQuestion();

            changeQuestionUsed = true;
            changeQuestionButton.SetColor(OptionWrongColor);

            LifelineState(true);
        }

        void AudiencePoll(object sender, EventArgs e)
        {
            var frag = AudiencePollFragment.Create(correctOption, () => OptionClick(correctOption));
            frag.Show(FragmentManager, "AudiencePoll");

            audiencePollUsed = true;
            audiencePollButton.SetColor(OptionWrongColor);

            LifelineState(false);
        }

        void DoubleTip(object sender, EventArgs e)
        {
            doubleTip = true;

            doubleTipUsed = true;
            doubleTipButton.SetColor(OptionWrongColor);

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
            fifty50Button.SetColor(OptionWrongColor);

            LifelineState(false);
        }

        void ViewMoneyTree(object sender, EventArgs e)
        {
            var frag = MoneyTreeFragment.Create(answered);
            frag.Show(FragmentManager, "MoneyTree" + answered);
        }

        Question[] GetQuestionBank(out List<int> Asked)
        {
            var easy = answered <= Question.SafeLevels[0];

            Asked = easy ? askedEasy : askedMedium;

            return easy ? Questions.Easy : Questions.Medium;
        }

        void ShowQuestion(int Index = -1)
        {
            var bank = GetQuestionBank(out var asked);

            if (Index == -1)
            {
                do Index = Extensions.Random.Next(bank.Length);
                while (asked.Contains(Index));
            }

            UpdateCashView();

            currentQuestion = Index;
            asked.Add(Index);

            var q = bank[Index];

            questionView.Text = $"{answered + 1}. {q.Statement}";

            for (int i = 0; i < 4; ++i)
                options[i].Text = $"{(char)('A' + i)}. {q.Options[i]}";

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

            RunOnUiThread(() => b.SetColor(OptionCorrectColor));

            // Money tree gets updated
            ++answered;

            Thread.Sleep(3000);

            RunOnUiThread(() =>
            {
                foreach (var option in options)
                    option.SetColor(OptionDefaultColor);

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
        
        void OptionClick(int Index)
        {
            var b = options[Index - 1];

            foreach (var option in options)
                option.Clickable = false;
            
            LifelineState(false);

            b.SetColor(OptionIndeterminateColor);

            new Thread(() =>
            {
                Thread.Sleep(1000);

                if (Index == correctOption)
                    AfterCorrectAnswer(b);
                else if (doubleTip)
                {
                    RunOnUiThread(() =>
                    {
                        b.SetColor(OptionWrongColor);
                        
                        foreach (var option in options)
                            option.Clickable = true;

                        b.Clickable = false;
                        doubleTip = false;
                    });
                }
                else
                {
                    RunOnUiThread(() => b.SetColor(OptionWrongColor));

                    Thread.Sleep(500);

                    RunOnUiThread(() => options[correctOption - 1].SetColor(OptionCorrectColor));

                    Thread.Sleep(1000);

                    RunOnUiThread(() => ResultView());
                }
            }).Start();
        }        
    }
}

