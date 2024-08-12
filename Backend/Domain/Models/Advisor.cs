using Domain.Extend;

namespace Domain.Models
{
    public partial class Advisor : IAuditable
    {
        public int AdvisorId { get; set; }
        public string Name { get; set; } = null!;
        public string Sin { get; set; } = null!;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? HealthStatus { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public int UpdatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
