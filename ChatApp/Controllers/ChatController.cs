using ChatApp.Models;
using ChatApp.Modules;
using ChatApp.Modules.Data.Finders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly PusherModule pusher;
        private readonly MessageManager messageManager;
        private readonly ChatManager chatManager;
        private readonly UserManager userManager;

        public ChatController(MessageManager messageManager, ChatManager chatManager, UserManager userManager)
        {
            this.userManager = userManager;
            this.messageManager = messageManager;
            this.chatManager = chatManager;
            pusher = new PusherModule();
        }

        public IActionResult Chats(string id)
        {
            Response.Cookies.Append("UserId", id, new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Secure = true
            });

            ViewBag.Chats = chatManager.getChatsByUserId(id);

            return View();
        }

        [HttpGet]
        public IActionResult GetMessanges(string chatId)
        {
            var chatInfo = messageManager.findAllMessagesByChatId(chatId);

            return Json(new { chatInfo });
        }

        [HttpPost]
        public IActionResult SendMessage(string message, string chatId, string id)
        {
            var userName = User.Identity.Name;
            pusher.sendInfo(chatId, "user-send-message", new { message, id, userName });

            messageManager.addMessage(new MessageModel { ChatId = chatId, Message = message, WhoSendId = id , WhoSendNick = userName});

            return new OkResult();
        }

        [HttpPost]
        public IActionResult AddFriend(string friendNick, string userId)
        {
            var userFriend = userManager.findUserByNick(friendNick);
            var user = userManager.findUserById(userId);

            string chatId = chatManager.createNewChat(user, userFriend);

            return Json(new { chatName = userFriend.UserName, chatId });
        }
    }
}