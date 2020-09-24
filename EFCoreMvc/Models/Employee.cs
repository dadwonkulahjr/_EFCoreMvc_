using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreMvc.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [Column(name:"FullName", TypeName ="nvarchar(50)")]
        [MaxLength(50, ErrorMessage ="Name cannot exceed 50 characters.")]
        public string Name { get; set; }
        [Required, RegularExpression(pattern: @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage ="Invalid email address.")]
        [Display(Name ="Office Email")]
        [Column(name:"Office Email",TypeName = "nvarchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        [Required]
        [Column(name: "Sex", TypeName = "nvarchar(20)")]
        public Gender? Gender { get; set; }
        [Required]
        //[Column(name: "Department", TypeName = "nvarchar(20)")]
        public Department? Departmen { get; set; }
        public int SomeTestProperty { get; set; }
    }
}
