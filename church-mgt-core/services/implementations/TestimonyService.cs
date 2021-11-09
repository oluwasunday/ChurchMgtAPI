using AutoMapper;
using church_mgt_core.services.interfaces;
using church_mgt_core.UnitOfWork.interfaces;
using church_mgt_dtos.Dtos;
using church_mgt_dtos.TestimonyDtos;
using church_mgt_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class TestimonyService : ITestimonyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TestimonyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Response<IEnumerable<Testimony>> GetTestimonies()
        {
            var testimonies = _unitOfWork.Testimony.GetAll();
            return Response<IEnumerable<Testimony>>.Success("Success", testimonies);
        }

        public async Task<Response<Testimony>> GetTestimonyById(string testimonyId)
        {
            var testimony = await _unitOfWork.Testimony.GetAsync(testimonyId);
            if (testimony == null)
                return Response<Testimony>.Fail("Testimony not found");

            return Response<Testimony>.Success("Success", testimony);
        }

        public async Task<Response<Testimony>> AddTestimony(AddTestimonyDto testimonyDto)
        {
            var model = _mapper.Map<Testimony>(testimonyDto);

            await _unitOfWork.Testimony.AddAsync(model);
            await _unitOfWork.CompleteAsync();

            return Response<Testimony>.Success("Successfully added", model);
        }

        public async Task<Response<string>> DeleteTestimonyById(string testimonyId)
        {
            var testimony = await _unitOfWork.Testimony.GetAsync(testimonyId);
            if (testimony == null)
                return Response<string>.Fail("Testimony not found");

            _unitOfWork.Testimony.Remove(testimony);
            await _unitOfWork.CompleteAsync();

            return Response<string>.Success("Success", $"Testimony with id {testimony.Id} deleted");
        }
    }
}
