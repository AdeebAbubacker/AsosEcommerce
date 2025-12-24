namespace AsosEcommerceApi.DTO
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }

        public List<CreateProductVariationDto> Variations { get; set; }

    }

}
