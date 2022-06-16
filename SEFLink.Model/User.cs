using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SEFLink.Model
{
    public class User
    {
        public int Id { get; set; }

        public bool IsAdmin { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public UserSettings UserSettings { get; set; }
    }
}
