﻿#pragma checksum "..\..\MainMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0DC6D25B7917910B48A8DF2AA8DABC2C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using View;


namespace View {
    
    
    /// <summary>
    /// MainMenu
    /// </summary>
    public partial class MainMenu : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exitButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button creditsButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button tutorialButton;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button optionsButton;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button multiplayerButton;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button singleplayerButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EvoCraft;component/mainmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainMenu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\MainMenu.xaml"
            ((View.MainMenu)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.exitButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\MainMenu.xaml"
            this.exitButton.Click += new System.Windows.RoutedEventHandler(this.exitButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.creditsButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\MainMenu.xaml"
            this.creditsButton.Click += new System.Windows.RoutedEventHandler(this.creditsButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tutorialButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\MainMenu.xaml"
            this.tutorialButton.Click += new System.Windows.RoutedEventHandler(this.tutorialButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.optionsButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\MainMenu.xaml"
            this.optionsButton.Click += new System.Windows.RoutedEventHandler(this.optionsButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.multiplayerButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\MainMenu.xaml"
            this.multiplayerButton.Click += new System.Windows.RoutedEventHandler(this.multiplayerButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.singleplayerButton = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\MainMenu.xaml"
            this.singleplayerButton.Click += new System.Windows.RoutedEventHandler(this.singleplayerButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

