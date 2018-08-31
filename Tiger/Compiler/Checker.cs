namespace Tiger
{
    /// <summary>
    /// Abstract definition for all the checking stages in a compilation process
    /// </summary>
    public abstract class Checker
    {
        /// <summary>
        /// Defines wether it has error or not
        /// </summary>
        public bool HasError { get; protected set; }
    }
}
