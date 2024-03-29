﻿using Newtonsoft.Json;
using EcommerceMonolithic.Models;

namespace EcommerceMonolithic.DataAccess
{
    public class OrderDetailsProvider : IOrderDetailsProvider
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderDetailsProvider> logger;

        public OrderDetailsProvider(IHttpClientFactory httpClientFactory,
            ILogger<OrderDetailsProvider> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<OrderDetail[]> Get()
        {
            try
            {
                using var client = httpClientFactory.CreateClient("order");
                var response = await client.GetAsync("/api/order");
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderDetail[]>(data);
            }
            catch (System.Exception exc)
            {
                // Log the exception
                logger.LogError($"Error getting order details {exc}");
                return Array.Empty<OrderDetail>();
            }
        }
    }
}