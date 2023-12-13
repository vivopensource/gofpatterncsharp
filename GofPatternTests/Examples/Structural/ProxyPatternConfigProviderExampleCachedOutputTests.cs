﻿using GofConsoleApp.Examples.Structural.ProxyPattern;
using GofConsoleApp.Examples.Structural.ProxyPattern.ConfigProviderCachedOutput;
using Moq;
using NUnit.Framework;

namespace GofPatternTests.Examples.Structural;

[TestFixture]
internal class ProxyPatternConfigProviderExampleCachedOutputTests : BaseTest
{
    [Test]
    public void Execute_PerformsSuccessfulExampleRun_ReturnsTrue()
    {
        // arrange
        var readerValues = new Queue<string>(new[]
        {
            EnumConfigEnv.Dev.ToString(),
            EnumConfigEnv.Prod.ToString(),
            EnumConfigEnv.Prod.ToString(),
            EnumConfigEnv.Dev.ToString(),
            EnumConfigEnv.Test.ToString(),
            "Quit program"
        });

        var expectedCount = readerValues.Count;

        MockInputReader.Setup(x => x.AcceptInput()).Returns(readerValues.Dequeue);

        // act
        var actualResult = new ProxyPatternConfigProviderExampleCachedOutput().Execute(MockConsoleLogger.Object, MockInputReader.Object);

        // assert
        Assert.That(actualResult, Is.True);

        MockInputReader.Verify(x => x.AcceptInput(), Times.Exactly(expectedCount));
    }
}