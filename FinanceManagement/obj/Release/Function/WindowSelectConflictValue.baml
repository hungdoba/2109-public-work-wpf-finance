   M S B A M L   `   `   `  ����   FinanceManagement:    4FinanceManagement.Function.WindowSelectConflictValue   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � WindowState$  	Maximized=� � Title$ WindowSelectConflictValue��$
��1080��$
��1920�� � 	Resources    datagridMouseHandler �� 
TargetType)  #System.Windows.Controls.DataGridRow  SelectDatagrid*  $System.Windows.Controls.DataGridCell �� Property �� Value �� 
TargetType J� SnapsToDevicePixels �� Triggers	 P� Property
 P� Value&   System.Windows.Controls.DataGrid   FocusBorderBrushKey ]� 
Conditions �� Property �� Value��%1  &	        &	      �� "  .�-   �� "  .���  � ��
 ���� ��  � ��
 ���� ��  � ��$ 1q���  � �� �� "  .R��� #��z��#��z��#��z�� . ._��� # z  P� 	 �@ 
IsSelected
 . . ���  � ��
 ��  ���  � ��
 �������  � ��# � ��P� 	 �@J�IsKeyboardFocusWithin
 . . ���  � ��# �  ]�  ��  �@ 
IsSelected . ��  �@��IsSelectionActive .  ./���  � ��
 ��  ���  � ��
 �������  � ��# � /�P� 	 � {�
 .  . ���  � ��# � ��.��(� .��� $
��1050��$
��1920�� � ColumnDefinitions �� $��3* ��� $
��Auto ��� $��3* ��� $
��Auto ��� $��1* � � RowDefinitions �� $��1* ��� $	��10* �.+��� $��1��$��Center=�$��Stretch=�$��2��
���   ��� $��3��$��Center=�$��Stretch=�$��2��
���   ��� $��0��$��0��$��Center=�$��Center=�$��新入データ���� $��0��$��2��$��Center=�$��Center=�$!��データベースデータ��  -     Name$ gridNewImport��$��1��$��0��$��5q�   	CellStyle# �     RowStyle# �      CanUserAddRows .    -   $ gridOldData��$��1��$��2��$��5q�# �  # �    .  � $��1��$��4�� �� $	��10* ��� $��1* ��� $��1* �.+��� -    ��Name$ 
btnConfirm��$��1��$��5q�$��確認���� -   $ 	btnCancel��$��2��$��5q�$��キャンセル��