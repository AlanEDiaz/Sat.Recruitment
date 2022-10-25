using Sat.Recruitment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Domain.Entities
{
    /// <summary>
    ///   <para>User class to save in db</para>
    /// </summary>
    public class User
    {

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>Gets or sets the address.</summary>
        /// <value>The address.</value>
        public string Address { get; set; }
        /// <summary>Gets or sets the phone.</summary>
        /// <value>The phone.</value>
        public string Phone { get; set; }
        /// <summary>Gets or sets the type of the user.</summary>
        /// <value>The type of the user.</value>
        public UserType UserType { get; set; }
        /// <summary>Gets or sets the money.</summary>
        /// <value>The money.</value>
        public decimal Money { get; set; }
    }
}
