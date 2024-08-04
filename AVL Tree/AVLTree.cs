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
        public class Node<T> where T : IComparable
        {
            public T Value { get; set; }

            public Node<T> LeftChild { get; set; }
            public Node<T> RightChild { get; set; }

            public int Height { get; set; }
            public int Balance = 0;
            public Node(T value, Node<T> leftChild, Node<T> rightChild)

            {
                Value = value;
                LeftChild = leftChild;
                RightChild = rightChild;
                Height = 1;
            }

            public Node(T value, Node<T> previous)
               : this(value, null, null) { }
            public Node(T value)
              : this(value, null, null) { }
            public Node()
              : this(default, null, null) { }

            public void UpdateBalance()
            {
                if(LeftChild == null && RightChild == null)
                {
                    Balance = 0;
                }
                else if(LeftChild == null)
                {
                    Balance = RightChild.Height;
                }
                else if(RightChild == null)
                {
                    Balance = -LeftChild.Height;
                }
                else
                {
                    Balance = RightChild.Height - LeftChild.Height;
                }
                
            }
        }


        public Node<T> Root { get; private set; }

        public AVLTree(Node<T> val)
        {
            Root = val;
        }

        public AVLTree()
            : this(null)
        {
        }

        public void Add(T val)
        {
            Root = AddHelper(Root, val);
        }

        public void Delete(T val)
        {
            Root = DeleteHelper(Root, val);
        }

        private Node<T> DeleteHelper(Node<T> current, T delete)
        {
            if (current.Value.Equals(delete))
            {

                if (current.LeftChild == null && current.RightChild == null)
                {
                   
                }
                else if (current.LeftChild == null)
                {
                    current = current.RightChild;
                }
                else if (current.RightChild == null)
                {
                    current = current.LeftChild;
                }
                else
                {
                    current.Value = FindMax(current.LeftChild).Value;
                    Delete(current.LeftChild.Value);
                }
                
            }
            else
            {
                if (delete.CompareTo(current.Value) < 0)
                {
                    current = DeleteHelper(current.LeftChild, delete);
                }
                else if (delete.CompareTo(current.Value) > 0)
                {
                    current = DeleteHelper(current.RightChild, delete);
                }
            }

            return current;


        }

     

        private Node<T> AddHelper(Node<T> node, T val)
        {

            if (node == null)
            {
                Node<T> temp = new Node<T>(val);
                node = temp;
                return node;
            }

            if (val.CompareTo(node.Value) < 0)
            {

                node.LeftChild = AddHelper(node.LeftChild, val);

            }
            else if (val.CompareTo(node.Value) > 0)
            {
                node.RightChild = AddHelper(node.RightChild, val);
            }

            UpdateHeight(node);

            node = SelfBalance(node);


            return node;


        }

        public Node<T> FindMax(Node<T> start)
        {
            Node<T> current = start;
            while (current.RightChild != null)
            {
                current = current.RightChild;
            }
            return current;
        }
        public void UpdateHeight(Node<T> node)
        {
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
            if(node.RightChild == null)
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

        public Node<T> SelfBalance(Node<T> node)
        {
            Node<T> child = node;
            if (node.Balance >= -1 || node.Balance <= 1)
            {
                Node<T> swap = FindSwap(Root);
                if (node.Balance > 1)
                {
                    child = swap.RightChild;
                    if (child.Balance < 0)
                    {
                        swap.RightChild = child.LeftChild;
                        RotateRight(child);
                        UpdateHeight(swap.RightChild);
                        UpdateHeight(swap);

                    }

                    child = swap.RightChild;
                    RotateLeft(swap);
                }
                else if (node.Balance < -1)
                {
                    child = swap.LeftChild;
                    if (child.Balance > 0)
                    {
                        swap.LeftChild = child.RightChild;
                        RotateLeft(child);
                        UpdateHeight(swap.LeftChild);
                        UpdateHeight(swap);
                    }

                    child = swap.LeftChild;
                    RotateRight(swap);
                }
                UpdateHeight(swap);
                UpdateHeight(child);
            }
            
            return child;
        }

        public Node<T> FindSwap(Node<T> current)
        {
            Node<T> temp = new Node<T>();
            if(current.Balance != -2 && current.Balance != 2)
            {
                if (current.RightChild != null)
                {
                    temp = FindSwap(current.RightChild);
                }
                if (current.LeftChild != null)
                {
                    temp = FindSwap(current.LeftChild);
                }
               
            }
            else
            {
                temp = current;
            }
            return temp;
        }

        public void updateChildren(Node<T> node)
        {
            if(node.LeftChild != null)
            {
                node.LeftChild.UpdateBalance();
                UpdateHeight(node.LeftChild);
            }
             if(node.RightChild != null)
            {
                node.RightChild.UpdateBalance();
                UpdateHeight(node.RightChild);
            }

        }

        //occurs when leaning right
        public void RotateLeft(Node<T> node)
        {
            Node<T> swap = node;
    
            node = node.RightChild;
            Node<T> temp = node.LeftChild;
            node.LeftChild = swap;
            node.LeftChild.RightChild = temp;
            

    
        }

        //occurs when leaning left
        public void RotateRight(Node<T> node)
        {
            Node<T> swap = node;
            node = node.LeftChild;
            Node<T> temp = node.RightChild;
            node.RightChild = swap;
            node.RightChild.LeftChild = temp;
        }

    }

}


       


 
