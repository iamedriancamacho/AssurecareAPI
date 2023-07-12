using System.ComponentModel.DataAnnotations;

namespace AssurecareAPI.Models
{
    public class OpenAI
    {
        [Key]
        public int? AIId { get; set; }
        public string? Query { get; set; }
        public string? Response { get; set; }

    }
}
