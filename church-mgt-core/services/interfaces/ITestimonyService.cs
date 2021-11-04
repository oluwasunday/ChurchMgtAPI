using church_mgt_dtos.Dtos;
using church_mgt_dtos.TestimonyDtos;
using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface ITestimonyService
    {
        Task<Response<Testimony>> AddTestimony(string memberId, AddTestimonyDto testimonyDto);
        Task<Response<string>> DeleteTestimonyById(string testimonyId);
        Response<IEnumerable<Testimony>> GetTestimonies();
        Task<Response<Testimony>> GetTestimonyById(string testimonyId);
    }
}