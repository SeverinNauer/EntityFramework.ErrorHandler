using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.ErrorHandler
{
    public interface IDbErrorHandler<TR> where TR : class
    {
        IDbErrorHandler<TR> Catch<T>(Func<T, TR> handle) where T : IDbError;

        TR Handle(Func<DbError, TR> handleElse);
    }

    public class DbErrorHandler<TR> : IDbErrorHandler<TR> where TR : class
    {
        private readonly DbUpdateException exception;
        private readonly IDictionary<Type, object> handlers;
        private readonly DbErrorHandlerConfiguration errorHandlerConfig;

        public DbErrorHandler(DbUpdateException exception, DbErrorHandlerConfiguration errorHandlerConfig)
        {
            this.exception = exception;
            handlers = new Dictionary<Type, object>();
            this.errorHandlerConfig = errorHandlerConfig;
        }

        public TR Handle(Func<DbError, TR> handleElse)
        {
            if (errorHandlerConfig.Converter.Count == 0)
                throw new InvalidOperationException("No converter registered");

            var error =
                    errorHandlerConfig.Converter
                    .FirstOrDefault(t => t.ShouldConvert(exception))?
                    .Convert(exception) ?? throw new InvalidOperationException("No suitable converter found for this 'DbUpdateException'");

            object handler = handlers[error.GetType()] ?? handleElse;
            return error.Handle<TR>(handler);
        }

        public IDbErrorHandler<TR> Catch<T>(Func<T, TR> handle) where T : IDbError
        {
            handlers.Add(typeof(T), handle);
            return this;
        }
    }
}