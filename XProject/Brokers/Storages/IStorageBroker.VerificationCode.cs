using System.Linq;
using System.Threading.Tasks;
using XProject.Models;

namespace XProject.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public ValueTask<VerificationCode> InsertCodeAsync(VerificationCode code);
        public ValueTask<VerificationCode> DeleteCodeAsync(VerificationCode code);
        public IQueryable<VerificationCode> GetAllCodesAsync();
    }
}
