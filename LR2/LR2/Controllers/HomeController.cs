using LR2.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;


namespace LR2.Controllers {
    public class HomeController : Controller {

        [Route("json-read")]
        public IActionResult JsonRead() {
            CompanyModel companyModel = new CompanyModel();
            companyModel.CompanyList = new List<Company>();

            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "JSON", "Companies.json");
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            var companiesContainer = JsonConvert.DeserializeObject<CompaniesContainer>(jsonContent);

            if (companiesContainer != null && companiesContainer.Companies != null) {
                companyModel.CompanyList = companiesContainer.Companies;
            }

            Company companyWithMostEmployees = companyModel.CompanyList.OrderByDescending(c => c.Employees).First();
            companyModel.CompanyWithMostEmployees = companyWithMostEmployees;

            return View("Index", companyModel);
        }



        [Route("xml-read")]
        public IActionResult XmlRead() {
            CompanyModel companyModel = new CompanyModel();
            companyModel.CompanyList = new List<Company>();

            string xmlFilePath = Path.Combine(Directory.GetCurrentDirectory(), "XML", "Companies.xml");
            string xmlString = System.IO.File.ReadAllText(xmlFilePath);
            var stringReader = new StringReader(xmlString);
            var dataSet = new System.Data.DataSet();

            dataSet.ReadXml(stringReader);

            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++) {
                companyModel.CompanyList.Add(new Company {
                    Name = (string)dataSet.Tables[0].Rows[i][0],
                    Employees = Convert.ToInt32(dataSet.Tables[0].Rows[i][1].ToString())
                });
            }

            Company companyWithMostEmployees = companyModel.CompanyList.OrderByDescending(c => c.Employees).First();
            companyModel.CompanyWithMostEmployees = companyWithMostEmployees;

            return View("Index", companyModel);
        }


        //  Start idea of realization is mine but with the rest ChatGPT helped
        [Route("ini-read")]
        public IActionResult IniRead() {
            CompanyModel companyModel = new CompanyModel();
            companyModel.CompanyList = new List<Company>();

            string iniFilePath = Path.Combine(Directory.GetCurrentDirectory(), "INI", "Companies.ini");
            var iniContent = System.IO.File.ReadAllLines(iniFilePath);

            string currentCompany = null;
            foreach (var line in iniContent) {
                if (line.StartsWith("[") && line.EndsWith("]")) {
                    currentCompany = line.Trim('[', ']');
                } else if (currentCompany != null) {
                    var parts = line.Split('=');
                    if (parts.Length == 2) {
                        if (int.TryParse(parts[1].Trim(), out int employees)) {
                            companyModel.CompanyList.Add(new Company {
                                Name = currentCompany,
                                Employees = employees
                            });
                            currentCompany = null;
                        }
                    }
                }
            }

            if (companyModel.CompanyList.Any()) {
                Company companyWithMostEmployees = companyModel.CompanyList.OrderByDescending(c => c.Employees).First();
                companyModel.CompanyWithMostEmployees = companyWithMostEmployees;
            } else {
                // Handle the case when there are no elements in CompanyList
                companyModel.CompanyWithMostEmployees = new Company { Name = "N/A", Employees = 0 };
            }

            return View("Index", companyModel); //
        }



    }
}