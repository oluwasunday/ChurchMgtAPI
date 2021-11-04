using church_mgt_models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.abstractions
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Department GetDepartmentByName(string name);
        IEnumerable<Department> GetDepartmentsByUserId(string userId);
        Task<Department> GetMembersInDepartmentAsync(string departmentId);
        bool UpdateDepartment(Department dept);
    }
}