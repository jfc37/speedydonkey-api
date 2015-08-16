using System;
using Common.Extensions;
using Models;

namespace Data.CodeChunks
{
    /// <summary>
    /// Returns a populated pass based on a pass template and a start date
    /// </summary>
    public class GetPopulatedPassFromPassTemplate : ICodeChunk<IPass>
    {
        private readonly IPass _passToPopulate;
        private readonly DateTime _startDate;
        private readonly PassTemplate _passTemplate;

        public GetPopulatedPassFromPassTemplate(IPass passToPopulate, DateTime startDate, PassTemplate passTemplate)
        {
            _passToPopulate = passToPopulate;
            _startDate = startDate;
            _passTemplate = passTemplate;
        }

        public IPass Do()
        {
            _passToPopulate.StartDate = _startDate;
            _passToPopulate.EndDate = _startDate.AddWeeks(_passTemplate.WeeksValidFor);
            _passToPopulate.Cost = _passTemplate.Cost;
            _passToPopulate.PassType = _passTemplate.PassType;
            _passToPopulate.Description = _passTemplate.Description;

            return _passToPopulate;
        }
    }
}