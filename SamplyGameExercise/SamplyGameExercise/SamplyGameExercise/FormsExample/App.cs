using System;
using Xamarin.Forms;
namespace SamplyGameExercise.FormsExample
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {
            MainPage = new NavigationPage(new StartPage { });
        }
    }
}
