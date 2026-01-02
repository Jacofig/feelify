using UnityEngine;

using System.Collections.Generic;

public interface IGameCommandHandler
{
    bool CanExecute(string commandName);
    bool ExecuteCommand(string commandName, string[] args);
}

