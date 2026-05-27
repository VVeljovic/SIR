using FluentValidation;

namespace Producer.Application.Commands
{
    public class ReadFileCommandValidator : AbstractValidator<ReadFileCommand>
    {
        public ReadFileCommandValidator() 
        {
            RuleFor(x => x.FilePath).NotEmpty().WithMessage("File path should not be empty.");
        }
    }
}
