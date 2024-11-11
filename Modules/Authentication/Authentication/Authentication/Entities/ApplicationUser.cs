
namespace Authentication.Authentication.Entities
{
    public class ApplicationUser : IdentityUser,IAggregate<string>
    {
       
        public override string Id { get => base.Id; set => base.Id = value; } 
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? OtpExpiration { get; set; }
        public bool IsOtpVerified { get; private set; }

       

        public static ApplicationUser CreateUser(string email,string username = null, string password = null)
        {
           return  new ApplicationUser
            {
                Email = email,
                UserName = (username != null) ? username : email,
                PasswordHash = (password != null) ? password : null,
                
            };
           
           
        }

        public void VerifyOtp()
        {
            if (OtpExpiration.HasValue && OtpExpiration > DateTime.UtcNow)
            {
                IsOtpVerified = true;
            }
            else
            {
                throw new InvalidOperationException("OTP has expired.");
            }
        }



        //EVENTS : IAggregate implementation
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyList<IDomainEvent> Events => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return dequeuedEvents;
        }
    }
}
