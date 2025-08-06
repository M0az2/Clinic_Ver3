using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        public int Age { get; set; }

        [Phone]
        public string PhoneNum { get; set; }

        [Required, MaxLength(50)]
        public string Specialization { get; set; }

        [Required, DataType(DataType.Time), Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required, DataType(DataType.Time), Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();


    }
}
