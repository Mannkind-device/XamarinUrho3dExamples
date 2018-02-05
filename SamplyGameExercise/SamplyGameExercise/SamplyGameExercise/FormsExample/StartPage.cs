using System;
using Xamarin.Forms;
namespace SamplyGameExercise.FormsExample
{
    public class StartPage : ContentPage
    {
        public StartPage()
        {
            var b = new Button { Text = "Launch Sample" };
            b.Clicked += (sender, e) => Navigation.PushAsync(new UrhoPage());

            Content = new StackLayout { Children = { b }, VerticalOptions = LayoutOptions.Center };

        }
    }
}
