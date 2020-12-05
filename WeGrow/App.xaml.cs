using System;
using System.Collections.Generic; 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Linq;
using System.Text;

namespace WeGrow
{
    public partial class App : Application
    {

        // Get path to use for database operations
        string dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "WeGrowData.db3"); 
        public static ManageDatabase DatabaseAccess { get; private set; }
        public App()
        {
            InitializeComponent();

            DatabaseAccess = new ManageDatabase(dbPath);


            if (Preferences.ContainsKey("firstUse") && Preferences.Get("firstUse", true))
            {
                var homePage = new MainPage();
                this.MainPage = new NavigationPage(homePage)
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.FromHex("#2c610e")
                };
            }
            else
            {
                var homePage = new Pages.MainLoggingPage();
                this.MainPage = new NavigationPage(homePage)
                {
                    BarBackgroundColor = Color.White,
                    BarTextColor = Color.FromHex("#2c610e")
                };
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
