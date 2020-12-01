// Copyright (c) Christopher Clayton. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace praxicloud.samples.democontainer
{
    #region Using Clauses
    using System;
    using System.Net;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using praxicloud.core.kubernetes;
    using praxicloud.core.metrics;
    using praxicloud.core.metrics.applicationinsights;
    using praxicloud.core.metrics.prometheus;
    #endregion

    /// <summary>
    /// Entry point for the console application
    /// </summary>
    class Program
    {
        #region Entry Point
        /// <summary>
        /// Entry point for the type
        /// </summary>
        static void Main()
        {
            var container = new DemoContainer("DemoContainer", new DiagnosticsConfiguration 
            { 
                LoggerFactory = GetLoggerFactory(),
                MetricFactory = GetMetricFactory()
            }, GetProbeConfiguration());

            container.StartContainer();
            container.Task.GetAwaiter().GetResult();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Retrieves the probe configuration for the health and availability probes
        /// </summary>
        /// <returns>Configuraiton for the health and availability probes</returns>
        private static IProbeConfiguration GetProbeConfiguration()
        {
            return new ProbeConfiguration
            {
                AvailabilityIPAddress = IPAddress.Any,
                AvailabilityPort = 10102,
                AvailabilityProbeInterval = TimeSpan.FromSeconds(5),
                UseTcp = true,
                HealthIPAddress = IPAddress.Any,
                HealthPort = 10101,
                HealthProbeInterval = TimeSpan.FromSeconds(5)
            };
        }

        /// <summary>
        /// Returns an instance of the metric factory that instrumentation will be built from
        /// </summary>
        /// <returns>A metric factory that metric containers will be constructed from</returns>
        private static IMetricFactory GetMetricFactory()
        {
            var metricFactory = new MetricFactory();

            var prometheusPortValue = Environment.GetEnvironmentVariable("PrometheusPort");

            if (!string.IsNullOrWhiteSpace(prometheusPortValue) && int.TryParse(prometheusPortValue, out int port))
            {
                metricFactory.AddPrometheus("prom", port);
            }

            var applicationInsightsValue = Environment.GetEnvironmentVariable("InstrumentationKey");

            if (!string.IsNullOrWhiteSpace(applicationInsightsValue))
            {
                metricFactory.AddApplicationInsights("app", applicationInsightsValue);
            }

            return metricFactory;
        }

        /// <summary>
        /// Builds the logger factory with associated providers
        /// </summary>
        /// <returns>An instance of a logger factory</returns>
        private static ILoggerFactory GetLoggerFactory()
        {
            var serviceContainer = new ServiceCollection();

            serviceContainer.AddLogging(loggerBuilder =>
            {
                loggerBuilder.AddConsole();
            });

            var provider = serviceContainer.BuildServiceProvider();

            return provider.GetRequiredService<ILoggerFactory>();
        }
        #endregion
    }
}
