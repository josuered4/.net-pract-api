using WebApiTicas2022.DTO;

namespace WebApiTicas2022.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? LastName { get; set; }
        public decimal SemesterPrice { get; set; }

        public bool IsActive { get; set; }

        public static explicit operator StudentDTO(Student studentDTO)
        {
            return new StudentDTO
            {
                Id = studentDTO.Id,
                Name = studentDTO.Name,
                LastName = studentDTO.LastName,
                SemesterPrice = studentDTO.SemesterPrice,
            };
        }

    }
}
