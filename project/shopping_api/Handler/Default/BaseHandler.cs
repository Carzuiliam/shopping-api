using shopping_api.Utils;

namespace shopping_api.Handler.Default
{
    /// <summary>
    ///     Defines the base handler. A handler, in this context, is an object that acts as
    /// a controller extension, performing the operations needed in order to transfer data
    /// between the application and the SQL database.
    /// </summary>
    public abstract class BaseHandler<T>
    {
        /// <summary>
        ///     Contains the connection string utilized to access the database.
        /// </summary>
        protected static readonly string CONNECTION_STRING = "DataSource=D:/Projects/Source Codes/C#/CS-CartApi/project/shopping_api/Database/DataSource/db_main.sqlite";

        /// <summary>
        ///     Contains the result of a request performed by this handler.
        /// </summary>
        public Response<T> Result { get; set; }

        /// <summary>
        ///     Creates a new instance of a <see cref="BaseHandler"/>.
        /// </summary>
        public BaseHandler()
        {
            Result = new();
        }
    }
}
