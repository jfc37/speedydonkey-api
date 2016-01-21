using System.ComponentModel.DataAnnotations;

namespace Contracts.Users
{
    /// <summary>
    /// Model for a user's name
    /// </summary>
    public class UserNamesModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNamesModel"/> class.
        /// </summary>
        public UserNamesModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNamesModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="surname">The surname.</param>
        public UserNamesModel(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }

    }
}