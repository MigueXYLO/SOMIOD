using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod/application/{appName}/container/{containerName}/record")]
    public class RecordsController : ApiController
    {
        private readonly AppDbContext _context;

        public RecordsController()
        {
            _context = new AppDbContext(); 
        }

        
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetRecords(string appName, string containerName, [FromUri] string locate = null)
        {
            var container = await _context.Containers
                .Include(c => c.Application)
                .FirstOrDefaultAsync(c => c.Application.Name == appName && c.Name == containerName);

            if (container == null)
                return NotFound();

            if (locate == "record")
            {
                var recordNames = await _context.Records
                    .Where(r => r.Parent == container.Id)
                    .Select(r => r.Name)
                    .ToListAsync();

                var xml = new XElement("Records", recordNames.Select(name => new XElement("Record", name)));
                return Ok(xml);
            }

            return BadRequest("Invalid locate parameter value");
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IHttpActionResult> GetRecordDetails(string appName, string containerName, string name)
        {
            var record = await _context.Records
                .Include(r => r.Container.Application)
                .FirstOrDefaultAsync(r => r.Container.Application.Name == appName && r.Container.Name == containerName && r.Name == name);

            if (record == null)
                return NotFound();

            var xml = new XElement("Record",
                new XElement("Id", record.Id),
                new XElement("Name", record.Name),
                new XElement("Content", record.Content),
                new XElement("CreationDateTime", record.CreationDateTime.ToString("o")),
                new XElement("Parent", record.Parent));

            return Ok(xml);
        }

        
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateRecord(string appName, string containerName, [FromBody] Record newRecord)
        {
            var container = await _context.Containers
                .Include(c => c.Application)
                .FirstOrDefaultAsync(c => c.Application.Name == appName && c.Name == containerName);

            if (container == null)
                return NotFound();

            if (await _context.Records.AnyAsync(r => r.Name == newRecord.Name && r.Parent == container.Id))
                return Conflict();

            newRecord.CreationDateTime = DateTime.UtcNow;
            newRecord.Parent = container.Id;

            if (string.IsNullOrEmpty(newRecord.Name))
                newRecord.Name = $"Record_{Guid.NewGuid()}";

            _context.Records.Add(newRecord);
            await _context.SaveChangesAsync();

            
            await TriggerNotifications(container.Id, "creation", newRecord);

            return Created(new Uri(Request.RequestUri, $"{newRecord.Name}"), newRecord);
        }

        
        [HttpDelete]
        [Route("{name}")]
        public async Task<IHttpActionResult> DeleteRecord(string appName, string containerName, string name)
        {
            var record = await _context.Records
                .Include(r => r.Container.Application)
                .FirstOrDefaultAsync(r => r.Container.Application.Name == appName && r.Container.Name == containerName && r.Name == name);

            if (record == null)
                return NotFound();

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();

            
            await TriggerNotifications(record.Parent, "deletion", record);

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        private async Task TriggerNotifications(int containerId, string eventType, Record record)
        {
            var notifications = await _context.Notifications
                .Where(n => n.Parent == containerId && (n.Event == 1 && eventType == "creation" || n.Event == 2 && eventType == "deletion"))
                .ToListAsync();

            foreach (var notification in notifications)
            {
                if (notification.Enabled)
                {
                    
                    Console.WriteLine($"Triggering {eventType} notification to {notification.Endpoint} for record: {record.Name}");
                }
            }
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
        public DbSet<Record> Records { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}