using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials; 

namespace WeGrow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackingSelectPage : ContentPage
    {
        Models.UserInfo inProgressUser;
        string currMeasurement = "lbs"; 
        public TrackingSelectPage()
        {
            InitializeComponent();
        }

        public TrackingSelectPage(Models.UserInfo createdUser)
        {
            InitializeComponent();

            // Set user profile to in-progress profile from previous page
            inProgressUser = createdUser; 
        }

        private async void countCalories_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // Change watering can image, do rotation
            if (countCalories.IsChecked)
            {
                wateringCanImage.Source = "watering_can_2";
                await wateringCanImage.RotateTo(-20, 700); 
            }

            else
            {
                wateringCanImage.Source = "watering_can_1";
                wateringCanImage.Rotation = 0; 
            }
        }

        private async void foodGroups_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // Change fertilizer image
            if (foodGroups.IsChecked)
            {
                fertilizerImage.Source = "fertilizer_2";
                fertilizerImage.Opacity = 0;
                await fertilizerImage.FadeTo(100, 3000, Easing.SinIn);
            }

            else
            {
                fertilizerImage.Source = "fertilizer_1"; 
            }
        }

        private async void ownThing_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // Change bee image and do rotation
            if (ownThing.IsChecked)
            {
                beeImage.Source = "bee_2";
                await beeImage.RotateTo(20, 700); 
            }

            else
            {
                beeImage.Source = "bee_1";
                beeImage.Rotation = 0; 
            }
        }

        private async void trackingSelectProceedButton_Clicked(object sender, EventArgs e)
        {

            int goalWeightInt = 0;
            int currentWeightInt = 0; 

            // Check for errors in weight entry
            errorBlankWeight.IsVisible = false;
            errorLoseWeight.IsVisible = false;
            errorNonNumericWeight.IsVisible = false;

            
            if(String.IsNullOrEmpty(goalWeight.Text) || String.IsNullOrEmpty(currentWeight.Text))
            {
                errorBlankWeight.IsVisible = true;
                return; 
            }

            if(!int.TryParse(goalWeight.Text, out goalWeightInt))
            {
                errorNonNumericWeight.IsVisible = true;
                return;
            }

            if(!int.TryParse(currentWeight.Text, out currentWeightInt))
            {
                errorNonNumericWeight.IsVisible = true;
                return; 
            }

            
            if(goalWeightInt <= currentWeightInt)
            {
                errorLoseWeight.IsVisible = true;
                return;
            }


            // Check for weight within min and max bounds

            if(!ReferenceItems.isInConstraints(goalWeightInt, currMeasurement))
            {
                errorWeightOutOfRange.IsVisible = true;
                return; 
            }
            

            // Update user information if weights are valid
            inProgressUser.GoalWeight = goalWeightInt;
            inProgressUser.CurrentWeight = currentWeightInt;
            inProgressUser.StartWeight = currentWeightInt;

            // Check for current selected radio button
            if(ownThing.IsChecked)
            {
                inProgressUser.TrackingPreference = "ownThing"; 
            }
            else if (countCalories.IsChecked)
            {
                inProgressUser.TrackingPreference = "countCalories"; 
            }
            else if (foodGroups.IsChecked)
            {
                inProgressUser.TrackingPreference = "foodGroups"; 
            }

            // Set measurement preference and growth stage
            inProgressUser.MeasurementUnit = currMeasurement;
            inProgressUser.GrowthStage = "starter"; 

            // Get random ID for new user
            inProgressUser.userId = new Random().Next();

            // Clear database, create new database, insert user profile into it
            App.DatabaseAccess.ClearTables();
            App.DatabaseAccess.CreateTables(); 
            App.DatabaseAccess.CreateUser(inProgressUser);

            // Update preferences to indicate that profile has been established
            Preferences.Set("firstUse", false);

            // Clear current navigation stack, go to main logging page
            var existingPages = Navigation.NavigationStack.ToList(); 

            await Navigation.PushAsync(new Pages.MainLoggingPage()); 

            foreach(var page in existingPages)
            {
                Navigation.RemovePage(page); 
            }
        }


        private void measurementSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            int goalWeightInt = 0;
            int currentWeightInt = 0;

            // Switch to kg and convert weights
            if (measurementSwitch.IsToggled && currMeasurement == "lbs")
            {
                currMeasurement = "kg"; 

                if(!int.TryParse(goalWeight.Text, out goalWeightInt) || !int.TryParse(currentWeight.Text, out currentWeightInt))
                {
                    return; 
                }

                else
                {
                    goalWeightInt = (int)Math.Round(goalWeightInt / 2.205);
                    goalWeight.Text = goalWeightInt.ToString();

                    currentWeightInt = (int)Math.Round(currentWeightInt / 2.205);
                    currentWeight.Text = currentWeightInt.ToString(); 

                    return; 

                }

            }

            // Switch to lbs and convert weights
            else if(!measurementSwitch.IsToggled && currMeasurement == "kg")
            {
                currMeasurement = "lbs";

                if (!int.TryParse(goalWeight.Text, out goalWeightInt) || !int.TryParse(currentWeight.Text, out currentWeightInt))
                {
                    return;
                }

                else
                {
                    goalWeightInt = (int)Math.Round(goalWeightInt * 2.205);
                    goalWeight.Text = goalWeightInt.ToString();

                    currentWeightInt = (int)Math.Round(currentWeightInt * 2.205);
                    currentWeight.Text = currentWeightInt.ToString();

                    return;

                }
            }
        }
    }
}