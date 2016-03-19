using System.Security.Claims;
using Data.CodeChunks;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class ExtractGlobalIdFromJwt : ICodeChunk<string>
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public ExtractGlobalIdFromJwt(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public string Do()
        {
            return _claimsPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
        }
    }
}