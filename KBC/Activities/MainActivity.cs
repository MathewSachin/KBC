using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace KBC
{
    [Activity(Label = "KBC", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
            newGameButton.Click += NewGame;
        }
                
        void NewGame(object sender, System.EventArgs e)
        {
            var i = new Intent(this, typeof(GameActivity));
            StartActivity(i);            
        }
    }
}

