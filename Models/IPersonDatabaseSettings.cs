namespace RestApiStudy.Models
{
    /// <summary>
    /// IPersonDatabaseSettings
    /// </summary>
    public interface IPersonDatabaseSettings
    {
        /// <summary>
        /// CollectionName
        /// </summary>
        string CollectionName { get; set; }
        /// <summary>
        /// ConnectionString
        /// </summary>
        string ConnectionString { get; set; }
        /// <summary>
        /// DatabaseName
        /// </summary>
        string DatabaseName { get; set; }
    }
}
