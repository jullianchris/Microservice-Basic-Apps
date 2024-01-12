using System;

namespace Orderservice.Models
{
    public class InventoryResponse
    {
        public int OrderId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
