using AutoMapper;
using church_mgt_core.services.interfaces;
using church_mgt_core.UnitOfWork.interfaces;
using church_mgt_dtos.Dtos;
using church_mgt_dtos.PaymentTypeDtos;
using church_mgt_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Response<IEnumerable<AddPaymentTypeResponseDto>> GetAllPaymentType()
        {
            var types = _unitOfWork.PaymentType.GetAll();

            var response = _mapper.Map<IEnumerable<AddPaymentTypeResponseDto>>(types);
            return Response<IEnumerable<AddPaymentTypeResponseDto>>.Success("Success", response);
        }

        public async Task<Response<AddPaymentTypeResponseDto>> GetPaymentTypeById(string paymentTypeId)
        {
            var type = await _unitOfWork.PaymentType.GetAsync(paymentTypeId);
            if (type == null)
                return Response<AddPaymentTypeResponseDto>.Fail("Payment type not found");

            var response = _mapper.Map<AddPaymentTypeResponseDto>(type);

            return Response<AddPaymentTypeResponseDto>.Success("Success", response);
        }

        public async Task<Response<AddPaymentTypeResponseDto>> AddPaymentType(AddPaymentTypeDto paymentTypeDto)
        {
            var confirmType = await _unitOfWork.PaymentType.GetPaymentTypeByName(paymentTypeDto.TypeOfPayment);
            if (confirmType != null)
                return Response<AddPaymentTypeResponseDto>.Fail("Payment type already added");

            var type = _mapper.Map<PaymentType>(paymentTypeDto);

            await _unitOfWork.PaymentType.AddAsync(type);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddPaymentTypeResponseDto>(type);

            return Response<AddPaymentTypeResponseDto>.Success("Successful", response);
        }

        public async Task<Response<AddPaymentTypeResponseDto>> UpdatePaymentType(string paymentTypeId)
        {
            var paymentType = await _unitOfWork.PaymentType.GetAsync(paymentTypeId);
            if (paymentType == null)
                return Response<AddPaymentTypeResponseDto>.Fail("Payment type not found");

            _unitOfWork.PaymentType.UpdatePaymentType(paymentType);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddPaymentTypeResponseDto>(paymentType);

            return Response<AddPaymentTypeResponseDto>.Success("Success", response);
        }

        public async Task<Response<string>> DeletePaymentType(string paymentTypeId)
        {
            var paymentType = await _unitOfWork.PaymentType.GetAsync(paymentTypeId);
            if (paymentType == null)
                return Response<string>.Fail("Payment type not found");

            _unitOfWork.PaymentType.Remove(paymentType);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Success", $"Payment type with id {paymentType.Id} deleted");
        }
    }
}
