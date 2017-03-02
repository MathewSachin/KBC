using Android.App;
using Android.OS;

namespace KBC
{
    [Activity(Label = "Help | KBC")]
    public class HelpActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView (Resource.Layout.Help);
        }
    }
}

