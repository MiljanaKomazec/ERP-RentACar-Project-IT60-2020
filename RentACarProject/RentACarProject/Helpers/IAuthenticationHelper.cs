using RentACarProject.DTO;

namespace RentACarProject.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticationPrincipal(Principal principal);
        public string GenerateJwt(Principal principal);
    }
}
