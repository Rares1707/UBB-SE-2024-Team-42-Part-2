using System;

namespace SuperbetBeclean.Model
{
    public class User
    {
        private int userID;
        private string userName;
        private int userCurrentFont;
        private int userCurrentTitle;
        private string userCurrentIconPath;
        private int userCurrentTable;
        private int userChips;
        private int userStack;
        private int userStreak;
        private int userHandsPlayed;
        private int userLevel;
        private int userStatus; // Inactive, Waiting, Playing
        private int userBet;
        private int userTablePlace;
        private DateTime userLastLogin;

        private PlayingCard[] userCurrentHand;

        public User(int userID = 0, string userName = "", int userCurrentFont = 0, int userCurrentTitle = 0, string userCurrentIconPath = "", int userCurrentTable = 0, int userChips = 0, int userStack = 0, int userStreak = 0, int userHandsPlayed = 0, int userLevel = 0, DateTime userLastLogin = default(DateTime))
        {
            this.userID = userID;
            this.userName = userName;
            this.userCurrentFont = userCurrentFont;
            this.userCurrentTitle = userCurrentTitle;
            this.userCurrentIconPath = userCurrentIconPath;
            this.userCurrentTable = userCurrentTable;
            this.userChips = userChips;
            this.userStack = userStack;
            this.userStreak = userStreak;
            this.userHandsPlayed = userHandsPlayed;
            this.userLevel = userLevel;
            this.userLastLogin = userLastLogin;
            userStatus = 0;
            userBet = 0;
            userCurrentHand = new PlayingCard[2];
            userTablePlace = 0;
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public int UserCurrentFont
        {
            get { return userCurrentFont; }
            set { userCurrentFont = value; }
        }
        public int UserCurrentTitle
        {
            get { return userCurrentTitle; }
            set { userCurrentTitle = value; }
        }
        public string UserCurrentIconPath
        {
            get { return userCurrentIconPath; }
            set { userCurrentIconPath = value; }
        }
        public int UserCurrentTable
        {
            get { return userCurrentTable; }
            set { userCurrentTable = value; }
        }
        public int UserChips
        {
            get { return userChips; }
            set { userChips = value; }
        }
        public int UserStack
        {
            get { return userStack; }
            set { userStack = value; }
        }
        public int UserStreak
        {
            get { return userStreak; }
            set { userStreak = value; }
        }
        public int UserHandsPlayed
        {
            get { return userHandsPlayed; }
            set { userHandsPlayed = value; }
        }
        public int UserLevel
        {
            get { return userLevel; }
            set { userLevel = value; }
        }
        public DateTime UserLastLogin
        {
            get { return userLastLogin; }
            set { userLastLogin = value; }
        }
        public int UserStatus
        {
            get { return userStatus; }
            set { userStatus = value; }
        }
        public int UserBet
        {
            get { return userBet; }
            set { userBet = value; }
        }
        public int UserTablePlace
        {
            get { return userTablePlace; }
            set { userTablePlace = value; }
        }
        public PlayingCard[] UserCurrentHand
        {
            get { return userCurrentHand; }
            set { userCurrentHand = value; }
        }
    }
}
