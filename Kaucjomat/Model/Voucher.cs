using SQLite;

namespace Kaucjomat.Model
{
    [Table("Vouchers")]
    class Voucher
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Indexed] // Index for faster filtering by store
        public int StoreId { get; set; }

        [MaxLength(255)]
        public string BarcodeValue { get; set; }

        public decimal Amount { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsUsed { get; set; }
    }
}
