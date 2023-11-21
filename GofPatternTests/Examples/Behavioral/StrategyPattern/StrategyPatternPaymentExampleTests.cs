﻿using GofConsoleApp.Examples.Behavioral.StrategyPattern;
using GofConsoleApp.Examples.Behavioral.StrategyPattern.PaymentExample;
using Moq;
using NUnit.Framework;
using static System.Globalization.CultureInfo;
using static GofConsoleApp.Examples.Behavioral.StrategyPattern.PaymentExample.EnumPaymentOptions;

namespace GofPatternTests.Examples.Behavioral.StrategyPattern;

[TestFixture]
internal class StrategyPatternPaymentExampleTests : BaseTest
{
    [TestCase(Debit, 12.2, true)]
    [TestCase(Credit, 13.3, true)]
    [TestCase(Credit, 1100.10, false)]
    public void Execute_PerformsSuccessfulExampleRun_ReturnsTrue(EnumPaymentOptions option, decimal amount,
        bool expectedResult)
    {
        // act
        var readerValues = new Queue<string>(new[]
        {
            option.ToString(), amount.ToString(InvariantCulture)
        });

        var expectedReaderCount = readerValues.Count;
        const int expectedLogCount = 5;

        MockInputReader.Setup(x => x.AcceptInput()).Returns(readerValues.Dequeue);

        // act
        var actualResult = sut.Execute(MockConsoleLogger.Object, MockInputReader.Object);

        // assert
        Assert.That(actualResult, Is.EqualTo(expectedResult));

        MockInputReader.Verify(x => x.AcceptInput(), Times.Exactly(expectedReaderCount));
        MockConsoleLogger.Verify(x => x.Log(It.IsAny<string>()), Times.Exactly(expectedLogCount));
    }

    [Test]
    public void Execute_QuitsExampleIfInvalidOption_ReturnsFalse()
    {
        // act
        var readerValues = new Queue<string>(new[]
        {
            Invalid.ToString()
        });

        var expectedReaderCount = readerValues.Count;
        const int expectedLogCount = 3;

        MockInputReader.Setup(x => x.AcceptInput()).Returns(readerValues.Dequeue);

        // act
        var actualResult = sut.Execute(MockConsoleLogger.Object, MockInputReader.Object);

        // assert
        Assert.That(actualResult, Is.False);

        MockInputReader.Verify(x => x.AcceptInput(), Times.Exactly(expectedReaderCount));
        MockConsoleLogger.Verify(x => x.Log(It.IsAny<string>()), Times.Exactly(expectedLogCount));
    }

    private readonly StrategyPatternPaymentExample sut = new();
}