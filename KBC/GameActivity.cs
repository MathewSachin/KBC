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
        Button fifty50Button;

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

            ShowQuestion();
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
            
            fifty50Button.Enabled = false;
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

        void OptionClick(Button b, int Index)
        {
            optionA.Clickable = optionB.Clickable = optionC.Clickable = optionD.Clickable = false;

            b.Background.SetColorFilter(Color.Gold, PorterDuff.Mode.SrcIn);

            new Thread(() =>
            {
                Thread.Sleep(1000);

                if (Index == correctOption)
                {
                    RunOnUiThread(() => b.Background.SetColorFilter(Color.Green, PorterDuff.Mode.SrcIn));
                    
                    Thread.Sleep(2000);

                    RunOnUiThread(() =>
                    {
                        b.Background.SetColorFilter(Color.Gray, PorterDuff.Mode.SrcIn);

                        cashView.Text = $"Cash: ₹{Question.Amounts[answered]}";

                        ++answered;

                        ShowQuestion();

                        optionA.Clickable = optionB.Clickable = optionC.Clickable = optionD.Clickable = true;
                        optionA.Enabled = optionB.Enabled = optionC.Enabled = optionD.Enabled = true;
                    });
                }
                else RunOnUiThread(() => b.Background.SetColorFilter(Color.Red, PorterDuff.Mode.SrcIn));
            }).Start();
        }
    }
}

