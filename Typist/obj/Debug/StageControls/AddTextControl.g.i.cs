﻿#pragma checksum "..\..\..\StageControls\AddTextControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C0D1503400AE7A85B9137D048CFBF24B"
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


namespace Typist.StageControls {
    
    
    /// <summary>
    /// AddTextControl
    /// </summary>
    public partial class AddTextControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox GroupCB;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel LessonSP;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel OldLessonSP;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox LessonCB;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel NewLessonSP;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LessonNameTB;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel TextSP;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextTB;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\StageControls\AddTextControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ResultTB;
        
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
            System.Uri resourceLocater = new System.Uri("/Typist;component/stagecontrols/addtextcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\StageControls\AddTextControl.xaml"
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
            this.GroupCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 15 "..\..\..\StageControls\AddTextControl.xaml"
            this.GroupCB.DropDownClosed += new System.EventHandler(this.GroupSelectionDone);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LessonSP = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.OldLessonSP = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.LessonCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\..\StageControls\AddTextControl.xaml"
            this.LessonCB.DropDownClosed += new System.EventHandler(this.LessonSelectionDone);
            
            #line default
            #line hidden
            return;
            case 5:
            this.NewLessonSP = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.LessonNameTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 32 "..\..\..\StageControls\AddTextControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewLessonClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TextSP = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.TextTB = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\..\StageControls\AddTextControl.xaml"
            this.TextTB.KeyDown += new System.Windows.Input.KeyEventHandler(this.ClearResultTB);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 42 "..\..\..\StageControls\AddTextControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTextClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ResultTB = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
