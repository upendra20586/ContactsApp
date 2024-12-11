using AutoMapper;
using ContactsManagementApi.Application.DTOs;
using ContactsManagementApi.Application.Interfaces;
using ContactsManagementApi.Domain.Entities;
using ContactsManagementApi.Domain.Interfaces;

namespace ContactsManagementApi.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;

        public ContactService(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            var contacts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactDto>>(contacts);
        }

        public async Task<ContactDto> GetContactByIdAsync(int id)
        {
            var contact = await _repository.GetByIdAsync(id);
            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactDto> CreateContactAsync(ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);
            var newContact = await _repository.AddAsync(contact);
            return _mapper.Map<ContactDto>(newContact);
        }

        public async Task<ContactDto> UpdateContactAsync(ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);
            var updatedContact = await _repository.UpdateAsync(contact);
            return _mapper.Map<ContactDto>(updatedContact);
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
