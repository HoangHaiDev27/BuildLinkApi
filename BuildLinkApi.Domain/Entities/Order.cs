using System;
using System.Collections.Generic;
using BuildLinkApi.Domain.Common;
using BuildLinkApi.Domain.Enums;

namespace BuildLinkApi.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string OrderCode { get; set; } = string.Empty;

        public Guid AccountId { get; set; }

        public Account Account { get; set; } = null!;

        public OrderStatus Status { get; set; } = OrderStatus.Processing;

        public decimal TotalAmount { get; set; }

        public Guid? ShippingAddressId { get; set; }

        public Address? ShippingAddress { get; set; }

        public string? Notes { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
