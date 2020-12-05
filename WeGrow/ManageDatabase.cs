using System;
using System.Collections.Generic;
using System.Linq;
using WeGrow.Models;
using SQLite; 

namespace WeGrow
{
    public class ManageDatabase
    {
        private SQLiteConnection conn;

        public ManageDatabase(string dbpath)
        {

            // Create new database connection at passed file path 
            try
            {
                conn = new SQLiteConnection(dbpath);

                Console.WriteLine("Successfully created database connection!"); 

            }
            
            catch(Exception ex)
            {
                Console.WriteLine($"Error: Failed to open DB connection.\nError message: {ex.Message}");
            }

        }

        public void CreateUser(Models.UserInfo user)
        {
            try
            {
                conn.Insert(user);

                Console.WriteLine("Created new user!"); 

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to initialize new user.\nError message: {ex}"); 
            }
        }

        public void ClearTables()
        {
            try
            {
                conn.DropTable<UserInfo>();
                conn.DropTable<CalorieLogInfo>();
                conn.DropTable<FoodGroupLogInfo>();
                conn.DropTable<OwnThingLogInfo>();

            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to reinitialize user table.\nError message: {ex}");
            }
        }


        public void CreateTables()
        {
            try
            {
                conn.CreateTable<UserInfo>();
                conn.CreateTable<CalorieLogInfo>();
                conn.CreateTable<FoodGroupLogInfo>();
                conn.CreateTable<OwnThingLogInfo>(); 
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to create tables.\nErorr message: {ex}"); 
            }
        }
        public List<UserInfo> GetUserInfos()
        {
            try
            {     
                return conn.Table<UserInfo>().ToList(); 
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to return table.\nError message: {ex}");
                return new List<UserInfo>(); 
            }
        }


        public void UpdateUser(UserInfo newUser)
        {
            try
            {
                conn.Update(newUser); 
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to update user info.\nError message: {ex}"); 
            }
        }

        public void CreateCalorieLog(CalorieLogInfo newLog)
        {
            try
            {
                conn.Insert(newLog);
                Console.WriteLine("Created new calorie log!"); 
            }
            
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to create new calorie log.\nError message: {ex}"); 
            }
        }

        public void UpdateCalorieLog(CalorieLogInfo newLog)
        {
            try
            {
                conn.Update(newLog);
                Console.WriteLine("Updated calorie log.");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to update calorie log.\nError message: {ex}"); 
            }
        }

        public List<CalorieLogInfo> FindCalorieLog(string searchDate)
        {
            try
            {
                List<CalorieLogInfo> foundLogs = conn.Query<CalorieLogInfo>("select * from CalorieLogInfo where date = ?", searchDate);
                return foundLogs; 
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to find calorie log.\nError message: {ex}");
                return new List<CalorieLogInfo>(); 
            }
        }

        public List<CalorieLogInfo> GetAllCalorieLogs()
        {
            try
            {
                return conn.Query<CalorieLogInfo>("select * from CalorieLogInfo");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get all calorie logs.\nError message: {ex}");
                return new List<CalorieLogInfo>();
            }
        }

        public void CreateFoodGroupLog(FoodGroupLogInfo newLog)
        {
            try
            {
                conn.Insert(newLog); 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to create new food group log.\nError message: {ex}"); 
            }
        }

        public void UpdateFoodGroupLog(FoodGroupLogInfo newLog)
        {
            try
            {
                conn.Update(newLog); 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to updatefood group log.\nError message: {ex}");
            }
        }

        public List<FoodGroupLogInfo> FindFoodGroupLog(string searchDate)
        {
            try
            {
                return conn.Query<FoodGroupLogInfo>("select * from FoodGroupLogInfo where date = ?", searchDate); 
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to find food group log.\nError message: {ex}");
                return new List<FoodGroupLogInfo>(); 
            }
        }

        public List<FoodGroupLogInfo> GetAllFoodGroupLogs()
        {
            try
            {
                return conn.Query<FoodGroupLogInfo>("select * from FoodGroupLogInfo");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to get all food group logs.\nError message: {ex}");
                return new List<FoodGroupLogInfo>(); 
            }
        }

        public void CreateOwnThingLog(OwnThingLogInfo newLog)
        {
            try
            {
                conn.Insert(newLog); 
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to create new own thing log.\nError message: {ex}"); 
            }

        }

        public void UpdateOwnThingLog(OwnThingLogInfo newLog)
        {
            try
            {
                conn.Update(newLog); 
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to update own thing log.\nError message: {ex}");
            }
        }

        public List<OwnThingLogInfo> FindOwnThingLog(string searchDate)
        {
            try
            {
                return conn.Query<OwnThingLogInfo>("select * from OwnThingLogInfo where date = ?", searchDate); 
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to find own thing log.\nError message: {ex}");
                return new List<OwnThingLogInfo>(); 
            }
        }

        public List<OwnThingLogInfo> GetAllOwnThingLogs()
        {
            try
            {
                return conn.Query<OwnThingLogInfo>("select * from OwnThingLogInfo");
            }

            catch(Exception ex)
            {
                Console.WriteLine($"Failed to get all own thing logs.\nError message: {ex}");
                return new List<OwnThingLogInfo>(); 
            }
        }

        
    }
}