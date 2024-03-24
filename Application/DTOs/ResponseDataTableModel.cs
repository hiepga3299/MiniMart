namespace MiniMart.Application.DTOs
{
    public class ResponseDataTableModel<T>
    {
        public int Draw { get; set; }
        public int RecordsFilltered { get; set; }
        public int RecordsTotal { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
