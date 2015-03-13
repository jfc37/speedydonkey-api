using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateReferenceDataHandler : CreateEntityHandler<CreateReferenceData, ReferenceData>
    {
        public CreateReferenceDataHandler(
            IRepository<ReferenceData> repository) : base(repository)
        {
        }
    }
}
