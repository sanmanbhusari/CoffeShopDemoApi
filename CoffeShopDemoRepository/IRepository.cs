using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeShopDemoRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();

        Task Insert(T entity);

    }
}
