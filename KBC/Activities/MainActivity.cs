using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Widget;

namespace KBC
{
    [Activity(Label = "KBC", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        MediaPlayer introPlayer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
            newGameButton.Click += NewGame;

            introPlayer = MediaPlayer.Create(this, Resource.Raw.Intro);
            introPlayer.Start();
        }

        void NewGame(object sender, System.EventArgs e)
        {
            if (introPlayer.IsPlaying)
                introPlayer.Stop();

            var i = new Intent(this, typeof(GameActivity));
            StartActivity(i);            
        }
    }
}

