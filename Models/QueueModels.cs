namespace FrontendEXAM.Models
{
    public class QueueEntry
    {
        public int Id { get; set; }
        public int TokenNumber { get; set; }
        public string Status { get; set; } = string.Empty;
        public string QueueDate { get; set; } = string.Empty;
        public int AppointmentId { get; set; }
        public QueueAppointment? Appointment { get; set; }
    }

    public class QueueAppointment
    {
        public QueuePatient? Patient { get; set; }
    }

    public class QueuePatient
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class QueueUpdateRequest
    {
        public string Status { get; set; } = string.Empty;
    }

    public class DoctorQueueItem
    {
        public int Id { get; set; }
        public int TokenNumber { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
    }
}
