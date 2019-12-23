using Microsoft.AspNetCore.Mvc.Rendering;
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
        private decimal studentLoanAmount;

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


        public async Task<decimal> StudentLoanRepaymentAmount(int employeeId, decimal totalAmount)
        {
            var employee = await GetById(employeeId);
            if (employee.StudentLoan == StudentLoan.Yes && totalAmount > 1750 && totalAmount < 2000)
            {
                studentLoanAmount = 15m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2000 && totalAmount < 2250)
            {
                studentLoanAmount = 38m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
            {
                studentLoanAmount = 60m;
            }
            else if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2500)
            {
                studentLoanAmount = 83m;
            }
            else
            {
                studentLoanAmount = 0m;
            }
            return studentLoanAmount;
        }

        public async Task<decimal> UnionFees(int employeeId)
        {
            var employee = await GetById(employeeId);
            var fee = employee.UnionMember == UnionMember.Yes ? 10m : 0m;
            return fee;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllEmployeesForPayroll()
        {
            var employees = await GetAll();
            var employeesSelectList = new List<SelectListItem>();
            foreach (var emp in employees)
            {
                employeesSelectList.Add(new SelectListItem
                {
                    Text = emp.FullName,
                    Value = emp.Id.ToString()
                });
            }
            return employeesSelectList;
        }
    }
}
