using Actions;
using Models;

namespace Action
{
    public class CreateReferenceData : ICreateAction<ReferenceData>
    {
        public CreateReferenceData(ReferenceData referenceData)
        {
            ActionAgainst = referenceData;
        }
        public ReferenceData ActionAgainst { get; set; }
    }
}
