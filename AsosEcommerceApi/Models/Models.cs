using System;
using System.Collections.Generic;

namespace AsosEcommerceApi.Models
{
    // =========================
    // USER & AUTH
    // =========================
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisteredAt { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }

    // =========================
    // ADDRESS
    // =========================
    public class Address
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    // =========================
    // CATALOG
    // =========================
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }

    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


    }

    public class Color
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    // =========================
    // PRODUCT
    // =========================
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public decimal Price { get; set; }
    }


    public class ProductVariation
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Stock { get; set; }
    }
    // =========================
    // CART
    // =========================
    public class Cart
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<CartItem> Items { get; set; }
    }

    public class CartItem
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }
        public Guid ProductVariationId { get; set; }

        public int Quantity { get; set; }
    }

    // =========================
    // ORDER
    // =========================
    public class Order
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }

        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } // Pending, Paid, Shipped

        public DateTime CreatedAt { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }

    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Guid ProductVariationId { get; set; }

        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
    }

    // =========================
    // PAYMENT (RAZORPAY)
    // =========================
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpaySignature { get; set; }

        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } // Created, Paid, Failed

        public DateTime CreatedAt { get; set; }
    }
}
