using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeGrow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateWeight : ContentPage
    {
        Models.UserInfo currentUser;
        int updatedWeight = -1; 
        public UpdateWeight()
        {
            // Load page with user info
            currentUser = App.DatabaseAccess.GetUserInfos()[0];

            InitializeComponent();

            currentWeight.Text = currentUser.CurrentWeight.ToString() + " " + currentUser.MeasurementUnit;
            plantImage.Source = currentUser.GrowthStage + "_" + currentUser.StarterPlant;
            Console.WriteLine(plantImage.Source);
        }

        private void decreaseWeightButton_Clicked(object sender, EventArgs e)
        {
            // Check that updated weight is not out of bounds
            updatedWeight = currentUser.CurrentWeight - 1;

            if(ReferenceItems.isInConstraints(updatedWeight, currentUser.MeasurementUnit)){

                currentUser.CurrentWeight = updatedWeight;

                // Update current weight in UI
                currentWeight.Text = updatedWeight.ToString() + " " + currentUser.MeasurementUnit;
            }

            
        }

        private void increaseWeightButton_Clicked(object sender, EventArgs e)
        {

            // Check that updated weight is not out of bounds
            updatedWeight = currentUser.CurrentWeight + 1;

            if(ReferenceItems.isInConstraints(updatedWeight, currentUser.MeasurementUnit))
            {
                currentUser.CurrentWeight = updatedWeight;

                // Update current weight in UI
                currentWeight.Text = updatedWeight.ToString() + " " + currentUser.MeasurementUnit;
            }

            
        }

        private async void confirmButton_Clicked(object sender, EventArgs e)
        {
            // Update current weight in database
            App.DatabaseAccess.UpdateUser(currentUser);

            // Plant growth logic
            double startWeight = Convert.ToDouble(currentUser.StartWeight);
            double weightGained = currentUser.CurrentWeight - startWeight;
            double weightToGain = currentUser.GoalWeight - startWeight;
            Console.WriteLine("Start Weight: " + startWeight.ToString()); 
            Console.WriteLine("Weight Gained: " + weightGained.ToString());
            Console.WriteLine("Total Weight to Gain: " + weightToGain.ToString()); 

            // If nothing gained
            if(weightGained <= 0)
            {
                return; 
            }

            // If goal weight reached
            else if(weightGained >= weightToGain)
            {
                // Update UI
                plantImage.Source = "final_" + currentUser.StarterPlant;
                plantImage.Opacity = 0;
                await plantImage.FadeTo(100, 1000, Easing.CubicInOut);      
                
                // Update database
                currentUser.GrowthStage = "final";
                App.DatabaseAccess.UpdateUser(currentUser); 


                await DisplayAlert("Goal Weight Met", "Congratulations on meeting your goal weight!\nFeel free to set a new goal in Settings, or just keep logging for your own convenience.", "Okay!");

            }

            // Standard case (recalculate growth stage) 
            else
            {
                double percentGained = (weightGained / weightToGain) * 100;
                Console.WriteLine("Percent gained: " + percentGained.ToString()); 

                if(percentGained <= 33)
                {
                    currentUser.GrowthStage = "starter"; 
                }
                else if (percentGained <= 66)
                {
                    currentUser.GrowthStage = "stage1"; 
                }
                else
                {
                    currentUser.GrowthStage = "stage2"; 
                }

                // Update in database
                App.DatabaseAccess.UpdateUser(currentUser);
                Console.WriteLine("New Growth Stage: " + currentUser.GrowthStage);

                // Update UI
                plantImage.Opacity = 0;
                plantImage.Source = currentUser.GrowthStage + "_" + currentUser.StarterPlant;
                await plantImage.FadeTo(100, 2000, Easing.CubicInOut);

            }
        }

        
    }
}