using Foundation;
using UIKit;
using Urho;
using Urho.iOS;
using System.Threading.Tasks;
using SamplyGameExercise.Samply;
using SamplyGameExercise.FormsExample;

namespace SamplyGameExercise.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // LaunchGame();
            // return true;

            return LaunchForms(application, launchOptions);
        }

        async void LaunchGame()
        {
            await Task.Yield();
            //new MyGame(new ApplicationOptions("Data")).Run();
            new SamplyGame().Run();
        }


        bool LaunchForms(UIApplication application, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(application, launchOptions);
        }
    }
}


