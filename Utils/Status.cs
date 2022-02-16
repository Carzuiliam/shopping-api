namespace shopping_api.Utils
{
    /// <summary>
    ///     Defines an object that contains the status for a request performed to the API.
    /// The status describes not only the success/failure of the request, but any error
    /// messages needed for debug purposes (if any).
    /// </summary>
    public class Status
    {
        /// <summary>
        ///     Contains the status of the request made to the API: <see cref="true"/> if
        /// success, <see cref="false"/> otherwise.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Contains the primary error message if the request failed, or
        /// <see cref="string.Empty"/> if not.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     May contains additional info in case of a request failure. If this field
        /// is not empty, it means <see cref="Message"/> is also not empty.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        ///     Creates a new Status object.
        /// </summary>
        public Status()
        {
            Success = true;
            Message = string.Empty;
            Details = string.Empty;
        }

        /// <summary>
        ///     Captures an <see cref="Exception"/>, converting it to a <see cref="Status"/>
        /// object. In general, any request received by the API will call this method if
        /// anything fails. Also, if the captured exception contains an
        /// <see cref="Exception.InnerException"/>, this last one will be captured too.
        /// </summary>
        /// 
        /// <param name="_exception">The exception to capture.</param>
        public void Capture(Exception _exception)
        {
            Success = false;
            Message = _exception.Message;
            
            if (_exception.InnerException != null)
            {
                Details = _exception.InnerException.Message;
            }
        }
    }
}
