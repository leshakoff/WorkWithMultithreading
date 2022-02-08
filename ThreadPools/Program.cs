using System;
using System.Threading;

namespace ThreadPools
{
    class Program
    {
        public static string[] hellos = { "Hello!", "Привет!", "Hola!", "Hallo!", "Le salut!" };
        static void Main(string[] args)
        {
            Console.WriteLine("ВНИМАНИЕ! Работает основной поток... \n");

            Random r = new Random();
            for (int i = 0; i < 5; ++i)
            {
                ThreadPool.SetMinThreads(2, 2); // устанавливаем минимальное количество потоков - 2,
                                                // чтобы продемонстрировать, что потоки из пула потоков
                                                // могут переиспользоваться. 

                ThreadPool.QueueUserWorkItem
                    (SayHello,
                    hellos[r.Next(0, 5)]);      // ставим метод в очередь;
                                                // в качестве второго объекта
                                                // будут передаваться какие-то
                                                // данные, которые необходимо использовать
                                                // в выполняемом методе.
            }

            // к сожалению, демонстрация приоритетности
            // потоков удаётся не всегда :D
            // чаще всего, но всё же не всегда, т.к., 
            // насколько я поняла, среда CLR - штука непредсказуемая, 
            // и какой-то поток из пула всё же может проскочить между 
            // инструкциями первичного потока. Попробуйте запустить программу
            // несколько раз, и вы поймёте, о чём я))

            Console.WriteLine("Основной поток продолжает работать,\n" +
                "потому что потоки из пула потоков - фоновые,\n" +
                "как следствие - они будут завершены только тогда,\n" +
                "когда завершатся главные потоки с более высоким приоритетом.\n" +
                "Look at this!\n");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Задача-болванка #{i}");
            }
            Console.WriteLine("\nОсновной поток завершает работу...\n");

            Console.ReadKey();
        }



        static void SayHello(object state)
        {
            Console.WriteLine("{0, -10} from thread #{1, 2}", state, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
