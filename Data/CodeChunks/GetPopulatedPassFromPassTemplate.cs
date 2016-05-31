using System;
using Common.Extensions;
using Models;

namespace Data.CodeChunks
{
    /// <summary>
    /// Returns a populated pass based on a pass template and a start date
    /// </summary>
    public class GetPopulatedPassFromPassTemplate : ICodeChunk<Pass>
    {
        private readonly Pass _passToPopulate;
        private readonly PassTemplate _passTemplate;

        public GetPopulatedPassFromPassTemplate(Pass passToPopulate, PassTemplate passTemplate)
        {
            _passToPopulate = passToPopulate;
            _passTemplate = passTemplate;
        }

        public Pass Do()
        {
            _passToPopulate.StartDate = DateTime.Now.Date;
            _passToPopulate.EndDate = _passToPopulate.StartDate.AddWeeks(_passTemplate.WeeksValidFor);
            _passToPopulate.Cost = _passTemplate.Cost;
            _passToPopulate.PassType = _passTemplate.PassType;
            _passToPopulate.Description = _passTemplate.Description;

            return _passToPopulate;
        }
    }
}