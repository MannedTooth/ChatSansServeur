namespace ChatSansServeur.Modeles
{
    /// <summary>
    /// Spécifie une ligne dans une conversation. Il y a une ligne par message envoyé ou reçu.
    /// </summary>
    internal class LigneConversation : ModeleBase
    {
        /// <summary>
        /// Variable privée de la propriété <see cref="Message"/>
        /// </summary>
        private string _message;

        /// <summary>
        /// Variable privée de la propriété <see cref="Utilisateur"/>
        /// </summary>
        private Utilisateur _utilisateur;

        /// <summary>
        /// Obtient ou définit le message que l'utilisateur a envoyé
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                ErreursValidationDictionary.Clear();
                if (value != _message)
                {
                    _message = value;
                    NotifierProprieteAChangee();
                }
            }
        }

        /// <summary>
        /// Obtient ou définit l'utilisateur qui a envoyé le message.
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