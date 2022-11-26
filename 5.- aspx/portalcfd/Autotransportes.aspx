<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeFile="Autotransportes.aspx.vb" Inherits="portalcfd_Autotransportes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4 {
            height: 17px;
        }

        .style5 {
            height: 14px;
        }

        .style6 {
            height: 21px;
        }
        .title {
            width: 100%; 
            border-top: 0px solid; 
            color: #faf9f9; 
            text-align: center; 
            background-color: grey;                         
            padding: 5px 0px;
            font-size: 9pt; 
            font-family: Verdana;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                 <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblUsersListLegend" runat="server" Font-Bold="true" CssClass="item">Listado de Autotransportes</asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="GridList" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" PageSize="15" ShowStatusBar="True"
                            Skin="Simple" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id"
                                Name="Pais" Width="100%">
                                <Columns>
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Placa" HeaderStyle-Font-Bold="true"
                                        UniqueName="nombre">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="cmdEdit" Text='<%# Eval("PlacaVM") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="AnioModeloVM" HeaderText="Año" HeaderStyle-Font-Bold="true"
                                        UniqueName="AnioModeloVM"/>
                                    <telerik:GridBoundColumn DataField="NombreAseg" HeaderText="Aseguradora" HeaderStyle-Font-Bold="true"
                                        UniqueName="NombreAseg"/>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-Font-Bold="true"
                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Delete" HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server"
                                                CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete"
                                                ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAdd1" runat="server" Text="Agrega Pais" CausesValidation="False" CssClass="item" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px"></td>
                </tr>
            </table>
        </fieldset>
        <br />
        <asp:Panel ID="panelRegistration" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/AgreEditUsuario_03.jpg" ImageAlign="AbsMiddle" />&nbsp;
                    <asp:Label ID="lblUserEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table width="100%" border="0">
                   
                 <tr>
                    <td colspan="4">
                    <br />
                        <div class="title">
                            <label>
                                Aútotrasporte federal
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblPermSCT" runat="server" CssClass="item" Font-Bold="True" Text="Clave del permiso SCT:" />
                        <asp:RequiredFieldValidator ID="valPermSCT" runat="server" InitialValue="0"  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="cmbPermSCT" SetFocusOnError="true" ForeColor="red" />
                        <br />
                        <asp:DropDownList ID="cmbPermSCT" runat="server" CssClass="box" Width="90%" Style="margin-top: 12px;"
                            DataValueField="clave" DataTextField="descripcion" />
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblNumPermisoSCT" runat="server" CssClass="item" Font-Bold="True" Text="Numero de permiso SCT:" />
                        <asp:RequiredFieldValidator ID="valNumPermisoSCT" runat="server" InitialValue=""  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="txtNumPermisoSCT" SetFocusOnError="true" ForeColor="red"  />
                        <br />
                        <telerik:RadTextBox ID="txtNumPermisoSCT" runat="server" Width="90%" Style="margin-top: 12px;  text-transform: uppercase;"
                            MaxLength="50" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4"  class="w-100">
                    <div class="df-100">
                        <div class="w-25">
                            <asp:Label ID="lblNombreAseg" runat="server" CssClass="item" Font-Bold="True" Text="Nombre de la aseguradora:" />
                            <asp:RequiredFieldValidator ID="valNombreAseg" runat="server" InitialValue="" CssClass="item"
                                ErrorMessage="Requerido." ControlToValidate="txtNombreAseg" SetFocusOnError="true"
                                ForeColor="red" /><br />
                            <telerik:RadTextBox ID="txtNombreAseg" runat="server" Width="90%" Style="margin-top: 12px;
                                text-transform: uppercase;" MaxLength="50" />
                        </div>
                        <div class="w-25">
                            <asp:Label ID="lblNumPolizaSeguro" runat="server" CssClass="item" Font-Bold="True"
                            Text="Poliza del seguro:" />
                            <asp:RequiredFieldValidator ID="valNumPolizaSeguro" runat="server" InitialValue=""
                                CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtNumPolizaSeguro"
                                SetFocusOnError="true" ForeColor="red" />
                            <br />
                            <telerik:RadTextBox ID="txtNumPolizaSeguro" runat="server" Width="90%" Style="margin-top: 12px;
                                text-transform: uppercase;" MaxLength="30" />
                        </div>
                        <div class="w-25">
                            <asp:Label ID="Label1" runat="server" CssClass="item" Font-Bold="True" Text="Aseguradora medio ambiente:" /><br />
                            <telerik:RadTextBox ID="txtAseguraMedAmbiente" runat="server" Width="90%" Style="margin-top: 12px;
                                text-transform: uppercase;" MaxLength="50" />
                        </div>
                        <div class="w-25">
                            <asp:Label ID="Label2" runat="server" CssClass="item" Font-Bold="True"
                            Text="Poliza medio ambiente:" />
                            <br />
                            <telerik:RadTextBox ID="txtPolizaMedAmbiente" runat="server" Width="90%" Style="margin-top: 12px;
                                text-transform: uppercase;" MaxLength="30" />
                        </div>
                    </div>
                        
                        
                    </td>
                </tr>
                 <tr>
                    <td colspan="4">
                    <br />
                        <div class="title">
                            <label>
                                Identificación vehicular
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                <td style="width:40%">
                <asp:Label ID="lblConfigVehicular" runat="server" CssClass="item" Font-Bold="True" Text="Clave del autotransporte:" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="cmbConfigVehicular" SetFocusOnError="true" ForeColor="red" />                       
                <br />
                        <asp:DropDownList ID="cmbConfigVehicular" runat="server" CssClass="box" Width="90%" Style="margin-top: 12px;"
                            DataValueField="clave" DataTextField="descripcion" AutoPostBack="true"/>
                </td>
                <td style="width:25%">
                <asp:Label ID="lblPlacaVM" runat="server" CssClass="item" Font-Bold="True" Text="Placa vehicular:" />
                <asp:RequiredFieldValidator ID="valPlacaVM" runat="server" InitialValue=""  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="txtPlacaVM" SetFocusOnError="true" ForeColor="red" />
                            <asp:RegularExpressionValidator ID="valePlacaVM" CssClass="item" runat="server"
                            ControlToValidate="txtPlacaVM" Text="Inválido" ForeColor="red"
                            SetFocusOnError="True" ValidationExpression="[^(?!.*\s)-]{6,7}"></asp:RegularExpressionValidator>                        
                        <telerik:RadTextBox ID="txtPlacaVM" runat="server" Width="90%" Style="margin-top: 12px;text-transform: uppercase;"
                            MaxLength="7" />
                </td>
                <td style="width:25%">
                <asp:Label ID="lblAnioModeloVM" runat="server" CssClass="item" Font-Bold="True" Text="Año:" />
                        <asp:RequiredFieldValidator ID="valAnioModeloVM" runat="server" InitialValue=""  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="txtPlacaVM" SetFocusOnError="true" ForeColor="red" />
                            <asp:RegularExpressionValidator ID="valeAnioModeloVM" CssClass="item" runat="server"
                            ControlToValidate="txtAnioModeloVM" Text="Inválido" ForeColor="red"
                            SetFocusOnError="True" ValidationExpression="(19[0-9]{2}|20[0-9]{2})"></asp:RegularExpressionValidator> <br />                     
                        <telerik:RadMaskedTextBox RenderMode="Lightweight" ID="txtAnioModeloVM" runat="server"
                            Mask="####" Width="40%" Style="margin-top: 12px;">
                        </telerik:RadMaskedTextBox>
                </td>
                </tr>
                <asp:Panel ID="panelRemolque" runat="server" Visible="false">
                <tr>
                    <td colspan="4">
                    <br />
                        <div class="title">
                            <label>
                                Remolque
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                <td colspan="4">
                <div class="df-100">
                    <div class="w-20">
                        <asp:Label ID="lblSubTipoRem" runat="server" CssClass="item" Font-Bold="True" Text="Subtipo de remolque:" /><br />
                        <asp:DropDownList ID="cmbSubTipoRem" runat="server" CssClass="box" Width="90%" Style="margin-top: 12px;"
                            DataValueField="clave" DataTextField="descripcion"  />
                    </div>
                    <div class="w-10">
                        <asp:Label ID="lblPlaca" runat="server" CssClass="item" Font-Bold="True" Text="Placa:" />
                            <asp:RegularExpressionValidator ID="valePlaca" CssClass="item" runat="server" ControlToValidate="txtPlaca"
                                Text="Iválido" ForeColor="#c10000" SetFocusOnError="True" ValidationExpression="[^(?!.*\s)-]{6,7}"></asp:RegularExpressionValidator>
                            <telerik:RadTextBox ID="txtPlaca" runat="server" Width="100%" Style="margin-top: 4px;"
                                MaxLength="7" />                       
                    </div>
                    <div class="w-10">
                        <br />
                        <asp:Button ID="btnAddRemolque" Text="Agregar" runat="server" CssClass="item ml-p5 mt-p5  " />
                    </div>
                    <div class="w-44">
                        <telerik:RadGrid ID="remolqueslist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            GridLines="None" AllowSorting="true" PageSize="10" ShowStatusBar="True" Width="100%"
                            RenderMode="Lightweight">
                            <PagerStyle Mode="NumericPages" />
                            <MasterTableView NoMasterRecordsText="No se han agregado remolques" Width="100%"
                                PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowMultiColumnSorting="true" AllowSorting="true"
                                CommandItemDisplay="none">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Item" UniqueName="descripcion"
                                        HeaderStyle-Width="40" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="Descripcion" UniqueName="identificador"
                                        HeaderStyle-Width="30">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="placa" HeaderText="Placa" UniqueName="nombre"
                                        HeaderStyle-Width="30">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="Delete" HeaderStyle-Width="40">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" CausesValidation="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
                
                </td>
                </tr>
                </asp:Panel>
                
               
                    <tr valign="top">
                          <td colspan="4" align="right">
                              <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
                          </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 5px;">
                            <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                            <asp:HiddenField ID="registroID" runat="server" Value="0" />
                            
                            
                        </td>
                    </tr>
                </table>

            </fieldset>
            <br />
            <br />
            <br />
        </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

