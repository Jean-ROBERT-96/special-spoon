   M S B A M L   `   `   `  ????F  BOptiEditeur, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>.clr-namespace:OptiEditeur;assembly=OptiEditeurOptiEditeur  5   	   7 clr-namespace:OptiEditeur.ModelsOptiEditeur.Models  5   	   5clr-namespace:OptiEditeur.ViewsOptiEditeur.Views  5   	   ;"clr-namespace:OptiEditeur.ServicesOptiEditeur.Services  5	   	   e aMicrosoft.Web.WebView2.Wpf, Version=1.0.1587.40, Culture=neutral, PublicKeyToken=2a8ab48044d2601ekLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.WpfMicrosoft.Web.WebView2.Wpf 5
   	       OptiEditeur.MainWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @local.clr-namespace:OptiEditeur;assembly=OptiEditeur     5   	   3Models clr-namespace:OptiEditeur.Models     5   	   1Viewsclr-namespace:OptiEditeur.Views     5   	   7Services"clr-namespace:OptiEditeur.Services     5	   	   \wpfLclr-namespace:Microsoft.Web.WebView2.Wpf;assembly=Microsoft.Web.WebView2.Wpf     5
   	     ? Title$  
MainWindow??5   	   $	??650??6   $
??1150??6,    J? CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           OptiEditeur.MainWindow   InsertMarkCommand   Command# Z 5      6x     5   
   -   5           InsertMarkLinkCommand# Z 5      6|     5   
   -   5           InsertMarkImageCommand# Z 5      6{    J? InputBindings 5      ?? 5   
    ?? Command# Z 6    ?? CommandParameter$ strong??6M   	 ?? Gesture$	 CTRL+B??6g   ?? 5   
   # Z 6   $ i??6M   $	 CTRL+I??6b   ?? 5   
   # Z 6   $ u??6M   $	 CTRL+U??6b   ?? 5   
   # Z 6   $ h1??6M   $	 CTRL+K??6c   ?? 5   
   # Z 6   $ h2??6M   $	 CTRL+L??6c   ?? 5   
   # Z 6   $ h3??6M   $	 CTRL+M??6c   
 ? 	Resources
 5      &    OptiEditeur.Services.TablesExist   TablesExist ?? 
TargetType ?? Property ?? Value ?? 	Resources??%6  &	        '??        5   
   6   ?? 5   
   " ??6   .?5      ??  ? ??
 ???? 6+   ?? 5       ? ??$ 5,3,5,3q?6'   ?? 5       ? ??$ 2q?6(    5       ???? 5!      '??       " ??6   .?5"      ??  ?@??CornerRadius$ 5??65   5#      5$      5%      5&      .??5'      ?  ? RowDefinitions 5(   
   ?? 5)      $??20 ?6   ?? 5*      .+?5-   
   o? 
?????? .@?5.      m? -   5         m?Name$ fileMenu??5.      $??Fichier??6)   .@?5/      m? -   5        $??Nouvelle documentation??5/      6;   m? 50      -   5        $??Ouvrir des documents...??50      6<   ?? 51      m? 52      -   5        $
 save??52      $??Sauvegarder??6)   {?6K   ?? *TableList.Count+ ?? 	Converter# [  m? 53      -   5        $ saveAs??53      $??Sauvegarder sous...??6,   {?6X   ?? *TableList.Count+# [  ?? 54      m? 55      -	   5        $??Quitter??55      6,   56      m? 57      -
   5        $ keyMenu??57      $??Clés??6(   {?66   ?? *TableList.Count+# [  .@?58      m? -   5        $ ??Ajouter une clé existante??58      6>   m? 59      -   5        $??Supprimer une clé??59      66   5:      m? 5;      -   5        $ helpMenu??5;      $?????6)   .@?5<      m? -   5        $
??Aide??5<      6)   5=      5>      ? 5A   
   $??1??6    ? ColumnDefinitions 5B      ?? 5C      $	??275 ?6#   ?? 5D      .+?5G      ?? -   5         ??Name$ 
tabControl??5G      $??5q?6\   
 5H      ??%S   '??       ?? 5I      " ??6   .?5J      ??  ? ??$	 130??62   5K      5L      .@?5M      ?? $??Sommaire??6   .??5N      &    OptiEditeur.Views.TablesTreeView  -   5          Name$ tablesTreeView??5N   +   5O      ?? 5P      $??	Recherche??6   .??5Q      &    OptiEditeur.Views.TablesGridView  -   5          Name$ tablesGridView??5Q   +    ? DataContext 6C   ??  ?? Path$ 
ListTables?5R      5S      ? 5V      $??1??6    5W      ?? 5X      $??40 ?6$   ?? 5Y      $??40 ?6$   ?? 5Z      ?? 5[      $??40 ?6$   .+?5_      ]? $??0??6   $??Center=?6)   $??10q?6D   .+?5`      ?? $??Nom de la table : ??6    $
??Left=?6:   ?? 5a      -   5         ??Name$ nameView??5a   %   $??Right=?67   
 5b       ?? DataType   OptiEditeur.Models.Tables ??Name??%s   (??       5c   +   *, +)?? 6   "  6+   .R?5d   "   ?? $ nameBox??6*   ??6;   ?? $
 Name?5e      5f      5g      5h      ?? 5k      -   5         ??Name$ buttonStack??5k      $??1??62    ?? Orientation$ 
Horizontal=?6?   $??5q?6X   .+?5l      ?? -   5         ??Name$ majorButton??5l      $??	Important??62   #??Z 6F   $??strong??6~   ?? 5m      -   5        $ commentButton??5m      $??Commentaire??64   #??Z 6J   $??i??6?   ?? 5n      -   5        $ detailsButton??5n      $??
Précision??64   #??Z 6H   $??u??6?   ?? 5o      -   5        $ 
linkButton??5o      $
??Lien??61   #??Z 6@   $??a??6|   ?? 5p      -   5        $ title1Button??5p      $??Titre 1??63   #??Z 6E   $??h1??6}   ?? 5q      -   5        $ title2Button??5q      $??Titre 2??63   #??Z 6E   $??h2??6}   ?? 5r      -   5        $ title3Button??5r      $??Titre 3??63   #??Z 6E   $??h3??6}   ?? 5s      -   5        $ imageButton??5s      $??Image??62   #??Z 6B   $	??img??6   5t      ? 5w      $??2??6    5x      ?? 5y      ?? 5z      .+?5}      ?? -   5        $ contentView??5}   %   $??0??6:   
 5~       ?? TextWrapping ?? AcceptsReturn  ?? UpdateSourceTrigger??%?   (??       5   +   *, +)?? 6   "  6+   .R?5?   "   ?? -   5        $
 Wrap=?5?   *   $
 True??6>   $??5q?5?   *   ??5?   *   ?? $ Content?$  PropertyChanged=?5?   *   5?      5?      5?      )  #Microsoft.Web.WebView2.Wpf.WebView2  5?      -   5        !  Name$! webView??5?   #   $??1??6j   5?      ?? 5?      -   5        $ saveContent??5?      $	??200??6.   $??3??6:   $??Right=?6G   $??Sauvegarder??6c   {?6?   ?? *TableList.Count+# [  5?      5?      5?      5?      