using Sat.Recruitment.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Domain.DTO
{
    /// <summary>User Data Transfer Object</summary>
    public class UserDto
    {

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Name is required")]
        public string Name { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Email is required")]
        [EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Address is required")]
        public string Address { get; set; }

        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The Phone is required")]
        public string Phone { get; set; }

        /// <summary>Gets or sets the type of the user.</summary>
        /// <value>The type of the user.</value>
        [EnumDataType(typeof(UserType))]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The UserType is required")]
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}
