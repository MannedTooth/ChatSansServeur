namespace ChatSansServeur.VueModeles
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Permet de créer une commande de type ICommand en ne spécifiant que les fonctions "Execute" et
    /// "CanExecute". On peut envoyer un paramètre à la commande.
    /// </summary>
    /// <typeparam name="T">Type du paramètre de la commande.</typeparam>
    internal class CommandeRelais<T> : ICommand
    {
        /// <summary>
        /// Contient la fonction permettant de savoir si on peut exécuter la commande ou non
        /// </summary>
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Contient la méthode permettant d'exécuter la commande
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CommandeRelais{T}"/>.
        /// </summary>
        /// <param name="execute">La logique d'exécution.</param>
        /// <param name="canExecute">La logique du statut de l'exécution</param>
        public CommandeRelais(Action<T> execute, Predicate<T> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentException(nameof(execute));
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Se produit lorsque la possibilité de pouvoir exécuter la commande change
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Permet de savoir si on peut exécuter la commande ou non
        /// </summary>
        /// <param name="parameter">
        /// Paramètre envoyer à la commande (peut être utilis à travers le CanExecute définit dans le constructeur
        /// </param>
        /// <returns>Vrai, si on peut exécuter la commande</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        /// <summary>
        /// Permet d'exécuter la commande
        /// </summary>
        /// <param name="parameter">
        /// Paramètre envoyer à la commande (peut être utilis à travers le Execute définit dans le constructeur
        /// </param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}