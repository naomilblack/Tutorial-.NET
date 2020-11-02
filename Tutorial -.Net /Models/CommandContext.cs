using Microsoft.EntityFrameworkCore;

namespace TutorialAPI.Models
{
    //inherits from DbContext class.
    //we need to make this class aware of our appsettings that
    //are connecting to our database/sql

    //our appsettings.json is our connection to the server
    //it contains the name of the sql server name "connection string"
    public class CommandContext : DbContext
    {
        //allows you to pass in options when creating DBcontext
        public CommandContext(DbContextOptions<CommandContext> options) : base (options)
        {

        }
        //tells entity that this is something we want to replicate as a set
        public DbSet<Command> CommandItems {get; set;}

    }
}
