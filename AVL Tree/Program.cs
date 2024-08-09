namespace AVL_Tree
{
    public class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>();

            tree.Add(10);
            tree.Add(15);       
            tree.Add(5);
            tree.Add(20);
            tree.Add(13);
            tree.Add(25);
            tree.Add(17);
            tree.Add(30);
            tree.Add(27);
            tree.Add(2);
             
            tree.Delete(15);


            ;
        }
    }
}
