   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:VideoRentalSystemVideoRentalSystem  5   	   )    #VideoRentalSystem.ReturnVideoWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:VideoRentalSystem     5   	     � Title$  Return Video��5   	   $��601.333��6   $
��1012��6/   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��0* �6   �� 5       � RowDefinitions 5   
   �� 5      �� 5      $��0* �6   .+�5   
   �� -   5         ��Name$ label��5      $��Returning from:��6   $
��Left=�69   $��	10,10,0,0q�6T   $	��Top=�6g   $��18(�6   $��34��6�   $	��134��6�   $��2��6�   �� 5   
   -   5        $ custName_label��5      $
��NAME��6(   $
��Left=�67   $��
138,10,0,0q�6R   $	��Top=�6f   $��18(�6~   $	��349��6�   $
��Bold"�6�   $��34��6�   $��2��6�   �� 5   
   $
��Left=�6   $	��100��6/   $��10,0,0,0q�6<   $	��Top=�6N   $	��972��6f   $��2��6r   �� 5   
   -   5        $ label1��5      $��Rented Videos:��6    $
��Left=�69   $��	10,49,0,0q�6T   $	��Top=�6g   $��26��6   $��89��6�   $��2��6�   �� 5   
   -   5         ��Name$ rentedVideos_listView��5      $
��Left=�62   $	��299��6M   $��	10,75,0,0q�6Z   $	��Top=�6m   $	��450��6�   $��2��6�    �� View 5      �� 5      .P�5      �� $��Title��6%    �� Width$	 135��64    �� DisplayMemberBinding 6@   �� *
VideoTitle+�� 5      $��Category��6%   $ 60��67    6B   �� *VideoCategory+�� 5      $��Date Rented��6%   $ 85��6:    6E   �� *
DateRented+�� 5      $��Due Date��6%   $ 85��67    6B   �� *	DateDue+�� 5      $��Days Overdue��6%   $ 80��6;    6F   �� *DaysOverdue+5      5       �� 5!   
   $
��Left=�6   $	��100��6/   $��
432,64,0,0q�6<   $	��Top=�6P   $	��126��6h    J� RenderTransformOrigin$ 0.5,0.5&�6t   $��2��6�   z�5"      V� 5#      .~�5$      �� �� 5%      �� 5&      	 �� Angle$	 90Q�6&   T� 5'      5(      5*      �� 5+   
   $
��Left=�6   $	��100��6/   $��441,269,0,0q�6<   $	��Top=�6Q   $	��108��6i   $ 0.5,0.5&�6u   $��2��6�   z�5,      V� 5-      .~�5.      �� �� 5/      �� 50      $	 90Q�6&   T� 51      52      54      �� 55   
   -   5        
 ��Name$
 ReturnFrom_button��55      $��>��6,   $��Center=�68   $
��Left=�6Z   $��475,192,0,0q�6u   $	��Top=�6�   $��41��6�   $��54��6�   $��22(�6�   {�.  $��2��6�   �� 56   
   -   5        $ returningVideos_listView��56      $
��Left=�65   $	��299��6P   $��
532,75,0,0q�6]   $	��Top=�6q   $	��450��6�   $��2��6�    57      �� 58      .P�59      �� $��Title��6%   $	 140��64    6@   �� *
VideoTitle+�� 5:      $��Category��6%   $ 60��67    6B   �� *VideoCategory+�� 5;      $��Days Overdue��6%   $ 85��6;    6F   �� *DaysOverdue+�� 5<      $��Rental Price��6%   $ 80��6;    6F   �� *
VideoPrice+�� 5=      $��Overdue Fee��6%   $ 80��6:    6E   �� *
OverdueFee+5>      5@      �� 5A   
   -   5        $ label1_Copy��5A      $��Returning Videos:��6%   $
��Left=�6A   $��
532,49,0,0q�6\   $	��Top=�6p   $��26��6�   $	��103��6�   $��2��6�   �� 5B   
   $
��Left=�6   $	��100��6/   $��
10,446,0,0q�6<   $	��Top=�6P   $	��972��6h   $��2��6t   �� 5C   
   -   5        $ label2��5C      $��Total:��6    $��Right=�61   $
��Left=�6T   $��696,448,0,0q�6o   $	��Top=�6�   $��20(�6�   $��37��6�   $��56��6�   $��2��6�   �� 5D   
   -	   5        $ total_label��5D      $��PHP 0.00��6%   $��Right=�68   $
��Left=�6[   $��752,446,0,0q�6v   $	��Top=�6�   $��22(�6�   $	��230��6�   $
��Bold"�6�   $��37��6�   $��2��6�   �� 5E   
   -
   5        $
 Confirm_btn��5E      $��Confirm��6&   $��Center=�68   $
��Left=�6Z   $��876,509,0,0q�6u   $	��Top=�6�   $	��106��6�   $��40��6�   $��14(�6�   $��2��6�   {�.  6�   �� 5F   
   -   5        $
 
Cancel_btn��5F      $��Cancel��6%   $��Center=�66   $
��Left=�6X   $��765,509,0,0q�6s   $	��Top=�6�   $	��106��6�   $��40��6�   $��14(�6�   $��2��6�   �� 5G   
   -   5        $ label2_Copy��5G      $��Overdue fees:��6%   $
��Left=�6=   $��621,411,0,0q�6X   $	��Top=�6m   $��20(�6�   $��37��6�   $	��131��6�   $��2��6�   �� 5H   
   -   5        $ overdueFees_label��5H      $��PHP 0.00��6+   $��Right=�6>   $
��Left=�6a   $��752,417,0,0q�6|   $	��Top=�6�   $��14(�6�   $	��230��6�   $��29��6�   $��2��6�   �� 5I   
   -   5        $ label2_Copy1��5I      $��Rental fees:��6&   $��Right=�6=   $
��Left=�6`   $��621,374,0,0q�6{   $	��Top=�6�   $��20(�6�   $	��131��6�   $��37��6�   $��2��6�   �� 5J   
   -   5        $ rentalFees_label��5J      $��PHP 0.00��6*   $��Right=�6=   $
��Left=�6`   $��752,381,0,0q�6{   $	��Top=�6�   $��16(�6�   $	��230��6�   $
��Bold"�6�   $��31��6�   $��2��6�   5L      5M      