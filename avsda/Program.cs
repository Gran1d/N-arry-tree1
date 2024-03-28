using System;
using System.Collections.Generic;
using System.Linq;
using avsda;

public class Node<T>
{
    
}
public class TreeNode<T>
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

    public TreeNode(TreeNode<T> node) : this(node.Data)
    {
        foreach (var child in node.Children)
        {
            Children.Add(new TreeNode<T>(child));
        }
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
    // Метод для вставки элемента как child элементу с соответствующим Id
    public void Insert(T element, int parentId)
    {
        foreach (var child in Children)
        {
            if (child.Id == parentId)
            {
                child.AddChild(new TreeNode<T>(element));
                return;
            }
            child.Insert(element, parentId); // Рекурсивно ищем узел для вставки в дочерних узлах
        }
    }
    public void InsertUnique(T element, int parentId)
    {
        foreach (var child in Children)
        {
            if (child.Id == parentId)
            {
                bool alreadyExists = false;
                foreach (var node in child.Children)
                {
                    if (node.Data.Equals(element))
                    {
                        alreadyExists = true;
                        break;
                    }
                }
                if (!alreadyExists)
                {
                    child.AddChild(new TreeNode<T>(element));
                }
                return;
            }
            child.InsertUnique(element, parentId);
        }
    }

    public bool Delete(T element)
    {
        foreach (var child in Children)
        {
            if (child.Data.Equals(element))
            {
                Children.Remove(child);
                return true;
            }
            if (child.Delete(element))
            {
                return true;
            }
        }
        return false;
    }
    
    public void Clear()
    {
        Children.Clear();
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
        queue.Enqueue(this);

        while (queue.Count > 0)
        {
            TreeNode<T> current = queue.Dequeue();

            if (EqualityComparer<T>.Default.Equals(current.Data, element))
            {
                return current.Id;
            }

            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }
        }

        return -1;
    }

    
    public void PrintTree()
    {
        TraverseDepthFirst((data, level) =>
        {
            string indentation = new string(' ', level * 2);
            Console.WriteLine(indentation + data);
        });
    }
    
    public List<T> GetItems<T>()
    {
        List<T> items = new List<T>();
        TraverseDepthFirst((data, _) =>
        {
            if (data is T)
            {
                items.Add((T)(object)data);
            }
        });
        return items;
    }
    
    

}

class Program
{
    static void Main(string[] args)
    {
        // TreeNode<string> root = new TreeNode<string>("Root");
        // TreeNode<string> child1 = new TreeNode<string>("Child1");
        // TreeNode<string> child2 = new TreeNode<string>("Child2");
        // TreeNode<string> child3 = new TreeNode<string>("Child3");
        // TreeNode<string> grandChild1 = new TreeNode<string>("GrandChild1");
        // TreeNode<string> grandChild2 = new TreeNode<string>("GrandChild2");
        //
        // root.AddChild(child1);
        // root.AddChild(child2);
        // root.AddChild(child3);
        // child1.AddChild(grandChild1);
        // child1.AddChild(grandChild2);
        //
        // // Получаем элементы на уровне 1 (индексация начинается с 0)
        // List<string> itemsAtLevel1 = root.ItemsByLevel(1);
        // Console.WriteLine("Items at level 1:");
        // foreach (var item in itemsAtLevel1)
        // {
        //     Console.WriteLine(item);
        // }
        // int idOfChild1 = root.IdOf("Child2");
        // Console.WriteLine("Id of 'Child1': " + idOfChild1);
        //
        // int idOfNonExisting = root.IdOf("NonExisting");
        // Console.WriteLine("Id of 'NonExisting': " + idOfNonExisting);
        // root.Insert("asdas", 2);
        // root.PrintTree();
        
        
        // TreeNode<object> root = new TreeNode<object>(10);
        // TreeNode<object> child1 = new TreeNode<object>("Child1");
        // TreeNode<object> child2 = new TreeNode<object>("Child2");
        // TreeNode<object> child3 = new TreeNode<object>(20);
        // TreeNode<object> grandChild1 = new TreeNode<object>(30);
        // TreeNode<object> grandChild2 = new TreeNode<object>("GrandChild2");
        //
        // root.AddChild(child1);
        // root.AddChild(child2);
        // root.AddChild(child3);
        // child1.AddChild(grandChild1);
        // child1.AddChild(grandChild2);
        //
        // // Получаем все элементы типа string
        // List<string> stringItems = root.GetItems<string>();
        // Console.WriteLine("String items:");
        // foreach (var item in stringItems)
        // {
        //     Console.WriteLine(item);
        // }
        //
        // // Получаем все элементы типа int
        // List<string> intItems = root.GetItems<string>();
        // Console.WriteLine("Int items:");
        // foreach (var item in intItems)
        // {
        //     Console.WriteLine(item);
        // }
        // root.PrintTree();



        TUW t = new T("T");
        TUW u1 = new U("U1");
        TUW t1 = new T("T1");
        TUW w1 = new W("W1");
        TUW u2 = new U("U2");
        TUW w2 = new W("W2");
        
        Console.WriteLine(t.CanInsert(u1));
        
        
        
        
        TreeNode<TUW> root1 = new TreeNode<TUW>(t);
        TreeNode<TUW> Child1 = new TreeNode<TUW>(u1);
        TreeNode<TUW> Child2 = new TreeNode<TUW>(t1);
        TreeNode<TUW> Child3 = new TreeNode<TUW>(w1);
        TreeNode<TUW> Grandchild1_1 = new TreeNode<TUW>(u2);
        TreeNode<TUW> Grandchild1_2 = new TreeNode<TUW>(w2);
        
        root1.AddChild(Child1);
        root1.AddChild(Child2);
        root1.AddChild(Child3);
        Child1.AddChild(Grandchild1_1);
        Child1.AddChild(Grandchild1_2);
        root1.Delete(u2);
        root1.Insert(u2, 3);
        root1.PrintTree();
        
        List<TUW> itemsAtLevel1 = root1.ItemsByLevel(1);
        Console.WriteLine("Items at level 1:");
        foreach (var item in itemsAtLevel1)
        {
            Console.WriteLine(item);
        }
        // root1.Clear();
        Console.WriteLine(root1.IdOf(w2));
        
        List<T> stringItems = root1.GetItems<T>();
        Console.WriteLine("String items:");
        foreach (var item in stringItems)
        {
            Console.WriteLine(item);
        }
    }
    
}


// public void Insert(T element, int parentId)
// {
//     foreach (var child in Children)
//     {
//         if (child.Id == parentId)
//         {
//             child.AddChild(new TreeNode<T>(element));
//             return;
//         }
//         child.Insert(element, parentId); // Рекурсивно ищем узел для вставки в дочерних узлах
//     }
// }