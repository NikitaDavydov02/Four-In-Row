   M S B A M L   `   `   `  ????E  AFourInRow2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:FourInRow2.ViewFourInRow2.View  5          FourInRow2.View.StartPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:FourInRow2.View     5        N? Title$  	StartPage??5	       ? 	Resources 5      ??%    5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $	??140 ?6   ?? 5      $??* ?6   .+?5   
   ?  ? ColumnDefinitions 5      ?? 5      $	??120 ?6#   ?? 5      $??* ?6#   .+?5      ?? -   5         ??Name$ 
backButton??5      $??
39,59,39,0q?6)   $	??Top=?5!      4  .System.Windows.Automation.AutomationProperties   Name$
 Back??5"         AutomationId$ 
BackButton??5#         ItemType$ Navigation Button??5$      ??5   =   ?? * NavigationHelper.GoBackCommand+ ?? ElementName$ pageRoot??5$      ?? 5%      -   5        	 ??Name$	 	pageTitle??5%      $??Four In Row??6+   $??1??6>   
 J? IsHitTestVisible$
 false??5&      $??NoWrap=?62   $??Bottom=?6H   $??	0,0,30,40q?6c   5'      ?? 5(   
   $??Center=?6   $??Center=?62   $??1??6M   
???????$	??300??6m   $	??400??6y   .+?5)      ?? -   5        $ 
twoPlayers??5)      $??Center=?6)   $??
0,100,0,20q?6F   $	??200??6Z   
??? ???.??6?   Two players6?   ?? 5*      -   5        $ playWithComputerButtom??5*      $??Center=?66   $	??200??6S   
??? ???.??6?   Play with computer6?   5,      5-      5.      