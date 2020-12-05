using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WeGrow.Models;

namespace WeGrow.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewLogs : ContentPage
    {
        private Models.UserInfo currentUser; 
        public ViewLogs()
        {
            InitializeComponent();

            currentUser = App.DatabaseAccess.GetUserInfos().ToList()[0];

            // Clear old buttons
            var oldChildren = new List<View>();

            foreach(var child in mainLayout.Children)
            {
                oldChildren.Add(child);
            }

            foreach (var child in oldChildren)
            {
                if (child.BackgroundColor != Color.FromHex("#2c610e"))
                {
                    mainLayout.Children.Remove(child);
                }
            }

            // Load all past logs (except today)
            switch (currentUser.TrackingPreference)
            {
                case "countCalories":
                    List<CalorieLogInfo> calorieLogs = App.DatabaseAccess.GetAllCalorieLogs(); 

                    if(calorieLogs.Count > 0)
                    {
                        foreach(CalorieLogInfo log in calorieLogs)
                        {
                            Button newButton = new Button
                            {
                                BackgroundColor = Color.White,
                                TextColor = Color.FromHex("#2c610e"),
                                Text = log.date,
                                CornerRadius = 10
                            };

                            newButton.Clicked += openLogButton_Clicked;

                            mainLayout.Children.Add(newButton); 
                        }
                    }
                    break;

                case "foodGroups":
                    List<FoodGroupLogInfo> foodGroupLogs = App.DatabaseAccess.GetAllFoodGroupLogs(); 

                    if(foodGroupLogs.Count > 0)
                    {
                        foreach(FoodGroupLogInfo log in foodGroupLogs)
                        {
                            Button newButton = new Button
                            {
                                BackgroundColor = Color.White,
                                TextColor = Color.FromHex("#2c610e"),
                                Text = log.date,
                                CornerRadius = 10
                            };

                            newButton.Clicked += openLogButton_Clicked;

                            mainLayout.Children.Add(newButton);
                        }
                    }
                    
                    break;
                case "ownThing":
                    List<OwnThingLogInfo> ownThingLogs = App.DatabaseAccess.GetAllOwnThingLogs();
                    
                    if(ownThingLogs.Count > 0)
                    {
                        foreach(OwnThingLogInfo log in ownThingLogs)
                        {                       
                            Button newButton = new Button
                            {
                                BackgroundColor = Color.White,
                                TextColor = Color.FromHex("#2c610e"),
                                Text = log.date,
                                CornerRadius = 10
                            };

                            newButton.Clicked += openLogButton_Clicked;

                            mainLayout.Children.Add(newButton);
                        }
                    }

                    break;
            }


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            currentUser = App.DatabaseAccess.GetUserInfos().ToList()[0];

            // Clear old buttons
            var oldChildren = new List<View>();

            foreach (var child in mainLayout.Children)
            {
                oldChildren.Add(child);
            }

            foreach (var child in oldChildren)
            {
                if (child.BackgroundColor != Color.FromHex("#2c610e"))
                {
                    mainLayout.Children.Remove(child);
                }
            }

            // Load all past logs (except today)
            switch (currentUser.TrackingPreference)
            {
                case "countCalories":
                    List<CalorieLogInfo> calorieLogs = App.DatabaseAccess.GetAllCalorieLogs();

                    if (calorieLogs.Count > 0)
                    {

                        foreach (CalorieLogInfo log in calorieLogs)
                        {               
                            Button newButton = new Button
                            {
                                BackgroundColor = Color.White,
                                TextColor = Color.FromHex("#2c610e"),
                                Text = log.date,
                                CornerRadius = 10
                            };

                            newButton.Clicked += openLogButton_Clicked;

                            mainLayout.Children.Add(newButton);
                        }
                    }
                    break;
                case "foodGroups":
                    List<FoodGroupLogInfo> foodGroupLogs = App.DatabaseAccess.GetAllFoodGroupLogs();

                    if (foodGroupLogs.Count > 0)
                    {
                        foreach (FoodGroupLogInfo log in foodGroupLogs)
                        {
                            Button newButton = new Button
                            {
                                BackgroundColor = Color.White,
                                TextColor = Color.FromHex("#2c610e"),
                                Text = log.date,
                                CornerRadius = 10
                            };

                            newButton.Clicked += openLogButton_Clicked;

                            mainLayout.Children.Add(newButton);
                        }
                    }
                    break;
                case "ownThing":
                    List<OwnThingLogInfo> ownThingLogs = App.DatabaseAccess.GetAllOwnThingLogs();

                    if (ownThingLogs.Count > 0)
                    {
                        foreach (OwnThingLogInfo log in ownThingLogs)
                        {                          
                            Button newButton = new Button
                            {
                                BackgroundColor = Color.White,
                                TextColor = Color.FromHex("#2c610e"),
                                Text = log.date,
                                CornerRadius = 10
                            };

                            newButton.Clicked += openLogButton_Clicked;

                            mainLayout.Children.Add(newButton);            
                        }
                    }
                    break;
            }
        }

        private async void newLogButton_Clicked(object sender, EventArgs e)
        {

            // Open up new logging view depending on user's logging style
            switch (currentUser.TrackingPreference)
            {
                case "countCalories":
                    await Navigation.PushAsync(new CalorieLog(DateTime.Now.Date.ToShortDateString())); 
                    break;
                case "foodGroups":
                    await Navigation.PushAsync(new FoodGroupLog(DateTime.Now.Date.ToShortDateString())); 
                    break;
                case "ownThing":
                    await Navigation.PushAsync(new OwnThingLog(DateTime.Now.Date.ToShortDateString())); 
                    break;
            }
            
        }

        private async void openLogButton_Clicked(object sender, EventArgs e)
        {
            string logDate = ((Button)sender).Text;

            switch (currentUser.TrackingPreference)
            {
                case "countCalories":
                    await Navigation.PushAsync(new CalorieLog(logDate));
                    break;
                case "foodGroups":
                    await Navigation.PushAsync(new FoodGroupLog(logDate));
                    break;
                case "ownThing":
                    await Navigation.PushAsync(new OwnThingLog(logDate));
                    break;
            }
        }
    }
}