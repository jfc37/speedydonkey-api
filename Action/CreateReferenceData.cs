using Actions;
using Models;

namespace Action
{
    public class CreateReferenceData : ICrudAction<ReferenceData>
    {
        public CreateReferenceData(ReferenceData referenceData)
        {
            ActionAgainst = referenceData;
        }
        public ReferenceData ActionAgainst { get; set; }
    }
}
