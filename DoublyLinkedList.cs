using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStruct
{
    //!  Двунаправленный список.
    /*!
      Представляет строго типизированный список объектов, к которым можно получить доступ по индексу. Предоставляет методы для поиска и управления списками. Оперирует данными типа Item<T>.
      \see Item
    */
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        private static Random _randomInstance = new Random();

        //! Представляет собой первый элемент в списке.
        /*!
          Previous ссылка этого элемента всегда равна null.
        */
        public Item<T> Head { get; private set; }

        //! Представляет собой последний элемент в списке
        /*!
          Next ссылка этого элемента всегда равна null.
        */
        public Item<T> Tail { get; private set; }

        //! Получает количество элементов, содержащихся в DoublyLinkedList<T>.
        /*!
        */
        public int Count
        {
            get
            {
                if (Head == null)
                    return 0;

                int count = 0;
                Item<T> tmp = Head;
                while (tmp != null)
                {
                    count++;
                    tmp = tmp.Next;
                }
                return count;
            }
        }

        //! Инициализирует новый пустой экземпляр класса DoublyLinkedList<T> с начальной емкостью по умолчанию.
        /*!
        */
        public DoublyLinkedList()
        {
            Initialize(null);
        }

        //! Инициализирует новый пустой экземпляр DoublyLinkedList<T> с указанной начальной емкостью.
        /*!
          \param capacity Количество элементов, которые изначально может хранить новый список.
        */
        public DoublyLinkedList(int capacity)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException(nameof(capacity));
            }
            if (capacity == 0)
            {
                Initialize(null);
                return;
            }

            Item<T> tmp = new Item<T>();
            Initialize(tmp);

            for (int i = 1; i < capacity; i++)
            {
                tmp = new Item<T>();
                Tail.Next = tmp;
                tmp.Previous = Tail;
                Tail = tmp;
            }
        }

        //! Инициализирует новый экземпляр DoublyLinkedList<T> с указанной начальной емкостью и заполняет его копиями переданного элемента.
        /*!
          \param capacity Количество элементов, которые изначально может хранить новый список.
          \param data Элемент, копиями которого необходимо наполнить новый список.
        */
        public DoublyLinkedList(int capacity, T data)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException(nameof(capacity));
            }
            if (capacity == 0)
            {
                Initialize(new Item<T>(data));
                return;
            }

            Item<T> tmp = new Item<T>(data);
            Initialize(tmp);

            //Добавление в начало
            for (int i = 1; i < capacity; i++)
            {
                tmp = new Item<T>(data);
                Tail.Next = tmp;
                tmp.Previous = Tail;
                Tail = tmp;
            }
        }

        //! Инициализирует новый экземпляр DoublyLinkedList<T>, который содержит элементы, скопированные из указанной коллекции, и имеет достаточную емкость для размещения количества скопированных элементов.
        /*!
          \param sample Коллекция, элементы которой копируются в новый список.
        */
        public DoublyLinkedList(IEnumerable<T> sample)
        {
            bool isFirstIteration = true;

            foreach (T human in sample)
            {
                Item<T> tmp = new Item<T>(human);

                if (isFirstIteration)
                {
                    Initialize(tmp);
                    isFirstIteration = false;
                    continue;
                }

                Tail.Next = tmp;
                tmp.Previous = Tail;
                Tail = tmp;
            }
        }

        //! Получает или задает элемент по указанному индексу.
        /*!
          \param index Отсчитываемый от нуля индекс элемента, который нужно получить или установить.
        */
        public T this[int index]
        {
            get
            {
                if (Head == null)
                {
                    throw new NullReferenceException();
                }

                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                Item<T> tmp = Head;

                for (int i = 0; i < Count; i++)
                {
                    if (i == index)
                        return tmp.Data;
                    tmp = tmp.Next;
                }

                return default(T);
            }
            set
            {
                if (Head == null)
                {
                    throw new NullReferenceException();
                }

                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                Item<T> newItem = new Item<T>(value);

                if (index == 0)
                {
                    Item<T> saveNext = Head.Next;
                    Head = newItem;
                    Head.Next = saveNext;
                    return;
                }

                if (index == Count - 1)
                {
                    Tail.Previous.Next = newItem;
                    newItem.Previous = Tail.Previous;
                    Tail = newItem;
                    return;
                }

                Item<T> tmp = Head;

                for (int i = 0; i < Count; i++)
                {
                    if (i == index)
                    {
                        tmp.Previous.Next = newItem;
                        newItem.Previous = tmp.Previous;
                        newItem.Next = tmp.Next;
                        newItem.Next.Previous = newItem;
                        return;
                    }
                    tmp = tmp.Next;
                }
            }
        }

        //! Выводит элементы списка в консоль.
        /*!
        */
        public void Print()
        {
            if (Head == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(">> LIST IS EMPTY <<");
                Console.ResetColor();
                return;
            }

            Item<T> tmp = Head;

            ConsoleColor randomColor = (ConsoleColor)_randomInstance.Next(1, 16);

            while (tmp != null)
            {
                Console.ResetColor();
                if (tmp.Data == null)
                {
                    Console.WriteLine("NULL");
                    tmp = tmp.Next;
                    continue;
                }
                Console.Write(tmp.Data.GetType().Name);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(" >> ");

                Console.ForegroundColor = randomColor;
                Console.WriteLine(tmp.ToString());
                tmp = tmp.Next;
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        //! Добавляет объект в конец DoublyLinkedList<T>.
        /*!
         \param item Объект, который нужно добавить в конец DoublyLinkedList<T>. Для ссылочных типов значение может быть null.
        */
        public void Add(T item)
        {
            Item<T> newItem = new Item<T>(item);

            if (Tail == null)
            {
                Initialize(newItem);
                return;
            }

            newItem.Previous = Tail;
            Tail.Next = newItem;
            Tail = newItem;
        }

        //! Добавляет объект в начало DoublyLinkedList<T>.
        /*!
         \param item Объект, который нужно добавить в начало DoublyLinkedList<T>. Для ссылочных типов значение может быть null.
        */
        public void AddLeft(T item)
        {
            Item<T> newItem = new Item<T>(item);

            if (Head == null)
            {
                Initialize(newItem);
                return;
            }

            Head.Previous = newItem;
            newItem.Next = Head;
            Head = newItem;
        }

        //! Добавляет объект в DoublyLinkedList<T> под заданным индексом.
        /*!
         \param index Отсчитываемый от нуля индекс элемента, на который необходимо "поставить" объект.
         \param item Объект, который нужно добавить в DoublyLinkedList<T>. Для ссылочных типов значение может быть null.
        */
        public void AddAt(int index, T item)
        {
            Item<T> newItem = new Item<T>(item);

            if (Head == null && index == 0)
            {
                Initialize(newItem);
                return;
            }

            if (index == 0)
            {
                Head.Previous = newItem;
                newItem.Next = Head;
                Head = newItem;
                return;
            }

            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            Item<T> tmp = Head;

            for (int i = 0; i < Count; i++)
            {
                if (i == index)
                {
                    newItem.Next = tmp;
                    newItem.Previous = tmp.Previous;
                    tmp.Previous.Next = newItem;
                    tmp.Previous = newItem;
                    return;
                }
                tmp = tmp.Next;
            }
        }

        //! Ищет элемент item и возвращает первое вхождение во всем DoublyLinkedList<T>.
        /*!
         \param item Объект, который нужно найти в DoublyLinkedList<T>. Значение не может быть null.
         \return item, если элемент найден в DoublyLinkedList<T>; в противном случае - default.
        */
        public T Find(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Item<T> tmp = Head;

            while (tmp != null)
            {
                if (tmp.Data.Equals(item))
                    return tmp.Data;
                tmp = tmp.Next;
            }

            return default(T);
        }

        //! Определяет, есть ли элемент в DoublyLinkedList<T>.
        /*!
         \param item Объект, который нужно найти в DoublyLinkedList<T>. Для ссылочных типов значение может быть null.
         \return true, если item найден в DoublyLinkedList<T>; в противном случае - false.
        */
        public bool Contains(T item)
        {
            Item<T> tmp = Head;

            while (tmp != null)
            {
                if (tmp.Data.Equals(item))
                    return true;
                tmp = tmp.Next;
            }
            return false;
        }

        //! Удаляет первое вхождение определенного объекта из DoublyLinkedList<T>.
        /*!
         \param item Объект, который нужно удалить из DoublyLinkedList<T>. Для ссылочных типов значение может быть null.
         \return true, если item был успешно удален; в противном случае - false. Этот метод также возвращает false, если item не был найден в DoublyLinkedList<T>.
        */
        public bool Remove(T item)
        {
            if (Head == null)
            {
                return false;
            }

            if (Head.Data.Equals(item))
            {
                Head = Head.Next;
                return true;
            }

            Item<T> tmp = Head;

            while (tmp != null)
            {
                if (tmp.Data.Equals(item))
                {
                    tmp.Previous.Next = tmp.Next;
                    if (tmp != Tail)
                        tmp.Next.Previous = tmp.Previous;
                    return true;
                }
                tmp = tmp.Next;
            }

            return false;
        }

        //! Удаляет объект под заданным индексом из DoublyLinkedList<T>.
        /*!
         \param index Отсчитываемый от нуля индекс удаляемого элемента.
        */
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            if (Head == null)
            {
                return;
            }

            Item<T> tmp = Head;

            for (int i = 0; i < Count; i++)
            {
                if (i == index)
                {
                    if (tmp == Head)
                    {
                        Head = Head.Next;
                    }
                    else if (tmp == Tail)
                    {
                        Tail.Previous.Next = null;
                    }
                    else
                    {
                        tmp.Previous.Next = tmp.Next;
                        tmp.Next.Previous = tmp.Previous;
                    }
                    return;
                }
                tmp = tmp.Next;
            }
        }

        //! Удаляет все вхождения указанного объекта из DoublyLinkedList<T>.
        /*!
         \param item Объект, вхождения которого нужно удалить из DoublyLinkedList<T>. Для ссылочных типов значение может быть null.
         \return true, если все вхождения item были успешно удалены; в противном случае - false. Этот метод также возвращает false, если item не был найден в DoublyLinkedList<T>.
        */
        public bool RemoveAll(T item)
        {
            if (Head == null)
            {
                return false;
            }

            bool isRemoved = false;
            while (Head.Data.Equals(item))
            {
                Head = Head.Next;
                isRemoved = true;
            }

            Item<T> tmp = Head;

            while (tmp != null)
            {
                if (tmp.Data.Equals(item))
                {
                    tmp.Previous.Next = tmp.Next;
                    if (tmp != Tail)
                        tmp.Next.Previous = tmp.Previous;
                    isRemoved = true;
                }
                tmp = tmp.Next;
            }

            return isRemoved;
        }

        //! Удаляет диапазон элементов из DoublyLinkedList<T>.
        /*!
         \param startIndex Отсчитываемый от нуля начальный индекс диапазона удаляемых элементов.
         \param count Количество удаляемых элементов.
        */
        public void RemoveRange(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= Count || (startIndex + count) > Count)
            {
                throw new IndexOutOfRangeException(nameof(startIndex));
            }
            if (count == 0)
            {
                return;
            }
            if (count < 0)
            {
                throw new NegativeCountException(nameof(count));
            }

            Item<T> tmp = Head;

            for (int i = 0; i < Count; i++)
            {
                if (i == startIndex)
                {
                    for (int j = 0; j < count; j++)
                        this.RemoveAt(i);
                }
                tmp = tmp.Next;
            }
        }

        //! Удаляет все элементы из DoublyLinkedList<T>.
        /*!
        */
        public void Clear()
        {
            Initialize(null);
        }

        private void Initialize(Item<T> item)
        {
            Head = item;
            Tail = item;
        }

        //! Удаляет все элементы из списка DoublyLinkedList<T>.
        /*!
         \return Список DoublyLinkedList<T>.Enumerator для DoublyLinkedList<T>.
        */
        public IEnumerator<T> GetEnumerator()
        {
            Item<T> tmp = Head;

            while (tmp != null)
            {
                yield return tmp.Data;
                tmp = tmp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}