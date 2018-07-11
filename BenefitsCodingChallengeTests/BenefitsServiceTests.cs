using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenefitsCodingChallenge;
using BenefitsCodingChallenge.Entities;
using NUnit.Framework;

namespace BenefitsServiceTests
{
    [TestFixture]
    public class BenefitServicesTests
    {
        [Test]
        public void Test_GetBenefitsCostForEmployeeOnly()
        {
            EmployeeData employeeData = new EmployeeData("John", "Doe");

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetTotalBenefitsCostsForEmployee(employeeData);

            Assert.That(result, Is.EqualTo(1000.00m));

            return;
        }

        [Test]
        public void Test_GetBenefitsCostForEmployeeOnlyWithDiscount()
        {
            EmployeeData employeeData = new EmployeeData("Aaron", "Smith");

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetTotalBenefitsCostsForEmployee(employeeData);

            Assert.That(result, Is.EqualTo(900.00m));

            return;
        }

        [Test]
        public void Test_GetBenefitsCostForEmployeeWithOneDependentNoDiscount()
        {
            EmployeeData employeeData = new EmployeeData("Sarah", "Lee")
            {
                Dependents = new List<Person>
            {
                new Person("Bill", "Jones")
            }
            };

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetTotalBenefitsCostsForEmployee(employeeData);

            Assert.That(result, Is.EqualTo(1500.00m));

            return;
        }

        [Test]
        public void Test_GetBenefitsCostForEmployeeWithOneDependentBothDiscounts()
        {
            EmployeeData employeeData = new EmployeeData("Anna", "Waters")
            {
                Dependents = new List<Person>
            {
                new Person("Andy", "Kirk")
            }
            };

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetTotalBenefitsCostsForEmployee(employeeData);

            Assert.That(result, Is.EqualTo(1350.00m));

            return;
        }

        [Test]
        public void Test_GetBenefitsCostForEmployeeWithFiveDependentsNoDiscounts()
        {
            EmployeeData employeeData = new EmployeeData("George", "Wright")
            {
                Dependents = new List<Person>
            {
                new Person("Bob", "Wright"),
                new Person("Billy", "Wright"),
                new Person("Susan", "Wright"),
                new Person("Mary", "Wright"),
                new Person("Frank", "Wright")
            }
            };

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetTotalBenefitsCostsForEmployee(employeeData);

            Assert.That(result, Is.EqualTo(3500.00m));

            return;
        }

        [Test]
        public void Test_GetBenefitsCostForEmployeeWithFiveDependentsWithDiscounts()
        {
            EmployeeData employeeData = new EmployeeData("George", "Wright")
            {
                Dependents = new List<Person>
            {
                new Person("Bob", "Wright"),
                new Person("Allen", "Wright"),
                new Person("Alice", "Wright"),
                new Person("Mary", "Wright"),
                new Person("Frank", "Wright")
            }
            };

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetTotalBenefitsCostsForEmployee(employeeData);

            Assert.That(result, Is.EqualTo(3400.00m));

            return;
        }

        [Test]
        public void Test_GetEmployeeCostPerPayPeriod()
        {
            decimal benefitsCost = 1000.00m;

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCostPerPayPeriod(benefitsCost);

            Assert.That(result, Is.EqualTo(2038.46m));
        }

        [Test]
        public void Test_GetEmployeeCostPerPayPeriodUsingZeroCost()
        {
            decimal benefitsCost = 0.00m;

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCostPerPayPeriod(benefitsCost);

            Assert.That(result, Is.EqualTo(2000.00m));
        }

        [Test]
        public void Test_GetEmployeeCostPerPayPeriodUsingHighCost()
        {
            decimal benefitsCost = 8000.00m;

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCostPerPayPeriod(benefitsCost);

            Assert.That(result, Is.EqualTo(2000.00m));
        }

        [Test]
        public void Test_GetEmployeeCostPerYear()
        {
            decimal benefitsCost = 1000.00m;

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCostPerYear(benefitsCost);

            Assert.That(result, Is.EqualTo(53000.00m));
        }

        [Test]
        public void Test_GetEmployeeCostPerYearZeroBenefitsCost()
        {
            decimal benefitsCost = 0.00m;

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCostPerYear(benefitsCost);

            Assert.That(result, Is.EqualTo(52000.00m));
        }

        [Test]
        public void Test_GetTotalEmployeeCostDataThreeDependentsNoDiscount()
        {
            var employeeData = new EmployeeData("sam", "person")
            {
                Dependents = new List<Person>
            {
                new Person("george", "dependent"),
                new Person("four", "three"),
                new Person("james", "brown")
            }
            };

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCost(employeeData);
            Assert.That(result.BenefitCostForEmployeeOnly, Is.EqualTo(1000.00m));
            Assert.That(result.BenefitCostForDependentsOnly, Is.EqualTo(1500.00m));
            Assert.That(result.TotalBenefitsCostPerYear, Is.EqualTo(2500.00m));
            Assert.That(result.TotalEmployeeCostPerPayPeriod, Is.EqualTo(2096.15m));
            Assert.That(result.TotalEmployeeCostPerYear, Is.EqualTo(54500.00m));
            Assert.That(result.ErrorDetails, Is.Null);
            Assert.That(result.EmployeeData, !Is.Null);
        }

        [Test]
        public void Test_GetTotalEmployeeCostDataNoDependentsNoDiscount()
        {
            var employeeData = new EmployeeData("joe", "person");

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCost(employeeData);
            Assert.That(result.BenefitCostForEmployeeOnly, Is.EqualTo(1000.00m));
            Assert.That(result.BenefitCostForDependentsOnly, Is.EqualTo(0.00m));
            Assert.That(result.TotalBenefitsCostPerYear, Is.EqualTo(1000.00m));
            Assert.That(result.TotalEmployeeCostPerPayPeriod, Is.EqualTo(2038.46m));
            Assert.That(result.TotalEmployeeCostPerYear, Is.EqualTo(53000.00m));
            Assert.That(result.ErrorDetails, Is.Null);
            Assert.That(result.EmployeeData, !Is.Null);
        }

        [Test]
        public void Test_GetTotalEmployeeCostDataNoDependentsWithDiscount()
        {
            var employeeData = new EmployeeData("emily", "apple");

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCost(employeeData);
            Assert.That(result.BenefitCostForEmployeeOnly, Is.EqualTo(900.00m));
            Assert.That(result.BenefitCostForDependentsOnly, Is.EqualTo(0.00m));
            Assert.That(result.TotalBenefitsCostPerYear, Is.EqualTo(900.00m));
            Assert.That(result.TotalEmployeeCostPerPayPeriod, Is.EqualTo(2034.62m));
            Assert.That(result.TotalEmployeeCostPerYear, Is.EqualTo(52900.00m));
            Assert.That(result.ErrorDetails, Is.Null);
            Assert.That(result.EmployeeData, !Is.Null);
        }

        [Test]
        public void Test_GetTotalEmployeeCostDataOneDependentsWithDependentDiscount()
        {
            var employeeData = new EmployeeData("bob", "orange")
            {
                Dependents = new List<Person>
                {
                    new Person("alice", "apple")
                }
            };

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCost(employeeData);
            Assert.That(result.BenefitCostForEmployeeOnly, Is.EqualTo(1000.00m));
            Assert.That(result.BenefitCostForDependentsOnly, Is.EqualTo(450.00m));
            Assert.That(result.TotalBenefitsCostPerYear, Is.EqualTo(1450.00m));
            Assert.That(result.TotalEmployeeCostPerPayPeriod, Is.EqualTo(2055.77m));
            Assert.That(result.TotalEmployeeCostPerYear, Is.EqualTo(53450.00m));
            Assert.That(result.ErrorDetails, Is.Null);
            Assert.That(result.EmployeeData, !Is.Null);
        }

        [Test]
        public void Test_GetTotalEmployeeCostDataTwoDependentsWithEmployeeAndOneDependentDiscount()
        {
            var employeeData = new EmployeeData("test", "apple")
            {
                Dependents = new List<Person>
                {
                    new Person("apple", "blue"),
                    new Person("jill", "jackson")
                }
            };

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCost(employeeData);
            Assert.That(result.BenefitCostForEmployeeOnly, Is.EqualTo(900.00m));
            Assert.That(result.BenefitCostForDependentsOnly, Is.EqualTo(950.00m));
            Assert.That(result.TotalBenefitsCostPerYear, Is.EqualTo(1850.00m));
            Assert.That(result.TotalEmployeeCostPerPayPeriod, Is.EqualTo(2071.15m));
            Assert.That(result.TotalEmployeeCostPerYear, Is.EqualTo(53850.00m));
            Assert.That(result.ErrorDetails, Is.Null);
            Assert.That(result.EmployeeData, !Is.Null);
        }

        [Test]
        public void Test_GetTotalEmployeeCostDataEmployeeAndDependentsWithSpecialCharacters()
        {
            var employeeData = new EmployeeData("#$%2223%%^", "9!!~~~~%^^&*()")
            {
                Dependents = new List<Person>
                {
                    new Person("@#$%^*(", "2342334&&*&%$"),
                    new Person("*^&(^&^$%%#$222", "333$%$^%")
                }
            };

            BenefitsService benefitsService = new BenefitsService();
            var result = benefitsService.GetEmployeeCost(employeeData);
            Assert.That(result.BenefitCostForEmployeeOnly, Is.EqualTo(1000.00m));
            Assert.That(result.BenefitCostForDependentsOnly, Is.EqualTo(1000.00m));
            Assert.That(result.TotalBenefitsCostPerYear, Is.EqualTo(2000.00m));
            Assert.That(result.TotalEmployeeCostPerPayPeriod, Is.EqualTo(2076.92m));
            Assert.That(result.TotalEmployeeCostPerYear, Is.EqualTo(54000.00m));
            Assert.That(result.ErrorDetails, Is.Null);
            Assert.That(result.EmployeeData, !Is.Null);
        }
    }
}
