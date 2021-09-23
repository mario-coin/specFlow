using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.Treinamento.TesteAutomatizado.SpecFlow.Controllers
{
    public class PostgresDatabaseController
    {
        NpgsqlConnection connection;

        public PostgresDatabaseController(NpgsqlConnection connection)
        {
            try
            {
                this.connection = connection;
                if (this.connection.State == System.Data.ConnectionState.Closed)
                {
                    this.connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível abrir a conexão com a base de dados", ex);
            }
            
        }
    
        public async Task ClearDatabase(string schema = "public")
        {
            var query = $@"DO
                        $$
                        DECLARE
                            l_stmt VARCHAR;
                            databaseschema VARCHAR:= '{schema}';
                        BEGIN
                            SELECT 'truncate ' || string_agg(format('%I.%I', schemaname, tablename), ',')
                            INTO l_stmt
                            FROM pg_tables
                            WHERE schemaname = databaseschema;

                            EXECUTE l_stmt || ' RESTART IDENTITY';
                        END;
                        $$";

            await this.connection.ExecuteAsync(query);
        }

        public async Task<IEnumerable<object>> SelectFrom(string tableName, Table table)
        {
            var selectColumns = string.Join(',', GetColumnsForSelect(table));
            var filterConditions = string.Join(" OR ", GetFilterConditions(table));

            var query = $"SELECT {selectColumns} FROM {tableName} WHERE {filterConditions}";

            return await connection.QueryAsync(query);
        }

        private string[] GetColumnsForSelect(Table table)
        {
            return table.Header.Select(s => $"{s} AS \"{s}\"").ToArray();
        }

        private string[] GetFilterConditions(Table table)
        {
            List<string> filters = new List<string>();

            for (int row = 0; row < table.Rows.Count; row++)
            {
                var rowConditions = new List<string>();

                for (int header = 0; header < table.Header.Count; header++)
                {
                    string column = table.Header.ElementAt(header);
                    string value = table.Rows[row][header];

                    rowConditions.Add($"{column} = {value}");
                }

                filters.Add($"({string.Join(" AND ", rowConditions)})");
            }

            return filters.ToArray();
        }
    }
}
