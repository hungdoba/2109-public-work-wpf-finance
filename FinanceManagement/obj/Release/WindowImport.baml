   M S B A M L   `   `   `  ����   FinanceManagement$    FinanceManagement.WindowImport   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     � WindowState$  	Maximized=� � Title$ WindowImport��$
��1080��$
��1920�� � 	Resources  �� 
TargetType �� Property �� Value��%-   '�       �� " �.���  � ��$ 18(�.��(� .��� $
��1050��$
��1920�� � ColumnDefinitions �� $��3* ��� $
��Auto ��� $��1* �.+��� $��1��$��Center=�$��Stretch=�$��2��
���   ��� $��0��.@��� -   $��	tabHQSale��$��本社売上��.��&   System.Windows.Controls.DataGrid  -     Name$ gridInputData��$��0��   
IsReadOnly .  	   AutoGenerateColumns	 . 
   CanUserAddRows
 .  � $��2�� �� $��1* ��� $��2* � � RowDefinitions �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $	��10* �.+��� $��0��$��0��$��2��$��インポート設定��$��Center=�$��Center=�$
��Bold"��� $��1��$��0��$��月度��$��20,0,0,0q�$��Center=�$
��Bold"��� $��2��$��0��$��部門��$��20,0,0,0q�$��Center=�$
��Bold"��� $��3��$��0��$��費タイプ��$��20,0,0,0q�$��Center=�$
��Bold"��� -   $��	txtNotice��$��6��$��2��$��1��$o��iデータをまだ保存しない、「保存」ボタンをクリックりて保存してください。��$��20,0,0,0q�$��WrapWithOverflow=�$
��Bold"�
���  ��$y�Hidden=��� -   $��cbMonth��$��1��$��1��$��5q� �� ItemStringFormat$ {0:MM/yyyy}��$��Center=�$��Center=��� -   $��cbDepartment��$��2��$��1��$��Center=�$��Center=�$��5q� �� SelectedIndex$ 0��.@��� $��本社��
 � Tag$ HeadQuarter���� $��太田工場��$	 Ota���� $��デザインセンター��$	 SDC���� -   $��
cbCostType��$��3��$��1��$��Center=�$��Center=�$��5q�$ 0��.@��� $��売上��$ Sales���� $��費用��$
 Cost���� -    ��Name$ 	btnImport��$��4��$��1��$��5q�$��登録���� -	   $ btnSave��$��5��$��1��$��5q�$��保存��