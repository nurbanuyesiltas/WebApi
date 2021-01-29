using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestApiStudy.Models
{
    /// <summary>
    /// Person class
    /// </summary>
    public class Person
    {
        /// <summary>
        /// id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        /// <summary>
        /// name
        /// </summary>
        [BsonElement("Name")]
        public string Name { get; set; }
        /// <summary>
        /// surname
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// phone
        /// </summary>
        public string Phone { get; set; }     
    }
}
