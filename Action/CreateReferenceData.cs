using System;
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
        public string LogText
        {
            get
            {
                return String.Format("Create reference data {0}:{1}:{2}:{3}", ActionAgainst.Type, ActionAgainst.Name, ActionAgainst.Description, ActionAgainst.Value);
            }
        }
    }
}
