﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using SuperbetBeclean.Model;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Services
{
    public class MainService : IMainService
    {
        private List<MenuWindow> openedUsersWindows;
        private SqlConnection sqlConnection;
        private DataBaseService databaseService;
        private const int FULL = 8;
        private const int EMPTY = 0;
        private const int INACTIVE = 0;
        private const int WAITING = 1;
        private const int PLAYING = 2;
        private const string JUNIOR = "junior";
        private const string INTERN = "intern";
        private const string SENIOR = "senior";
        private ChatWindow chatWindowIntern;
        private ChatWindow chatWindowJuniorm;
        private ChatWindow chatWindowSenior;
        private TableService internTable;
        private TableService juniorTable;
        private TableService seniorTable;
        private string connectionString;

        private const int INTERN_BUY_IN = 500;
        private const int JUNIOR_BUY_IN = 5000;
        private const int SENIOR_BUY_IN = 50000;

        private const int INTERN_SMALL_BLIND = 50;
        private const int INTERN_BIG_BLIND = 100;

        private const int JUNIOR_SMALL_BLIND = 500;
        private const int JUNIOR_BIG_BLIND = 1000;

        private const int SENIOR_SMALL_BLIND = 5000;
        private const int SENIOR_BIG_BLIND = 10000;

        private const int DAILY_LOGIN_STREAK_MULTIPLIER = 5000;

        private const int INITIAL_STREAK = 1;
        private const int DAYS_BETWEEN_LOGIN_BONUSES = 1;

        // Task internTask, juniorTask, seniorTask;
        public MainService()
        {
            connectionString = "Data Source= DESKTOP-F6HM4JS; Initial Catalog = Team42; Integrated Security = True;";
            sqlConnection = new SqlConnection(connectionString);
            databaseService = new DataBaseService(new SqlConnection(connectionString));
            openedUsersWindows = new List<MenuWindow>();
            internTable = new TableService(INTERN_BUY_IN, INTERN_SMALL_BLIND, INTERN_BIG_BLIND, INTERN, databaseService);
            juniorTable = new TableService(JUNIOR_BUY_IN, JUNIOR_SMALL_BLIND, JUNIOR_BIG_BLIND, JUNIOR, databaseService);
            seniorTable = new TableService(SENIOR_BUY_IN, SENIOR_SMALL_BLIND, SENIOR_BIG_BLIND, SENIOR, databaseService);
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
                if (diffDates.Days == DAYS_BETWEEN_LOGIN_BONUSES)
                {
                    newUser.UserStreak++;
                }
                else
                {
                    newUser.UserStreak = INITIAL_STREAK;
                }
                newUser.UserChips += newUser.UserStreak * DAILY_LOGIN_STREAK_MULTIPLIER;
                databaseService.UpdateUserChips(newUser.UserID, newUser.UserChips);
                databaseService.UpdateUserStreak(newUser.UserID, newUser.UserStreak);
                MessageBox.Show("Congratulations, you got your daily bonus!\n" + "Streak: " + newUser.UserStreak + " Bonus: " + (DAILY_LOGIN_STREAK_MULTIPLIER * newUser.UserStreak).ToString());
            }
            databaseService.UpdateUserLastLogin(newUser.UserID, DateTime.Now);
        }
        public int GetIntFromReader(SqlDataReader reader, string columnName)
        {
            const int DEFAULT_VALUE = 0;
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? DEFAULT_VALUE : reader.GetInt32(reader.GetOrdinal(columnName));
        }
        public string GetStringFromReader(SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? string.Empty : reader.GetString(reader.GetOrdinal(columnName));
        }
        public DateTime GetDateFromReader(SqlDataReader reader, string columnName)
        {
            return reader.IsDBNull(reader.GetOrdinal(columnName)) ? default : reader.GetDateTime(reader.GetOrdinal(columnName));
        }

        public User CreateUserFromReader(SqlDataReader reader)
        {
            int userID = GetIntFromReader(reader, "user_id");
            string userName = GetStringFromReader(reader, "user_username");
            int currentFont = GetIntFromReader(reader, "user_currentFont");
            int currentTitle = GetIntFromReader(reader, "user_currentTitle");
            string currentIconPath = reader.IsDBNull(reader.GetOrdinal("user_currentIcon")) ? ConfigurationManager.AppSettings["DEFAULT_ICON_PATH"] : databaseService.GetIconPath(reader.GetInt32(reader.GetOrdinal("user_currentIcon")));
            int currentTable = GetIntFromReader(reader, "user_currentTable");
            int chips = GetIntFromReader(reader, "user_chips");
            int stack = GetIntFromReader(reader, "user_stack");
            int streak = GetIntFromReader(reader, "user_streak");
            int handsPlayed = GetIntFromReader(reader, "user_handsPlayed");
            int level = GetIntFromReader(reader, "user_level");
            DateTime lastLogin = GetDateFromReader(reader, "user_lastLogin");
            return new User(userID, userName, currentFont, currentTitle, currentIconPath, currentTable, chips, stack, streak, handsPlayed, level, lastLogin);
        }
        private void OpenUserWindow(User user)
        {
            MenuWindow menuWindow = new MenuWindow(user, this);
            menuWindow.Show();
            openedUsersWindows.Add(menuWindow);
        }
        public User FetchUser(SqlConnection sqlConnection, string userName)
        {
            using (SqlCommand command = new SqlCommand("EXEC getUser @username", sqlConnection))
            {
                command.Parameters.AddWithValue("@username", userName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        User newUser = CreateUserFromReader(reader);
                        reader.Close();
                        return newUser;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public void AddWindow(string username)
        {
            sqlConnection.Open();
            User user = FetchUser(sqlConnection, username);
            try
            {
                if (user != null)
                {
                    OpenUserWindow(user);
                    NewUserLogin(user);
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
            databaseService.UpdateUserChips(player.UserID, player.UserChips);
            player.UserStack = EMPTY;
            databaseService.UpdateUserStack(player.UserID, player.UserStack);
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
