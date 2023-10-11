using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.TestHost;

[TestFixture]
public class ProgramTests
{
    private StringWriter output;

    [SetUp]
    public void SetUp()
    {
        output = new StringWriter();
        Console.SetOut(output);
    }

    [TearDown]
    public void TearDown()
    {
        output.Dispose();
    }

    [Test]
    public void AddTask_ValidInput_TaskAddedSuccessfully()
    {
        // Arrange
        var input = new StringReader("1\nTask 1\nDescription 1\n10/04/2023\ntag1\ntag2\n\n");
        Console.SetIn(input);

        // Act
        Program.Main();

        // Assert
        string expectedOutput = "Главное меню:\r\n1. Добавить новую задачу\r\n2. Поиск задач по тэгам и вывод N наиболее актуальных задач\r\n3. Выйти\r\nВыберите пункт меню: Добавление новой задачи\r\nТема задачи: Описание задачи: Дата выполнения (дд/мм/гггг): Введите теги (пустая строка для завершения): Задача успешно добавлена.\r\n\r\nГлавное меню:\r\n1. Добавить новую задачу\r\n2. Поиск задач по тэгам и вывод N наиболее актуальных задач\r\n3. Выйти\r\nВыберите пункт меню: Программа завершена.\r\n";
        Assert.AreEqual(expectedOutput, output.ToString());
    }

    [Test]
    public void SearchTasks_MatchingTasksFound_TasksDisplayedSuccessfully()
    {
        // Arrange
        var tasks = new List<Task>
        {
            new Task("Task 1", "Description 1", new DateTime(2023, 4, 10), new List<string> { "tag1", "tag2" }),
            new Task("Task 2", "Description 2", new DateTime(2023, 4, 11), new List<string> { "tag1", "tag3" }),
            new Task("Task 3", "Description 3", new DateTime(2023, 4, 12), new List<string> { "tag2", "tag3" })
        };
        Program.tasks = tasks;

        var input = new StringReader("2\ntag1 tag2\n");
        Console.SetIn(input);

        // Act
        Program.Main();

        // Assert
        string expectedOutput = "Главное меню:\r\n1. Добавить новую задачу\r\n2. Поиск задач по тэгам и вывод N наиболее актуальных задач\r\n3. Выйти\r\nВыберите пункт меню: Поиск задач\r\nВведите ключевые слова для поиска: Найденные задачи:\r\nТема задачи: Task 1\r\nОписание задачи: Description 1\r\nДата выполнения: 10/04/2023\r\n\r\nГлавное меню:\r\n1. Добавить новую задачу\r\n2. Поиск задач по тэгам и вывод N наиболее актуальных задач\r\n3. Выйти\r\nВыберите пункт меню: Программа завершена.\r\n";
        Assert.AreEqual(expectedOutput, output.ToString());
    }
}