using ContactsManagementApi.Application.DTOs;
using ContactsManagementApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManagementApi.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contact = await _service.GetAllContactsAsync();
            return contact != null ? Ok(contact) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _service.GetContactByIdAsync(id);
            return contact != null ? Ok(contact) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactDto contactDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdContact = await _service.CreateContactAsync(contactDto);
            return CreatedAtAction(nameof(Get), new { id = createdContact.Id }, createdContact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactDto contactDto)
        {
            if (id != contactDto.Id) return BadRequest("Id mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedContact = await _service.UpdateContactAsync(contactDto);
            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteContactAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
