   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:VideoRentalSystemVideoRentalSystem  5   	   '    !VideoRentalSystem.RentVideoWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:VideoRentalSystem     5   	   -   5          �Name$  RentVideoWin��5   	    � Title$ 
Rent Video��5   	   $��279.999��6   $	��712��6-    � 
ResizeMode$ CanMinimize=�69   .��5	      � .+�5
   
   �� -   5         ��Name$ label��5
      $��Renting to:��6   $
��Left=�65   $��	10,10,0,0q�6P   $	��Top=�6c   $��99��6{   $��16(�6�   �� 5   
   -   5        $ CustomerName_label��5      $��[NAME]��6,   $
��Left=�6=   $��
100,10,0,0q�6X   $	��Top=�6l   $	��206��6�   $��16(�6�   $
��Bold"�6�   �� 5   
   -   5        $ label_Copy1��5      $��Video Renting:��6%   $
��Left=�6>   $��	10,52,0,0q�6Y   $	��Top=�6l   $	��113��6�   $��16(�6�   �� 5   
   -   5         ��Name$ VideoName_comboBox��5      $
��Left=�6/   $��	10,88,0,0q�6J   $	��Top=�6]   $	��238��6u   6�   �� 5   
   -   5         ��Name$ Renting_listView��5      $
��Left=�6-   $	��150��6H   $��
253,26,0,0q�6U   $	��Top=�6i   $	��441��6�    �� View 5      �� 5      .P�5      �� $��Title��6%    �� Width$	 210��64    �� DisplayMemberBinding 6@   �� *
VideoTitle+�� 5      $��Category��6%   $ 75��67    6B   �� *VideoCategory+�� 5      $��Due Date��6%   $ 75��67    6B   �� *	DateDue+�� 5      $��Price��6%   $ 75��64    6?   �� *
VideoPrice+5      5      �� 5   
   -   5        	 ��Name$	 
Add_button��5      $	��Add��6%   $
��Left=�63   $��
10,156,0,0q�6N   $	��Top=�6b   $	��238��6z   {�.  6�   �� 5   
   -   5        $ label1��5      $��Total:��6    $
��Left=�61   $��561.999,176,0,0q�6L   $	��Top=�6e   $��132.001��6}   �� 5   
   -	   5        $ total_Label��5      $��PHP 0.00��6%   $��Right=�68   $
��Left=�6[   $��598.333,173.843,0,0q�6v   $	��Top=�6�   $
��Bold"�6�   $��14(�6�   $��95.667��6�   $��26��6�   �� 5   
   -
   5        $	 Checkout_button��5      $��Checkout��6*   $
��Left=�6=   $��598.333,199.843,0,0q�6X   $	��Top=�6u   $��95.667��6�   $��31.333��6�   {�.  6�   �� 5   
   -   5        $	 Cancel_button��5      $��Cancel��6(   $
��Left=�69   $��497.666,199.843,0,0q�6T   $	��Top=�6q   $��95.667��6�   $��31.333��6�   6�   5      5      