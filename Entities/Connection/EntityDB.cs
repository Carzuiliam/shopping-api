using Microsoft.Data.Sqlite;

namespace Shopping_API.Entities.Connection
{
    public class EntityDB
    {
        /// <summary>
        ///     Contains the connection string utilized to access the database.
        /// </summary>
        private static readonly string CONNECTION_STRING = "DataSource=D:/Projects/Source Codes/C#/CS-ShoppingApi/Database/DataSource/db_main.sqlite";

        /// <summary>
        ///     Contains the SQl connection with the database.
        /// </summary>
        private static readonly SqliteConnection DBConnection = new(CONNECTION_STRING);

        /// <summary>
        ///     Contains the current transaction being performed in the database.
        /// </summary>
        public SqliteTransaction? CurrentTransaction { get; set; }

        /// <summary>
        ///     Starts a EntityDB object. This opens a new connection with the database Also, 
        /// this method allows the object to perform transaction into the same database.
        /// </summary>
        public void Start()
        {
            if (DBConnection.State == System.Data.ConnectionState.Closed)
            {
                DBConnection.Open();
            }

            if (CurrentTransaction != null)
            {
                CurrentTransaction = null;
            }
        }

        /// <summary>
        ///     Opens a new transaction on the database. All database operations after this method call
        /// will only be commited when the <see cref="Commit()"/> method is called.
        /// </summary>
        public void Transact()
        {
            if (DBConnection.State == System.Data.ConnectionState.Closed)
            {
                DBConnection.Open();
            }

            if (CurrentTransaction == null)
            {
                CurrentTransaction = DBConnection.BeginTransaction();
            }
        }

        /// <summary>
        ///     Commits a transaction on the database. This method will only work if a previous call
        /// of the <see cref="Transact()"/> method was made before.
        /// </summary>
        public void Commit()
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Commit();
                CurrentTransaction = null;
            }
        }

        /// <summary>
        ///     Rollbacks all operations in a open transaction. This method will discard all database
        /// operation made after the call of the <see cref="Transact()"/> method.
        /// </summary>
        public void Rollback()
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Rollback();
                CurrentTransaction = null;
            }
        }

        /// <summary>
        ///     Ends the EntityDB object, closing the database connection and destroying any transaction,
        /// opened or not.
        /// </summary>
        public void Finish()
        {
            if (DBConnection.State == System.Data.ConnectionState.Open)
            {
                DBConnection.Close();
            }

            if (CurrentTransaction != null)
            {
                CurrentTransaction = null;
            }
        }

        /// <summary>
        ///     Returns a <see cref="SqliteDataReader"/> object with a result of a database query.
        /// </summary>
        /// 
        /// <param name="_command">A SQL query to be performed in the database.</param>
        ///     
        /// <returns>
        ///     An <see cref="SqliteDataReader"/> object with the result of the query.
        /// </returns>
        public SqliteDataReader Query(string _command)
        {
            return Query(_command, null);
        }

        /// <summary>
        ///     Returns a <see cref="SqliteDataReader"/> object with a result of a database query,
        /// but performed in the context of a transaction.
        /// </summary>
        /// 
        /// <param name="_command">A SQL query to be performed in the database.</param>
        /// <param name="_transaction">A <see cref="SqliteTransaction"/> object.</param>
        ///     
        /// <returns>
        ///     An <see cref="SqliteDataReader"/> object with the result of the query.
        /// </returns>
        public SqliteDataReader Query(string _command, SqliteTransaction? _transaction)
        {
            SqliteCommand command = DBConnection.CreateCommand();
            command.CommandText = _command;

            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }

            return command.ExecuteReader();
        }

        /// <summary>
        ///     Performs a non-query against the database, returning the number of affected columns.
        /// </summary>
        /// 
        /// <param name="_command">A SQL query to be performed in the database.</param>
        /// 
        /// <returns>
        ///     The number of affected rows.
        /// </returns>
        public int NonQuery(string _command)
        {
            return NonQuery(_command, null);
        }

        /// <summary>
        ///     Performs a non-query against the database, but performed in the context of a transaction,
        /// and returning the number of affected columns.
        /// </summary>
        /// 
        /// <param name="_command">A SQL query to be performed in the database.</param>
        /// <param name="_transaction">A <see cref="SqliteTransaction"/> object.</param>
        /// 
        /// <returns>
        ///     The number of affected rows.
        /// </returns>
        public int NonQuery(string _command, SqliteTransaction? _transaction)
        {
            SqliteCommand command = DBConnection.CreateCommand();
            command.CommandText = _command;

            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }

            return command.ExecuteNonQuery();
        }        
    }
}
