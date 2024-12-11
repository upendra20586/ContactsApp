using Moq;
using Xunit;
using AutoMapper;
using ContactsManagementApi.Application.DTOs;
using ContactsManagementApi.Application.Interfaces;
using ContactsManagementApi.Application.Services;
using ContactsManagementApi.Domain.Entities;
using ContactsManagementApi.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactsManagementApi.Tests
{
    public class ContactServiceTests
    {
        private readonly Mock<IContactRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly ContactService _service;

        public ContactServiceTests()
        {
            _repositoryMock = new Mock<IContactRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Contact, ContactDto>().ReverseMap();
            });
            _mapper = mapperConfig.CreateMapper();

            _service = new ContactService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllContactsAsync_ShouldReturnContacts()
        {
            // Arrange
            var contacts = new List<Contact>
        {
            new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" }
        };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);

            // Act
            var result = await _service.GetAllContactsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task CreateContactAsync_ShouldAddContact()
        {
            // Arrange
            var contactDto = new ContactDto { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" };
            var contact = new Contact { Id = 1, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com" };

            _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Contact>())).ReturnsAsync(contact);

            // Act
            var result = await _service.CreateContactAsync(contactDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane", result.FirstName);
        }
    }
}
