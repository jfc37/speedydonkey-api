using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateLectureValidator : AbstractValidator<Lecture>, IActionValidator<UpdateLecture, Lecture>
    {
        private ILectureRepository _lectureRepository;

        public UpdateLectureValidator(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
            RuleFor(x => x.Id).Must(BeExistingLecture).WithMessage(ValidationMessages.LectureDoesntExist);
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.MissingName);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateBeforeStartDate);
            RuleFor(x => x.Occurence).NotEqual(Occurence.Invalid).WithMessage(ValidationMessages.MissingOccurence);
        }

        private bool BeExistingLecture(int id)
        {
            return _lectureRepository.Get(id) != null;
        }
    }
}
