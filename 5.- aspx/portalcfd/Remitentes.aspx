<%@ Page Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false"
    CodeFile="Remitentes.aspx.vb" Inherits="Remitentes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4
        {
            height: 17px;
        }
        .style5
        {
            height: 14px;
        }
        .style6
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet"
        LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:Panel runat="server" ID="panelList">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg"
                        ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblUsersListLegend" runat="server"
                            Font-Bold="true" CssClass="item">Listado de direcciones de origen</asp:Label>
                </legend>
                <table width="50%">
                    <tr>
                        <td style="height: 5px">
                            <telerik:RadGrid ID="GridList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                GridLines="None" PageSize="15" ShowStatusBar="True" Skin="Simple" Width="100%">
                                <PagerStyle Mode="NumericPages" />
                                <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Pais" Width="100%">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Identificador-catalogo:" HeaderStyle-Font-Bold="true"
                                            UniqueName="nombre">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit"
                                                    Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="CodigoPostal_or" HeaderText="Codigo postal" HeaderStyle-Font-Bold="true"
                                            UniqueName="NumLicencia" />
                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="Delete" HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                    CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
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
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="height: 5px">
                            <asp:Button ID="btnAdd1" runat="server" Text="Agrega Nuevo" CausesValidation="False"
                                CssClass="item" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 2px">
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelRegistration" runat="server" Visible="false">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/AgreEditUsuario_03.jpg"
                        ImageAlign="AbsMiddle" />&nbsp;
                    <asp:Label ID="lblLegend" runat="server" Font-Bold="True" CssClass="item">Nueva dirección</asp:Label>
                </legend>
                <br />
                <table width="100%" border="0">
                    <tr>
                        <!-- ::::::::::::::::: Domicilio Origen -->
                        <asp:Panel ID="Panel3" runat="server">
                            <table style="width: 95%;">
                                <tr>
                                                <td style="width: 100%; display: flex;">
                                                    <div style="width: 20%;">
                                                            <asp:Label ID="lblNombre" runat="server" CssClass="item" Font-Bold="True" Text="Identificador-catalogo:" />                                                            
                                                            <telerik:RadTextBox ID="txtNombre" runat="server" Width="90%" Style="margin-top: 18px;
                                                                text-transform: uppercase;" MaxLength="254" />
                                                        </div>
                                                    <div style="width: 20%;">
                                                        <asp:Label ID="lblRFCRemitente" runat="server" CssClass="item" Font-Bold="True"
                                                            Text="RFC del Remitente:" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="item"
                                                            runat="server" ControlToValidate="txtRFCRemitente" Text="Inválido" ForeColor="red"
                                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                                                        <br />
                                                        <telerik:RadTextBox ID="txtRFCRemitente" runat="server" Width="90%" Style="margin-top: 12px;
                                                            text-transform: uppercase;" MaxLength="13" />
                                                    </div>
                                                    <div style="width: 20%;">
                                                        <asp:Label ID="lblNombreRemitente" runat="server" CssClass="item" Font-Bold="True"
                                                            Text="Nombre del Remitente:" />
                                                        <br />
                                                        <telerik:RadTextBox ID="txtNombreRemitente" runat="server" Width="80%" Style="margin-top: 15px;
                                                            text-transform: uppercase;" MaxLength="256" />
                                                    </div>
                                                    <div style="width: 20%;">
                                                        <asp:Label ID="lbltxtIDUbicacion_or" runat="server" CssClass="item" Font-Bold="True"
                                                            Text="ID:" />
                                                        <asp:RegularExpressionValidator ID="valtxtIDUbicacion_or" CssClass="item" runat="server"
                                                            ControlToValidate="txtIDUbicacion_or" Text="Inválido" ForeColor="red" SetFocusOnError="True"
                                                            ValidationExpression="(OR)[0-9]{6}"></asp:RegularExpressionValidator>
                                                        <br />
                                                        <telerik:RadTextBox ID="txtIDUbicacion_or" runat="server" Width="60%" Style="margin-top: 8px;
                                                            text-transform: uppercase;" MaxLength="8" Text="OR" />
                                                    </div>
                                                    <div style="width: 20%;">
                                                            <asp:Label ID="lblCodigoPostal_or" runat="server" CssClass="item" Font-Bold="True"
                                                                Text="C. P.:*" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue=""
                                                                CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtCodigoPostal_or"
                                                                SetFocusOnError="true" ForeColor="red" />
                                                            <asp:RegularExpressionValidator ID="valeCodigoPostal_or" CssClass="item" runat="server"
                                                                ControlToValidate="txtCodigoPostal_or" Text="Inválido" ForeColor="red" SetFocusOnError="True"
                                                                ValidationExpression="[0-9]{5}" />
                                                            <br />
                                                            <telerik:RadTextBox ID="txtCodigoPostal_or" runat="server" Width="60%" Style="margin-top: 5px;"
                                                                MaxLength="5" AutoPostBack="true" />
                                                        </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <br />
                                                    <div style="width: 100%; display: flex;">
                                                        
                                                        
                                                        <div style="width: 20%;">
                                                            <asp:Label ID="lblEstado_or" runat="server" CssClass="item" Font-Bold="True" Text="Estado:*" />
                                                            <asp:RequiredFieldValidator ID="valEstado_or" runat="server" InitialValue="0" CssClass="item"
                                                                ErrorMessage="Requerido." ControlToValidate="cmbEstado_or" SetFocusOnError="true"
                                                                ForeColor="red" />
                                                            <br />
                                                            <asp:DropDownList ID="cmbEstado_or" runat="server" CssClass="box" Width="80%" Style="margin-top: 10px;"
                                                                DataValueField="clave" DataTextField="nombre" AutoPostBack="true" />
                                                        </div>
                                                       
                                                        <div style="width: 20%; display: none">
                                                            <asp:Label ID="lblPais_or" runat="server" CssClass="item" Font-Bold="True" Text="Pais:*" />
                                                            <asp:RequiredFieldValidator ID="valPais_or" runat="server" InitialValue="0" CssClass="item"
                                                                ErrorMessage="Requerido." ControlToValidate="cmbPais_or" SetFocusOnError="true"
                                                                ForeColor="red" /><br />
                                                            <asp:DropDownList ID="cmbPais_or" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                                                                DataValueField="clave" DataTextField="descripcion" Enabled="false" />
                                                        </div>
                                                        <div style="width: 20%;">
                                                            <asp:Label ID="lblMunicipio_or" runat="server" CssClass="item" Font-Bold="True" Text="Municipio:*" />
                                                            <asp:RequiredFieldValidator ID="valMunicipio_or" runat="server" InitialValue="0"
                                                                CssClass="item" ErrorMessage="Requerido." ControlToValidate="cmbMunicipio_or"
                                                                SetFocusOnError="true" ForeColor="red" /><br />
                                                            <asp:DropDownList ID="cmbMunicipio_or" runat="server" Width="90%" Style="margin-top: 12px;"
                                                                MaxLength="120" DataValueField="clave" DataTextField="nombre" CssClass="box" />
                                                        </div>
                                                        <div style="width: 20%;">
                                                            <asp:Label ID="lblColonia_or" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:*" />
                                                            <asp:RequiredFieldValidator ID="valColonia_or" runat="server" InitialValue="0" CssClass="item"
                                                                ErrorMessage="Requerido." ControlToValidate="cmbColonia_or" SetFocusOnError="true"
                                                                ForeColor="red" />
                                                            <br />
                                                            <asp:DropDownList ID="cmbColonia_or" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                                                                DataValueField="clave" DataTextField="nombre" />
                                                        </div>
                                                        <div style="width: 20%;">
                                                            <asp:Label ID="lblCalle_or" runat="server" CssClass="item" Font-Bold="True" Text="Calle*:" />
                                                            <asp:RequiredFieldValidator ID="valCalle_or" runat="server" InitialValue="" CssClass="item"
                                                                ErrorMessage="Requerido." ControlToValidate="txtCalle_or" SetFocusOnError="true" />
                                                            <br />
                                                            <telerik:RadTextBox ID="txtCalle_or" runat="server" Width="90%" Style="margin-top: 8px;"
                                                                MaxLength="100" />
                                                        </div>
                                                        <div style="width: 20%;">
                                                             <div style="width: 100%; display: flex;">
                                                        <div style="display: none">
                                                            <asp:Label ID="lblLocalidad_or" runat="server" CssClass="item" Font-Bold="True" Text="Localidad:" />
                                                            <asp:DropDownList ID="txtLocalidad_or" runat="server" Width="80%" Style="margin-top: 12px;"
                                                                MaxLength="120" DataValueField="clave" DataTextField="nombre" CssClass="box" />
                                                        </div>
                                                        <div style="width: 50%;">
                                                            <asp:Label ID="lblNumeroExterior_or" runat="server" CssClass="item" Font-Bold="True"
                                                                Text="No. Exterior:" />
                                                            <telerik:RadTextBox ID="txtNumeroExterior_or" runat="server" Width="80%" Style="margin-top: 12px;"
                                                                MaxLength="20" />
                                                        </div>
                                                        <div style="width: 50%;">
                                                            <asp:Label ID="lblNumeroInterior_or" runat="server" CssClass="item" Font-Bold="True"
                                                                Text="No. Interior:" />
                                                            <telerik:RadTextBox ID="txtNumeroInterior_or" runat="server" Width="80%" Style="margin-top: 12px;"
                                                                MaxLength="20" />
                                                        </div>
                                                        
                                                    </div>
                                                        </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                    
                                                        
                                                    <div>
                                                    <div style="width: 38%;">
                                                            <asp:Label ID="lblReferencia_or" runat="server" CssClass="item" Font-Bold="True"
                                                                Text="Referencia:" /><br />
                                                            <telerik:RadTextBox ID="txtReferencia_or" runat="server" Width="100%" Style="margin-top: 12px;"
                                                                MaxLength="200" />
                                                        </div>
                                                     </div>
                                                    <br />
                                                    <br />
                                              </td>      
                                            </tr>
                                <tr valign="top">
                                    <td colspan="4" align="right">
                                        <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="item" />&nbsp;<asp:Button
                                            ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 5px;">
                            <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                            <asp:HiddenField ID="registroID" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </telerik:RadAjaxPanel>
</asp:Content>
