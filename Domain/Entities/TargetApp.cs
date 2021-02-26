namespace Domain.Entities
{
    public class TargetApp : AuditableEntityBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public uint Interval { get; set; }
        public bool IsActive { get; set; }

    }
}