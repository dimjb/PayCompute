using Microsoft.EntityFrameworkCore;
using PayCompute.Entity;
using PayCompute.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public async Task CreateAsync(Employee newEmployee)
        {
             _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetById(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task Delete(int employeeId)
        {
            var employee = await GetById(employeeId);
            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int employeeId)
        {
            var employee = await GetById(employeeId);
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }


        public decimal StudentLoanRepaymentAmount(int employeeId, decimal totalAmount)
        {
            throw new NotImplementedException();
        }

        public decimal UnionFees(int employeeId)
        {
            throw new NotImplementedException();
        }

    }
}
