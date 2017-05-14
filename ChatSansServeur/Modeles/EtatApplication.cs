namespace ChatSansServeur.Modeles
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Contient toutes les variables définissant l'état de l'application
    /// </summary>
    internal class EtatApplication : ModeleBase
    {
        /// <summary>
        /// Variable privée de la propriété <see cref="Conversations"/>
        /// </summary>
        private readonly ObservableCollection<Conversation> _conversations = new ObservableCollection<Conversation>
        {
            new Conversation
            {
                Connecte = true,
                EstGlobale = true,
                Utilisateur = new Utilisateur
                {
                    Nom = "Global"
                }
            }
        };

        /// <summary>
        /// Variable privée de la propriété <see cref="UtilisateurLocal"/>
        /// </summary>
        private readonly Utilisateur _utilisateurLocal = new Utilisateur
        {
            Nom = string.Empty,
            Ip = "localhost"
        };

        /// <summary>
        /// Variable privée de la propriété <see cref="Connecte"/>
        /// </summary>
        private bool _connecte;

        /// <summary>
        /// Variable privée de la propriété <see cref="ConnexionEnCours"/>
        /// </summary>
        private bool _connexionEnCours;

        /// <summary>
        /// Obtient ou définit une valeur indiquant si l'utilisateur local a réussi à se connecter.
        /// </summary>
        public bool Connecte
        {
            get
            {
                return _connecte;
            }

            set
            {
                ErreursValidationDictionary.Clear();
                if (value != _connecte)
                {
                    _connecte = value;
                    NotifierProprieteAChangee();
                }
            }
        }

        /// <summary>
        /// Obtient ou définit une valeur indiquant si le processus de connexion est en cours
        /// </summary>
        public bool ConnexionEnCours
        {
            get
            {
                return _connexionEnCours;
            }

            set
            {
                ErreursValidationDictionary.Clear();
                if (value != _connexionEnCours)
                {
                    _connexionEnCours = value;
                    NotifierProprieteAChangee();
                }
            }
        }

        /// <summary>
        /// Obtient la liste des conversations en cours. Une conversation globale existe par défaut
        /// pour parler à tous les utilisateurs connectés. Les autres conversations seront de type privé.
        /// </summary>
        public ObservableCollection<Conversation> Conversations => _conversations;

        /// <summary>
        /// Obtient l'objet de type Utilisateur représentant l'utilisateur local. Utile pour ajouter
        /// une ligne dans une conversation envoyé par l'utilisateur local.
        /// </summary>
        public Utilisateur UtilisateurLocal => _utilisateurLocal;

        /// <summary>
        /// Obtient la liste des utilisateurs connectées qui utilise présentement cette application
        /// sur le réseau.
        /// </summary>
        public ObservableCollection<Utilisateur> UtilisateursConnectes { get; } = new ObservableCollection<Utilisateur>();
    }
}