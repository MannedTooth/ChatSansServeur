namespace ChatSansServeur.Modeles
{
    /// <summary>
    /// Utilisateur du système. Soit utilisateur local ou utilisateurs connectés utilisant cette application.
    /// </summary>
    internal class Utilisateur : ModeleBase
    {
        /// <summary>
        /// Variable privée de la propriété <see cref="Ip"/>
        /// </summary>
        private string _ip;

        /// <summary>
        /// Variable privée de la propriété <see cref="Nom"/>
        /// </summary>
        private string _nom;

        /// <summary>
        /// Obtient ou définit l'adresse IP permettant d'envoyer un message à cet utilisateur.
        /// </summary>
        public string Ip
        {
            get
            {
                return _ip;
            }

            set
            {
                ErreursValidationDictionary.Clear();
                if (value != _ip)
                {
                    _ip = value;
                    NotifierProprieteAChangee();
                }
            }
        }

        /// <summary>
        /// Obtient ou définit le nom d'utilisateur
        /// </summary>
        public string Nom
        {
            get
            {
                return _nom;
            }

            set
            {
                ErreursValidationDictionary.Clear();
                if (value != _nom)
                {
                    _nom = value;
                    NotifierProprieteAChangee();
                }
            }
        }
    }
}