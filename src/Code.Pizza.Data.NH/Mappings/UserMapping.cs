using Code.Pizza.Core.Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace Code.Pizza.Data.NH.Mappings
{
    public class UserMapping : ClassMapping<User>
    {
        public UserMapping()
        {
            this.Table("Users");

            this.Id(x => x.ID, mapper => mapper.Generator(Generators.Identity));
            this.Version(x => x.Version, mapper => mapper.Type(new Int32Type()));

            this.Property("PasswordSalt", mapper => mapper.Access(Accessor.Property));
            this.Property("PasswordHash", mapper => mapper.Access(Accessor.Property));

            this.Property(x => x.Email, mapper => mapper.Length(50));
            this.Property(x => x.Name, mapper => mapper.Length(50));
            this.Property(x => x.Surname, mapper => mapper.Length(50));
        }
    }
}
