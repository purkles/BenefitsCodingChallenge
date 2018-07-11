using System.Collections.Generic;

namespace BenefitsCodingChallenge.Entities
{
    public class EmployeeData
    {
        public EmployeeData(string firstName, string lastName)
        {
            Employee = new Person(firstName, lastName, true);
        }

        public Person Employee { get; set; }

        public List<Person> Dependents { get; set; }
    }
}
