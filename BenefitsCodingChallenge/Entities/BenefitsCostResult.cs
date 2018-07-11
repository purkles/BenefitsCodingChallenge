
namespace BenefitsCodingChallenge.Entities
{
    public class BenefitsCostResult 
    {
        public EmployeeData EmployeeData { get; set; }

        public decimal BenefitCostForEmployeeOnly { get; private set; }
        public decimal BenefitCostForDependentsOnly { get; private set; }

        public decimal TotalBenefitsCostPerYear { get; private set; }
        public decimal TotalEmployeeCostPerPayPeriod { get; private set; }
        public decimal TotalEmployeeCostPerYear { get; private set; }

        public string ErrorDetails { get; set; }

        public void Update(EmployeeData employeeData, decimal benefitCostForEmployeeOnly,
            decimal benefitCostForDependentsOnly, decimal totalBenefitsCostPerYear,
            decimal totalEmployeeCostPerPayPeriod, decimal totalEmployeeCostPerYear)
        {
            EmployeeData = employeeData;
            BenefitCostForEmployeeOnly = benefitCostForEmployeeOnly;
            BenefitCostForDependentsOnly = benefitCostForDependentsOnly;
            TotalBenefitsCostPerYear = totalBenefitsCostPerYear;
            TotalEmployeeCostPerPayPeriod = totalEmployeeCostPerPayPeriod;
            TotalEmployeeCostPerYear = totalEmployeeCostPerYear;
        }
    }
}
