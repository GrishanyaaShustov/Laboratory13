using System;
using System.Collections.Generic;

// Делегат события
public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

// Аргументы события
public class CollectionHandlerEventArgs : EventArgs
{
    public string ChangeType { get; set; }
    public object ChangedItem { get; set; }

    public CollectionHandlerEventArgs(string changeType, object changedItem)
    {
        ChangeType = changeType;
        ChangedItem = changedItem;
    }
}

// Класс с событиями
public class MyObservableCollection<T> : MyCollection<T> where T : IInit, new()
{
    private List<T> items = new List<T>();

    public event CollectionHandler CollectionCountChanged;
    public event CollectionHandler CollectionReferenceChanged;

    public MyObservableCollection() : base() { }

    public MyObservableCollection(int count) : base()
    {
        for (int i = 0; i < count; i++)
        {
            T item = new T();
            item.RandomInit();
            Add(item);
        }
    }

    public new void Add(T obj)
    {
        base.Add(obj);
        items.Add(obj);
        CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Добавлен элемент", obj));
    }

    public new bool Remove(T obj)
    {
        bool removed = base.Remove(obj);
        if (removed)
        {
            items.Remove(obj);
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Удалён элемент", obj));
        }
        return removed;
    }

    public new void Clear()
    {
        foreach (var item in items)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Удалён элемент", item));
        }
        base.Clear();
        items.Clear();
    }

    public T this[int index]
    {
        get => items[index];
        set
        {
            items[index] = value;
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs("Изменён элемент по ссылке", value));
        }
    }

    public int Length => items.Count;

    public new IEnumerator<T> GetEnumerator() => items.GetEnumerator();
    
    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"[{i}] {items[i]}");
        }
    }
}