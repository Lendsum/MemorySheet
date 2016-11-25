namespace Lendsum.MemorySheet
{
    /// <summary>
    /// Interface to be implemented by evaluable objects.
    /// </summary>
    public interface IEvaluable
    {
        /// <summary>
        /// Evaluates this instance.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        object Evaluate(params object[] args);

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        Position[] Parameters { get; }
    }
}