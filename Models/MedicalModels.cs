using System.Collections.Generic;

namespace FrontendEXAM.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<PrescriptionMedicine> Medicines { get; set; } = new();
    }

    public class PrescriptionMedicine
    {
        public string Name { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }

    public class AddPrescriptionRequest
    {
        public string Notes { get; set; } = string.Empty;
        public List<PrescriptionMedicine> Medicines { get; set; } = new();
    }

    public class Report
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string Diagnosis { get; set; } = string.Empty;
        public string TestRecommended { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }

    public class AddReportRequest
    {
        public string Diagnosis { get; set; } = string.Empty;
        public string TestRecommended { get; set; } = string.Empty;
        public string Remarks { get; set; } = string.Empty;
    }
}
