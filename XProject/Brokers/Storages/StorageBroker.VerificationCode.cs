using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using XProject.Models;

namespace XProject.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<VerificationCode> VerificationCodes { get; set; }

        public ValueTask<VerificationCode> InsertCodeAsync(VerificationCode code) =>
            InsertAsync(code);

        public ValueTask<VerificationCode> DeleteCodeAsync(VerificationCode code) =>
            DeleteAsync(code);

        public IQueryable<VerificationCode> GetAllCodesAsync() =>
            SelectAll<VerificationCode>();
    }
}
