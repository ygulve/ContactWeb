using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactWeb.Models
{
    public class Contact
    {       
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Email")]
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",  ErrorMessage = "Please Enter Correct Email Address")]
        public string Email { get; set; }
        [DisplayName("Phone number")]        
        [Required]
        [MaxLength(10, ErrorMessage = "The Phone number must contains 10 characters")]        
        [RegularExpression("^[0-9]+", ErrorMessage = "Phone must be numeric")]        
        public string PhoneNumber { get; set; }
        [DisplayName("Status")]
        [Required]
        public bool Status { get; set; }
    }
}