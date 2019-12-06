using NUnit.Framework;
using BinaryTree;
using System.Collections;
using System.Collections.Generic;


namespace BinaryTree.Tests
{
    [TestFixture]
    class BinaryTreeTests
    {
        BinaryTree<int> bt;

        [Test]
        public void AddNodeTest()
        {
            //Arrenge
            bt = new BinaryTree<int>();

            //Act
            bt.AddRange(42, 2, 44, 3);

            //Assert
            Assert.AreEqual(bt.Count, 4);
            Assert.AreEqual(bt.Head.Value, 42);
            Assert.AreEqual(bt.Head.Left.Value, 2);
            Assert.AreEqual(bt.Head.Right.Value, 44);
            Assert.AreEqual(bt.Head.Left.Right.Value, 3);
        }

        [Test]
        public void RemoveNodeTest()
        {
            //Arrenge
            bt = new BinaryTree<int>();

            //Act
            bt.AddRange(42, 2, 44, 1, 3);
            bt.Remove(44);
            bt.Remove(1);
            bt.Remove(3);

            //Assert
            Assert.AreEqual(bt.Count, 2);
            Assert.IsNull(bt.Head.Right);
            Assert.IsNull(bt.Head.Left.Left);
            Assert.IsNull(bt.Head.Left.Right);

        }

        [Test]
        public void InOrederTrevalTest()
        {
            //Arrenge
            bt = new BinaryTree<int>();
            List<int> values = new List<int>();

            //Act
            bt.Add(42);
            bt.Add(2);
            bt.Add(44);
            bt.Add(1);
            bt.Add(43);
            foreach (var item in bt)
            {
                values.Add(item);
            }

            //Assert
            CollectionAssert.AreEqual(values, new[] { 1, 2, 42, 43, 44 });

        }
    }
}
