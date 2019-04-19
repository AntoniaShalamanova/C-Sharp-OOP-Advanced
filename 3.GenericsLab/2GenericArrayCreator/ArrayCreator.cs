﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GenericArrayCreator
{
    public static class ArrayCreator
    {
        public static T[] Create<T>(int length, T item)
        {
            T[] items = new T[length];

            for (int i = 0; i < length; i++)
            {
                items[i] = item;
            }

            return items;
        }
    }
}
