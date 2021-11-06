using church_mgt_dtos.Dtos;
using church_mgt_dtos.PaymentTypeDtos;
using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface IPaymentTypeService
    {
        Task<Response<AddPaymentTypeResponseDto>> AddPaymentType(AddPaymentTypeDto paymentTypeDto);
        Task<Response<string>> DeletePaymentType(string paymentTypeId);
        Response<IEnumerable<AddPaymentTypeResponseDto>> GetAllPaymentType();
        Task<Response<AddPaymentTypeResponseDto>> GetPaymentTypeById(string paymentTypeId);
        Task<Response<AddPaymentTypeResponseDto>> UpdatePaymentType(string paymentTypeId);
    }
}