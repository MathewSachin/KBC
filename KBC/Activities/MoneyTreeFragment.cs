using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Linq;

namespace KBC
{
    public class MoneyTreeFragment : DialogFragment
    {
        public static MoneyTreeFragment Create(int Answered)
        {
            var f = new MoneyTreeFragment();

            var args = new Bundle();
            args.PutInt(nameof(Answered), Answered);
            f.Arguments = args;
            
            return f;
        }

        public override Dialog OnCreateDialog(Bundle bundle)
        {
            var builder = new AlertDialog.Builder(Activity);

            var view = Activity.LayoutInflater.Inflate(Resource.Layout.MoneyTree, null);

            builder.SetTitle("Money Tree")
                .SetPositiveButton("Close", (s, e) => { })
                .SetView(view);

            var answered = Arguments.GetInt("Answered");

            var layout = view.FindViewById<LinearLayout>(Resource.Id.moneyTree);

            for (int i = Question.Amounts.Length - 1; i >= 0; --i)
            {
                var b = new TextView(view.Context)
                {
                    Text = $"{(i + 1).ToString().PadLeft(2, ' ')}.\t\t ₹{Question.Amounts[i]}",
                    TextAlignment = TextAlignment.ViewEnd,
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent),
                    TextSize = 20
                };

                if (i == answered - 1)
                    b.SetBackgroundColor(Color.Orange);

                if (Question.SafeLevels.Contains(i))
                    b.SetTextColor(Color.Yellow);

                layout.AddView(b);
            }

            return builder.Create();
        }
    }
}

