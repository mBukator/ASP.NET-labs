namespace LR2.Model {
    public class CompanyModel {
        public List<Company> CompanyList {  get; set; }
        public Company CompanyWithMostEmployees { get; set; } 

    }

    public class Company {
        public string Name { get; set; }
        public int Employees { get; set; }
    }

    public class CompaniesContainer {
        public List<Company> Companies { get; set; }
    }

}
