using System.Collections.Generic;

namespace EntityFramework.ErrorHandler
{
    public class DbErrorHandlerConfiguration
    {
        public static DbErrorHandlerConfiguration Config => new DbErrorHandlerConfiguration();
        internal List<IDbErrorConverter> Converter { get; } = new List<IDbErrorConverter>();

        public void AddConverter(IDbErrorConverter converter)
        {
            Converter.Add(converter);
        }
    }
}