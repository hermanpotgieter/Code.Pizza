using System;
using Code.Pizza.Common.Utilities;
using Code.Pizza.Core.Domain;
using Code.Pizza.Core.Services.Interfaces;

namespace Code.Pizza.Core.Builders
{
    /// <summary>
    ///     Enforces the minimum data required to create a valid instance of <see cref="User" />
    /// </summary>
    public class UserBuilder : Builder<User>
    {
        private readonly ICryptoService cryptoService;

        private string setEmail;
        private string setName;
        private string setSurname;
        private string setPassword;

        public UserBuilder(ICryptoService cryptoService)
        {
            this.cryptoService = cryptoService;
        }

        public override User Create()
        {
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(this.setEmail), "Cannot create a User without an Email address.");
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(this.setName), "Cannot create a User without a Name.");
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(this.setSurname), "Cannot create a User without a Surname.");
            Guard.Against<InvalidOperationException>(string.IsNullOrEmpty(this.setPassword), "Cannot create a User without a Password.");

            User user = new User
                        {
                            Email = this.setEmail,
                            Name = this.setName,
                            Surname = this.setSurname,
                        };

            user.SetPassword(this.cryptoService, this.setPassword);

            return user;
        }

        public UserBuilder SetEmail(string email)
        {
            Guard.AgainstNull(() => email);

            this.setEmail = email;

            return this;
        }

        public UserBuilder SetName(string name)
        {
            Guard.AgainstNull(() => name);

            this.setName = name;

            return this;
        }

        public UserBuilder SetSurname(string surname)
        {
            Guard.AgainstNull(() => surname);

            this.setSurname = surname;

            return this;
        }

        public UserBuilder SetPassword(string password)
        {
            Guard.AgainstNull(() => password);

            this.setPassword = password;

            return this;
        }
    }
}
