using System;
using System.Threading;

namespace MonitorPools
{
    class Program
    {
        static object locker = new object();
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                // показываем состояние локера. метод IsEntered возвращает true,
                // если объект был заблокирован, и false - если заглушка
                // освобождена и не используется. 
                Console.WriteLine("\n --- Состояние локера: " +
                    ((Monitor.IsEntered(locker)) ?
                    "заблокирован. ---\n" :
                    "освобождён. ---\n"));
                Thread thread = new Thread(MonitorDemonstration);
                thread.Start();
                thread.Join();      // вызываем метод Join(), чтобы код
                                    // отрабатывал синхронно не только внутри
                                    // метода потока, 
                                    // но и в первичном потоке. 
            }

            Console.ReadLine();
        }

        public static void MonitorDemonstration()
        {
            // методом Enter блокируем объект-заглушку.
            Monitor.Enter(locker);
            try
            {
                // и выполняем код, который посредством
                // блокировки будет недоступен для других
                // потоков, пока не отработает до конца
                Console.WriteLine($"Выполняется поток: {Thread.CurrentThread.ManagedThreadId}");
                for (int i = 1; i < 5; i++)
                {
                    Console.Write($" [{i}] ");
                    Console.WriteLine("Состояние локера: " +    // проверяем состояние заглушки
                        ((Monitor.IsEntered(locker)) ?
                        "заблокирован." :
                        "освобождён."));
                    Thread.Sleep(100);

                }
            }
            finally
            {
                // и освобождаем объект-заглушку,
                // снова делая код доступным для других потоков 
                Monitor.Exit(locker);
            }
        }
    }
}
