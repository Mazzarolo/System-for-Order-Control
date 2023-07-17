using Parser;
using Data;
using System;
using System.Runtime.Serialization;
using System.Xml;

namespace Application
{
    class Application
    {
        static void SaveData<T>(T serializableObject, string filepath)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var settings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t",
            };
            var writer = XmlWriter.Create(filepath, settings);
            serializer.WriteObject(writer, serializableObject);
            writer.Close();
        }

        static T LoadData<T>(string filepath)
        {
            var fileStream = new FileStream(filepath, FileMode.Open);
            var reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());
            var serializer = new DataContractSerializer(typeof(T));
            T serializableObject = (T)serializer.ReadObject(reader, true);
            reader.Close();
            fileStream.Close();
            return serializableObject;
        }

        static DataBase LoadNewOrders(Reader reader, DataBase dataBase)
        {
            reader.Clear();
            reader.LoadFiles();
            reader.ParseFiles();
            try
            {
                dataBase = LoadData<DataBase>("DataBase.xml");
            }
            catch
            {
                dataBase = new DataBase();
                Console.WriteLine("DataBase is empty");
            }
            dataBase.AddNotes(reader.getFiles());
            SaveData(dataBase, "DataBase.xml");
            LoadData<DataBase>("DataBase.xml");

            return dataBase;
        }

        static void Menu(Reader reader, DataBase dataBase)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\tDigite a opção desejada:");
                Console.WriteLine(" - Carregar Novas Ordens (0)");
                Console.WriteLine(" - Mostrar Ordens (1)");
                Console.WriteLine(" - Pesquisar Ordens (2)");
                Console.WriteLine(" - Sair (3)");
                char key = Console.ReadKey().KeyChar;
                switch(key)
                {
                    case '0':
                        dataBase = LoadNewOrders(reader, dataBase);
                        Console.Clear();
                        Console.WriteLine("-> Novas Ordens Adicionadas!\n\tPrecione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '1':
                        Console.Clear();
                        if(dataBase != null)
                            dataBase.PrintInfo();
                        else
                            Console.WriteLine("-> Banco de dados Vazio!");
                        Console.WriteLine("\tPrecione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '3':
                        exit = true;
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Reader reader = new Reader();
            DataBase dataBase = new DataBase();
            try
            {
                dataBase = LoadData<DataBase>("DataBase.xml");
            }
            catch
            {
                dataBase = null;
                Console.WriteLine("DataBase is empty");
            }

            Menu(reader, dataBase);
        }
    }
}