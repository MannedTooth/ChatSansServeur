namespace ChatSansServeur.Vues
{
    using System.Collections;
    using System.Collections.Specialized;

    /// <summary>
    /// ListView ayant la possibilité de défiler automatique vers le bas lors de l'ajout de contenu
    /// </summary>
    public partial class AutoScrollingListView
    {
        /// <summary>
        /// Redéfinition de la méthode appelée lorsque la liste des items change. Ici, on s'assure
        /// d'associer la méthode ItemsCollectionChanged à l'évènement CollectionChanged de la
        /// nouvelle liste.
        /// </summary>
        /// <param name="oldValue">Ancienne liste d'items</param>
        /// <param name="newValue">Nouvelle liste d'items</param>
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            try
            {
                ((INotifyCollectionChanged)oldValue).CollectionChanged -= ItemsCollectionChanged;
            }
            catch
            {
                // ignored
            }

            try
            {
                ((INotifyCollectionChanged)newValue).CollectionChanged += ItemsCollectionChanged;
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Lorsque la liste change, on défile vers l'item le plus bas.
        /// </summary>
        /// <param name="sender">Celui qui a appelé l'évènement</param>
        /// <param name="e">Contient les changements effectués sur la liste</param>
        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Items.Count > 0)
            {
                ScrollIntoView(Items[Items.Count - 1]);
            }
        }
    }
}