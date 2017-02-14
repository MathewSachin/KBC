using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace KBC
{
    public class AudiencePollFragment : DialogFragment
    {
        event Action GoingWithAudience;

        public static AudiencePollFragment Create(int CorrectOption, Action GoWithAudience)
        {
            var f = new AudiencePollFragment();
            f.GoingWithAudience += GoWithAudience;

            var args = new Bundle();
            args.PutInt(nameof(CorrectOption), CorrectOption);
            f.Arguments = args;
            
            return f;
        }

        public override Dialog OnCreateDialog(Bundle bundle)
        {
            var builder = new AlertDialog.Builder(Activity);
            
            var correctOption = Arguments.GetInt("CorrectOption") - 1;

            var view = Activity.LayoutInflater.Inflate(Resource.Layout.AudiencePoll, null);
            
            builder.SetTitle("Audience Poll")
                .SetPositiveButton("Go with Audience", (s, e) => GoingWithAudience?.Invoke())
                .SetView(view);

            var textViews = new TextView[4];
            textViews[0] = view.FindViewById<TextView>(Resource.Id.optionACent);
            textViews[1] = view.FindViewById<TextView>(Resource.Id.optionBCent);
            textViews[2] = view.FindViewById<TextView>(Resource.Id.optionCCent);
            textViews[3] = view.FindViewById<TextView>(Resource.Id.optionDCent);

            var progressBars = new ProgressBar[4];
            progressBars[0] = view.FindViewById<ProgressBar>(Resource.Id.optionABar);
            progressBars[1] = view.FindViewById<ProgressBar>(Resource.Id.optionBBar);
            progressBars[2] = view.FindViewById<ProgressBar>(Resource.Id.optionCBar);
            progressBars[3] = view.FindViewById<ProgressBar>(Resource.Id.optionDBar);

            if (correctOption != 0)
            {
                var tTextView = textViews[correctOption];
                var tProgressBar = progressBars[correctOption];

                textViews[correctOption] = textViews[0];
                progressBars[correctOption] = progressBars[0];

                textViews[0] = tTextView;
                progressBars[0] = tProgressBar;
            }

            int centLeft = 100, option;

            option = Extensions.Random.Next(55, 85);
            textViews[0].Text = option + "%";
            progressBars[0].Progress = option;
            centLeft -= option;

            option = Extensions.Random.Next(centLeft);
            textViews[1].Text = option + "%";
            progressBars[1].Progress = option;
            centLeft -= option;

            option = Extensions.Random.Next(centLeft);
            textViews[2].Text = option + "%";
            progressBars[2].Progress = option;
            centLeft -= option;

            textViews[3].Text = centLeft + "%";
            progressBars[3].Progress = centLeft;

            return builder.Create();
        }
    }
}

