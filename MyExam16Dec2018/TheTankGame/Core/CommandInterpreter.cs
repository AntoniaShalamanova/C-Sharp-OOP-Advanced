namespace TheTankGame.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IManager tankManager;
        private readonly Type tankManagerType;

        public CommandInterpreter(IManager tankManager)
        {
            this.tankManager = tankManager;
            this.tankManagerType = tankManager.GetType();
        }

        public string ProcessInput(IList<string> inputParameters)
        {
            string command = inputParameters[0];
            inputParameters = inputParameters.Skip(1).ToArray();

            string result = string.Empty;

            var method = this.tankManagerType.GetMethods()
                .FirstOrDefault(m => m.Name.EndsWith(command));

            result = (string)method.Invoke(this.tankManager, new object[] { inputParameters });

            return result;
        }
    }
}