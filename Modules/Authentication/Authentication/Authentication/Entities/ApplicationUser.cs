
using Authentication.Data.Constants;
using Authentication.DomainEvents;
using System.Security.Claims;

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

        //Roles
        private readonly List<ApplicationUserRole> _userRoles = new();
        public IReadOnlyCollection<ApplicationUserRole> UserRoles => _userRoles.AsReadOnly();

        //claims
        private readonly List<IdentityUserClaim<string>> _claims = new();
        public IReadOnlyCollection<IdentityUserClaim<string>> Claims => _claims.AsReadOnly();


      

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

        public void AddRole(ApplicationUserRole role)
        {
            if (!_userRoles.Contains(role))
            {
                _userRoles.Add(role);
            }
           

        }

        public void AddClaim(IdentityUserClaim<string> claim)
        {
            if (!_claims.Contains(claim))
            {
                _claims.Add(claim);
            }
        }

        public void RemoveRole(ApplicationUserRole role)
        {
            if (_userRoles.Contains(role))
            {
                _userRoles.Remove(role);
            }
        }

        public void RemoveClaim(IdentityUserClaim<string> claim)
        {
            if (_claims.Contains(claim))
            {
                _claims.Remove(claim);
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
