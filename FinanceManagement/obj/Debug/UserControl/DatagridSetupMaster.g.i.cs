﻿#pragma checksum "..\..\..\UserControl\DatagridSetupMaster.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "04FF672763F205DF179A5E7B2B81A348BA41A8F568461BC6812C646EF58E55A5"
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


namespace FinanceManagement.UserControl {
    
    
    /// <summary>
    /// DatagridSetupMaster
    /// </summary>
    public partial class DatagridSetupMaster : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\UserControl\DatagridSetupMaster.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal FinanceManagement.UserControl.DatagridSetupMaster UserControlDataGridSetupMaster;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\UserControl\DatagridSetupMaster.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid gridMaster;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\UserControl\DatagridSetupMaster.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup popupDragDrop;
        
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
            System.Uri resourceLocater = new System.Uri("/FinanceManagement;component/usercontrol/datagridsetupmaster.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControl\DatagridSetupMaster.xaml"
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
            this.UserControlDataGridSetupMaster = ((FinanceManagement.UserControl.DatagridSetupMaster)(target));
            return;
            case 2:
            this.gridMaster = ((System.Windows.Controls.DataGrid)(target));
            
            #line 36 "..\..\..\UserControl\DatagridSetupMaster.xaml"
            this.gridMaster.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.gridCustomerNameMasterUsed_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\UserControl\DatagridSetupMaster.xaml"
            this.gridMaster.MouseMove += new System.Windows.Input.MouseEventHandler(this.gridMaster_MouseMove);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\UserControl\DatagridSetupMaster.xaml"
            this.gridMaster.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.gridMaster_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\UserControl\DatagridSetupMaster.xaml"
            this.gridMaster.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.gridMaster_AutoGeneratingColumn);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\UserControl\DatagridSetupMaster.xaml"
            this.gridMaster.AutoGeneratedColumns += new System.EventHandler(this.gridMaster_AutoGeneratedColumns);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\UserControl\DatagridSetupMaster.xaml"
            this.gridMaster.Sorting += new System.Windows.Controls.DataGridSortingEventHandler(this.gridMaster_Sorting);
            
            #line default
            #line hidden
            return;
            case 3:
            this.popupDragDrop = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
