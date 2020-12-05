using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials; 

namespace WeGrow
{
    public partial class MainPage : ContentPage
    {
        // Plant sources to cycle through
        List<string> plantImageSources = ReferenceItems.PlantImageSources; 

        int currPlantImageSource = 0;

        string userName; 

        public MainPage()
        {
            InitializeComponent();

            // Set first use flag to true
            Preferences.Set("firstUse", true); 
        }

        private async void cycleStarterPlantLeftButton_Clicked(object sender, EventArgs e)
        {
            // Cycle through to next image to the left
            --currPlantImageSource;
            if(currPlantImageSource < 0)
            {
                currPlantImageSource = plantImageSources.Count - 1;
            }

            starterPlantImage.Source = plantImageSources[currPlantImageSource];
            starterPlantImage.Opacity = 0;
            await starterPlantImage.FadeTo(100, 4000, Easing.SinInOut);
        }

        private async void cycleStarterPlantRightButton_Clicked(object sender, EventArgs e)
        {
            // Cycle through to next image to the right
            ++currPlantImageSource;
            if(currPlantImageSource >= plantImageSources.Count)
            {
                currPlantImageSource = 0; 
            }

            starterPlantImage.Source = plantImageSources[currPlantImageSource];
            starterPlantImage.Opacity = 0;
            await starterPlantImage.FadeTo(100, 4000, Easing.SinInOut); 
        }

        private async void starterPlantSelectProceedButton_Clicked(object sender, EventArgs e)
        {
            // Extract name to store of starter plant from current source 
            string starterPlant = plantImageSources[currPlantImageSource].Substring(plantImageSources[currPlantImageSource].IndexOf("_") + 1);
            starterPlant = starterPlant.Remove(starterPlant.IndexOf("."));

            // Create User profile based on plant selection
            Models.UserInfo newUser = new Models.UserInfo
            {
                Name = userName,
                StarterPlant = starterPlant
            };

            // Navigate to tracking select page, sending over user information 
            var nextPage = new TrackingSelectPage(newUser);

            await this.Navigation.PushAsync(nextPage); 

        }

        private async void nameSelectProceedButton_Clicked(object sender, EventArgs e)
        {
            emptyNameErrorText.IsVisible = false; 

            // Store entered name; give error if empty 
            if (String.IsNullOrEmpty(userNameEntry.Text))
            {
                emptyNameErrorText.IsVisible = true;
                return; 
            }
            else
            {
                userName = userNameEntry.Text;

                // Switch visibility to plant selection 
                userNameSelection.IsVisible = false;
                starterPlantSelection.IsVisible = true;

                await starterPlantSelection.FadeTo(100, 3000, Easing.SinInOut); 
            }

        }
    }

    
}
