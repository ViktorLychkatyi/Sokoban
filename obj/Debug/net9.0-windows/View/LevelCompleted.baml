   M S B A M L   `   `   `  ����B  >Sokoban, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Sokoban.ViewSokoban.View  5      !    Sokoban.View.LevelCompleted   5      W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Sokoban.View     5        N� Title$  LevelCompleted��5	       � 	Resources 5
         FocusVisual �� Property �� Value �� StrokeDashArray!  System.Windows.SystemColors   ControlTextBrushKey J� SnapsToDevicePixels  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonDesign �� 
TargetType  OnestMedium	 �� 
TargetType
 ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName   HighlightBrushKey��%�  &	        &	 �     &	 �     &	 �     &	     &	 9    &	 Z    &	 {    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7  7  7 	 �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$   $	 1 2R�6/   #���  6E   $
 true��6�   $��1��6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ #B115B4��67   �� 5   
   $ #FF95B3��6>   �� 5   
   $ #B115B4��6:   �� 5   
   $ #FF95B3��6<   �� 5   
   $ #B115B4��68   �� 5   
   $ #FF95B3��6=   �� 5   
   $ #B115B4��69   �� 5   
   $ #FF95B3��6=   �� 5   
   " ��6%   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6+   �� 5        � ��8  6,   �� 5!       � ��# �  6+   �� 5"       � ��$ 1q�60   �� 5#       � ��$ Center=�6;   �� 5$       � ��$ Center=�69   �� 5%       � ��$ 1q�6(   �� 5&       � ��8  6+   �� 5'       � �� 5(      �� 5)      "	 ��6&   .R�5*      �� $
 border��6!   $ 50��61   #��z��6C   #��z��6m   #��z��6�   $
 true��6�   ._�5+      �� $ contentPresenter��6/   $|�False��6I   #��z��6[   #��z��6�   $
��True��6�   # z 6�   #��z��6  5,       5-      P� 5.       �@��IsDefaulted$
 true��6=   . �5/   "   �� $ border��6@   # �  6T    � ��50      P� 51       �@J�IsMouseOver$
 true��6=   . �52   "   �� $ border��6?   8   � ��6S   �� 53   "   $ border��6@   8   � ��6T   54      P� 55       � ��$
 true��6;   . �56   "   �� $ border��6?   8   � ��6S   �� 57   "   $ border��6@   8   � ��6T   58      P� 59       � {�$ false��6;   . �5:   "   �� $ border��6?   8   � ��6S   �� 5;   "   $ border��6@   8 	  � ��6T   �� 5<   "   $ contentPresenter��6K   8 
  � ��6i   5=      5?      5A      5B      5C      .��5D      � ���#F1ED72.+�5E   
   �� $��Center=�6   $��Center=�60    � UseLayoutRounding$ False��6M   .+�5F      ��   OnestExtraBold#��[ 6   $��LEVEL COMPLETED!��6E   $��Center=�6]   $	��110(�6z   ���	#FFB115B4$	��178��5G   (   $
��Bold"�65   �� 5H      -   5        #��� 
 5H      $��RESTART��6<   $��86��6N   $	��362��6Z   $��0, 0, 0, 30q�6f   $��36(�6�   $
��Bold"�5I      ���	#FFB115B4���	#FFFFA7C0 � Cursor$
 Hand��5J   ?   �� 5K      -   5        #��� 
 5K      $
��MAIN��6<   $��86��6K   $	��362��6W   $��0, 0, 0, 30q�6c   $��36(�6�   $
��Bold"�5L      ���	#FFB115B4���	#FFFFA7C0$
 Hand��5M   ?   5N      5O      5P      