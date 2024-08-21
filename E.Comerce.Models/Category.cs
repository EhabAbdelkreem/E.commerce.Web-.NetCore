using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E.Comerce.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Dipkay order")]
        public int DiplayOrder { get; set; }
        public DateTime creadDataTime { get; set; } = DateTime.Now;
    }
}
