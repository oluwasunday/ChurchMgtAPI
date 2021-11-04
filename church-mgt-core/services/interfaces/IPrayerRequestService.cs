using church_mgt_dtos.Dtos;
using church_mgt_dtos.PrayerRequestDtos;
using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface IPrayerRequestService
    {
        Task<Response<PrayerRequest>> AddPrayerRequest(string userId, AddPrayerRequestDto prayerRequestDto);
        Task<Response<string>> DeletePrayerRequestsById(string requestId);
        Response<IEnumerable<PrayerRequest>> GetAllPrayerRequests();
        Task<Response<PrayerRequest>> GetPrayerRequestByIdAsync(string requestId);
        Response<IEnumerable<PrayerRequest>> GetPrayerRequestsByMemberId(string memberId);
    }
}