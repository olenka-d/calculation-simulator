//ще тестуватиму на пошук помилок
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal class Program
    {
        static void CreateArray(int limit, int[] array, int variants_length, string operation, out int[,] main_array) //створення масиву за заданими лімітами
        {
            int a,b,k = 0;
            main_array = new int[variants_length, 2];
            bool condition = false;

            for (int i = 0; i < limit; i++)
            {
                for (int j = 0; j < limit; j++)
                {
                    a = array[i];
                    b = array[j];

                    switch (operation)
                    {
                        case "+":
                            condition = a + b > limit;
                            break;
                        case "-":
                            condition = a - b == 0 || a < b;
                            break;
                        case "*":
                            condition = a * b > limit;
                            break;
                        case "/":
                            condition = a % b != 0 || a < b;
                            break;
                    }

                    if (condition)
                    {
                        continue;
                    }
                    else
                    {
                        main_array[k, 0] = a;
                        main_array[k, 1] = b;
                        k++;
                    }
                }
            }
        }
        static void Processing(string operation, out bool work)  //отримання даних з консолі, виклик методу генерацій прикладів та підрахунок результатів
        {
            Console.Write("Введіть кількість прикладів: ");
            int count_exercises = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введіть ліміт обчислень: ");
            int limit = Convert.ToInt32(Console.ReadLine());

            GenerationExercise(ref count_exercises, out int count_right_user_answer, operation, limit);
            Console.WriteLine($"Результат: {count_right_user_answer} з {count_exercises} правильних відповідей!");
            Console.WriteLine();
            work = true;
            while (work)
            {
                Console.Write("Повернутися до головного меню (1 - так, 0 - ні): ");
                int answer = Convert.ToInt32(Console.ReadLine());
                if (answer == 1)
                {
                    work = true;
                    break;
                }
                else if (answer == 0)
                {
                    work = false;
                }
                else
                {
                    Console.WriteLine("Oops...невірно обране меню, спробуйте ще раз.");
                }
            }
            Console.WriteLine();
        }
        static int CheckRightAnswer(int a, int b, string operation)  //повертає правильну відповідь
        {
            int right_answer = 0;
            switch (operation)
            {
                case "+":
                    right_answer = a + b;
                    break;
                case "-":
                    right_answer = a - b;
                    break;
                case "*":
                    right_answer = a * b;
                    break;
                case "/":
                    right_answer = a / b;
                    break;
            }
            return right_answer;
        }
        static void GenerationExercise(ref int count_exercises, out int count_right_user_answer, string operation, int limit) //генерація прикладів
        {
            int[] array = new int[limit];
            for (int n = 0; n < limit; n++)
            {
                array[n] = n + 1;
            }
            int a, b, answer, variants_length = 0;
            count_right_user_answer = 0;

            switch (operation) // формули для підрахунку загальної кількості варіантів 
            {
                case "+":
                    variants_length = ((limit / 2) * (limit - 1));
                    break;
                case "-":
                    variants_length = ((limit / 2) * (limit - 1));
                    break;
                case "*":
                    for (int j = 1; j <= limit; j++)
                    {
                        variants_length += limit / j;
                    }
                    break;
                case "/":
                    for (int l = 1; l <= limit; l++) 
                    {
                        for (int j = 1; j <= limit; j++)
                        {
                            if (l % j != 0)
                            {
                                continue;
                            }
                            variants_length += 1;
                        }
                    }
                    break;
            }
            CreateArray(limit, array, variants_length, operation, out int[,] main_array);

            if (main_array.GetLength(0) < count_exercises)
            {
                count_exercises = main_array.GetLength(0);
                Console.WriteLine("Кількість прикладів більша за можливі згенеровані приклади, виведеться можлива кількість без повторень");
            }

            int i = (main_array.GetLength(0) / count_exercises) - 1;
            int r = 0;

            while (i < main_array.GetLength(0) && r < count_exercises)
            {
                a = main_array[i, 0];
                b = main_array[i, 1];
                Console.Write($"{a} {operation} {b} = ");
                answer = Convert.ToInt32(Console.ReadLine());
                int right_answer = CheckRightAnswer(a, b, operation);
                count_right_user_answer = (answer == right_answer) ? count_right_user_answer + 1 : count_right_user_answer;
                i += (main_array.GetLength(0) / count_exercises);
                r++;
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Привіт! Вітаю в програмі з усного рахування!");
            bool work = true;
            while (work)
            {
                Console.WriteLine("Навігація програми:");
                Console.WriteLine("додавання - 1, віднімання - 2, множення - 3, ділення - 4, вихід - 0");
                Console.Write("Введіть режим програми: ");
                int menu = Convert.ToInt32(Console.ReadLine());
                string operation; //змінна для збереження матем.операції

                switch (menu)
                {
                    case 0:
                        work = false;
                        break;
                    case 1:
                        operation = "+";
                        Processing(operation, out work);
                        break;
                    case 2:
                        operation = "-";
                        Processing(operation, out work);
                        break;
                    case 3:
                        operation = "*";
                        Processing(operation, out work);
                        break;
                    case 4:
                        operation = "/";
                        Processing(operation, out work);
                        break;
                    default:
                        Console.WriteLine("Oops...невірно обране меню, спробуйте ще раз.");
                        break;
                }
            }

        }
    }
}

