   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Client.ViewsClient.Views  5   	   5clr-namespace:Client.ViewModelsClient.ViewModels  5   	   "    Client.Views.AddClientWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Client.Views     5   	   .vmclr-namespace:Client.ViewModels     5   	     � Title$  AddClientWindow��5	   	   $	��460��6!   $	��350��6.   .��5      ]� .+�5   
   �� $��ФИО��6   $	��Top=�6   $��10q�64   $��Center=�6@   �� 5   
   $	��Top=�6   $��5q�6'   $��Center=�62   $	��250��6O   ��6[   �� *Fio+�� 5   
   $��Email��6   $	��Top=�6!   $��5q�66   $��Center=�6A   �� 5   
   $	��Top=�6   $��5q�6'   $��Center=�62   $	��250��6O   ��6[   �� *Email+�� 5   
   $��Телефон��6   $	��Top=�6#   $��5q�68   $��Center=�6C   �� 5   
   $	��Top=�6   $��5q�6'   $��Center=�62   $	��250��6O   ��6[   �� *Phone+�� 5   
   $��Проект��6   $	��Top=�6"   $��5q�67   $��Center=�6B   �� 5   
   $	��Top=�6   $��5q�6(   $��Center=�63   $	��250��6P    �� DisplayMemberPath$
 Name��5   :   ��6   ��  �� Path$ ProjectList� �� SelectedItem 5      �� $ SelectedProject��� 5   
   $��Комментарий��6   $	��Top=�6'   $��5q�6<   $��Center=�6G   �� 5   
   $	��Top=�6   $	��250��6'    J� 	AllowDrop .  �� AcceptsReturn . $��Visible=�6Y   $��70��5      ��6   �� *ClientComment+]� 5   
   $��Bottom=�6   .+�5      �� -   5        $
��Left=�5      $��Добавить��6+   $	��120��6>   $��30��6J   $��40, 10, 10, 10q�6V   ��5       �� $ 	AddClient�69   �� 5!      -   5        $��Right=�5!      $��Отмена��60   $	��120��6A   $��30��6M   $��10, 10, 40, 10q�6Y   6q   �� 5"      $�� ��6   5#      5'      5)      