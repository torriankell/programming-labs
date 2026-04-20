using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project2;
using System;

namespace Project2.Tests
{
    [TestClass]
    public class TopicParserTests
    {
        [TestMethod]
        public void BuildTopicFromStrings_ValidData_ReturnsTopic()
        {
            string input = "Nikolay the Second;Математика;2024.03.31";
            var result = TopicParser.BuildTopicFromStrings(input);

            Assert.IsNotNull(result);
            Assert.AreEqual("Nikolay the Second", result.StudentName);
            Assert.AreEqual("Математика", result.TopicName);
            Assert.AreEqual(new DateOnly(2024, 3, 31), result.Date);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildTopicFromStrings_EmptyString_ThrowsArgumentException()
        {
            TopicParser.BuildTopicFromStrings("");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void BuildTopicFromStrings_OnlyTwoFields_ThrowsFormatException()
        {
            TopicParser.BuildTopicFromStrings("Иванов;Математика");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void BuildTopicFromStrings_FourFields_ThrowsFormatException()
        {
            TopicParser.BuildTopicFromStrings("Иванов;Математика;2024.03.31;Лишнее");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildTopicFromStrings_ManySemicolons_ThrowsFormatException()
        {
            TopicParser.BuildTopicFromStrings(";;");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void BuildTopicFromStrings_InvalidDate_ThrowsFormatException()
        {
            TopicParser.BuildTopicFromStrings("Иванов;Математика;31.03.2024");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildTopicFromStrings_EmptyStudentName_ThrowsArgumentException()
        {
            TopicParser.BuildTopicFromStrings(" ;Математика;2024.03.31");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildTopicFromStrings_EmptyTopicName_ThrowsArgumentException()
        {
            TopicParser.BuildTopicFromStrings("Иванов; ;2024.03.31");
        }

        [TestMethod]
        public void BuildTopicFromStrings_WithSpaces_TrimsCorrectly()
        {
            var result = TopicParser.BuildTopicFromStrings("  Иванов Иван  ;  Математика  ; 2024.03.31");

            Assert.IsNotNull(result);
            Assert.AreEqual("Иванов Иван", result.StudentName);
            Assert.AreEqual("Математика", result.TopicName);
        }
    }
}