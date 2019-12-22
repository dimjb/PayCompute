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
    public class PayComputationService : IPayComputationService
    {
        private decimal contractualEarnings;
        private decimal overtimeHours;
        private readonly ApplicationDbContext _context;

        public PayComputationService(ApplicationDbContext ctx)
        {
            _context = ctx;
        }
        public decimal ContractualEarnings(decimal contractualHours, decimal hoursWorked, decimal hourlyRate)
        {
            if (hoursWorked < contractualHours)
            {
                contractualEarnings = hoursWorked * hourlyRate;
            }
            else
            {
                contractualEarnings = contractualHours * hourlyRate;
            }

            return contractualEarnings;
        }

        public async Task CreateAsync(PaymentRecord paymentRecord)
        {
            _context.PaymentRecords.Add(paymentRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PaymentRecord>> GetAll()
        {
            return await _context.PaymentRecords.ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllTaxYear()
        {
            var taxYears = await _context.TaxYears.ToListAsync();
            var allTaxYear = new List<SelectListItem>();
            foreach (var taxYear in taxYears)
            {
                allTaxYear.Add(new SelectListItem
                {
                    Text = taxYear.YearOfTax,
                    Value = taxYear.Id.ToString()
                });
            }

            return allTaxYear;
        }

        public async Task<PaymentRecord> GetById(int paymentId)
        {
            return await _context.PaymentRecords.FindAsync(paymentId);
        }

        public decimal NetPay(decimal totalEarnings, decimal totalDeduction) => totalEarnings - totalDeduction;

        public decimal OvertimeEarnings(decimal overtimeRate, decimal overtimeHours) => overtimeHours * overtimeRate;

        public decimal OvertimeHours(decimal hoursWorked, decimal contractualHours)
        {
            if (hoursWorked <= contractualHours)
            {
                overtimeHours = 0.00m;
            }
            else if (hoursWorked > contractualHours)
            {
                overtimeHours = hoursWorked - contractualHours;
            }
            return overtimeHours;
        }

        public decimal OvertimeRate(decimal hourlyRate) => hourlyRate * 1.5m;

        public decimal TotalDeduction(decimal tax, decimal nic, decimal studentLoanRepayment, decimal unionFees)
         => tax + nic + studentLoanRepayment + unionFees;

        public decimal TotalEarnings(decimal overtimeEarnings, decimal contractualEarnings)
        => overtimeEarnings + contractualEarnings;
    }
}
