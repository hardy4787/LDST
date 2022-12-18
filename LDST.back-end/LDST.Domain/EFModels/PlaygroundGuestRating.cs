namespace LDST.Domain.EFModels
{
    public class PlaygroundGuestRating
    {
        public int Id { get; set; }
        public int Rating { get; set; }

        public Guest Guest { get; set; } = new();
        public Playground Playground { get; set; } = null!;
    } 
}
