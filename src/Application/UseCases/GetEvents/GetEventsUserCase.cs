using CrossCutting.Abstractions;
using CrossCutting.Dtos;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases.GetEvents
{
    public class GetEventsUserCase : IUseCase<IEnumerable<EventDto>>
    {
        private readonly IRepository<Event> _repository;
        public GetEventsUserCase(IRepository<Event> repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<EventDto>>> Execute()
        {
            var events = await _repository.GetAll();

            var eventsDto = events.Select(e => new EventDto
            {
                Name = e.Name,
                Description = e.Description,
                Date = e.Date,
                Street = e.Address.Street,
                Number = e.Address.Number,
                City = e.Address.City,
                State = e.Address.State,
                ZipCode = e.Address.ZipCode,
                Country = e.Address.Country,
                Complement = e.Address.Complement
            });

            return Result<IEnumerable<EventDto>>.Success(eventsDto);
        }
    }
}
