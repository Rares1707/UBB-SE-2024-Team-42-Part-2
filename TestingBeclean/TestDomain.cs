using SuperbetBeclean.Model;
using SuperbetBeclean.Models;
using SuperbetBeclean.Services;


namespace TestingBeclean
{
    public class TestsDomain
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestIconGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Icon icon = new Icon();
            Icon icon1 = new Icon();
            Assert.That(icon.IconID, Is.EqualTo(icon1.IconID));
        }
        [Test]
        public void TestIconSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Icon icon = new Icon();
            icon.IconID = 1;
            Assert.That(icon.IconID, Is.EqualTo(1));
        }

        [Test]
        public void TestIconNameGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Icon icon = new Icon();
            Icon icon1 = new Icon();
            Assert.That(icon.IconName, Is.EqualTo(icon1.IconName));
        }
        [Test]
        public void TestIconNameSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Icon icon = new Icon();
            icon.IconName = "icon1";
            Assert.That(icon.IconName, Is.EqualTo("icon1"));
        }
        [Test]
        public void TestIconPriceGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Icon icon = new Icon();
            Icon icon1 = new Icon();
            Assert.That(icon.IconPrice, Is.EqualTo(icon1.IconPrice));
        }
        [Test]
        public void TestIconPriceSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Icon icon = new Icon();
            icon.IconPrice = 1;
            Assert.That(icon.IconPrice, Is.EqualTo(1));
        }

        [Test]
        public void TestIconPathGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Icon icon = new Icon();
            Icon icon1 = new Icon();
            Assert.That(icon.IconPath, Is.EqualTo(icon1.IconPath));
        }

        [Test]
        public void TestIconPathSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Icon icon = new Icon();
            icon.IconPath = "iconPath";
            Assert.That(icon.IconPath, Is.EqualTo("iconPath"));
        }

        [Test]
        public void TestFontGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Font font = new Font();
            Font font1 = new Font();
            Assert.That(font.FontID, Is.EqualTo(font1.FontID));
        }
        [Test]
        public void TestFontSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Font font = new Font();
            font.FontID = 1;
            Assert.That(font.FontID, Is.EqualTo(1));
        }
        [Test]
        public void TestFontNameGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Font font = new Font();
            Font font1 = new Font();
            Assert.That(font.FontName, Is.EqualTo(font1.FontName));
        }

        [Test]
        public void TestFontNameSetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Font font = new Font();
            font.FontName = "font1";
            Assert.That(font.FontName, Is.EqualTo("font1"));
        }

        [Test]
        public void TestFontPriceGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Font font = new Font();
            Font font1 = new Font();
            Assert.That(font.FontPrice, Is.EqualTo(font1.FontPrice));
        }

        [Test]
        public void TestFontPriceSetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Font font = new Font();
            font.FontPrice = 1;
            Assert.That(font.FontPrice, Is.EqualTo(1));
        }

        [Test]
        public void TestFontTypeGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Font font = new Font();
            Font font1 = new Font();
            Assert.That(font.FontType, Is.EqualTo(font1.FontType));
        }

        [Test]
        public void TestFontTypeSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Font font = new Font();
            font.FontType = "fontType";
            Assert.That(font.FontType, Is.EqualTo("fontType"));
        }

        [Test]
        public void TestChallengeGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeID, Is.EqualTo(challenge1.ChallengeID));
        }
        [Test]
        public void TestChallengeSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeID = 1;
            Assert.That(challenge.ChallengeID, Is.EqualTo(1));
        }

        [Test]
        public void TestChallengeDescription_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeDescription, Is.EqualTo(challenge1.ChallengeDescription));
        }

        [Test]

        public void TestChallengeDescriptionSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeDescription = "challenge1";
            Assert.That(challenge.ChallengeDescription, Is.EqualTo("challenge1"));
        }

        [Test]
        public void TestChallengeRuleGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeRule, Is.EqualTo(challenge1.ChallengeRule));
        }

        [Test]
        public void TestChallengeRuleSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeRule = "challengeRule";
            Assert.That(challenge.ChallengeRule, Is.EqualTo("challengeRule"));
        }

        [Test]
        public void TestChallengeAmountGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeAmount, Is.EqualTo(challenge1.ChallengeAmount));
        }

        [Test]
        public void TestChallengeAmountSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeAmount = 1;
            Assert.That(challenge.ChallengeAmount, Is.EqualTo(1));
        }

        [Test]
        public void TestChallengeRewardGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeReward, Is.EqualTo(challenge1.ChallengeReward));
        }

        [Test]
        public void TestChallengeRewardSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeReward = 1;
            Assert.That(challenge.ChallengeReward, Is.EqualTo(1));
        }

        [Test]
        public void TestShopItem_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            int Id = 1;
            string ImagePath = "imagePath";
            string Name = "name";
            int Price = 1;

            ShopItem shopItem = new ShopItem(Id, ImagePath, Name, Price);

            Assert.That(shopItem.Id, Is.EqualTo(Id));
            Assert.That(shopItem.ImagePath, Is.EqualTo(ImagePath));
            Assert.That(shopItem.Name, Is.EqualTo(Name));
            Assert.That(shopItem.Price, Is.EqualTo(Price));
        }

        [Test]
        public void TestTableGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Table table = new Table();
            Table table1 = new Table();
            Assert.That(table.TableID, Is.EqualTo(table1.TableID));
        }

        [Test]
        public void TestTableSetter__SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Table table = new Table();
            table.TableID = 1;
            Assert.That(table.TableID, Is.EqualTo(1));
        }

        [Test]
        public void TestTableNameGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Table table = new Table();
            Table table1 = new Table();
            Assert.That(table.TableName, Is.EqualTo(table1.TableName));
        }

        [Test]
        public void TestTableNameSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Table table = new Table();
            table.TableName = "tableName";
            Assert.That(table.TableName, Is.EqualTo("tableName"));
        }
        [Test]
        public void TestTableBuyInGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Table table = new Table();
            Table table1 = new Table();
            Assert.That(table.TableBuyIn, Is.EqualTo(table1.TableBuyIn));
        }
        [Test]
        public void TestTableBuyInSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Table table = new Table();
            table.TableBuyIn = 1;
            Assert.That(table.TableBuyIn, Is.EqualTo(1));
        }
        [Test]
        public void TestTablePlayerLimitGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Table table = new Table();
            Table table1 = new Table();
            Assert.That(table.TablePlayerLimit, Is.EqualTo(table1.TablePlayerLimit));
        }

        [Test]
        public void TestTablePlayerLimitSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Table table = new Table();
            table.TablePlayerLimit = 1;
            Assert.That(table.TablePlayerLimit, Is.EqualTo(1));
        }

        [Test]
        public void TestTitleIdGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Title table = new Title();
            Title table1 = new Title();
            Assert.That(table.TitleID, Is.EqualTo(table1.TitleID));
        }
        [Test]
        public void TestTitleIdSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Title table = new Title();
            table.TitleID = 1;
            Assert.That(table.TitleID, Is.EqualTo(1));
        }

        [Test]
        public void TestTitleNameGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Title table = new Title();
            Title table1 = new Title();
            Assert.That(table.TitleName, Is.EqualTo(table1.TitleName));
        }

        [Test]
        public void TestTitleNameSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Title table = new Title();
            table.TitleName = "titleName";
            Assert.That(table.TitleName, Is.EqualTo("titleName"));
        }

        [Test]
        public void TestTitlePriceGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Title table = new Title();
            Title table1 = new Title();
            Assert.That(table.TitlePrice, Is.EqualTo(table1.TitlePrice));
        }
        [Test]
        public void TestTitlePriceSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Title table = new Title();
            table.TitlePrice = 1;
            Assert.That(table.TitlePrice, Is.EqualTo(1));
        }
        [Test]
        public void TestTitleContentGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Title table = new Title();
            Title table1 = new Title();
            Assert.That(table.TitleContent, Is.EqualTo(table1.TitleContent));
        }
        [Test]
        public void TestTitleContentSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Title table = new Title();
            table.TitleContent = "titleContent";
            Assert.That(table.TitleContent, Is.EqualTo("titleContent"));
        }

        public void TestCardDeck_GetDeckSize_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            CardDeck cardDeck = new CardDeck();
            Assert.That(cardDeck.GetDeckSize(), Is.EqualTo(52));
        }
        [Test]
        public void TestCardDeck_RemoveCardFromIndex_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            CardDeck cardDeck = new CardDeck();
            cardDeck.RemoveCardFromIndex(0);
            Assert.That(cardDeck.GetDeckSize(), Is.EqualTo(51));
        }

        [Test]
        public void TestChallengeIDGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeID, Is.EqualTo(challenge1.ChallengeID));
        }
        [Test]
        public void TestChallengeIDSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeID = 1;
            Assert.That(challenge.ChallengeID, Is.EqualTo(1));
        }
        
        [Test]
        public void TestChallengeDescriptionGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeDescription, Is.EqualTo(challenge1.ChallengeDescription));
        }
        [Test]
        public void TestChallengeDescriptionSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeDescription = "challengeDescription";
            Assert.That(challenge.ChallengeDescription, Is.EqualTo("challengeDescription"));
        }
        [Test]
        public void TestChallengeRuleGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeRule, Is.EqualTo(challenge1.ChallengeRule));
        }
        [Test]
        public void TestChallengeRuleSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeRule = "challengeRule";
            Assert.That(challenge.ChallengeRule, Is.EqualTo("challengeRule"));
        }
        [Test]
        public void TestChallengeAmountGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeAmount, Is.EqualTo(challenge1.ChallengeAmount));
        }
        [Test]
        public void TestChallengeAmountSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeAmount = 1;
            Assert.That(challenge.ChallengeAmount, Is.EqualTo(1));
        }
        [Test]
        public void TestChallengeRewardGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Challenge challenge = new Challenge();
            Challenge challenge1 = new Challenge();
            Assert.That(challenge.ChallengeReward, Is.EqualTo(challenge1.ChallengeReward));
        }
        [Test]
        public void TestChallengeRewardSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            Challenge challenge = new Challenge();
            challenge.ChallengeReward = 1;
            Assert.That(challenge.ChallengeReward, Is.EqualTo(1));
        }
      
        [Test]
        public void TestPlayingCardValueGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            PlayingCard playingCard = new PlayingCard("value", "suit");
            PlayingCard playingCard1 = new PlayingCard("value", "suit");
            Assert.That(playingCard.Value, Is.EqualTo(playingCard1.Value));
        }
        [Test]
        public void TestPlayingCardValueSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            PlayingCard playingCard = new PlayingCard("value", "suit");
            playingCard.Value = "value";
            Assert.That(playingCard.Value, Is.EqualTo("value"));
        }
        [Test]
        public void TestPlayingCardSuitGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            PlayingCard playingCard = new PlayingCard("value", "suit");
            PlayingCard playingCard1 = new PlayingCard("value", "suit");
            Assert.That(playingCard.Suit, Is.EqualTo(playingCard1.Suit));
        }
        [Test]
        public void TestPlayingCardSuitSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            PlayingCard playingCard = new PlayingCard("value", "suit");
            playingCard.Suit = "suit";
            Assert.That(playingCard.Suit, Is.EqualTo("suit"));
        }
        
        [Test]
        public void TestUserIDGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserID, Is.EqualTo(user1.UserID));
        }
        [Test]
        public void TestUserIDSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserID = 1;
            Assert.That(user.UserID, Is.EqualTo(1));
        }
        [Test]
        public void TestUserNameGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserName, Is.EqualTo(user1.UserName));
        }
        [Test]
        public void TestUserNameSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserName = "userName";
            Assert.That(user.UserName, Is.EqualTo("userName"));
        }
        [Test]
        public void TestUserCurrentFontGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserCurrentFont, Is.EqualTo(user1.UserCurrentFont));
        }
        [Test]
        public void TestUserCurrentFontSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserCurrentFont = 1;
            Assert.That(user.UserCurrentFont, Is.EqualTo(1));
        }

        [Test]
        public void TestUserCurrentTitleGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserCurrentTitle, Is.EqualTo(user1.UserCurrentTitle));
        }
        [Test]
        public void TestUserCurrentTitleSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserCurrentTitle = 2;
            Assert.That(user.UserCurrentTitle, Is.EqualTo(2));
        }
        [Test]
        public void TestUserCurrentIconPathGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserCurrentIconPath, Is.EqualTo(user1.UserCurrentIconPath));
        }
        [Test]
        public void TestUserCurrentIconPathSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserCurrentIconPath = "userCurrentIconPath";
            Assert.That(user.UserCurrentIconPath, Is.EqualTo("userCurrentIconPath"));
        }
        [Test]
        public void TestUserCurrentTableGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserCurrentTable, Is.EqualTo(user1.UserCurrentTable));
        }
        [Test]
        public void TestUserCurrentTableSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserCurrentTable = 3;
            Assert.That(user.UserCurrentTable, Is.EqualTo(3));
        }
        [Test]
        public void TestUserChipsGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserChips, Is.EqualTo(user1.UserChips));
        }
        [Test]
        public void TestUserChipsSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserChips = 4;
            Assert.That(user.UserChips, Is.EqualTo(4));
        }
        [Test]
        public void TestUserStackGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserStack, Is.EqualTo(user1.UserStack));
        }
        [Test]
        public void TestUserStackSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserStack = 5;
            Assert.That(user.UserStack, Is.EqualTo(5));
        }
        [Test]
        public void TestUserStreakGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserStreak, Is.EqualTo(user1.UserStreak));
        }
        [Test]
        public void TestUserStreakSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserStreak = 6;
            Assert.That(user.UserStreak, Is.EqualTo(6));
        }
        [Test]
        public void TestUserHandsPlayedGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserHandsPlayed, Is.EqualTo(user1.UserHandsPlayed));
        }
        [Test]
        public void TestUserHandsPlayedSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserHandsPlayed = 7;
            Assert.That(user.UserHandsPlayed, Is.EqualTo(7));
        }
        [Test]
        public void TestUserLevelGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserLevel, Is.EqualTo(user1.UserLevel));
        }
        [Test]
        public void TestUserLevelSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserLevel = 8;
            Assert.That(user.UserLevel, Is.EqualTo(8));
        }
        [Test]
        public void TestUserLastLogInGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserLastLogin, Is.EqualTo(user1.UserLastLogin));
        }
        [Test]
        public void TestUserLastLogInSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            DateTime current_time = DateTime.Now;
            User user = new User();
            user.UserLastLogin = current_time;
            Assert.That(user.UserLastLogin, Is.EqualTo(current_time));

        }

        [Test]
        public void TestUserStatusGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserStatus, Is.EqualTo(user1.UserStatus));
        }
        [Test]
        public void TestUserStatusSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserStatus = 1;
            Assert.That(user.UserStatus, Is.EqualTo(1));
        }
        [Test]
        public void TestUserBetGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserBet, Is.EqualTo(user1.UserBet));
        }
        [Test]
        public void TestUserBetSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserBet = 2;
            Assert.That(user.UserBet, Is.EqualTo(2));
        }
        [Test]
        public void TestUserCurrentHandGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserCurrentHand, Is.EqualTo(user1.UserCurrentHand));
        }
        [Test]
        public void TestUserCurrentHandSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserCurrentHand = new PlayingCard[2];
            Assert.That(user.UserCurrentHand, Is.EqualTo(new PlayingCard[2]));
        }
        [Test]
        public void TestUserTablePlaceGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            User user = new User();
            User user1 = new User();
            Assert.That(user.UserTablePlace, Is.EqualTo(user1.UserTablePlace));
        }
        [Test]
        public void TestUserTablePlaceSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            User user = new User();
            user.UserTablePlace = 3;
            Assert.That(user.UserTablePlace, Is.EqualTo(3));
        }











    } 
}
