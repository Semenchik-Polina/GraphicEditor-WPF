   M S B A M L   `   `   `  ����H  DGraphicEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:GraphicEditorGraphicEditor  5   	       GraphicEditor.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:GraphicEditor     5   	   -   5          � Title$  Almost Paint��5   	   $	��800��6   $
��1300��6+   $	��550��6O   $	��800��6_   .��5
      �  � ColumnDefinitions 5      �� 5      $
��auto �6   6.   �� 5      6!   �� 5      $
��auto �6   6.   .+�5   
   �� -   5        $��ShapesPanel��5      $��0��6(   $
��Left=�68   $��	0,0,0,0.4q�6S   $	��140��6f   .+�5      �� -   5        $��BLine��5      .��66   Line6<   �� 5      -   5        $��BSquare��5      .��6:   Square6B   �� 5      -   5        $��
BRectangle��5      .��6@   	Rectangle6K   �� 5      -   5        $��BCircle��5      .��6:   Circle6B   �� 5      -   5        $��BEllipse��5      .��6<   	Ellipse6E   �� 5      -   5        $��	BTriangle��5      .��6>   Load Triangles6N   5      �� 5   
   $��0��6   $��Bottom=�6%   $��	0,0,0,0.4q�6@   $	��140��6S   .+�5      �� -	   5        $��BChange��5      .��69   Move6?   �� 5      -
   5        $��BSave��5      .��66   Save6<   �� 5      -   5        $��BLoad��5      .��66   Load6<   �� 5      -   5        $��BRemove��5      .��6:   Remove6B   �� 5      -   5        $��BClear��5      .��68   Clear6?   5      �� 5   
   $��2��6   $	��Top=�6%   $��	0,0,0,0.4q�6=   $	��140��6P   .+�5       �� -   5        $��ListBoxOfFigures��5       $��Visible=�6.   5!      5"      �� 5#   
   -   5         ��Name$ 
CanvasMain��5#   4   $��1��6   
�������}�. 6}   5%      5&      