using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode2020
{
    class BookComparer : IComparer<Book>
    {

        public int Compare(Book x, Book y)
        {
            //first by priority
            int result = y.Score.CompareTo(x.Score);

            //then id
            if (result == 0)
                result = y.Id.CompareTo(x.Id);

            return result;
        }
    }
}
