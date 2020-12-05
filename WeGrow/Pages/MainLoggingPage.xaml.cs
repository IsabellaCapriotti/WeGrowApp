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
    public partial class MainLoggingPage : ContentPage
    {
        Models.UserInfo currentUser;
        public MainLoggingPage()
        {
            // Get user info
            currentUser = App.DatabaseAccess.GetUserInfos()[0];
            
            InitializeComponent();

            // Update UI 
            welcomeLabel.Text = "Welcome back, " + currentUser.Name;
            plantImage.Source = currentUser.GrowthStage + "_" + currentUser.StarterPlant;
            currentWeight.Text = "Current weight: " + currentUser.CurrentWeight.ToString() + " " + currentUser.MeasurementUnit;
            goalWeight.Text = "Goal weight: " + currentUser.GoalWeight.ToString() + " " + currentUser.MeasurementUnit;

            string dateStr = DateTime.Today.Date.ToShortDateString();
            date.Text = dateStr;

            // Fix image scale
            settingsButton.Scale = 1.2;
            logsButton.Scale = 1.2;
            updateWeightButton.Scale = 1.2; 

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Get user info
            currentUser = App.DatabaseAccess.GetUserInfos()[0];

            // Update UI
            welcomeLabel.Text = "Welcome back, " + currentUser.Name;
            plantImage.Source = currentUser.GrowthStage + "_" + currentUser.StarterPlant;
            currentWeight.Text = "Current weight: " + currentUser.CurrentWeight.ToString() + " " + currentUser.MeasurementUnit;
            goalWeight.Text = "Goal weight: " + currentUser.GoalWeight.ToString() + " " + currentUser.MeasurementUnit;

            string dateStr = DateTime.Today.Date.ToShortDateString();
            date.Text = dateStr;

            // Fix image scale
            settingsButton.Scale = 1.2;
            logsButton.Scale = 1.2;
            updateWeightButton.Scale = 1.2;

        }

        private async void settingsButton_Clicked(object sender, EventArgs e)
        {
            await settingsButton.ScaleTo(Scale * 1.5);
            settingsButton.Scale = 1.2; 
            await Navigation.PushAsync(new Settings()); 
        }

        private async void updateWeightButton_Clicked(object sender, EventArgs e)
        {
            await updateWeightButton.ScaleTo(Scale * 1.5);
            settingsButton.Scale = 1.2;
            await Navigation.PushAsync(new UpdateWeight()); 
        }

        private async void logsButton_Clicked(object sender, EventArgs e)
        {
            await logsButton.ScaleTo(Scale * 1.5);
            settingsButton.Scale = 1.2;
            await Navigation.PushAsync(new ViewLogs()); 
        }

       
    }
}