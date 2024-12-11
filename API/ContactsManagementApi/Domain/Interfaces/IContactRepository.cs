using ContactsManagementApi.Domain.Entities;

namespace ContactsManagementApi.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(int id);
        Task<Contact> AddAsync(Contact contact);
        Task<Contact> UpdateAsync(Contact contact);
        Task<bool> DeleteAsync(int id);
    }
}
