   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:VideoRentalSystemVideoRentalSystem  5   	   &     VideoRentalSystem.AddVideoWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:VideoRentalSystem     5   	   -   5          �Name$  AddVideoWin��5   	    � Title$ 	Add Video��5   	   $��288.519��6   $��366.265��6,   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��94* �6   �� 5      $	��85* �6   .+�5   
   �� -   5         ��Name$ label��5      $��Video Title:��6   $
��Left=�66   $��	10,10,0,0q�6Q   $	��Top=�6d   $��20(�6|   �� 5   
   -   5        $ 
label_Copy��5      $��Video Type:��6$   $
��Left=�6:   $��	10,52,0,0q�6U   $	��Top=�6h   $��20(�6�   �� 5   
   -   5        $ label_Copy1��5      $��Stock:��6%   $
��Left=�66   $��
10,136,0,0q�6Q   $	��Top=�6e   $��20(�6}   �� 5   
   -   5         ��Name$ textBox_Title��5      $��Center=�6)   $
��Left=�6K   $��25.072��6f   $��127,16.508,0,0q�6v    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $��211.084��6�   $��16(�6�   $��2��6�   6�   �� 5   
   -   5         ��Name$ comboBox_Type��5      $��Center=�6*   $
��Left=�6L   $��
127,59,0,0q�6g   $	��Top=�6{   $��83��6�   $��27��6�    �� SelectedIndex$ -1��6�   $��2��6�   {�.  .@�5      �� -   5        $	��vcd��5      .��6&   VCD6+   �� 5      -   5        $	��dvd��5      .��6&   DVD6+   5      �� 5   
   -	   5        $ textBox_Stock��5      $��Center=�6)   $
��Left=�6K   $��26��6f   $��
77,142,0,0q�6r   $
 Wrap=�6�   $	��Top=�6�   $��45��6�   $��16(�6�   {�.  6�   �� 5   
   -
   5         ��Name$ 
btn_Submit��5      $��2��6%   $��Submit��69   $
��Left=�6J   $��
10,198,0,0q�6e   $	��Top=�6y   $	��338��6�   $��32��6�   $��20(�6�   {�.  6�   �� 5   
   -   5        $ label_Copy2��5      $��Rental Days:��6%   $
��Left=�6<   $��	10,94,0,0q�6W   $	��Top=�6j   $��20(�6�   �� 5   
   -   5        $ rental_ComboBox��5      $��Center=�6,   $
��Left=�6N   $��132,100,0,0q�6i   $	��Top=�6~   $��78��6�   $��27��6�   $ -1��6�   {�.  $��2��6  .@�5      �� -   5        	 ��Name$
	 day1��5      $��1��6)   �� 5      -   5        $
	 day2��5      $��2��6)   �� 5      -   5        $
	 day3��5      $��3��6)   5      5      5      