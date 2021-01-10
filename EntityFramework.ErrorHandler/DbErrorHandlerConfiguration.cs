using System;
using System.Collections.Generic;

namespace EntityFramework.ErrorHandler
{
    public class DbErrorHandlerConfiguration
    {
        private static readonly Lazy<DbErrorHandlerConfiguration> config =
            new Lazy<DbErrorHandlerConfiguration>(() => new DbErrorHandlerConfiguration());
        public static DbErrorHandlerConfiguration Config => config.Value;
        public List<IDbErrorConverter> Converter { get; } = new List<IDbErrorConverter>();

        public void AddConverter(IDbErrorConverter converter)
        {
            Converter.Add(converter);
        }
    }
}