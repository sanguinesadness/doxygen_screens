using System;

namespace DataStruct
{
    //!  Исключение, которое выдается, когда значение аргумента представляет собой отрицательное значение.
    /*!
      \see ArgumentException
    */
    class NegativeCountException : ArgumentException
    {
        //!  Инициализирует новый экземпляр класса NegativeCountException.
        /*!
          \param message Сообщение об ошибке.
        */
        public NegativeCountException(string message) : base(message) { }
    }
}
