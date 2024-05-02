using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SuperbetBeclean.Model;
using SuperbetBeclean.Models;

namespace SuperbetBeclean.Services
{
    public class DataBaseService : IDataBaseService
    {
        private SqlConnection connection;
        private const int FIRST_USER_RANK = 1;
        private const int MAXIMUM_VARCHAR_SIZE = -1;
        private const int DEFAULT_VARCHAR_SIZE = 255;
        private const int CONVERSION_ERROR_VALUE = -1;

        public DataBaseService()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
        }

        public DataBaseService(SqlConnection connection)
        {
            this.connection = connection;
        }
        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
        private void ExecuteNonQuery(string procedureName, SqlParameter[] parameters)
        {
            OpenConnection();
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
            CloseConnection();
        }

        public void UpdateUser(int id, string username, int currentFont, int currentTitle, int currentIcon, int currentTable, int chipsCount, int chipStack, int streakCount, int handsPlayed, int level, DateTime lastLogin)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@username", SqlDbType.VarChar) { Value = username },
                new SqlParameter("@currentFont", SqlDbType.Int) { Value = currentFont },
                new SqlParameter("@currentTitle", SqlDbType.Int) { Value = currentTitle },
                new SqlParameter("@currentIcon", SqlDbType.Int) { Value = currentIcon },
                new SqlParameter("@currentTable", SqlDbType.Int) { Value = currentTable },
                new SqlParameter("@chips", SqlDbType.Int) { Value = chipsCount },
                new SqlParameter("@stack", SqlDbType.Int) { Value = chipStack },
                new SqlParameter("@streak", SqlDbType.Int) { Value = streakCount },
                new SqlParameter("@handsPlayed", SqlDbType.Int) { Value = handsPlayed },
                new SqlParameter("@level", SqlDbType.Int) { Value = level },
                new SqlParameter("@lastLogin", SqlDbType.DateTime) { Value = lastLogin }
            };
            ExecuteNonQuery("updateUser", parameters);
        }

        public void UpdateUserFont(int id, int font)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@font", SqlDbType.Int) { Value = font }
            };
            ExecuteNonQuery("updateUserFont", parameters);
        }

        public void UpdateUserTitle(int id, int title)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@title", SqlDbType.Int) { Value = title }
            };
            ExecuteNonQuery("updateUserTitle", parameters);
        }

        public void UpdateUserIcon(int id, int icon)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@icon", SqlDbType.Int) { Value = icon }
            };
            ExecuteNonQuery("updateUserIcon", parameters);
        }

        public void UpdateUserChips(int id, int chips)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@chips", SqlDbType.Int) { Value = chips }
            };
            ExecuteNonQuery("updateUserChips", parameters);
        }

        public void UpdateUserStack(int id, int stack)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@stack", SqlDbType.Int) { Value = stack }
            };
            ExecuteNonQuery("updateUserStack", parameters);
        }

        public void UpdateUserStreak(int id, int streak)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@streak", SqlDbType.Int) { Value = streak }
            };
            ExecuteNonQuery("updateUserStreak", parameters);
        }

        public void UpdateUserHandsPlayed(int id, int handsPlayed)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@handsPlayed", SqlDbType.Int) { Value = handsPlayed }
            };
            ExecuteNonQuery("updateUserHandsPlayed", parameters);
        }

        public void UpdateUserLevel(int id, int level)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@level", SqlDbType.Int) { Value = level }
            };
            ExecuteNonQuery("updateUserLevel", parameters);
        }

        public void UpdateUserLastLogin(int id, DateTime lastLogin)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id },
                new SqlParameter("@lastLogin", SqlDbType.DateTime) { Value = lastLogin }
            };
            ExecuteNonQuery("updateUserLastLogin", parameters);
        }

        public void UpdateChallenge(int challengeId, string description, string rule, int amount, int reward)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@challenge_id", SqlDbType.Int) { Value = challengeId },
                new SqlParameter("@challenge_description", SqlDbType.VarChar, MAXIMUM_VARCHAR_SIZE) { Value = description },
                new SqlParameter("@challenge_rule", SqlDbType.VarChar, MAXIMUM_VARCHAR_SIZE) { Value = rule },
                new SqlParameter("@challenge_amount", SqlDbType.Int) { Value = amount },
                new SqlParameter("@challenge_reward", SqlDbType.Int) { Value = reward }
            };
            ExecuteNonQuery("updateChallenge", parameters);
        }

        public void UpdateFont(int fontId, string fontName, int fontPrice, string fontType)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@font_id", SqlDbType.Int) { Value = fontId },
                new SqlParameter("@font_name", SqlDbType.VarChar, DEFAULT_VARCHAR_SIZE) { Value = fontName },
                new SqlParameter("@font_price", SqlDbType.Int) { Value = fontPrice },
                new SqlParameter("@font_type", SqlDbType.VarChar, DEFAULT_VARCHAR_SIZE) { Value = fontType }
            };
            ExecuteNonQuery("updateFont", parameters);
        }

        public void UpdateIcon(int iconId, string iconName, int iconPrice, string iconPath)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@icon_id", SqlDbType.Int) { Value = iconId },
                new SqlParameter("@icon_name", SqlDbType.VarChar, DEFAULT_VARCHAR_SIZE) { Value = iconName },
                new SqlParameter("@icon_price", SqlDbType.Int) { Value = iconPrice },
                new SqlParameter("@icon_path", SqlDbType.VarChar, DEFAULT_VARCHAR_SIZE) { Value = iconPath }
            };
            ExecuteNonQuery("updateIcon", parameters);
        }

        public void UpdateTitle(int titleId, string titleName, int titlePrice, string titleContent)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@title_id", SqlDbType.Int) { Value = titleId },
                new SqlParameter("@title_name", SqlDbType.VarChar, DEFAULT_VARCHAR_SIZE) { Value = titleName },
                new SqlParameter("@title_price", SqlDbType.Int) { Value = titlePrice },
                new SqlParameter("@title_content", SqlDbType.VarChar, DEFAULT_VARCHAR_SIZE) { Value = titleContent }
            };
            ExecuteNonQuery("updateTitle", parameters);
        }

        public string GetIconPath(int iconId)
        {
                using (SqlCommand command = new SqlCommand("getIconByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@icon_id", SqlDbType.Int) { Value = iconId });

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                    if (reader.Read())
                    {
                        return reader["icon_path"] as string;
                    }
                    else
                    {
                        throw new Exception("No icon found with the provided ID.");
                    }
                    }
            }
        }

        public List<string> GetLeaderboard()
        {
            List<string> leaderboard = new List<string>();
            OpenConnection();
            using (SqlCommand command = new SqlCommand("getLeaderboard", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int rank = FIRST_USER_RANK;
                    while (reader.Read())
                    {
                        string username = reader["user_username"] as string;
                        int chips = Convert.ToInt32(reader["user_chips"]);
                        int level = Convert.ToInt32(reader["user_level"]);
                        leaderboard.Add($"{rank}. {username} - Lvl: {level} - Chips: {chips}");
                        rank++; // Increment rank counter for the next entry
                    }
                }
            }
            return leaderboard;
        }

        public List<ShopItem> GetShopItems()
        {
            List<ShopItem> shopItems = new List<ShopItem>();

            OpenConnection();
            using (SqlCommand command = new SqlCommand("getAllIcons", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int iconId = Convert.ToInt32(reader["icon_id"]);
                        string iconName = reader["icon_name"] as string;
                        int iconPrice = Convert.ToInt32(reader["icon_price"]);
                        string iconPath = reader["icon_path"] as string;

                        // Assuming ShopItem is a class with appropriate properties
                        ShopItem shopItem = new ShopItem(iconId, iconPath, iconName, iconPrice);
                        shopItems.Add(shopItem);
                    }
                }
            }

            return shopItems;
        }

        public List<ShopItem> GetAllUserIconsByUserId(int userId)
        {
            List<ShopItem> userIcons = new List<ShopItem>();

            OpenConnection();
            using (SqlCommand command = new SqlCommand("getAllUserIconsByUserId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@user_id", SqlDbType.Int) { Value = userId });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int iconId = Convert.ToInt32(reader["icon_id"]);
                        string iconName = reader["icon_name"] as string;
                        int iconPrice = Convert.ToInt32(reader["icon_price"]);
                        string iconPath = reader["icon_path"] as string;

                        // Assuming ShopItem is a class with appropriate properties
                        ShopItem shopItem = new ShopItem(iconId, iconPath, iconName, iconPrice);
                        userIcons.Add(shopItem);
                    }
                }
            }

            return userIcons;
        }

        public void CreateUserIcon(int userId, int iconId)
        {
            OpenConnection();
            using (SqlCommand command = new SqlCommand("createUserIcon", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@user_id", SqlDbType.Int) { Value = userId });
                command.Parameters.Add(new SqlParameter("@icon_id", SqlDbType.Int) { Value = iconId });

                command.ExecuteNonQuery();
            }
        }

        public int GetIconIDByIconName(string iconName)
        {
            int iconId = CONVERSION_ERROR_VALUE;

            OpenConnection();
            using (SqlCommand command = new SqlCommand("getIconIDByIconName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@icon_name", SqlDbType.VarChar, DEFAULT_VARCHAR_SIZE) { Value = iconName });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        iconId = Convert.ToInt32(reader["icon_id"]);
                    }
                }
            }

            return iconId;
        }

        public void SetCurrentIcon(int userId, int iconId)
        {
            OpenConnection();
            using (SqlCommand command = new SqlCommand("setCurrentIcon", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@user_id", SqlDbType.Int) { Value = userId });
                command.Parameters.Add(new SqlParameter("@icon_id", SqlDbType.Int) { Value = iconId });

                command.ExecuteNonQuery();
            }
        }
        public List<string> GetAllRequestsByToUserID(int toUser)
        {
            List<string> requests = new List<string>();

            OpenConnection();
            using (SqlCommand command = new SqlCommand("getAllRequestsByToUserID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@toUser", SqlDbType.Int) { Value = toUser });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Extract date from reader and convert it to a DateTime object
                        DateTime requestDate = (DateTime)reader["request_date"];
                        // Format the date to include only the date portion without the time
                        string formattedDate = requestDate.ToShortDateString();

                        int fromUserID = Convert.ToInt32(reader["request_fromUser"]);
                        int toUserID = Convert.ToInt32(reader["request_toUser"]);

                        // Get usernames corresponding to user IDs
                        string fromUserName = GetUserNameByUserId(fromUserID);
                        string toUserName = GetUserNameByUserId(toUserID);

                        string requestInfo = $"From: {fromUserName}, To: {toUserName}, Date: {formattedDate}";
                        requests.Add(requestInfo);
                    }
                }
            }

            return requests;
        }

        public List<Tuple<int, int>> GetAllRequestsByToUserIDSimplified(int toUser)
        {
            List<Tuple<int, int>> requests = new List<Tuple<int, int>>();

            OpenConnection();
            using (SqlCommand command = new SqlCommand("getAllRequestsByToUserID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@toUser", SqlDbType.Int) { Value = toUser });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int fromUserID = Convert.ToInt32(reader["request_fromUser"]);
                        int toUserID = Convert.ToInt32(reader["request_toUser"]);

                        requests.Add(Tuple.Create(fromUserID, toUserID));
                    }
                }
            }

            return requests;
        }
        public void CreateRequest(int fromUser, int toUser)
        {
            OpenConnection();
            using (SqlCommand command = new SqlCommand("createRequest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@fromUser", SqlDbType.Int) { Value = fromUser });
                command.Parameters.Add(new SqlParameter("@toUser", SqlDbType.Int) { Value = toUser });

                command.ExecuteNonQuery();
            }
        }
        public string GetUserNameByUserId(int userId)
        {
            string username = null;

            // Open a new connection
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
            {
                connection.Open();

                // Use a new SqlCommand
                using (SqlCommand command = new SqlCommand("getUserNameByUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = userId });

                    // Execute the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            username = reader["user_username"] as string;
                        }
                    }
                }
            }

            return username;
        }

        private const int NAME_MAX_LENGTH = 128;
        public int GetUserIdByUserName(string username)
        {
            int userId = CONVERSION_ERROR_VALUE;

            OpenConnection();
            using (SqlCommand command = new SqlCommand("getUserIdByUserName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@name", SqlDbType.VarChar, NAME_MAX_LENGTH) { Value = username });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userId = Convert.ToInt32(reader["user_id"]);
                    }
                }
            }

            return userId;
        }
        public int GetChipsByUserId(int userId)
        {
            int chips = CONVERSION_ERROR_VALUE;

            OpenConnection();
            using (SqlCommand command = new SqlCommand("getChipsByUserId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = userId });

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        chips = Convert.ToInt32(reader["user_chips"]);
                    }
                }
            }

            return chips;
        }
        public void DeleteRequestsByUserId(int userId)
        {
            OpenConnection();
            using (SqlCommand command = new SqlCommand("DeleteRequestsByUserId", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@userId", SqlDbType.Int) { Value = userId });

                command.ExecuteNonQuery();
            }
        }
    }
}
