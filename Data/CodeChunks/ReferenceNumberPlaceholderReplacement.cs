using System;

namespace Data.CodeChunks
{
    public class ReferenceNumberPlaceholderReplacement : ICodeChunk<string>
    {
        private readonly string _stringWithPlaceholder;
        private readonly Guid _referenceNumber;

        public ReferenceNumberPlaceholderReplacement(string stringWithPlaceholder, Guid referenceNumber)
        {
            _stringWithPlaceholder = stringWithPlaceholder;
            _referenceNumber = referenceNumber;
        }

        public string Do()
        {
            return _stringWithPlaceholder.Replace("{referenceNumber}", _referenceNumber.ToString());
        }
    }
}