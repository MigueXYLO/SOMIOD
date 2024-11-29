using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod/applications")]
    public class ApplicationsController : ApiController
    {
        private static List<Application> applications = new List<Application>();

        // GET: List all applications or get details of a specific one
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetApplications([FromUri] string locate = null)
        {
            if (locate == "application")
            {
                var appNames = applications.Select(a => a.Name).ToList();
                var xml = new XElement("Applications", appNames.Select(name => new XElement("Application", name)));
                return Ok(xml);
            }
            return BadRequest("Invalid locate parameter value");
        }

        [HttpGet]
        [Route("{name}")]
        public IHttpActionResult GetApplicationDetails(string name)
        {
            var app = applications.FirstOrDefault(a => a.Name == name);
            if (app == null)
                return NotFound();

            var xml = new XElement("Application",
                new XElement("Id", app.Id),
                new XElement("Name", app.Name),
                new XElement("CreationDateTime", app.CreationDateTime.ToString("o")));

            return Ok(xml);
        }

        // POST: Create a new application
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateApplication([FromBody] Application newApp)
        {
            if (applications.Any(a => a.Name == newApp.Name))
                return Conflict();

            newApp.Id = applications.Count + 1;
            newApp.CreationDateTime = DateTime.UtcNow;

            if (string.IsNullOrEmpty(newApp.Name))
                newApp.Name = $"App{newApp.Id}";

            applications.Add(newApp);

            return Created(new Uri(Request.RequestUri, $"{newApp.Name}"), newApp);
        }

        // PUT: Update an application
        [HttpPut]
        [Route("{name}")]
        public IHttpActionResult UpdateApplication(string name, [FromBody] Application updatedApp)
        {
            var app = applications.FirstOrDefault(a => a.Name == name);
            if (app == null)
                return NotFound();

            app.Name = updatedApp.Name;
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        // DELETE: Delete an application
        [HttpDelete]
        [Route("{name}")]
        public IHttpActionResult DeleteApplication(string name)
        {
            var app = applications.FirstOrDefault(a => a.Name == name);
            if (app == null)
                return NotFound();

            applications.Remove(app);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}