using System;
using System.Threading;

namespace MutexPools
{
    class Program
    {
        static Mutex mute = new Mutex();
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(MutexDemonstration);
                thread.Start();

            }
        }

        public static void MutexDemonstration()
        {
            mute.WaitOne();     // блокируем следующий блок кода, 
                                // чтобы он был доступен для выполнения
                                // только одним потоком
            Console.WriteLine($" --- Поток #{Thread.CurrentThread.ManagedThreadId} выполняет цикл: --- ");
            for (int i = 0; i < 5; i++)
            {
                Console.Write($" [{i}] ");
            }
            Console.WriteLine();
            mute.ReleaseMutex();     // и освобождаем объект мьютекса,
                                     // чтобы им могли пользоваться другие
                                     // потоки по завершению данного блока кода
        }
    }
}
