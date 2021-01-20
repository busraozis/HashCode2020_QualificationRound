using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace HashCode2020
{
    class Library
    {
        public int Id;
        public int SignUpTime;
        public int ScanCapacity;
        public SortedSet<Book> Books;
        public double Priority;

        public Library(int id, int signUpTime, int scanCapacity, SortedSet<Book> books)
        {
            this.SignUpTime = signUpTime;
            this.Id = id;
            this.ScanCapacity = scanCapacity;
            this.Books = books;
        }


        public void SetPriority(double priority)
        {
            this.Priority = priority;
        }

    }
}

