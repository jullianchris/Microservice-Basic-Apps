using EcommerceMonolithic.Models;

namespace EcommerceMonolithic.DataAccess
{
    public interface IInventoryProvider
    {
        Inventory[] Get();
    }
}