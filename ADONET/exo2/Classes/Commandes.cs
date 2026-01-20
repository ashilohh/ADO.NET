using System;
using System.Collections.Generic;
using System.Text;

namespace exo2.Classes
{
    internal class Commandes
    {
        public int CommandeID {  get; set; }
        public DateTime Date { get; set; }
        public decimal Montant { get; set; }
        public int ClientID { get; set; }

        //private static decimal _montantTotal = 0;

        public Commandes() { }

        public Commandes(DateTime date_, decimal montant,int clientID)
        {
            Montant = montant;
            ClientID = clientID;
            Date = date_;
            //_montantTotal += montant;
            
        }

        public Commandes(int commandeID, DateTime date_,decimal montant, int clientID):this(date_,montant,clientID) 
        {
            CommandeID = commandeID;
            //_montantTotal += montant;
        }

        public override string ToString()
        {
            return $"Commande n°{CommandeID} | {Date} | Montant: {Montant} $ | ID Client: {ClientID} ";
        }
    }
}
