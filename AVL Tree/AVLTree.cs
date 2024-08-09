using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;



namespace AVL_Tree
{
    //AVL Tree should balance a tree around so the each subtree is even
    public class AVLTree<T> where T : IComparable
    {
        public class Node
        {
            public T Value { get; set; }

            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }

            public int Height { get; set; }
            public int Balance = 0;
            public Node(T value, Node leftChild, Node rightChild)

            {
                Value = value;
                LeftChild = leftChild;
                RightChild = rightChild;
                Height = 1;
            }

            public Node(T value, Node previous)
               : this(value, null, null) { }
            public Node(T value)
              : this(value, null, null) { }
            public Node()
              : this(default, null, null) { }

            public void UpdateBalance()
            {
                int leftHeight = 0;
                if (LeftChild != null)
                {
                    leftHeight = LeftChild.Height;
                }

                int rightHeight = 0;
            
                if (RightChild != null)
                {
                    rightHeight = RightChild.Height;
                }


                Balance = rightHeight - leftHeight;
               

            }
        }


        public Node Root { get; private set; }

        public AVLTree(Node val)
        {
            Root = val;
        }

        public AVLTree()
            : this(null)
        {
        }

        public void Add(T val)
        {
            Root = Add(Root, val);
        }

        public void Delete(T val)
        {
            Root = Delete(Root, val);
        }

        private Node Delete(Node current, T delete)
        {
            if (delete == null)
            {
                return null;
            }

            if (delete.CompareTo(current.Value) < 0)
            {
                current.LeftChild = Delete(current.LeftChild, delete);
            }
            else if (delete.CompareTo(current.Value) > 0)
            {
                current.RightChild = Delete(current.RightChild, delete);
            }
            else
            {
                if(current.LeftChild == null)
                {
                    return current.RightChild;
                }
                else if(current.RightChild == null)
                {
                    return current.LeftChild;
                }
                else
                {
                    current.Value = FindMax(current.LeftChild).Value;
                    Delete(FindMax(current.LeftChild),FindMax(current.LeftChild).Value);
                }
            }

            UpdateHeight(current);

            return SelfBalance(current);

        }



        private Node Add(Node node, T val)
        {

            if (node == null)
            {
                Node temp = new Node(val);
                node = temp;
                return node;
            }

            if (val.CompareTo(node.Value) < 0)
            {

                node.LeftChild = Add(node.LeftChild, val);

            }
            else if (val.CompareTo(node.Value) > 0)
            {
                node.RightChild = Add(node.RightChild, val);
            }

            UpdateHeight(node);


            return SelfBalance(node);


        }

        public Node FindMax(Node start)
        {
            Node current = start;
            while (current.RightChild != null)
            {
                current = current.RightChild;
            }
            return current;
        }
        public void UpdateHeight(Node node)
        {
            if(node == null)
            {
                return;
            }
            updateChildren(node);
            int left;
            int right;
            if (node.LeftChild == null)
            {
                left = 0;
            }
            else
            {
                left = node.LeftChild.Height;
            }
            if (node.RightChild == null)
            {
                right = 0;
            }
            else
            {
                right = node.RightChild.Height;
            }


            node.Height = Math.Max(left, right) + 1;

            node.UpdateBalance();


        }



        public Node SelfBalance(Node node)
        {
            if (node.Balance > 1)
            {
                if (node.RightChild.Balance < 0)
                {

                    node.RightChild = RotateRight(node.RightChild);

                }


                node = RotateLeft(node);
            }
            else if (node.Balance < -1)
            {
                if (node.LeftChild.Balance > 0)
                {

                    node.LeftChild = RotateLeft(node.LeftChild);

                }

                node = RotateRight(node);
            }

            UpdateHeight(node);


            return node;
        }

        public void updateChildren(Node node)
        {
           /* if (node == null)
            {
                return;
            }*/
                if (node.LeftChild != null)
                {
                    node.LeftChild.UpdateBalance();
                    UpdateHeight(node.LeftChild);
                }
                if (node.RightChild != null)
                {
                    node.RightChild.UpdateBalance();
                    UpdateHeight(node.RightChild);
                }

        }

        //occurs when leaning right
        public Node RotateLeft(Node node)
        {
            Node swap = node;

            node = node.RightChild;
            Node temp = node.LeftChild;
            node.LeftChild = swap;
            node.LeftChild.RightChild = temp;

            UpdateHeight(node.LeftChild);
            UpdateHeight(node.LeftChild.RightChild);


            return node;
        }

        //occurs when leaning left
        public Node RotateRight(Node node)
        {
            Node swap = node;

            node = node.LeftChild;
            Node temp = node.RightChild;
            node.RightChild = swap;
            node.RightChild.LeftChild = temp;

            UpdateHeight(node.RightChild);
            UpdateHeight(node.RightChild.LeftChild);

            return node;
        }

    }

}






