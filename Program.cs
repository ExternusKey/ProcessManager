using System.Diagnostics;
using System.Security.Cryptography;

internal class Program
{
    static int pid;
    static bool access;

    private static void Main(string[] args)
    {
        Console.WriteLine("ProcessManager started...");
        Thread.Sleep(1500);
        while (true)
        {
            access = true;
            Console.Clear();
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Вывести список процессов");
            Console.WriteLine("2. Удалить процесс (требуется ID)");
            Console.Write("Действие - ");
            ChooseAction();
        }
    }

    public static void ChooseAction()
    {
        switch (Console.ReadLine())
        {
            case "1":
                ShowProcessList();
                break;
            case "2":
                Console.Clear();
                KillProcess();
                return;
            default:
                HandleError("Действите выбрано неверно. Повторите ещё раз.");
                return;
        }
    }

    static public void ShowProcessList()
    {
        foreach (Process process in Process.GetProcesses())
            Console.WriteLine($"ID: {process.Id,-10} Name: {process.ProcessName,-20}");

        while (access)
        {
            Console.WriteLine(new string('-', Console.WindowWidth));
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Вернуться в главное меню");
            Console.WriteLine("2. Удалить процесс (требуется ID)");
            Console.Write("Действие - ");
            switch (Console.ReadLine())
            {
                case "1":
                    access = false;
                    break;
                case "2":
                    Console.WriteLine(new string('-', Console.WindowWidth));
                    KillProcess();
                    access = false;
                    break;
                default:
                    HandleError("Действите выбрано неверно. Повторите ещё раз.");
                    break;
            }
        }
    }

    static public void KillProcess()
    {
        Console.Write("Номер процесса - ");
        try
        {
            pid = int.Parse(Console.ReadLine());
            Process processToKill = Process.GetProcessById(pid);
            processToKill.Kill();
        }
        catch (FormatException)
        {
            HandleError("Неверный тип данных. Требуется число");
            return;
        }
        catch (ArgumentException)
        {
            HandleError("Такой процесс не существует");
            return;
        }
        catch (OverflowException)
        {
            HandleError("Переданное значение не укладывается в диапазон int32");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Процесс {pid} удалён");
        Console.ResetColor();
        Thread.Sleep(1500);
        return;
    }

    static void HandleError(string errorMessage)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
        Thread.Sleep(1500);
    }
}