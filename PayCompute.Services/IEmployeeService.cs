using Microsoft.AspNetCore.Mvc.Rendering;
using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee newEmployee);
        Task<Employee> GetById(int employeeId);
        Task UpdateAsync(Employee employee);
        Task UpdateAsync(int employeeId);
        Task Delete(int employeeId);
        Task<decimal> UnionFees(int employeeId);
        Task<decimal> StudentLoanRepaymentAmount(int employeeId, decimal totalAmount);
        Task<IEnumerable<Employee>> GetAll();
        Task<IEnumerable<SelectListItem>> GetAllEmployeesForPayroll();
    }
}
