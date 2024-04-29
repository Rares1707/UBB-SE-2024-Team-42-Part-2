using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbetBeclean.Model
{
    public class Challenge
    {
        private int challengeID;
        private string challengeDescription;
        private string challengeRule;
        private int challengeAmount;
        private int challengeReward;

        public Challenge(int challengeID = 0,  string challengeDescription = "", string challengeRule = "", int challengeAmount = 0, int challengeReward = 0)
        {
            this.challengeID = challengeID;
            this.challengeDescription = challengeDescription;
            this.challengeRule = challengeRule;
            this.challengeAmount = challengeAmount;
            this.challengeReward = challengeReward;
        }

        public int ChallengeID
        {
            get { return challengeID; } set { challengeID = value; }
        }
        public string ChallengeDescription
        {
            get { return challengeDescription; } set { challengeDescription = value; }
        }
        public string ChallengeRule
        {
            get { return challengeRule; } set { challengeRule = value; }
        }
        public int ChallengeAmount
        {
            get { return challengeAmount; } set { challengeAmount = value; }
        }
        public int ChallengeReward
        {
            get { return challengeReward; } set { challengeReward = value; }
        }
    }
}
