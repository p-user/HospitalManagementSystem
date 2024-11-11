
namespace Authentication.Authentication.Entities
{
    public class ApplicationRole : IdentityRole, IEntity<String>
    {
        public override string Id { get => base.Id; set => base.Id =value; }
        public DateTime? LastUpdate { get ; set; }
        public string? CreatedBy { get ; set ; }
        public DateTime? CreatedAt { get; set; }
        public string? LastUpdatedBy { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
