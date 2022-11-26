<%@ Page Title="" Language="VB" MasterPageFile="~/portalcfd/MasterPage_PortalCFD.master" AutoEventWireup="false" CodeFile="Facturar_Extended.aspx.vb" Inherits="portalcfd_Facturar_Extended" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .titulos 
        {
            font-family:verdana;
            font-size:medium;
            color:Purple;
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
    <script type="text/javascript">       

        function OnClientBlurHandler(sender, eventArgs) {
            var textInTheCombo = sender.get_text();
            var item = sender.findItemByText(textInTheCombo);
            //if there is no item with that text
            if (!item) {
                sender.set_text("");
                setTimeout(function () {
                    var inputElement = sender.get_inputDomElement();
                    //inputElement.focus();
                    inputElement.set_text = "--Seleccione--";
                }, 20);
            }
            else{
            document.getElementById("ctl00_ContentPlaceHolder1_txtCantidad").click();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
        </telerik:RadWindowManager>
        <br />

        <asp:Panel ID="panelClients" runat="server">

            <fieldset style="padding:10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="imgPanel1" runat="server" ImageUrl="~/portalcfd/images/comprobant.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientsSelectionLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>

                <br />
                <table border="0" cellpadding="2" cellspacing="0" align="left" width="95%">
                    <tr>
                        <td class="item" colspan="2">
                            <strong>Seleccione el cliente:</strong>&nbsp;<asp:RequiredFieldValidator ID="valClienteID" runat="server" InitialValue="0" ErrorMessage="Seleccione el cliente al cual le va a facturar." ControlToValidate="cmbClient" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <asp:DropDownList ID="cmbClient" runat="server" CausesValidation="false" CssClass="item" AutoPostBack="true"></asp:DropDownList>            
                        </td>
                        <td class="item" colspan="2">
                            <strong>Lugar de expedición:</strong>&nbsp;<asp:RequiredFieldValidator ID="valExpedicion" runat="server" ErrorMessage="Especifique el lugar de expedición." ControlToValidate="txtLugarExpedicion" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <telerik:RadTextBox ID="txtLugarExpedicion" Runat="server" Width="350px" CssClass="item">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"><br /></td>
                    </tr>
                    <tr>
                        <td class="item">
                            <strong>Tipo de documento:</strong>&nbsp;<asp:RequiredFieldValidator ID="valSerieId" runat="server" InitialValue="0" ErrorMessage="Requerido." ControlToValidate="serieid" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <asp:DropDownList ID="serieid" runat="server" CssClass="box" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td class="item">
                            <strong>Metodo de Pago:</strong>&nbsp;<asp:RequiredFieldValidator ID="valMetodoPago" runat="server" InitialValue="0" ErrorMessage="Requerido." ControlToValidate="metodopagoid" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                            <asp:DropDownList ID="metodopagoid" runat="server" CssClass="box"></asp:DropDownList>
                        </td>
                        <td class="item">&nbsp;
                        </td>
                       <td class="item">
                            <asp:panel ID="panelDivisas" runat="server" Visible="false">
                                <strong>Tipo de cambio:</strong>&nbsp;<asp:RequiredFieldValidator ID="valTipoCambio" runat="server" ControlToValidate="tipocambio" ErrorMessage="Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator><br /><br />
                                $ <telerik:RadNumericTextBox ID="tipocambio" runat="server" NumberFormat-DecimalDigits="2" Value="0"></telerik:RadNumericTextBox>
                            </asp:panel>
                        </td>
                    </tr>
                </table>
                    
                
            </fieldset>
        </asp:Panel>
        <br />    

        <asp:Panel ID="panelSpecificClient" runat="server" Visible="False">

            <fieldset style="padding:10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/portalcfd/images/datClient.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientData" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>

                <br />

 <table width="100%" border="0">
                <tr>
                    <td colspan="2">
                         <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                         <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td width="43%">
                        <asp:Label ID="Label1" runat="server" CssClass="item" Font-Bold="True" Text="Condiciones"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblSocialReasonValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                       <asp:Label ID="lblContactValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                   <td width="43%">
                        <asp:DropDownList ID="condicionesId" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStreet" runat="server" CssClass="item" Font-Bold="True" Text="Calle:"></asp:Label>
                    </td>
                    <td>
                        <table style="width: 100%;" border="0" cellpadding="2" cellspacing="0">
                            <tr>
                                <td style="width: 60%;">
                                    <asp:Label ID="lblExtNumber" runat="server" CssClass="item" Font-Bold="True" Text="No. Ext."></asp:Label>
                                </td>
                                <td style="width: 40%;">
                                    <asp:Label ID="lblIntNumber" runat="server" CssClass="item" Font-Bold="True" Text="No. Int."></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Label ID="lblColony" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:"></asp:Label>
                    </td>
                    <td width="43%">
                        <asp:Label ID="lblCountry" runat="server" CssClass="item" Font-Bold="True" Text="País:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStreetValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        <table style="width: 100%;" border="0" cellpadding="2" cellspacing="0">
                            <tr>
                                <td style="width: 60%;">
                                    <asp:Label ID="lblExtNumberValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                                </td>
                                <td style="width: 54%;">
                                   <asp:Label ID="lblIntNumberValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                   <td>
                       <asp:Label ID="lblColonyValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="43%">
                        <asp:Label ID="lblCountryValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblState" runat="server" CssClass="item" Font-Bold="True" Text="Estado:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTownship" runat="server" CssClass="item" Font-Bold="True" Text="Ciudad:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblZipCode" runat="server" CssClass="item" Font-Bold="True" Text="Código Postal:"></asp:Label>
                    </td>
                   <td width="43%">
                        <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True" Text="RFC:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                       <asp:Label ID="lblEstadoValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTownshipValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                       <asp:Label ID="lblZipCodeValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblRFCValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFormaPago" runat="server" CssClass="item" Font-Bold="true" Text="Forma de pago:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNumCtaPago" runat="server" CssClass="item" Font-Bold="true" Text="Número de cuenta: (opcional):"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblContribuyente" runat="server" Text="Tipo de contribuyente" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                   <td width="43%">
                       <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True" Text="Teléfono"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="formapagoid" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtNumCtaPago" runat="server">
                        </telerik:RadTextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoContribuyenteValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                    <td width="43%">
                        <asp:Label ID="lblContactPhoneValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                        <td width="33%">

                        </td>
                        <td width="33%">
                            <asp:Label ID="Label2" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                        </td>
                    </tr>
                    
                    <tr>
                        <td width="66%" colspan="2">
                            <asp:Label ID="lblUsoCFDI" runat="server" CssClass="item" Font-Bold="True">Uso De Comprobantes</asp:Label>
                        </td>
                        
                        <td width="33%">
                            <asp:Label ID="Label3" runat="server" CssClass="item" Font-Bold="True" Visible=false>Tipo de Relacion</asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td colspan="2" width="66%">
                            <asp:DropDownList ID="usoCFDIID" runat="server" CssClass="box"></asp:DropDownList>
                        </td>
                        <td width="33%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ErrorMessage="Requerido." ControlToValidate="usoCFDIID" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr>
                        <td width="33%">

                        </td>
                        <td width="33%">
                            <asp:Label ID="Label5" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                        <td width="33%">
                        </td>
                    </tr>
                    <tr>
                        <td width="66%" colspan="2">
                            <asp:Label ID="Label6" runat="server" CssClass="item" Font-Bold="True" Visible=false>Tipo de Relacion</asp:Label>
                        </td>
                        
                        <td width="33%">
                             <asp:Label ID="Label7" runat="server" CssClass="item" Font-Bold="True" Visible=false>UUID</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="66%">
                            <asp:DropDownList ID="tiporelacionid" runat="server" CssClass="box" Visible=false></asp:DropDownList>&nbsp;
                            <asp:RequiredFieldValidator ID="ValTipoRelecion" runat="server" InitialValue="0" ErrorMessage="Requerido." ControlToValidate="tiporelacionid" SetFocusOnError="true" Enabled=false></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="2" width="66%">
                             <telerik:RadTextBox ID="txtFolioFiscal" runat="server"  Width="80%" Visible=false></telerik:RadTextBox>&nbsp;
                             <asp:RequiredFieldValidator ID="ValFolioFiscal" runat="server" ErrorMessage="Requerido." ControlToValidate="txtFolioFiscal" SetFocusOnError="true" Enabled=false></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="66%" colspan="2">
                            <asp:Label ID="lblInstruccionesEspeciales" runat="server" CssClass="item" Font-Bold="True" Text="Observaciones:"></asp:Label>
                        </td>
                        
                        <td width="33%">
                            <asp:Label ID="Label4" runat="server" Font-Bold="true" CssClass="item"></asp:Label>    
                        </td>
                    </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadTextBox ID="instrucciones" Runat="server" Width="450px" CssClass="item" TextMode="MultiLine" Height="40px"></telerik:RadTextBox>
                    </td>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox runat="server" ID="ckCartaPorte" Text="&nbsp;Complemento Carta Porte" AutoPostBack="true"  CssClass="item" />&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox runat="server" ID="ckTernium" Text="&nbsp;Addenda Ternium" AutoPostBack="true"  CssClass="item" />
                    </td>
                </tr>
                <tr>
                        <td width="33%">
                            <asp:CheckBox Visible="false" CssClass="item" ID="chkAduana" runat="server" Text="Incluye información aduanera" AutoPostBack="true" TextAlign="Right" />
                        </td>
                        <td width="33%">
                            
                        </td>
                        <td width="33%">
                            
                        </td>
                    </tr>
                    
                <asp:Panel ID="panelInformacionAduanera" runat="server" Visible ="false">
                        <tr>
                            <td colspan="3" class="item" style="line-height:20px;">
                                <strong>Nombre de la aduana:</strong> <asp:RequiredFieldValidator ID="valNombreAduana" runat="server" ControlToValidate="nombreaduana" ErrorMessage="Escriba el nombre de la aduana." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <telerik:RadTextBox ID="nombreaduana" Runat="server" Width="450px" CssClass="item">
                                </telerik:RadTextBox>
                                <br />
                                <strong>Fecha de pedimento:</strong> <asp:RequiredFieldValidator ID="valFechaPedimento" runat="server" ControlToValidate="fechapedimento" ErrorMessage="Selecciona la fecha del pedimento." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <telerik:RadDatePicker ID="fechapedimento" runat="server">
                                </telerik:RadDatePicker><br />
                                <strong>Número de pedimento:</strong> <asp:RequiredFieldValidator ID="valNumeroPedimento" runat="server" ControlToValidate="numeropedimento" ErrorMessage="Escriba el número de pedimento." SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                                <telerik:RadTextBox ID="numeropedimento" Runat="server" Width="450px" CssClass="item">
                                </telerik:RadTextBox>
                                <br />
                            </td>
                        </tr>
                </asp:Panel>
                
                <tr>
                   <td style="height:2px">
                     <asp:HiddenField ID="serie" runat="server" Value=""></asp:HiddenField>
                     <asp:HiddenField ID="folio" runat="server" Value="0"/>
                     <asp:HiddenField ID="tipoidF" runat="server" Value="0"/>
                     <asp:HiddenField ID="ImporteIva" runat="server" Value="0"/>
                     <asp:HiddenField id="Curp" runat="server" Value=""/>
                     <asp:HiddenField id="CadenaF" runat="server" Value=""/>
                     <asp:HiddenField ID="tmpIeps" runat="server" Value="0"/>
                  </td>
                </tr>
            </table>
            </fieldset>

        </asp:Panel>

        <br />
    <asp:Panel ID="PanelTernium" runat="server" Visible="False">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black" >
                <asp:Label runat="server" Font-Bold="true" CssClass="item">Ternium</asp:Label>
            </legend>
            <table style="width:100%">
                        <tr>
                            <td style="width:100%" class="item" style="line-height: 20px;">
                                <strong>Viaje:</strong>
                                <asp:RequiredFieldValidator ID="valTernium" runat="server" ControlToValidate="txtTernium"
                                    ErrorMessage="Escriba el nombre de la aduana." SetFocusOnError="true" ValidationGroup="off"></asp:RequiredFieldValidator><br />
                                <telerik:RadTextBox ID="txtTernium" runat="server" Width="20%" CssClass="item" style="margin-top:8px;">
                                </telerik:RadTextBox>
                                <br />
                            </td>
                        </tr>
                    </table>
        </fieldset>
    </asp:Panel>
    <br />
        <asp:Panel ID="panelItemsRegistration" runat="server" Visible="False">

            <fieldset style="padding:10px;">
                <asp:HiddenField ID="productoid" runat="server" />
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/portalcfd/images/concept.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblClientItems" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <br />
                <table width="900" cellspacing="0" cellpadding="1" border="0" align="center">
                    <tr>
                        <td  valign="bottom" class="item">
                            <strong>Buscar:</strong> <asp:TextBox ID="txtSearchItem" runat="server" CssClass="box" AutoPostBack="true"></asp:TextBox>&nbsp;presione enter después de escribir el código
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <telerik:RadGrid ID="gridResults" runat="server" Width="100%" ShowStatusBar="True"
                                AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                                Skin="Simple" Visible="False">
                                <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
                                    <Columns>
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Código</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%# eval("codigo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Descripción</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# eval("descripcion") %>' Width="400px" CssClass="box" TextMode="MultiLine" MaxLength="400" Height="80px"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>U. Medida</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnidad" runat="server" Text='<%# eval("unidad") %>'></asp:Label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Cant.</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtQuantity" runat="server" Skin="Default" Width="50px"
                                                    MinValue="0" Value='0'>
                                                    <NumberFormat DecimalDigits="4" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Precio Unit.</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtUnitaryPrice" runat="server" MinValue="0"  Value="0"
                                                    Skin="Default" Width="80px">
                                                    <NumberFormat DecimalDigits="4" GroupSeparator="," />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                         <telerik:GridTemplateColumn UniqueName="maniobra">
                                            <HeaderTemplate>Aplicar Para Maniobra</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ItemChkManiobra" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn UniqueName="predial">
                                            <HeaderTemplate>Cuenta Predial</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCuenta" runat="server" Width="100px" Skin="Default"></asp:TextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                       <telerik:GridTemplateColumn>
                                            <HeaderTemplate>Descuento.</HeaderTemplate>
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtDescuento" runat="server" Skin="Default" Width="60px"
                                                    MinValue="0" Value='0'>
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        
                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                            UniqueName="Add">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnAdd" runat="server" CommandArgument='<%# Eval("id") %>'
                                                    CommandName="cmdAdd" ImageUrl="~/portalcfd/images/action_add.gif" CausesValidation="False" ToolTip="Agregar producto comoo partida" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <br />
                            <asp:Button ID="btnCancelSearch" Visible="false" runat="server" CausesValidation="False" CssClass="item" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="900" cellspacing="0" cellpadding="1" border="0" align="center">
                <tr>
                    <td>
                        <br />
                        <telerik:RadGrid ID="itemsList" runat="server" Width="100%" ShowStatusBar="True"
                            AutoGenerateColumns="False" AllowPaging="False" GridLines="None"
                            Skin="Simple" Visible="False">
                            <MasterTableView Width="100%" DataKeyNames="id" Name="Items" AllowMultiColumnSorting="False">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="codigo" HeaderText="" UniqueName="codigo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="descripcion" HeaderText="" UniqueName="descripcion">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="unidad" HeaderText="" UniqueName="unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="cantidad" HeaderText="" UniqueName="cantidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="precio" HeaderText="" UniqueName="precio" DataFormatString="{0:C}" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="importe" HeaderText="" UniqueName="importe" DataFormatString="{0:C}" >
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" CausesValidation="False" />
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
                    <td><br /></td>
                </tr>
                </table>

            </fieldset>

        </asp:Panel>
        
        
        <br />
                <!-- :::::::::::::::::::::: Carta porte start :::::::::::::::::::::::::::::::: -->
        <telerik:RadAjaxPanel ID="panelCartaPorte" runat="server" Visible="false" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <fieldset style="padding: 10px;">
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image4" runat="server" ImageUrl="~/portalcfd/images/file.gif" ImageAlign="AbsMiddle" />&nbsp;&nbsp;<asp:Label
                    ID="lblTitleCartaPorte" runat="server" Font-Bold="true" CssClass="item" Text="Complemento carta porte"></asp:Label>
            </legend>
            
            <table style="width: 100%;">
                <tr>
                    <td colspan="4">
                    <br />                    
                        <div class="title">
                            <label >
                                Horarios
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;">
                        <asp:Panel ID="Panel4" runat="server">
                            <table style="width: 95%;">
                                <tr>
                                    <td >
                                        <div style="width: 100%; display: flex;">
                                            <div style="width: 25%;">
                                                <asp:Label ID="lblFechaHoraSalida" runat="server" CssClass="item" Font-Bold="True"
                                                    Text="Fecha y hora de salida:*" />
                                                <asp:RequiredFieldValidator ID="valFechaHoraSalida" runat="server" InitialValue=""
                                                    CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtFechaHoraSalida"
                                                    SetFocusOnError="true" ForeColor="red" />
                                                <br />
                                                <telerik:RadDateTimePicker ID="txtFechaHoraSalida" runat="server" Width="80%" Style="margin-top: 6px;"
                                                    Culture="en-US">
                                                </telerik:RadDateTimePicker>
                                            </div>
                                            <div style="width: 20%;">
                                            <asp:Label ID="Label12" runat="server" CssClass="item" Font-Bold="True"
                                                    Text="Unidad Peso:" />
                                                <br />
                                                <telerik:RadComboBox DataValueField="codigo" DataTextField="descripcion" 
                                                    ID="cmbUnidadPeso" Skin="Default" runat="server" Width="70%"  Style="margin-top: 8px;" ExpandDirection="Down" >
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="KGM - Kilogramo" Value="KGM" />
                                                        <telerik:RadComboBoxItem Text="TNE - Tonelada" Value="TNE" />                                                    
                                                        <telerik:RadComboBoxItem Text="LTR - Litro" Value="LTR" />
                                                    </Items>
                                                  </telerik:RadComboBox>
                                            </div>
                                            <div style="width: 10%;">
                                            <asp:Label ID="Label13" runat="server" CssClass="item" Font-Bold="True"
                                                    Text="Peso Neto Total:" />
                                                <br />
                                                <telerik:RadTextBox ID="txtPesoNetoTotal" runat="server" NumberFormat-DecimalDigits="2"
                                                    Style="margin-top: 6px;" MaxValue="99999" Width="60%" MinValue="0.01" />
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    <br />                    
                        <div class="title">
                            <label >
                                Domicilio de Origen
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr >
                    <td style="width: 100%;Display:flex;">
                        <div style="width: 19%;margin-top:4px;"> 
                                 <asp:Label ID="Label9" runat="server" CssClass="item" Font-Bold="True"
                                        Text="Origen-Catalogo:" />                    
                                <asp:DropDownList ID="cmbOrigenCatalogo" runat="server" CssClass="box" Width="80%" Style="margin-top: 14px;"
                                    DataValueField="id" DataTextField="nombre" AutoPostBack="true" />
                        </div>
                        <div style="width:19%;">
                            <asp:Label ID="lblRFCRemitente" runat="server" CssClass="item" Font-Bold="True" Text="RFC del remitente:" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="item"
                                runat="server" ControlToValidate="txtRFCRemitente" Text="Inválido" ForeColor="red"
                                SetFocusOnError="True" ValidationExpression="^([a-zA-Z]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                            <br />
                            <telerik:RadTextBox ID="txtRFCRemitente" runat="server" Width="78%" Style="margin-top: 12px;
                                text-transform: uppercase;" MaxLength="13" Enabled="false" />
                        </div>
                        <div style="width: 42%;">
                            <asp:Label ID="lblNombreRemitente" runat="server" CssClass="item" Font-Bold="True"
                                Text="Nombre del remitente:" />
                            <br />
                            <telerik:RadTextBox ID="txtNombreRemitente" runat="server" Width="80%" Style="margin-top: 15px;
                                text-transform: uppercase;" MaxLength="256" Enabled="false" />
                        </div>
                       
                    </td>
                </tr>
                
                <tr><td> &nbsp; </td></tr>
                </table>
                <!-- ::::::::::::::::: Domicilio origen-->
                    <asp:Panel ID="Panel1" runat="server"  >
                     <table style="width: 95%;" >
                
                    <tr>
                    <td >
                    <div style="width: 100%; display: flex;">
                         <div style="width:20%;">
                            <asp:Label ID="lbltxtIDUbicacion_or" runat="server" CssClass="item" Font-Bold="True" Text="ID:" />                        
                            <br />
                            <telerik:RadTextBox ID="txtIDUbicacion_or" runat="server" Width="78%" Style="margin-top: 8px;
                                text-transform: uppercase;" MaxLength="8" Text="OR" Enabled="false" />
                        </div>
                        <div style="width: 20%;">
                            <asp:Label ID="lblPais_or" runat="server" CssClass="item" Font-Bold="True" Text="Pais:*" />
                            <asp:RequiredFieldValidator ID="valPais_or" runat="server" InitialValue="0" CssClass="item"
                                ErrorMessage="Requerido." ControlToValidate="cmbPais_or" SetFocusOnError="true"
                                ForeColor="red" /><br />
                            <asp:DropDownList ID="cmbPais_or" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                                DataValueField="clave" DataTextField="descripcion" Enabled ="false" />
                        </div>
                        <div style="width: 20%;">
                            <asp:Label ID="lblCodigoPostal_or" runat="server" CssClass="item" Font-Bold="True"
                                Text="C. P.:*" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtCodigoPostal_or"
                                SetFocusOnError="true" ForeColor="red" />
                            <asp:RegularExpressionValidator ID="valeCodigoPostal_or" CssClass="item" runat="server"
                                ControlToValidate="txtCodigoPostal_or" Text="Inválido" ForeColor="red" SetFocusOnError="True"
                                ValidationExpression="[0-9]{5}" />
                            <br />
                            <telerik:RadTextBox ID="txtCodigoPostal_or" runat="server" Width="80%" Style="margin-top: 12px;"
                                MaxLength="5" AutoPostBack="true" Enabled="false" />
                        </div>
                        <div style="width: 20%;">
                            <asp:Label ID="lblEstado_or" runat="server" CssClass="item" Font-Bold="True" Text="Estado:*" />
                            <asp:RequiredFieldValidator ID="valEstado_or" runat="server" InitialValue="0" CssClass="item"
                                ErrorMessage="Requerido." ControlToValidate="cmbEstado_or" SetFocusOnError="true"
                                ForeColor="red" />
                            <br />
                            <asp:DropDownList ID="cmbEstado_or" runat="server" CssClass="box" Width="80%" Style="margin-top: 10px;"
                                DataValueField="clave" DataTextField="nombre" AutoPostBack="true" Enabled="false" />
                        </div>
                        <div style="width: 20%;">
                                <asp:Label ID="lblMunicipio_or" runat="server" CssClass="item" Font-Bold="True" Text="Municipio:*" />
                                <asp:RequiredFieldValidator ID="valMunicipio_or" runat="server" InitialValue="0"
                                    CssClass="item" ErrorMessage="Requerido." ControlToValidate="cmbMunicipio_or"
                                    SetFocusOnError="true" ForeColor="red" /><br />
                                <asp:DropDownList ID="cmbMunicipio_or" runat="server" Width="80%" Style="margin-top: 12px;"
                                    MaxLength="120" DataValueField="clave" DataTextField="nombre" CssClass="box" Enabled="false"/>
                            </div>
                        
                    </div>
                    <br />
                    <br />
                        <div style="width: 100%; display: flex;">                            
                            <div style="width: 20%;">
                                <asp:Label ID="lblColonia_or" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:*" />
                                <asp:RequiredFieldValidator ID="valColonia_or" runat="server" InitialValue="0" CssClass="item"
                                    ErrorMessage="Requerido." ControlToValidate="cmbColonia_or" SetFocusOnError="true"
                                    ForeColor="red" />
                                <br />
                                <asp:DropDownList ID="cmbColonia_or" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                                    DataValueField="clave" DataTextField="nombre" Enabled="false"/>
                            </div>
                            <div style="display: none">
                                <asp:Label ID="lblLocalidad_or" runat="server" CssClass="item" Font-Bold="True" Text="Localidad:"  />
                                <asp:DropDownList ID="txtLocalidad_or" runat="server" Width="80%" Style="margin-top: 12px;"
                                    MaxLength="120" DataValueField="clave" DataTextField="nombre" CssClass="box" />
                            </div>
                            <div style="width: 20%;">
                                <asp:Label ID="lblCalle_or" runat="server" CssClass="item" Font-Bold="True" Text="Calle:*" />
                                <asp:RequiredFieldValidator ID="valCalle_or" runat="server" InitialValue="" CssClass="item"
                                    ErrorMessage="Requerido." ControlToValidate="txtCalle_or" SetFocusOnError="true" />
                                <br />
                                <telerik:RadTextBox ID="txtCalle_or" runat="server" Width="90%" Style="margin-top: 8px;"
                                    MaxLength="100" Enabled="false"/>
                            </div>
                            <div style="width: 10%;">
                                    <asp:Label ID="lblNumeroExterior_or" runat="server" CssClass="item" Font-Bold="True" Text="No. Exterior:" />
                                    <telerik:RadTextBox ID="txtNumeroExterior_or" runat="server" Width="80%" Style="margin-top: 12px;" MaxLength="20" Enabled="false"/>
                            </div>
                            <div style="width: 10%;">
                                    <asp:Label ID="lblNumeroInterior_or" runat="server" CssClass="item" Font-Bold="True" Text="No. Interior:" />
                                    <telerik:RadTextBox ID="txtNumeroInterior_or" runat="server" Width="80%" Style="margin-top: 12px;" MaxLength="20" Enabled="false" />
                            </div>
                            <div style="width: 36%;">
                                <asp:Label ID="lblReferencia_or" runat="server" CssClass="item" Font-Bold="True" Text="Referencia:"  /><br />
                                <telerik:RadTextBox ID="txtReferencia_or" runat="server" Width="100%" Style="margin-top: 12px;" MaxLength="200" Enabled="false" />
                            </div>
                        </div>
                    </tr>
                  </table>
                </asp:Panel>
               
                <table style="width: 100%;">
                <tr><td>&nbsp;</td></tr>                
                <tr>
                    <td colspan="4">
                    <br />
                        <div class="title">
                            <label>
                                Domicilio de Destino
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr> 
                
                    <td class="df-100">
                        <div class="df-100">
                            <div style="width: 50%;margin-top:4px;"> 
                                <div style="width: 100%;margin-top:4px;">
                                    <asp:Label ID="lblDestinoCatalogo" runat="server" CssClass="item" Font-Bold="True" Text="Destino:" />                                
                                    <asp:RequiredFieldValidator ID="valDestinoCatalogo" runat="server" InitialValue="0"
                                            CssClass="item" ErrorMessage="Requerido." ControlToValidate="cmbDestinoCatalogo"
                                            SetFocusOnError="true" ValidationGroup="destino"></asp:RequiredFieldValidator>
                                </div>
                                <div class="df-100 mt-p5 ">
                                 <asp:DropDownList ID="cmbDestinoCatalogo" runat="server" CssClass="box" Width="65%" 
                                        DataValueField="id" DataTextField="nombre" AutoPostBack="true" />
                                       
                                     <asp:Button  ID="btnAddDestino" runat="server" Text="Agregar Destino" CssClass="boton ml-p5"
                                        CausesValidation="true" TabIndex="6" ValidationGroup="destino">
                                    </asp:Button>
                                </div>
                                <asp:Panel runat="server" ID="PanelDireccionDestino" Visible="false">
                                <div class="df-100 mt-p5">
                                    <asp:Label ID="lblNombreDestinatario" runat="server" CssClass="item " Font-Bold="True"
                                        Text="Nombre del Destinatario:" />
                                     <asp:Label ID="txtNombreDestinatario" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                <div class="df-100 mt-p5">
                                    <asp:Label ID="lblRFCDestinatario" runat="server" CssClass="item " Font-Bold="True"
                                        Text="RFC del Destinatario:" />
                                     <asp:Label ID="txtRFCDestinatario" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                <div class="df-100">
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lbltxtIDUbicacion_des" runat="server" CssClass="item " Font-Bold="True"
                                            Text="ID:" />
                                         <asp:Label ID="txtIDUbicacion_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblPais_des" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Pais:" />
                                         <asp:Label ID="txtPais_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                </div>
                                <div class="df-100">
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblCodigoPostal_des" runat="server" CssClass="item " Font-Bold="True"
                                            Text="C.P:" />
                                         <asp:Label ID="txtCodigoPostal_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblEstado_des" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Estado:" />
                                         <asp:Label ID="txtEstado_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                </div>
                                <div class="df-100">
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblColonia_des" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Colonia:" />
                                         <asp:Label ID="txtColonia_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblMunicipio_des" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Municipio:" />
                                         <asp:Label ID="txtMunicipio_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                   
                                </div>
                                <div class="df-50 mt-p5 dnone">
                                    <asp:Label ID="lblLocalidad_des" runat="server" CssClass="item " Font-Bold="True"
                                        Text="Calle:" />
                                     <asp:Label ID="txtLocalidad_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                <div class="df-50 mt-p5">
                                    <asp:Label ID="lblCalle_des" runat="server" CssClass="item " Font-Bold="True"
                                        Text="Calle:" />
                                     <asp:Label ID="txtCalle_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                <div class="df-100">
                                    
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblNumeroExterior_des" runat="server" CssClass="item " Font-Bold="True"
                                            Text="No. Exterior:" />
                                         <asp:Label ID="txtNumeroExterior_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblNumeroInterior_des" runat="server" CssClass="item " Font-Bold="True"
                                            Text="No. Interior:" />
                                         <asp:Label ID="txtNumeroInterior_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                </div>     
                                <div class="df-50 mt-p5">
                                    <asp:Label ID="lblReferencia_des" runat="server" CssClass="item " Font-Bold="True"
                                        Text="Referencia:" />
                                     <asp:Label ID="txtReferencia_des" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                <div class="df-100 mt-p5">
                                    <div style="width: 60%;">
                                        <asp:Label ID="lblFechaHoraProgLlegada_d" runat="server" CssClass="item" Font-Bold="True"
                                            Text="Fecha y hora de llegada:*" />
                                        <asp:RequiredFieldValidator ID="valFechaHoraProgLlegada" runat="server" InitialValue=""
                                            CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtFechaHoraProgLlegada"
                                            SetFocusOnError="true" ValidationGroup="destino" />
                                        <br />
                                        <%--<telerik:RadTextBox  InputType="DateTimeLocal" />--%>
                                        <telerik:RadDateTimePicker runat="server" ID="txtFechaHoraProgLlegada" Width="80%"
                                            Style="margin-top: 6px;" Culture="en-US">
                                        </telerik:RadDateTimePicker>
                                    </div>
                                    <div style="width: 40%;">
                                        <asp:Label ID="lblDistanciaRecorrida" runat="server" CssClass="item" Font-Bold="True"
                                            Text="Distancia (KM) :*" tooltip="Distancia recorrida" />
                                        <asp:RequiredFieldValidator ID="valDistanciaRecorrida" runat="server" InitialValue=""
                                            CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtDistanciaRecorrida_d"
                                            SetFocusOnError="true" ValidationGroup="destino"></asp:RequiredFieldValidator><br />
                                        <telerik:RadNumericTextBox ID="txtDistanciaRecorrida_d" runat="server" NumberFormat-DecimalDigits="2"
                                            Style="margin-top: 6px;" MaxValue="99999" Width="60%" MinValue="0.01" />
                                    </div>
                                    
                                </div>
                                </asp:Panel>
                            </div>
                            <div style="width: 50%;margin-top:4px;">
                                <telerik:RadGrid ID="destinoslist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    GridLines="None" AllowSorting="true" PageSize="10" ShowStatusBar="True" Width="100%"
                                    RenderMode="Lightweight">
                                    <PagerStyle Mode="NumericPages" />
                                    <MasterTableView NoMasterRecordsText="No se han agregado destinos" Width="100%"
                                        PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowMultiColumnSorting="true" AllowSorting="true"
                                        CommandItemDisplay="none">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="id" HeaderText="Item" UniqueName="descripcion"
                                                HeaderStyle-Width="40" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="nombre_des" HeaderText="Nombre" UniqueName="identificador"
                                                HeaderStyle-Width="30">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="fechaHoraLlegada" HeaderText="Fecha" UniqueName="nombre"
                                                HeaderStyle-Width="30">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="distanciaRecorrida" HeaderText="Distancia(Km)" UniqueName="nombre"
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
                  </table>
                <table>
                
                               
                <tr><td> &nbsp; </td></tr>
                <tr>
                <td colspan="4">
                <asp:UpdatePanel ID="panelMercacnia" runat ="server" UpdateMode="Conditional">
                <ContentTemplate>
                <tr>
                    <td  colspan="4">
                    <br />
                        <div class="title">
                            <label>
                                Mercancías
                            </label>
                        </div> 
                    </td>
                </tr> 
                <tr>
                <td colspan="4">
                <asp:Panel runat="server" ID="PaneltblMercancias">
                <br />
                    <telerik:RadGrid ID="tblMercancias" runat="server" AllowPaging="True" AutoGenerateColumns="False" GridLines="None" AllowSorting="true" PageSize="10" ShowStatusBar="True" Width="100%" RenderMode="Lightweight" >
                    <PagerStyle Mode="NumericPages" />
                    <MasterTableView NoMasterRecordsText="No se han agregado mercancias" Width="100%" PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowMultiColumnSorting="true" AllowSorting="true" CommandItemDisplay="none">
                    <Columns>
                    <telerik:GridBoundColumn DataField="id" HeaderText="Item" UniqueName="descripcion" HeaderStyle-Width="40" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="BienesTransp" HeaderText="C&oacute;digo" UniqueName="descripcion" HeaderStyle-Width="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Descripcion" HeaderText="Descripción" UniqueName="descripcion" HeaderStyle-Width="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Cantidad" HeaderText="Cantidad" UniqueName="descripcion" HeaderStyle-Width="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="PesoEnKg" HeaderText="Peso (KG)" UniqueName="descripcion" HeaderStyle-Width="40"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ClaveUnidad" HeaderText="Unidad" UniqueName="descripcion" HeaderStyle-Width="40" ></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="MaterialPeligroso" HeaderText="Material Peligroso" UniqueName="MaterialPeligroso" HeaderStyle-Width="30" ></telerik:GridBoundColumn>
                    
                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center"
                                        UniqueName="Delete" HeaderStyle-Width="40">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>'
                                                CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" CausesValidation="false"/>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                    </Columns>
                    </MasterTableView>
                    </telerik:RadGrid>
                </asp:Panel>
                <br />
                </td>
                </tr>
               
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblBienesTransp" runat="server" CssClass="item" Font-Bold="True" Text="Clave de producto:*" />
                        <asp:RequiredFieldValidator ID="valBienesTransp" runat="server" InitialValue=""  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="cmbBienesTransp" SetFocusOnError="true" ForeColor="red" ValidationGroup ="mercancia" />
                        <br />                       
                       <telerik:RadComboBox TabIndex="-1"  OnClientBlur ="OnClientBlurHandler" EnableTextSelection="true"  MarkFirstMatch="true" DataValueField="codigo" DataTextField="descripcion" RenderMode="Lightweight" CausesValidation="false" 
                                            ID="cmbBienesTransp" Skin="Default" runat="server" Width="90%" EmptyMessage="Busqueda por palabra clave" EnableLoadOnDemand="true" Style="margin-top: 12px;" ExpandDirection="Down" AutoPostBack="true"  />
                    </td>                    
                    <td colspan="2">
                        <asp:Label ID="lblCantidad" runat="server" CssClass="item" Font-Bold="True" Text="Cantidad:*" />
                        <asp:RequiredFieldValidator ID="valCantidad" runat="server" InitialValue=""  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="txtCantidad" SetFocusOnError="true" ForeColor="red" ValidationGroup ="mercancia" /><br />
                        <telerik:RadNumericTextBox ID="txtCantidad" runat="server" NumberFormat-DecimalDigits="0" Style="margin-top: 12px;" Type="Number" Width="85%" ValidationGroup="mercancia" />
                    </td>
                   
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <asp:Label ID="lblDescripcion" runat="server" CssClass="item" Font-Bold="True" Text="Descripción:" /><br />
                        <telerik:RadTextBox ID="txtDescripcion" runat="server" Width="85%" Style="margin-top: 12px;" MaxLength="1000" />
                    </td>
                    
                    <td colspan="1">
                    <asp:Label ID="lblPesoEnKg" runat="server" CssClass="item" Font-Bold="True" Text="Peso (KG):*" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="txtPesoEnKg" SetFocusOnError="true" ForeColor="red" ValidationGroup ="mercancia" /><br />
                        <telerik:RadNumericTextBox ID="txtPesoEnKg" runat="server" NumberFormat-DecimalDigits="2" Style="margin-top: 12px;" Type="Number" Width="70%"/>
                    </td>
                    <td colspan="2">
                    <asp:Label ID="lblClaveUnidad" runat="server" CssClass="item" Font-Bold="True" Text="Unidad de medida:*" />
                    <asp:RequiredFieldValidator ID="valClaveUnidad" runat="server" InitialValue=""  CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="cmbClaveUnidad" SetFocusOnError="true" ForeColor="red" ValidationGroup ="mercancia" />
                    <br />                        
                        <telerik:RadComboBox OnClientBlur ="OnClientBlurHandler" EnableTextSelection="true"  MarkFirstMatch="true" DataValueField="codigo" DataTextField="descripcion" RenderMode="Lightweight" 
                                            ID="cmbClaveUnidad" Skin="Default" runat="server" Width="85%" EmptyMessage="Busqueda por palabra clave" EnableLoadOnDemand="true" Style="margin-top: 12px;" ExpandDirection="Down"  />
                    </td>                     
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="visibility: collapse;">
                    <td colspan="1">
                        <asp:Label ID="lblValorMercancia" runat="server" CssClass="item" Font-Bold="True"
                            Text="Valor de la mercancias:" /><br />
                        <telerik:RadNumericTextBox ID="txtValorMercancia" runat="server" NumberFormat-DecimalDigits="2"
                            Style="margin-top: 12px;" Type="Currency" Width="85%" />
                    </td>
                    <td colspan="1">
                        <asp:Label ID="lblMoneda" runat="server" CssClass="item" Font-Bold="True" Text="Moneda:" /><br />
                        <asp:DropDownList ID="cmbMoneda" runat="server" CssClass="box" Width="85%" Style="margin-top: 12px;"
                            DataValueField="clave" DataTextField="descripcion" />
                    </td>                    
                    <td colspan="2">                   
                    </td>
                </tr>
                <tr>
                    <td>
                       &nbsp;
                    </td>
                </tr>
                <tr>
                <td colspan="4">
                    <asp:Panel ID="PanelMaterialPeligroso" runat="server" Visible="false">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="lblMaterialPeligroso" runat="server" CssClass="item" Font-Bold="True"
                                        Text="Material peligroso:*" /><br />
                                    <asp:DropDownList ID="cmbMaterialPeligroso" runat="server" CssClass="box" Width="80%" AutoPostBack="true"
                                        Style="margin-top: 14px;">
                                        <asp:ListItem Value="Si"> Si </asp:ListItem> 
                                        <asp:ListItem Value="No"> No </asp:ListItem>                                        
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 40%">
                                    <asp:Label ID="lblCveMaterialPeligroso" runat="server" CssClass="item" Font-Bold="True"
                                        Text="Tipo de material peligroso:*" />
                                    <asp:RequiredFieldValidator ID="valCveMaterialPeligroso" runat="server" InitialValue=""  CssClass="item"
                                         ErrorMessage="Requerido." ControlToValidate="cmbCveMaterialPeligroso" SetFocusOnError="true" ForeColor="red" ValidationGroup="mercancia" />    
                                        <br />
                                        <telerik:RadComboBox OnClientBlur ="OnClientBlurHandler" EnableTextSelection="true"  MarkFirstMatch="true" DataValueField="clave" DataTextField="descripcion" RenderMode="Lightweight" CausesValidation="false" 
                                            ID="cmbCveMaterialPeligroso" Skin="Default" runat="server" Width="90%" EmptyMessage="Busqueda por palabra clave" EnableLoadOnDemand="true"  Style="margin-top: 12px;" ExpandDirection="Down" AutoPostBack="false"  />
                                </td>
                                <td style="Width: 40%">
                                    <asp:Label ID="lblEmbalaje" runat="server" CssClass="item" Font-Bold="True" Text="Embalaje:*" />
                                    <asp:RequiredFieldValidator ID="valEmbalaje" runat="server" InitialValue="0"  CssClass="item"
                                        ErrorMessage="Requerido." ControlToValidate="cmbEmbalaje" SetFocusOnError="true" ForeColor="red" ValidationGroup="mercancia" />
                                    <br />
                                    <asp:DropDownList ID="cmbEmbalaje" runat="server" CssClass="box" Width="92%" Style="margin-top: 12px;"
                                        DataValueField="clave" DataTextField="descripcion" />
                                </td>
                                <td style="width: 0%;Display:none">
                                    <asp:Label ID="lblDescripEmbalaje" runat="server" CssClass="item" Font-Bold="True"
                                        Text="Descripcion del embalaje:" /><br />
                                    <telerik:RadTextBox ID="cmbDescripEmbalaje" runat="server" Width="80%" Style="margin-top: 12px;"
                                        MaxLength="100" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>                      
                </tr>                
                <tr>
                        <td colspan="4" style="text-align: end; padding-right: 50px;">
                            <br />
                            <asp:RequiredFieldValidator ID="valMercancias" runat="server" InitialValue=""  CssClass="item"
                            ErrorMessage="Por favor agrege una o más mercancias." ControlToValidate="cmbClient" SetFocusOnError="true" ForeColor="red" />
                            <asp:FileUpload ID="FileMercancia" runat="server" 
                                CausesValidation="false" ToolTip="selecciona un archivo .csv" c>
                            </asp:FileUpload>&nbsp;&nbsp;
                            
                            <asp:Button  ID="btnAddMercancia" runat="server" Text="Agregar mercancia"
                                CausesValidation="true" TabIndex="6" ValidationGroup="mercancia">
                            </asp:Button>
                            <asp:HiddenField runat="server" ID="InsertOrUpdate"  Value ="0"/>
                        </td>
                    </tr> 
                </ContentTemplate>
                <Triggers>
                <asp:PostBackTrigger ControlID="btnAddMercancia" />
            </Triggers>
                </asp:UpdatePanel>
                </td>
                </tr>             
                               
                              
                  
                <!-- here start-->
                <tr>
                    <td colspan="4">
                    <br />
                        <div class="title">
                            <label>
                                Autotransporte
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div style="width: 100%; display: flex;">
                            <div style="width: 50%; padding-top: 6px;">
                                <asp:Label ID="Label8" runat="server" CssClass="item" Font-Bold="true" Text="Autotransporte-catalogo:*"
                                    Style="margin-top: 4px;"></asp:Label>
                                <asp:DropDownList ID="cmbAutotransporte" runat="server" CssClass="box" Width="95%" Style="margin-top: 10px;" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div style="width: 50%;">
                                <asp:Label ID="lblPermSCT" runat="server" CssClass="item" Font-Bold="True" Text="Clave del permiso SCT:*" />
                                <asp:RequiredFieldValidator ID="valPermSCT" runat="server" InitialValue="0" CssClass="item"
                                    ErrorMessage="Requerido." ControlToValidate="cmbPermSCT" SetFocusOnError="true"
                                    ForeColor="red" />
                                <br />
                                <asp:DropDownList ID="cmbPermSCT" runat="server" CssClass="box" Width="90%" Style="margin-top: 12px;"
                                    DataValueField="clave" DataTextField="descripcion" Enabled="false"/>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                <td colspan="4">
                <div style="width: 100%; display: flex;">
                    <div style="width: 33%;">
                    <asp:Label ID="lblNumPermisoSCT" runat="server" CssClass="item" Font-Bold="True"
                                    Text="Numero de permiso SCT:*" />
                                <asp:RequiredFieldValidator ID="valNumPermisoSCT" runat="server" InitialValue=""
                                    CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtNumPermisoSCT"
                                    SetFocusOnError="true" ForeColor="red" />
                                <br />
                                <telerik:RadTextBox ID="txtNumPermisoSCT" runat="server" Width="90%" Style="margin-top: 12px;
                                    text-transform: uppercase;" MaxLength="50" Enabled="false"/>
                    </div>
                    <div style="width: 33%;">
                     <asp:Label ID="lblNombreAseg" runat="server" CssClass="item" Font-Bold="True" Text="Nombre de la aseguradora:*" />
                        <asp:RequiredFieldValidator ID="valNombreAseg" runat="server" InitialValue="" CssClass="item"
                            ErrorMessage="Requerido." ControlToValidate="txtNombreAseg" SetFocusOnError="true"
                            ForeColor="red" />
                        <asp:RegularExpressionValidator ID="valeNombreAseg" CssClass="item" runat="server"
                            ControlToValidate="txtNombreAseg" Text="Inválido" ForeColor="red" SetFocusOnError="True"
                            ValidationExpression="[^|]{3,50}" />
                        <br />
                        <telerik:RadTextBox ID="txtNombreAseg" runat="server" Width="90%" Style="margin-top: 12px;
                            text-transform: uppercase;" MaxLength="50" Enabled="false"/>
                    </div>
                    <div style="width: 33%;">
                    <asp:Label ID="lblNumPolizaSeguro" runat="server" CssClass="item" Font-Bold="True"
                            Text="Poliza del seguro:*" />
                        <asp:RequiredFieldValidator ID="valNumPolizaSeguro" runat="server" InitialValue=""
                            CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtNumPolizaSeguro"
                            SetFocusOnError="true" ForeColor="red" />
                        <asp:RegularExpressionValidator ID="valeNumPolizaSeguro" CssClass="item" runat="server"
                            ControlToValidate="txtNumPolizaSeguro" Text="Inválido" ForeColor="red" SetFocusOnError="True"
                            ValidationExpression="[^|]{3,50}" />
                        <br />
                        <telerik:RadTextBox ID="txtNumPolizaSeguro" runat="server" Width="90%" Style="margin-top: 12px;
                            text-transform: uppercase;" MaxLength="30" Enabled="false"/>
                    </div>
                </div>
                </td>
                    
                </tr>
                <asp:Panel ID="panelMedAmbiente" runat="server" Visible="false">
                    <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                <td colspan="4">
                <div style="width: 100%; display: flex;">
                    
                    <div style="width: 33%;">
                     <asp:Label ID="Label11" runat="server" CssClass="item" Font-Bold="True" Text="Aseguradora medio ambiente:*" />
                     <asp:RequiredFieldValidator ID="valAseguraMedAmbiente" runat="server" InitialValue=""
                            CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtAseguraMedAmbiente"
                            SetFocusOnError="true" ForeColor="red" />
                        <telerik:RadTextBox ID="txtAseguraMedAmbiente" runat="server" Width="90%" Style="margin-top: 12px;
                            text-transform: uppercase;" MaxLength="50" Enabled="false" />
                    </div>
                    <div style="width: 33%;">
                    <asp:Label ID="Label14" runat="server" CssClass="item" Font-Bold="True"
                            Text="Poliza medio ambiente:*" />
                     <asp:RequiredFieldValidator ID="valPolizaMedAmbiente" runat="server" InitialValue=""
                            CssClass="item" ErrorMessage="Requerido." ControlToValidate="txtPolizaMedAmbiente"
                            SetFocusOnError="true" ForeColor="red" />
                        <telerik:RadTextBox ID="txtPolizaMedAmbiente" runat="server" Width="90%" Style="margin-top: 12px;
                            text-transform: uppercase;" MaxLength="30" Enabled="false" />
                    </div> 
                </div>
                </td>
                    
                </tr>
                </asp:Panel>
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
                    <td colspan="4">
                        <div class="df-100">
                            <div class="w-50">
                                <div class="w-100">
                                    <asp:Label ID="lblConfigVehicular" runat="server" CssClass="item" Font-Bold="True"
                                        Text="Clave del autotransporte:*" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                        CssClass="item" ErrorMessage="Requerido." ControlToValidate="cmbConfigVehicular"
                                        SetFocusOnError="true" ForeColor="red" />
                                    <br />
                                    <asp:DropDownList ID="cmbConfigVehicular" runat="server" CssClass="box" Width="95%"
                                        Style="margin-top: 12px;" DataValueField="clave" DataTextField="descripcion"
                                        Enabled="false" />
                                </div>
                                <br />
                                <div class="df-100">
                                    <div class="w-50">
                                        <asp:Label ID="lblPlacaVM" runat="server" CssClass="item" Font-Bold="True" Text="Placa vehicular:*" />
                                        <asp:RequiredFieldValidator ID="valPlacaVM" runat="server" InitialValue="" CssClass="item"
                                            ErrorMessage="Requerido." ControlToValidate="txtPlacaVM" SetFocusOnError="true"
                                            ForeColor="red" />
                                        <asp:RegularExpressionValidator ID="valePlacaVM" CssClass="item" runat="server" ControlToValidate="txtPlacaVM"
                                            Text="Inválido" ForeColor="red" SetFocusOnError="True" ValidationExpression="[^(?!.*\s)-]{6,7}"></asp:RegularExpressionValidator>
                                        <telerik:RadTextBox ID="txtPlacaVM" runat="server" Width="90%" Style="margin-top: 12px;
                                            text-transform: uppercase;" MaxLength="7" Enabled="false" />
                                    </div>
                                    <div class="w-50">
                                        <asp:Label ID="lblAnioModeloVM" runat="server" CssClass="item" Font-Bold="True" Text="Año:*" />
                                        <asp:RequiredFieldValidator ID="valAnioModeloVM" runat="server" InitialValue="" CssClass="item"
                                            ErrorMessage="Requerido." ControlToValidate="txtPlacaVM" SetFocusOnError="true"
                                            ForeColor="red" />
                                        <asp:RegularExpressionValidator ID="valeAnioModeloVM" CssClass="item" runat="server"
                                            ControlToValidate="txtAnioModeloVM" Text="Inválido" ForeColor="red" SetFocusOnError="True"
                                            ValidationExpression="(19[0-9]{2}|20[0-9]{2})"></asp:RegularExpressionValidator>
                                        <br />
                                        <telerik:RadMaskedTextBox RenderMode="Lightweight" ID="txtAnioModeloVM" runat="server"
                                            Mask="####" Width="40%" Style="margin-top: 12px;" Enabled="false">
                                        </telerik:RadMaskedTextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="w-50">
                                <asp:Label ID="Label98" runat="server" CssClass="item" Font-Bold="True" Text="Remolques:" />
                               
                                <telerik:RadGrid ID="remolqueslist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                GridLines="None" AllowSorting="true" PageSize="10" ShowStatusBar="True" Width="100%"
                                RenderMode="Lightweight" CssClass="mt-1">
                                <PagerStyle Mode="NumericPages" />
                                <MasterTableView NoMasterRecordsText=" Sin remolques" Width="100%"
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
                                    </Columns>
                                </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </td>
                </tr>
<%--                <tr>
                    <td colspan="4">
                    <br />
                        <div style="width: 100%; border-top: 0px solid; color: #131010; text-align: center;
                            padding: 5px 0px;">
                            <label>
                                Remolque
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                <td colspan="1">
                <asp:Label ID="lblSubTipoRem" runat="server" CssClass="item" Font-Bold="True" Text="Subtipo de remolque:" /><br />
                    <asp:DropDownList ID="cmbSubTipoRem" runat="server" CssClass="box" Width="80%" Style="margin-top: 12px;"
                        DataValueField="clave" DataTextField="descripcion" />
                </td>
                    <td colspan="1">
                        <asp:Label ID="lblPlaca" runat="server" CssClass="item" Font-Bold="True" Text="Placa del remolque:" />
                        <asp:RegularExpressionValidator ID="valePlaca" CssClass="item" runat="server" ControlToValidate="txtPlaca"
                            Text="Formato no válido" ForeColor="#c10000" SetFocusOnError="True" ValidationExpression="[^(?!.*\s)-]{6,7}"></asp:RegularExpressionValidator>
                        <telerik:RadTextBox ID="txtPlaca" runat="server" Width="80%" Style="margin-top: 12px;"
                            MaxLength="7" />
                    </td>
                </tr>
               --%>
                 <tr>
                    <td colspan="4">
                    <br />
                        <div class="title">
                            <label>
                               Figura Transporte
                            </label>
                        </div>
                        <br />
                    </td>
                </tr>
                <tr>
                <td colspan="4" class="">
                        <div  class="df-100">
                            <div style="width: 50%;margin-top:4px;"> 
                                <div style="width: 100%;margin-top:4px;">
                                    <asp:Label ID="lblOperadorCatalogo" runat="server" CssClass="item" Font-Bold="True" Text="Figura:" />                                
                                    <asp:RequiredFieldValidator ID="valOperadorCatalogo" runat="server" InitialValue="0"
                                            CssClass="item" ErrorMessage="Requerido." ControlToValidate="cmbOperador"
                                            SetFocusOnError="true" ValidationGroup="Operador"></asp:RequiredFieldValidator>
                                </div>
                                <div class="df-100 mt-p5 ">
                                 <asp:DropDownList ID="cmbOperador" runat="server" CssClass="box" Width="65%" 
                                        DataValueField="id" DataTextField="nombre" AutoPostBack="true" />
                                       
                                     <asp:Button  ID="btnAddOperador" runat="server" Text="Agregar Operador" CssClass="boton ml-p5"
                                        CausesValidation="true" TabIndex="6" ValidationGroup="Operador">
                                    </asp:Button>
                                </div>
                                <asp:Panel runat ="server" ID="PanelDireccionOperador" Visible="false">
                                <div class="df-100 mt-p5">
                                    <asp:Label ID="lblNombreOperador" runat="server" CssClass="item " Font-Bold="True"
                                        Text="Nombre:" />
                                     <asp:Label ID="txtNombreOperador" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                <div class="df-100">
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblRFCOperador" runat="server" CssClass="item " Font-Bold="True"
                                            Text="RFC:" />
                                         <asp:Label ID="txtRFCOperador" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="Label99" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Tipo Figura:" />
                                         <asp:Label ID="txtTipoFigura" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                </div>
                                
                                <asp:Panel ID ="panelLicencia" runat="Server">
                                <div class="df-100 mt-p5">
                                        <asp:Label ID="lblNumLicencia" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Número de licencia:" />
                                         <asp:Label ID="txtNumLicencia" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                </asp:Panel>
                                <div class="df-100 mt-p5">
                                        <asp:Label ID="lblPais_op" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Pais:" />
                                         <asp:Label ID="txtPais_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                <div class="df-100">
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblCodigoPostal_op" runat="server" CssClass="item " Font-Bold="True"
                                            Text="C.P:" />
                                         <asp:Label ID="txtCodigoPostal_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblEstado_op" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Estado:" />
                                         <asp:Label ID="txtEstado_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                </div>
                                <div class="df-100">
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblColonia_op" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Colonia:" />
                                         <asp:Label ID="txtColonia_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblMunicipio_op" runat="server" CssClass="item " Font-Bold="True"
                                            Text="Municipio:" />
                                         <asp:Label ID="txtMunicipio_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                   
                                </div>
                                <div class="df-50 mt-p5 dnone">
                                    <asp:Label ID="lblLocalidad_op" runat="server" CssClass="item " Font-Bold="True"
                                        Text="Calle:" />
                                     <asp:Label ID="txtLocalidad_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                <div class="df-50 mt-p5">
                                    <asp:Label ID="lblCalle_op" runat="server" CssClass="item " Font-Bold="True"
                                        Text="Calle:" />
                                     <asp:Label ID="txtCalle_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                <div class="df-100">
                                    
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblNumeroExterior_op" runat="server" CssClass="item " Font-Bold="True"
                                            Text="No. Exterior:" />
                                         <asp:Label ID="txtNumeroExterior_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                    <div class="df-50 mt-p5">
                                        <asp:Label ID="lblNumeroInterior_op" runat="server" CssClass="item " Font-Bold="True"
                                            Text="No. Interior:" />
                                         <asp:Label ID="txtNumeroInterior_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                            Text="" />
                                    </div>
                                </div>     
                                <div class="df-50 mt-p5">
                                    <asp:Label ID="lblReferencia_op" runat="server" CssClass="item " Font-Bold="True"
                                        Text="Referencia:" />
                                     <asp:Label ID="txtReferencia_op" runat="server" CssClass="item ml-p5" Font-Bold="false"
                                        Text="" />
                                </div>
                                </asp:Panel>
                            </div>
                            <div style="width: 50%;margin-top:4px;">
                                <telerik:RadGrid ID="Operadoreslist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    GridLines="None" AllowSorting="true" PageSize="10" ShowStatusBar="True" Width="100%"
                                    RenderMode="Lightweight">
                                    <PagerStyle Mode="NumericPages" />
                                    <MasterTableView NoMasterRecordsText="vacio" Width="100%"
                                        PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowMultiColumnSorting="true" AllowSorting="true"
                                        CommandItemDisplay="none">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="id" HeaderText="Item" UniqueName="descripcion"
                                                HeaderStyle-Width="40" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="tipoFigura_descripcion" HeaderText="Tipo" UniqueName="identificador"
                                                HeaderStyle-Width="30">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NombreOperador" HeaderText="Nombre" UniqueName="nombre"
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
                 
                 </table>

        </fieldset>
    </telerik:RadAjaxPanel>

                <!-- :::::::::::::::::::::: Carta porte end :::::::::::::::::::::::::::::::: -->
        <br />

        <asp:Panel ID="panelResume" runat="server" Visible="False">

            <fieldset style="padding:10px;">
                <legend style="padding-right: 6px; color: Black">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/portalcfd/images/resumen.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblResume" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>

                <br />

                <table width="100%" align="left">
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblSubTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblSubTotalValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblDescuento" runat="server" CssClass="item" Font-Bold="True" Text="Descuento="></asp:Label>
                            &nbsp;<asp:Label ID="lblDescuentoValue" runat="server" CssClass="item" 
                                Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblIVA" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblIEPS" runat="server" CssClass="item" Font-Bold="True" Text="IEPS="></asp:Label>
                            &nbsp;<asp:Label ID="lblIEPSvalue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 32%">
                            <asp:Label ID="lblRetISR" runat="server" CssClass="item" Font-Bold="True" Text="Ret. ISR="></asp:Label>
                            &nbsp;<asp:Label ID="lblRetISRValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 32%">
                            <asp:Label ID="lblRetIVA" runat="server" CssClass="item" Font-Bold="True" Text="Ret. IVA="></asp:Label>
                            &nbsp;<asp:Label ID="lblRetIVAValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    <asp:panel ID="panelRetencion" runat="server" Visible="false">
                        <tr>
                            <td width="16%" align="left" style="width: 32%">
                                <asp:Label ID="lblRet" runat="server" CssClass="item" Font-Bold="True" Text="Retención 4%="></asp:Label>
                                &nbsp;<asp:Label ID="lblRetValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                            </td>
                        </tr>
                    </asp:panel>
                    <tr>
                        <td width="16%" align="left" style="width: 32%">
                            <asp:Label ID="lblTotal" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                            &nbsp;<asp:Label ID="lblTotalValue" runat="server" CssClass="item" Font-Bold="False"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="left" width="16%">
                            <br /><br />
                            <asp:Button ID="btnCreateInvoice" runat="server" CausesValidation="true" CssClass="item" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelInvoice" runat="server" CausesValidation="False" CssClass="item" />    
                            <br /><br />
                        </td>
                    </tr>
                </table>

            </fieldset>

        </asp:Panel>
    
      <telerik:RadWindow ID="RadWindow1" runat="server" Modal="true" CenterIfModal="true" AutoSize="False" Behaviors="None" VisibleOnPageLoad="False" Width="740" Height="600">
        <ContentTemplate>
        <br />
        <table align="center" width="95%">
            <tr>
                <td>
                    <asp:TextBox ID="txtErrores" TextMode="MultiLine" Width="100%" Rows="32" ReadOnly="true" CssClass="item" runat="server"></asp:TextBox>
                </td>
                <tr>
                    <td align="left" width="16%">
                       <br /><br />
                       <asp:Button ID="btnAceptar" runat="server" CausesValidation="true" CssClass="item" Text="Aceptar"/>&nbsp;&nbsp;
                     <br /><br />
                    </td>
                </tr>
            </tr>
        </table>
        <br />
        </ContentTemplate>
    </telerik:RadWindow>
    
    <%--</telerik:RadAjaxPanel>
  
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>
