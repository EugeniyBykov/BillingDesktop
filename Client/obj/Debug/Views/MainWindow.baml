   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	   5clr-namespace:Client.ViewModelsClient.ViewModels  5	   	       Client.Views.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	    localclr-namespace:Client  5   	   (vmclr-namespace:Client.ViewModels  5	   	     � WindowStartupLocation$  CenterScreen=�5   	    � 
ResizeMode$ CanResizeWithGrip=�6.    � WindowStyle$
 None=�6M    � AllowsTransparency .  � Title$ 
MainWindow��5   	   $	��650��6   $
��1000��6)   .��5      ]� .+�5   
   ]� $
��Left=�6   .+�5      �� -   5         ��Name$ ListViewMenu��5      $	��Top=�6-   
���   �$��Disabled=�6U   $��16(�5       �� SelectedIndex$ 0��6"   
���  ��.@�5      �� -   5         ��Name$ clients��5      $��60��60   .��5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]	   Kind$	 
AccountBox=�62   $��25��6D   $��33��6P   $��10q�6[   �� 5      -   5        
 ��Name$
 t1��5   $   $��Клиенты��60   $��Center=�6?   $��20 10q�6Z   $y�	Collapsed=�6i   5      5      �� 5      -   5        $ projects��5      $��60��61   .��5      �� $ 
Horizontal=�6!   .+�5        $	 Pencil=�62   $��25��6@   $��33��6L   $��10q�6W   �� 5       -   5        $
 t2��5    $   $��Проекты��60   $��Center=�6?   $��20 10q�6Z   $y�	Collapsed=�6i   5!      5"      �� 5#      -   5        $ payment��5#      $��60��60   .��5$      �� $ 
Horizontal=�6!   .+�5%        $	 AccountBalanceWallet=�62   $��25��6N   $��33��6Z   $��10q�6e   �� 5&      -   5        $
 t3��5&   $   $��Оплаты��60   $��Center=�6>   $��20 10q�6Z   $y�	Collapsed=�6i   5'      5(      5*      5+      � 5-   
   -   5         �Name$ GridTop��5-      $��60��6    $	��Top=�6,   ��5.      �� 5/       �� Color$
 Blue��6"    �� Opacity$	 0.6Q�6/   .+�51      �� $ 
Horizontal=�6   $��Right=�62   .+�52      �� $��Меню��6   $��Center=�6(   $��16(�6C   '  !MaterialDesignThemes.Wpf.PopupBox  53         PlacementMode$ BottomAndAlignRightEdges=�6*      	StaysOpen .  $��10q�6e      PopupContent. 54      �� $	��150��6!   
�������.+�55      �� $��Настройки��6!   �� 56      $��Справка��6!   �� 57      �� 58      -	   5         ��Name$ hide_btn��58   4   $��Свернуть��6!   6F   �� 59      -
   5        $ 	close_btn��59   1   $��
Выход��6!   6D   5:      5;      5<      �� 5=      $��Center=�6   $��Center=�65   $��22(�6P   	Billing6g   5>      � 5@   
   -   5        $ GridMain��5@      ��5A      �� 5B      $ 	#FF5C99D6��6"   $	 0.7Q�64   5E      5G      5H      