using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SimpleReport.Template.Core.Repository;

namespace SimpleReport.Template.Infrastructure.Repository
{
    public class SqlServerRepository : IRepositoryForSql, IRepositoryCommon
    {
        protected readonly string connectionString;

        public SqlServerRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DataTable GetDataTable(IRepositoryCommand commandRepository)
        {
            var table = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(commandRepository.CommandName, connection))
                {
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var parameter in commandRepository.Parameters)
                        {
                            command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                        }

                        connection.Open();
                        adapter.Fill(table);
                    }
                }
            }

            return table;
        }

        public int ExecuteInsert(IRepositoryCommand commandRepository)
        {
            var returnValue = new SqlParameter { Direction = ParameterDirection.ReturnValue };

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(commandRepository.CommandName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.Add(returnValue);

                    foreach (var parameter in commandRepository.Parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();                    
                }
            }

            return (int)returnValue.Value;
        }

        public void ExecuteCommand(IRepositoryCommand commandRepository)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(commandRepository.CommandName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;                   

                    foreach (var parameter in commandRepository.Parameters)
                    {
                        command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<TEntity> GetList<TEntity>(IRepositoryCommand command) where TEntity : new()
        {
            return GetList<TEntity>(GetDataTable(command));
        }

        public TEntity GetSingle<TEntity>(IRepositoryCommand command) where TEntity : new()
        {
            var table = GetDataTable(command);

            if (table.Rows.Count == 0)
                return default(TEntity);

            return GetSingle<TEntity>(table.Rows[0]);
        }

        protected IEnumerable<TEntity> GetList<TEntity>(DataTable table) where TEntity : new()
        {
            var list = new List<TEntity>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(GetSingle<TEntity>(row));
            }

            return list;
        }

        protected TEntity GetSingle<TEntity>(DataRow row) where TEntity : new()
        {
            TEntity instance = new TEntity();

            foreach (var prop in instance.GetType().GetProperties())
            {
                if (!row.Table.Columns.Contains(prop.Name))
                    continue;

                if (row[prop.Name] == DBNull.Value)
                    continue;

                prop.SetValue(instance, Convert.ChangeType(row[prop.Name], prop.PropertyType), null);
            }

            return instance;
        }
    }
}