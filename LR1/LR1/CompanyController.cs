using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR1 {
    [Route("/company")]
    [ApiController]
    public class CompanyController : ControllerBase {

        [HttpGet]
        public ActionResult<string> GetCompany() {
            var company = new Company();

            company.companyCeo = "Max Bukator";
            company.companyName = "Apple Inc.";
            company.companyPhone = "+380777777777";

            string companyCeo = company.companyCeo;
            string companyName = company.companyName;
            string companyPhone = company.companyPhone;

            return "Company Name: " + companyName + " Company CEO: " + companyCeo + " Company Phone: " + companyPhone;

        } 
    }
}
