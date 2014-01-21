// FILE:        IMonitor.cs
// Purpose:     Provides interface for a custom monitor/locking class to assist with locating and debugging when/where deadlocks occur...
// See:         TimedMonitor.cs
// Author(s):   Eagan Rackley

using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Sync
{
    /// <summary>
    /// Provides interface for a custom monitor/locking class to assist with locating and debugging when/where deadlocks occur...
    /// </summary>
    interface IMonitor : IDisposable
    {
        /// <summary>
        /// Uses a monitor with timeoutCounter to enter a monitor specified object
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>A ITimedMonitor object that can be used to catch deadlocks with</returns>
        void Enter(object obj);

        /// <summary>
        /// Uses a monitor with timeoutCounter to enter a monitor specified object
        /// </summary>
        /// <param name="obj">The object</param>
        /// <param name="timeout">The timeoutCounter.</param>
        /// <returns>
        /// A ITimedMonitor object that can be used to catch deadlocks with
        /// </returns>
        void Enter(object obj, TimeSpan timeout);
    }
}
