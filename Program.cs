using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*! \mainpage Основная информация
 *
 * \section intro_section Что это за проект?
 * 
 * Программа представляет собой консольное приложение, реализованное на платформе .NET Framework. \n
 * Вам предоставляется возможность использования специально созданной структуры данных - \link DataStruct.DoublyLinkedList двусвязного списка\endlink. Мы можете добавлять, удалять и искать элементы списка. \n
 * В программе также предусмотрена \link DataStruct.Program.BubbleSort функция\endlink сортировки по возрастанию методом пузырька.
 * 
 * \section definition_section Определение
 *
 * Двусвязный список - это структура данных, состоящая из узлов. Ссылки в каждом узле указывают на предыдущий и на последующий узел в списке. Как и односвязный список, двусвязный допускает только последовательный доступ к элементам, но при этом дает возможность перемещения в обе стороны.
 *
 * \section usage_section Использование
 *
 * \subsection step1 Шаг 1: Откройте приложение
 * \subsection step2 Шаг 2: Введите элементы списка в строку через пробелы (целые числа)
 * \subsection step3 Шаг 3: Нажмите Enter
 *
 * \sa \link DataStruct.DoublyLinkedList DoublyLinkedList\endlink - двусвязный список.
 * \sa \link DataStruct.Item Item\endlink - элемент двусвязаного списка.
 */
namespace DataStruct
{
    //! Главный класс, содержащий метод Main.
    /*!
    */
    class Program
    {
        //! Точка входа в приложение.
        /*!
        */
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите элементы в строку:");
            string input = Console.ReadLine();

            DoublyLinkedList<int> nums = new DoublyLinkedList<int>();
            input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach((a) => nums.Add(int.Parse(a)));

            Console.WriteLine("Список до сортировки:");
            nums.Print();

            BubbleSort(ref nums);

            Console.WriteLine("Список после сортировки:");
            nums.Print();

            Console.ReadKey();
        }

        //! Сортировка по возрастанию списка DoublyLinkedList<int> методом пузырька.
        /*!
         \param list Ссылка на список, который необходимо отсортировать.
        */
        public static void BubbleSort(ref DoublyLinkedList<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        int temp = list[j + 1];
                        list[j + 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
        }
    }
}
