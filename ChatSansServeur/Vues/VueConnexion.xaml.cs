namespace ChatSansServeur.Vues
{
    using System.Windows.Input;
    using VueModeles;

    /// <summary>
    /// Logique d'interaction pour VueConnexion.xaml
    /// </summary>
    public partial class VueConnexion
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="VueConnexion"/>
        /// </summary>
        public VueConnexion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Permet de gérer l'appuie de la touche "Enter" sur le textbox du nom d'utilisateur pour
        /// activer la commande de connexion
        /// </summary>
        /// <param name="sender">Celui qui a appelé l'évènement</param>
        /// <param name="e">Contient les informations sur la touche appuyée</param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = (VueModeleConnexion)DataContext;
            if (e.Key == Key.Enter)
            {
                if (vm.ConnecterUtilisateurCommand.CanExecute(null))
                {
                    vm.ConnecterUtilisateurCommand.Execute(null);
                }
            }
        }
    }
}