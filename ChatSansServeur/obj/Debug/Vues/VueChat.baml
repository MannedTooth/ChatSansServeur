   M S B A M L   `   `   `  ����J  FChatSansServeur, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ChatSansServeur.VueModelesChatSansServeur.VueModeles  5      ;"clr-namespace:ChatSansServeur.VuesChatSansServeur.Vues  5      A%clr-namespace:ChatSansServeur.ModelesChatSansServeur.Modeles  5      "    ChatSansServeur.Vues.VueChat   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?
vueModeles(clr-namespace:ChatSansServeur.VueModeles     5      4local"clr-namespace:ChatSansServeur.Vues     5      9modeles%clr-namespace:ChatSansServeur.Modeles     5      .��5      �   � RowDefinitions  5   
   �� 5      �� 5      $	��100 �6    � ColumnDefinitions 5   
   �� 5      �� 5      $	��150 �6   .+�5   
   �� $��0��6   $��0��6"   ��62   �� *ListeConversations+ �� SelectedItem 6]   �� *ConversationEnCours+ � 	Resources 5       �� 
TargetType �� Property �� Value �� DataType*  $ChatSansServeur.Modeles.Conversation �� Orientation   Key	 �� RelativeSource
 �� AncestorType �� Mode �� Path   BooleanToVisibilityConverter �� 	Converter0   *ChatSansServeur.Vues.AutoScrollingListView/  )ChatSansServeur.Modeles.LigneConversation �� 
IsReadOnly �� TextWrapping��%V  '��       7  �� 5      " ��6   .�5      ��  � �� 5      �� 5      "  6+   .R�5   "   �� $ 
Horizontal=�6-    5   &   ���� 5   *    BooleanToVisibilityConverter  6G   5   '   .+�5    &   �� ��6,   �� *Utilisateur.Nom+�� 5!   &   
������ 
������ ��6-   �� 	 �� $
 UserControlL�$ FindAncestor=�$+ %DataContext.FermerConversationCommand���6�   �� y�6�   �� *	EstPrivee+8   .��6d  X6g  5"   #   5#      5%      �� 5'       � �� 5(      �� 5)      "  6+   .R�5*   "     ��6>   �� *LigneConversations+��5+   &   �� 5,   *   "  67   (��       *, +).R�5-   .   �� $ 
Horizontal=�69   .+�5.   2   �� 
��� ���$
��Bold"�6P   ��6b   �� *Utilisateur.Nom+�� 5/   2   $
��Bold"�6<   $	��2,0q�6O   @6_   �� 50   2   
����  �$
��Bold"�6R   ��6e   �� *Utilisateur.Ip+�� 51   2   $
��Bold"�6<   $��2,0,4,0q�6O   :6c   �� 52   2   
������ $��0q�6S    . $
 Wrap=�6y   ��6�   �� *	Message+53   /   54   +   56   #   57      59      5:      5;      5<      �� 5>   
   $��0��6   $��1��6"   .@�5?      �� $��Utilisateurs��6   .��5@      �� -   5         ��Name$ LvUtilisateursConnectes��5@       �� SelectionMode$ Single=�6o   ��6<   �� *ListeUtilisateursConnectes+��5B      �� 5C      " ��6    .�5D      -   5        5E      ��5H      �� 5I      )  #ChatSansServeur.Modeles.Utilisateur"  6'   .R�5J      �� $ 
Horizontal=�6)   .+�5K   "   �� 
��� ���$
��Bold"�6@   ��6R   �� *Nom+�� 5L   "   
���   �$
��Bold"�6?   $	��2,0q�6R   @6b   �� 5M   "   
����  �$
��Bold"�6B   ��6U   �� *Ip+5N      5O      5Q      5R      5S      �� 5U   
   -   5         ��Name$ TxtMessageAEnvoyer��5U      $��1��6.   $��0��6;   $
 Wrap=�6�   ��6K   �� *MessageAEnvoyer+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5V   
   $��1��6   $��1��6   $��15,30q�6.   ��6=   �� *EnvoyerMessageCommand+.��6g   	Envoyer6p   5W      5X      