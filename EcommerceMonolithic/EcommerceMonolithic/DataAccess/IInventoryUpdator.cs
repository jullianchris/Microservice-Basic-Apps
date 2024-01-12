using System.Threading.Tasks;

namespace EcommerceMonolithic.DataAccess
{
    public interface IInventoryUpdator
    {
        Task Update(int productId, int quantity);
    }
}