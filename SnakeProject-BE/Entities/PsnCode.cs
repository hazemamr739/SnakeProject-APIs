namespace SnakeProject_BE.Entities
{
    public class PsnCode
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public int ProductId { get; set; }
        public int DenominationId { get; set; }
        
        public Product Product { get; set; } = default!;
        public PsnCodesDenomination Denomination { get; set; } = default!;
        
        public bool IsUsed { get; set; }
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    }
}
