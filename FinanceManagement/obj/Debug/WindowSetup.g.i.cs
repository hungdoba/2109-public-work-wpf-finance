﻿#pragma checksum "..\..\WindowSetup.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2444F9159502D639A07F34B16713A6D86B8778548277378A1D7E934D951AA3DB"
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


namespace FinanceManagement.WindowReport {
    
    
    /// <summary>
    /// WindowSetup
    /// </summary>
    public partial class WindowSetup : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 33 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition colControl;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCompany;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabControl;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabFeeStruct;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridFeeStruct;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabFeeType;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridFeeType;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabFeeField;
        
        #line default
        #line hidden
        
        
        #line 173 "..\..\WindowSetup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridFeeField;
        
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
            System.Uri resourceLocater = new System.Uri("/FinanceManagement;component/windowsetup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\WindowSetup.xaml"
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
            
            #line 6 "..\..\WindowSetup.xaml"
            ((FinanceManagement.WindowReport.WindowSetup)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.colControl = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 3:
            
            #line 38 "..\..\WindowSetup.xaml"
            ((System.Windows.Controls.GridSplitter)(target)).MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.GridSplitter_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cbCompany = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\WindowSetup.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\WindowSetup.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tabControl = ((System.Windows.Controls.TabControl)(target));
            
            #line 89 "..\..\WindowSetup.xaml"
            this.tabControl.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.tabControl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.tabFeeStruct = ((System.Windows.Controls.TabItem)(target));
            return;
            case 9:
            this.gridFeeStruct = ((System.Windows.Controls.DataGrid)(target));
            
            #line 91 "..\..\WindowSetup.xaml"
            this.gridFeeStruct.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.gridFeeStruct_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 91 "..\..\WindowSetup.xaml"
            this.gridFeeStruct.BeginningEdit += new System.EventHandler<System.Windows.Controls.DataGridBeginningEditEventArgs>(this.gridFeeStruct_BeginningEdit);
            
            #line default
            #line hidden
            
            #line 91 "..\..\WindowSetup.xaml"
            this.gridFeeStruct.AddHandler(System.Windows.Input.CommandManager.PreviewCanExecuteEvent, new System.Windows.Input.CanExecuteRoutedEventHandler(this.gridFeeStruct_PreviewCanExecute));
            
            #line default
            #line hidden
            return;
            case 12:
            this.tabFeeType = ((System.Windows.Controls.TabItem)(target));
            return;
            case 13:
            this.gridFeeType = ((System.Windows.Controls.DataGrid)(target));
            
            #line 141 "..\..\WindowSetup.xaml"
            this.gridFeeType.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.gridFeeType_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 141 "..\..\WindowSetup.xaml"
            this.gridFeeType.AddHandler(System.Windows.Input.CommandManager.PreviewCanExecuteEvent, new System.Windows.Input.CanExecuteRoutedEventHandler(this.gridFeeType_PreviewCanExecute));
            
            #line default
            #line hidden
            
            #line 141 "..\..\WindowSetup.xaml"
            this.gridFeeType.BeginningEdit += new System.EventHandler<System.Windows.Controls.DataGridBeginningEditEventArgs>(this.gridFeeType_BeginningEdit);
            
            #line default
            #line hidden
            return;
            case 15:
            this.tabFeeField = ((System.Windows.Controls.TabItem)(target));
            return;
            case 16:
            this.gridFeeField = ((System.Windows.Controls.DataGrid)(target));
            
            #line 173 "..\..\WindowSetup.xaml"
            this.gridFeeField.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.gridFeeField_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 173 "..\..\WindowSetup.xaml"
            this.gridFeeField.AddHandler(System.Windows.Input.CommandManager.PreviewCanExecuteEvent, new System.Windows.Input.CanExecuteRoutedEventHandler(this.gridFeeField_PreviewCanExecute));
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 10:
            
            #line 118 "..\..\WindowSetup.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnFeeSetup_Click);
            
            #line default
            #line hidden
            break;
            case 11:
            
            #line 131 "..\..\WindowSetup.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnDeleteRow_Click);
            
            #line default
            #line hidden
            break;
            case 14:
            
            #line 163 "..\..\WindowSetup.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnDeleteFeeTypeRow_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
