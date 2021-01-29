namespace RestApiStudy.Models
{
    /// <summary>
    /// PersonDatabaseSettings
    /// </summary>
    public class PersonDatabaseSettings : IPersonDatabaseSettings
    {
        /// <summary>
        /// CollectionName
        /// </summary>
        public string CollectionName { get; set; }
        /// <summary>
        /// ConnectionString
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// DatabaseName
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
