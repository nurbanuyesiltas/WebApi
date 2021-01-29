using RestApiStudy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiStudy.Repositories
{
    /// <summary>
    /// IPersonRepository
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Returns people data
        /// </summary>
        /// <returns></returns>
        Task<List<Person>> GetPeople();
        /// <summary>
        ///  Brings the person given tc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Person> GetPerson(string id);
        /// <summary>
        /// Add person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        Task<bool> AddPerson(Person person);

    }
}
