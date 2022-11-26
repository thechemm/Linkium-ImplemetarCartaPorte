<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" CodeFile="FigurasTransporte.aspx.vb" Inherits="FigurasTransporte" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                 <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblUsersListLegend" runat="server" Font-Bold="true" CssClass="item">Listado de Figuras Autotransporte</asp:Label>
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
                                    
                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Nombre" HeaderStyle-Font-Bold="true"
                                        UniqueName="nombre">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="cmdEdit" Text='<%# Eval("NombreOperador") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="NumLicencia" HeaderText="Licencia" HeaderStyle-Font-Bold="true"
                                        UniqueName="NumLicencia"/>
                                        <telerik:GridBoundColumn DataField="RFCOperador" HeaderText="RFC" HeaderStyle-Font-Bold="true"
                                        UniqueName="RFCOperador"/>
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
                        <asp:Button ID="btnAdd1" runat="server" Text="Agrega Nuevo" CausesValidation="False" CssClass="item" />
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
                <td style="width: 20%;">
                            <asp:Label ID="Label1" runat="server" CssClass="item" Font-Bold="True" Text="Figura Transporte" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" CssClass="item"
                                ErrorMessage="Requerido." ControlToValidate="cmbFiguraTransporte" SetFocusOnError="true"
                                ForeColor="red" />
                            <br />
                   <asp:DropDownList ID="cmbFiguraTransporte" runat="server" CssClass="box" Width="80%" Style="margin-top: 10px;"
                                DataValueField="clave" DataTextField="nombre" AutoPostBack="true"/>
                 </td>
                <td style="width:26%">
                    <asp:Label ID="lblNombreOperador" runat="server" CssClass="item" Font-Bold="True" Text="Nombre:" />
                    <asp:RegularExpressionValidator ID="valeNombreOperador" CssClass="item"
                        runat="server" ControlToValidate="txtNombreOperador" Text="Formato inválido" ForeColor="#c10000"
                        SetFocusOnError="True" ValidationExpression="[^|]{1,254}" />
                    <telerik:RadTextBox ID="txtNombreOperador" runat="server" Width="90%" Style="margin-top: 5px;text-transform: uppercase;"
                        MaxLength="254" />
                </td>
                <td style="width:25%">
                    <asp:Label ID="lblRFCOperador" runat="server" CssClass="item" Font-Bold="True" Text="RFC del operador:" />
                    <%--<asp:RegularExpressionValidator ID="valeRFCOperador" CssClass="item" 
                        runat="server" ControlToValidate="txtRFCOperador" Text="Inválido " ForeColor="red"
                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z]{3,4})\d{6}([a-zA-Z\w]{3})$" />--%>
                    <asp:RequiredFieldValidator ID="valRFCOperador" runat="server" InitialValue="" CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="txtRFCOperador" SetFocusOnError="true" />
                    <br />
                    <telerik:RadTextBox ID="txtRFCOperador" runat="server" Width="90%" Style="margin-top: 5px; text-transform: uppercase;" />
                </td>
                <td style="width:26%">
                    <asp:Panel ID="panelLicencia" runat=Server>
                        <asp:Label ID="lblNumLicencia" runat="server" CssClass="item" Font-Bold="True" Text="Número de licencia:" />
                        <asp:RegularExpressionValidator ID="valeNumLicencia" CssClass="item"
                            runat="server" ControlToValidate="txtNumLicencia" Text="Formato inválido" ForeColor="red"
                            SetFocusOnError="True" ValidationExpression="[^|]{6,16}" />
                        <telerik:RadTextBox ID="txtNumLicencia" runat="server" Width="90%" Style="margin-top: 5px;text-transform: uppercase;"
                            MaxLength="16" />
                    </asp:Panel>                    
                </td>
                </tr>                     
                    <tr>
                    <!-- ::::::::::::::::: Domicilio operador -->
                    <asp:Panel ID="Panel3" runat="server"  >
                     <table style="width: 95%;" >
                
                    <tr>
                    <td >
                    <br />
                    <div style="width: 100%;display: flex;">
                       
                        <div style="width: 20%;display:none">
                            <asp:Label ID="lblPais_op" runat="server" CssClass="item" Font-Bold="True" Text="Pais:*" />
                            <asp:RequiredFieldValidator ID="valPais_op" runat="server" InitialValue="0" CssClass="item"
                                ErrorMessage="Requerido." ControlToValidate="cmbPais_op" SetFocusOnError="true"
                                ForeColor="red" /><br />
                            <asp:DropDownList ID="cmbPais_op" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                                DataValueField="clave" DataTextField="descripcion" Enabled ="false" />
                        </div>
                        <div style="width: 22%;">
                            <asp:Label ID="lblCodigoPostal_op" runat="server" CssClass="item" Font-Bold="True"
                                Text="Código Postal:*" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue=""
                                CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtCodigoPostal_op"
                                SetFocusOnError="true" ForeColor="red" />
                            <asp:RegularExpressionValidator ID="valeCodigoPostal_op" CssClass="item" runat="server"
                                ControlToValidate="txtCodigoPostal_op" Text="Inválido" ForeColor="red" SetFocusOnError="True"
                                ValidationExpression="[0-9]{5}" />
                            <br />
                            <telerik:RadTextBox ID="txtCodigoPostal_op" runat="server" Width="80%" Style="margin-top: 12px;"
                                MaxLength="5" AutoPostBack="true" />
                        </div>
                        <div style="width: 20%;">
                            <asp:Label ID="lblEstado_op" runat="server" CssClass="item" Font-Bold="True" Text="Estado:*" />
                            <asp:RequiredFieldValidator ID="valEstado_op" runat="server" InitialValue="0" CssClass="item"
                                ErrorMessage="Requerido." ControlToValidate="cmbEstado_op" SetFocusOnError="true"
                                ForeColor="red" />
                            <br />
                            <asp:DropDownList ID="cmbEstado_op" runat="server" CssClass="box" Width="80%" Style="margin-top: 10px;"
                                DataValueField="clave" DataTextField="nombre" AutoPostBack="true"/>
                        </div>
                        <div style="width: 20%;">
                            <asp:Label ID="lblMunicipio_op" runat="server" CssClass="item" Font-Bold="True" Text="Municipio:*" />                            
                            <asp:RequiredFieldValidator ID="valMunicipio_op" runat="server" InitialValue="0" CssClass="item"
                                ErrorMessage="Requerido." ControlToValidate="cmbMunicipio_op" SetFocusOnError="true"
                                ForeColor="red" /><br />
                            <asp:DropDownList ID="cmbMunicipio_op" runat="server" Width="80%" Style="margin-top: 12px;"
                                MaxLength="120" DataValueField="clave" DataTextField="nombre" CssClass="box"  />
                        </div>
                        <div style="width: 20%;">
                                <asp:Label ID="lblColonia_op" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:*" />
                                <asp:RequiredFieldValidator ID="valColonia_op" runat="server" InitialValue="0" CssClass="item"
                                ErrorMessage="Requerido." ControlToValidate="cmbColonia_op" SetFocusOnError="true"
                                ForeColor="red" />
                                <br />
                                <asp:DropDownList ID="cmbColonia_op" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                                    DataValueField="clave" DataTextField="nombre" />
                        </div>
                    </div>
                    <br />
                    <br />
                        <div style="width: 100%;display: flex;">
                            
                            <div style="Display:none">
                                <asp:Label ID="lblLocalidad_op" runat="server" CssClass="item" Font-Bold="True" Text="Localidad:"  />
                                <asp:DropDownList ID="txtLocalidad_op" runat="server" Width="80%" Style="margin-top: 12px;"
                                    MaxLength="120" DataValueField="clave" DataTextField="nombre" CssClass="box" />
                            </div>
                            <div style="width: 20%;">
                                <asp:Label ID="lblCalle_op" runat="server" CssClass="item" Font-Bold="True" Text="Calle*:" />
                                <asp:RequiredFieldValidator ID="valCalle_op" runat="server" InitialValue="" CssClass="item"
                                    ErrorMessage="Requerido." ControlToValidate="txtCalle_op" SetFocusOnError="true" />
                                <br />
                                <telerik:RadTextBox ID="txtCalle_op" runat="server" Width="90%" Style="margin-top: 8px;"
                                    MaxLength="100" />
                            </div>
                            <div style="width: 10%;">
                                    <asp:Label ID="lblNumeroExterior_op" runat="server" CssClass="item" Font-Bold="True" Text="No. Exterior:" />
                                    <telerik:RadTextBox ID="txtNumeroExterior_op" runat="server" Width="80%" Style="margin-top: 12px;" MaxLength="20" />
                            </div>
                            <div style="width: 10%;">
                                    <asp:Label ID="lblNumeroInterior_op" runat="server" CssClass="item" Font-Bold="True" Text="No. Interior:" />
                                    <telerik:RadTextBox ID="txtNumeroInterior_op" runat="server" Width="80%" Style="margin-top: 12px;" MaxLength="20" />
                            </div>
                            <div style="width: 25%;">
                                <asp:Label ID="lblReferencia_op" runat="server" CssClass="item" Font-Bold="True" Text="Referencia:"  /><br />
                                <telerik:RadTextBox ID="txtReferencia_op" runat="server" Width="100%" Style="margin-top: 12px;" MaxLength="200" />
                            </div>
                        </div>
                    </tr>
                    <tr valign="top">
                          <td colspan="4" align="right">
                              <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="item" />&nbsp;<asp:Button ID="btnCancelar" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
                          </td>
                    </tr>
                    <tr><td> &nbsp; </td></tr>
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
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

