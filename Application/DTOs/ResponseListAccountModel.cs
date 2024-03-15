namespace MiniMart.Application.DTOs
{
    public class ResponseListAccountModel<T>
    {
        public int Draw { get; set; }
        public int RecordsFilltered { get; set; }
        public int RecordsTotal { get; set; }
        public object Data { get; set; }
    }
}
