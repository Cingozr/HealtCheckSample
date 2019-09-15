using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealtCheck.Web.HealthExampleHelpers
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        private SqlConnection _sqlConnection;
        public string Name => "Sql";
        public SqlServerHealthCheck(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _sqlConnection.Open();
            }
            catch (SqlException)
            {
                return Task.FromResult(
                    HealthCheckResult.Unhealthy(
                        description: "Hata : Sql ile baglanti kurulamadi"));
            }

            return Task.FromResult(
                HealthCheckResult.Healthy(
                    description: "Basarili : Sql ile baglanti kuruldu."));
        }
    }
}
