using System.Collections.Generic;

namespace DesignPatternCourse.CommandPattern
{
    public class CommandInvoker
    {
        Queue<Command> commands = new Queue<Command>();

        public void AddCommand(Command command)
        {
            commands.Enqueue(command);
        }

        public void ExecuteCommands()
        {
            foreach (Command t in commands)
            {
                t.Execute();
            }

            commands.Clear();
        }
    }
}