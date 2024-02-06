namespace VillaMVCProj.Services
{
    public class StudentDataService
    {
        public StudentDataService() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public List<StudentDataService> GetSudentData()
        {
            List<StudentDataService> studentDataServices = new List<StudentDataService>() { 
             new StudentDataService() {
                Id = 1,
                Age = 21,
                Name = "John",
                City= "Toronto"
            },
            new StudentDataService() {
                Id = 2,
                Age = 22,
                Name = "Edward",
                City= "Ottawa"
            },
            new StudentDataService() {
                Id = 3,
                Age = 23,
                Name = "Jack",
                City= "New York"
            },
            new StudentDataService() {
                Id = 4,
                Age = 22,
                Name = "Peter",
                City= "Ottawa"
            }};           
            return studentDataServices;
        }
    }
}
