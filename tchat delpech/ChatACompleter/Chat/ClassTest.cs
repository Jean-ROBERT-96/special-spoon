// ESPACE DE NOM DE LA CLASSE / PACKAGE EN JAVA
using ChatServer.Model.Business;
using System.Collections.Generic;

namespace Chat
{
    // VISIBILITE ET NOM DE LA CLASSE
    public class ClassTest
    {
        // PROPRIETES
        public List<string> loginList;
        public string Nom { get; set; }

        // CONSTRUCTEUR
        public ClassTest()
        {
            loginList = new List<string>();
        }

        // METHODE SANS RETOUR
        public void MethodeTest()
        {

        }

        // METHODE AVEC RETOUR
        public string MethodeTest2(string nom)
        {
            return string.Format("bonjour {0}", nom);
        }

       
    }
}
