namespace ChatApp.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string ChatId { get; set; }
        public string WhoSendId { get; set; }
        public string WhoSendNick { get; set; }
    }
}