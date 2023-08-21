using System;

namespace CustomTimer.Factories
{
    /// <summary>
    /// Implements the factory method pattern <see><cref>https://en.wikipedia.org/wiki/Factory_method_pattern</cref></see>
    /// for creating an object of the <see cref="Timer"/> class.
    /// </summary>
#pragma warning disable CA1052
    public class TimerFactory
#pragma warning restore CA1052
    {
        /// <summary>
        /// Create an object of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="name">Name of timer.</param>
        /// <param name="ticks">Count of ticks.</param>
        /// <returns>A reference to an object of the <see cref="Timer"/> class.</returns>
        public static Timer CreateTimer(string name, int ticks)
        {
            return new Timer(name, ticks);
        }
    }
}
