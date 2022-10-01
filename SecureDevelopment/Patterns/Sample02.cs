using Microsoft.Data.Sqlite;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    internal class Sample02
    {
        public void Main(string[] args)
        {
            //SqliteFactory
            //OracleClientFactory

            //DbProviderFactory


            LogSaver oracleLogSaver = new LogSaver(new OracleClientFactory());
            oracleLogSaver.Save(new LogEntry[] { new LogEntry(), new LogEntry() });

            LogSaver sqliteLogSaver = new LogSaver(SqliteFactory.Instance);
            oracleLogSaver.Save(new LogEntry[] { new LogEntry(), new LogEntry() });
        }
    }

    public class LogEntry
    {
        public string? Text { get; set; }
    }

    public class LogSaver
    {
        private readonly DbProviderFactory _factory;

        public LogSaver(DbProviderFactory factory)
        {
            _factory = factory;
        }

        public void Save(IEnumerable<LogEntry> logs)
        {
            using (var dbConnection = _factory.CreateConnection())
            {
                SetConnectionString(dbConnection);

                using (var dbCommand = _factory.CreateCommand())
                {
                    SetCommandArguments(logs);

                    dbCommand.ExecuteNonQuery();
                }

            }
        }


        private void SetConnectionString(DbConnection? connection) { }
        private void SetCommandArguments(IEnumerable<LogEntry> logEntries) { }
    }
}
