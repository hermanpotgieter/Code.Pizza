using System;
using System.Collections.Generic;
using Code.Pizza.Common.Specifications;
using Code.Pizza.Common.Utilities;
using Code.Pizza.Core.Builders;
using Code.Pizza.Core.Data;
using Code.Pizza.Core.Domain;
using Code.Pizza.Core.Services.Interfaces;

namespace Code.Pizza.Core.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly ICryptoService cryptoService;

        public UserService(IRepository<User> userRepository, ICryptoService cryptoService)
        {
            this.userRepository = userRepository;
            this.cryptoService = cryptoService;
        }

        public User Create(string email, string name, string surname, string password)
        {
            UserBuilder builder = new UserBuilder(this.cryptoService);

            User user = builder
                .SetEmail(email)
                .SetName(name)
                .SetSurname(surname)
                .SetPassword(password)
                .Create();

            User createdUser = this.userRepository.Save(user);

            return createdUser;
        }

        public User Update(User user, string password = null)
        {
            Guard.Against<InvalidOperationException>(() => user.ID == 0, "You may only update existing users. Call Create() to save a new User.");

            if(!string.IsNullOrWhiteSpace(password))
            {
                user.SetPassword(this.cryptoService, password);
            }

            User saved = this.userRepository.Save(user);

            return saved;
        }

        public User Logon(string email, string password)
        {
            AdHocSpecification<User> specification = new AdHocSpecification<User>(x => x.Email == email);
            User user = this.userRepository.FindSingle(specification);

            if(user == null)
            {
                return null;
            }

            bool loggedIn = user.Login(this.cryptoService, password);
            this.userRepository.Save(user);

            return loggedIn ? user : null;
        }

        public IEnumerable<User> FindAll()
        {
            IEnumerable<User> users = this.userRepository.FindAll();

            return users;
        }

        public User FindById(int id)
        {
            User user = this.userRepository.FindById(id);

            return user;
        }

        public User FindByEmail(string email)
        {
            AdHocSpecification<User> specification = new AdHocSpecification<User>(x => x.Email == email);
            User user = this.userRepository.FindSingle(specification);

            return user;
        }
    }
}
