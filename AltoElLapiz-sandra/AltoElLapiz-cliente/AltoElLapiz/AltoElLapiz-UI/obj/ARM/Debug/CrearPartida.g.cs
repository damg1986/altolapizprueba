﻿#pragma checksum "C:\Users\damg3\Desktop\AltoElLapiz-sandra\AltoElLapiz-cliente\AltoElLapiz\AltoElLapiz-UI\CrearPartida.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C8B02361D394CBD04ADA23FFCCEA86C6"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AltoElLapiz_UI
{
    partial class CrearPartida : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1: // CrearPartida.xaml line 11
                {
                    this.txtNombreGrupo = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 2: // CrearPartida.xaml line 12
                {
                    this.txtboxNombre = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3: // CrearPartida.xaml line 13
                {
                    this.boton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.boton).Click += this.btnJoin_Click;
                }
                break;
            case 4: // CrearPartida.xaml line 14
                {
                    this.atras = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.atras).Click += this.atras_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.16.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

