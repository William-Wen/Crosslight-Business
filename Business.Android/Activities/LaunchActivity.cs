using Android.App;
using Intersoft.Crosslight.Android;

namespace Business.Android
{
    /// <summary>
    ///     The splash screen Activity. To change the launcher icon for the Android app, simply change the Icon property.
    /// </summary>
    [Activity(Label = "Business.Android", MainLauncher = true, NoHistory = true, Icon = "@mipmap/icon", Theme = "@style/Theme.Splash")]
    public class LaunchActivity : StartActivity
    {
    }
}