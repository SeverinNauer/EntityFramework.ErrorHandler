using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.ErrorHandler.PostgreSQL
{
    public static class PostgresExtensions
    {
        public static void AddPostgres(this DbErrorHandlerConfiguration config)
        {
            config.AddConverter(new PostgresConverter());
        }
    }
}
