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
                return await Task.FromResult(_person.Find(p => true).ToList()).ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// GetPerson
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Person> GetPerson(string id)
        {
            try
            {
                return await Task.FromResult(_person.Find<Person>(p => p.Id == id).FirstOrDefault()).ConfigureAwait(false);
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
        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                person.Id = string.Empty;
                _person.InsertOne(person);
                return await Task.FromResult(true).ConfigureAwait(false);
            }
            catch (System.Exception)
            {
                return await Task.FromResult(false).ConfigureAwait(false);
            }

        }
    }
}
