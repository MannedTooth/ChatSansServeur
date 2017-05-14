namespace ChatSansServeur.VueModeles
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using Modeles;
    using Modules;

    /// <summary>
    /// Vue-modèle pour la vue vueChat
    /// </summary>
    internal class VueModeleChat : VueModeleBase
    {
        /// <summary>
        /// État de l'application à faire suivre à travers les vues-modèles de l'application
        /// </summary>
        private readonly EtatApplication _etatApplication;

        /// <summary>
        /// Variable privée de la propriété <see cref="ConversationEnCours"/>
        /// </summary>
        private Conversation _conversationEnCours;

        /// <summary>
        /// Variable privée de la propriété <see cref="EnvoyerMessageCommand"/>
        /// </summary>
        private ICommand _envoyerMessageCommand;

        /// <summary>
        /// Variable privée de la propriété <see cref="FermerConversationCommand"/>
        /// </summary>
        private ICommand _fermerConversationCommand;

        /// <summary>
        /// Variable privée de la propriété <see cref="MessageAEnvoyer"/>
        /// </summary>
        private string _messageAEnvoyer;

        /// <summary>
        /// Variable privée de la propriété <see cref="OuvrirConversationCommand"/>
        /// </summary>
        private ICommand _ouvrirConversationCommand;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="VueModeleChat"/>
        /// </summary>
        /// <param name="etatApplication">
        /// État de l'application à faire suivre à travers les vues-modèles de l'application
        /// </param>
        public VueModeleChat(EtatApplication etatApplication)
        {
            _etatApplication = etatApplication;

            _etatApplication.PropertyChanged += EtatApplicationOnPropertyChanged;
        }

        /// <summary>
        /// Obtient ou définit la conversation présentement sélectionnée, à qui les messages sont envoyés
        /// </summary>
        public Conversation ConversationEnCours
        {
            get
            {
                if (_conversationEnCours == null && _etatApplication.Conversations.Count > 0)
                {
                    _conversationEnCours = _etatApplication.Conversations[0];
                }

                return _conversationEnCours;
            }

            set
            {
                if (!Equals(value, _conversationEnCours))
                {
                    _conversationEnCours = value;
                    NotifierProprieteAChangee();
                }
            }
        }

        /// <summary>
        /// Obtient la commande permettant d'envoyer le message se trouvant dans MessageAEnvoyer dans
        /// la conversation en cours.
        /// </summary>
        public ICommand EnvoyerMessageCommand
            => _envoyerMessageCommand ?? (_envoyerMessageCommand = new CommandeRelais<object>(
                   obj =>
                   {
                       ConversationEnCours.LigneConversations.Add(new LigneConversation
                       {
                           Utilisateur = _etatApplication.UtilisateurLocal,
                           Message = MessageAEnvoyer
                       });

                       Protocole.EnvoyerMessage(ConversationEnCours, MessageAEnvoyer);
                       MessageAEnvoyer = string.Empty;
                   },
                   obj => !string.IsNullOrWhiteSpace(MessageAEnvoyer) && ConversationEnCours.Connecte));

            /// <summary>
            /// Obtient la commande permettant de fermer une conversation privée.
            /// </summary>
            public ICommand FermerConversationCommand
            => _fermerConversationCommand ?? (_fermerConversationCommand = new CommandeRelais<Conversation>(
                   conversation =>
                   {
                       ConversationEnCours = _etatApplication.Conversations[0];
                       Protocole.TerminerConversationPrivee(conversation);
                   },
                   conversation => conversation != null && _etatApplication.Conversations.Count > 0 && conversation.EstPrivee));

                /// <summary>
                /// Obtient la liste des conversations en cours incluant la conversation globale
                /// </summary>
                public ObservableCollection<Conversation> ListeConversations => _etatApplication.Conversations;

                /// <summary>
                /// Obtient la liste des utilisateurs connectées qui utilise cette application sur le réseau présentement
                /// </summary>
                public ObservableCollection<Utilisateur> ListeUtilisateursConnectes => _etatApplication.UtilisateursConnectes;

                /// <summary>
                /// Obtient ou définit le message a envoyé à la conversation en cours lors de l'appel de la
                /// commande EnvoyerMessageCommand
                /// </summary>
                public string MessageAEnvoyer
                {
                    get
                    {
                        return _messageAEnvoyer;
                    }

                    set
                    {
                        if (value != _messageAEnvoyer)
                        {
                            _messageAEnvoyer = value;
                            NotifierProprieteAChangee();
                        }
                    }
                }

                /// <summary>
                /// Obtient la commande permettant d'ouvrir une conversation entre l'utilisateur local et un
                /// utilisateur dans la liste des utilisateurs connectés.
                /// </summary>
                public ICommand OuvrirConversationCommand
                => _ouvrirConversationCommand ?? (_ouvrirConversationCommand = new CommandeRelais<Utilisateur>(
                   utilisateur =>
                   {
                       var conversationsExistantes = _etatApplication.Conversations.Where(
                           c => c.Utilisateur.Nom == utilisateur.Nom && c.Utilisateur.Ip == utilisateur.Ip).ToList();
                       if (conversationsExistantes.Count == 0)
                       {
                           var nouvelleConversation = new Conversation
                           {
                               Connecte = false,
                               EstGlobale = false,
                               Utilisateur = utilisateur
                           };

                           _etatApplication.Conversations.Add(nouvelleConversation);
                           Protocole.DemarrerConversationPrivee(nouvelleConversation);
                           conversationsExistantes.Add(nouvelleConversation);
                       }

                       ConversationEnCours = conversationsExistantes[0];
                    },
                    utilisateur => utilisateur != null));

                    /// <summary>
                    /// Permet de démarrer le processus de récupération des utilisateurs
                    /// </summary>
                    /// <param name="sender">Celui qui a appelé l'évènement</param>
                    /// <param name="propertyChangedEventArgs">Contient le nom de la propriété qui a changée</param>
                    private void EtatApplicationOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
                    {
                        if (propertyChangedEventArgs.PropertyName.Equals(nameof(_etatApplication.Connecte)) && _etatApplication.Connecte)
                        {
                            Protocole.RafraichirListeUtilisateursConnectes();
                        }
                    }
                }
            }