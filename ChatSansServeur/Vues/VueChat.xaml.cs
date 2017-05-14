namespace ChatSansServeur.Vues
{
    using System.Windows.Input;
    using VueModeles;

    /// <summary>
    /// Logique d'interaction pour VueChat.xaml
    /// </summary>
    public partial class VueChat
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="VueChat"/>
        /// </summary>
        public VueChat()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Permet de gérer l'appuie du DoubleClick sur un nom d'utilisateur pour ouvrir une
        /// conversation avec cet utilisateur.
        /// </summary>
        /// <param name="sender">Celui qui a appelé l'évènement</param>
        /// <param name="e">Contient les informations sur le clique effectué</param>
        private void LvUtilisateursConnectesItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (VueModeleChat)DataContext;
            if (vm.OuvrirConversationCommand.CanExecute(LvUtilisateursConnectes.SelectedItem))
            {
                vm.OuvrirConversationCommand.Execute(LvUtilisateursConnectes.SelectedItem);
            }
        }

        /// <summary>
        /// Permet de gérer l'appuie de la touche "Enter" sur le TextBox de message à envoyer pour
        /// appeler la commande d'envoie de message
        /// </summary>
        /// <param name="sender">Celui qui a appelé l'évènement</param>
        /// <param name="e">Contient les informations sur la touche appuyée</param>
        private void TxtMessageAEnvoyer_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (VueModeleChat)DataContext;
            if (e.Key == Key.Enter && vm.EnvoyerMessageCommand.CanExecute(null))
            {
                vm.EnvoyerMessageCommand.Execute(null);
            }
        }
    }
}