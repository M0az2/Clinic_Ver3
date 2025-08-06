using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        public string FullName {get; set;}
        [Required]
        [Range(0,int.MaxValue,ErrorMessage = "the age field should be Positive number")]
        public int Age {  get; set;}

        [Phone, Required]
        public string PhoneNum { get; set;}
    }
}
