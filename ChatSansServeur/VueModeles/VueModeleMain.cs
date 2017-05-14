namespace ChatSansServeur.VueModeles
{
    using System.ComponentModel;
    using System.Windows.Input;
    using Modeles;
    using Modules;

    /// <summary>
    /// Vue-modèle pour la vue principale
    /// </summary>
    internal class VueModeleMain : VueModeleBase
    {
        /// <summary>
        /// État de l'application à faire suivre à travers les vues-modèles de l'application
        /// </summary>
        private readonly EtatApplication _etatApplication;

        /// <summary>
        /// Contient le vue-modèle associé à l'interface de demande de connexion
        /// </summary>
        private readonly VueModeleConnexion _vueModeleConnexion;

        /// <summary>
        /// Contient le vue-modèle associé à l'interface d'attente de connexion en cours
        /// </summary>
        private readonly VueModeleConnexionEnCours _vueModeleConnexionEnCours;

        /// <summary>
        /// Contient le vue-modèle associé à l'interface de clavardage
        /// </summary>
        private readonly VueModeleChat _vueModeleChat;

        /// <summary>
        /// Variable privée de la propriété <see cref="FermerApplication"/>
        /// </summary>
        private CommandeRelais<object> _fermerApplication;

        /// <summary>
        /// Variable privée de la propriété <see cref="VueModeleCourant"/>
        /// </summary>
        private VueModeleBase _vueModeleCourant;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="VueModeleMain"/>. Initialise les
        /// vues-modèles de l'application et spécifie le premier vue-modèle à afficher
        /// </summary>
        public VueModeleMain()
        {
            _etatApplication = new EtatApplication();
            _vueModeleConnexion = new VueModeleConnexion(_etatApplication);
            _vueModeleConnexionEnCours = new VueModeleConnexionEnCours();
            _vueModeleChat = new VueModeleChat(_etatApplication);

            VueModeleCourant = _vueModeleConnexion;

            _etatApplication.PropertyChanged += EtatApplicationOnPropertyChanged;
            _etatApplication.UtilisateurLocal.PropertyChanged += UtilisateurLocalOnPropertyChanged;
        }

        /// <summary>
        /// Obtient la commande permettant de bien fermer les connexions en cours avant de fermer
        /// l'application (UDP et TCP)
        /// </summary>
        public ICommand FermerApplication
        {
            get
            {
                return _fermerApplication ?? (_fermerApplication = new CommandeRelais<object>(
                           obj => { Protocole.Deconnexion(); }));
            }
        }

        /// <summary>
        /// Obtient le titre de l'application pour y mettre le nom de l'utilisateur
        /// </summary>
        public string Titre
        {
            get
            {
                if (_etatApplication == null || _etatApplication.UtilisateurLocal.Nom.Equals(string.Empty))
                {
                    return "ExempleMVVM";
                }

                return "ExempleMVVM @ " + _etatApplication.UtilisateurLocal.Nom;
            }
        }

        /// <summary>
        /// Obtient ou définit le vue-modèle présentement affiché dans la vue principale (les
        /// vues-modèles sont liés à leur vue dans le fichier App.xml)
        /// </summary>
        public VueModeleBase VueModeleCourant
        {
            get
            {
                return _vueModeleCourant;
            }

            set
            {
                if (!Equals(value, _vueModeleCourant))
                {
                    _vueModeleCourant = value;
                    NotifierProprieteAChangee();
                }
            }
        }

        /// <summary>
        /// Permet de changer d'interface lorsque l'utilisateur est connecté
        /// </summary>
        /// <param name="sender">Celui qui a appelé l'évènement</param>
        /// <param name="propertyChangedEventArgs">Contient le nom de la propriété qui a changée</param>
        private void EtatApplicationOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "Connecte":
                    if (_etatApplication.Connecte)
                    {
                        VueModeleCourant = _vueModeleChat;
                    }
                    else
                    {
                        VueModeleCourant = _vueModeleConnexion;
                    }

                    break;
                case "ConnexionEnCours":
                    if (_etatApplication.ConnexionEnCours)
                    {
                        VueModeleCourant = _vueModeleConnexionEnCours;
                    }
                    else if (!_etatApplication.Connecte)
                    {
                        VueModeleCourant = _vueModeleConnexion;
                    }

                    break;
            }
        }

        /// <summary>
        /// Permet de changer le titre de l'application lorsque le nom de l'utilisateur local change
        /// </summary>
        /// <param name="sender">Celui qui a appelé l'évènement</param>
        /// <param name="propertyChangedEventArgs">Contient le nom de la propriété qui a changée</param>
        private void UtilisateurLocalOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Nom")
            {
                NotifierProprieteAChangee(nameof(Titre));
            }
        }
    }
}