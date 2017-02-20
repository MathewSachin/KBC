using Android.App;
using Android.OS;
using Android.Text;
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

            var helpText = FindViewById<TextView>(Resource.Id.helpText);

            var html = Html.FromHtml(@"
<h1>Hello</h1>
hi<br/>
<b>bold</b>
");

            helpText.TextFormatted = html;
        }
    }
}

