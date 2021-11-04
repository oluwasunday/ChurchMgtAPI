using church_mgt_dtos.DepartmentDtos;
using church_mgt_dtos.Dtos;
using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.services.interfaces
{
    public interface IDepartmentService
    {
        Task<Response<AppUser>> AddMemberToDepartment(string userId, string departmentId);
        Task<Response<AddDepartmentResponseDto>> CreateDepartmentAsync(AddDepartmentDto departmentDto);
        Task<Response<string>> DeleteDepartmentByIdAsync(string departmentId);
        Response<IEnumerable<Department>> DepartmentsByUserId(string userId);
        Response<IEnumerable<AddDepartmentResponseDto>> GetAllDepartments();
        Task<Response<AddDepartmentResponseDto>> GetDepartmentById(string departmentId);
        Task<Response<MembersInDeptDto>> GetMembersInDepartmentAsync(string departmentId);
        Task<Response<AddDepartmentResponseDto>> UpdateDepartment(string deptId, AddDepartmentDto departmentDto);
    }
}