using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using CrossCutting.Abstractions;

namespace Application.Events.CreateEvent
{
    public class CreateEventUseCase : IUseCase<CreateEventInputModel, VoidResult>
    {
        private readonly IRepository<Event> _repository;
        public CreateEventUseCase(IRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task<Result<VoidResult>> Execute(CreateEventInputModel input)
        {
            var validationResult = new CreateEventInputModelValidator().Validate(input);

            if (!validationResult.IsValid)
            {
                return Result<VoidResult>.Fail(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            var address = new Address(input.Street, input.Number, input.City, input.State, input.ZipCode, input.Country, input.Complement);

            var @event = new Event(input.Name, input.Description, input.Date, address);

            await _repository.Add(@event);

            await _repository.SaveChanges();

            return Result<VoidResult>.Success(new VoidResult());
        }
    }
}
