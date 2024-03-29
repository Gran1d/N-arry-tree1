using System;
using System.Collections.Generic;
using System.Linq;
namespace avsda;

public class TreeNode<T> where T : TUW
{
    public T Data { get; set; }
    public int Id { get; set; }
    public List<TreeNode<T>> Children { get; set; }

    private static int NextId = 0;

    public TreeNode()
    {
        Children = new List<TreeNode<T>>();
        Id = NextId++;
    }

    public TreeNode(T data) : this()
    {
        Data = data;
    }
    

    public void AddChild(TreeNode<T> child)
    {
        Children.Add(child);
    }

    public void TraverseDepthFirst(Action<T, int> action, int level = 0)
    {
        action(Data, level);
        foreach (var child in Children)
        {
            child.TraverseDepthFirst(action, level + 1);
        }
    }
}



public class Tree<T> where T : TUW
{
    private TUW t;
    
    public TreeNode<T> Root { get; set; }
    
    public Tree()
    {
        Root = null;
    }

    public Tree(T root)
    {
        Root = new TreeNode<T>(root);
    }
    // Конструктор копирования
 
    // Конструктор копирования
    public Tree(Tree<T> other)
    {
        if (other == null || other.Root == null)
        {
            Root = null;
            return;
        }

        Root = CopyNode(other.Root);
    }

    // Метод для копирования узла
    private TreeNode<T> CopyNode(TreeNode<T> node)
    {
        if (node == null)
            return null;

        TreeNode<T> copiedNode = new TreeNode<T>(node.Data);

        foreach (var child in node.Children)
        {
            copiedNode.Children.Add(CopyNode(child));
        }

        return copiedNode;
    }
    
    
    

    public void Insert(T element, int parentId)
    {
        
        if (Root == null)
        {
            Root = new TreeNode<T>(element);
        }

        Insert(element, parentId, Root);
    }

    private void Insert(T element, int parentId, TreeNode<T> node)
    {
        
        if (node.Id == parentId && element.CanInsert(GetItem(parentId)))
        {
            node.AddChild(new TreeNode<T>(element));
            return;
        }

        foreach (var child in node.Children)
        {
            Insert(element, parentId, child);
        }
    }

    public void InsertUnique(T element, int parentId)
    {
        if (Root == null)
        {
            Root = new TreeNode<T>(element);
        }

        InsertUnique(element, parentId, Root);
    }

    private void InsertUnique(T element, int parentId, TreeNode<T> node)
    {
        if (node.Id == parentId && element.CanInsert(GetItem(parentId)))
        {
            if (!node.Children.Exists(n => EqualityComparer<T>.Default.Equals(n.Data, element)))
            {
                node.AddChild(new TreeNode<T>(element));
            }
            return;
        }

        foreach (var child in node.Children)
        {
            InsertUnique(element, parentId, child);
        }
    }

    public bool Delete(T element)
    {
        if (Root == null)
        {
            return false;
        }

        if (EqualityComparer<T>.Default.Equals(Root.Data, element))
        {
            Root = null;
            return true;
        }

        return Delete(element, Root);
    }

    private bool Delete(T element, TreeNode<T> node)
    {
        for (int i = 0; i < node.Children.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(node.Children[i].Data, element))
            {
                node.Children.RemoveAt(i);
                return true;
            }
            if (Delete(element, node.Children[i]))
            {
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        Root = null;
    }

    public List<T> ItemsByLevel(int level)
    {
        List<T> items = new List<T>();
        TraverseDepthFirst((data, nodeLevel) =>
        {
            if (nodeLevel == level)
            {
                items.Add(data);
            }
        });
        return items;
    }

    public int IdOf(T element)
    {
        Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
        if (Root != null)
        {
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                TreeNode<T> current = queue.Dequeue();

                // Проверяем, равен ли текущий элемент элементу, переданному в качестве параметра
                if (EqualityComparer<T>.Default.Equals(current.Data, element))
                {
                    return current.Id;
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }
        return -1;
    }


    
    public void PrintTree()
    {
        if (Root != null)
        {
            PrintTree(Root, 0);
        }
    }

    private void PrintTree(TreeNode<T> node, int level)
    {
        string indentation = new string(' ', level * 2);
        Console.WriteLine(indentation + node.Data.Name);
        foreach (var child in node.Children)
        {
            PrintTree(child, level + 1);
        }
    }

    public List<T> GetItems<T>()
    {
        List<T> items = new List<T>();
        if (Root != null)
        {
            TraverseDepthFirst((data, _) =>
            {
                if (data is T)
                {
                    items.Add((T)(object)data);
                }
            });
        }
        return items;
    }

    private void TraverseDepthFirst(Action<T, int> action, TreeNode<T> node = null, int level = 0)
    {
        if (node == null)
        {
            node = Root;
        }

        action(node.Data, level);
        foreach (var child in node.Children)
        {
            TraverseDepthFirst(action, child, level + 1);
        }
    }
    
    public T GetItem(int id)
    {
        return FindItemById(Root, id);
    }

    private T FindItemById(TreeNode<T> node, int id)
    {
        if (node == null)
            return null;

        if (node.Id == id)
            return node.Data;

        foreach (var child in node.Children)
        {
            var result = FindItemById(child, id);
            if (result != null)
                return result;
        }

        return null;
    }
    
}



class Program
{
    static void Main(string[] args)
    {
  
        // тесты для tuw
        TUW root = new T("Root");
        TUW t1 = new T("T1");
        TUW u1 = new U("U1");
        TUW w1 = new W("W1");
        TUW t2 = new T("T2");
        TUW w2 = new W("W2");
        TUW u2 = new U("U2");

        Tree<TUW> tree1 = new Tree<TUW>(root);
        tree1.Insert(t1, 0);
        tree1.Insert(u1,0);
        tree1.Insert(w1, 0);
        tree1.Insert(t2,1);
        tree1.Insert(w2,1);
        tree1.Insert(u2,2);
        
        // Печатаем дерево
        Console.WriteLine("Tree:");
        tree1.PrintTree();
        
        // Выводим Id элемента
        Console.WriteLine("Id of w2: " + tree1.IdOf(w2));
        Console.WriteLine("Items at level 1:");
        var items1 = tree1.ItemsByLevel(1);
        foreach (var item in items1)
        {
            Console.WriteLine(item);
        }
        
        
        // Получаем элементы определенного типа
        Console.WriteLine("Items of type T:");
        var Items = tree1.GetItems<T>();
        foreach (var item in Items)
        {
            Console.WriteLine(item);
        }

        // Удаляем элемент
        Console.WriteLine("Delete w2");
        tree1.Delete(w2);
        Console.WriteLine("Tree after deletion:");
        tree1.PrintTree();

        Console.WriteLine(tree1.GetItem(2).Name);
        Tree<TUW> tree2 = new Tree<TUW>(tree1);
        // Очищаем дерево
        Console.WriteLine("Clear tree");
        tree1.Clear();
        Console.WriteLine("Tree after clearing:");
        tree2.PrintTree();
        
        
    }
    
}
