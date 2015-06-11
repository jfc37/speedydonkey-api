using System.Linq;
using System.Web.Http;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserAnnouncementApiController : BaseApiController
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Announcement> _announcementRepository;
        private readonly ICurrentUser _currentUser;
        private readonly ICommonInterfaceCloner _cloner;

        public UserAnnouncementApiController(
            IRepository<User> userRepository, 
            IRepository<Announcement> announcementRepository, 
            ICurrentUser currentUser,
            ICommonInterfaceCloner cloner)
        {
            _userRepository = userRepository;
            _announcementRepository = announcementRepository;
            _currentUser = currentUser;
            _cloner = cloner;
        }

        public IHttpActionResult Get()
        {
            var user = _userRepository.GetWithChildren(_currentUser.Id, new[] {"EnroledBlocks", "EnroledBlocks.Announcements"});
            var globalAnnouncements = _announcementRepository
                .GetAll()
                .Where(x => x.NotifyAll && x.ShouldShowBanner());

            var userSpecificAnnouncements = user.EnroledBlocks.SelectMany(x => x.Announcements)
                .OfType<Announcement>()
                .Where(x => x.ShouldShowBanner());

            var allAnnouncementsToShow = globalAnnouncements.Union(userSpecificAnnouncements).ToList();

            if (allAnnouncementsToShow.NotAny())
                return NotFound();

            return Ok(allAnnouncementsToShow.Select(x => new AnnouncementModel().CloneFromEntity(x, _cloner)));
        }
    }
}