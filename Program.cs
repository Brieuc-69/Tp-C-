using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Contact
{
    public string Nom { get; set; }
    public string Numero { get; set; };

    public string ImageProfil { get; set;}

     public Dictionary<string, string> ChampsPersonnalises { get; set;} = new Dictionary<string, string>();
}



class GestionnaireContacts
{
    private List<Contact> contacts = new List<Contact>();
    private string fichierContacts = "contacts.txt";


    public void ChargerContacts()
    {
        if (File.Exists(fichierContacts))
        {
            string[] lignes = File.ReadAllLines(fichierContacts);
            foreach (var ligne in lignes)
            {
                string[] elements = ligne.Split(',');
                AjouterContact(elements[0], elements[1]);
            }
        }
    }

   
    public void SauvegarderContacts()
    {
        using (StreamWriter writer = new StreamWriter(fichierContacts))
        {
            foreach (var contact in contacts)
            {
                writer.WriteLine($"{contact.Nom},{contact.Numero}");
            }
        }
    }


    public void AjouterContact(string nom, string numero string ImageProfil)
    {
        Contact nouveauContact = new Contact { Nom = nom, Numero = numero, ImageProfil = imageProfil};
        contacts.Add(nouveauContact);
        Console.WriteLine($"Contact ajouté : {nouveauContact}");
    }

    
    public void AfficherContacts()
    {
        Console.WriteLine("Liste des contacts :");
        foreach (var contact in contacts)
        {
            Console.WriteLine(contact);
        }
    }

   
    public void RechercherContact(string nom)
    {
        Contact contactTrouve = contacts.FirstOrDefault(c => c.Nom.Equals(nom, StringComparison.OrdinalIgnoreCase));
        if (contactTrouve != null)
        {
            Console.WriteLine($"Contact trouvé : {contactTrouve}");
        }
        else
        {
            Console.WriteLine("Aucun contact trouvé avec ce nom.");
        }
    }

    public void ModifierNumeroContact(string nom, string nouveauNumero)
    {
        Contact contactAModifier = contacts.FirstOrDefault(c => c.Nom.Equals(nom, StringComparison.OrdinalIgnoreCase));
        if (contactAModifier != null)
        {
            contactAModifier.Numero = nouveauNumero;
            Console.WriteLine($"Numéro de contact mis à jour : {contactAModifier}");
        }
        else
        {
            Console.WriteLine("Aucun contact trouvé avec ce nom.");
        }
    }

    public void SupprimerContact(string nom)
    {
        Contact contactASupprimer = contacts.FirstOrDefault(c => c.Nom.Equals(nom, StringComparison.OrdinalIgnoreCase));
        if (contactASupprimer != null)
        {
            contacts.Remove(contactASupprimer);
            Console.WriteLine($"Contact supprimé : {contactASupprimer}");
        }
        else
        {
            Console.WriteLine("Aucun contact trouvé avec ce nom.");
        }
    }

    public void EnvoyerMessage(string nom, string message)
    {
       Contact contactDestinataire = contacts.FirstOrDefault(c => c.Nom.Equals(nom, StringComparison.OrdinalIgnoreCase));
       if(contactDestinataire != null)
       {
        System.Console.WriteLine($"Message envoyer a {nom} : {messages}");
       }
       else{
        System.Console.WriteLine("Aucun contact trouvé avec ce nom");
       }
    }

    private void CheckRappels(object state)
{
    DateTime now = DateTime.Now;
    List<Rappel> rappelsAFaire = rappels.Where(r => r.Date <= now).ToList();
    foreach (var rappel in rappelsAFaire)
    {
        AfficherNotificationRappel(rappel.Description);
        rappels.Remove(rappel);
    }
}

public void AfficherNotificationRappel(string description)
{
    Console.WriteLine($"Rappel : {description}");
}

  public void TrierContactsParNom()
    {
        contacts = contacts.OrderBy(c => c.Nom).ToList();
        Console.WriteLine("Contacts triés par nom.");
    }

    public void FiltrerContactsParPrefixe(string prefixe)
    {
        List<Contact> contactsFiltres = contacts.Where(c => c.Nom.StartsWith(prefixe)).ToList();
        if (contactsFiltres.Any())
        {
            Console.WriteLine($"Contacts filtrés par le préfixe '{prefixe}':");
            foreach (var contact in contactsFiltres)
            {
                Console.WriteLine(contact);
            }
        }
        else
        {
            Console.WriteLine($"Aucun contact trouvé avec le préfixe '{prefixe}'.");
        }
    }

    public void AjouterChampPersonnalise(Contact contact, string cle, string valeur)
    {
        contact.ChampsPersonnalises[cle] = valeur;
        Console.WriteLine($"Champ personnalisé ajouté au contact {contact.Nom}: {cle} - {valeur}");
    }

}



class Program
{
    static void Main(string[] args)
    {
        GestionnaireContacts gestionnaire = new GestionnaireContacts();

   
        gestionnaire.ChargerContacts();

        bool continuer = true;
        while (continuer)
        {
            Console.WriteLine("\nMENU:");
            Console.WriteLine("1. Ajouter un contact");
            Console.WriteLine("2. Afficher tous les contacts");
            Console.WriteLine("3. Rechercher un contact par nom");
            Console.WriteLine("4. Modifier le numéro d'un contact");
            Console.WriteLine("5. Supprimer un contact");
            Console.WriteLine("6. Quitter");
            Console.Write("Votre choix : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    Console.Write("Nom du contact : ");
                    string nom = Console.ReadLine();
                    Console.Write("Numéro du contact : ");
                    string numero = Console.ReadLine();
                    gestionnaire.AjouterContact(nom, numero);
                    break;
                case "2":
                    gestionnaire.AfficherContacts();
                    break;
                case "3":
                    Console.Write("Nom du contact à rechercher : ");
                    string nomRecherche = Console.ReadLine();
                    gestionnaire.RechercherContact(nomRecherche);
                    break;
                case "4":
                    Console.Write("Nom du contact à modifier : ");
                    string nomModification = Console.ReadLine();
                    Console.Write("Nouveau numéro : ");
                    string nouveauNumero = Console.ReadLine();
                    gestionnaire.ModifierNumeroContact(nomModification, nouveauNumero);
                    break;
                case "5":
                    Console.Write("Nom du contact à supprimer : ");
                    string nomSuppression = Console.ReadLine();
                    gestionnaire.SupprimerContact(nomSuppression);
                    break;
                case "6":
                    continuer = false;
                    break;
                case "7":
                    Console.Write("Nom du contact destinataire : ");
                    string nomDestinataire = Console.ReadLine();
                    Console.Write("Message : ");
                    string message = Console.ReadLine();
                    gestionnaire.EnvoyerMessage(nomDestinataire, message);
                case "8":
                     Console.Write("Notification de Rappel");
                     string description = Console.ReadLine();
                     break;
                case "9":
                    Console.Write("Entrez le préfixe pour filtrer les contacts :");
                     string prefixe = Console.ReadLine();
                     gestionnaire.FiltrerContactsParPrefixe(prefixe);
                    break;
        
                default:
                    Console.WriteLine("Choix invalide. Veuillez entrer un numéro entre 0 et 9.");
                    break;
            }
        }

        gestionnaire.SauvegarderContacts();
    }



}


