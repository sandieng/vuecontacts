using System.ComponentModel.DataAnnotations;

namespace VueContactsAPI.ViewModels
{
    public class ContactVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string Company { get; set; }

        public string JobTitle { get; set; } 

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]

        public string Phone { get; set; }
        [StringLength(120)]
        public string Notes { get; set; }
    }
}
