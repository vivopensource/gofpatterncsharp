﻿using Core.Console.Interfaces;
using GofPatterns.Structural.BridgePattern;

namespace GofConsoleApp.Examples.Structural.BridgePattern;

// Implementations for bridge pattern
internal interface IProcessEmployee : IBridgeImplementation
{
    void Process(Employee emp, IConsoleLogger logger);
}

// Bridge pattern - Concrete implementation - Register
internal class Register : IProcessEmployee
{
    public void Process(Employee emp, IConsoleLogger logger)
    {
        logger.Log($"Registering employee: {emp.Id} [{emp.FirstName} {emp.LastName}].");
    }
}

// Bridge pattern - Concrete implementation - Task assignment
internal class TaskAssignment : IProcessEmployee
{
    private readonly string name;

    public TaskAssignment(string name)
    {
        this.name = name;
    }

    public void Process(Employee emp, IConsoleLogger logger)
    {
        logger.Log($"Assigning employee {emp.Id} [{emp.FirstName} {emp.LastName}] with task [{name}].");
    }
}