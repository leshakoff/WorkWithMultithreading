using System;
using System.Threading;

namespace Events
{
    class Program
    {
        public static int i = 0;
        static void Main(string[] args)
        {
            ManualResetEvent Meventobj = new ManualResetEvent(false);   // создаём объект
                                                                        // события с ручным сбросом.
                                                                        // в качестве параметра передаём
                                                                        // false, чтобы самостоятельно 
                                                                        // устанавливать, когда событие
                                                                        // начнёт подавать сигнал. 

            for (i = 0; i < 5; i++)                                     // запускаем цикл, 
            {                                                           // в котором будем создавать
                Console.WriteLine($" --- Отправлено событие #{i} из цикла. --- ");  // пять потоков,
                Console.WriteLine();                                    // которые будут реагировать на 
                                                                        // событие.
                Meventobj.Set();                                        // Включаем сигнал от события.

                Thread t = new Thread(() => SomeMethod(Meventobj));     // и запускаем поток.
                t.Start();
                t.Join();                                               // используем join для того, 
                                                                        // чтобы работа потоко                                                   // выполнялась последовательно
                                                                        // друг за другом.
            }
        }

        /// <summary>
        /// Метод, который отображает работу с событием.
        /// </summary>
        /// <param name="ev">В качестве аргумента метод принимает событие класса EventWaitHandle.</param>
        static void SomeMethod(EventWaitHandle ev)
        {
            Console.WriteLine($" [x] Ожидаем событие #{i} в потоке: " +
                $"{Thread.CurrentThread.ManagedThreadId}");
            ev.WaitOne();       // ожидаем сигнал от события... 


            Console.WriteLine($" [x] Сигнал от события #{i} в потоке " +
                $"{Thread.CurrentThread.ManagedThreadId} получен!");


            ev.Reset();         // как только событие дало сигнал, сбрасываем его состояние. 
            Console.WriteLine($" [x] Событие #{i} сброшено.");
            Console.WriteLine();


        }

    }
}