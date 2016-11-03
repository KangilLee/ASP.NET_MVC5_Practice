using System.ComponentModel.DataAnnotations;

namespace Assignment8.Controllers
{
    public class GenreBase
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}