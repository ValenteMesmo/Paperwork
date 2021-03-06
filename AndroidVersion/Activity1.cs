using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using GameCore.ActualCore;
using PaperWork;

namespace AndroidVersion
{
    [Activity(        
        MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        private Game1 game;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Vibrator vibrator = (Vibrator)GetSystemService(VibratorService);
            AndroidStuff.Vibrate = f => vibrator.Vibrate(f);
            AndroidStuff.RunningOnAndroid = true;
            game = new Game1();

            SetViewFullScreen();
            game.Run();
        }

        private void SetViewFullScreen()
        {
            game.FullScreen = true;
            var view = (View)game.Services.GetService(typeof(View));
            view.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.ImmersiveSticky
                );

            SetContentView(view);
        }

        protected override void OnResume()
        {
            game.UnPause();
            base.OnResume();
            SetViewFullScreen();
        }

        protected override void OnPause()
        {
            game.Pause();
            base.OnPause();
        }

        protected override void OnRestart()
        {
            game.UnPause();
            base.OnRestart();
            SetViewFullScreen();
        }
    }
}

