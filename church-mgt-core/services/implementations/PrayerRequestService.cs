using AutoMapper;
using church_mgt_core.services.interfaces;
using church_mgt_core.UnitOfWork.interfaces;
using church_mgt_dtos.Dtos;
using church_mgt_dtos.PrayerRequestDtos;
using church_mgt_models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class PrayerRequestService : IPrayerRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public PrayerRequestService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public Response<IEnumerable<PrayerRequest>> GetAllPrayerRequests()
        {
            var requests = _unitOfWork.PrayerRequest.GetAll();
            return Response<IEnumerable<PrayerRequest>>.Success("Success", requests);
        }

        public async Task<Response<PrayerRequest>> GetPrayerRequestByIdAsync(string requestId)
        {
            var request = await _unitOfWork.PrayerRequest.GetAsync(requestId);
            if(request == null)
                return Response<PrayerRequest>.Fail("Failed");

            return Response<PrayerRequest>.Success("Success", request);
        }

        public async Task<Response<PrayerRequest>> AddPrayerRequest(string userId, AddPrayerRequestDto prayerRequestDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Response<PrayerRequest>.Fail($"User with id {userId} not found");

            var request = _mapper.Map<PrayerRequest>(prayerRequestDto);
            request.AppUserId = userId;
            

            await _unitOfWork.PrayerRequest.AddAsync(request);
            await _unitOfWork.CompleteAsync();

            return Response<PrayerRequest>.Success("Success", request);
        }

        public Response<IEnumerable<PrayerRequest>> GetPrayerRequestsByMemberId(string memberId)
        {
            var request = _unitOfWork.PrayerRequest.GetPrayerRequestsByAMember(memberId);
            if (request == null)
                return Response<IEnumerable<PrayerRequest>>.Fail("Failed");

            return Response<IEnumerable<PrayerRequest>>.Success("Success", request);
        }

        public async Task<Response<string>> DeletePrayerRequestsById(string requestId)
        {
            var request = await _unitOfWork.PrayerRequest.GetAsync(requestId);
            if (request == null)
                return Response<string>.Fail("Prayer request not found");

            _unitOfWork.PrayerRequest.Remove(request);
            await _unitOfWork.CompleteAsync();
            

            return Response<string>.Success("Success", $"Prayer request with id {request.Id} deleted");
        }
    }
}
