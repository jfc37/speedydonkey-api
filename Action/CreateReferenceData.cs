using System;
using Actions;
using Models;

namespace Action
{
    public class CreateReferenceData : SystemAction<ReferenceData>, ICrudAction<ReferenceData>
    {
        public CreateReferenceData(ReferenceData referenceData)
        {
            ActionAgainst = referenceData;
        }
    }
}
