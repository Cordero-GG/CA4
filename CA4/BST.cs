using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA4
{
   public class Node
    {
        public int Key;          // Valor del nodo
        public Node Left;        // Hijo izquierdo
        public Node Right;       // Hijo derecho

        public Node(int key)
        {
            Key = key;
            Left = Right = null;
        }
    }

    // Clase principal del BST
    public class BST
    {
        public Node Root;        // Raíz del árbol

        // Constructor
        public BST()
        {
            Root = null;
        }

        // Método para insertar un valor (público)
        public void Insert(int key)
        {
            Root = InsertRecursive(Root, key);
        }

        // Método auxiliar para inserción (recursivo)
        private Node InsertRecursive(Node root, int key)
        {
            if (root == null)
            {
                return new Node(key); // Crear nuevo nodo si el árbol/subárbol está vacío
            }

            if (key < root.Key)
            {
                root.Left = InsertRecursive(root.Left, key); // Insertar en subárbol izquierdo
            }
            else if (key > root.Key)
            {
                root.Right = InsertRecursive(root.Right, key); // Insertar en subárbol derecho
            }

            return root; // Si el valor ya existe, no se hace nada
        }

        // Método para eliminar un valor (público)
        public void Delete(int key)
        {
            Root = DeleteRecursive(Root, key);
        }

        // Método auxiliar para eliminación (recursivo)
        private Node DeleteRecursive(Node root, int key)
        {
            if (root == null) return root; // Nodo no encontrado

            // Buscar el nodo a eliminar
            if (key < root.Key)
            {
                root.Left = DeleteRecursive(root.Left, key);
            }
            else if (key > root.Key)
            {
                root.Right = DeleteRecursive(root.Right, key);
            }
            else
            {
                // Caso 1: Nodo hoja o con un solo hijo
                if (root.Left == null)
                {
                    return root.Right; // Retorna el hijo derecho (o null si es hoja)
                }
                else if (root.Right == null)
                {
                    return root.Left;  // Retorna el hijo izquierdo
                }

                // Caso 2: Nodo con dos hijos
                // Reemplazar con el sucesor (mínimo del subárbol derecho)
                root.Key = MinValue(root.Right);
                // Eliminar el sucesor original
                root.Right = DeleteRecursive(root.Right, root.Key);
            }

            return root;
        }

        // Método para encontrar el mínimo valor en un subárbol
        private int MinValue(Node root)
        {
            int min = root.Key;
            while (root.Left != null)
            {
                min = root.Left.Key;
                root = root.Left;
            }
            return min;
        }

        // --- Recorridos del Árbol ---
        // 1. Inorden (Izquierda → Raíz → Derecha)
        public void InOrder(Node root)
        {
            if (root != null)
            {
                InOrder(root.Left);
                Console.Write(root.Key + " ");
                InOrder(root.Right);
            }
        }

        // 2. Preorden (Raíz → Izquierda → Derecha)
        public void PreOrder(Node root)
        {
            if (root != null)
            {
                Console.Write(root.Key + " ");
                PreOrder(root.Left);
                PreOrder(root.Right);
            }
        }

        // 3. Postorden (Izquierda → Derecha → Raíz)
        public void PostOrder(Node root)
        {
            if (root != null)
            {
                PostOrder(root.Left);
                PostOrder(root.Right);
                Console.Write(root.Key + " ");
            }
        }

        // 4. Por Niveles (BFS usando Queue)
        public void LevelOrder(Node root)
        {
            if (root == null) return;

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                Console.Write(current.Key + " ");

                if (current.Left != null) queue.Enqueue(current.Left);
                if (current.Right != null) queue.Enqueue(current.Right);
            }
        }
    }

    // Clase principal para probar el BST
    class Program
    {
        static void Main()
        {
            BST tree = new BST();

            // Insertar valores
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Insert(2);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(6);

            Console.WriteLine("Recorrido Inorden (ordenado):");
            tree.InOrder(tree.Root); // 2 3 4 5 6 7 8

            Console.WriteLine("\n\nRecorrido Preorden:");
            tree.PreOrder(tree.Root); // 5 3 2 4 7 6 8

            Console.WriteLine("\n\nRecorrido Postorden:");
            tree.PostOrder(tree.Root); // 2 4 3 6 8 7 5

            Console.WriteLine("\n\nRecorrido por Niveles (BFS):");
            tree.LevelOrder(tree.Root); // 5 3 7 2 4 6 8

            // Eliminar un nodo (ejemplo: eliminar 3)
            tree.Delete(3);
            Console.WriteLine("\n\nInorden después de eliminar 3:");
            tree.InOrder(tree.Root); // 2 4 5 6 7 8
        }
    }
}
