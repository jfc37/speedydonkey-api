using System.Linq;
using Action.Users;
using Data.Repositories;
using Models;

namespace ActionHandlers.Users.TermsAndConditions
{
    /// <summary>
    /// Sets terms and conditions agreement for user
    /// </summary>
    /// <seealso cref="ActionHandlers.IActionHandler{Action.Users.AgreeToTermsAndConditions, Models.User}" />
    public class AgreeToTermsAndConditionsHandler : IActionHandler<AgreeToTermsAndConditions, User>
    {
        private readonly IRepository<User> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgreeToTermsAndConditionsHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AgreeToTermsAndConditionsHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Handles the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public User Handle(AgreeToTermsAndConditions action)
        {
            var user = _repository.Queryable().Single(x => x.Id == action.ActionAgainst.Id);
            user.AgreesToTerms = true;

            return _repository.Update(user);
        }
    }
}