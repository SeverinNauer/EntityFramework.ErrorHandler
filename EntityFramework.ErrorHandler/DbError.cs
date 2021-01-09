using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFramework.ErrorHandler
{
    public interface IDbError
    {
        DbUpdateException UpdateException { get; }

        internal TR Handle<TR>(object handler);
    };

    public record DbError(DbUpdateException UpdateException) : IDbError
    {
        TR IDbError.Handle<TR>(object handler) =>
            handler is Func<DbError, TR> handlerFunc
                ? handlerFunc.Invoke(this)
                : throw new InvalidCastException();
    }

    public record UniqueViolationError(DbUpdateException UpdateException, string? ConstraintName) : IDbError
    {
        TR IDbError.Handle<TR>(object handler) =>
           handler is Func<UniqueViolationError, TR> handlerFunc
               ? handlerFunc.Invoke(this)
               : throw new InvalidCastException();
    }
}