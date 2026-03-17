namespace FrontendEXAM.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string AppointmentDate { get; set; } = string.Empty;
        public string TimeSlot { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public int ClinicId { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
        public Prescription? Prescription { get; set; }
        public Report? Report { get; set; }
        public QueueEntry? QueueEntry { get; set; }
    }

    public class BookAppointmentRequest
    {
        public string AppointmentDate { get; set; } = string.Empty;
        public string TimeSlot { get; set; } = string.Empty;
    }
}
