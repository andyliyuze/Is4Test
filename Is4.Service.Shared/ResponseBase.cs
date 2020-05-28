namespace Is4.Service.Shared
{
    public class ResponseBase<T>  
    {
        public T Result { get; set; }

        public string Message { get; set; }
    }
}
