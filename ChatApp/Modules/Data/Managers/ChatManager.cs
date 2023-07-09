using ChatApp.Models;

namespace ChatApp.Modules.Data.Finders
{
    public class ChatManager
    {
        private readonly ApplicationDbContext _context;

        public ChatManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public ChatModel getChatById(string chatId)
        {
            var chatModel = _context.Chats.First(x => x.ChatId == chatId);

            return chatModel;
        }

        public string getChatNameById(string chatId)
        {
            var chatName = _context.Chats?.First(x => x.ChatId == chatId).ChatName ?? "";

            return chatName;
        }

        public List<ChatModel> getChatsByUserId(string userId)
        {
            var chats = _context.Chats?
                        .Where(x => x.UserId == userId)
                        .ToList();

            return chats;
        }

        public string createNewChat(UserModel firstUser, UserModel secondUser)
        {
                var chatId = Guid.NewGuid().ToString();
                _context.Chats.Add(new ChatModel { ChatId = chatId, UserId = firstUser.Id, ChatName = secondUser.UserName });
                _context.Chats.Add(new ChatModel { ChatId = chatId, UserId = secondUser.Id, ChatName = firstUser.UserName });
                _context.SaveChanges();
                return chatId;
        }
    }
}
