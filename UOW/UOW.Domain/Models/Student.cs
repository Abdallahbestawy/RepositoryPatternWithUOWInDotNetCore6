using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UOW.Domain.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(1000)]
        public string studentName { get; set; }
        [Required, MaxLength(14)]
        [RegularExpression(@"^\d+$", ErrorMessage = "must be numeric")]
        [Display(Name = "National ID")]
        public string NationalID { get; set; }
        [Required, MaxLength(11)]
        [Display(Name = "Phone Number")]

        public string Mobile { get; set; }
        [ForeignKey("Department")]
        public int DeptId { get; set; }
        public virtual Department Department { get; set; }
    }
}
