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

        static void Main(string[] args)
        {
            Reader reader = new Reader();
            reader.LoadFiles();
            reader.ParseFiles();
            //reader.PrintInfo();
            DataBase dataBase = new DataBase();
            //SaveData(dataBase, "DataBase.xml");
            //dataBase = null;
            dataBase = LoadData<DataBase>("DataBase.xml");
            dataBase.AddNotes(reader.getFiles());
            dataBase.PrintInfo();
        }
    }
}