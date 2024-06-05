using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Application.Notifications.Queries.GetList;
using WebAPI.Application.Notifications.Queries.GetNotification;
using WebAPI.Application.Notifications.Commands.CreateNotifications;
using WebAPI.Application.Notifications.Commands.UpdateNotifications;
using WebAPI.Application.Notifications.Commands.DeleteNotifications;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class NotificationController : BaseController
    {
        private readonly IMapper _mapper;

        public NotificationController(IMapper mapper) => _mapper = mapper;
		/// <summary>
		/// Gets the list of notifications from database
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /notification
		/// </remarks>
		/// <returns>Returns NotificationListVM</returns>
		/// <response code="200">Success</response>
		[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<NotificationsListVM>> GetAll()
        {
            var query = new GetNotificationsListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
		/// <summary>
		/// Gets the notification by id
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// GET /notification/31f49a44-77c0-45de-a5ed-424b6ec42a4e
		/// </remarks>
		/// <param name="id"> Notification id (guid) </param>
		/// <returns>Returns NotificationDetailsVM</returns>
		/// <response code="200">Success</response>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<NotificationDetailsVM>> Get(Guid id)
        {
            var query = new GetNotificationsDetailsQuery
			{
               Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);

        }
		/// <summary>
		/// Creates the notification
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// POST/notification
		/// {
		///        content: "notification text",
		///        value: "notification data",
		///        quality: "notification valid"
		/// }
		/// </remarks>
		/// <param name="createNotificationsDTO">createNotificationsDTO object</param>
		/// <returns>Returns id (guid)</returns>
		/// <response code="201">Success</response>
		/// <returns></returns>
		/// <summary>
		/// Deletes the notification by id
		/// </summary>
		/// <remarks>
		/// Sample request:
		/// DELETE/notification/31f49a44-77c0-45de-a5ed-424b6ec42a4e
		/// </remarks>
		/// <param name="id"> Id of the notification (guid) </param>
		/// <returns>Returns NoContent</returns>
		/// <response code="204">Success</response>
		[HttpDelete("{id}")]
		//[Authorize]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNotificationsCommand
            {
                Id = id,
            };
            await Mediator.Send(command);
            return NoContent();

        }
    }
}   
