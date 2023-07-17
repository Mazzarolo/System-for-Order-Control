using Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    [DataContract]
    class DataBase
    {
        [DataMember]
        private List<Note> notes;

        public DataBase()
        {
            notes = new List<Note>();
        }

        public void AddNotes(List<Note> newNotes)
        {
            foreach (Note newNote in newNotes)
            {
                bool add = true;
                foreach (Note note in notes)
                {
                    if(newNote.GetOrder() == note.GetOrder())
                    {
                        add = false;
                    }
                }
                if(add)
                    notes.Add(newNote);
            }
        }

        public void PrintInfo()
        {
            int i = 1;
            foreach (Note note in notes)
            {
                Console.WriteLine("Nota {0}:", i++);
                note.PrintInfo();
            }
        }
    }
}
