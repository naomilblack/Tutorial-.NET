using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TutorialAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TutorialAPI.Controllers
{
    //Commands Controller is a class that inherits from Controller base
    //ControllerBase is a basic class for an MVC controller without a view.
    //need to decorate the controller with [ApiController] so that the MVC
    //framework can recognize it as a controller class
    //we also need to give it a route (like a path)
    //so that we know where to call the method. Think of a url.
    //you can use [controller] or the first name of the controller folder in lowercase
    //just remember to use that when typing the path into postman/google
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        //creating action results that will map to swagger
        private readonly CommandContext _context;
        //using constructor dependency injection to instantiate
        //so we're setting _context to context in the constructor.
        //context comes from our startup class when we registered our DB service.
        //pretty much allows for usage of info from startup
        public CommandsController(CommandContext context)
        {
            _context = context;
        }

        //Get:      api/commands        endpoint
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            return _context.CommandItems;
        }

        //Get:      api/commands/1      endpoint
        //expecting an id/number to be passed through the url
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);

            if (commandItem == null)
            {
                return NotFound();
            }
            return commandItem;
        }

        //Post:     api/commands        endpoint
        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItem", new Command { Id = command.Id }, command);
        }


        //Put       api/commands/#      endpoint
        [HttpPut("{id}")]
        public ActionResult PutCommandItem(int id, Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            _context.Entry(command).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        //Delete    api/commands/#      endpoint
        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommandItem(int id)
        {
            var commandItem = _context.CommandItems.Find(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            _context.CommandItems.Remove(commandItem);
            _context.SaveChanges();

            return commandItem;
        }
    }
}
