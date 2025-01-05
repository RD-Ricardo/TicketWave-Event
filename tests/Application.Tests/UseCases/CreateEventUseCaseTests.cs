using Application.Events.CreateEvent;
using CrossCutting.Abstractions;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Application.Tests.UseCases
{
    public class CreateEventUseCaseTests
    {
        private readonly Mock<IRepository<Event>> _repository;

        private readonly CreateEventUseCase _useCase;
        public CreateEventUseCaseTests()
        {
            _repository = new Mock<IRepository<Event>>();
            _useCase = new CreateEventUseCase(_repository.Object);
        }

        [Fact]
        public async Task Execute_ValidInput_ShouldReturnSuccess()
        {
            // Arrange
            var input = new CreateEventInputModel("Evento 1", "Um evento muito devertido", 
                DateTime.UtcNow.AddDays(2), 
                "Avenida Joaquim Ferreira Souto", 
                "343", 
                "São Paulo", 
                "SP", 
                "17120-970", 
                "Brasil", null);

            _repository.Setup(x => x.Add(It.IsAny<Event>())).Returns(Task.CompletedTask);

            _repository.Setup(x => x.SaveChanges()).Returns(Task.FromResult(1));

            // Act
            var result = await _useCase.Execute(input);

            // Assert
            result.IsFail.Should().BeFalse();
            result.Should().BeOfType<Result<VoidResult>>();
            _repository.Verify(x => x.Add(It.IsAny<Event>()), Times.Once);
            _repository.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public async Task Execute_InvalidInput_ShouldReturnFail()
        {
            // Arrange
            var input = new CreateEventInputModel("", "Um evento muito devertido",
                DateTime.UtcNow.AddDays(-2),
                "Avenida Joaquim Ferreira Souto",
                "343",
                "São Paulo",
                "SP",
                "17120-970",
                "Brasil", null);

            // Act
            var result = await _useCase.Execute(input);
            
            // Assert
            _repository.Verify(x => x.Add(It.IsAny<Event>()), Times.Never);
            _repository.Verify(x => x.SaveChanges(), Times.Never);
            result.IsFail.Should().BeTrue();
            result.Should().BeOfType<Result<VoidResult>>();
        }

        [Fact]
        public async Task Execute_InvalidInputZipCode_ShouldReturnFail()
        {
            // Arrange
            var input = new CreateEventInputModel("Evento 1", "Um evento muito devertido",
                DateTime.UtcNow.AddDays(-2),
                "Avenida Joaquim Ferreira Souto",
                "343",
                "São Paulo",
                "SP",
                "17120970",
                "Brasil", null);

            _repository.Setup(x => x.Add(It.IsAny<Event>()));
            
            // Act
            var result = await _useCase.Execute(input);
            
            // Assert
            _repository.Verify(x => x.Add(It.IsAny<Event>()), Times.Never);
            _repository.Verify(x => x.SaveChanges(), Times.Never);
            result.IsFail.Should().BeTrue();
            result.Should().BeOfType<Result<VoidResult>>();
        }
    }
}
