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
            // Изменено: добавлено 4-е поле (преподаватель)
            string input = "Nikolay the Second;Математика;2024.03.31;Иванов И.И.";
            var result = TopicParser.BuildTopicFromStrings(input);

            Assert.IsNotNull(result);
            Assert.AreEqual("Nikolay the Second", result.StudentName);
            Assert.AreEqual("Математика", result.TopicName);
            Assert.AreEqual(new DateOnly(2024, 3, 31), result.Date);
            // Новая проверка
            Assert.AreEqual("Иванов И.И.", result.TeacherName);
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
            // 2 поля — всё ещё ошибка
            TopicParser.BuildTopicFromStrings("Иванов;Математика");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void BuildTopicFromStrings_ThreeFields_ThrowsFormatException()
        {
            // 3 поля теперь тоже ошибка (ожидается 4)
            TopicParser.BuildTopicFromStrings("Иванов;Математика;2024.03.31");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void BuildTopicFromStrings_FiveFields_ThrowsFormatException()
        {
            // 5 полей — ошибка
            TopicParser.BuildTopicFromStrings("Иванов;Математика;2024.03.31;Преподаватель;Лишнее");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildTopicFromStrings_ManySemicolons_ThrowsFormatException()
        {
            TopicParser.BuildTopicFromStrings(";;;");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void BuildTopicFromStrings_InvalidDate_ThrowsFormatException()
        {
            // Неверный формат даты
            TopicParser.BuildTopicFromStrings("Иванов;Математика;31.03.2024;Преподаватель");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildTopicFromStrings_EmptyStudentName_ThrowsArgumentException()
        {
            TopicParser.BuildTopicFromStrings(" ;Математика;2024.03.31;Преподаватель");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildTopicFromStrings_EmptyTopicName_ThrowsArgumentException()
        {
            TopicParser.BuildTopicFromStrings("Иванов; ;2024.03.31;Преподаватель");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BuildTopicFromStrings_EmptyTeacherName_ThrowsArgumentException()
        {
            // Новая проверка: пустой преподаватель
            TopicParser.BuildTopicFromStrings("Иванов;Математика;2024.03.31; ");
        }

        [TestMethod]
        public void BuildTopicFromStrings_WithSpaces_TrimsCorrectly()
        {
            // Добавлен преподаватель с пробелами
            var result = TopicParser.BuildTopicFromStrings("  Иванов Иван  ;  Математика  ; 2024.03.31;  Иванов И.И.  ");

            Assert.IsNotNull(result);
            Assert.AreEqual("Иванов Иван", result.StudentName);
            Assert.AreEqual("Математика", result.TopicName);
            Assert.AreEqual("Иванов И.И.", result.TeacherName);
        }
    }
}