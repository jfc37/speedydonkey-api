using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Extensions;

namespace SpeedyDonkeyApi.Models
{
    /// <summary>
    /// Model for when user is enrolling in blocks
    /// </summary>
    public class EnrolmentModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolmentModel"/> class.
        /// </summary>
        public EnrolmentModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolmentModel"/> class.
        /// </summary>
        /// <param name="blockIds">The block ids.</param>
        public EnrolmentModel(params int[] blockIds)
        {
            BlockIds = blockIds.ToList();
        }

        /// <summary>
        /// Block ids to enrol user in
        /// </summary>
        /// <value>
        /// The block ids.
        /// </value>
        [Required]
        public List<int> BlockIds { get; set; }
        public override string ToString()
        {
            return this.ToDebugString(nameof(BlockIds));
        }
    }
}