using ContactsManagementApi.Domain.Entities;
using ContactsManagementApi.Domain.Interfaces;
using System.Text.Json;

namespace ContactsManagementApi.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private const string JsonFilePath = "Infrastructure/Data/contacts.json";
        private List<Contact> _contacts;

        public ContactRepository()
        {
            if (File.Exists(JsonFilePath))
            {
                var json = File.ReadAllText(JsonFilePath);
                _contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
            }
            else
            {
                _contacts = new List<Contact>();
            }
        }

        private void SaveChanges()
        {
            var json = JsonSerializer.Serialize(_contacts);
            File.WriteAllText(JsonFilePath, json);
        }

        public async Task<IEnumerable<Contact>> GetAllAsync() => await Task.FromResult(_contacts);

        public async Task<Contact?> GetByIdAsync(int id) =>
            await Task.FromResult(_contacts.FirstOrDefault(c => c.Id == id));

        public async Task<Contact> AddAsync(Contact contact)
        {
            contact.Id = _contacts.Any() ? _contacts.Max(c => c.Id) + 1 : 1;
            _contacts.Add(contact);
            SaveChanges();
            return await Task.FromResult(contact);
        }

        public async Task<Contact> UpdateAsync(Contact contact)
        {
            var existingContact = _contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact == null) throw new KeyNotFoundException("Contact not found.");

            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Email = contact.Email;

            SaveChanges();
            return await Task.FromResult(existingContact);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return false;

            _contacts.Remove(contact);
            SaveChanges();
            return await Task.FromResult(true);
        }
    }
}
