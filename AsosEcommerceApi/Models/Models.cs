using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AsosEcommerceApi.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    // Catalog tables
    public class ProductCategory { public Guid Id { get; set; } = Guid.NewGuid(); public string Name { get; set; } public string Description { get; set; } public Guid? ParentCategoryId { get; set; } }
    public class Brand { public Guid Id { get; set; } = Guid.NewGuid(); public string Name { get; set; } public string Description { get; set; } }
    public class Color { public Guid Id { get; set; } = Guid.NewGuid(); public string Name { get; set; } }
    public class SizeCategory { public Guid Id { get; set; } = Guid.NewGuid(); public string Name { get; set; } }
    public class SizeOption { public Guid Id { get; set; } = Guid.NewGuid(); public string Name { get; set; } public Guid SizeCategoryId { get; set; } }

    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string About { get; set; }
        public string CareInstructions { get; set; }
        public string ModelHeight { get; set; }
        public string ModelWearing { get; set; }
        public Guid ProductCategoryId { get; set; }
        public Guid BrandId { get; set; }
        public ICollection<ProductItem> Items { get; set; }
        public ICollection<ProductAttribute> Attributes { get; set; }
    }

    public class ProductItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Guid ColorId { get; set; }
        public string ProductCode { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public ICollection<ProductVariation> Variations { get; set; }
    }

    public class ProductVariation { public Guid Id { get; set; } = Guid.NewGuid(); public Guid ProductItemId { get; set; } public Guid SizeOptionId { get; set; } public int QuantityInStock { get; set; } }

    public class AttributeType { public Guid Id { get; set; } = Guid.NewGuid(); public string Name { get; set; } }
    public class AttributeOption { public Guid Id { get; set; } = Guid.NewGuid(); public Guid AttributeTypeId { get; set; } public string Name { get; set; } }
    public class ProductAttribute { public Guid ProductId { get; set; } public Guid AttributeOptionId { get; set; } }

    // Cart & Orders
    public class CartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid ProductVariationId { get; set; }
        public int Quantity { get; set; }
    }

    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductVariationId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
