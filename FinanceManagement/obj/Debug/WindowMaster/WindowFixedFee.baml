   M S B A M L   `   `   `  ����  FinanceManagement3    -FinanceManagement.WindowMaster.WindowFixedFee   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          � Title$  WindowFixedFee��5   	   $	��350��6    $	��600��6-    � 	Resources 5       �� 
TargetType �� Property �� Value(  "System.Windows.Controls.DatePicker   resizedCalendarItem&   System.Windows.Controls.Calendar �� 
TargetType5  /System.Windows.Controls.Primitives.CalendarItem  Name��%�  '��       '�� H     '  �     '�� �     '��      &	  h    �� 5	   
   " ��6   .�5
      ��  � ��$ 24(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 24(�6)   5      �� 5   
   "  6   .�5      ��  � ��$ 24(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 24(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 24(�6)   5      �� 5   
   "  6,   .�5      ��  � �� 5      �� 5      "  6&   .R�5      (� $	��350��6"   $	��350��5      .��5        $ PART_CalendarItem��6+   #��z��5   !   #��z��6U   #��z��5    !   5!      5"      5$      5%      5&      .��5'      �  � ColumnDefinitions 5(   
   �� 5)      $��1* �6   �� 5*      $��2* �6    � RowDefinitions 5,   
   �� 5-      $��1* �6   �� 5.      $��1* �6   �� 5/      $��1* �6   �� 50      $��1* �6   �� 51      $
��1.5* �6   .+�54   
   �� -   5        	 ��Name$	 txtItem��54      $��1��6%   $��0��65   $��Center=�6B   $��Center=�6]   �� 56   
   -   5        
 ��Name$
 cbxIsFixedFee��56      $��0��6*   $��0��6:   $��Center=�6G   $��	固定費��6i   $��10,0,0,0q�6w   $��Center=�6�    d� IsThreeState .   d� 	IsChecked$
 TrueY�6�   $��15(�6�    � LayoutTransform 57      �� 58       �� ScaleX$	 1.5Q�6!    �� ScaleY$	 1.5Q�6.   5:      �� 5<   
   $��0��6   $��1��6$   $��Center=�61   $��Center=�6L   $��始期��6i   $��5q�6s   �� 5>   
   $��0��6   $��2��6$   $��Center=�61   $��Center=�6L   $��終期��6i   $��5q�6s   �� 5@   
   $��0��6   $��3��6$   $��Center=�61   $��Center=�6L   $��金額��6i   $��5q�6s     5B   
   -   5        $��dateFrom��5B      $��1��6%   $��1��65   $��Center=�6B   $��5q�6d      CalendarStyle# [  6o     5E   
   -   5        $��dateTo��5E      $��1��6#   $��2��63   $��Center=�6@   $��5q�6b   # [  6m   �� 5G   
   -   5        $��txtAmout��5G      $��1��6"   $��3��62   $��Center=�6?   $��5q�6a   6l   �� 5I   
   -   5        $��0��5I      $��4��6!   $��2��6.   $��Center=�6B   $��保存��6d   $��5q�6q   6|   5K      5L      