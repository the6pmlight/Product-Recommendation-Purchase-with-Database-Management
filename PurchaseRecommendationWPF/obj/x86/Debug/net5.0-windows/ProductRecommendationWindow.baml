   M S B A M L   `   `   `  ����T  PPurchaseRecommendationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:PurchaseRecommendationWPFPurchaseRecommendationWPF  5   	   ;    5PurchaseRecommendationWPF.ProductRecommendationWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:PurchaseRecommendationWPF     5   	     � Title$!  ProductRecommendationWindow��5   	   $	��450��6-   $	��800��6:   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��129 �6   �� 5      $��31 �6   �� 5      $
��103* �6   �� 5      $	��57* �6   .+�5   
   �� -   5         ��Name$ lvProductRecommendation��5       J� OpacityMask
 �����$��10,36,10,10q�6H   $��4��6]    �� View 5      �� 5      .P�5      �� $��Customer ID��6%    �� Width$	 100��6:    �� DisplayMemberBinding 6F   �� *
CustomerID+�� 5      $��Contact Name��6%   $	 150��6;    6G   �� *ContactName+�� 5      $��
Product ID��6%   $	 100��69    6E   �� *	ProductID+�� 5      $��Product Name��6%   $	 150��6;    6G   �� *ProductName+�� 5      $��Score��6%   $ 50��64    6?   �� *Score+�� 5      $��Decision��6%   $ 60��67    6B   �� *
Decision+5      5      �� 5   
   $��Selected Model:��6   $��Center=�6*   $��0,5,0,0q�6G   $	��Top=�6X   $��26��6p   $��94��6|   �� 5   
   -   5         ��Name$ txtSelectedModel��5      $��21��6,   $��	0,7,120,0q�68    �� TextWrapping$
 Wrap=�5       $	��Top=�6&   $��1��6>   $��2��6N   �� 5!   
   -   5        	 ��Name$	 btnPickModel��5!      $��
Pick Model��6'   $��2��6<   $
��Left=�6L   $��	321,7,0,0q�6g   $	��Top=�6z   $	��104��6�   ���	#FFEBEBE6
����  �$��2��6�   6�   5#      5$      