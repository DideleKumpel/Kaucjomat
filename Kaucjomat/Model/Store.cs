using SQLite;


namespace Kaucjomat.Model
{
    [Table("Stores")]
    class Store
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique, MaxLength(100)]
        public string Name { get; set; }

        public bool IsUserDefined { get; set; }
    }
}
