using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateNoticeValidator : AbstractValidator<Notice>, IActionValidator<UpdateNotice, Notice>
    {
        private INoticeRepository _noticeRepository;

        public UpdateNoticeValidator(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
            RuleFor(x => x.Id).Must(BeExistingNotice).WithMessage(ValidationMessages.NoticeDoesntExist);
            RuleFor(x => x.Message).NotEmpty().WithMessage(ValidationMessages.MissingNoticeMessage);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateBeforeStartDate);
        }

        private bool BeExistingNotice(int id)
        {
            return _noticeRepository.Get(id) != null;
        }
    }
}
