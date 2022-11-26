<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Menu_PortalCFD.ascx.vb" Inherits="portalcfd_usercontrols_portalcfd_Menu_PortalCFD" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
        #container {
           width:100%;
           text-align:center;
        }

        #left {
            float:left;
        }

        #center {
            margin:0 auto;
        }

        #right {
            float:right;

        }
</style>
    
<telerik:RadMenu ID="RadMenu1" runat="server" Width="100%" Skin="Sunset" style="z-index:3000">
    <Items>
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Inicio" NavigateUrl="~/portalcfd/Home.aspx">
        </telerik:RadMenuItem>
        
        <%--<telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Nómina" NavigateUrl="#">
            <Items>
                <telerik:RadMenuItem Text="Empleados" NavigateUrl="~/portalcfd/Empleados.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Timbrado" NavigateUrl="~/portalcfd/TimbradoNomina.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Catálogos" NavigateUrl="#">
                    <Items>
                        <telerik:RadMenuItem Text="Departamentos" NavigateUrl="~/portalcfd/Departamentos.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Percepciones" NavigateUrl="~/portalcfd/Percepciones.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Deducciones" NavigateUrl="~/portalcfd/Deducciones.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Otros Pagos" NavigateUrl="~/portalcfd/OtroPago.aspx"></telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Consulta de CFDS" NavigateUrl="~/portalcfd/ReporteEmpleado.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Reporte General" NavigateUrl="~/portalcfd/Reporte.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem Text="Repote Detallado" NavigateUrl="~/portalcfd/Reporte3.aspx"></telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>--%>

        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Mis Clientes" NavigateUrl="~/portalcfd/Clientes.aspx">
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Almacén" NavigateUrl="#">
            <Items>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Ver Productos" NavigateUrl="~/portalcfd/almacen/Conceptos.aspx"></telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Comprobantes Fiscales Digitales" NavigateUrl="~/portalcfd/CFD.aspx">
            <Items>
                <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Nuevo comprobante" NavigateUrl="~/portalcfd/Facturar_Extended.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Nuevo Complemento de Pagos" NavigateUrl="~/portalcfd/ComplementoDePagos.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Complementos de Pago Emitidos" NavigateUrl="~/portalcfd/Complementosemitidos.aspx"></telerik:RadMenuItem>
                <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Traslado Carta Porte" NavigateUrl="~/portalcfd/CartaPorte.aspx"></telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Folios" NavigateUrl="~/portalcfd/folios.aspx">
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Reportes" NavigateUrl="~/portalcfd/Reportes.aspx">
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Configuración">
            <Items>
                <telerik:RadMenuItem ImageUrl="../../images/icons/data.png" Text="Mis Datos" NavigateUrl="~/portalcfd/Datos.aspx"></telerik:RadMenuItem>
            </Items>
            <Items>
                <telerik:RadMenuItem ImageUrl="../../images/icons/certificates.png" Text="Firma & Certificados" NavigateUrl="~/portalcfd/Configuracion.aspx"></telerik:RadMenuItem>
            </Items>
            <Items>
                 <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Catalogos SAT" NavigateUrl="#">
                    <Items>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Aduanas" NavigateUrl="~/portalcfd/catalogos/Aduana.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Productos / Servicios" NavigateUrl="~/portalcfd/catalogos/ProdServ.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Unidades De Medida" NavigateUrl="~/portalcfd/catalogos/ClaveUnidad.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Códigos Postales" NavigateUrl="~/portalcfd/catalogos/CodigoPostal.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Formas De Pago" NavigateUrl="~/portalcfd/catalogos/FormaPago.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Impuestos" NavigateUrl="~/portalcfd/catalogos/Impuestos.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Método de Pago" NavigateUrl="~/portalcfd/catalogos/MetodoPago.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Moneda" NavigateUrl="~/portalcfd/catalogos/Moneda.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Países" NavigateUrl="~/portalcfd/catalogos/Países.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Patentes Aduanales" NavigateUrl="~/portalcfd/catalogos/PatenteAduanal.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Régimen Fiscal" NavigateUrl="~/portalcfd/catalogos/RegimenFiscal.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Tasas O Cuotas de Impuestos" NavigateUrl="~/portalcfd/catalogos/TasaOCuota.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Tipos de Comprobante" NavigateUrl="~/portalcfd/catalogos/TipoDeComprobante.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Tipo Factor" NavigateUrl="~/portalcfd/catalogos/Tipofactor.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Tipo Relación entre CFDI" NavigateUrl="~/portalcfd/catalogos/TipoRelacion.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Uso de Comprobantes" NavigateUrl="~/portalcfd/catalogos/UsoCFDI.aspx"></telerik:RadMenuItem>
                    </Items>
                </telerik:RadMenuItem>
            </Items>
            <Items>
                <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Catalogos Sistema" NavigateUrl="#">
                    <Items>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Unidades de Medida" NavigateUrl="~/portalcfd/Unidad.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Productos / Servicios" NavigateUrl="~/portalcfd/claveproducto.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Paises" NavigateUrl="~/portalcfd/Paises.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Cuentas de Beneficiario" NavigateUrl="~/portalcfd/CuentasBeneficiario.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Autotransportes" NavigateUrl="~/portalcfd/Autotransportes.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Figuras Transporte" NavigateUrl="~/portalcfd/FigurasTransporte.aspx"></telerik:RadMenuItem>
                        <telerik:RadMenuItem ImageUrl="../../images/icons/file.png" Text="Direcciones de Origen" NavigateUrl="~/portalcfd/Remitentes.aspx"></telerik:RadMenuItem>
                    
                    </Items>
                </telerik:RadMenuItem>
            </Items>
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Mis Usuarios" NavigateUrl="~/portalcfd/usuarios/usuarios.aspx">
        </telerik:RadMenuItem>
        
        <telerik:RadMenuItem runat="server" ExpandMode="ClientSide" Text="Salir" NavigateUrl="~/portalcfd/Salir.aspx">
        </telerik:RadMenuItem>
    </Items>
</telerik:RadMenu>
<br />
<br />
<div id="container">
  <div id="left">
      <asp:Label ID="lblUsuario" runat="server" CssClass="item"></asp:Label>
  </div>
  <div id="right">
      <asp:Label ID="lblCertificado" runat="server" CssClass="item"></asp:Label>
  </div>
  <div id="center">
      <asp:LinkButton ID="lnkConsultarFolios" runat="server" CssClass="item">Consultar Folios Disponible</asp:LinkButton>
  </div>
</div>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" BorderStyle="None" BorderWidth="0px" VisibleStatusbar="True" VisibleTitlebar="False">
     <Windows> 
        <telerik:RadWindow ID="RadWindow1" runat="server" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" VisibleStatusbar="False" VisibleTitlebar="True" BorderStyle="None" BorderWidth="0px" Behaviors="Close" Width="380px" Height="250px" Skin="Simple">
        <ContentTemplate>
        <table style="width:100%; height:100%;" align="center" cellpadding="0" cellspacing="3" border="0">
            <tr>
                <td style="height: 5px" colspan="2">
                   <telerik:RadGrid ID="FoliosGrid" runat="server" Width="100%" ShowStatusBar="True"
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15" GridLines="None"
                        Skin="Simple">
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView Width="100%" Name="Folios" AllowMultiColumnSorting="False">
                            <Columns>
                                <telerik:GridBoundColumn DataField="tipo" HeaderText="Documento" UniqueName="tipo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="folioDisponible" HeaderText="Folios Disponibles" UniqueName="folioDisponible" ItemStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
        </ContentTemplate>
        </telerik:RadWindow>
        </Windows>
</telerik:RadWindowManager>