using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod/application/{appName}/container/{containerName}/notification")]
    public class NotificationsController : ApiController
    {
        private readonly AppDbContext _context;

        public NotificationsController()
        {
            _context = new AppDbContext(); 
        }

        
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetNotifications(string appName, string containerName, [FromUri] string locate = null)
        {
            var container = await _context.Containers
                .Include(c => c.Application)
                .FirstOrDefaultAsync(c => c.Application.Name == appName && c.Name == containerName);

            if (container == null)
                return NotFound();

            if (locate == "notification")
            {
                var notificationNames = await _context.Notifications
                    .Where(n => n.Parent == container.Id)
                    .Select(n => n.Name)
                    .ToListAsync();

                var xml = new XElement("Notifications", notificationNames.Select(name => new XElement("Notification", name)));
                return Ok(xml);
            }

            return BadRequest("Invalid locate parameter value");
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IHttpActionResult> GetNotificationDetails(string appName, string containerName, string name)
        {
            var notification = await _context.Notifications
                .Include(n => n.Container.Application)
                .FirstOrDefaultAsync(n => n.Container.Application.Name == appName && n.Container.Name == containerName && n.Name == name);

            if (notification == null)
                return NotFound();

            var xml = new XElement("Notification",
                new XElement("Id", notification.Id),
                new XElement("Name", notification.Name),
                new XElement("CreationDateTime", notification.CreationDateTime.ToString("o")),
                new XElement("Parent", notification.Parent),
                new XElement("Event", notification.Event),
                new XElement("Endpoint", notification.Endpoint),
                new XElement("Enabled", notification.Enabled));

            return Ok(xml);
        }

        
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateNotification(string appName, string containerName, [FromBody] Notification newNotification)
        {
            var container = await _context.Containers
                .Include(c => c.Application)
                .FirstOrDefaultAsync(c => c.Application.Name == appName && c.Name == containerName);

            if (container == null)
                return NotFound();

            if (await _context.Notifications.AnyAsync(n => n.Name == newNotification.Name && n.Parent == container.Id))
                return Conflict();

            newNotification.CreationDateTime = DateTime.UtcNow;
            newNotification.Parent = container.Id;

            if (string.IsNullOrEmpty(newNotification.Name))
                newNotification.Name = $"Notification_{Guid.NewGuid()}";

            _context.Notifications.Add(newNotification);
            await _context.SaveChangesAsync();

            return Created(new Uri(Request.RequestUri, $"{newNotification.Name}"), newNotification);
        }

        
        [HttpDelete]
        [Route("{name}")]
        public async Task<IHttpActionResult> DeleteNotification(string appName, string containerName, string name)
        {
            var notification = await _context.Notifications
                .Include(n => n.Container.Application)
                .FirstOrDefaultAsync(n => n.Container.Application.Name == appName && n.Container.Name == containerName && n.Name == name);

            if (notification == null)
                return NotFound();

            _context.Notifications.Remove(notification);
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
        public DbSet<Notification> Notifications { get; set; }
    }
}