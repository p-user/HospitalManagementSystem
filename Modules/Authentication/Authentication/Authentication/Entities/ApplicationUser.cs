
namespace Authentication.Authentication.Entities
{
    public class ApplicationUser : IdentityUser,IEntity<string>
    {
       
        public override string Id { get => base.Id; set => base.Id = value; } 
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? OtpExpiration { get; set; }
        public bool IsOtpVerified { get; set; }
       
    }
}
