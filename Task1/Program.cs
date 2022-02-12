using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        const int arrayMinValueLimit = 0;   // Минимальное значение для генератора случайных чисел
        const int arrayMaxValueLimit = 1000;// Максимальное значение для генератора случайных чисел
        static void Main(string[] args)
        {
            /*
            ***** Условие задачи ***** 

            Сформировать массив случайных целых чисел (размер  задается пользователем). Вычислить сумму чисел массива
            и максимальное число в массиве. Реализовать решение задачи с использованием механизма задач продолжения.
            */
            Console.WriteLine("Вычисление суммы и определение максимума массива случайных целых чисел");

            // Создаётся массив, длина которого запрашивается у пользователя в методе InputArrayLength
            int[] array = new int[InputArrayLength()];
            // Массив заполняется случайными значениями с помощью метода SetRandomArrayValues, в качестве fheuvtynjd
            // принимающий сам массив, и константы с лимитами на значения генератора случайных чисел
            SetRandomArrayValues(array, arrayMinValueLimit, arrayMaxValueLimit);

            // Создаётся задача вычисления суммы с неявным использованием делегата Func с помощью лямбда-выражения,
            // поскольку присваиваемый делегату метод имеет параметр (обрабатываемый массив)
            Task<int> taskSum = new Task<int>(() => ArraySum(array));

            // Создаётся задача вычисления максимума в качестве задачи продолжения с неявным использованием делегата Func
            // с помощью лямбда-выражения, поскольку присваиваемый делегату метод имеет параметр (обрабатываемый массив)
            Task<int> taskMax = taskSum.ContinueWith(task => FindMaxInArray(array));

            // Запускается задача вычисления суммы, которая продолжится выполнением задачи поиска максимума
            taskSum.Start();

            // Результат вычисления суммы будет выведен после завершения первой задачи и до начала второй
            Console.WriteLine($"\nСумма значений массива равна {taskSum.Result}\n");

            // Результат поиска максимума будет выведен после завершения второй задачи, то есть в самом конце
            Console.WriteLine($"\nМаксимум среди значений массива равен {taskMax.Result}\n");

            Console.ReadKey();
        }

        static int InputArrayLength()
        // Метод запрашивает у пользователя длину массива и обрабатывает ошибки ввода
        {
            int arrayLength;
            while (true)
            {
                Console.Write("\nВведите длину массива: ");
                try
                {
                    arrayLength = Convert.ToInt32(Console.ReadLine());
                    if (arrayLength > 0)
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception("Значение длины массива — неположительное число.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nОшибка! {ex.Message}");
                }
            }
            Console.WriteLine();
            return arrayLength;
        }
        static void SetRandomArrayValues(int[] array, int minvaluelimit, int maxvaluelimit)
        // Метод принимает в качестве параметра массив и заполняет его случайными числами, лимиты значений которых
        // также являются параметрами метода
        {
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minvaluelimit, maxvaluelimit);
            }
        }
        static int ArraySum(int[] array)
        // Метод принимает в качестве параметра массив и находит сумму его значений.
        // На каждой итерации вычислений в консоль выводится промежуточная сумма.
        {
            int sum = 0;
            foreach (int item in array)
            {
                sum += item;
                Console.WriteLine($"{"Промежуточная сумма", -23}:{sum,10}");
            }
            return sum;
        }
        static int FindMaxInArray(int[] array)
        // Метод принимает в качестве параметра массив и находит максимум среди его значений.
        // На каждой итерации вычислений в консоль выводится промежуточный максимум.
        {
            int max = -2147483648; // В переменную максимума по умолчанию заносим минимум для типа int
            foreach (int item in array)
            {
                if (item > max)
                {
                    max = item;
                }
                Console.WriteLine($"{"Промежуточный максимум",-23}:{max,10}");
            }
            return max;
        }
    }
}
