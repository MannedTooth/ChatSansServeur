namespace ChatSansServeur.VueModeles
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using Modeles;
    using Modules;

    /// <summary>
    /// Vue-modèle pour la vue vueConnexion
    /// </summary>
    internal class VueModeleConnexion : VueModeleBase
    {
        /// <summary>
        /// État de l'application à faire suivre à travers les vues-modèles de l'application
        /// </summary>
        private readonly EtatApplication _etatApplication;

        /// <summary>
        /// Variable privée de la propriété <see cref="ConnecterUtilisateurCommand"/>
        /// </summary>
        private ICommand _connecterUtilisateurCommand;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="VueModeleConnexion"/>
        /// </summary>
        /// <param name="etatApplication">
        /// État de l'application à faire suivre à travers les vues-modèles de l'application
        /// </param>
        public VueModeleConnexion(EtatApplication etatApplication)
        {
            _etatApplication = etatApplication;

            _etatApplication.UtilisateurLocal.ErrorsChanged += UtilisateurLocalOnErrorsChanged;
        }

        /// <summary>
        /// Obtient la commande permettant de connecter l'utilisateur à l'application.
        /// </summary>
        public ICommand ConnecterUtilisateurCommand => _connecterUtilisateurCommand ?? (_connecterUtilisateurCommand = new CommandeRelais<object>(
                                                           obj =>
                                                           {
                                                               Protocole.Connexion(_etatApplication);
                                                           },
                                                           obj => !string.IsNullOrWhiteSpace(NomUtilisateur)));

        /// <summary>
        /// Obtient ou définit le nom de l'utilisateur local pour se connecter
        /// </summary>
        public string NomUtilisateur
        {
            get
            {
                return _etatApplication.UtilisateurLocal.Nom;
            }

            set
            {
                ErreursValidationDictionary.Clear();
                _etatApplication.UtilisateurLocal.ErreursValidationDictionary.Clear();
                if (value != _etatApplication.UtilisateurLocal.Nom)
                {
                    _etatApplication.UtilisateurLocal.Nom = value;
                    NotifierProprieteAChangee();
                }
            }
        }

        /// <summary>
        /// Permet de vérifier si la propriété Nom de l'utilisateur local a eu des erreurs de validation lors de la connexion
        ///     (Ex. : Nom d'utilisateur déjà utilisé)
        /// </summary>
        /// <param name="sender">Envoyeur de l'évènement</param>
        /// <param name="dataErrorsChangedEventArgs">Contient le nom de la propriété erronée</param>
        private void UtilisateurLocalOnErrorsChanged(object sender, DataErrorsChangedEventArgs dataErrorsChangedEventArgs)
        {
            if (dataErrorsChangedEventArgs.PropertyName == nameof(_etatApplication.UtilisateurLocal.Nom))
            {
                ErreursValidationDictionary[nameof(NomUtilisateur)] =
                    (IEnumerable<string>)
                    _etatApplication.UtilisateurLocal.GetErrors(nameof(_etatApplication.UtilisateurLocal.Nom));
                NotifierErreursChangees(nameof(NomUtilisateur));
            }
        }
    }
}