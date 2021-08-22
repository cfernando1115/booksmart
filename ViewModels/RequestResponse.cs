namespace BookSmart.ViewModels
{
    public class RequestResponse<T>
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
