using Android.App;
using Android.OS;

namespace KBC
{
    [Activity(Label = "KBC", Icon = "@drawable/icon")]
    public class ResultActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Result);
        }
    }
}

