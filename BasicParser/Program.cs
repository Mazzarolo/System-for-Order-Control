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

        static void FilterMenu(Reader reader, DataBase dataBase)
        {
            bool back = false;
            List<string> startDates = new List<string>();
            List<string> endDates = new List<string>();
            List<string> sectors = new List<string>();
            
            while(!back)
            {
                Console.Clear();
                Console.WriteLine("\tDigite a opção desejada:");
                Console.WriteLine(" - Adicionar Filtro de Data de Emissão (0)");
                Console.WriteLine(" - Adicionar Filtro de Data de Saída (1)");
                Console.WriteLine(" - Adicionar Filtro de Setor (2)");
                Console.WriteLine(" - Remover Todos os Filtros Aplicados (3)");
                Console.WriteLine(" - Mostrar Ordens (4)");
                Console.WriteLine(" - Voltar (5)");

                char key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case '0':
                        Console.Clear();
                        Console.WriteLine("-> Digite a menor data de emissão que voce deseja filtrar (dd/mm/aaaa)");
                        startDates.Clear();
                        startDates.Add(Console.ReadLine());
                        Console.WriteLine("\n-> Digite a maior data de emissão que voce deseja filtrar (dd/mm/aaaa)");
                        startDates.Add(Console.ReadLine());
                        Console.WriteLine("\n-> Filtro Adicionado!");
                        Console.WriteLine("\tPrecione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '1':

                        break;
                    case '3':
                        startDates.Clear();
                        endDates.Clear();
                        sectors.Clear();
                        Console.Clear();
                        Console.WriteLine("-> Filtros Removidos!");
                        Console.WriteLine("\tPrecione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '4':
                        Console.Clear();
                        if (dataBase != null)
                        {
                            if (!dataBase.PrintInfo(startDates, endDates, sectors))
                            {
                                Console.Clear();
                                Console.WriteLine("-> Não há resultados para a busca selecionada!");
                            }
                        }
                        else
                            Console.WriteLine("-> Não há resultados para a busca selecionada!");
                        Console.WriteLine("\tPrecione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '5':
                        back = true;
                        break;
                }
            }
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
                        FilterMenu(reader, dataBase);
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