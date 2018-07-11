using System;
using BenefitsCodingChallenge.Entities;

namespace BenefitsCodingChallenge
{
    public interface IBenefitsService
    {
        BenefitsCostResult GetEmployeeCost(EmployeeData employee);
    }


    public class BenefitsService : IBenefitsService
    {

        private const decimal PayAmount = 2000.00m;
        private const int PayPeriods = 26;
        private const decimal EmployeeBenefitsCostPerYear = 1000.00m;
        private const decimal DependentBenefitsCostPerYear = 500.00m;
        private const decimal Discount = 0.9m;

        private decimal _benefitCostForEmployee;
        private decimal _benefitCostForDependents;

        public BenefitsCostResult GetEmployeeCost(EmployeeData employeeData)
        {
            BenefitsCostResult costResult = new BenefitsCostResult();

            try
            {
                var totalBenefitsCostPerYear = GetTotalBenefitsCostsForEmployee(employeeData);

                var totalEmployeeCostPerPayPeriod =
                    GetEmployeeCostPerPayPeriod(totalBenefitsCostPerYear);

                var totalEmployeeCostPerYear =
                    GetEmployeeCostPerYear(totalBenefitsCostPerYear);

                var benefitCostForEmployeeOnly = _benefitCostForEmployee;
                var benefitCostForDependentsOnly = _benefitCostForDependents;

                costResult.Update(employeeData, benefitCostForEmployeeOnly, benefitCostForDependentsOnly,
                    totalBenefitsCostPerYear, totalEmployeeCostPerPayPeriod, totalEmployeeCostPerYear);
            }
            catch (Exception ex)
            {
                costResult.ErrorDetails = ex.Message;
                // log ex details to datastore here
            }

            return costResult;
        }


        public decimal GetTotalBenefitsCostsForEmployee(EmployeeData employeeData)
        {
            decimal totalBenefitsCostForEmployee;

            try
            {
                totalBenefitsCostForEmployee = GetBenefitsCost(employeeData.Employee);

                _benefitCostForEmployee = totalBenefitsCostForEmployee;

                if (employeeData.Dependents != null && employeeData.Dependents.Count > 0)
                {
                    foreach (var dependent in employeeData.Dependents)
                    {
                        _benefitCostForDependents += GetBenefitsCost(dependent);
                    }
                }

                totalBenefitsCostForEmployee += _benefitCostForDependents;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting total benefits costs for employee", ex);
            }

            return totalBenefitsCostForEmployee;
        }

        private decimal ApplyDiscount(decimal cost)
        {
            decimal result = Decimal.Round(cost * Discount, 2, MidpointRounding.ToEven);
            return result;

        }

        private decimal GetBenefitsCost(Person person)
        {
            decimal result;

            try
            {
                result = person.IsEmployee ? EmployeeBenefitsCostPerYear : DependentBenefitsCostPerYear;

                if (person.IsEligibleForDiscount)
                {
                    result = ApplyDiscount(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting benefits cost for person", ex);
            }

            return result;

        }

        public decimal GetEmployeeCostPerPayPeriod(decimal benefitsCost)
        {
            decimal totalCostPerPayPeriod;

            try
            {
                decimal benefitsCostPerPayPeriod = decimal.Round(benefitsCost / PayPeriods, 2, MidpointRounding.ToEven);

                totalCostPerPayPeriod = PayAmount + benefitsCostPerPayPeriod;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting employee cost per pay period", ex);
            }

            return totalCostPerPayPeriod;
        }

        public decimal GetEmployeeCostPerYear(decimal benefitsCostPerYear)
        {
            decimal totalCostPerYear;

            try
            {
                decimal totalPay = decimal.Round(PayAmount * PayPeriods, 2, MidpointRounding.ToEven);

                totalCostPerYear = totalPay + benefitsCostPerYear;
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting employee cost per year", ex);
            }

            return totalCostPerYear;
        }
    }
}
