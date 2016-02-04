using System.Collections.Generic;
using System.Linq;
using Models;

namespace ActionHandlers.Announcements.RetrieveUsers
{
    /// <summary>
    /// Decorator around IRetrieveUsersToNotify to filter out duplicate users, and those with do not email flags
    /// </summary>
    /// <seealso cref="ActionHandlers.Announcements.RetrieveUsers.IRetrieveUsersToNotify" />
    public class RetrieveUsersToNotifyFilterDecorator : IRetrieveUsersToNotify
    {
        private readonly IRetrieveUsersToNotify _actualRetriever;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetrieveUsersToNotifyFilterDecorator"/> class.
        /// </summary>
        /// <param name="actualRetriever">The actual retriever.</param>
        public RetrieveUsersToNotifyFilterDecorator(IRetrieveUsersToNotify actualRetriever)
        {
            _actualRetriever = actualRetriever;
        }

        /// <summary>
        /// Gets the specified announcement.
        /// </summary>
        /// <param name="announcement">The announcement.</param>
        /// <returns></returns>
        public IEnumerable<User> Get(Announcement announcement)
        {
            return _actualRetriever.Get(announcement)
                .Distinct()
                .Where(x => !x.DoNotEmail);
        }
    }
}