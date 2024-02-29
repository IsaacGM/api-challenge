using ApiApplication.Dto;
using FluentValidation;

namespace ApiApplication.Application.DataJobs.Validators;

public class DataJobValidator : AbstractValidator<DataJobDTO>
{
    public DataJobValidator()
    {
        RuleFor(dto => dto.FilePathToProcess)
            .NotEmpty()
            .EmailAddress()
            .WithName("FilePathToProcess")
            .WithMessage("FilePathToProcess must not be null.");

        RuleFor(dto => dto.Name)
            .NotEmpty()
            .EmailAddress()
            .WithName("Name")
            .WithMessage("Name must not be null.");

    }
}
