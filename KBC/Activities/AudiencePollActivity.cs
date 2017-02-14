using Android.App;
using Android.OS;
using Android.Widget;

namespace KBC
{
    [Activity(Label = "KBC", Icon = "@drawable/icon")]
    public class AudiencePollActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.AudiencePoll);

            var correctOption = Intent.Extras.GetInt("correctOption") - 1;

            var textViews = new TextView[4];
            textViews[0] = FindViewById<TextView>(Resource.Id.optionACent);
            textViews[1] = FindViewById<TextView>(Resource.Id.optionBCent);
            textViews[2] = FindViewById<TextView>(Resource.Id.optionCCent);
            textViews[3] = FindViewById<TextView>(Resource.Id.optionDCent);

            var progressBars = new ProgressBar[4];
            progressBars[0] = FindViewById<ProgressBar>(Resource.Id.optionABar);
            progressBars[1] = FindViewById<ProgressBar>(Resource.Id.optionBBar);
            progressBars[2] = FindViewById<ProgressBar>(Resource.Id.optionCBar);
            progressBars[3] = FindViewById<ProgressBar>(Resource.Id.optionDBar);

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
        }
    }
}

