using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CA4;
using System.Reflection;

namespace Testeo_de_BST
{
    [TestClass]
    public class BSTTests
    {
        private BST CreateTestTree()
        {
            BST tree = new BST();
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(6);
            return tree;
        }

        // Helper method to invoke private methods
        private T InvokePrivateMethod<T>(object instance, string methodName, params object[] parameters)
        {
            Type type = instance.GetType();
            MethodInfo method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)method.Invoke(instance, parameters);
        }

        [TestMethod]
        public void Node_Constructor_SetsKeyAndChildren()
        {
            // Arrange & Act
            var node = new Node(10);

            // Assert
            Assert.AreEqual(10, node.Key);
            Assert.IsNull(node.Left);
            Assert.IsNull(node.Right); // Corregido: Cambiado de Right a Right
        }

        [TestMethod]
        public void BST_Constructor_InitializesRootAsNull()
        {
            // Arrange & Act
            var bst = new BST();

            // Assert
            Assert.IsNull(bst.Root);
        }

        [TestMethod]
        public void Insert_FirstElement_BecomesRoot()
        {
            // Arrange
            var bst = new BST();

            // Act
            bst.Insert(10);

            // Assert
            Assert.IsNotNull(bst.Root);
            Assert.AreEqual(10, bst.Root.Key);
        }

        [TestMethod]
        public void InsertRecursive_LeftAndRightPlacement_Correct()
        {
            // Arrange
            var bst = new BST();

            // Act
            var root = InvokePrivateMethod<Node>(bst, "InsertRecursive", null, 10);
            root = InvokePrivateMethod<Node>(bst, "InsertRecursive", root, 5);
            root = InvokePrivateMethod<Node>(bst, "InsertRecursive", root, 15);
            root = InvokePrivateMethod<Node>(bst, "InsertRecursive", root, 3);
            root = InvokePrivateMethod<Node>(bst, "InsertRecursive", root, 7);

            // Assert
            Assert.AreEqual(10, root.Key);
            Assert.AreEqual(5, root.Left.Key);
            Assert.AreEqual(15, root.Right.Key);
            Assert.AreEqual(3, root.Left.Left.Key);
            Assert.AreEqual(7, root.Left.Right.Key);
        }

        [TestMethod]
        public void Insert_DuplicateValue_DoesNothing()
        {
            // Arrange
            var bst = new BST();
            bst.Insert(10);

            // Act
            bst.Insert(10);

            // Assert
            Assert.AreEqual(10, bst.Root.Key);
            Assert.IsNull(bst.Root.Left);
            Assert.IsNull(bst.Root.Right);
        }

        [TestMethod]
        public void MinValue_FindsMinimumInSubtree()
        {
            // Arrange
            var bst = new BST();

            // Build a subtree
            var root = new Node(10)
            {
                Left = new Node(5)
                {
                    Left = new Node(3),
                    Right = new Node(7)
                },
                Right = new Node(15)
            };

            // Act
            int min = InvokePrivateMethod<int>(bst, "MinValue", root);

            // Assert
            Assert.AreEqual(3, min);
        }

        [TestMethod]
        public void Delete_LeafNode_RemovesCorrectly()
        {
            // Arrange
            BST tree = CreateTestTree();

            // Act
            tree.Delete(2);

            // Assert
            Assert.IsNull(tree.Root.Left.Left);
            Assert.AreEqual(4, tree.Root.Left.Right.Key);
        }

        [TestMethod]
        public void Delete_NodeWithTwoChildren_RemovesCorrectly()
        {
            // Arrange
            BST tree = CreateTestTree();

            // Act
            tree.Delete(3);

            // Assert
            Assert.AreEqual(4, tree.Root.Left.Key);
            Assert.AreEqual(2, tree.Root.Left.Left.Key);
        }
    }
}

