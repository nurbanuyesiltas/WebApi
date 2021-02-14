using MongoDB.Driver;
using RestApiStudy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiStudy.Repositories
{
    /// <summary>
    /// PersonRepository
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly IMongoCollection<Person> _person;
        /// <summary>
        /// PersonRepository ctor
        /// </summary>
        public PersonRepository(IPersonDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _person = database.GetCollection<Person>(settings.CollectionName);
        }

        /// <summary>
        /// Returns people data
        /// </summary>
        /// <returns></returns>
        public async Task<List<Person>> GetPeople()
        {
            try
            {
                return (await _person.FindAsync(p => true).ConfigureAwait(false)).ToList();
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Gives all data of the person given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Person> GetPerson(string id)
        {
            try
            {
                var person = await _person.FindAsync<Person>(p => p.Id == id).Result
                                         .FirstOrDefaultAsync()
                                         .ConfigureAwait(false);
                return person;
            }
            catch (System.Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// Add Person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task AddPerson(Person person)
        {
            try
            {
                person.Id = string.Empty;
                await _person.InsertOneAsync(person).ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                
            }

        }
    }
}
