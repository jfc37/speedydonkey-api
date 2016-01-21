using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Contracts.Events
{
    /// <summary>
    /// Model for when user is registering for events
    /// </summary>
    public class EventRegistrationModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventRegistrationModel"/> class.
        /// </summary>
        public EventRegistrationModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventRegistrationModel"/> class.
        /// </summary>
        /// <param name="eventIds">The event ids.</param>
        public EventRegistrationModel(params int[] eventIds)
        {
            EventIds = eventIds.ToList();
        }

        /// <summary>
        /// Event ids to to register user in
        /// </summary>
        /// <value>
        /// The event ids.
        /// </value>
        [Required]
        public List<int> EventIds { get; set; } 
    }
}