using System;
using System.Collections.Generic;
using System.Text;

namespace MyContactBook
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Contact(string id)
        {
            Id = id;
        }

        public Contact(string id, string name,
            string phone, string address)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Address = address;
        }

        public string ToCsv()
        {
            return $"{Id},{Name},{Phone},{Address},";
        }
    }
}
