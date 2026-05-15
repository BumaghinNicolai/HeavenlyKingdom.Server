using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repo;
        private readonly IMapper _mapper;

        public NotificationService(INotificationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<NotificationDto>> GetByUserIdAsync(int userId)
        {
            var notifications = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<List<NotificationDto>>(notifications);
        }

        public async Task MarkAsReadAsync(int id) =>
            await _repo.MarkAsReadAsync(id);

        public async Task MarkAllAsReadAsync(int userId) =>
            await _repo.MarkAllAsReadAsync(userId);
    }
}
