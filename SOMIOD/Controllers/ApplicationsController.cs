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
    [RoutePrefix("api/somiod/application")]
    public class ApplicationsController : ApiController
    {
        private readonly AppDbContext _context;

        public ApplicationsController()
        {
            _context = new AppDbContext(); 
        }

        
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetApplications([FromUri] string locate = null)
        {
            if (locate == "application")
            {
                var appNames = await _context.Applications.Select(a => a.Name).ToListAsync();
                var xml = new XElement("Applications", appNames.Select(name => new XElement("Application", name)));
                return Ok(xml);
            }
            return BadRequest("Invalid locate parameter value");
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IHttpActionResult> GetApplicationDetails(string name)
        {
            var app = await _context.Applications.FirstOrDefaultAsync(a => a.Name == name);
            if (app == null)
                return NotFound();

            var xml = new XElement("Application",
                new XElement("Id", app.Id),
                new XElement("Name", app.Name),
                new XElement("CreationDateTime", app.CreationDateTime.ToString("o")));

            return Ok(xml);
        }

        
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateApplication([FromBody] Application newApp)
        {
            if (await _context.Applications.AnyAsync(a => a.Name == newApp.Name))
                return Conflict();

            newApp.CreationDateTime = DateTime.UtcNow;

            if (string.IsNullOrEmpty(newApp.Name))
                newApp.Name = $"Application_{Guid.NewGuid()}";

            _context.Applications.Add(newApp);
            await _context.SaveChangesAsync();

            return Created(new Uri(Request.RequestUri, $"{newApp.Name}"), newApp);
        }

        
        [HttpPut]
        [Route("{name}")]
        public async Task<IHttpActionResult> UpdateApplication(string name, [FromBody] Application updatedApp)
        {
            var app = await _context.Applications.FirstOrDefaultAsync(a => a.Name == name);
            if (app == null)
                return NotFound();

            app.Name = updatedApp.Name;
            await _context.SaveChangesAsync();

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        
        [HttpDelete]
        [Route("{name}")]
        public async Task<IHttpActionResult> DeleteApplication(string name)
        {
            var app = await _context.Applications.FirstOrDefaultAsync(a => a.Name == name);
            if (app == null)
                return NotFound();

            _context.Applications.Remove(app);
            await _context.SaveChangesAsync();

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=DBData") { }

        public DbSet<Application> Applications { get; set; }
    }


}