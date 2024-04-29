using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using SuperbetBeclean.Model;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Services
{
    public class MainService
    {
        private List<MenuWindow> openedUsersWindows;
        private SqlConnection sqlConnection;
        private DBService dbService;
        private const int FULL = 8;
        private const int EMPTY = 0;
        private const int INACTIVE = 0;
        private const int WAITING = 1;
        private const int PLAYING = 2;
        private ChatWindow chatWindowIntern;
        private ChatWindow chatWindowJuniorm;
        private ChatWindow chatWindowSenior;
        private TableService internTable;
        private TableService juniorTable;
        private TableService seniorTable;
        private string connectionString;
        // Task internTask, juniorTask, seniorTask;
        public MainService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            dbService = new DBService(new SqlConnection(connectionString));
            openedUsersWindows = new List<MenuWindow>();
            internTable = new TableService(5000, 50, 100, "intern", dbService);
            juniorTable = new TableService(50000, 500, 1000, "junior", dbService);
            seniorTable = new TableService(500000, 5000, 10000, "senior", dbService);
            // chatWindowIntern = new ChatWindow();
            // chatWindowJuniorm = new ChatWindow();
            // chatWindowSenior = new ChatWindow();
        }

        public int OccupiedIntern()
        {
            return internTable.Occupied();
        }

        public int OccupiedJunior()
        {
            return juniorTable.Occupied();
        }

        public int OccupiedSenior()
        {
            return seniorTable.Occupied();
        }

        public void NewUserLogin(User newUser)
        {
            if (DateTime.Now.Date != newUser.UserLastLogin.Date)
            {
                var diffDates = DateTime.Now.Date - newUser.UserLastLogin.Date;
                if (diffDates.Days == 1)
                {
                    newUser.UserStreak++;
                }
                else
                {
                    newUser.UserStreak = 1;
                }
                newUser.UserChips += newUser.UserStreak * 5000;
                dbService.UpdateUserChips(newUser.UserID, newUser.UserChips);
                dbService.UpdateUserStreak(newUser.UserID, newUser.UserStreak);
                MessageBox.Show("Congratulations, you got your daily bonus!\n" + "Streak: " + newUser.UserStreak + " Bonus: " + (5000 * newUser.UserStreak).ToString());
            }
            dbService.UpdateUserLastLogin(newUser.UserID, DateTime.Now);
        }

        public void AddWindow(string username)
        {
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("EXEC getUser @username", sqlConnection);
            command.Parameters.AddWithValue("@username", username);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    int userID = reader.IsDBNull(reader.GetOrdinal("user_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_id"));
                    string userName = reader.IsDBNull(reader.GetOrdinal("user_username")) ? string.Empty : reader.GetString(reader.GetOrdinal("user_username"));
                    int currentFont = reader.IsDBNull(reader.GetOrdinal("user_currentFont")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_currentFont"));
                    int currentTitle = reader.IsDBNull(reader.GetOrdinal("user_currentTitle")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_currentTitle"));
                    string currentIconPath = reader.IsDBNull(reader.GetOrdinal("user_currentIcon")) ? ConfigurationManager.AppSettings["DEFAULT_ICON_PATH"] : dbService.GetIconPath(reader.GetInt32(reader.GetOrdinal("user_currentIcon")));
                    int currentTable = reader.IsDBNull(reader.GetOrdinal("user_currentTable")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_currentTable"));
                    int chips = reader.IsDBNull(reader.GetOrdinal("user_chips")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_chips"));
                    int stack = reader.IsDBNull(reader.GetOrdinal("user_stack")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_stack"));
                    int streak = reader.IsDBNull(reader.GetOrdinal("user_streak")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_streak"));
                    int handsPlayed = reader.IsDBNull(reader.GetOrdinal("user_handsPlayed")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_handsPlayed"));
                    int level = reader.IsDBNull(reader.GetOrdinal("user_level")) ? 0 : reader.GetInt32(reader.GetOrdinal("user_level"));
                    DateTime lastLogin = reader.IsDBNull(reader.GetOrdinal("user_handsPlayed")) ? default(DateTime) : reader.GetDateTime(reader.GetOrdinal("user_lastLogin"));
                    User newUser = new User(userID, userName, currentFont, currentTitle, currentIconPath, currentTable, chips, stack, streak, handsPlayed, level, lastLogin);
                    MenuWindow menuWindow = new MenuWindow(newUser, this);
                    reader.Close();
                    menuWindow.Show();
                    NewUserLogin(newUser);
                    openedUsersWindows.Add(menuWindow);
                }
                else
                {
                    MessageBox.Show("The username is not valid.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            sqlConnection.Close();
        }
        public void DisconnectUser(MenuWindow window)
        {
            User player = window.Player();
            player.UserStatus = INACTIVE;
            player.UserBet = 0;
            internTable.DisconnectUser(window);
            juniorTable.DisconnectUser(window);
            seniorTable.DisconnectUser(window);

            player.UserChips += player.UserStack;
            dbService.UpdateUserChips(player.UserID, player.UserChips);
            player.UserStack = EMPTY;
            dbService.UpdateUserStack(player.UserID, player.UserStack);
        }
        public int JoinInternTable(MenuWindow window)
        {
            return internTable.JoinTable(window, ref sqlConnection);
        }

        public int JoinJuniorTable(MenuWindow window)
        {
            return juniorTable.JoinTable(window, ref sqlConnection);
        }

        public int JoinSeniorTable(MenuWindow window)
        {
            return seniorTable.JoinTable(window, ref sqlConnection);
        }
    }
}
