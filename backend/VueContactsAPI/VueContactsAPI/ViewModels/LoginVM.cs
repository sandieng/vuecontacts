using System.ComponentModel.DataAnnotations;

namespace VueContactsAPI.ViewModels
{
    public class LoginVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string Notes { get; set; }

        public string WebSiteUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string LoginName { get; set; }

        [Required]
        [StringLength(50)]
        public string LoginPassword { get; set; }
    }
}
