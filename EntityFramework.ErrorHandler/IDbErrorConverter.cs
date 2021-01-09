using Microsoft.EntityFrameworkCore;

namespace EntityFramework.ErrorHandler
{
    public interface IDbErrorConverter
    {
        bool ShouldConvert(DbUpdateException exn);

        IDbError Convert(DbUpdateException exn);
    }
}