namespace AsosEcommerceApi.DTO
{
    public class CreateProductVariationDto
    {
        public Guid ProductId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

}
