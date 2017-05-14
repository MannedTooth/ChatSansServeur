namespace ChatSansServeur.Modeles
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using JetBrains.Annotations;

    /// <summary>
    /// Classe mère des modèles du système.
    /// </summary>
    internal class ModeleBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// Dictionnaire contenant les erreurs de validation. La clé est égal au nom de la propriété
        /// contenant des erreurs en string. La valeur est égal à une liste de string des erreurs.
        /// </summary>
        public Dictionary<string, IEnumerable<string>> ErreursValidationDictionary = new Dictionary<string, IEnumerable<string>>();

        /// <summary>
        /// Évènement avertissant qu'il y a des erreurs de validation pour une propriété.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Évènement permettant d'avertir les observateurs du changement d'une propriété
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Obtient une valeur indiquant si la liste ErreursValidation contient des erreurs.
        /// </summary>
        public bool HasErrors => ErreursValidationDictionary.Count > 0;

        /// <summary>
        /// Permet d'obtenir la liste des erreurs d'une propriété.
        /// </summary>
        /// <param name="nomPropriete">Nom de la propriété en string</param>
        /// <returns>Liste des erreurs d'une propriété</returns>
        public IEnumerable GetErrors(string nomPropriete)
        {
            IEnumerable<string> erreurs;
            return ErreursValidationDictionary.TryGetValue(nomPropriete, out erreurs) ? erreurs : null;
        }

        /// <summary>
        /// Méthode permettant d'appeler ErrorsChanged sans avoir à spécifier ses paramètres
        /// </summary>
        /// <param name="nomPropriete">Nom de la propriété qui a des erreurs de validation</param>
        public virtual void NotifierErreursChangees(string nomPropriete = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nomPropriete));
        }

        /// <summary>
        /// Méthode permettant d'appeler PropertyChanged sans avoir à spécifier ses paramètres
        /// </summary>
        /// <param name="nomPropriete">Nom de la propriété qui a changée</param>
        [NotifyPropertyChangedInvocator]
        public virtual void NotifierProprieteAChangee([CallerMemberName] string nomPropriete = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}