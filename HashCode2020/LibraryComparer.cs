using System;
using System.Collections.Generic;
using System.Text;

namespace HashCode2020
{
    class LibraryComparer : IComparer<Library>
    {
        public int Compare(Library x, Library y)
            {
                //first by priority
                int result = y.Priority.CompareTo(x.Priority);

                //then id
                if (result == 0)
                    result = y.Id.CompareTo(x.Id);

                return result;
            }
    }
}
