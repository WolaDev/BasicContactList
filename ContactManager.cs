namespace BasicContactList
{
    internal class ContactManager : IContactManager
    {
        public static List<Contact> Contacts = new();
        public void AddContact(string name, string phoneNumber, string? email)
        {
            int id = Contacts.Count > 0 ? Contacts.Count + 1 : 1;

            var isContactExist = IsContactExist(phoneNumber);

            if (isContactExist)
            {
                Console.WriteLine("Contact already exist!");
                return;
            }

            var contact = new Contact
            {
                Id = id,
                Name = name,
                PhoneNumber = phoneNumber,
                Email = email,
                CreatedAt = DateTime.Now
            };

            Contacts.Add(contact);
            Console.WriteLine("Contact added successfully.");
            using (StreamWriter contactList = new ("ContactList/contacts.txt", true))
            {
                contactList.Write("ID: " + id + "\t");
                contactList.Write("Name: " + name + "\t");
                contactList.Write("Phone Number: " + phoneNumber + "\t");
                contactList.Write("Email: " + email + "\n");
                
            }
        }

        private bool IsContactExist(string phoneNumber)
        {
            return Contacts.Any(c => c.PhoneNumber == phoneNumber);
        }

        public void DeleteContact(string phoneNumber)
        {
            var contact = FindContact(phoneNumber);

            if (contact is null)
            {
                Console.WriteLine("Unable to delete contact as it does not exist!");
                return;
            }

            Contacts.Remove(contact);


        }

        public Contact? FindContact(string phoneNumber)
        {
            return Contacts.Find(c => c.PhoneNumber == phoneNumber);
        }

        public void GetContact(string phoneNumber)
        {
            var contact = FindContact(phoneNumber);
            if(contact is null)
            {
                Console.WriteLine($"Contact with {phoneNumber} not found");
            }
            else
            {
                Print(contact);
            }            
        }

        
            public void GetAllContacts()
            {
                // if(Contacts.Count == 0)
                // {
                //     Console.WriteLine("There is no contact added yet.");
                //     return;
                // }
                // else {

                try
                {
                    using (StreamReader listContacts = new ("ContactList/contacts.txt"))
                    {
                        string? readContacts = listContacts.ReadLine();
                        if (readContacts is null){
                            Console.WriteLine("There is no data in the file currently. Try adding one.");
                        }
                        while (readContacts is not null)
                            {
                                Console.WriteLine(readContacts);
                                readContacts = listContacts.ReadLine();
                            }
                    }
                    // }
                    
                    // foreach(var contact in Contacts)
                    // {
                    //     Print(contact, true);
                    // }
                }
                catch (Exception)
                {
                    
                    Console.WriteLine($"Thanks for checking the list. There is no data in the list yet, try adding one and check the list again.\nNOTE: You can add as many contact as you like and don't hesitate to close your program, your contact list is safe and available in our server.");
                }
            }
            

        public void UpdateContact(string phoneNumber, string name, string email)
        {
            var contact = FindContact(phoneNumber);

            if (contact is null)
            {
                Console.WriteLine("Contact does not exist!");
                return;
            }

            contact.Name = name;
            contact.Email = email;
            Console.WriteLine("Contact updated successfully.");
        }

        private void Print(Contact contact, bool full=false)
        {
            if (full)
            {
                Console.WriteLine($"{contact.Id}\t{contact.Name}\t{contact.PhoneNumber}\t{contact.Email}\t{contact.CreatedAt}");
            }
            else
            {
                Console.WriteLine($"Name: {contact!.Name}\nPhone Number: {contact!.PhoneNumber}\nEmail: {contact!.Email}");
            }

        }
    }
}
