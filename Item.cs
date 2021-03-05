using System;

namespace DataStruct
{
    //!  Элемент двунаправленного списка.
    /*!
      Представляет из себя строго типизированный объект, хранящий данные типа T. Имеет ссылки на предыдущий и следующий элементы списка. Используется при реализации DoublyLinkedList<T>.
      \see DoublyLinkedList
    */
    public class Item<T>
    {
        /**
        * Хранит данные пользователя типа T.
        */
        public T Data { get; set; }

        /**
        * Хранит ссылку на следующий элемент списка.
        */
        public Item<T> Next { get; set; }

        /**
        * Хранит ссылку на предыдущий элемент списка.
        */
        public Item<T> Previous { get; set; }

        //! Инициализирует свойство Data передаваемым параметром и выполняет null-инициализацию свойств Next и Previous.
        /*!
          \param data Информация типа T, которую нужно записать в экземпляр.
          \sa Item()
        */
        public Item(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }

        //! Инициализирует свойство Data по умолчанию и выполняет null-инициализацию свойств Next и Previous.
        /*!
          \sa Item(T data)
        */
        public Item()
        {
            Data = default(T);
            Next = null;
            Previous = null;
        }

        //! Переопределяемый метод.
        /*!
          \return Строчное представление данных из Data.
        */
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
