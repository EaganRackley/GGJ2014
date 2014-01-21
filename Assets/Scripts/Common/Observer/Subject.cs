// FILE:        Subject.cs
// Purpose:     Creates a delegate implementation of the ISubject interface for use with other classes that also implement ISubject
// See:         IObserver, 
// Author(s):   Eagan Rackley

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Common.Sync;

namespace Common.Observer
{
    /// <summary>
    /// Creates a delegate implementation of the ISubject interface for use with other classes that also implement ISubject
    /// </summary>
    public sealed class Subject : ISubject, IDisposable
    {
        private List<IObserver> myObservers = new List<IObserver>();
        private Object mySender;
        private bool myClassIsDisposing;

        /// <summary>
        /// Initializes a new instance of the <see cref="Subject"/> class.
        /// </summary>
        /// <param name="owner">The object that's implementing ISubject</param>
        public Subject(Object owner)
        {
            mySender = owner;
        }
        
        /// <summary>
        /// Registers an observer object.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void registerObserver(IObserver observer)
        {
            Debug.Assert(observer != null);

            if (myObservers.Contains(observer) == false)
            {
                myObservers.Add(observer);
            }
        }

        /// <summary>
        /// Removes an observer object.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void removeObserver(IObserver observer)
        {
            Debug.Assert(observer != null);

            // Use double checked locking to make sure we safely remove our observer ...
            if (myObservers.Contains(observer) == true)
            {
                
                using (TimedMonitor timedMonitor = new TimedMonitor(myObservers))
                {
                    if (myObservers.Contains(observer) == true)
                    {
                        myObservers.Remove(observer);
                    }
                }
            }
        }

        /// <summary>
        /// Unregisters all observer objects to verify that there are no references to other objects...
        /// </summary>
        public void removeAllObservers()
        {
            myObservers.Clear();
        }

        /// <summary>
        /// Determines whether the specified subject contains observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        /// 	<c>true</c> if the specified subject contains observer; otherwise, <c>false</c>.
        /// </returns>
        public bool containsObserver(IObserver observer)
        {
            Debug.Assert(observer != null);

            bool returnValue = myObservers.Contains(observer);

            //PolarisTrace.trace(TRACE_TYPE.VERBOSE, "000000", observer.GetType(), " Is observing connection: " + returnValue.ToString()
            //    + " count: " + myObservers.count.ToString());

            return returnValue;
        }

        /// <summary>
        /// Notifies the list of observer objects when necessary
        /// </summary>
        public void notifyObservers(Object arg)
        {
            Debug.Assert(arg != null);
            try
            {
                if ((myObservers.Count > 0) && (myClassIsDisposing == false))
                {
                    //UnityEngine.Debug.Log("Notifying: " + myObservers.Count + " Observers");
                    for (int index = 0; index < myObservers.Count; index++)
                    {
                        Debug.Assert(arg != null);
                        if (myObservers[index] != null)
                        {
                            myObservers[index].update(mySender, arg);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException iorException)
            {
                // An IOR Exception is not an error condition here, it just means that an observer was disconnected
                // and/or removed before this subject could send a notification to it.
                UnityEngine.Debug.Log("Exception: Observer was removed during the update process. " + iorException.Message);
            }
            catch (ArgumentOutOfRangeException aorException)
            {
                // Same thing here ...
                UnityEngine.Debug.Log("Exception: Observer was removed during the update process. " + aorException.Message);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (myClassIsDisposing == false)
            {
                myClassIsDisposing = true;
                myObservers.Clear();
            }
        }        
    }
}
