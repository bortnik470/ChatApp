using ChatApp.Models;

namespace ChatApp.Modules.Data.Finders
{
    public class MessageManager
    {
        private readonly ApplicationDbContext _context;

        public MessageManager(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public MessageModel findMessageById(int id)
        {
            return _context.Messages?.Find(id);
        }

        public List<MessageModel> findAllMessagesByChatId(string chatId)
        {
            var messages = _context.Messages
                .Where(x => x.ChatId == chatId)
                .ToList();

            return messages;
        }

        public void addMessage(MessageModel model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }
    }
}