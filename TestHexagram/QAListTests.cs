using FluentAssertions;
using HexagramNS;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class QAListTests
{
    [Test]
    public void CanSerializeAndDeserializeQAList()
    {
        // Arrange
        var qas = new List<QA>
        {
            new QA { Datum = DateTime.Now, Question = "What is the capital of France?", Answer = "Paris" },
            new QA { Datum = DateTime.Now, Question = "What is the largest planet in our solar system?", Answer = "Jupiter" },
            new QA { Datum = DateTime.Now, Question = "What is the chemical symbol for gold?", Answer = "Au" }
        };
        var qaList = new QAList(qas);

        // Act
        var json = qaList.ToJson();
        var deserializedQAList = QAList.FromJson(json);

        // Assert
        deserializedQAList.Should().NotBeNull();
        deserializedQAList.Should().HaveCount(qas.Count);
        deserializedQAList.Should().BeEquivalentTo(qas);
    }

    [Test]
    public void CanHandleNullJsonInput()
    {
        // Arrange
        string nullJson = null;

        // Act
        var deserializedQAList = QAList.FromJson(nullJson);

        // Assert
        deserializedQAList.Should().BeNull();
    }

    [Test]
    public void CanHandleEmptyJsonInput()
    {
        // Arrange
        string emptyJson = "";

        // Act
        var deserializedQAList = QAList.FromJson(emptyJson);

        // Assert
        deserializedQAList.Should().NotBeNull();
        deserializedQAList.Should().BeEmpty();
    }
}