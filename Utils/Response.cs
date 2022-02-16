namespace shopping_api.Utils
{
    /// <summary>
    ///     Defines an object that represents a response for requests performed to the API.
    /// This object contains two parts: the status of the response, and the returned data.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the objects inside the response.</typeparam>
    public class Response<T>
    {
        /// <summary>
        ///     Contains the status info of the response made to the API.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        ///     Contais a list of elements returned by the API.
        /// </summary>
        public List<T>? Data { get; set; }

        /// <summary>
        ///     Creates a new <see cref="Response"/> object.
        /// </summary>
        public Response()
        {
            Status = new Status();
            Data = null;
        }
    }
}
