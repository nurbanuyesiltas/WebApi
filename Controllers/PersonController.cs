using Microsoft.AspNetCore.Mvc;
using RestApiStudy.Models;
using RestApiStudy.Repositories;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace RestApiStudy.Controllers
{
    /// <summary>
    /// PersonController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository mPersonRepository;
        /// <summary>
        /// PersonController ctor
        /// </summary>
        /// <param name="personRepository"></param>
        public PersonController(IPersonRepository personRepository)
        {
            mPersonRepository = personRepository;
        }
        /// <summary>
        /// Returns the people
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("People")]
        public async Task<IActionResult> GetPeople()
        {
            return Ok(await mPersonRepository.GetPeople().ConfigureAwait(false));
        }
        /// <summary>
        /// Returns person given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:length(24)}", Name = "GetPerson")]
        public async Task<IActionResult> GetPerson(string id)
        {
            var person = await mPersonRepository.GetPerson(id).ConfigureAwait(false);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
        /// <summary>
        /// Service used to add person
        /// </summary>
        /// <remarks>
        /// Request Example:
        /// {
        ///     "Id": "",
        ///     "Name": "NURBANU",
        ///     "Surname": "YEŞİLTAŞ",
        ///     "Gender": "K",
        ///     "Email": "nurbanu.yesiltas.55@gmail.com",
        ///     "Phone": "05555555555"
        /// }
        /// </remarks>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost, Route("AddPerson")]
        public async Task<IActionResult> AddPerson(Person person)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new System.Uri("amqp://guest:guest@localhost:5672");

            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();
            channel.QueueDeclare("queue",
                             durable: true,
                             autoDelete: false,
                             arguments: null,
                             exclusive: false
                             );

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(person));
            channel.BasicPublish("", "queue", null, body);

            channel.Close();
            conn.Close();

            return await Task.FromResult((Ok())).ConfigureAwait(false);
        }
    }
}
