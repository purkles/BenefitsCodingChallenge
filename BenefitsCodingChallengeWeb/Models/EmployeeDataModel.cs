using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BenefitsCodingChallengeWeb.Models
{
    public class EmployeeDataModel
    {
       
        public EmployeeDataModel()
        {
            Employee = new PersonModel();
            Dependents = new List<PersonModel>();
        }


        [Required]
        public PersonModel Employee { get; set; }

        public List<PersonModel> Dependents { get; set; }
    }


    public class PersonModel
    {
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
    }
}