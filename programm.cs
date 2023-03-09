using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    internal class Program
    {   
        static void Addition(int limit, int[] array, out int[,] main_array) // генерація варіантів для додавання
        {
            int a, b, k = 0;

            int variants_length = (limit / 2) * (limit - 1); // формула для підрахунку загальної кількості варіантів 
            main_array = new int[variants_length, 2];

            for (int i = 0; i < limit; i++)
            {
                for (int j = 0; j < limit; j++)
                {
                    a = array[i];
                    b = array[j];
                    if ((a + b > limit) || a == 0 || b == 0)
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
        static void Subtraction(int limit, int[] array, out int[,] main_array) // генерація варіантів для віднімання
        {
            int a, b, k = 0;
            int variants_length = ((limit / 2) * (limit - 1)); // формула для підрахунку загальної кількості варіантів 
            main_array = new int[variants_length, 2];

            for (int i = 0; i < limit; i++)
            {
                for (int j = 0; j < limit; j++)
                {
                    a = array[i];
                    b = array[j];
                    if ((a > limit && b > limit) || a == 0 || b == 0 || a < b || a - b == 0)
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
        static void Multiplication(int limit, int[] array, out int[,] main_array) //генерація варіантів для множення
        {
            int a, b, k = 0;
            int variants_length = 0; 
            for(int i = 1; i<=limit; i++)
            {
                variants_length += limit / i;   // формула для підрахунку загальної кількості варіантів 
            }
            main_array = new int[variants_length, 2]; 

            for (int i = 0; i < limit; i++)
            {
                for (int j = 0; j < limit; j++)
                {
                    a = array[i];
                    b = array[j];
                    if ((a * b > limit) || a == 0 || b == 0)
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
        static void Division(int limit, int[] array, out int[,] main_array) // генерація варіантів для ділення
        {
            int a, b, k = 0;
            int variants_length = 0;
            for (int i = 1; i <= limit; i++) // формула для підрахунку загальної кількості варіантів 
            {
                for (int j = 1; j <= limit; j++)
                {
                    if (i % j != 0)
                    {
                        continue;
                    }
                    variants_length += 1; 
                }
            }
            main_array = new int[variants_length, 2]; 

            for (int i = 0; i < limit; i++)
            {
                for (int j = 0; j < limit; j++)
                {
                    a = array[i];
                    b = array[j];
                    if (a > limit || b > limit || a == 0 || b == 0 || a < b || a % b !=0)
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

            GenerationExercise(count_exercises, out int count_right_user_answer, operation, limit);
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
        static void GenerationExercise(int count_exercises, out int count_right_user_answer, string operation, int limit) //генерація прикладів
        {
            int[] array = new int[limit];
            for (int n = 0; n < limit; n++)
            {
                array[n] = n + 1;
            }
            int a, b, answer;
            count_right_user_answer = 0;
            int[,] main_array = null;

            switch (operation)
            {
                case "+":
                    Addition(limit, array, out main_array);
                    break;
                case "-":
                    Subtraction(limit, array, out main_array);
                    break;
                case "*":
                    Multiplication(limit, array, out main_array);
                    break;
                case "/":
                    Division(limit, array, out main_array);
                    break;
            }

            int i = (main_array.GetLength(0) / count_exercises)-1;
            while (i < main_array.GetLength(0))
            {
                a = main_array[i, 0];
                b = main_array[i, 1];
                Console.Write($"{a} {operation} {b} = ");
                answer = Convert.ToInt32(Console.ReadLine());
                int right_answer = CheckRightAnswer(a, b, operation);
                count_right_user_answer = (answer == right_answer) ? count_right_user_answer + 1 : count_right_user_answer;
                i += (main_array.GetLength(0) / count_exercises);
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
