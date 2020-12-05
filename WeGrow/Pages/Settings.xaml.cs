using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials; 

namespace WeGrow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        private async void resetDataButton_Clicked(object sender, EventArgs e)
        {

            // Pop up question
            bool response = await DisplayAlert("Reset User Data", "This will clear all of your data. Are you sure you want to continue?", "I'm sure", "No");

            // If user said yes, proceed with reset 
            if (response)
            {

                // Clear data
                App.DatabaseAccess.ClearTables();
                Preferences.Set("firstUse", true); 

                // Go back to first page
                var existingPages = Navigation.NavigationStack.ToList();

                await Navigation.PushAsync(new MainPage()); 

                foreach (var page in existingPages)
                {
                    Navigation.RemovePage(page);
                }
            
            }

            return;
        }

        private async void changeNameButton_Clicked(object sender, EventArgs e)
        {

            string newName = await DisplayPromptAsync("Enter the new name you would like to use:", "");
            Models.UserInfo currentUser; 

            if (!String.IsNullOrEmpty(newName))
            {
                // Update in database
                currentUser = App.DatabaseAccess.GetUserInfos()[0];
                currentUser.Name = newName;

                App.DatabaseAccess.UpdateUser(currentUser);


                /* Replace old tracking page with reloaded tracking page
                var currentPages = Navigation.NavigationStack.ToList();
                var oldTrackingPage = currentPages[currentPages.Count - 2];

                Navigation.RemovePage(oldTrackingPage);

                await Navigation.PushAsync(new MainLoggingPage());
                */
            }
        }

        private async void changeLoggingButton_Clicked(object sender, EventArgs e)
        {

            string newTrackingMethod = await DisplayActionSheet("Select a new tracking method:", "Cancel", null, "Calorie counting", "Food groups", "Own thing");
            Models.UserInfo currentUser;

            if (!String.IsNullOrEmpty(newTrackingMethod))
            {
                switch (newTrackingMethod)
                {
                    case "Calorie counting":
                        newTrackingMethod = "countCalories";
                        break;
                    case "Food groups":
                        newTrackingMethod = "foodGroups";
                        break;
                    case "Own thing":
                        newTrackingMethod = "ownThing";
                        break;
                }
            }

            // Update in database
            currentUser = App.DatabaseAccess.GetUserInfos()[0];
            currentUser.TrackingPreference = newTrackingMethod; 

            App.DatabaseAccess.UpdateUser(currentUser);


            /* Replace old tracking page with reloaded tracking page
            var currentPages = Navigation.NavigationStack.ToList();
            var oldTrackingPage = currentPages[currentPages.Count - 2];

            Navigation.RemovePage(oldTrackingPage);

            await Navigation.PushAsync(new MainLoggingPage());
            */
        }

        private async void changeGoalWeightButton_Clicked(object sender, EventArgs e)
        {
            Models.UserInfo currentUser = App.DatabaseAccess.GetUserInfos()[0];

            int currentWeight = currentUser.CurrentWeight;
            string inputString = await DisplayPromptAsync("Enter your new goal weight:", "");
            int newWeight = 0; 

            

            if(!String.IsNullOrEmpty(inputString) && int.TryParse(inputString, out newWeight))
            {
                // If weight is valid, update in database
                if (newWeight >= currentWeight && ReferenceItems.isInConstraints(newWeight, currentUser.MeasurementUnit))
                {
                    currentUser.GoalWeight = newWeight;
                    App.DatabaseAccess.UpdateUser(currentUser);
                    return;
                }

                // Otherwise, display error message
                await DisplayAlert("Error", "Goal weight is either less than your current weight, or out of constraints.", "Okay");

            }

            else
            {
                await DisplayAlert("Error", "Invalid weight entered.", "Okay");
            }
                
            



        }
    }


}