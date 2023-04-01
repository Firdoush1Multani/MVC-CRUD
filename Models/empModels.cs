using System.ComponentModel.DataAnnotations;

namespace MVC_CRUD.Models
{
    public class empModels
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
