//File:      ISubject.cs
//Desc:      Interface that allows other objects to observe the behavior on an implementing class
//Date:      3/12/2011
//Author(s): Eagan Rackley

using System;

namespace Common.Observer
{
    /// <summary>
    /// Interface that allows other objects to observe the behavior on an implementing class
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// Registers the observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void registerObserver(IObserver observer);

        /// <summary>
        /// Removes the observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        void removeObserver(IObserver observer);

        /// <summary>
        /// Determines whether the specified subject contains observer.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>
        /// <c>true</c> if the specified subject contains observer; otherwise, <c>false</c>.
        /// </returns>
        bool containsObserver(IObserver observer);

        /// <summary>
        /// Notifies the observers.
        /// </summary>
        void notifyObservers(Object arg);

        /// <summary>
        /// Unregisters all observer objects to verify that there are no references to other objects...
        /// </summary>
        void removeAllObservers();
    }
}
