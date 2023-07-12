using System.ComponentModel.DataAnnotations;

namespace AssurecareAPI.Models
{
    public class Brand
    {
        [Key]
        public string? BrandId { get; set; }
        public string? BrandName { get; set; }

        public string? Category { get; set; }

        public bool ActiveFlag { get; set; }
    }
}
