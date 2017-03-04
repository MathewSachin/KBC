using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;

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
            newGameButton.SetColor(Extensions.OptionDefaultColor);
            newGameButton.Click += NewGame;

            var helpButton = FindViewById<Button>(Resource.Id.helpButton);
            helpButton.SetColor(Extensions.OptionDefaultColor);
            helpButton.Click += Help;
        }

        void Help(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(HelpActivity));
            StartActivity(i);
        }
        
        void NewGame(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(GameActivity));
            StartActivity(i);            
        }
    }
}

