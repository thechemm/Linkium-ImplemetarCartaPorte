<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeFile="Clientes.aspx.vb" Inherits="portalcfd_Clientes" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
         <fieldset>
            <legend style="padding-right: 6px; color: Black">
               <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icons/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <br />
            <span class="item">
                <asp:Panel ID="searchPanel" DefaultButton="btnSearch" runat="server">
                       Palabra clave: 
                      <asp:TextBox ID="txtSearch" runat="server" CssClass="box"></asp:TextBox>&nbsp;
                      <asp:Button ID="btnSearch" runat="server" CssClass="boton" Text="Buscar" />&nbsp;&nbsp;<asp:Button ID="btnAll" runat="server" CssClass="boton" Text="Ver todo" />
                </asp:Panel>
            </span>
            <br /><br />
        </fieldset>
        <br />
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblClientsListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <table width="100%">
                <tr>
                    <td style="height: 5px">
                        <telerik:RadGrid ID="clientslist" runat="server" AllowPaging="True" AllowFilteringByColumn="False"
                            AutoGenerateColumns="False" GridLines="None" 
                            OnNeedDataSource="clientslist_NeedDataSource" PageSize="15" ShowStatusBar="True" 
                            Skin="Simple" Width="100%">
                            <PagerStyle Mode="NumericPages" />
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" 
                                Name="Clients" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Id" AllowFiltering="false"
                                        UniqueName="id"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="" 
                                        SortExpression="razonsocial" UniqueName="razonsocial">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' 
                                                CommandName="cmdEdit" Text='<%# Eval("razonsocial") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="contacto" HeaderText="" FilterControlWidth="80px" ShowFilterIcon="false"
                                        UniqueName="contacto" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="telefono_contacto" HeaderText="" AllowFiltering="false"
                                        UniqueName="telefono_contacto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="rfc" HeaderText="" UniqueName="rfc" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False"
                                        HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
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
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" style="height: 5px">
                        <asp:Button ID="btnAddClient" runat="server" CausesValidation="False" 
                            CssClass="item" TabIndex="6" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 2px">
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <br />
        
        
        <asp:Panel ID="panel1" runat="server" Visible=false BorderWidth="1px" BorderStyle=Dotted>
            <table border="0" style="width: 100%" cellpadding="3" cellspacing="3">
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:Label ID="lblNombreTitle" runat="server" CssClass="item" Font-Bold="True" Text="Contacto:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblClienteTitle" runat="server" CssClass="item" Font-Bold="True" Text="Raz�n Social:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEmailTitle" runat="server" CssClass="item" Font-Bold="True" Text="Email del Contacto:"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:Label ID="lblNombreValue" runat="server" CssClass="item" Font-Bold="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblClienteValue" runat="server" CssClass="item" Font-Bold="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEmailValue" runat="server" CssClass="item" Font-Bold="false"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
        </asp:Panel>
        <asp:Panel ID="panelClientRegistration" runat="server" Visible="False">
        <table style="width: 100%;" border="0">
                    <tr style="vertical-align: top;">
                        <td>
                             <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Office2010Black" MultiPageID="RadMultiPage1"
                                    SelectedIndex="0" CausesValidation="False">
                                    <Tabs>
                                        <telerik:RadTab Text="Datos Generales" TabIndex="0" Value="0" Enabled="True" Selected="true">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="Cuentas Bancarias" TabIndex="1" Value="1" Enabled="false">
                                        </telerik:RadTab>
                                        <telerik:RadTab Text="Domicilios de Destino" TabIndex="2" Value="2" Enabled="false" >
                                        </telerik:RadTab>
                                    </Tabs>
                             </telerik:RadTabStrip>
                        </td>
                    </tr>
                </table>
        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%" Width="100%">
            <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%" Selected="true">   
                <br />
                 <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblClientEditLegend" runat="server" Font-Bold="True" 
                                CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table width="100%">
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    &nbsp;</td>
                                <td width="33%">
                                    <asp:Label id="lblPriceType" runat="server" CssClass="item" Font-Bold ="true" Text="Tipo de precio:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%" valign="top" colspan="2" style="width: 66%">
                                    <telerik:RadTextBox ID="txtSocialReason" Runat="server" Width="92%">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="tipoprecioid" runat="server" CssClass="box"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="txtSocialReason" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    &nbsp;</td>
                                <td width="33%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    &nbsp;</td>
                                <td width="33%">
                                    &nbsp;</td>
                                <td width="33%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContactEmail" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContact" Runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContactEmail" Runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContactPhone" Runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">
                                    &nbsp;</td>
                                <td class="style4" width="33%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtContactEmail" CssClass="item" 
                                        ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                                </td>
                                <td class="style4" width="33%">
                                </td>
                            </tr>
                            <tr>
                                <td width="33%" class="style5">
                                    </td>
                                <td width="33%" class="style5">
                                    </td>
                                <td width="33%" class="style5">
                                    </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblStreet" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%" align="left">
                                    <asp:Label ID="lblExtNumber" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblIntNumber" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="left" width="33%">
                                    <asp:Label ID="lblColony" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtStreet" Runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtExtNumber" Runat="server" Width="35%">
                                    </telerik:RadTextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <telerik:RadTextBox ID="txtIntNumber" Runat="server" Width="35%">
                                    </telerik:RadTextBox>
                                </td>
                                <td align="left" width="33%">
                                    <telerik:RadTextBox ID="txtColony" Runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="txtStreet" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="txtExtNumber" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left" width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="txtColony" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%" class="style6">
                                    </td>
                                <td width="33%" class="style6">
                                    </td>
                                <td width="33%" class="style6">
                                    </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblCountry" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblState" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblTownship" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                   <asp:DropDownList ID="fac_paisid" runat="server" CssClass="box" AutoPostBack=true></asp:DropDownList>
                                </td>
                                <td width="33%">
                                     <asp:DropDownList ID="cmbState" runat="server" CssClass="box" AutoPostBack=true Width="85%"></asp:DropDownList>
                                    <telerik:RadTextBox ID="txtStates" Runat="server" Width="85%" Visible="false"></telerik:RadTextBox>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtTownship" Runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="fac_paisid" CssClass="item" InitialValue="0"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="cmbState" CssClass="item" InitialValue="0"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="txtStates" CssClass="item" Enabled=false></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="txtTownship" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    &nbsp;</td>
                                <td width="33%">
                                    &nbsp;</td>
                                <td width="33%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblZipCode" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblCondiciones" runat="server" CssClass="item" Font-Bold="true" Text="Condiciones:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtZipCode" Runat="server" Width="85%" Visible=false>
                                    </telerik:RadTextBox>
                                    
                                    <telerik:RadAutoCompleteBox TextSettings-SelectionMode="Single" runat="server" ID="txtZipCod"
                                    DataTextField="codigo" DataValueField="id" InputType="Text" Width="290px" DropDownWidth="150px">
                                   <%-- <TokensSettings AllowTokenEditing="true" />--%>
                                </telerik:RadAutoCompleteBox>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtRFC" Runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="condicionesid" runat="server" CssClass="box"></asp:DropDownList>
                                </td>
                                
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="txtZipCode" CssClass="item" Enabled=false></asp:RequiredFieldValidator>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                        ControlToValidate="txtZipCod" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="vDatosGenerales"
                                        ControlToValidate="txtRFC" CssClass="item"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="valRFC" CssClass="item" runat="server" ControlToValidate="txtRFC"
                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z&]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                                </td>
                                <td width="33%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%" class="style6">
                                    </td>
                                <td width="33%" class="style6">
                                    </td>
                                <td width="33%" class="style6">
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblContribuyente" runat="server" Text="Tipo de contribuyente / Honorarios" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label id="lblFormaPago" runat="server" CssClass="item" Font-Bold="true" Text="Forma de pago:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNumCtaPago" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="tipoContribuyenteid" runat="server" CssClass="box"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="formapagoid" runat="server" CssClass="box"></asp:DropDownList>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNumCtaPago" Runat="server" Width="55%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="valTipoContribuyente" runat="server"  InitialValue="0" SetFocusOnError="true" ControlToValidate="tipoContribuyenteid" CssClass="item" ValidationGroup="vDatosGenerales"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="M�todo de Pago:" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label id="Label2" runat="server" CssClass="item" Font-Bold="true" Text="Uso del Comprobante:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" CssClass="item" Font-Bold="true" Text="Cuenta Predial (opcional)"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="metodopagoid" runat="server" CssClass="box"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="comprobanteid" runat="server" CssClass="box" Width="85%"></asp:DropDownList>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtCuentaPredial" Runat="server" Width="55%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    &nbsp;</td>
                                <td width="33%">
                                    &nbsp;</td>
                                <td width="33%">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="3">
                                    <asp:Button ID="btnSaveClient" runat="server" CssClass="item" ValidationGroup="vDatosGenerales"/>&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="item" 
                                        CausesValidation="False" />
                                    
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 66%; height: 5px;">
                                    <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                                    <asp:HiddenField ID="ClientsID" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                </fieldset>
                </telerik:RadPageView>
             
            <telerik:RadPageView ID="RadPageView2" runat="server">
                          <br />
                            <fieldset>
                                <legend style="padding-right: 6px; color: Black">
                                  <asp:Label ID="Label4" runat="server" Text="Agregar / Editar Cuentas Bancarias" Font-Bold="true" CssClass="item"></asp:Label>
                                </legend>
                                    <table border="0" class="style1">
                                        <tr
                                            <td width="100%" colspan="7">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                <asp:Label ID="lblBanco" runat="server" CssClass="item" Font-Bold="True" Text="Banco Nacional:"></asp:Label>
                                            </td>
                                            <td width="30%">
                                                 <asp:Label ID="lblBancoExtr" runat="server" CssClass="item" Font-Bold="True" Text="Banco Extranjero:"></asp:Label>
                                            </td>
                                            <td width="30%">
                                                 <asp:Label ID="lblRfc1" runat="server" CssClass="item" Font-Bold="True" Text="RFC:"></asp:Label>
                                            </td>
                                             <td width="10%">
                                                  &nbsp;
                                             </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                <telerik:RadTextBox ID="txtBanco" runat="server" Width="95%">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td width="30%">
                                                <telerik:RadTextBox ID="txtBancoExtr" runat="server" Width="95%">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td width="30%">
                                                 <telerik:RadTextBox ID="txtRFCBAK" Runat="server" Width="85%">
                                                 </telerik:RadTextBox>
                                            </td>
                                            <td width="10%">
                                                  
                                             </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">&nbsp;</td>
                                            <td width="30%">&nbsp;</td>
                                            <td width="30%">
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="vDatosCuenta"
                                                        ControlToValidate="txtRFCBAK" CssClass="item" Text="Requerido"></asp:RequiredFieldValidator>
                                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="item" runat="server" ControlToValidate="txtRFCBAK"
                                                        SetFocusOnError="True" ValidationExpression="^([a-zA-Z&]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td width="30%">
                                                <asp:Label ID="lblNonCuenta" runat="server" CssClass="item" Font-Bold="True" Text="N�mero de Cuenta:"></asp:Label>
                                            </td>
                                            <td width="30%">
                                                 <asp:Label ID="Label5" runat="server" CssClass="item" Font-Bold="True" Text="Predeterminado:"></asp:Label>
                                            </td>
                                            <td width="30%">&nbsp;
                                            </td>
                                             <td width="10%">&nbsp;
                                             </td>
                                        </tr>
                                        <tr>
                                            <td width="30%">
                                                <telerik:RadTextBox ID="txtCuenta" runat="server" Width="96%">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td width="30%">
                                                <asp:CheckBox runat="server" ID="chkPredeterminado"/>
                                            </td>
                                            <td width="30%">
                                            </td>
                                            <td width="10%">
                                            
                                             </td>
                                        </tr>
                                        <tr>
                                            <td width="30%"><asp:RequiredFieldValidator ID="valCuenta" runat="server" SetFocusOnError="true" ControlToValidate="txtCuenta" ValidationGroup="vDatosCuenta" Text="Requerido" ForeColor="Red" CssClass="item" ></asp:RequiredFieldValidator></td>
                                            <td width="30%">&nbsp;</td>
                                            <td width="30%">&nbsp;</td>
                                            <td width="30%">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width="30%">&nbsp;</td>
                                            <td width="30%">&nbsp;</td>
                                            <td width="23%" align="right" valign="top">
                                                <asp:Button ID="btnGuardar" runat="server" ValidationGroup="vDatosCuenta" Text="Guardar" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="false"/>
                                            </td>
                                        </tr>
                                        <tr>
                                             <td colspan="4" style="width: 66%; height: 5px;">
                                                   <asp:HiddenField ID="CuentaID" runat="server" Value="0" />
                                             </td>
                                        </tr>
                                    </table>
                                </fieldset>
                             <br />
                          <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                               <asp:Label ID="lblSucursalesListLegend" runat="server" Text="Listado de Cuentas Bancarias" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table width="100%">
                                <tr>
                                    <td style="height: 5px">
                                        <telerik:RadGrid ID="cuentasList" runat="server" AllowPaging="True" 
                                            AutoGenerateColumns="False" GridLines="None" 
                                            PageSize="20" ShowStatusBar="True" 
                                            Skin="Simple" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Cuentas" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Folio" UniqueName="id">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="banconacional" HeaderText="Banco Nacional" UniqueName="banconacional">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="bancoextranjero" HeaderText="Banco Extranjero" UniqueName="bancoextranjero">
                                                    </telerik:GridBoundColumn> 
                                                    <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc">
                                                    </telerik:GridBoundColumn>     
                                                    <telerik:GridBoundColumn DataField="numctapago" HeaderText="Cuenta Bancaria" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                     <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Predeterminado" UniqueName="">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgPredeterminado" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/icons/arrow.gif" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>            
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
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
                            </table>
                            </fieldset>
                </telerik:RadPageView>
            <telerik:RadPageView ID="RadPageView3" runat="server">
                <br />
                <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" Width="100%" HorizontalAlign="NotSet"
                    LoadingPanelID="RadAjaxLoadingPanel1">
                    <asp:Panel runat="server" ID="panelList">
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Image ID="Image24" runat="server" ImageUrl="~/images/icons/ListaProveedores_03.jpg"
                                    ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblUsersListLegend" runat="server"
                                        Font-Bold="true" CssClass="item">Listado de Direcciones</asp:Label>
                            </legend>
                            <table width="50%">
                                <tr>
                                    <td style="height: 5px">
                                    <br />
                                        <telerik:RadGrid ID="GridList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            GridLines="None" PageSize="15" ShowStatusBar="True" Skin="Simple" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Pais" Width="100%" NoMasterRecordsText="No se encontraron registros">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="true" HeaderText="Identificador-catalogo:" HeaderStyle-Font-Bold="true"
                                                        UniqueName="nombre">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit"
                                                                Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="CodigoPostal_des" HeaderText="Codigo postal" HeaderStyle-Font-Bold="true"
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
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/icons/AgreEditUsuario_03.jpg"
                                    ImageAlign="AbsMiddle" />&nbsp;
                                <asp:Label ID="lblLegend" runat="server" Font-Bold="True" CssClass="item">Nueva direcci�n</asp:Label>
                            </legend>
                            <br />
                            <table width="100%" border="0">
                                <tr>
                                    <!-- ::::::::::::::::: Domicilio destino -->
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
                                                        <asp:Label ID="lblRFCDestinatario" runat="server" CssClass="item" Font-Bold="True"
                                                            Text="RFC del Destinatario:" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="item"
                                                            runat="server" ControlToValidate="txtRFCDestinatario" Text="Inv�lido" ForeColor="red"
                                                            SetFocusOnError="True" ValidationExpression="^([a-zA-Z]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                                                        <br />
                                                        <telerik:RadTextBox ID="txtRFCDestinatario" runat="server" Width="90%" Style="margin-top: 12px;
                                                            text-transform: uppercase;" MaxLength="13" />
                                                    </div>
                                                    <div style="width: 20%;">
                                                        <asp:Label ID="lblNombreDestinatario" runat="server" CssClass="item" Font-Bold="True"
                                                            Text="Nombre del Destinatario:" />
                                                        <br />
                                                        <telerik:RadTextBox ID="txtNombreDestinatario" runat="server" Width="80%" Style="margin-top: 15px;
                                                            text-transform: uppercase;" MaxLength="256" />
                                                    </div>
                                                    <div style="width: 20%;">
                                                        <asp:Label ID="lbltxtIDUbicacion_des" runat="server" CssClass="item" Font-Bold="True"
                                                            Text="ID:" />
                                                        <asp:RegularExpressionValidator ID="valtxtIDUbicacion_des" CssClass="item" runat="server"
                                                            ControlToValidate="txtIDUbicacion_des" Text="Inv�lido" ForeColor="red" SetFocusOnError="True"
                                                            ValidationExpression="(DE)[0-9]{6}"></asp:RegularExpressionValidator>
                                                        <br />
                                                        <telerik:RadTextBox ID="txtIDUbicacion_des" runat="server" Width="60%" Style="margin-top: 8px;
                                                            text-transform: uppercase;" MaxLength="8" Text="DE" />
                                                    </div>
                                                    <div style="width: 20%;">
                                                            <asp:Label ID="lblCodigoPostal_des" runat="server" CssClass="item" Font-Bold="True"
                                                                Text="C. P.:*" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue=""
                                                                CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtCodigoPostal_des"
                                                                SetFocusOnError="true" ForeColor="red" />
                                                            <asp:RegularExpressionValidator ID="valeCodigoPostal_des" CssClass="item" runat="server"
                                                                ControlToValidate="txtCodigoPostal_des" Text="Inv�lido" ForeColor="red" SetFocusOnError="True"
                                                                ValidationExpression="[0-9]{5}" />
                                                            <br />
                                                            <telerik:RadTextBox ID="txtCodigoPostal_des" runat="server" Width="60%" Style="margin-top: 5px;"
                                                                MaxLength="5" AutoPostBack="true" />
                                                        </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                <br />
                                                    <div style="width: 100%; display: flex;">
                                                        
                                                        
                                                        <div style="width: 20%;">
                                                            <asp:Label ID="lblEstado_des" runat="server" CssClass="item" Font-Bold="True" Text="Estado:*" />
                                                            <asp:RequiredFieldValidator ID="valEstado_des" runat="server" InitialValue="0" CssClass="item"
                                                                ErrorMessage="Requerido." ControlToValidate="cmbEstado_des" SetFocusOnError="true"
                                                                ForeColor="red" />
                                                            <br />
                                                            <asp:DropDownList ID="cmbEstado_des" runat="server" CssClass="box" Width="80%" Style="margin-top: 10px;"
                                                                DataValueField="clave" DataTextField="nombre" AutoPostBack="true" />
                                                        </div>
                                                       
                                                        <div style="width: 20%; display: none">
                                                            <asp:Label ID="lblPais_des" runat="server" CssClass="item" Font-Bold="True" Text="Pais:*" />
                                                            <asp:RequiredFieldValidator ID="valPais_des" runat="server" InitialValue="0" CssClass="item"
                                                                ErrorMessage="Requerido." ControlToValidate="cmbPais_des" SetFocusOnError="true"
                                                                ForeColor="red" /><br />
                                                            <asp:DropDownList ID="cmbPais_des" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                                                                DataValueField="clave" DataTextField="descripcion" Enabled="false" />
                                                        </div>
                                                        <div style="width: 20%;">
                                                            <asp:Label ID="lblMunicipio_des" runat="server" CssClass="item" Font-Bold="True" Text="Municipio:*" />
                                                            <asp:RequiredFieldValidator ID="valMunicipio_des" runat="server" InitialValue="0"
                                                                CssClass="item" ErrorMessage="Requerido." ControlToValidate="cmbMunicipio_des"
                                                                SetFocusOnError="true" ForeColor="red" /><br />
                                                            <asp:DropDownList ID="cmbMunicipio_des" runat="server" Width="90%" Style="margin-top: 12px;"
                                                                MaxLength="120" DataValueField="clave" DataTextField="nombre" CssClass="box" />
                                                        </div>
                                                        <div style="width: 20%;">
                                                            <asp:Label ID="lblColonia_des" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:*" />
                                                            <asp:RequiredFieldValidator ID="valColonia_des" runat="server" InitialValue="0" CssClass="item"
                                                                ErrorMessage="Requerido." ControlToValidate="cmbColonia_des" SetFocusOnError="true"
                                                                ForeColor="red" />
                                                            <br />
                                                            <asp:DropDownList ID="cmbColonia_des" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                                                                DataValueField="clave" DataTextField="nombre" />
                                                        </div>
                                                        <div style="width: 20%;">
                                                            <asp:Label ID="lblCalle_des" runat="server" CssClass="item" Font-Bold="True" Text="Calle*:" />
                                                            <asp:RequiredFieldValidator ID="valCalle_des" runat="server" InitialValue="" CssClass="item"
                                                                ErrorMessage="Requerido." ControlToValidate="txtCalle_des" SetFocusOnError="true" />
                                                            <br />
                                                            <telerik:RadTextBox ID="txtCalle_des" runat="server" Width="90%" Style="margin-top: 8px;"
                                                                MaxLength="100" />
                                                        </div>
                                                        <div style="width: 20%;">
                                                             <div style="width: 100%; display: flex;">
                                                        <div style="display: none">
                                                            <asp:Label ID="lblLocalidad_des" runat="server" CssClass="item" Font-Bold="True" Text="Localidad:" />
                                                            <asp:DropDownList ID="txtLocalidad_des" runat="server" Width="80%" Style="margin-top: 12px;"
                                                                MaxLength="120" DataValueField="clave" DataTextField="nombre" CssClass="box" />
                                                        </div>
                                                        <div style="width: 50%;">
                                                            <asp:Label ID="lblNumeroExterior_des" runat="server" CssClass="item" Font-Bold="True"
                                                                Text="No. Exterior:" />
                                                            <telerik:RadTextBox ID="txtNumeroExterior_des" runat="server" Width="80%" Style="margin-top: 12px;"
                                                                MaxLength="20" />
                                                        </div>
                                                        <div style="width: 50%;">
                                                            <asp:Label ID="lblNumeroInterior_des" runat="server" CssClass="item" Font-Bold="True"
                                                                Text="No. Interior:" />
                                                            <telerik:RadTextBox ID="txtNumeroInterior_des" runat="server" Width="80%" Style="margin-top: 12px;"
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
                                                            <asp:Label ID="lblReferencia_des" runat="server" CssClass="item" Font-Bold="True"
                                                                Text="Referencia:" /><br />
                                                            <telerik:RadTextBox ID="txtReferencia_des" runat="server" Width="100%" Style="margin-top: 12px;"
                                                                MaxLength="200" />
                                                        </div>
                                                     </div>
                                                    <br />
                                                    <br />
                                              </td>      
                                            </tr>
                                            <tr valign="top">
                                                <td colspan="4" align="right">
                                                    <asp:Button ID="btnSaveItem" Text="Guardar" runat="server" CssClass="item" />&nbsp;
                                                    <asp:Button ID="btnCancelItem" CausesValidation="false" Text="Cancelar" runat="server" CssClass="item" />
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
                                        <asp:HiddenField ID="InsertOrUpdateItem" runat="server" Value="0" />
                                        <asp:HiddenField ID="registroID" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </telerik:RadAjaxPanel>
            </telerik:RadPageView>
       </telerik:RadMultiPage>
    </asp:Panel>
    
    </telerik:RadAjaxPanel>
    
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
    
</asp:Content>

