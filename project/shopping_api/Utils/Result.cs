namespace shopping_api.Utils
{
    public class Result<T>
    {
        public Status Status { get; set; }

        public List<T>? Data { get; set; } 
        
        public Result()
        {
            Status = new Status();
            Data = null;
        }
    }
}
