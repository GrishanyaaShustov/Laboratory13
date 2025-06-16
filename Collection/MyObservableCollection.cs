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

// Наблюдаемая коллекция с событиями и поддержкой ключей
public class MyObservableCollection<T> : MyCollection<T> where T : IInit, new()
{
    public event CollectionHandler CollectionCountChanged;
    public event CollectionHandler CollectionReferenceChanged;

    public MyObservableCollection() : base() { }

    public MyObservableCollection(int count, Func<T, object> keySelector, Func<object, object, bool> keyComparer = null)
        : base(0, keySelector, keyComparer)
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
        CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Добавлен элемент", obj));
    }

    public new bool Remove(T obj)
    {
        bool removed = base.Remove(obj);
        if (removed)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Удалён элемент", obj));
        }
        return removed;
    }

    public new void Clear()
    {
        foreach (var item in this)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Удалён элемент", item));
        }
        base.Clear();
    }

    // Индексатор: доступ по ключу (объекту)
    public T this[T index]
    {
        get
        {
            foreach (var item in this)
            {
                if (KeyEquals(item, index))
                    return item;
            }
            throw new KeyNotFoundException("Элемент не найден в коллекции.");
        }

        set
        {
            
            // Удаляем старый элемент (если есть)
            foreach (var item in this)
            {
                if (KeyEquals(item, index))
                {
                    CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs("Изменён элемент по ссылке", item));
                    base.Remove(item);
                    break;
                }
            }
            // Добавляем новый
            base.Add(value);
        }
    }

    private bool KeyEquals(T a, T b)
    {
        if (KeySelector != null)
        {
            var keyA = KeySelector(a);
            var keyB = KeySelector(b);
            return KeyComparer != null ? KeyComparer(keyA, keyB) : Equals(keyA, keyB);
        }
        return Equals(a, b);
    }

    public void Print()
    {
        foreach (var item in this)
        {
            Console.WriteLine(item);
        }
    }
}
