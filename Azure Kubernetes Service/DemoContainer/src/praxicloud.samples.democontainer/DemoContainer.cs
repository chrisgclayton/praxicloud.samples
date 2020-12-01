// Copyright (c) Christopher Clayton. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace praxicloud.samples.democontainer
{
    #region Using Clauses
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using praxicloud.core.exceptions;
    using praxicloud.core.kubernetes;
    using praxicloud.core.metrics;
    #endregion

    /// <summary>
    /// A sample container that demonstrates a simple container that is initiated and provides logging, metrics, availability, health probes, scale notifications etc.
    /// </summary>
    public sealed class DemoContainer : ContainerBase
    {
        #region Variables
        /// <summary>
        /// A metric that records the number of iterations that have occurred since the start of the container
        /// </summary>
        private readonly ICounter _iterationCounter;
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the type
        /// </summary>
        /// <param name="containerName">The name of the container to use in logging operations</param>
        /// <param name="diagnosticsConfiguration">A diagnostics instance to use when instantiating the container</param>
        /// <param name="probeConfiguration">The availability and liveness probe configuration</param>
        public DemoContainer(string containerName, DiagnosticsConfiguration diagnosticsConfiguration, IProbeConfiguration probeConfiguration) : base(containerName, probeConfiguration, diagnosticsConfiguration)
        {
            _iterationCounter = diagnosticsConfiguration.MetricFactory.CreateCounter("demo_iteration_counter", "Counts the number of iterations the demo container has performed.", false, null);
        }
        #endregion
        #region Properties
        /// <inheritdoc />
        public override bool NotifyOfScaleEvents => true;
        #endregion
        #region Methods
        /// <inheritdoc />
        public override Task<bool> IsAvailableAsync()
        {
            Logger.LogInformation("Availability tested");

            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Task<bool> IsHealthyAsync()
        {
            Logger.LogInformation("Health tested");

            return Task.FromResult(true);
        }

        /// <inheritdoc />
        protected override void KubernetesReplicaChange(int? previousReplicaCount, int? previousDesiredReplicaCount, int? previousReadyReplicaCount, int? newReplicaCount, int? newDesiredReplicaCount, int? newReadyReplicaCount)
        {
            Logger.LogWarning($"The replica count is changing, new desired is {newDesiredReplicaCount ?? -1}");
        }

        /// <inheritdoc />
        protected override Task StartupAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Startup invoked");

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override Task ShutdownAsync(bool startupSuccessful, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Shutdown invoked");

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        protected override bool UnhandledExceptionRaised(object sender, Exception exception, bool isTerminating, UnobservedType sourceType)
        {
            Logger.LogError(exception, "An unhandled exception was raised");

            return false;
        }
       

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Started execution loop");

            while (!ContainerCancellationToken.IsCancellationRequested)
            {
                await Task.WhenAny(Task.Delay(15000), ContainerTask).ConfigureAwait(false);

                _iterationCounter.Increment();
                Logger.LogDebug("Iteration completed");
            }

            Logger.LogWarning("Ending execution loop");
        }
        #endregion
    }
}
