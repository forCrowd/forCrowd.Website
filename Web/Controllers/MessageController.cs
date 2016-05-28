using forCrowd.Website.Web.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace forCrowd.Website.Web.Controllers
{
    public class MessageController : ApiController
    {
        // POST api/values
        public async Task<IHttpActionResult> Post([FromBody]Message message)
        {
            var emailService = new Framework.EmailService();
            await emailService.SendAsync(message);

            return Ok(string.Empty);
        }
    }
}
