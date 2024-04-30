using SuperbetBeclean.Model;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Services
{
    public interface IMainService
    {
        int OccupiedIntern();
        int OccupiedJunior();
        int OccupiedSenior();
        void NewUserLogin(User newUser);
        void AddWindow(string username);
        void DisconnectUser(MenuWindow window);
        int JoinInternTable(MenuWindow window);
        int JoinJuniorTable(MenuWindow window);
        int JoinSeniorTable(MenuWindow window);
    }
}