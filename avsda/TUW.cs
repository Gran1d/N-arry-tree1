namespace avsda;

public abstract class TUW
{
    public string Name { get; set; }
    public int Id { get; protected set; }
    public abstract bool CanInsert(object item);


}

class T : TUW 
{

    private static int nextId = 1;

    public T(string name)
    {
        Name = name;
        Id = nextId++;
    }

    public override bool CanInsert(object item)
    {
        if (item.GetType() == typeof(U) || item.GetType() == typeof(W))
        {
            return true;
        }

        return false;
    }
}

class U : TUW
{
    
    private static int nextId = 1;

    public U(string name)
    {
        Name = name;
        Id = nextId++;
    }
    
    public override bool CanInsert(object item)
    {
        if (item.GetType() == typeof(T))
        {
            return true;
        }

        return false;
    }
    
}

class W : TUW
{
    
    private static int nextId = 1;

    public W(string name)
    {
        Name = name;
        Id = nextId++;
    }
    public override bool CanInsert(object item)
    {
        if (item.GetType() == typeof(T) || item.GetType() == typeof(U))
        {
            return true;
        }

        return false;
    }

}