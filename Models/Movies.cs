using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace move_store_app.Models
{
    public class Movies
    {   
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime RelDate { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
        [Range(1, 10)]
        public int Rating { get; set; }
        public string? Image { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

    }
}
