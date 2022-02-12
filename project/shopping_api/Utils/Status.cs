namespace shopping_api.Utils
{
    public class Status
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }

        public Status()
        {
            Success = true;
            Message = string.Empty;
            Details = string.Empty;
        }

        public void Capture(Exception ex)
        {
            Success = false;
            Message = ex.Message;
            
            if (ex.InnerException != null)
            {
                Details = ex.InnerException.Message;
            }
        }
    }
}
