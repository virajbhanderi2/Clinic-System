namespace FrontendEXAM.Models
{
    public class Clinic
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int UserCount { get; set; }
        public int PatientCount { get; set; }
        public int DoctorCount { get; set; }
        public int ReceptionistCount { get; set; }
    }
}
