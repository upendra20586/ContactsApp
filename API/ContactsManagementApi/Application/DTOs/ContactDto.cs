﻿using System.ComponentModel.DataAnnotations;

namespace ContactsManagementApi.Application.DTOs
{
    public class ContactDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

    }
}
