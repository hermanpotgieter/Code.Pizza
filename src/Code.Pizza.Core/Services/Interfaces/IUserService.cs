using System.Collections.Generic;
using Code.Pizza.Core.Domain;

namespace Code.Pizza.Core.Services.Interfaces
{
    public interface IUserService
    {
        User Create(string email, string name, string surname, string password);

        User Update(User user, string password = null);

        User Logon(string email, string password);

        IEnumerable<User> FindAll();

        User FindById(int id);

        User FindByEmail(string email);
    }
}
