using EcommerceMonolithic.Models;

namespace EcommerceMonolithic.DataAccess
{
    public interface IOrderDetailsProvider
    {
        Task<OrderDetail[]> Get();
    }
}