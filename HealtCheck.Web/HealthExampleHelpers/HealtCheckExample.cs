using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealtCheck.Web.HealthExampleHelpers
{
	public class HealtCheckExample : IHealthCheck
	{
		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
		{
			var healtCheckResultHealty = false;




			if (healtCheckResultHealty)
			{
				return Task.FromResult(
					HealthCheckResult.Healthy(
						description: "Bu mesaj servisin saglikli oldugunu gostermektedir!"));
			}

			return Task.FromResult(
				HealthCheckResult.Unhealthy(
					description: "Bu mesaj servisin sagliksiz oldugunu gostermektedir!"));
		}
	}
}
