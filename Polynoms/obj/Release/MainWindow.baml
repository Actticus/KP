   M S B A M L   `   `   `  ���� C  ?Polynoms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PolynomsPolynoms      Polynoms.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     LmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     (localclr-namespace:Polynoms     $	��300��$	��850��  � WindowStyle$
  None=� � WindowStartupLocation$ Manual=� � 
ResizeMode$ NoResize=� � 	Resources    	closeIcon  
turnupIcon  turndownIcon 
 Roboto �� Property �� Value  DefaultButtonStyle �� 
TargetType �� 
TargetType ��Name	 �� CornerRadius
 J� SnapsToDevicePixels ��Name  DefaultTextBoxStyle-  'System.Windows.Input.KeyboardNavigation �� Padding'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty ��Name��%,  &	        &	      &	 7     &	 U     &	 �     &	     �� $��icons/close.png���� $��icons/turnup.png���� $��icons/turndown.png���� .���  � ��$! /fonts;Component/#Roboto Bk)��� " ��.���  � ��$! /fonts;Component/#Roboto Bk)���  � �� Z� ��  � �� Z� ��  � ��# � ����  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R��� $ border��$	 5��#��z��#��z��#��z��
 . ._��� $ contentPresenter��#��z��#��z��#��z��|�.  ��. #
 z
 �� " ��.���  � ��$! /fonts;Component/#Roboto Bk)���  � ��$ 20(���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  �@ TabNavigation$
 None=���  �@�FocusVisualStyle Z� ��  � ��$	 120����  � ��$ 20����  �@J�	AllowDrop . ��  � �� �� " ��.R��� $��Border��$	 2��$ 2q�$��1q����#AAAAAA��Z�    CommonStates��.   
Normal��  Disabled��. �� .���� $ Border��$E ?(Panel.Background).                     (SolidColorBrush.Color)�  ReadOnly��. �� .���� $ Border��$E ?(Panel.Background).                     (SolidColorBrush.Color)�  	MouseOver��._��� $ PART_ContentHost��$��0q�.���� -   
�������.+�� $��40�� � ColumnDefinitions �� $
��135* ��� $
��182* �.+���  �� Orientation$ 
Horizontal=�$��Right=�$��
0,0,10.4,0q�$��1��.+��� -   #��[ #��[ $��Center=�$��24��$��24���� #��[ #��[ $��Center=�$��24��$��24���� -   #��[ #��[  $��Center=�$��24��$��24��� $��50��.+��� $ 
Horizontal=�$	��Top=�$
��10 0q�.+��� -    ��Name$ Button0��#��[ 
 J� Uid$ 0��$	��184��$��48��$'��!Значение полинома��$��16(����#074EE8�� -   $ Button1��#��[ $ 1��$	��169��$��48��$$��Полином в строку��$��16(�
���   ��� -   $ Button2��#��[ $ 2��$	��169��$��48��$$��Строку в полином��$��16(�
���   ��� -   $ Button3��#��[ $ 3��$	��273��$��48��$9��3Дифференцирование полинома��$��16(�
���   �� -    �Name$ 
GridCursor��$	��184��$��5�����#074EE8$��10, 0, 0, 0q�$
��Left=�� -	   $ Grid0��$y�Visible=�.+��� .+�� $��21��$��0 15 0 0q�.+��� $��10 0 0 0q�$
��Left=�$��Полином��$	��400��#��[ ���#767676$	��Top=���Z� �� $��	450 0 0 0q�$
��Left=�$	��225��$'��!Значение полинома��#��[ ���#767676$	��Top=���Z� � $��40��.+��� -
    ��Name$ CalcPolStringTextBox��#��[ $
��15 0q�$
��Left=�$��30��$��Center=�$	��400��$��2q����#AAAAAA��Z� �� -   $ CalcPolValueTextBox��#��[ $��	455 0 0 0q�$
��Left=�$��30��$��Center=�$	��225��$��2q����#AAAAAA��Z� �� -   #��[ $��Right=�$��0 0 30 0q�$��16(�$��2q����#074EE8���#074EE8$	��116��$��Вычислить��� -   $ Grid1��$y�	Collapsed=�.+��� .+�� $��21��$��0 15 0 0q�.+��� $��10 0 0 0q�$
��Left=�$��Полином��$	��400��#��[ ���#767676$	��Top=���Z� � $��40��.+��� -   $ PolToStrTextBox��#��[ $
��15 0q�$
��Left=�$��30��$��Center=�$	��625��$��2q����#AAAAAA��Z� �� -   #��[ $��Right=�$��0 0 30 0q�$��16(�$��2q����#074EE8���#074EE8$	��148��$ ��Преобразовать��� -   $ Grid2��$y�	Collapsed=�.+��� .+�� $��21��$��0 15 0 0q�.+��� $��10 0 0 0q�$
��Left=�$��Строка��$	��400��#��[ ���#767676$	��Top=���Z� � $��40��.+��� -   $ StrToPolTextBox��#��[ $
��15 0q�$
��Left=�$��30��$��Center=�$	��625��$��2q����#AAAAAA��Z� �� -   #��[ $��Right=�$��0 0 30 0q�$��16(�$��2q����#074EE8���#074EE8$	��148��$ ��Преобразовать��� -   $ Grid3��$y�	Collapsed=�.+��� .+�� $��21��$��0 15 0 0q�.+��� $��10 0 0 0q�$
��Left=�$��Строка��$	��400��#��[ ���#767676$	��Top=���Z� � $��40��.+��� -   $ DiffPolTextBox��#��[ $
��15 0q�$
��Left=�$��30��$��Center=�$	��575��$��2q����#AAAAAA��Z� �� -   #��[ $��Right=�$��0 0 30 0q�$��16(�$��2q����#074EE8���#074EE8$	��198��$&�� Дифференцировать��� $��30��$��0 30 0 0q�.+��� -    ��Name$ AnswerLabel��$��10 0 0 0q�$��18(�$
��Left=�$	��800��#��[ ���#111111$�� ��$	��Top=���Z� 