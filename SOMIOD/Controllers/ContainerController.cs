using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod/application/{appName}/container")]
    public class ContainersController : ApiController
    {
        private readonly AppDbContext _context;

        public ContainersController()
        {
            _context = new AppDbContext(); 
        }

        
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetContainers(string appName, [FromUri] string locate = null)
        {
            var app = await _context.Applications.FirstOrDefaultAsync(a => a.Name == appName);
            if (app == null)
                return NotFound();

            if (locate == "container")
            {
                var containerNames = await _context.Containers
                    .Where(c => c.Parent == app.Id)
                    .Select(c => c.Name)
                    .ToListAsync();

                var xml = new XElement("Containers", containerNames.Select(name => new XElement("Container", name)));
                return Ok(xml);
            }

            return BadRequest("Invalid locate parameter value");
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IHttpActionResult> GetContainerDetails(string appName, string name)
        {
            var container = await _context.Containers
                .Include(c => c.Application)
                .FirstOrDefaultAsync(c => c.Application.Name == appName && c.Name == name);

            if (container == null)
                return NotFound();

            var xml = new XElement("Container",
                new XElement("Id", container.Id),
                new XElement("Name", container.Name),
                new XElement("CreationDateTime", container.CreationDateTime.ToString("o")),
                new XElement("Parent", container.Parent));

            return Ok(xml);
        }

        
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateContainer(string appName, [FromBody] Container newContainer)
        {
            var app = await _context.Applications.FirstOrDefaultAsync(a => a.Name == appName);
            if (app == null)
                return NotFound();

            if (await _context.Containers.AnyAsync(c => c.Name == newContainer.Name && c.Parent == app.Id))
                return Conflict();

            newContainer.CreationDateTime = DateTime.UtcNow;
            newContainer.Parent = app.Id;

            if (string.IsNullOrEmpty(newContainer.Name))
                newContainer.Name = $"Container_{Guid.NewGuid()}";

            _context.Containers.Add(newContainer);
            await _context.SaveChangesAsync();

            return Created(new Uri(Request.RequestUri, $"{newContainer.Name}"), newContainer);
        }

        
        [HttpDelete]
        [Route("{name}")]
        public async Task<IHttpActionResult> DeleteContainer(string appName, string name)
        {
            var container = await _context.Containers
                .Include(c => c.Application)
                .FirstOrDefaultAsync(c => c.Application.Name == appName && c.Name == name);

            if (container == null)
                return NotFound();

            _context.Containers.Remove(container);
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
        public DbSet<Container> Containers { get; set; }
    }
}