using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstAspCore.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Column("StudentName", TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Column("StudentGender", TypeName = "nvarchar(20)")]
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Standard { get; set; }
    }
}
