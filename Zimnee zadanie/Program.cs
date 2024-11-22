class ToDoListApp
{
    private static Dictionary<DateTime, List<string>> toDoList = new();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Список дел:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Удалить выполненную задачу");
            Console.WriteLine("3. Показать задачи на указанную дату");
            Console.WriteLine("4. Экспортировать задачи в HTML");
            Console.WriteLine("5. Выйти");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    RemoveTask();
                    break;
                case "3":
                    ShowTasks();
                    break;
                case "4":
                    ExportToHtml();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Нажмите Enter и попробуйте снова.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private static void AddTask()
    {
        Console.Write("Введите дату задачи (ГГГГ-ММ-ДД): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            Console.Write("Введите описание задачи: ");
            string task = Console.ReadLine();
            if (!toDoList.ContainsKey(date))
            {
                toDoList[date] = new List<string>();
            }
            toDoList[date].Add(task);
            Console.WriteLine("Задача добавлена. Нажмите Enter для продолжения.");
        }
        else
        {
            Console.WriteLine("Некорректная дата. Нажмите Enter для продолжения.");
        }
        Console.ReadLine();
    }

    private static void RemoveTask()
    {
        Console.Write("Введите дату задачи для удаления (ГГГГ-ММ-ДД): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date) && toDoList.ContainsKey(date))
        {
            Console.WriteLine("Задачи на указанную дату:");
            for (int i = 0; i < toDoList[date].Count; i++)
            {
                Console.WriteLine($"{i + 1}. {toDoList[date][i]}");
            }

            Console.Write("Введите номер выполненной задачи: ");
            if (int.TryParse(Console.ReadLine(), out int taskIndex) && taskIndex > 0 && taskIndex <= toDoList[date].Count)
            {
                toDoList[date].RemoveAt(taskIndex - 1);
                Console.WriteLine("Задача удалена. Нажмите Enter для продолжения.");
            }
            else
            {
                Console.WriteLine("Некорректный номер задачи. Нажмите Enter для продолжения.");
            }

            if (toDoList[date].Count == 0)
            {
                toDoList.Remove(date);
            }
        }
        else
        {
            Console.WriteLine("Задачи на указанную дату не найдены. Нажмите Enter для продолжения.");
        }
        Console.ReadLine();
    }

    private static void ShowTasks()
    {
        Console.Write("Введите дату для отображения задач (ГГГГ-ММ-ДД): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date) && toDoList.ContainsKey(date))
        {
            Console.WriteLine("Задачи на указанную дату:");
            foreach (var task in toDoList[date])
            {
                Console.WriteLine($"- {task}");
            }
        }
        else
        {
            Console.WriteLine("На указанную дату задач нет.");
        }
        Console.WriteLine("Нажмите Enter для продолжения.");
        Console.ReadLine();
    }
    private static void ExportToHtml()
    {
        string fileName = "ToDoList.html";
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\">");
            writer.WriteLine("<head>");
            writer.WriteLine("<meta charset=\"UTF-8\">");
            writer.WriteLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            writer.WriteLine("<title>Список дел</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<h1>Список дел</h1>");

            foreach (var entry in toDoList)
            {
                writer.WriteLine($"<h2>{entry.Key:yyyy-MM-dd}</h2>");
                writer.WriteLine("<ul>");
                foreach (var task in entry.Value)
                {
                    writer.WriteLine($"<li>{task}</li>");
                }
                writer.WriteLine("</ul>");
            }

            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }

        Console.WriteLine($"Задачи экспортированы в файл {fileName}. Нажмите Enter для продолжения.");
        Console.ReadLine();
    }
}