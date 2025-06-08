using System;
using System.Collections.Generic;

// Запись в журнал
public class JournalEntry
{
    public string CollectionName { get; set; }
    public string ChangeType { get; set; }
    public string ChangedItemData { get; set; }

    public JournalEntry(string name, string changeType, string data)
    {
        CollectionName = name;
        ChangeType = changeType;
        ChangedItemData = data;
    }

    public override string ToString()
    {
        return $"Коллекция: {CollectionName}, Тип изменения: {ChangeType}, Данные: {ChangedItemData}";
    }
}

// Журнал
public class Journal
{
    private readonly List<JournalEntry> entries = new List<JournalEntry>();
    private readonly string journalName;

    public Journal(string name)
    {
        journalName = name;
    }

    public void OnCollectionChanged(object source, CollectionHandlerEventArgs args)
    {
        string collectionName = source.GetType().Name;
        entries.Add(new JournalEntry(collectionName, args.ChangeType, args.ChangedItem?.ToString()));
    }

    public void Print()
    {
        Console.WriteLine($"Журнал: {journalName}");
        foreach (var entry in entries)
        {
            Console.WriteLine(entry);
        }
    }
}