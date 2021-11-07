using AutoMapper;
using church_mgt_core.services.interfaces;
using church_mgt_core.UnitOfWork.interfaces;
using church_mgt_dtos.ContactUsDtos;
using church_mgt_dtos.Dtos;
using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class ContactUsService : IContactUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactUsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<string>> AddContactAsync(AddContactDto contactDto)
        {
            var contact = _mapper.Map<ContactUs>(contactDto);

            await _unitOfWork.ContactUs.AddAsync(contact);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Successful", "Comment recorded, thanks for your feedback");
        }

        public async Task<Response<IEnumerable<ContactUs>>> GetAllContactsAsync()
        {
            var contacts = await _unitOfWork.ContactUs.GetAllAsync();
            return Response<IEnumerable<ContactUs>>.Success("Success", contacts);
        }

        public async Task<Response<ContactUs>> GetAllContactsAsync(string contactId)
        {
            var contact = await _unitOfWork.ContactUs.GetAsync(contactId);
            if (contact == null)
                return Response<ContactUs>.Fail($"Contact with id {contactId} not found");

            return Response<ContactUs>.Success("Success", contact);
        }

        public async Task<Response<string>> DeletContactsAsync(string contactId)
        {
            var contact = await _unitOfWork.ContactUs.GetAsync(contactId);
            if (contact == null)
                return Response<string>.Fail($"Contact with id {contactId} not found");

            _unitOfWork.ContactUs.Remove(contact);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Success", $"Contact with id {contactId} successfully deleted");
        }
    }
}
