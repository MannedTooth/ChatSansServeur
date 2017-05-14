namespace ChatSansServeur.Modeles
{
    using System.Collections.ObjectModel;
    using System.Net.Sockets;

    /// <summary>
    /// Permet de gérer une conversation. Une conversation peut être la conversation globale (avec
    /// tous les utilisateurs) ou une conversation privée (avec un utilisateur seulement).
    /// </summary>
    internal class Conversation : ModeleBase
    {
        /// <summary>
        /// Variable privée de la propriété <see cref="Connecte"/>
        /// </summary>
        private bool _connecte;

        /// <summary>
        /// Variable privée de la propriété <see cref="EstGlobale"/>
        /// </summary>
        private bool _estGlobale;

        /// <summary>
        /// Variable privée de la propriété <see cref="EstGlobale"/>
        /// </summary>
        private Utilisateur _utilisateur;

        /// <summary>
        /// Obtient ou définit une valeur indiquant si la connexion est établie avec l'utilisateur
        /// distant (pour les conversations privée) (à vrai si conversation globale)
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
        /// Obtient ou définit une valeur indiquant si la conversation est globale, c'est-à-dire, à
        /// tous les usagers du système
        /// </summary>
        public bool EstGlobale
        {
            get
            {
                return _estGlobale;
            }

            set
            {
                ErreursValidationDictionary.Clear();
                if (value != _estGlobale)
                {
                    _estGlobale = value;
                    NotifierProprieteAChangee();
                }
            }
        }

        /// <summary>
        /// Obtient une valeur indiquant si la conversation est privée, c'est-à-dire, liée à un seul utilisateur.
        /// </summary>
        public bool EstPrivee => !EstGlobale;

        /// <summary>
        /// Obtient ou définit la variable IV pour le cryptage en AES 128 pour les conversations privées
        /// </summary>
        public byte[] IvBytes
        {
            get;
            set;
        }

        /// <summary>
        /// Obtient ou définit la variable Key pour le cryptage en AES 128 pour les conversations privées
        /// </summary>
        public byte[] KeyBytes
        {
            get;
            set;
        }

        /// <summary>
        /// Obtient la liste des lignes de la conversation contenant le nom d'utilisateur,
        /// l'adresse IP et le message.
        /// </summary>
        public ObservableCollection<LigneConversation> LigneConversations { get; } = new ObservableCollection<LigneConversation>();

        /// <summary>
        /// Obtient ou définit le Socket associé à la conversation
        /// </summary>
        public Socket Socket
        {
            get;
            set;
        }

        /// <summary>
        /// Obtient ou définit l'utilisateur auquel la conversation est liée. Utile pour une
        /// conversation privée. Dans le cas d'une conversation globale, on peut mettre un
        /// utilisateur bidon pour avoir le nom de l'utilisateur de la conversation.
        /// </summary>
        public Utilisateur Utilisateur
        {
            get
            {
                return _utilisateur;
            }

            set
            {
                ErreursValidationDictionary.Clear();
                if (!Equals(value, _utilisateur))
                {
                    _utilisateur = value;
                    NotifierProprieteAChangee();
                }
            }
        }
    }
}