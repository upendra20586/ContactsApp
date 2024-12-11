using ContactsManagementApi.Application.DTOs;

namespace ContactsManagementApi.Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllContactsAsync();
        Task<ContactDto> GetContactByIdAsync(int id);
        Task<ContactDto> CreateContactAsync(ContactDto contactDto);
        Task<ContactDto> UpdateContactAsync(ContactDto contactDto);
        Task<bool> DeleteContactAsync(int id);
    }
}

