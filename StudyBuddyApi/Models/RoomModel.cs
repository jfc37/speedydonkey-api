using Common;

namespace SpeedyDonkeyApi.Models
{
    public class RoomModel : IEntity
    {
        public RoomModel()
        {
            
        }

        public RoomModel(string name, string location)
        {
            Name = name;
            Location = location;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}