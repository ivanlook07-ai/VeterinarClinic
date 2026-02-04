using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string Age { get; set; }

        [Required]
        public string Owner { get; set; }

        //public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}