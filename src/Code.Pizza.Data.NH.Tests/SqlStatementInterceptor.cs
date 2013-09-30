using System;
using NHibernate;
using NHibernate.SqlCommand;

namespace Code.Pizza.Data.NH.Tests
{
    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Console.WriteLine(sql.ToString());

            return sql;
        }
    }
}
