namespace Chat.Models
{
    public partial class HiddenMessage : BaseModel
    {
        public HiddenMessage(int messageId, int userId)
        {
            MessageId = messageId; 
            UserId = userId;
        }

        public int UserId { get; set; }
        public int MessageId { get; set; }

        public virtual Message Message { get; set; }
        public virtual User User { get; set; }
    }
}
