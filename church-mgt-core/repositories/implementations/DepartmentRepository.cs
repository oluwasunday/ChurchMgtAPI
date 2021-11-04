using church_mgt_core.repositories.abstractions;
using church_mgt_database;
using church_mgt_dtos.DepartmentDtos;
using church_mgt_dtos.Dtos;
using church_mgt_models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_core.repositories.implementations
{
    public class DepartmentRepository : Repositories<Department>, IDepartmentRepository
    {
        private readonly ChurchDbContext _context;
        private readonly DbSet<Department> _dbSet;
        public DepartmentRepository(ChurchDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Department>();
        }

        public bool UpdateDepartment(Department dept)
        {
            if (dept == null)
                return false;

            _context.Departments.Update(dept);
            return true;
        }

        public Department GetDepartmentByName(string name)
        {
            var dept = _context.Departments.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
            return dept;
        }

        public IEnumerable<Department> GetDepartmentsByUserId(string userId)
        {
            var depts = _dbSet.Include(x => x.AppUsers).Where(y => y.Id == userId);
            //var depts = _context.Departments.Where(x => x == userId);
            return depts;
        }

        public async Task<Department> GetMembersInDepartmentAsync(string departmentId)
        {
            var dept = await _context.Departments
                .Where(x => x.Id == departmentId).Include(x => x.AppUsers).Take(1).FirstOrDefaultAsync();
            return dept;
        }
    }
}
