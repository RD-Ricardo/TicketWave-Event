using FluentValidation;

namespace Application.Events.CreateEvent
{
    public record CreateEventInputModel(
        string Name,
        string? Description,
        DateTime Date,
        string Street,
        string Number,
        string City,
        string State,
        string ZipCode,
        string Country,
        string? Complement
     );
  
    public class CreateEventInputModelValidator : AbstractValidator<CreateEventInputModel>
    {
        public CreateEventInputModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nome não pode ser vazio/nulo");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Data não pode ser vazia/nula");
                //.LessThan(DateTime.UtcNow.Date).WithMessage("A data não pode ser menor que hoje.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Rua não pode ser vazia/nula");

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage("Número não pode ser vazio/nulo");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Cidade não pode ser vazia/nula");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("Estado não pode ser vazio/nulo");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("CEP não pode ser vazio/nulo");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("País não pode ser vazio/nulo");
        }
    }
}
