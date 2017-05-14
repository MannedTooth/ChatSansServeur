namespace ChatSansServeur.Modules
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Modeles;
    using System.Net;
    using System.Net.Sockets;
    using System.Net.NetworkInformation;
    using System.Threading;

    /// <summary>
    /// Protocole utilisant UDP 50000 permettant de clavarder avec plusieurs utilisateurs sans avoir
    /// un serveur centralisé. De plus, on peut initialiser une conversation crypter en AES128
    /// utilisant le TCP (port aléatoire).
    /// </summary>
    internal class Protocole
    {
        /// <summary>
        /// Représente l'état de l'application, contient: la liste des utilisateurs, la liste des
        /// conversations, le nom de l'utilisateur
        /// </summary>
        private static EtatApplication _etatApplication;

        /// <summary>
        /// Permet d'annuler toutes les tâches et les connexions
        /// </summary>
        private static readonly CancellationTokenSource Cts = new CancellationTokenSource();

        /// <summary>
        /// Permet d'obtenir rapidement le Token de Cts
        /// </summary>
        private static CancellationToken Ct => Cts.Token;

        /// <summary>
        /// Permet de vérifier si le nom d'utilisateur est déjà utilisé sur le réseau et de démarrer
        /// l'écoute sur le port 50000 UDP pour répondre au demande des autres utilisateurs. Durant
        /// le processus de connexion, etatApplication.ConnexionEnCours est égal à vrai. Si le nom
        /// d'utilisateur est utilisé, on ferme l'écoute sur le port 50000 UDP. Sinon,
        /// etatApplication.Connecte est égal à vrai.
        /// </summary>
        /// <param name="etatApplication">
        /// Représente l'état de l'application, contient: la liste des utilisateurs, la liste des
        /// conversations, le nom de l'utilisateur
        /// </param>
        public static async void Connexion(EtatApplication etatApplication)
        {
            _etatApplication = etatApplication;

            //jai ajouter ces 3 trucs la car cest comme ca que stephane le fait dans son exemple
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.EnableBroadcast = true;
            socket.Bind(new IPEndPoint(IPAddress.Any, 50000));

            await Task.Delay(1);

            _etatApplication.UtilisateurLocal.ErreursValidationDictionary[nameof(_etatApplication.UtilisateurLocal.Nom)] = new List<string> { "Connexion non implémentée." };
            _etatApplication.UtilisateurLocal.NotifierErreursChangees(nameof(_etatApplication.UtilisateurLocal.Nom));
        }

        /// <summary>
        /// Méthode permettant de fermer toutes les connexions en cours (UDP et TCP)
        /// </summary>
        public static void Deconnexion()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Permet d'ouvrir un port TCP et d'envoyer par UDP une demande de connexion en privée à l'utilisateur distant
        /// </summary>
        /// <param name="nouvelleConversation">Conversation privée contenant l'utilisateur distant</param>
        public static void DemarrerConversationPrivee(Conversation nouvelleConversation)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Méthode permettant d'envoyer un message sur la conversation en cours
        /// </summary>
        /// <param name="conversationEnCours">
        /// Conversation présentement sélectionnée pour envoyer le message
        /// </param>
        /// <param name="messageAEnvoyer">Message à envoyer à tous les utilisateurs</param>
        public static void EnvoyerMessage(Conversation conversationEnCours, string messageAEnvoyer)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Méthode permettant d'envoyer un message en "broadcast" pour sonder tous les utilisateurs
        /// utilisant l'application sur le réseau. Par la suite, on peut rafraîchir la liste etatApplication.UtilisateursConnectes.
        /// </summary>
        public static async void RafraichirListeUtilisateursConnectes()
        {
            await Task.Delay(1);
        }

        /// <summary>
        /// Permet de fermer correctement une conversation privée
        /// </summary>
        /// <param name="conversation">Conversation à fermer</param>
        public static void TerminerConversationPrivee(Conversation conversation)
        {
            throw new System.NotImplementedException();
        }
    }
}