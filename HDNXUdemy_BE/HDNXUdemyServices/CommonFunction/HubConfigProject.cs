using HDNXUdemyData.Entities;
using HDNXUdemyData.IRepository;
using HDNXUdemyModel.Constant;
using HDNXUdemyModel.SystemExceptions;
using Microsoft.AspNetCore.SignalR;

namespace HDNXUdemyServices.CommonFunction
{
    public class HubConfigProject : Hub
    {
        private readonly ISystemConfigRepository _systemConfigRepository;

        public HubConfigProject(ISystemConfigRepository systemConfigRepository)
        { _systemConfigRepository = systemConfigRepository ?? throw new ProjectException(nameof(_systemConfigRepository)); }

        private int UserId
        {
            get { return int.Parse(Context?.User?.Claims.Where(x => x.Type == "user-id").FirstOrDefault()?.Value ?? "0"); }
        }

        private ERoles Role
        {
            get { return (ERoles)int.Parse(Context?.User?.Claims.Where(x => x.Type == "role-id").FirstOrDefault()?.Value ?? "0"); }
        }

        public override Task OnConnectedAsync()
        {
            try
            {
                // Tạo group để nhận thông tin cho admin
                if (Role == ERoles.Admin)
                {
                    Groups.AddToGroupAsync(Context.ConnectionId, "notificationAdmin");
                    var dataInsert = new SystemConfigEntities()
                    {
                        KeyConfig = "KeyNotificationAdmin",
                        Value = Context.ConnectionId
                    };
                    var getDataCheckExits = _systemConfigRepository.GetObject(x => x.KeyConfig == dataInsert.KeyConfig);
                    if (getDataCheckExits != null)
                    {
                        getDataCheckExits.Value = dataInsert.Value;
                        _systemConfigRepository.Update(getDataCheckExits);
                    }
                    else
                    {
                        _systemConfigRepository.Add(dataInsert);
                    }
                }
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", $"OnConnected: {Context.ConnectionId} with messenger {ex.Message}");
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        // Notification for user.
        public async Task SendNotificationCommentOfCourse(int userId, string contentMessenger)
        {
            await this.Clients.Client(userId.ToString()).SendAsync(TypeNotification.CommentOnCourse.GetEnumDescription(), contentMessenger);
        }

        public async Task SendNotificationTagOnCommentOfCourse(int userId, string contentMessenger)
        {
            await this.Clients.Client(userId.ToString()).SendAsync(TypeNotification.TagOnCommentOnCourse.GetEnumDescription(), contentMessenger);
        }

        public async Task SendNotificationUpdateOfCourse(int userId, string contentMessenger)
        {
            await this.Clients.Client(userId.ToString()).SendAsync(TypeNotification.UpdateOnCourse.GetEnumDescription(), contentMessenger);
        }

        public async Task SendNotificationDiscountOfCourse(int userId, string contentMessenger)
        {
            await this.Clients.Client(userId.ToString()).SendAsync(TypeNotification.DiscountOnCourse.GetEnumDescription(), contentMessenger);
        }

        public async Task SendNotificationPromotionOfCourse(int userId, string contentMessenger)
        {
            await this.Clients.Client(userId.ToString()).SendAsync(TypeNotification.PromotionOnCourse.GetEnumDescription(), contentMessenger);
        }

        public async Task SendNotificationUpgradeSystemOnCourse(int userId, string contentMessenger)
        {
            await this.Clients.Client(userId.ToString()).SendAsync(TypeNotification.UpgradeSystemOnCourse.GetEnumDescription(), contentMessenger);
        }
    }
}