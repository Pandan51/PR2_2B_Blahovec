using System;
using System.Collections.Generic;
using System.Text.Json;

class Salesman
{
    private int ID { get; set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public int Sales { get; private set; }
    public List<Salesman> Subordinates { get; private set; }

    //Musím najít náhradu
    //public Salesman? Manager { get; private set; }

    //private static int lastID = 0;

    static int NextId = 1;

    public Salesman(string surname, string name, int sales, int id=0)
    {


        //ID = lastID;
        //lastID++;

        if (id != 0)
        {
            ID = id;
            if (id > NextId)
                NextId = id + 1;
        }
        else
        {
            ID = NextId++;
        }

        Name = name;
        Surname = surname;
        Sales = sales;
        Subordinates = new List<Salesman>();
        //Manager = Manager;
    }

    public void AddSubordinate(Salesman subordinate)
    {
        Subordinates.Add(subordinate);
    }

    //public void AssignManagers(Salesman parentNode)
    //{
    //    foreach (var subordinate in parentNode.Subordinates)
    //    {
    //        subordinate.Manager = parentNode; // Assign the manager before going deeper
    //        AssignManagers(subordinate); // Recursively process the next level
    //    }
    //}

    public static Salesman DeserializeTree(string jsonString)
    {
        List<SalesmanData> deserializedData = JsonSerializer.Deserialize<List<SalesmanData>>(jsonString);

        Dictionary<int, Salesman> treeData = new Dictionary<int, Salesman>();
        Salesman root = null;

        foreach (var item in deserializedData)
        {
            Salesman salesman = item.ToSalesman();
            treeData[salesman.ID] = salesman;
            // TODO IF marked then add to Progam.Marked

            if (item.ParentId != 0)
                treeData[item.ParentId].AddSubordinate(salesman);
            else
                root = salesman;
        }

        return root;
    }

    public static void SaveTree(Salesman root)
    {
        // CONVERT TO List<SalesmanData> AND THE SAVE
    }

    private class SalesmanData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Sales { get; set; }
        public int ParentId { get; set; }
        // TODO bool isBookmark

        public SalesmanData() { }

        public SalesmanData(Salesman salesman, int parentId = 0)
        {
            ID = salesman.ID;
            Name = salesman.Name;
            Surname = salesman.Surname;
            Sales = salesman.Sales;
            ParentId = parentId;
            // bool
        }

        public Salesman ToSalesman() => new Salesman(Surname, Name, Sales, ID);
    }

}