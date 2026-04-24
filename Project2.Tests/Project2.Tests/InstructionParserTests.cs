using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project2;
using System;
using System.Linq;

namespace Project2.Tests
{
    [TestClass]
    public class InstructionParserTests
    {
        private InstructionParser _parser;

        [TestInitialize]
        public void Setup()
        {
            _parser = new InstructionParser();
        }

        [TestMethod]
        public void ParseCommands_ValidAddLine_ReturnsAddInstruction()
        {
            // Arrange
            string[] lines = { "ADD Иванов; C#; 2025.04.20" };
            string tempFile = CreateTempFile(lines);

            // Act
            var commands = _parser.ParseCommands(tempFile);

            // Assert
            Assert.AreEqual(1, commands.Count);
            Assert.IsInstanceOfType(commands[0], typeof(AddInstruction));

            DeleteTempFile(tempFile);
        }

        [TestMethod]
        public void ParseCommands_ValidRemLine_ReturnsRemInstruction()
        {
            string[] lines = { "REM StudentName == Иванов" };
            string tempFile = CreateTempFile(lines);

            var commands = _parser.ParseCommands(tempFile);

            Assert.AreEqual(1, commands.Count);
            Assert.IsInstanceOfType(commands[0], typeof(RemInstruction));

            DeleteTempFile(tempFile);
        }

        [TestMethod]
        public void ParseCommands_ValidSaveLine_ReturnsSaveInstruction()
        {
            string[] lines = { "SAVE result.txt" };
            string tempFile = CreateTempFile(lines);

            var commands = _parser.ParseCommands(tempFile);

            Assert.AreEqual(1, commands.Count);
            Assert.IsInstanceOfType(commands[0], typeof(SaveInstruction));

            DeleteTempFile(tempFile);
        }

        [TestMethod]
        public void ParseCommands_MultipleLines_ReturnsMultipleInstructions()
        {
            string[] lines = { "ADD Иванов; C#; 2025.04.20", "REM Date > 2025.01.01", "SAVE out.txt" };
            string tempFile = CreateTempFile(lines);

            var commands = _parser.ParseCommands(tempFile);

            Assert.AreEqual(3, commands.Count);

            DeleteTempFile(tempFile);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParseCommands_UnknownCommand_ThrowsArgumentException()
        {
            string[] lines = { "UNKNOWN some arg" };
            string tempFile = CreateTempFile(lines);

            _parser.ParseCommands(tempFile);

            DeleteTempFile(tempFile);
        }

        [TestMethod]
        public void ParseCommands_EmptyLines_Skipped()
        {
            string[] lines = { "", "   ", "ADD Иванов; C#; 2025.04.20", "", "REM StudentName == Иванов" };
            string tempFile = CreateTempFile(lines);

            var commands = _parser.ParseCommands(tempFile);

            Assert.AreEqual(2, commands.Count);

            DeleteTempFile(tempFile);
        }

        private string CreateTempFile(string[] lines)
        {
            string path = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllLines(path, lines);
            return path;
        }

        private void DeleteTempFile(string path)
        {
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }
    }
}