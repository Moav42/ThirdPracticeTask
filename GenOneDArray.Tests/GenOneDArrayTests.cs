using NUnit.Framework;
using GenOneDArray;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GenOneDArray.Tests
{
    [TestFixture]
    public class Tests
    {
        GenOneDArray<int> array;

        [Test]
        public void AddingTest()
        {
            //Arrenge
            array = new GenOneDArray<int>(42, 10);

            //Act
            array[42] = 10;

            //Assert
            Assert.AreEqual(array[42], 10);
        }

        [Test]
        public void IndexOfTest()
        {
            //Arrenge
            array = new GenOneDArray<int>(42, 10);

            //Act
            array[42] = 10;

            //Assert
            Assert.AreEqual(array.IndexOf(10), 42);
        }

        [Test]
        public void EnumeratorTest()
        {
            //Arrenge
            GenOneDArray<int> array = new GenOneDArray<int>(42, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            List<int> values = new List<int>();

            //Act
            foreach (var item in array)
            {
                values.Add(item);
            }

            //Assert
            CollectionAssert.AreEqual(array.Elements, values);
        }

    }
}