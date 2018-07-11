using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BenefitsCodingChallenge;
using BenefitsCodingChallenge.Entities;
using BenefitsCodingChallengeWeb.Models;

namespace BenefitsCodingChallengeWeb.Controllers
{
    public class BenefitsController : Controller
    {
        public Lazy<IBenefitsService> BenefitsService = new Lazy<IBenefitsService>(() => new BenefitsService());


        public ActionResult Index()
        {
            var employee = new EmployeeDataModel();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Calculate(EmployeeDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var employeeData = new EmployeeData(model.Employee.FirstName, model.Employee.LastName);

            if (model.Dependents != null && model.Dependents.Count > 0)
            {
                var dependents = new List<Person>();
                foreach (var dependent in model.Dependents)
                {
                    var employeeDependent = new Person(dependent.FirstName, dependent.LastName);
                    dependents.Add(employeeDependent);
                }

                employeeData.Dependents = dependents;
            }

            var response = BenefitsService.Value.GetEmployeeCost(employeeData);

            return View("View", response);
        }

        public ActionResult AddNewDependent()
        {
            var dependent = new PersonModel();
            return PartialView("_Dependent", dependent);
        }
    }
}