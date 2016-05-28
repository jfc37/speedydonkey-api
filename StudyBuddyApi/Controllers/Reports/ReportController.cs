using System;
using System.Web.Http;
using SpeedyDonkeyApi.Extensions;
using Validation;

namespace SpeedyDonkeyApi.Controllers.Reports
{
    /// <summary>
    /// Generic report controller that knows how to generate a report
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <seealso cref="SpeedyDonkeyApi.Controllers.BaseApiController" />
    public abstract class ReportController<TRequest, TResponse> : BaseApiController
    {
        private readonly IValidatorOverlord _validatorOverlord;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validatorOverlord">The validator overlord.</param>
        protected ReportController(IValidatorOverlord validatorOverlord)
        {
            _validatorOverlord = validatorOverlord;
        }

        /// <summary>
        /// Runs the report.
        /// Will validate the report inputs
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="generateReport">The generate report.</param>
        /// <returns></returns>
        protected IHttpActionResult RunReport(TRequest request, Func<TRequest, TResponse> generateReport)
        {
            var validationResult = _validatorOverlord.Validate(request);

            return validationResult.IsValid 
                ? Ok(generateReport(request)) 
                : this.BadRequestWithContent(validationResult);
        }
    }
}