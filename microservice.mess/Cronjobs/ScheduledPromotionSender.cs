using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using microservice.mess.Repositories;
using microservice.mess.Models;
using microservice.mess.Services;
namespace microservice.mess.Cronjobs
{
    public class ScheduledPromotionSender
    {
        private readonly ILogger<ScheduledPromotionSender> _logger;
        private readonly ZaloPromotionRepository _promotionRepo;
        private readonly GroupMemberRepository _groupMemberRepo;
        private readonly TokenRepository _tokenRepo;
        private readonly ZaloService _zaloService;

        public ScheduledPromotionSender(
            ZaloPromotionRepository promotionRepo,
            ILogger<ScheduledPromotionSender> logger,
            GroupMemberRepository groupMemberRepo,
            TokenRepository tokenRepo,
            ZaloService zaloService)
        {
            _promotionRepo = promotionRepo;
            _groupMemberRepo = groupMemberRepo;
            _tokenRepo = tokenRepo;
            _zaloService = zaloService;
            _logger = logger;
        }

        // public async Task RunAsync()
        // {
        //     var now = DateTime.UtcNow.AddHours(7); // Giờ VN

        //     //   15:00  27/06/2025
        //     if (now.Date == new DateTime(2025, 6, 27) && now.Hour == 16 && now.Minute == 41)
        //     {
        //         Console.WriteLine("Đúng lịch hẹn gửi promotion #tintuc!");

        //         var promotion = await _promotionRepo.GetPromotionByIdAsync("tintuc");
        //         if (promotion == null)
        //         {
        //             Console.WriteLine(" Không tìm thấy promotion 'tintuc'");
        //             return;
        //         }
        //         _logger.LogInformation("Nội dung log promotion..." + promotion);

        //         var users = await _groupMemberRepo.ListUserAsync();
        //         if (users == null || !users.Any())
        //         {
        //             Console.WriteLine(" Không có user nào để gửi.");
        //             return;
        //         }
        //         _logger.LogInformation("Nội dung log users..." + users);

        //         var token = await _tokenRepo.GetLatestTokenAsync(); // hoặc theo OA cụ thể
        //         _logger.LogInformation("Nội dung log token..." + token);
        //         if (token == null || token.ExpiredAt <= DateTime.UtcNow)
        //         {
        //             Console.WriteLine(" Token hết hạn → đang gọi refresh...");
        //             token = await _tokenRepo.RefreshTokenAsync(token?.RefreshToken ?? "");

        //             if (token == null)
        //             {
        //                 Console.WriteLine("Không refresh được token mới.");
        //                 return;
        //             }

        //             Console.WriteLine(" Refresh thành công.");
        //         }


        //         foreach (var userId in users)
        //         {
        //             try
        //             {
        //                 await _zaloService.SendPromotionToUser(
        //                     userId: userId,
        //                     header: promotion.Header,
        //                     texts: promotion.Texts.ToArray(),
        //                     accessToken: token.AccessToken,
        //                     buttons: promotion.Buttons,
        //                     bannerAttachmentIds: promotion.BannerAttachmentIds ?? new List<string>()
        //                 );
        //             }
        //             catch (Exception ex)
        //             {
        //                 Console.WriteLine($" Gửi cho {userId} lỗi: {ex.Message}");
        //             }
        //         }

        //         Console.WriteLine(" Đã gửi xong cho toàn bộ user!");
        //     }
        //     else
        //     {
        //         Console.WriteLine(" Chưa đến giờ gửi promotion.");
        //     }
        // }
    }

}