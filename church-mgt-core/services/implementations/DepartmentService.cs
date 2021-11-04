using AutoMapper;
using church_mgt_core.services.interfaces;
using church_mgt_core.UnitOfWork.interfaces;
using church_mgt_database;
using church_mgt_dtos.DepartmentDtos;
using church_mgt_dtos.Dtos;
using church_mgt_models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.services.implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ChurchDbContext _context;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, ChurchDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        public async Task<Response<AddDepartmentResponseDto>> CreateDepartmentAsync(AddDepartmentDto departmentDto)
        {
            var confirmDept = _unitOfWork.Department.GetDepartmentByName(departmentDto.Name);
            if (confirmDept != null)
                return Response<AddDepartmentResponseDto>.Fail("Department already exist", StatusCodes.Status409Conflict);

            var dept = _mapper.Map<Department>(departmentDto);

            await _unitOfWork.Department.AddAsync(dept);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddDepartmentResponseDto>(dept);

            return Response<AddDepartmentResponseDto>.Success("Successful", response);
        }

        public Response<IEnumerable<AddDepartmentResponseDto>> GetAllDepartments()
        {
            var depts = _unitOfWork.Department.GetAll();
            if (depts == null)
                return Response<IEnumerable<AddDepartmentResponseDto>>.Fail("Data is empty");

            var response = _mapper.Map<IEnumerable<AddDepartmentResponseDto>>(depts);

            return Response<IEnumerable<AddDepartmentResponseDto>>.Success("Successful", response);
        }

        public async Task<Response<AddDepartmentResponseDto>> GetDepartmentById(string departmentId)
        {
            var depts = await _unitOfWork.Department.GetAsync(departmentId);
            if (depts == null)
                return Response<AddDepartmentResponseDto>.Fail("Data is empty");

            var response = _mapper.Map<AddDepartmentResponseDto>(depts);

            return Response<AddDepartmentResponseDto>.Success("Successful", response);
        }

        public async Task<Response<AddDepartmentResponseDto>> UpdateDepartment(string deptId, AddDepartmentDto departmentDto)
        {
            var dept = await _unitOfWork.Department.GetAsync(deptId);
            if(dept == null)
                return Response<AddDepartmentResponseDto>.Fail($"Department with id {deptId} not found");

            dept.Name = departmentDto.Name;
            dept.Description = departmentDto.Description;
            dept.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Department.UpdateDepartment(dept);
            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<AddDepartmentResponseDto>(dept);

            return Response<AddDepartmentResponseDto>.Success($"Department with id {dept.Id} Successfully updated", response);
        }

        public async Task<Response<string>> DeleteDepartmentByIdAsync(string departmentId)
        {
            var dept = await _unitOfWork.Department.GetAsync(departmentId);
            if (dept == null)
                return Response<string>.Fail("No data found!");

            _unitOfWork.Department.Remove(dept);
            await _unitOfWork.CompleteAsync();


            return Response<string>.Success("Successfully removed", "No data", StatusCodes.Status204NoContent);
        }

        public async Task<Response<AppUser>> AddMemberToDepartment(string userId, string departmentId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                return Response<AppUser>.Fail("User not found");

            var dept = await _unitOfWork.Department.GetAsync(departmentId);
            if(dept == null)
                return Response<AppUser>.Fail("Department not found");

            user.Departments = new List<Department> { dept };
            
            _context.Users.Update(user);
            await _unitOfWork.CompleteAsync();

            return Response<AppUser>.Success("Successfully add to department", user);
        }

        public Response<IEnumerable<Department>> DepartmentsByUserId(string userId)
        {
            if (userId == null)
                return Response<IEnumerable<Department>>.Fail("User id is required");

            var depts = _unitOfWork.Department.GetDepartmentsByUserId(userId);
            if (depts == null)
                return Response<IEnumerable<Department>>.Fail($"No department for user id {userId} found");

            return Response<IEnumerable<Department>>.Success("Success", depts);
        }

        public async Task<Response<MembersInDeptDto>> GetMembersInDepartmentAsync(string departmentId)
        {
            if (departmentId == null)
                return Response<MembersInDeptDto>.Fail("Department id is required");

            var depts = await _unitOfWork.Department.GetMembersInDepartmentAsync(departmentId);
            if (depts == null)
                return Response<MembersInDeptDto>.Fail($"No department with id {departmentId} found");

            var response = _mapper.Map<MembersInDeptDto>(depts);

            return Response<MembersInDeptDto>.Success("Success", response);
        }
    }
}
