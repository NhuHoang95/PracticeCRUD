namespace PracticeCRUD.Response
{
    public class ResponseType<T> where T : class
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }
}
