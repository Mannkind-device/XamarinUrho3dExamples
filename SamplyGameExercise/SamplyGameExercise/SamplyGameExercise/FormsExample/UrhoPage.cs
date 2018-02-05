using System;
using Xamarin.Forms;
using Urho.Forms;
using SamplyGameExercise.Samply;
using Urho;

namespace SamplyGameExercise.FormsExample
{
    public class UrhoPage : ContentPage
    {
        UrhoSurface UrhoSurface;
        SamplyGame UrhoApp;

        public UrhoPage()
        {
            var restartButton = new Button { Text = "Restart" };

            UrhoSurface = new UrhoSurface();
            UrhoSurface.VerticalOptions = LayoutOptions.FillAndExpand;

            Slider rotationSlider = new Slider(0, 500, 250);

            Slider selectedBarSlider = new Slider(0, 5, 2.5);

            Title = " UrhoSharp + Xamarin.Forms";
            Content = new StackLayout
            {
                Padding = new Thickness(12, 12, 12, 40),
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    UrhoSurface,
                    restartButton,
                    new Label { Text = "ROTATION::" },
                    rotationSlider,
                    new Label { Text = "SELECTED VALUE:" },
                    selectedBarSlider,
                }
            };
        }

        protected override void OnDisappearing()
        {
            UrhoSurface.OnDestroy();
            base.OnDisappearing();
        }

        protected override async void OnAppearing()
        {
            StartUrhoApp();
        }

        async void StartUrhoApp()
        {
            UrhoApp = 
                await UrhoSurface.Show<SamplyGame>(new ApplicationOptions(assetsFolder: "Data") { Orientation = ApplicationOptions.OrientationType.Portrait });
        }
    }
}
