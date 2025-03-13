using main;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Xml.Linq;

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
        
    }

    public void AddSubordinate(Salesman subordinate)
    {
        Subordinates.Add(subordinate);
    }

    

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

    public static void SaveList(List<Salesman> markedSalesmen, string filename)
    {

        if (File.Exists(filename)) // Check if the file exists
        {
            Console.WriteLine($"Varování: Tento soubor '{filename}' už existuje.");
            Console.WriteLine("Chcete soubour pøepsat? (ano)");
            string response = Console.ReadLine()?.Trim().ToLower();

            if (response != "ano")
            {
                Console.WriteLine("Ukládání pøerušeno");
                return; // Exit without saving
            }
        }

        //List id
        List<int> markedSalesmenIDs = new List<int>();

        //Naètení id oznaèených
        foreach(Salesman x in markedSalesmen)
        {
            markedSalesmenIDs.Add(x.ID);
        }

        
        using (StreamWriter writer = new StreamWriter(filename))
            //Zapsání id do souboru
        {
            foreach (int id in markedSalesmenIDs)
            {
                writer.WriteLine(id);
            }
        }
    }

    public static List<Salesman> LoadList(string filename, Salesman root)
    {
        filename = NameFile();
        List<int> selectedEmployeeIDs = new List<int>();
        List<Salesman> markedEmployees = new List<Salesman>();

        if (File.Exists(filename)) // Check if the file exists
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                if (int.TryParse(line, out int id)) // Convert to int safely
                {
                    selectedEmployeeIDs.Add(id);
                }
            }
            markedEmployees = FindIDTraverseTree(root, selectedEmployeeIDs, markedEmployees);
        }

        return markedEmployees;
    }

    private static List<Salesman> FindIDTraverseTree(Salesman root, List<int> markedIDs, List<Salesman> markedList)
    {
        if (root == null)
            return markedList;
            if (markedIDs.Contains(root.ID))
                markedList.Add(root);

            foreach (var subordinate in root.Subordinates)
            {
                FindIDTraverseTree(subordinate, markedIDs, markedList);
            }
        
        return markedList;
    }
    

    public static string CreateList(string filename = "muj_vyber.txt", string defaultContent = "")
    {
        filename = NameFile();
        string response = "";
        //Console.WriteLine("Pojmenujte seznam, jinak se bude jmenovat muj_vyber");
        //string response = Console.ReadLine()?.Trim();
        //if(response != null && response != "")
        //{
        //    filename = response+".txt";
        //    Console.WriteLine("Vytvoøeno");
        //}
        
        bool overwrite = false;
        do
        {
            
            if (!File.Exists(filename))  // Check if the file already exists
            {
                File.WriteAllText(filename,defaultContent);

                Console.WriteLine($"Soubor '{filename}' vytvoøen.");
            }
            else if(overwrite)
            {
                File.WriteAllText(filename, defaultContent);
                Console.WriteLine($"Soubor '{filename}' vytvoøen.");
            }
            else
            {
                overwrite = false;
                Console.WriteLine($"Soubor '{filename}' už existuje. Chcete ho pøepsat? (ano)");

                response = Console.ReadLine()?.Trim().ToLower();

                if (response == "ano")
                {
                    
                    overwrite = true;
                    continue;
                    
                }
                else
                {
                    Console.WriteLine("Ukládání pøerušeno");
                }

            }
        } while (false);

        return filename;
    }

    private static string NameFile()
    {
        string filename = "muj_vyber.txt";
        Console.WriteLine("Pojmenujte seznam, jinak se bude jmenovat muj_vyber");
        string response = Console.ReadLine()?.Trim();
        if (response != null && response != "")
        {
            filename = response + ".txt";
            Console.WriteLine("Vytvoøeno");
        }
        FileManagement.currentListName = filename;
        return filename;
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