using Android.App;
using Android.OS;
using Android.Widget;

namespace KBC
{
    [Activity(Label = "Help | KBC")]
    public class HelpActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView (Resource.Layout.Help);

            var introText = FindViewById<TextView>(Resource.Id.introText);

            introText.Text = @"
There are 15 questions from ₹ 1,000 to ₹ 7 Crore. There are 4 options for every question out of which only one is correct.


The game ends when you give a wrong answer and you get the amount secured in the last safe level. Safe levels are at ₹ 40,000, ₹ 6,40,000 and ₹ 7 Crore. You will lose all your money if you give a wrong answer before reaching a safe level.


You can Quit any time by pressing the back button if you are not sure of the answer to avoid giving a wrong answer and losing your money.


There is a 30 second timer till the first safe level and 60 second till the second. After the second safe level, there is no time limit. You lose if you run out of time.
";

            var audiencePollText = FindViewById<TextView>(Resource.Id.audiencePollText);
            audiencePollText.Text = @"1. Audience Poll

This will simulate a poll amongst the audience attending KBC and will show you the results of the poll in the form of a bar graph.
";

            var fifty50Text = FindViewById<TextView>(Resource.Id.fifty50Text);
            fifty50Text.Text = @"2. Fifty Fifty

This lifeline will remove 2 wrong choices and you have to pick the right answer from the remaining 2 choices.
";

            var doubleDipText = FindViewById<TextView>(Resource.Id.doubleDipText);
            doubleDipText.Text = @"3. Double Dip

This lifeline allows you to answer two options for a question, winning if either of those is correct.
";

            var changeQuestionText = FindViewById<TextView>(Resource.Id.changeQuestionText);
            changeQuestionText.Text = @"4. Flip the Question

This will skip the current questions and show you a new question without increasing your money.
";
        }
    }
}

