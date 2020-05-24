namespace Chat.Models
{
    public partial class HiddenMessage : BaseModel
    {
        public int UserId { get; set; }
        public int MessageId { get; set; }

        public virtual Message Message { get; set; }
        public virtual User User { get; set; }
    }
}
