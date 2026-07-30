using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Orders
{
    public class Order : BaseEntity<Guid>
    {
        private Order() { }

        public Order(
            string buyerEmail,ICollection<OrderItem> items, OrderAddress shipToAddress, DeliveryMethod deliveryMethod,
            decimal subTotal,
            string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            Items = items;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            DeliveryMethodId = deliveryMethod.Id;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string PaymentIntentId { get; set; } = default!;
        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public ICollection<OrderItem> Items { get; set; } = [];
        public OrderAddress ShipToAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal SubTotal { get; set; }

        public decimal GetTotal() => SubTotal + (DeliveryMethod?.Cost ?? 0m);

    }
}
