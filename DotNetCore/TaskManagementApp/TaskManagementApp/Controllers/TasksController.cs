using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementApp.Application.Tasks.Commands;
using TaskManagementApp.Application.Tasks.Dto;
using TaskManagementApp.Application.Tasks.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManagementApp.Api.Controllers
{
    [ApiController]
    [Route("rest/tasks")]
    public class TasksController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost("SaveTask")]
        public async Task<ActionResult<int>> CreateTask(CreateTaskCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("GetAllTasks")]
        public async Task<ActionResult<List<TaskDto>>> GetAllTasks()
        {
            return await Mediator.Send(new GetAllTasksQuery());
        }

        [HttpGet("GetTaskById")]
        public async Task<ActionResult<TaskDto>> GetTaskById(int id)
        {
            return await Mediator.Send(new GetTaskByIdQuery { Id = id });
        }

        [HttpPut("UpdateTask")]
        public async Task<ActionResult<int>> UpdateTask(UpdateTaskCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("DeleteTaskById")]
        public async Task<ActionResult<int>> DeleteTask(int id)
        {
            return await Mediator.Send(new DeleteTaskCommand { Id = id });
        }
    }
}
