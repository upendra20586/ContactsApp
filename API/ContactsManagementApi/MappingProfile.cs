using AutoMapper;
using ContactsManagementApi.Domain.Entities;
using ContactsManagementApi.Application.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contact, ContactDto>();
        CreateMap<ContactDto, Contact>();
    }
}
