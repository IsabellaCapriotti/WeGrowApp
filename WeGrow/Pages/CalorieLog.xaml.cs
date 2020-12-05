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
    public partial class CalorieLog : ContentPage
    {
        private int totalCals = 0;
        List<CalorieLogInfo> foundLogs = new List<CalorieLogInfo>(); 
        string logDate;
        private bool isInitializedLog = false; 

        public CalorieLog()
        {
            InitializeComponent();

            logDate = DateTime.Today.ToShortDateString();

            // Attempt to find log with current date in database 
            foundLogs = App.DatabaseAccess.FindCalorieLog(logDate);

            if (foundLogs.Count == 0)
            {
                isInitializedLog = false;
            }
            else
            {
                isInitializedLog = true;

                breakfastEntry.Text = foundLogs[0].breakfastCals.ToString();
                lunchEntry.Text = foundLogs[0].lunchCals.ToString();
                dinnerEntry.Text = foundLogs[0].dinnerCals.ToString();
                otherEntry.Text = foundLogs[0].otherCals.ToString();
                notes.Text = foundLogs[0].notes;
            }
        }

        public CalorieLog(string date)
        {

            InitializeComponent();

            logDate = date;

            // Attempt to find log with current date in database 
            List<CalorieLogInfo> foundLogs = App.DatabaseAccess.FindCalorieLog(logDate);

            if(foundLogs.Count == 0)
            {
                isInitializedLog = false;
            }
            else
            {
                isInitializedLog = true;

                breakfastEntry.Text = foundLogs[0].breakfastCals.ToString();
                lunchEntry.Text = foundLogs[0].lunchCals.ToString();
                dinnerEntry.Text = foundLogs[0].dinnerCals.ToString();
                otherEntry.Text = foundLogs[0].otherCals.ToString();
                notes.Text = foundLogs[0].notes;
            }

        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            int breakfastCals = 0;
            int lunchCals = 0;
            int dinnerCals = 0;
            int otherCals = 0;

            int.TryParse(breakfastEntry.Text, out breakfastCals);
            int.TryParse(lunchEntry.Text, out lunchCals);
            int.TryParse(dinnerEntry.Text, out dinnerCals);
            int.TryParse(otherEntry.Text, out otherCals);

            totalCals = breakfastCals + lunchCals + dinnerCals + otherCals;

            totalLabel.Text = totalCals.ToString() + " calories"; 
        }


        private void submitButton_Clicked(object sender, EventArgs e)
        {
            // Get values from page
            string newDate = logDate; 
            
            int newBreakfastCals = 0;
            int.TryParse(breakfastEntry.Text, out newBreakfastCals);

            int newLunchCals = 0;
            int.TryParse(lunchEntry.Text, out newLunchCals);

            int newDinnerCals = 0;
            int.TryParse(dinnerEntry.Text, out newDinnerCals);

            int newOtherCals = 0;
            int.TryParse(otherEntry.Text, out newOtherCals); 
  
            string newNotes = notes.Text;


            if (!isInitializedLog)
            {
                Console.WriteLine("Creating new log");
                CalorieLogInfo newLog = new CalorieLogInfo
                {
                    date = logDate,
                    isInitialized = true,
                    breakfastCals = newBreakfastCals,
                    lunchCals = newLunchCals,
                    dinnerCals = newDinnerCals,
                    otherCals = newOtherCals,
                    notes = newNotes
                };

                App.DatabaseAccess.CreateCalorieLog(newLog);

                isInitializedLog = true; 
            }
            else
            {
                Console.WriteLine("Updating old log"); 
                foundLogs = App.DatabaseAccess.FindCalorieLog(logDate);

                foundLogs[0].breakfastCals = newBreakfastCals;
                foundLogs[0].lunchCals = newLunchCals;
                foundLogs[0].dinnerCals = newDinnerCals;
                foundLogs[0].otherCals = newOtherCals;
                foundLogs[0].notes = newNotes;

                App.DatabaseAccess.UpdateCalorieLog(foundLogs[0]); 
            }


        }
    }

}