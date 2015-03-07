using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateUserHandler : CreateEntityHandler<CreateUser, User>
    {

        public CreateUserHandler(
            IRepository<User> repository) : base(repository)
        {
        }
    }
}
