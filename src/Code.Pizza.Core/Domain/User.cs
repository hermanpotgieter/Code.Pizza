using Code.Pizza.Common.Utilities;
using Code.Pizza.Core.Abstractions;
using Code.Pizza.Core.Services.Interfaces;

namespace Code.Pizza.Core.Domain
{
    public class User : AggregateRoot
    {
        protected internal virtual string PasswordSalt { get; set; }

        protected internal virtual string PasswordHash { get; set; }

        public virtual string Email { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual bool Login(ICryptoService cryptoService, string password)
        {
            Guard.AgainstNull(() => cryptoService);
            Guard.AgainstNullOrWhiteSpaceString(() => password);

            bool loggedIn = cryptoService.VerifyPassword(this.PasswordSalt, this.PasswordHash, password);

            return loggedIn;
        }

        public virtual void SetPassword(ICryptoService cryptoService, string password)
        {
            Guard.AgainstNull(() => cryptoService);
            Guard.AgainstNullOrWhiteSpaceString(() => password);

            this.PasswordSalt = cryptoService.GenerateSalt(password);
            this.PasswordHash = cryptoService.ComputeHash(this.PasswordSalt, password);
        }
    }
}
