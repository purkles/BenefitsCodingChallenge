
namespace BenefitsCodingChallenge.Entities
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsEligibleForDiscount { get; }
        public bool IsEmployee { get; }


        public Person(string firstName, string lastName, bool isEmployee = false)
        {
            FirstName = firstName;
            LastName = lastName;
            IsEmployee = isEmployee;

            // employees and dependents whose first or last name starts with the character A are eligible
            IsEligibleForDiscount = FirstName.ToLower().StartsWith("a") ||
                                    LastName.ToLower().StartsWith("a");
        }
    }
}
