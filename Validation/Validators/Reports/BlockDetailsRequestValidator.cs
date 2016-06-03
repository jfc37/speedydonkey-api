using Contracts.Reports.BlockDetails;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Reports
{
    /// <summary>
    /// Validator for BlockDetailsRequest
    /// </summary>
    public class BlockDetailsRequestValidator : AbstractValidator<BlockDetailsRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlockDetailsRequestValidator"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public BlockDetailsRequestValidator(IRepository<Block> repository)
        {
            RuleFor(x => x.BlockId)
                .Must(x => new DoesIdExist<Block>(repository, x).IsValid()).WithMessage("Please a block");
        }
    }
}