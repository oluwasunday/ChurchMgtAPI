using church_mgt_dtos.ContactUsDtos;
using church_mgt_dtos.Dtos;
using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface IContactUsService
    {
        Task<Response<string>> AddContactAsync(AddContactDto contactDto);
        Task<Response<string>> DeletContactsAsync(string contactId);
        Task<Response<IEnumerable<ContactUs>>> GetAllContactsAsync();
        Task<Response<ContactUs>> GetAllContactsAsync(string contactId);
    }
}