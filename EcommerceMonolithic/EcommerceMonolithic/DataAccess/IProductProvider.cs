using EcommerceMonolithic.Models;

namespace EcommerceMonolithic.DataAccess
{
    public interface IProductProvider
    {
        Product[] Get();
    }
}