namespace TapMarket.Models.Message
{
    using TapMarket.Data.Models;

    public class MessageIndexViewModel
    {
        public int Id { get; set; }

        public string ReceiverProfileImage { get; set; }

        public User Receiver { get; set; }

        public string LastOnline { get; set; }
    }
}
