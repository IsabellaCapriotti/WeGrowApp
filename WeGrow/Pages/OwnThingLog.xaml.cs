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
    public partial class OwnThingLog : ContentPage
    {
        string logDate;
        List<OwnThingLogInfo> foundLogs;
        bool isInitializedLog = false; 

        public OwnThingLog()
        {
            InitializeComponent();

            logDate = DateTime.Now.Date.ToShortDateString(); 
        }

        public OwnThingLog(string logDate)
        {
            InitializeComponent();

            this.logDate = logDate; 

            // Check if log has already been started
            foundLogs = App.DatabaseAccess.FindOwnThingLog(logDate); 

            if(foundLogs.Count == 0)
            {
                isInitializedLog = false; 
            }

            else
            {
                isInitializedLog = true;

                // Display log info on page
                breakfastNotes.Text = foundLogs[0].breakfastNotes;
                lunchNotes.Text = foundLogs[0].lunchNotes;
                dinnerNotes.Text = foundLogs[0].dinnerNotes;
                otherNotes.Text = foundLogs[0].otherNotes; 

                if(foundLogs[0].dayState == "good")
                {
                    goodDay.IsChecked = true; 
                }
                else if(foundLogs[0].dayState == "avg")
                {
                    avgDay.IsChecked = true; 
                }
                else if(foundLogs[0].dayState == "bad")
                {
                    badDay.IsChecked = true; 
                }

            }

        }

        private void submitButton_Clicked(object sender, EventArgs e)
        {
            // Get information from page
            string newBreakfastNotes = breakfastNotes.Text;
            string newLunchNotes = lunchNotes.Text;
            string newDinnerNotes = dinnerNotes.Text;
            string newOtherNotes = otherNotes.Text;
            string newDayState = ""; 

            if (goodDay.IsChecked)
            {
                newDayState = "good"; 
            }
            else if(avgDay.IsChecked){
                newDayState = "avg"; 
            }
            else if (badDay.IsChecked)
            {
                newDayState = "bad"; 
            }
            


            // Create or update log
            if (isInitializedLog)
            {
                foundLogs = App.DatabaseAccess.FindOwnThingLog(logDate);

                foundLogs[0].breakfastNotes = newBreakfastNotes;
                foundLogs[0].lunchNotes = newLunchNotes;
                foundLogs[0].dinnerNotes = newDinnerNotes;
                foundLogs[0].otherNotes = newOtherNotes;
                foundLogs[0].dayState = newDayState;

                App.DatabaseAccess.UpdateOwnThingLog(foundLogs[0]);
                Console.WriteLine("Updated old log"); 
            }

            else
            {
                OwnThingLogInfo newLog = new OwnThingLogInfo
                {
                    date = logDate,
                    isInitialized = true,
                    breakfastNotes = newBreakfastNotes,
                    lunchNotes = newLunchNotes,
                    dinnerNotes = newDinnerNotes,
                    otherNotes = newOtherNotes,
                    dayState = newDayState
                };

                isInitializedLog = true; 

                App.DatabaseAccess.CreateOwnThingLog(newLog);
                Console.WriteLine("Created new log"); 
            }
        }
    }
}