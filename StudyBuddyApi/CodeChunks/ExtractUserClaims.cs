using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Data.CodeChunks;
using Models;

namespace SpeedyDonkeyApi.CodeChunks
{
    public class ExtractUserClaims : ICodeChunk<IEnumerable<string>>
    {
        private readonly User _user;

        public ExtractUserClaims(User user)
        {
            _user = user;
        }

        public IEnumerable<string> Do()
        {
            return _user.IsNull() || _user.Claims.IsNullOrWhiteSpace()
                ? null
                : _user.Claims.Split(',').ToList();
        }
    }
}