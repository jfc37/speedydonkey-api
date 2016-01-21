using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Contracts.Reports;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Reports
{
    [RoutePrefix("api/report/profit")]
    public class ProfitReportApiController : BaseApiController
    {
        private readonly IRepository<Pass> _passRepository;
        private readonly IRepository<Block> _blockRepository;

        public ProfitReportApiController(
            IRepository<Pass> passRepository,
            IRepository<Block> blockRepository)
        {
            _passRepository = passRepository;
            _blockRepository = blockRepository;
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public virtual HttpResponseMessage Get(DateTime from, DateTime to)
        {
            var passesBoughtWithInPeriod =
                _passRepository.GetAll().Where(x => x.CreatedDateTime >= from && x.CreatedDateTime <= to)
                .ToList();

            var unlimitedPassesValidWithInPeriod =
                _passRepository.GetAll().Where(x => x.StartDate >= from && x.EndDate <= to && x.PassType == PassType.Unlimited.ToString())
                .ToList();

            var blocksWithInPeriod =
                _blockRepository.GetAll().Where(x => x.StartDate >= from && x.EndDate <= to)
                .ToList();

            var profitReportModel = new ProfitReportModel().Populate(passesBoughtWithInPeriod, blocksWithInPeriod, unlimitedPassesValidWithInPeriod);
            return Request.CreateResponse(profitReportModel);
        }
    }
}