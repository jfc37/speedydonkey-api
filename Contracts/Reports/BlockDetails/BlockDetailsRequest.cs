namespace Contracts.Reports.BlockDetails
{
    /// <summary>
    /// Block details report request
    /// </summary>
    public class BlockDetailsRequest
    {
        public BlockDetailsRequest()
        {
            
        }

        public BlockDetailsRequest(int blockId)
        {
            BlockId = blockId;
        }

        public int BlockId { get; set; }
    }
}