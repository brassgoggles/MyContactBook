using System;
using System.Collections.Generic;
using System.IO;

namespace MyContactBook
{
    class Program
    {
        static string savePath = Path.Combine(Path.GetTempPath(), "my_contact_book.csv");

        static void Main(string[] args)
        {
            // So you can confirm the path that the .csv is written to.
            Console.WriteLine($"********** Path: {savePath} **********");

            List<Contact> contacts = RetrieveContacts();
            MainMenu(contacts);
        }

        private static List<Contact> RetrieveContacts()
        {
            List<Contact> contacts = new List<Contact>();

            try
            {
                using (var streamReader = new StreamReader(savePath))
                {
                    string currentLine = streamReader.ReadLine();

                    while (currentLine != null)
                    {
                        contacts.Add(CsvToContact(currentLine));
                        currentLine = streamReader.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RetrieveContacts Exception: {ex}");
            }
            return contacts;
        }

        private static Contact CsvToContact(string csv)
        {
            var lines = csv.Split(",");

            return new Contact(
                lines[0],
                lines[1],
                lines[2],
                lines[3]
                );
        }

        private static void MainMenu(List<Contact> contacts)
        {
            Console.WriteLine("--------------------------------------------\n\n" +
                "Welcome to MyContactBook\n\n" +
                "Please enter a number from one of the following options:\n" +
                "1 - Create contact.\n" +
                "2 - View all contacts.\n" +
                "3 - Exit program.\n\n" +
                "--------------------------------------------\n\n");

            string response = Console.ReadLine();

            switch (response)
            {
                case "1":
                    Contact contact = CreateContact(contacts.Count.ToString());
                    contacts.Add(contact);
                    SaveContact(contact);
                    break;
                case "2":
                    ViewContacts(contacts);
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
            }
            MainMenu(contacts);
        }

        private static Contact CreateContact(string id)
        {
            // Automatically assign an incremented id to the new contact.
            Contact contact = new Contact(id);

            // Assign remaining properties.
            contact.Name = AssignProperty("name");
            contact.Phone = AssignProperty("phone");
            contact.Address = AssignProperty("address");
            return contact;
        }

        private static string AssignProperty(string propertyName)
        {
            Console.WriteLine($"Please enter {propertyName}...");
            return Console.ReadLine();
        }

        // Save in .csv file.
        private static void SaveContact(Contact contact)
        {
            using (var writer = File.AppendText(savePath))
            {
                writer.WriteLine(contact.ToCsv());
                writer.Flush();
            }
        }

        private static void ViewContacts(List<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Name: {contact.Name}\n" +
                    $"Phone: {contact.Phone}\n" +
                    $"Address: {contact.Address}\n" +
                    $"--------------------------------------------\n\n");
            }
        }
    }
}
