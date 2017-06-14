using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using GameCore.ActualCore;
using PaperWork;

namespace AndroidVersion
{
    [Activity(Label = "AndroidVersion"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Vibrator vibrator = (Vibrator)GetSystemService(VibratorService);            
            AndroidStuff.Vibrate = f => vibrator.Vibrate(f, -1);

            var g = new Game1() { FullScreen = true };
            var view = (View)g.Services.GetService(typeof(View));
            view.SystemUiVisibility = (StatusBarVisibility)
                (SystemUiFlags.LayoutStable
                | SystemUiFlags.LayoutHideNavigation
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.HideNavigation
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.ImmersiveSticky
                );

            SetContentView(view);
            g.Run();
        }
    }
}

