using System.Collections.Generic;
using System.Linq;

public interface INode<TData>
{
    TData Data { get; }
    INode<TData> Parent { get; }
    INode<TData> Root { get; }
    IEnumerable<INode<TData>> DirectChildren { get; }
    IEnumerable<INode<TData>> Nodes { get; }
    INode<TData> AddChild(TData data);
}

public class Node<TData> : INode<TData>
{
    private readonly List<Node<TData>> children = new List<Node<TData>>();

    private Node(TData data, Node<TData> parent)
    {
        Data = data;
        Parent = parent;
    }
    
    public static Node<TData> CreateTree(TData data)
    {
        return new Node<TData>(data, null);
    }

    public INode<TData> AddChild(TData data)
    {
        var child = new Node<TData>(data, this);
        children.Add(child);
        return child;
    }

    public TData Data { get; }

    public INode<TData> Parent { get; }

    public INode<TData> Root => (Parent == null) ? this : Parent.Root;    

    public IEnumerable<INode<TData>> DirectChildren => children;

    public IEnumerable<INode<TData>> Nodes => children.Concat(children.SelectMany(x => x.Nodes));
}