//File:      IObserver.cs
//Desc:      Interface that allows other objects to observe the behavior on an implementing class
//Date:      3/12/2011
//Author(s): Eagan Rackley

using System;

namespace Common.Observer
{
    /// <summary>
    /// Provides interface for an object to observe the behavior of an observable object ...
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// Updates the specified sender information using the data object.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="data">The data.</param>
        void update(Object sender, Object data);
    }
}
