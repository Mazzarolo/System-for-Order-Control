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
                        Console.WriteLine("-> Digite a mais antiga data de emissão que voce deseja filtrar (dd/mm/aaaa)");
                        startDates.Clear();
                        startDates.Add(Console.ReadLine());
                        Console.WriteLine("\n-> Digite a mais recente data de emissão que voce deseja filtrar (dd/mm/aaaa)");
                        startDates.Add(Console.ReadLine());
                        Console.WriteLine("\n-> Filtro Adicionado!");
                        Console.WriteLine("\tPressione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '1':
                        Console.Clear();
                        Console.WriteLine("-> Digite a mais antiga data de saída que voce deseja filtrar (dd/mm/aaaa)");
                        endDates.Clear();
                        endDates.Add(Console.ReadLine());
                        Console.WriteLine("\n-> Digite a mais recente data de saída que voce deseja filtrar (dd/mm/aaaa)");
                        endDates.Add(Console.ReadLine());
                        Console.WriteLine("\n-> Filtro Adicionado!");
                        Console.WriteLine("\tPressione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '2':
                        Console.Clear();
                        if(dataBase != null)
                        {
                            Console.WriteLine("-> Setores: ");
                            int i = 0;
                            List<string> uniqueSectors = dataBase.GetSectors();
                            foreach (string sector in uniqueSectors)
                                Console.WriteLine("\t {0}) - {1}.", i++, sector);
                            Console.WriteLine("-> Digite o indice do setor que quer filtrar: ");
                            i = Int32.Parse(Console.ReadLine());
                            sectors.Add(uniqueSectors[i]);
                            Console.WriteLine("\n-> Filtro Adicionado!\n\tPressione enter para continuar...");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("-> Banco de dados vazio!");
                            Console.WriteLine("\tPressione enter para continuar...");
                            Console.ReadLine();
                        }
                        break;
                    case '3':
                        startDates.Clear();
                        endDates.Clear();
                        sectors.Clear();
                        Console.Clear();
                        Console.WriteLine("-> Filtros Removidos!");
                        Console.WriteLine("\tPressione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '4':
                        Console.Clear();
                        if (dataBase != null)
                        {
                            if (!dataBase.PrintOrders(startDates, endDates, sectors))
                            {
                                Console.Clear();
                                Console.WriteLine("-> Não há resultados para a busca selecionada!");
                            }
                        }
                        else
                            Console.WriteLine("-> Não há resultados para a busca selecionada!");
                        Console.WriteLine("\tPressione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '5':
                        back = true;
                        break;
                }
            }
        }

        static void ItensMenu(Reader reader, DataBase dataBase)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\tDigite a opção desejada:");
                Console.WriteLine(" - Mudar setor de um item (0)");
                Console.WriteLine(" - Baixar item (1)");
                Console.WriteLine(" - Mudar a ordem de um item (2)");
                Console.WriteLine(" - Mostrar Itens por Setor (3)");
                Console.WriteLine(" - Voltar (4)");
                char key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case '0':

                        break;
                    case '1':
                        Console.Clear();
                        if (dataBase != null)
                        {
                            Console.WriteLine("-> Setores: ");
                            int i = 0;
                            List<string> uniqueSectors = dataBase.GetItemSectors();
                            foreach (string sector in uniqueSectors)
                                Console.WriteLine("\t {0}) - {1}.", i++, sector);
                            Console.WriteLine("-> Digite o indice do setor que quer baixar um item: ");
                            i = Int32.Parse(Console.ReadLine());
                            dataBase.PrintSector(i);
                            Console.WriteLine("-> Digite o indice do item que deseja baixar: ");
                            int j = Int32.Parse(Console.ReadLine());
                            dataBase.AdvanceItem(i, j);
                            Console.WriteLine("\n-> Item Baixado!\n\tPressione enter para continuar...");
                            SaveData(dataBase, "DataBase.xml");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("-> Banco de dados vazio!");
                            Console.WriteLine("\tPressione enter para continuar...");
                            Console.ReadLine();
                        }
                        break;
                    case '2':
                        Console.Clear();
                        if (dataBase != null)
                        {
                            Console.WriteLine("-> Setores: ");
                            int i = 0;
                            List<string> uniqueSectors = dataBase.GetItemSectors();
                            foreach (string sector in uniqueSectors)
                                Console.WriteLine("\t {0}) - {1}.", i++, sector);
                            Console.WriteLine("-> Digite o indice do setor que quer mover um item: ");
                            i = Int32.Parse(Console.ReadLine());
                            dataBase.PrintSector(i);
                            Console.WriteLine("-> Digite o indice do item que deseja mover: ");
                            int j = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("\n-> Digite o indice em que o item ficará: ");
                            int k = Int32.Parse(Console.ReadLine());
                            dataBase.MoveItemInSector(i, j, k);
                            Console.WriteLine("\n-> Item Movido!\n\tPressione enter para continuar...");
                            SaveData(dataBase, "DataBase.xml");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("-> Banco de dados vazio!");
                            Console.WriteLine("\tPressione enter para continuar...");
                            Console.ReadLine();
                        }
                        break;
                    case '3':
                        Console.Clear();
                        if (dataBase != null)
                        {
                            dataBase.PrintItemsPerSector();
                        }
                        else
                        {
                            Console.WriteLine("-> Banco de dados vazio!");
                        }
                        Console.WriteLine("\n\tPressione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '4':
                        exit = true;
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
                Console.WriteLine(" - Mostrar Itens por Setor (3)");
                Console.WriteLine(" - Sair (4)");
                char key = Console.ReadKey().KeyChar;
                switch(key)
                {
                    case '0':
                        dataBase = LoadNewOrders(reader, dataBase);
                        Console.Clear();
                        Console.WriteLine("-> Novas Ordens Adicionadas!\n\tPressione enter para continuar...");
                        Console.ReadLine();
                        break;
                    case '1':
                        FilterMenu(reader, dataBase);
                        break;
                    case '2':

                        break;
                    case '3':
                        ItensMenu(reader, dataBase);
                        break;
                    case '4':
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