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
    public partial class FoodGroupLog : ContentPage
    {
        static List<string> labels = new List<string> { "Veg", "Fruit", "Protein", "Grain", "Other" };

        List<FoodGroupLogInfo> foundLogs = new List<FoodGroupLogInfo>();
        string logDate; 
        bool isInitializedLog = false; 

        public FoodGroupLog()
        {
            InitializeComponent();

            logDate = DateTime.Now.Date.ToShortDateString(); 

            // Check if log has already been started
            foundLogs = App.DatabaseAccess.FindFoodGroupLog(logDate);

            if (foundLogs.Count == 0)
            {
                isInitializedLog = false;
            }
            else
            {
                isInitializedLog = true;

                FoodGroupLogInfo currentLog = foundLogs[0];

                // Display log on page
                breakfastVegBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[0].ToString());
                breakfastFruitBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[1].ToString());
                breakfastProteinBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[2].ToString());
                breakfastGrainBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[3].ToString());
                breakfastOtherBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[4].ToString());

                lunchVegBox.IsChecked = stringToBool(foundLogs[0].lunchVals[0].ToString());
                lunchFruitBox.IsChecked = stringToBool(foundLogs[0].lunchVals[1].ToString());
                lunchProteinBox.IsChecked = stringToBool(foundLogs[0].lunchVals[2].ToString());
                lunchGrainBox.IsChecked = stringToBool(foundLogs[0].lunchVals[3].ToString());
                lunchOtherBox.IsChecked = stringToBool(foundLogs[0].lunchVals[4].ToString());

                dinnerVegBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[0].ToString());
                dinnerFruitBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[1].ToString());
                dinnerProteinBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[2].ToString());
                dinnerGrainBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[3].ToString());
                dinnerOtherBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[4].ToString());

                otherVegBox.IsChecked = stringToBool(foundLogs[0].otherVals[0].ToString());
                otherFruitBox.IsChecked = stringToBool(foundLogs[0].otherVals[1].ToString());
                otherProteinBox.IsChecked = stringToBool(foundLogs[0].otherVals[2].ToString());
                otherGrainBox.IsChecked = stringToBool(foundLogs[0].otherVals[3].ToString());
                otherOtherBox.IsChecked = stringToBool(foundLogs[0].otherVals[4].ToString());

                notes.Text = foundLogs[0].notes;
            }

        }

        public FoodGroupLog(string date)
        {
            InitializeComponent();

            logDate = date;

            // Check if log has already been started
            foundLogs = App.DatabaseAccess.FindFoodGroupLog(logDate); 

            if(foundLogs.Count == 0)
            {
                isInitializedLog = false;
            }
            else
            {
                isInitializedLog = true;

                FoodGroupLogInfo currentLog = foundLogs[0];

                // Display log on page
                breakfastVegBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[0].ToString());
                breakfastFruitBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[1].ToString());
                breakfastProteinBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[2].ToString());
                breakfastGrainBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[3].ToString());
                breakfastOtherBox.IsChecked = stringToBool(foundLogs[0].breakfastVals[4].ToString());

                lunchVegBox.IsChecked = stringToBool(foundLogs[0].lunchVals[0].ToString());
                lunchFruitBox.IsChecked = stringToBool(foundLogs[0].lunchVals[1].ToString());
                lunchProteinBox.IsChecked = stringToBool(foundLogs[0].lunchVals[2].ToString());
                lunchGrainBox.IsChecked = stringToBool(foundLogs[0].lunchVals[3].ToString());
                lunchOtherBox.IsChecked = stringToBool(foundLogs[0].lunchVals[4].ToString());

                dinnerVegBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[0].ToString());
                dinnerFruitBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[1].ToString());
                dinnerProteinBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[2].ToString());
                dinnerGrainBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[3].ToString());
                dinnerOtherBox.IsChecked = stringToBool(foundLogs[0].dinnerVals[4].ToString());

                otherVegBox.IsChecked = stringToBool(foundLogs[0].otherVals[0].ToString());
                otherFruitBox.IsChecked = stringToBool(foundLogs[0].otherVals[1].ToString());
                otherProteinBox.IsChecked = stringToBool(foundLogs[0].otherVals[2].ToString());
                otherGrainBox.IsChecked = stringToBool(foundLogs[0].otherVals[3].ToString());
                otherOtherBox.IsChecked = stringToBool(foundLogs[0].otherVals[4].ToString());

                notes.Text = foundLogs[0].notes; 
            }

        }

        private void VegBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            int currentVeg = int.Parse(vegTotal.Text);

            if (e.Value)
            {
                ++currentVeg; 
            }
            else
            {
                --currentVeg; 
            }

            vegTotal.Text = currentVeg.ToString(); 
        }

        private void FruitBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            int currentFruit = int.Parse(fruitTotal.Text);

            if (e.Value)
            {
                ++currentFruit;
            }
            else
            {
                --currentFruit;
            }

            fruitTotal.Text = currentFruit.ToString();
        }

        private void ProteinBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            int currentProtein = int.Parse(proteinTotal.Text);

            if (e.Value)
            {
                ++currentProtein;
            }
            else
            {
                --currentProtein;
            }

            proteinTotal.Text = currentProtein.ToString();
        }

        private void GrainBox_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            int currentGrain = int.Parse(grainTotal.Text);

            if (e.Value)
            {
                ++currentGrain;
            }
            else
            {
                --currentGrain;
            }

            grainTotal.Text = currentGrain.ToString();
        }

        private void OtherBox_CheckChanged(object sender, CheckedChangedEventArgs e)
        {
            int currentOther = int.Parse(otherTotal.Text);

            if (e.Value)
            {
                ++currentOther;
            }
            else
            {
                --currentOther;
            }

            otherTotal.Text = currentOther.ToString();
        }

        private void submitButton_Clicked(object sender, EventArgs e)
        {

            // Set value strings 
            
            string newBreakfastVals = "";

            newBreakfastVals = newBreakfastVals + Convert.ToInt32(breakfastVegBox.IsChecked).ToString();
            newBreakfastVals = newBreakfastVals + Convert.ToInt32(breakfastFruitBox.IsChecked).ToString();
            newBreakfastVals = newBreakfastVals + Convert.ToInt32(breakfastProteinBox.IsChecked).ToString();
            newBreakfastVals = newBreakfastVals + Convert.ToInt32(breakfastGrainBox.IsChecked).ToString();
            newBreakfastVals = newBreakfastVals + Convert.ToInt32(breakfastOtherBox.IsChecked).ToString();

            string newLunchVals = "";

            newLunchVals = newLunchVals + Convert.ToInt32(lunchVegBox.IsChecked).ToString();
            newLunchVals = newLunchVals + Convert.ToInt32(lunchFruitBox.IsChecked).ToString();
            newLunchVals = newLunchVals + Convert.ToInt32(lunchProteinBox.IsChecked).ToString();
            newLunchVals = newLunchVals + Convert.ToInt32(lunchGrainBox.IsChecked).ToString();
            newLunchVals = newLunchVals + Convert.ToInt32(lunchOtherBox.IsChecked).ToString();

            string newDinnerVals = "";

            newDinnerVals = newDinnerVals + Convert.ToInt32(dinnerVegBox.IsChecked).ToString();
            newDinnerVals = newDinnerVals + Convert.ToInt32(dinnerFruitBox.IsChecked).ToString();
            newDinnerVals = newDinnerVals + Convert.ToInt32(dinnerProteinBox.IsChecked).ToString();
            newDinnerVals = newDinnerVals + Convert.ToInt32(dinnerGrainBox.IsChecked).ToString();
            newDinnerVals = newDinnerVals + Convert.ToInt32(dinnerOtherBox.IsChecked).ToString();

            string newOtherVals = "";

            newOtherVals = newOtherVals + Convert.ToInt32(otherVegBox.IsChecked).ToString();
            newOtherVals = newOtherVals + Convert.ToInt32(otherFruitBox.IsChecked).ToString();
            newOtherVals = newOtherVals + Convert.ToInt32(otherProteinBox.IsChecked).ToString();
            newOtherVals = newOtherVals + Convert.ToInt32(otherGrainBox.IsChecked).ToString();
            newOtherVals = newOtherVals + Convert.ToInt32(otherOtherBox.IsChecked).ToString();
            

            // Set notes
            string newNotes = notes.Text;

            if (isInitializedLog)
            {
                // Get matching log and update
                foundLogs = App.DatabaseAccess.FindFoodGroupLog(logDate);

                foundLogs[0].breakfastVals = newBreakfastVals;
                foundLogs[0].lunchVals = newLunchVals;
                foundLogs[0].dinnerVals = newDinnerVals;
                foundLogs[0].otherVals = newOtherVals;
                foundLogs[0].notes = newNotes;

                App.DatabaseAccess.UpdateFoodGroupLog(foundLogs[0]);

                isInitializedLog = true; 

                Console.WriteLine("Updated log"); 
            }
            else
            {
                // Create new log
                FoodGroupLogInfo newLog = new FoodGroupLogInfo
                {
                    date = logDate,
                    isInitialized = true,
                    breakfastVals = newBreakfastVals,
                    lunchVals = newLunchVals,
                    dinnerVals = newDinnerVals,
                    otherVals = newOtherVals,
                    notes = newNotes
                };

                isInitializedLog = true;

                App.DatabaseAccess.CreateFoodGroupLog(newLog);

                Console.WriteLine("Created new log"); 
            }

            
        }

        private bool stringToBool(string toConvert)
        {
            if(toConvert == "0")
            {
                return false;
            }
            else
            {
                return true; 
            }
        }
    }
}