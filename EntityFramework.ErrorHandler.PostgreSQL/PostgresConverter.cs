using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

namespace EntityFramework.ErrorHandler.PostgreSQL
{
    public class PostgresConverter : IDbErrorConverter
    {
        public bool ShouldConvert(DbUpdateException exn)
            => exn.GetBaseException() is PostgresException;

        public IDbError Convert(DbUpdateException exn)
        {
            if (exn.GetBaseException() is PostgresException ex)
            {
                return ex.SqlState switch
                {
                    "23505" => new UniqueViolationError(exn, ex.ConstraintName),
                    _ => new DbError(exn)
                };
            }
            throw new InvalidCastException();
        }
    }
}