using Microsoft.AspNetCore.Mvc;
using SoftEng2BackendAPI.ApikeyAttribute;
using SoftEng2BackendAPI.Models;
using SoftEng2BackendAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Controllers
{
    [ApiController]
    [ApikeyAuth]
    [Route("api/Notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _repository;
        public NotificationsController(INotificationRepository repository)
        {
            _repository = repository;
        }
        //GET api/Notifications
        [HttpGet]
        public async Task<ActionResult> LoadNotifcations()
        {
            var notifications = await _repository.FetchAllNotificationsAsync();
            if(notifications == null)
            {
                return NotFound("No Records");
            }
            return Ok(notifications);
        }
        //POST api/Notifications
        [HttpPost]
        public async Task<ActionResult> CreateNewNotification([FromBody]NotificationsModel newNotification)
        {
            if(newNotification == null)
            {
                return new NoContentResult();
            }
            await _repository.InsertNewNotification(newNotification);
            return Ok();
        }
    }
}
