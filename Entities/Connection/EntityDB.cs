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

        public void Start()
        {
            DBConnection.Open();
        }

        public void Transact()
        {
            DBConnection.Open();
            CurrentTransaction = DBConnection.BeginTransaction();
        }

        public void Commit()
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Commit();
            }
        }

        public void Rollback()
        {
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Rollback();
            }
        }

        public void Finish()
        {
            DBConnection.Close();
            CurrentTransaction = null;
        }

        public SqliteDataReader Query(string _command)
        {
            return Query(_command, null);
        }

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

        public int NonQuery(string _command)
        {
            return NonQuery(_command, null);
        }

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
