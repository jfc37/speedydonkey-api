using Action.Rooms;
using Data.Repositories;
using Models;

namespace Validation.Validators.Rooms
{
    public class DeleteRoomValidator : PreExistingValidator<Room>, IActionValidator<DeleteRoom, Room>
    {
        public DeleteRoomValidator(IRepository<Room> repository)
            : base(repository)
        {
        }
    }
}