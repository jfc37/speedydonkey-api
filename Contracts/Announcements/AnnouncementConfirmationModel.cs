namespace Contracts.Announcements
{
    public class AnnouncementConfirmationModel
    {
        public AnnouncementConfirmationModel(int numberOfUsersEmailed)
        {
            NumberOfUsersEmailed = numberOfUsersEmailed;
        }

        public AnnouncementConfirmationModel()
        {
            
        }

        public int NumberOfUsersEmailed { get; set; }
    }
}