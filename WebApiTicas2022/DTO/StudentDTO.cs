using WebApiTicas2022.Models;

namespace WebApiTicas2022.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public decimal SemesterPrice { get; set; }

        public static implicit operator Student(StudentDTO studentDTO)
        {
            return new Student
            {
                Id = studentDTO.Id,
                Name = studentDTO.Name,
                LastName = studentDTO.LastName,
                SemesterPrice = studentDTO.SemesterPrice,
            };
        }

    }
}
