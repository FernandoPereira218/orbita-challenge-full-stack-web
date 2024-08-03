using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace orbita.API.Models
{
    [Table("alunos")]
    public class Student
    {
        [Key]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Field must have 9 characters")]
        [Column("ra")]
        public required string StudentID { get; set; }

        [Required(ErrorMessage ="Name field is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Field must have between 3 and 200 characters")]
        [Column("nome")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email field is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Field must have between 3 and 200 characters")]
        [Column("email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "CPF field is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Field must have 11 characters")]
        [Column("cpf")]
        public required string CPF { get; set; }
    }
}
