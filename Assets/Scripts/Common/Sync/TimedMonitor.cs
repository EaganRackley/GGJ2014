// FILE:        TimedMonitor.cs
// Purpose:     Provides extended behavior to Monitor class to prevent Couroutines (or threads if we use them) from accessing objects that aren't thread safe
// See:         IMonitor.cs
// Author(s):   Eagan Rackley

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Common.Sync
{
    /// <summary>
    /// Implements IMonitor, which interfaces a custom monitor class to assist with locating and debugging when/where deadlocks occur...
    /// This class only works well as a persistent object, and must be disposed of in a finally statement!!!
    /// </summary>    
    class TimedMonitor : IMonitor
    {
#if DEBUG
        /// <summary>
        ///In Debug mode, we make it a class so that we can add a finalizer
        ///in order to detect when an object is not freed.) 
        /// </summary>
        public class DeadlockNotifier
        {
            /// <summary>
            /// Releases unmanaged resources and performs other cleanup operations before the
            /// <see cref="DeadlockNotifier"/> is reclaimed by garbage collection.
            /// </summary>
            ~DeadlockNotifier()
            {
                // If this finalizer runs, someone somewhere failed to call Dispose, which means we've failed to leave
                // a monitor!
                throw new System.Threading.SynchronizationLockException("Object never disposed -> meaning you've got a deadlock! (Check the stack trace)");
            }
        }

#endif

        // If we don't call dispose, we don't call supressfinalize on our deadlock notifier. 
        // If we don't call supressfinalize, then the deadlock notifier object will trigger
        // an exception letting us know that we forgot something important!
#if DEBUG
        private DeadlockNotifier myDeadlockNotifier;
        private DateTime myObjectStartedWaiting;
#endif
        private object myMonitoredObject;
        private bool myObjectIsAlreadyLocked;
        private const int kDefaultTimespan = 600;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedMonitor"/> class.
        /// </summary>
        public TimedMonitor(object obj)
        {
#if DEBUG
            myDeadlockNotifier = new DeadlockNotifier();
            myObjectStartedWaiting = DateTime.Now;
#endif
            Enter(obj);
        }

        /// <summary>
        /// Uses a monitor with timeoutCounter to enter a monitor specified object
        /// </summary>
        /// <param name="obj">The object</param>
        /// <returns>
        /// An IMonitor object that can be used to catch deadlocks with
        /// </returns>
        public void Enter(object obj)
        {
            Enter(obj, TimeSpan.FromSeconds(kDefaultTimespan));
        }

        /// <summary>
        /// Uses a monitor with timeoutCounter to enter a monitor specified object
        /// </summary>
        /// <param name="obj">The object</param>
        /// <param name="timeout"></param>
        /// <returns>
        /// An IMonitor object that can be used to catch deadlocks with
        /// </returns>
        public void Enter(object obj, TimeSpan timeout)
        {
            myMonitoredObject = obj;

            // If we didn't use SpinTryEnter, or if it failed to obtain a lock, then let's go the traditional route ...
            if ((myMonitoredObject != null) && (!Monitor.TryEnter(myMonitoredObject, timeout)))
            {                
                // If TryEnter fails, then we failed to acquire a lock. Trigger an exception so we knows it ;)
                throw new System.Threading.SynchronizationLockException("Possible Deadlock! TryEnter failed!!!");
            }

            if (myObjectIsAlreadyLocked == true)
            {
                Monitor.Wait(myMonitoredObject, timeout.Milliseconds);

                // If the object is *still* locked, then we're SOL, toss up an exception...
                if (myObjectIsAlreadyLocked == true)
                {
                    throw new System.Threading.SynchronizationLockException("Possible Deadlock! Waiting thread was never pulsed!!!");
                }
            }
            myObjectIsAlreadyLocked = true;
        }


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
#if DEBUG
            // It's just absolutely awful if someone forgets to call dispose. So, in order to track this down (while debugging), we put
            // a finalizer in the class, that way if dispose does get called we simply supress it. Otherwise, the finalizer
            // will assert a failure! (e.g. The finalizer should never ever get called b4 dispose does or we trigger an exception)
            GC.SuppressFinalize(myDeadlockNotifier);
#endif
            myObjectIsAlreadyLocked = false;
            if (myMonitoredObject != null)
            {
                Monitor.Pulse(myMonitoredObject);
                Monitor.Exit(myMonitoredObject);
#if DEBUG
                // If we're in debugging mode, trace a metric to indicate the amount of time that was spent waiting on a particlar object...
                TimeSpan span = (DateTime.Now - myObjectStartedWaiting);
                if (span.TotalSeconds > 30)
                {
                    UnityEngine.Debug.Log("Object: was locked for " + span.TotalSeconds + " seconds: " 
                        + myMonitoredObject.GetType().Name.ToString());
                }
#endif
            } // end if (myMonitoredOjbect)
        }// End Dispose()
    }//  End class class TimedMonitor()
}
