using System.Threading.Tasks;

namespace Is4.Domain.Repostitory
{
    public interface IUnitOfWork 
    { 
        Task<int> Commit();
    }
}
