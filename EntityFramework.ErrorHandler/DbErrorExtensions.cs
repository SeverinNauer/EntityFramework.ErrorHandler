using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFramework.ErrorHandler
{
    public static class DbErrorExtensions
    {
        public static IDbErrorHandler<TR> ToDbError<TR>(this Exception exn) where TR : class
        {
            return exn is DbUpdateException ex
                ? new DbErrorHandler<TR>(ex, DbErrorHandlerConfiguration.Config)
                : throw new InvalidCastException("The Exception must be a DbUpdateException");
        }

        public static IDbErrorHandler<TR> ToDbError<TR>(this Exception exn, DbErrorHandlerConfiguration dbErrorHandlerSettings) where TR : class
        {
            return exn is DbUpdateException ex
                ? new DbErrorHandler<TR>(ex, dbErrorHandlerSettings)
                : throw new InvalidCastException("The Exception must be a DbUpdateException");
        }
    }
}