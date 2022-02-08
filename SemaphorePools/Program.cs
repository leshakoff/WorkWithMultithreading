using System;
using System.Threading;

namespace SemaphorePools
{
    class Program
    {
        static void Main(string[] args)
        {
            // ✨ элегантно ✨ синхронизируем потоки в пуле при помощи семафора: 
            // 1. устанавливаем объекту класса семафор значение 1-1
            // (один начальный вход и один одновременный вход)
            Semaphore semaphore = new Semaphore(1, 1);
            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(SemaphoreDemonstration, semaphore); // ставим метод в очередь 
            }
            Console.ReadKey();
        }

        static void SemaphoreDemonstration(object obj)
        {
            Semaphore sem = obj as Semaphore; // приводим переданный объект к классу семафора

            // до сигнала от семафора ничего работать не будет

            sem.WaitOne();  // 2. Дожидаемся сигнала. 

            // как только сигнал поступил, код выполняется, 


            Console.WriteLine($"Поток #{Thread.CurrentThread.ManagedThreadId} работает... \n");
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(200);
                Console.Write($" [{i}] ");
            }
            Console.WriteLine("\n");

            Console.WriteLine($"Поток #{Thread.CurrentThread.ManagedThreadId} завершается... ");

            // и по его завершению мы выходим из блокировки семафора, 

            sem.Release(1); // освобождая только 1 поток. 
            Console.WriteLine($"Поток #{Thread.CurrentThread.ManagedThreadId} освободил очередь!");

            Console.WriteLine("----------------------------------------");


        }
    }
}
