﻿#pragma checksum "..\..\..\Items\wndItems.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C33029104CA022C376792AAF247C10181CA045A839881BA87C97FBC542683973"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Group_Project_Prototype.Items;
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


namespace Group_Project_Prototype.Items {
    
    
    /// <summary>
    /// wndItems
    /// </summary>
    public partial class wndItems : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbItems;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSelect;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox gbEdit;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCost;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCost;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblDesc;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDesc;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddItem;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMain;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Items\wndItems.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblError;
        
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
            System.Uri resourceLocater = new System.Uri("/Group Project Prototype;component/items/wnditems.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Items\wndItems.xaml"
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
            this.cbItems = ((System.Windows.Controls.ComboBox)(target));
            
            #line 10 "..\..\..\Items\wndItems.xaml"
            this.cbItems.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbItems_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lblSelect = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Items\wndItems.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.gbEdit = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 5:
            this.lblCost = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.tbCost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.lblDesc = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.tbDesc = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Items\wndItems.xaml"
            this.btnEdit.Click += new System.Windows.RoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnAddItem = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Items\wndItems.xaml"
            this.btnAddItem.Click += new System.Windows.RoutedEventHandler(this.btnAddItem_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnMain = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Items\wndItems.xaml"
            this.btnMain.Click += new System.Windows.RoutedEventHandler(this.btnMain_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.lblError = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

