using Action;
using Common.Extensions;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Announcements
{
    public class CreateAnnouncementValidator : AbstractValidator<Announcement>, IActionValidator<CreateAnnouncement, Announcement>
    {
        public CreateAnnouncementValidator(IRepository<Block> blockRepository)
        {
            RuleFor(x => x.Id).Must((x, y) => new DoesAnnouncementHaveSomeoneToNotify(x).IsValid());

            RuleFor(x => x.Receivers)
                .Must(x => new DoAllIdExists<Block>(blockRepository, x).IsValid())
                .When(x => x.Receivers.IsNotNullOrEmpty());

            RuleFor(x => x.Subject).NotEmpty();

            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
