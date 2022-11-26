Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Telerik.Reporting.Processing
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath
Imports System.Xml.Xsl
Imports System.Xml.Schema
Imports System.Xml.XPath.XPathItem
Imports System.Xml.XPath.XPathNavigator
Imports System.Xml.Serialization
Imports System.Collections
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.OpenSsl
Imports Org.BouncyCastle.Security
Imports FirmaSAT.Sat
Imports ThoughtWorks.QRCode.Codec
Imports ThoughtWorks.QRCode.Codec.Util
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Security
Imports System.Runtime.InteropServices
Imports Ionic.Zip

Imports System.Web.Services.Protocols
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Net.Security
Imports System.Net
Imports System.Security.Authentication
Imports SIFEI33

Partial Class portalcfd_Facturar_Extended
    Inherits System.Web.UI.Page
    Private importe As Decimal = 0
    Private iva As Decimal = 0
    Private ieps As Integer = 0
    Private ieps_total As Decimal = 0
    Private total As Decimal = 0
    Private importesindescuento As Decimal = 0
    Private totaldescuento As Decimal = 0
    Private tieneIvaTasaCero As Boolean = False
    Private tieneIva16 As Boolean = False
    Private archivoLlavePrivada As String = ""
    Private contrasenaLlavePrivada As String = ""
    Private archivoCertificado As String = ""
    Private _selloCFD As String = ""
    Private _cadenaOriginal As String = ""

    Private tipocontribuyenteid As Integer = 0
    Private tipoid As Integer = 0
    Private tipoprecioid As Integer
    Private cadOrigComp As String
    '**************************
    Private retencion4 As Decimal = 0
    Private maniobras As Decimal = 0

    Private m_xmlDOM As New XmlDocument
    Const URI_SAT = "http://www.sat.gob.mx/cfd/3"
    Private listErrores As New List(Of String)
    Private Comprobante As XmlNode
    Public Const NOMBRE_XSLT = "cadenaoriginal_3_3.xslt"
    Public Const DIR_SAT = "\SAT\"
    Dim UUID As String = ""
    Private qrBackColor As Integer = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb
    Private qrForeColor As Integer = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb
    Dim AplicarRetencion As Boolean = False
    Dim ValidarConcepto As Boolean = False
    Dim NotacreditoRetencion4 As Boolean = False
    Dim HonorarioArrend As Boolean = False
    Private data As Byte()
    Private AplicaImpuestoTasa0 As Boolean
    'Carta porte 
    Shared pcache As New DataTable
    Public Shared idCache As Integer = 0
    Shared _RfcRemitente As String = ""
    Shared _RfcDestinatario As String = ""
    Shared _NombreRemitente As String = ""
    Shared _NombreDestinatario As String = ""
    Shared _CartaPorteID As Integer = 0

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Title = Resources.Resource.WindowsTitle

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblClientsSelectionLegend.Text = Resources.Resource.lblClientsSelectionLegend
            lblClientData.Text = Resources.Resource.lblClientData
            lblClientItems.Text = Resources.Resource.lblItems
            lblResume.Text = Resources.Resource.lblResume

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''


            Dim ObjCat As New DataControl
            ObjCat.Catalogo(cmbClient, "EXEC pMisClientes @cmd=1, @clienteUnionId='" & Session("clienteid") & "' ", 0)
            ObjCat.Catalogo(serieid, "select id, isnull(nombre,'') as nombre from tblTipoDocumento where id in (select distinct tipoid from tblMisFolios where ISNULL(utilizado,0)=0) order by nombre", 1)
            ObjCat.Catalogo(metodopagoid, "select codigo,descripcion from tblc_MetodoPago order by codigo", "PUE")
            ObjCat.Catalogo(usoCFDIID, "EXEC pCatalogos @cmd=8", 0)
            ObjCat.Catalogo(tiporelacionid, "select id, nombre from tblTipoRelacion order by id asc", 0)
            ''''''''''''''''''''''''
            'Carta Porte 
            '''''''''''''''''''''''
            ObjCat.Catalogo(cmbEmbalaje, "select clave, concat(clave, ' - ', descripcion) as descripcion from tblTipoEmbalaje ", 0)
            ObjCat.Catalogo(cmbMoneda, "select clave, nombre as descripcion from tblMoneda ", 0)
            ObjCat.Catalogo(cmbPermSCT, "select clave, descripcion from tblTipoPermiso where claveTransporte = '01'", 0)
            ObjCat.Catalogo(cmbConfigVehicular, "select clave, CONCAT(clave, ' - ', descripcion) as descripcion from tblConfigAutotransporte ", 0)
            ObjCat.Catalogo(cmbAutotransporte, "EXEC pAutotransportes @cmd=6", 0)
            ObjCat.Catalogo(cmbOperador, "EXEC pOperadores @cmd=6 ", 0)

            ObjCat = Nothing
            DomiciliosPais()
            CargaCmbDirecciones_Remitente()
            cmbClient.Text = Resources.Resource.cmbEmptyMessage

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblContact.Text = Resources.Resource.lblContact
            lblRFC.Text = Resources.Resource.lblRFC
            lblNumCtaPago.Text = Resources.Resource.lblNumCtaPago
            lblNumCtaPago.ToolTip = Resources.Resource.lblNumCtaPagoTooltip


            lblSubTotal.Text = Resources.Resource.lblSubTotal
            lblIVA.Text = Resources.Resource.lblIVA
            lblTotal.Text = Resources.Resource.lblTotal

            Call CargaLugarExpedicion()

            btnCreateInvoice.Text = Resources.Resource.btnCreateInvoice
            btnCancelInvoice.Text = Resources.Resource.btnCancelInvoice
            '
            '   Protege contra doble clic la creación de la factura
            '
            btnCreateInvoice.Attributes.Add("onclick", "javascript:" + btnCreateInvoice.ClientID + ".disabled=true;" + ClientScript.GetPostBackEventReference(btnCreateInvoice, ""))

            ''''''''''''''''''''''''''
            'Set CFD Session Variable'
            ''''''''''''''''''''''''''

            If Not String.IsNullOrEmpty(Request("id")) Then

                Session("CFD") = Request("id")

                Call CargaCFD()

                panelItemsRegistration.Visible = True
                itemsList.Visible = True
                panelResume.Visible = True

                Call verificaCartaPorte()
                Call tblMercancias_NeedData("on")

                Call DisplayItems()
                Call CargaTotales()
                DeternimaEstatusTernium("Comprueba")
            Else

                Session("CFD") = 0
                Call creaColumnasCache()

            End If
        End If

    End Sub

    Private Sub CargaCFD()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=10, @cfdid='" & Session("CFD").ToString & "'", conn)
        Dim clienteid As Long = 0
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()


            panelSpecificClient.Visible = True
            panelItemsRegistration.Visible = True

            If rs.Read() Then

                cmbClient.SelectedValue = rs("clienteid")
                clienteid = rs("clienteid")
                usoCFDIID.SelectedValue = rs("usocfdi")
                serieid.SelectedValue = rs("serieid")
                tipocambio.Text = rs("tipocambio")
                instrucciones.Text = rs("instrucciones")

                If serieid.SelectedValue = 4 Then
                    panelDivisas.Visible = True
                    valTipoCambio.Enabled = True
                End If

            End If

            rs.Close()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try
        '
        Call CargaCliente(clienteid)
        ''
    End Sub
    
    Private Sub CargaLugarExpedicion()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=3, @clienteid=1", conn)
        Dim clienteid As Long = 0
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()


            If rs.Read() Then
                txtLugarExpedicion.Text = rs("expedicionLinea3")
            End If

            rs.Close()

        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        ''
    End Sub

#End Region

#Region "Combobox Events"

    Private Sub CargaCliente(ByVal ClienteId As Long)
        'Dim metodopagoid As String
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=2, @clienteid='" & ClienteId.ToString & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            tipoprecioid = 0
            panelSpecificClient.Visible = True
            panelItemsRegistration.Visible = True

            If rs.Read() Then

                lblSocialReasonValue.Text = rs("razonsocial")
                lblContactValue.Text = rs("contacto")
                lblContactPhoneValue.Text = rs("telefono_contacto")
                lblRFCValue.Text = rs("rfc")
                tipoprecioid = rs("tipoprecioid")
                Dim ObjData As New DataControl
                ObjData.Catalogo(formapagoid, "select id, nombre from tblFormaPago", rs("formapagoid"))
                ObjData.Catalogo(condicionesId, "select id, nombre from tblCondiciones", rs("condicionesid"))

                If rs("metodopagoid").ToString.Length > 1 Then
                    ObjData.Catalogo(metodopagoid, "select codigo,descripcion from tblc_MetodoPago order by codigo", rs("metodopagoid"))
                End If

                ObjData.Catalogo(usoCFDIID, "EXEC pCatalogos @cmd=8", rs("usocfdi"))
                ObjData = Nothing
                txtNumCtaPago.Text = rs("numctapago")
                tipocontribuyenteid = rs("tipocontribuyenteid")

                'Nuevos Campos
                lblStreetValue.Text = rs("fac_calle")
                lblExtNumberValue.Text = rs("fac_num_ext")
                lblIntNumberValue.Text = rs("fac_num_int")
                lblColonyValue.Text = rs("fac_colonia")
                lblCountryValue.Text = rs("fac_pais")

                lblEstadoValue.Text = rs("estado")
                lblTownshipValue.Text = rs("fac_municipio")
                lblZipCodeValue.Text = rs("fac_cp")
                lblRFCValue.Text = rs("rfc")

                lblTipoContribuyenteValue.Text = rs("tipocontribuyente")
            End If
            '
            rs.Close()
            conn.Close()
            conn.Dispose()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try
        CargaCbmDirecciones_Cliente(cmbClient.SelectedValue)
    End Sub

    Private Sub ClearItems()

        itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
        itemsList.DataSource = Nothing
        itemsList.DataBind()

        Session("CFD") = 0
        itemsList.Visible = False

        lblSubTotalValue.Text = ""
        lblIVAValue.Text = ""
        lblIEPSvalue.Text = ""

        lblTotalValue.Text = ""
        panelResume.Visible = False

    End Sub



#End Region

#Region "Add Invoice Items"

    Protected Sub GetCFD()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd=1, @clienteid='" & cmbClient.SelectedValue & "'", conn)

        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                Session("CFD") = rs("cfdid")

            End If

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            '

        Finally

            conn.Close()
            conn.Dispose()

        End Try
        DeternimaEstatusTernium("Checked")
    End Sub

    Protected Sub InsertItem(ByVal id As Integer, ByVal item As GridItem)
        '
        ' Instancía objetos del grid
        '
        Dim lblCodigo As Label = DirectCast(item.FindControl("lblCodigo"), Label)
        Dim txtDescripcion As System.Web.UI.WebControls.TextBox = DirectCast(item.FindControl("txtDescripcion"), System.Web.UI.WebControls.TextBox)
        Dim txtCuenta As System.Web.UI.WebControls.TextBox = DirectCast(item.FindControl("txtCuenta"), System.Web.UI.WebControls.TextBox)
        Dim lblUnidad As Label = DirectCast(item.FindControl("lblUnidad"), Label)
        Dim txtQuantity As RadNumericTextBox = DirectCast(item.FindControl("txtQuantity"), RadNumericTextBox)
        Dim txtUnitaryPrice As RadNumericTextBox = DirectCast(item.FindControl("txtUnitaryPrice"), RadNumericTextBox)
        Dim txtDescuento As RadNumericTextBox = DirectCast(item.FindControl("txtDescuento"), RadNumericTextBox)
        Dim ItemChkManiobra As System.Web.UI.WebControls.CheckBox = DirectCast(item.FindControl("ItemChkManiobra"), System.Web.UI.WebControls.CheckBox)

        'Dim ValidarConcepto As Boolean = False

        Dim MsValido As String = ""
        Dim retencionbit As Integer = 0
        Dim tiporetencion As Integer = 0
        Dim maniobraBit As Boolean = False

        If serieid.SelectedValue = 3 Then
            If tipocontribuyenteid <> 1 Then
                If txtCuenta.Text.Length > 0 Then
                    MsValido = "OK"
                    ValidarConcepto = True
                    retencionbit = 1
                    tiporetencion = serieid.SelectedValue
                Else
                    MsValido = "Favor de Agregar el Numero de la Cuenta Predial"
                    ValidarConcepto = False
                End If
            Else
                If txtCuenta.Text.Length > 0 Then
                    MsValido = "OK"
                    ValidarConcepto = True
                    retencionbit = 0
                    tiporetencion = 0
                Else
                    MsValido = "Favor de Agregar el Numero de la Cuenta Predial"
                    ValidarConcepto = False
                End If
            End If
        ElseIf serieid.SelectedValue = 6 Or serieid.SelectedValue = 5 Then
            If tipocontribuyenteid <> 1 Then
                MsValido = "OK"
                ValidarConcepto = True
                retencionbit = 1
                tiporetencion = serieid.SelectedValue

                If serieid.SelectedValue = 5 Then
                    If ItemChkManiobra.Checked = True Then
                        MsValido = "OK"
                        ValidarConcepto = True
                        retencionbit = 0
                        tiporetencion = 0
                        maniobraBit = True
                    End If
                End If
            Else
                MsValido = "OK"
                ValidarConcepto = True
                retencionbit = 0
                tiporetencion = 0
            End If

        ElseIf serieid.SelectedValue = 2 Or serieid.SelectedValue = 8 Then
            If txtFolioFiscal.Text.ToString.Length > 1 Then

                Dim ObjDat As New DataControl
                Dim tipodocumento As Long = ObjDat.RunSQLScalarQuery("select top 1 isnull(serieid,0) from tblCFD where isnull(timbrado,0)=1 and isnull(estatus,0)=1 and uuid='" & txtFolioFiscal.Text & "'")
                ObjDat = Nothing

                If tipodocumento = 5 Then
                    MsValido = "OK"
                    NotacreditoRetencion4 = True
                    ValidarConcepto = True
                    retencionbit = 1
                    tiporetencion = tipodocumento.ToString

                ElseIf tipodocumento = 6 Or tipodocumento = 3 Then
                    MsValido = "OK"
                    HonorarioArrend = True
                    ValidarConcepto = True
                    retencionbit = 1
                    tiporetencion = tipodocumento.ToString
                Else
                    MsValido = "OK"
                    ValidarConcepto = True
                    retencionbit = 0
                    tiporetencion = 0
                End If
            Else
                MsValido = "Favor de Ingresar el Cfdi Relacionado"
                ValidarConcepto = False
            End If
        Else
            MsValido = "OK"
            ValidarConcepto = True
            retencionbit = 0
            tiporetencion = 0
        End If

        'If ValidarConcepto = True Then
        If MsValido = "OK" Then
            Dim porcentaje As Double = 0
            '
            '   Agrega la partida
            '
            Dim objdata As New DataControl
            objdata.RunSQLQuery("EXEC pCFD @cmd=2, @cfdid='" & Session("CFD").ToString & "', @codigo='" & lblCodigo.Text & "', @descripcion='" & txtDescripcion.Text & "', @cantidad='" & txtQuantity.Text & "', @unidad='" & lblUnidad.Text & "', @precio='" & txtUnitaryPrice.Text & "', @productoid='" & id.ToString & "', @descuento='" & txtDescuento.Text & "', @retencionbit='" & retencionbit.ToString & "', @tiporetencion='" & tiporetencion.ToString & "',@cuentaPredial='" & txtCuenta.Text & "', @maniobraBit='" & maniobraBit.ToString & "'")
            objdata = Nothing
            '
            ''
        Else
            RadWindowManager2.RadAlert(MsValido.ToString, 330, 180, "Alert", "", "")
        End If
        'Else
        'RadWindowManager2.RadAlert("Si la Factura es Recibo de arrendamiento Favor de Agregar el Numero de la Cuenta Predial.", 330, 180, "Alert", "", "")
        'End If
    End Sub

    Private Sub DisplayItems()
        Dim ds As DataSet
        Dim ObjData As New DataControl
        itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
        ds = ObjData.FillDataSet("EXEC pCFD @cmd=3, @cfdid='" & Session("CFD").ToString & "'")
        itemsList.DataSource = ds
        itemsList.DataBind()
        ObjData = Nothing
    End Sub

    Protected Sub itemsList_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles itemsList.NeedDataSource
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCFD @cmd=3, @cfdid='" & Session("CFD").ToString & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            itemsList.MasterTableView.NoMasterRecordsText = Resources.Resource.ItemsEmptyGridMessage
            itemsList.DataSource = ds
            itemsList.DataBind()

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            '
        Finally

            conn.Close()
            conn.Dispose()

        End Try
    End Sub

    Private Sub CargaTotales()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=16, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "'", conn)
        ' Try

        conn.Open()

        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()

        If rs.Read Then
            tieneIva16 = rs("tieneIva16")
            tieneIvaTasaCero = rs("tieneIvaTasaCero")
            importe = rs("importe")
            iva = rs("iva")
            ieps_total = rs("ieps_total")
            ieps = rs("ieps_total")
            ImporteIva.Value = rs("iva")
            tipoid = rs("tipoid")
            tipoidF.Value = rs("tipoid")
            totaldescuento = rs("totaldescuento")
            total = rs("total")
            importesindescuento = rs("importe_sindescuento")
            tipocontribuyenteid = rs("tipocontribuyenteid")
            'Carta Aporte
            maniobras = rs("maniobras")
            retencion4 = rs("retencion4")


            '
            lblSubTotalValue.Text = FormatCurrency(importe, 2).ToString
            lblDescuentoValue.Text = FormatCurrency(totaldescuento, 2).ToString
            lblIVAValue.Text = FormatCurrency(rs("iva_pesos"), 2).ToString
            ImporteIva.Value = rs("iva_pesos")
            lblIEPSvalue.Text = FormatCurrency(ieps_total, 2).ToString
            lblTotalValue.Text = FormatCurrency(rs("total_pesos"), 2).ToString
            '
            '
            Dim TmpRetIVA As Decimal = 0
            If AplicarRetencion = True Then
                Select Case tipoid
                    Case 3, 6
                        '
                        If tipocontribuyenteid <> 1 Then
                            lblRetISRValue.Text = FormatCurrency((importe * 0.1), 2).ToString
                            TmpRetIVA = FormatNumber((iva / 3) * 2, 2).ToString 'total - (importe * 0.1) - ((iva / 3) * 2)

                            If TmpRetIVA >= 2000 Then
                                TmpRetIVA = FormatNumber((importe * 0.106667), 2).ToString
                                'total = total - (importe * 0.1) - (TmpRetIVA)
                                lblRetIVAValue.Text = FormatCurrency((TmpRetIVA), 2).ToString
                                lblTotalValue.Text = FormatCurrency((total - (importe * 0.1) - (TmpRetIVA)), 2).ToString
                            Else
                                lblRetIVAValue.Text = FormatCurrency((iva / 3) * 2, 2).ToString
                                lblTotalValue.Text = FormatCurrency((total - (importe * 0.1) - ((iva / 3) * 2)), 2).ToString
                            End If
                            'lblTotalValue.Text = FormatCurrency((total - (importe * 0.1) - ((iva / 3) * 2)), 2).ToString
                        Else
                            lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                        End If
                        '
                    Case 7
                        '
                        If tipocontribuyenteid <> 1 Then
                            lblRetIVAValue.Text = FormatCurrency((iva * 0.1), 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                            lblTotalValue.Text = FormatCurrency((total - (iva * 0.1)), 2).ToString
                        Else
                            lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                        End If
                        '
                    Case Else
                        If NotacreditoRetencion4 = True Then
                            panelRetencion.Visible = True
                            lblRetValue.Text = FormatCurrency(importe * 0.04, 2).ToString
                            lblTotalValue.Text = FormatCurrency(total - (importe * 0.04), 2).ToString
                            total = FormatNumber(total - (importe * 0.04), 2)
                        Else
                            lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                        End If

                        If HonorarioArrend = True Then
                            If tipocontribuyenteid <> 1 Then
                                lblRetISRValue.Text = FormatCurrency((importe * 0.1), 2).ToString
                                TmpRetIVA = FormatNumber((iva / 3) * 2, 2).ToString 'total - (importe * 0.1) - ((iva / 3) * 2)

                                If TmpRetIVA >= 2000 Then
                                    TmpRetIVA = FormatNumber((importe * 0.106667), 2).ToString
                                    lblRetIVAValue.Text = FormatCurrency((TmpRetIVA), 2).ToString
                                    lblTotalValue.Text = FormatCurrency((total - (importe * 0.1) - (TmpRetIVA)), 2).ToString
                                Else
                                    lblRetIVAValue.Text = FormatCurrency((iva / 3) * 2, 2).ToString
                                    lblTotalValue.Text = FormatCurrency((total - (importe * 0.1) - ((iva / 3) * 2)), 2).ToString
                                End If
                            Else
                                lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                                lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                            End If
                        Else
                            lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                            lblRetISRValue.Text = FormatCurrency(0, 2).ToString
                        End If
                End Select

                If System.Configuration.ConfigurationManager.AppSettings("retencion4") = 1 And tipoid = 5 Then
                    lblSubTotalValue.Text = FormatCurrency(importe, 2).ToString
                    panelRetencion.Visible = True
                    lblRetValue.Text = FormatCurrency(retencion4, 2).ToString
                    total = FormatCurrency(Convert.ToDecimal(FormatNumber(importe, 2)) + Convert.ToDecimal(FormatNumber(iva, 2)) - Convert.ToDecimal(FormatNumber(retencion4, 2)), 2)
                    lblTotalValue.Text = FormatCurrency(Convert.ToDecimal(FormatNumber(importe + maniobras, 2)) + Convert.ToDecimal(FormatNumber(iva, 2)) - Convert.ToDecimal(FormatNumber(retencion4, 2)), 2)
                End If
                '
            Else
                lblRetIVAValue.Text = FormatCurrency(0, 2).ToString
                lblRetISRValue.Text = FormatCurrency(0, 2).ToString
            End If
        End If

        ' Catch ex As Exception
        '
        ' Finally
        conn.Close()
        conn.Dispose()
        conn = Nothing
        'End Try
    End Sub

#End Region

#Region "Telerik Grid Items Deleting Events"

    Protected Sub itemsList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles itemsList.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Items" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ItemsDeleteConfirmationMessage & "');")

            End If

        End If

    End Sub

    Protected Sub itemsList_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles itemsList.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                DeleteItem(e.CommandArgument)
                CargaTotales()
        End Select
    End Sub

    Private Sub DeleteItem(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCFD @cmd='4', @partidaId ='" & id.ToString & "'", conn)

        conn.Open()

        cmd.ExecuteReader()

        conn.Close()

        Call DisplayItems()

    End Sub

#End Region

#Region "Telerik Grid Items Column Names (From Resource File)"

    Protected Sub itemsList_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles itemsList.ItemCreated

        If TypeOf e.Item Is GridHeaderItem Then

            Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

            If e.Item.OwnerTableView.Name = "Items" Then

                header("codigo").Text = Resources.Resource.gridColumnNameCode
                header("descripcion").Text = Resources.Resource.gridColumnNameDescription
                header("cantidad").Text = Resources.Resource.gridColumnNameQuantity
                header("unidad").Text = Resources.Resource.gridColumnNameMeasureUnit
                header("precio").Text = Resources.Resource.gridColumnNameUnitaryPrice
                header("importe").Text = Resources.Resource.gridColumnNameAmount

            End If

        End If

    End Sub

#End Region

#Region "Create Invoice"

    Protected Sub btnCreateInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateInvoice.Click
        
        'CFD V 3.3

        If Page.IsValid Then

            Dim Timbrado As Boolean = False
            Dim MensageError As String = ""
            AplicaImpuestoTasa0 = False
            RadWindow1.VisibleOnPageLoad = False

            If serieid.SelectedValue = 10 Then
                Call GeneraDocumento()
            Else
                '
                '   Rutina de generación de XML CFDI Versión 3.3
                '
                Call CargaTotales()
                '
                '   Guadar Metodo de Pago
                '
                Call GuadarMetodoPago()
                '
                m_xmlDOM = CrearDOM()
                '
                '   Verifica que tipo de comprobante se va a emitir
                '
                Dim TipoDeComprobante As String = Nothing
                Select Case tipoidF.Value
                    Case 1, 3, 4, 5, 6, 30
                        '
                        TipoDeComprobante = "I"
                        '
                    Case 2, 8   '   Nota de Crédito
                        '
                        TipoDeComprobante = "E"
                End Select
                '
                Call AsignaSerieFolio()
                '
                Comprobante = CrearNodoComprobante(metodopagoid.SelectedValue, formapagoid.SelectedValue, condicionesId.SelectedItem.Text, importe, total, TipoDeComprobante, tipoidF.Value)
                '
                m_xmlDOM.AppendChild(Comprobante)
                IndentarNodo(Comprobante, 1)
                '
                If serieid.SelectedValue = 2 Or serieid.SelectedValue = 8 Then
                    '
                    '   Agrega CfdiRelacionados
                    '
                    Call CfdiRelacionados()
                    '
                End If
                '
                '   Agrega los datos del emisor
                '
                Call ConfiguraEmisor()
                '
                '
                '   Asigna los datos del receptor
                '
                Call ConfiguraReceptor()
                '
                '   Agrega los conceptos de la factura
                '
                CrearNodoConceptos(Comprobante)
                IndentarNodo(Comprobante, 1)
                '
                '   Agrega los impuestos
                '
                CrearNodoImpuestos(Comprobante)
                IndentarNodo(Comprobante, 1)
                '
                '
                'Agrega complemento de carta porte 
                '
                If ckCartaPorte.Checked = True Then
                    If tblMercancias.Items.Count < 1 Then
                        valMercancias.IsValid = False
                        Exit Sub
                    Else
                        Call CreaNodoCartaPorte(Comprobante)
                        IndentarNodo(Comprobante, 1)
                    End If
                End If
                IndentarNodo(Comprobante, 1)
                '
                '
                SellarCFD(Comprobante)
                m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
                m_xmlDOM.Save(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value & folio.Value & ".xml")
                '
                '   Realiza Timbrado
                '
                If folio.Value > 0 Then
                    '
                    '   Timbrado SIFEI
                    '
                    Dim Usuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                    Dim Password As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                    Dim IdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")

                    System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType) Or DirectCast(768, System.Net.SecurityProtocolType) Or DirectCast(192, System.Net.SecurityProtocolType) Or DirectCast(48, System.Net.SecurityProtocolType)

                    'System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True

                    'Pruebas
                    Dim TimbreSifeiVersion33 As New SIFEIPruebas33.SIFEIService()

                    'Produccion
                    'Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()

                    Call Comprimir()

                    Try
                        Dim bytes() As Byte
                        bytes = TimbreSifeiVersion33.getCFDI(Usuario, Password, data, "", IdEquipo)
                        Descomprimir(bytes)
                        Timbrado = True
                    Catch ex As SoapException
                        Call cfdnotimbrado()
                        Timbrado = False
                        MensageError = ex.Message.ToString & ex.Detail.InnerText
                    Catch ex As FaultException
                        Call cfdnotimbrado()
                        Timbrado = False
                        MensageError = ex.Message.ToString
                    End Try

                    If Timbrado = True Then
                        '
                        UUID = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                        '
                        '   Marca el cfd como timbrado
                        '
                        Call cfdtimbrado()
                        '
                        cadOrigComp = CadenaOriginalComplemento()
                        '
                        '   Genera Código Bidimensional
                        '
                        Call generacbb()
                        '
                        '   Genera PDF
                        '
                        If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.Value & folio.Value & ".pdf") Then
                            GuardaPDF(GeneraPDF(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.Value & folio.Value & ".pdf")
                        End If                    '
                        '                    '
                        '   Genera PDF Pre-Impreso
                        '                    '
                        If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.Value & folio.Value & "_preimpreso.pdf") Then
                            GuardaPDF(GeneraPDF_PreImpreso(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.Value & folio.Value & "_preimpreso.pdf")
                        End If
                        '                    '
                        '                    '
                        '                    '
                        '
                        '   Descarga Inventario si hay folio y fué timbrado el cfdi
                        '
                        'Try
                        '    If System.Configuration.ConfigurationManager.AppSettings("inventarios") = 1 Then
                        '        Call DescargaInventario(Session("CFD"))
                        '    End If
                        'Catch ex As Exception
                        '    Response.Write(ex.ToString)
                        '    Response.End()
                        'End Try

                        Session("CFD") = 0

                    Else

                    End If
                End If
            End If

            Session("CFD") = 0
            DataControl.NuevopedidoCDF = False

            If Timbrado = True Then
                Response.Redirect("~/portalcfd/cfd.aspx")
            Else
                txtErrores.Text = MensageError.ToString
                RadWindow1.VisibleOnPageLoad = True
            End If

        End If
    End Sub

    Private Function Comprimir()
        Dim zip As ZipFile = New ZipFile(serie.Value.ToString & folio.Value.ToString & ".zip")
        zip.AddFile(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value & folio.Value & ".xml", "")
        Dim ms As New MemoryStream()
        zip.Save(ms)
        data = ms.ToArray
    End Function

    Private Function Descomprimir(ByVal data5 As Byte())
        Dim ms1 As New MemoryStream(data5)
        Dim zip1 As ZipFile = New ZipFile()
        zip1 = ZipFile.Read(ms1)

        Dim archivo As String = ""
        Dim DirectorioExtraccion As String = Server.MapPath("~/portalcfd/cfd_storage/").ToString
        Dim e As ZipEntry
        For Each e In zip1
            archivo = e.FileName.ToString
            e.Extract(DirectorioExtraccion, ExtractExistingFileAction.OverwriteSilently)
        Next

        Dim Path = Server.MapPath("~/portalcfd/cfd_storage/")
        If File.Exists(Path & archivo) Then
            System.IO.File.Copy(Path & archivo, Path & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml")
        End If

        response.write(archivo)
        AddendaTernium(Path & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml")
    End Function

    Private Function CrearNodoComprobante(ByVal MetodoPago As String, ByVal FormaPago As String, ByVal CondicionesDePago As String, ByVal subTotal As Decimal, ByVal total As Decimal, ByVal TipoDeComprobante As String, ByVal tipoid As Integer) As XmlNode
        Dim Comprobante As XmlNode
        Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
        CrearAtributosComprobante(Comprobante, MetodoPago, FormaPago, CondicionesDePago, subTotal, total, TipoDeComprobante, tipoid)
        CrearNodoComprobante = Comprobante
    End Function


    Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement, ByVal MetodoPago As String, ByVal FormaPago As String, ByVal CondicionesDePago As String, ByVal subTotal As Decimal, ByVal total As Decimal, ByVal TipoDeComprobante As String, ByVal tipoid As Integer)
        Dim cartaPorte As String = ""
        Nodo.SetAttribute("xmlns:cfdi", URI_SAT)
        Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        If ckCartaPorte.Checked = True Then
            cartaPorte = " http://www.sat.gob.mx/CartaPorte20 http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte20.xsd"
            Nodo.SetAttribute("xmlns:cartaporte20", "http://www.sat.gob.mx/CartaPorte20")
        End If
        Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd" & cartaPorte)

        Nodo.SetAttribute("Version", "3.3")

        If serie.Value <> "" Then
            Nodo.SetAttribute("Serie", serie.Value)
        End If

        Nodo.SetAttribute("Folio", folio.Value)
        Nodo.SetAttribute("Fecha", Format(Now(), "yyyy-MM-ddThh:mm:ss"))
        Nodo.SetAttribute("Sello", "")
        Nodo.SetAttribute("FormaPago", FormaPago) '01,02,03,04,05,06,07...
        Nodo.SetAttribute("NoCertificado", "")
        Nodo.SetAttribute("Certificado", "")

        If condicionesId.SelectedValue > 0 Then
            Nodo.SetAttribute("CondicionesDePago", CondicionesDePago) 'CREDITO, CONTADO, CREDITO A 3 MESES ETC
        End If

        Nodo.SetAttribute("SubTotal", Format(subTotal, "#0.00"))

        If totaldescuento > 0 Then
            Nodo.SetAttribute("Descuento", Format(totaldescuento, "#0.00"))
        End If

        If (tipoid = 4 Or tipoid = 8) Then
            Nodo.SetAttribute("Moneda", "USD")
            Nodo.SetAttribute("TipoCambio", FormatNumber(tipocambio.Text, 2).ToString)
        Else
            Nodo.SetAttribute("Moneda", "MXN")
        End If

        Nodo.SetAttribute("Total", Format(total, "#0.00"))
        Nodo.SetAttribute("TipoDeComprobante", TipoDeComprobante)
        Nodo.SetAttribute("MetodoPago", MetodoPago) 'PUE, PID, PPD
        Nodo.SetAttribute("LugarExpedicion", CargaLugarExpedicionAtributos())
    End Sub


    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    'Crear Node
    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function

    'Private Function CrearNodoComprobante(ByVal MetodoPago As String, ByVal FormaPago As String, ByVal CondicionesDePago As String, ByVal subTotal As Decimal, ByVal total As Decimal, ByVal TipoDeComprobante As String, ByVal tipoid As Integer, ByVal tipoRelacion As String) As XmlNode
    '    Dim Comprobante As XmlNode
    '    Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
    '    CrearAtributosComprobante(Comprobante, MetodoPago, FormaPago, CondicionesDePago, subTotal, total, TipoDeComprobante, tipoid, tipoRelacion)
    '    CrearNodoComprobante = Comprobante
    'End Function

    'Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement, ByVal MetodoPago As String, ByVal FormaPago As String, ByVal CondicionesDePago As String, ByVal subTotal As Decimal, ByVal total As Decimal, ByVal TipoDeComprobante As String, ByVal tipoid As Integer, ByVal tipoRelacion As String)
    '    Nodo.SetAttribute("xmlns:cfdi", URI_SAT)
    '    Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
    '    Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd")
    '    Nodo.SetAttribute("Version", "3.3")
    '    If serie.Value <> "" Then
    '        Nodo.SetAttribute("Serie", serie.Value)
    '    End If
    '    Nodo.SetAttribute("Folio", folio.Value)
    '    Nodo.SetAttribute("Fecha", Format(Now(), "yyyy-MM-ddThh:mm:ss"))
    '    Nodo.SetAttribute("Sello", "")
    '    Nodo.SetAttribute("FormaPago", FormaPago) '01,02,03,04,05,06,07...
    '    Nodo.SetAttribute("NoCertificado", "")
    '    Nodo.SetAttribute("Certificado", "")
    '    Nodo.SetAttribute("CondicionesDePago", CondicionesDePago) 'CREDITO, CONTADO, CREDITO A 3 MESES ETC
    '    Nodo.SetAttribute("SubTotal", Format(subTotal, "#0.00"))

    '    If totaldescuento > 0 Then
    '        Nodo.SetAttribute("Descuento", Format(totaldescuento, "#0.00"))
    '    End If

    '    If (tipoid = 4 Or tipoid = 8) Then
    '        Nodo.SetAttribute("Moneda", "USD")
    '        Nodo.SetAttribute("TipoCambio", FormatNumber(tipocambio.Text, 2).ToString)
    '    Else
    '        Nodo.SetAttribute("Moneda", "MXN")
    '    End If

    '    Nodo.SetAttribute("Total", Format(total, "#0.00"))
    '    Nodo.SetAttribute("TipoDeComprobante", TipoDeComprobante)
    '    Nodo.SetAttribute("MetodoPago", MetodoPago) 'PUE, PID, PPD
    '    Nodo.SetAttribute("LugarExpedicion", CargaLugarExpedicionAtributos())
    'End Sub



    Private Function CargaLugarExpedicionAtributos() As String
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=3", conn)
        Dim LugarExpedicion As String = ""
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read() Then
                LugarExpedicion = rs("fac_cp")
            End If

            rs.Close()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        '
        Return LugarExpedicion
        ''
    End Function

    Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal Regimen As String)
        Dim Emisor As XmlElement
        Dim DomFiscal As XmlElement
        'Dim ExpedidoEn As XmlElement
        Dim RegimenFiscal As XmlElement

        Emisor = CrearNodo("cfdi:Emisor")
        Emisor.SetAttribute("Nombre", nombre)
        Emisor.SetAttribute("Rfc", rfc)
        Emisor.SetAttribute("RegimenFiscal", Regimen)

        Nodo.AppendChild(Emisor)
    End Sub

    Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal nombre As String, ByVal rfc As String, ByVal UsoCFDI As String)
        Dim Receptor As XmlElement
        Dim Domicilio As XmlElement

        Receptor = CrearNodo("cfdi:Receptor")
        Receptor.SetAttribute("Rfc", rfc)
        Receptor.SetAttribute("Nombre", nombre)
        Receptor.SetAttribute("UsoCFDI", UsoCFDI)

        Nodo.AppendChild(Receptor)
    End Sub

    Private Sub SellarCFD(ByVal NodoComprobante As XmlElement)
        Dim Certificado As String = ""
        Certificado = LeerCertificado()

        Dim Clave As String = ""
        Clave = LeerClave()

        'Dim Llave As String = ""
        'Llave = Leerllave()

        Dim objCert As New X509Certificate2()
        'y pasarle el nombre y ruta del Cerfificado para obtener la información en bytes

        Dim bRawData As Byte() = ReadFile(Server.MapPath("~/portalcfd/certificados/") & Certificado)

        objCert.Import(bRawData)
        Dim cadena As String = Convert.ToBase64String(bRawData)
        'comentando las dos lineas siguientes no agrega el certificado al comprobante xml
        NodoComprobante.SetAttribute("NoCertificado", FormatearSerieCert(objCert.SerialNumber))
        NodoComprobante.SetAttribute("Total", Format(CDbl(total), "#.#0"))
        NodoComprobante.SetAttribute("Certificado", Convert.ToBase64String(bRawData))
        'comentando la siguiente linea no agregar el sello al comprobante xml
        NodoComprobante.SetAttribute("Sello", GenerarSello(Clave))
    End Sub
    Private Function GenerarSello(ByVal Clave As String) As String
        Try
            Dim pkey As New Chilkat.PrivateKey
            Dim pkeyXml As String
            Dim rsa As New Chilkat.Rsa
            pkey.LoadPkcs8EncryptedFile(Server.MapPath("~/portalcfd/llave/") & Leerllave(), Clave)
            pkeyXml = pkey.GetXml()
            rsa.UnlockComponent("RSAT34MB34N_7F1CD986683M")
            rsa.ImportPrivateKey(pkeyXml)
            rsa.Charset = "utf-8"
            rsa.EncodingMode = "base64"
            rsa.LittleEndian = 0
            Dim base64Sig As String
            base64Sig = rsa.SignStringENC(GetCadenaOriginal(m_xmlDOM.InnerXml), "sha256")
            GenerarSello = base64Sig
        Catch oExcep As Exception
            MsgBox(oExcep.Message)
        Finally

        End Try
    End Function
    Function ReadFile(ByVal strArchivo As String) As Byte()
        Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
        Dim size As Integer = CInt(f.Length)
        Dim data As Byte() = New Byte(size - 1) {}
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function
    Private Function LeerCertificado() As String
        Dim Certificado As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19, @clienteid='" & Session("clienteid").ToString & "', @cfdid='" & Session("CFD").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Certificado = rs("archivo_certificado")
            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try


        Return Certificado
    End Function
    Private Function Leerllave() As String
        Dim llave As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19, @clienteid='" & Session("clienteid").ToString & "', @cfdid='" & Session("CFD").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                llave = rs("archivo_llave_privada")
            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return llave
    End Function
    Private Function LeerClave() As String
        Dim contrasena As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Try
            Dim cmd As New SqlCommand("exec pCFD @cmd=19, @clienteid='" & Session("clienteid").ToString & "', @cfdid='" & Session("CFD").ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                contrasena = rs("contrasena_llave_privada")
            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return contrasena
    End Function
    Public Function FormatearSerieCert(ByVal Serie As String) As String
        Dim Resultado As String = ""
        Dim I As Integer
        For I = 2 To Len(Serie) Step 2
            Resultado = Resultado & Mid(Serie, I, 1)
        Next
        FormatearSerieCert = Resultado
    End Function
    Private Function CrearNodo(ByVal Nombre As String) As XmlNode
        CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, URI_SAT)
        'CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, Nothing)
    End Function
    ' Formatea el aspecto de los nodos para que sea mas comoda su lectura.
    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub
    Private Sub CrearNodoComplemento(ByVal Nodo As XmlNode)
        Dim Complemento As XmlElement

        Complemento = CrearNodo("cfdi:Complemento")
        IndentarNodo(Complemento, 1)

        Nodo.AppendChild(Complemento)
    End Sub
    Private Function FileToMemory(ByVal Filename As String) As MemoryStream
        Dim FS As New System.IO.FileStream(Filename, FileMode.Open)
        Dim MS As New System.IO.MemoryStream
        Dim BA(FS.Length - 1) As Byte
        FS.Read(BA, 0, BA.Length)
        FS.Close()
        MS.Write(BA, 0, BA.Length)
        Return MS
    End Function

    Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
        Dim cadena As String = ""
        Try
            Dim xslt As New XslCompiledTransform
            Dim xmldoc As New XmlDocument
            Dim navigator As XPath.XPathNavigator
            Dim output As New StringWriter
            xmldoc.LoadXml(xmlCFD)
            navigator = xmldoc.CreateNavigator()
            'xslt.Load(Server.MapPath("~/portalcfd/SAT/cadenaoriginal_3_3.xslt"))
            xslt.Load("http://www.sat.gob.mx/sitio_internet/cfd/3/cadenaoriginal_3_3/cadenaoriginal_3_3.xslt")
            xslt.Transform(navigator, Nothing, output)
            cadena = output.ToString
        Catch ex As Exception
            Response.Write(ex.ToString)
            Response.End()
            'listErrores.Add("ERROR: " & ex.Message.ToString)
        End Try

        Return cadena
        CadenaF.Value = cadena

    End Function
    Private Sub CrearNodoImpuestos(ByVal Nodo As XmlNode)
        Dim AgregarTraslado As Boolean = False
        Dim AgregarIeps As Boolean = False
        Dim TasaOCuotas As String = ""
        Dim TipoFactor As String = ""
        Dim TipoImpuesto As String = ""
        Dim Impuestos As XmlElement
        Dim Traslados As XmlElement
        Dim Traslado As XmlElement

        Call CargaTotales()
        '
        Dim Retenciones As XmlElement
        Dim Retencion As XmlElement

        Impuestos = CrearNodo("cfdi:Impuestos")

        If iva > 0 Then
            Impuestos.SetAttribute("TotalImpuestosTrasladados", Math.Round(iva + ieps_total, 2)) 'Format(CDbl(iva + ieps_total), "0.#0")) 
        Else
            'Impuestos.SetAttribute("TotalImpuestosTrasladados", "0.00")
            Impuestos.SetAttribute("TotalImpuestosTrasladados", Math.Round(ieps_total, 2))
        End If

        If ieps_total > 0 Then
            AgregarIeps = True
        End If

        'Impuestos.SetAttribute("TotalImpuestosRetenidos", "0.00")

        '****Impuesto 
        '001 ISR
        '002 IVA
        '003 IEPS
        Dim AttIva As Integer = 0

        Dim ObjData As New DataControl
        AttIva = ObjData.RunSQLScalarQueryString("SELECT SUM(isnull(iva,0)) FROM tblCFD_Partidas WHERE cfdid='" & Session("CFD").ToString & "'")
        ObjData = Nothing

        If AttIva > 0 Then
            TasaOCuotas = "0.160000"
            AgregarTraslado = True
            TipoFactor = "Tasa"
            TipoImpuesto = "002"
        Else
            TasaOCuotas = "0.000000"
            TipoFactor = "Tasa"
            AgregarTraslado = True
            TipoFactor = "Tasa"
            TipoImpuesto = "002"

            'Dim ExentoBit As Boolean = False
            Dim Exento As Integer
            Dim ObjExento As New DataControl
            'Exento = ObjExento.RunSQLScalarQueryString("select top 1 case when p.tasaid= 4 then 'True' else 'False' end as exento from tblCFD_Partidas a left join tblMisProductos p on p.id=a.productoid where cfdid='" & Session("CFD").ToString & "' order by a.id desc")
            Exento = ObjExento.RunSQLScalarQueryString("select count(1)from tblCFD_Partidas where isnull(tasaceroBit,0)=1 and cfdid='" & Session("CFD").ToString & "'")
            ObjExento = Nothing

            If Exento = 0 Then
                AgregarTraslado = False
            Else
                AgregarTraslado = True
            End If
            'If ExentoBit = True Then
            '    AgregarTraslado = False
            'End If
        End If

        If AplicarRetencion = True Then

            '
            '   Retenciones
            '
            Select Case tipoid
                Case 2, 8 '   Carta aporte Retencion
                    Dim ObjDat As New DataControl
                    Dim tipodocumento2 As Long = ObjDat.RunSQLScalarQuery("select top 1 isnull(serieid,0) from tblCFD where isnull(timbrado,0)=1 and isnull(estatus,0)=1 and uuid='" & txtFolioFiscal.Text & "'")
                    ObjDat = Nothing

                    If tipodocumento2 = 5 Then
                        '
                        '   Retención del 4% Carta Porte
                        '
                        Retenciones = CrearNodo("cfdi:Retenciones")
                        IndentarNodo(Retenciones, 3)
                        Impuestos.AppendChild(Retenciones)

                        '
                        '   ISR
                        '
                        Retencion = CrearNodo("cfdi:Retencion")
                        Retencion.SetAttribute("Impuesto", "002")
                        Retencion.SetAttribute("Importe", Format(CDbl(importe * 0.04), "0.#0"))
                        Retenciones.AppendChild(Retencion)


                        Impuestos.SetAttribute("TotalImpuestosRetenidos", Format(CDbl((importe * 0.04)), "0.#0"))
                        total = total - (importe * 0.04)
                        '
                        '
                    ElseIf tipodocumento2 = 3 Or tipodocumento2 = 6 Then
                        '
                        '   Retenciones
                        '
                        If tipocontribuyenteid = 1 Then
                            'FacturaXML.total = FormatNumber(total, 4)
                        Else
                            '
                            '   ISR
                            '
                            Dim ImporteISR As Double = 0
                            Dim ImporteIVA As Double = 0

                            Retenciones = CrearNodo("cfdi:Retenciones")
                            IndentarNodo(Retenciones, 3)
                            Impuestos.AppendChild(Retenciones)

                            '
                            '   ISR
                            '
                            Retencion = CrearNodo("cfdi:Retencion")
                            Retencion.SetAttribute("Impuesto", "001")
                            ImporteISR = Math.Round((importe * 0.1), 2)

                            If ImporteISR >= 2000 Then
                                ImporteISR = Math.Round((importe * 0.1), 2)
                            End If
                            Retencion.SetAttribute("Importe", Format(CDbl(ImporteISR), "0.#0"))
                            'Retencion.AppendChild(Retencion)
                            Retenciones.AppendChild(Retencion)
                            '
                            '  IVA
                            '
                            Retencion = CrearNodo("cfdi:Retencion")
                            Retencion.SetAttribute("Impuesto", "002")
                            ImporteIVA = Math.Round((iva / 3) * 2, 2)
                            If ImporteIVA >= 2000 Then
                                ImporteIVA = Math.Round((importe * 0.106667), 2)
                            End If
                            Retencion.SetAttribute("Importe", Format(CDbl(ImporteIVA), "0.#0"))
                            Retenciones.AppendChild(Retencion)

                            IndentarNodo(Retenciones, 2)

                            Impuestos.AppendChild(Retenciones)
                            IndentarNodo(Impuestos, 1)

                            Impuestos.SetAttribute("TotalImpuestosRetenidos", Format(CDbl(ImporteISR + ImporteIVA), "0.#0"))
                            'total = FormatNumber((total - (importe * 0.1) - ((iva / 3) * 2)), 2)
                            total = Math.Round((total - (ImporteISR) - (ImporteIVA)), 2)
                        End If
                    End If

                Case 3, 6   '   Recibos de honorarios o arrendamiento
                    '
                    '   Retenciones
                    '
                    If tipocontribuyenteid = 1 Then
                        'FacturaXML.total = FormatNumber(total, 4)
                    Else
                        '
                        '   ISR
                        '
                        Dim ImporteISR As Double = 0
                        Dim ImporteIVA As Double = 0

                        Retenciones = CrearNodo("cfdi:Retenciones")
                        IndentarNodo(Retenciones, 3)
                        Impuestos.AppendChild(Retenciones)

                        '
                        '   ISR
                        '
                        Retencion = CrearNodo("cfdi:Retencion")
                        Retencion.SetAttribute("Impuesto", "001")
                        ImporteISR = Math.Round((importe * 0.1), 2)

                        If ImporteISR >= 2000 Then
                            ImporteISR = Math.Round((importe * 0.1), 2)
                        End If
                        Retencion.SetAttribute("Importe", Format(CDbl(ImporteISR), "0.#0"))
                        'Retencion.AppendChild(Retencion)
                        Retenciones.AppendChild(Retencion)
                        '
                        '  IVA
                        '
                        Retencion = CrearNodo("cfdi:Retencion")
                        Retencion.SetAttribute("Impuesto", "002")
                        ImporteIVA = Math.Round((iva / 3) * 2, 2)
                        If ImporteIVA >= 2000 Then
                            ImporteIVA = Math.Round((importe * 0.106667), 2)
                        End If
                        Retencion.SetAttribute("Importe", Format(CDbl(ImporteIVA), "0.#0"))
                        Retenciones.AppendChild(Retencion)

                        IndentarNodo(Retenciones, 2)

                        Impuestos.AppendChild(Retenciones)
                        IndentarNodo(Impuestos, 1)

                        Impuestos.SetAttribute("TotalImpuestosRetenidos", Format(CDbl(ImporteISR + ImporteIVA), "0.#0"))
                        'total = FormatNumber((total - (importe * 0.1) - ((iva / 3) * 2)), 2)
                        total = Math.Round((total - (ImporteISR) - (ImporteIVA)), 2)
                    End If
                Case 5
                    '
                    '   Retención del 4% Carta Porte
                    '
                    Retenciones = CrearNodo("cfdi:Retenciones")
                    IndentarNodo(Retenciones, 3)
                    Impuestos.AppendChild(Retenciones)

                    '
                    '   ISR
                    '
                    Retencion = CrearNodo("cfdi:Retencion")
                    Retencion.SetAttribute("Impuesto", "002")
                    Retencion.SetAttribute("Importe", Format(CDbl(retencion4), "0.#0"))
                    Retenciones.AppendChild(Retencion)


                    Impuestos.SetAttribute("TotalImpuestosRetenidos", Format(CDbl(retencion4), "0.#0"))
                    total = FormatCurrency(Convert.ToDecimal(FormatNumber(importe, 2)) + Convert.ToDecimal(FormatNumber(iva, 2)) - Convert.ToDecimal(FormatNumber(retencion4, 2)), 2)
                    '
                    '
                Case 7  ' Factura con Retención de 2/3 partes del IVA

                    '   Retenciones
                    '
                    If tipocontribuyenteid = 1 Then
                        'FacturaXML.total = FormatNumber(total, 4)
                    Else
                        '
                        '   IVA
                        '
                        'Dim Retencion As New ComprobanteImpuestosRetencion()
                        'Retencion.importe = FormatNumber(((iva * 2) / 3), 4)
                        'Retencion.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
                        '
                        '   Retenciones
                        '
                        '
                        'FacturaXML.Impuestos.Retenciones = New ComprobanteImpuestosRetencion() {Retencion}
                        'FacturaXML.Impuestos.totalImpuestosRetenidosSpecified = True
                        'FacturaXML.Impuestos.totalImpuestosRetenidos = FormatNumber(((iva * 2) / 3), 4)
                        'FacturaXML.total = FormatNumber((total - ((iva * 2) / 3)), 4)
                        '
                        '
                    End If
                Case 11 ' Retención de 5 al millar (0.5 %)
                    '
                    '   IVA
                    '
                    'Dim Retencion As New ComprobanteImpuestosRetencion()
                    'Retencion.importe = FormatNumber((importe * 0.005), 4)
                    'Retencion.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
                    'FacturaXML.Impuestos.totalImpuestosRetenidosSpecified = True
                    'FacturaXML.Impuestos.totalImpuestosRetenidos = FormatNumber((importe * 0.005), 4)
                    '
                Case 13 ' Retención de 16%
                    '
                    '   IVA
                    '
                    'Dim Retencion As New ComprobanteImpuestosRetencion()
                    'Retencion.importe = FormatNumber((importe * 0.16), 4)
                    'Retencion.impuesto = ComprobanteImpuestosRetencionImpuesto.IVA
                    '
                    '   Retenciones
                    '
                    '
                    'FacturaXML.Impuestos.Retenciones = New ComprobanteImpuestosRetencion() {Retencion}
                    'FacturaXML.Impuestos.totalImpuestosRetenidosSpecified = True
                    'FacturaXML.Impuestos.totalImpuestosRetenidos = FormatNumber((importe * 0.16), 4)
                    'FacturaXML.total = FormatNumber((total - (importe * 0.16)), 4)
                    '
                Case 14 ' Honorarios con Retención de 2/3 partes del IVA
                    '
                    '
            End Select

        End If

        If AgregarTraslado = True Then

            Traslados = CrearNodo("cfdi:Traslados")
            IndentarNodo(Traslados, 3)
            Impuestos.AppendChild(Traslados)

            Traslado = CrearNodo("cfdi:Traslado")
            Traslado.SetAttribute("Impuesto", TipoImpuesto)
            Traslado.SetAttribute("TipoFactor", TipoFactor)
            Traslado.SetAttribute("TasaOCuota", TasaOCuotas)
            If iva > 0 Then
                Traslado.SetAttribute("Importe", Format(CDbl(iva), "0.#0"))
            Else
                Traslado.SetAttribute("Importe", "0.00") 'Format(CDbl(iva), "0.#0"))
            End If

            Traslados.AppendChild(Traslado)

            If AgregarIeps = True Then
                Dim ds As DataSet
                Dim ObjData1 As New DataControl
                ds = ObjData1.FillDataSet("EXEC pIepsTotal @cfdid='" & Session("CFD").ToString & "'")

                Dim tasa As String = ""
                For Each rows As DataRow In ds.Tables(0).Rows

                    Traslado = CrearNodo("cfdi:Traslado")
                    Traslado.SetAttribute("Impuesto", "003")
                    Traslado.SetAttribute("TipoFactor", "Tasa")
                    ieps = rows("ieps")
                    tasa = ieps / 100

                    Dim tasas As String = "0.000000"
                    Dim tasaDec As Decimal = CType(tasa, Decimal)
                    tasa = tasaDec.ToString(tasas)

                    Traslado.SetAttribute("TasaOCuota", tasa)
                    If ieps_total > 0 Then
                        Traslado.SetAttribute("Importe", Math.Round(ieps_total, 2))
                    Else
                        Traslado.SetAttribute("Importe", "0.00")
                    End If

                    Traslados.AppendChild(Traslado)
                Next

            End If

            IndentarNodo(Traslados, 2)
            Impuestos.AppendChild(Traslados)
        End If
        IndentarNodo(Impuestos, 1)
        Nodo.AppendChild(Impuestos)

    End Sub

    Private Sub AsignaCFDUsuario(ByVal cfdid As Long)
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pUsuarios @cmd=7, @userid='" & Session("userid").ToString & "', @cfdid='" & cfdid.ToString & "'")
        ObjData = Nothing
    End Sub


    Private Sub GeneraDocumento()
        '
        Call CargaTotales()
        '


        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        '

        '
        '   Obtiene folio y actualiza cfd
        '
        Dim serie As String = ""
        Dim folio As Long = 0
        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""
        Dim tipoid As Integer = 0

        Dim SQLUpdate As String = ""

        If Not chkAduana.Checked Then
            SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "',@instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='', @numero_pedimento='" & numeropedimento.Text & "', @fecha_factura='" & Now.ToShortDateString & "', @metodopagoid='" & metodopagoid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @lugarexpedicion='" & txtLugarExpedicion.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "'"
        Else
            SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='" & fechapedimento.SelectedDate.Value.ToShortDateString & "', @numero_pedimento='" & numeropedimento.Text & "', @fecha_factura='" & Now.ToShortDateString & "', @metodopagoid='" & metodopagoid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @lugarexpedicion='" & txtLugarExpedicion.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "'"
        End If

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand(SQLUpdate, conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            While rs.Read
                serie = rs("serie").ToString
                folio = rs("folio").ToString
                aprobacion = rs("aprobacion").ToString
                annioaprobacion = rs("annio_solicitud").ToString
                tipoid = rs("tipoid")
            End While
        Catch ex As Exception
            '
        Finally
            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try
        '
        '
        '   Marca el documento como formato
        '
        Dim ObjM As New DataControl
        ObjM.RunSQLQuery("exec pCFD @cmd=33, @cfdid='" & Session("CFD").ToString & "'")
        ObjM = Nothing
        '
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
        '
        '   Genera PDF
        '
        If Not File.Exists(Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & ".pdf") Then
            'GuardaPDF(GeneraPDF_Documento(Session("CFD")), Server.MapPath("~/portalcfd/pdf") & "\iu_" & serie.ToString & folio.ToString & ".pdf")
        End If
        ''
    End Sub


    Private Sub AsignaSerieFolio()
        '
        '   Obtiene serie y folio
        '
        Dim aprobacion As String = ""
        Dim annioaprobacion As String = ""

        'Dim SQLUpdate As String = ""

        'If Not chkAduana.Checked Then
        '    SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @tipopagoId='" & metodopagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "'"
        'Else
        '    SQLUpdate = "exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='" & fechapedimento.SelectedDate.Value.ToShortDateString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @tipopagoId='" & metodopagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "'"
        'End If

        Dim connF As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdF As New SqlCommand("exec pCFD @cmd=17, @cfdid='" & Session("CFD").ToString & "', @tipodocumentoid='" & serieid.SelectedValue.ToString & "', @instrucciones='" & instrucciones.Text & "', @aduana='" & nombreaduana.Text & "', @fecha_pedimento='', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @tipopagoId='" & metodopagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @condicionesid='" & condicionesId.SelectedValue.ToString & "', @tiporelacion='" & tiporelacionid.SelectedValue.ToString & "', @uuid_relacionado='" & txtFolioFiscal.Text.ToString & "'", connF)
        'Dim cmdF As New SqlCommand(SQLUpdate, connF)
        Try

            connF.Open()

            Dim rs As SqlDataReader
            rs = cmdF.ExecuteReader()

            If rs.Read Then
                serie.Value = rs("serie").ToString
                folio.Value = rs("folio").ToString
                aprobacion = rs("aprobacion").ToString
                annioaprobacion = rs("annio_solicitud").ToString
                tipoidF.Value = rs("tipoid")
                tipoid = rs("tipoid")
            End If
        Catch ex As Exception
            '
        Finally
            connF.Close()
            connF.Dispose()
            connF = Nothing
        End Try
        '
        ''
    End Sub
    Private Sub CfdiRelacionados()
        '
        '   Obtiene datos del Cfdi Relacionados
        '
        CrearNodoCfdiRelacionados(Comprobante)
        IndentarNodo(Comprobante, 1)
    End Sub

    Private Sub ConfiguraEmisor()
        '
        '   Obtiene datos del emisor
        '
        '
        '   Datos del Emisor
        '
        'Dim Emisor As New ComprobanteEmisor()

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCFD @cmd=11", conn)
        Try

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                CrearNodoEmisor(Comprobante, rs("razonsocial"), rs("fac_rfc"), rs("regimenid"))
                IndentarNodo(Comprobante, 1)
                _RfcRemitente = rs("fac_rfc")
                _NombreRemitente = rs("razonsocial")
            End If

        Catch ex As Exception
            '
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
        End Try
        '
        ''
    End Sub

    Private Sub ConfiguraReceptor()
        '
        '
        '
        '   Obtiene datos del receptor
        '
        Dim connR As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdR As New SqlCommand("exec pCFD @cmd=12, @clienteId='" & cmbClient.SelectedValue.ToString & "'", connR)
        Try

            connR.Open()

            Dim rs As SqlDataReader
            rs = cmdR.ExecuteReader()

            If rs.Read Then
                CrearNodoReceptor(Comprobante, rs("razonsocial"), rs("fac_rfc"), usoCFDIID.SelectedValue)
                IndentarNodo(Comprobante, 1)
                _RfcDestinatario = rs("fac_rfc")
                _NombreDestinatario = rs("razonsocial")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            connR.Close()
            connR.Dispose()
            connR = Nothing
        End Try
        '
        ''
    End Sub

    Private Sub CrearNodoConceptos(ByVal Nodo As XmlNode)
        '
        '   Agrega Partidas
        '
        Dim Conceptos As XmlElement
        Dim Concepto As XmlElement
        Dim Impuestos As XmlElement
        Dim Traslados As XmlElement
        Dim Traslado As XmlElement
        Dim Retenciones As XmlElement
        Dim Retencion As XmlElement
        Dim CuentaPredial As XmlElement
        Dim Aduanera As XmlElement
        Dim RetencionBit As Boolean = False
        Dim conceptoid As Integer

        Conceptos = CrearNodo("cfdi:Conceptos")
        IndentarNodo(Conceptos, 2)

        ieps = 0
        ieps_total = 0
        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pCFD @cmd=13, @cfdId='" & Session("CFD").ToString & "'", connP)
        Try
            connP.Open()
            '
            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()
            '

            While rs.Read
                'Datos Nuevo 3.3
                '
                conceptoid = rs("id")
                Concepto = CrearNodo("cfdi:Concepto")
                Concepto.SetAttribute("ClaveProdServ", rs("claveprodserv"))
                Concepto.SetAttribute("NoIdentificacion", rs("codigo"))
                Concepto.SetAttribute("Cantidad", rs("cantidad"))
                Concepto.SetAttribute("ClaveUnidad", rs("claveunidad"))
                Concepto.SetAttribute("Unidad", rs("unidad"))
                Concepto.SetAttribute("Descripcion", rs("descripcion"))
                ieps = rs("ieps")
                tmpIeps.Value = rs("ieps") / 100
                ieps_total = rs("ieps_total")

                If rs("descuento") > 0 Then
                    Concepto.SetAttribute("Descuento", Math.Round(rs("descuento"), 2))
                End If

                Concepto.SetAttribute("ValorUnitario", Math.Round(rs("precio"), 2))
                Concepto.SetAttribute("Importe", Math.Round(rs("importe"), 2))
                RetencionBit = rs("retencion")

                Impuestos = CrearNodo("cfdi:Impuestos")

                'Campos Traslados e Retenciones
                Dim Base As Decimal = 0
                Dim Impuesto As String
                Dim TipoFactor As String
                Dim TasaOCuota As String = ""
                Dim TasaCuota As String = 0
                Dim Importe As Decimal = 0

                Traslados = CrearNodo("cfdi:Traslados")
                Traslado = CrearNodo("cfdi:Traslado")

                If rs("descuento") > 0 Then
                    Base = Math.Round(rs("importe") - rs("descuento"), 2)
                    Traslado.SetAttribute("Base", Math.Round(rs("importe") - rs("descuento"), 2))
                Else
                    Traslado.SetAttribute("Base", Math.Round(rs("importe"), 2))
                    Base = Math.Round(rs("importe"), 2)
                End If

                If CBool(rs("exento")) = False Then
                    Traslado.SetAttribute("Impuesto", "002")
                    Impuesto = "002"
                    Traslado.SetAttribute("TipoFactor", "Tasa")
                    TipoFactor = "Tasa"
                    Traslado.SetAttribute("TasaOCuota", rs("tasaocuota"))
                    TasaOCuota = rs("tasaocuota")
                    Traslado.SetAttribute("Importe", Format(rs("iva"), "#0.00"))
                    Importe = Format(rs("iva"), "#0.00")
                Else
                    Traslado.SetAttribute("Impuesto", "002")
                    Impuesto = "002"
                    Traslado.SetAttribute("TipoFactor", "Exento")
                    TipoFactor = "Exento"
                End If

                Dim Objdata As New DataControl
                Objdata.RunSQLQuery("exec pCFD @cmd=42, @conceptoid='" & conceptoid.ToString & "', @base='" & Base.ToString & "', @impuesto='" & Impuesto.ToString & "', @tipofactor='" & TipoFactor.ToString & "', @tasaOcuota='" & TasaOCuota.ToString & "', @importe1='" & Importe.ToString & "'")
                Objdata = Nothing


                Traslados.AppendChild(Traslado)

                If ieps >= 1 Then
                    Traslado = CrearNodo("cfdi:Traslado")

                    If rs("descuento") > 0 Then
                        Base = Math.Round(rs("importe") - rs("descuento"), 2)
                        Traslado.SetAttribute("Base", Format(Base, "#0.00"))
                    Else
                        Base = Math.Round(rs("importe"), 2) '
                        Traslado.SetAttribute("Base", Format(Base, "#0.00"))
                    End If

                    Traslado.SetAttribute("Impuesto", "003")
                    Impuesto = "003"
                    Traslado.SetAttribute("TipoFactor", "Tasa")
                    TipoFactor = "Tasa"
                    TasaCuota = ieps / 100

                    Dim tasa As String = "0.000000"
                    Dim tasaDec As Decimal = CType(TasaCuota, Decimal)
                    TasaCuota = tasaDec.ToString(tasa)

                    Traslado.SetAttribute("TasaOCuota", TasaCuota)
                    Importe = Math.Round(rs("ieps_total"), 2)
                    Traslado.SetAttribute("Importe", Format(Importe, "#0.00"))

                    Dim Objdat As New DataControl
                    Objdat.RunSQLQuery("exec pCFD @cmd=42, @conceptoid='" & conceptoid.ToString & "', @base='" & Base.ToString & "', @impuesto='" & Impuesto.ToString & "', @tipofactor='" & TipoFactor.ToString & "', @tasaOcuota='" & TasaCuota.ToString & "', @importe1='" & Importe.ToString & "'")
                    Objdat = Nothing

                    Traslados.AppendChild(Traslado)
                End If

                Impuestos.AppendChild(Traslados)

                'AplicarRetencion = False
                If RetencionBit = True Then
                    If rs("tipoRetencion") > 0 Then
                        If rs("tipoRetencion") = 3 Or rs("tipoRetencion") = 6 Then 'Recibos de honorarios o arrendamiento
                            Retenciones = CrearNodo("cfdi:Retenciones")
                            Retencion = CrearNodo("cfdi:Retencion")

                            '
                            '   IVA
                            '
                            Retencion.SetAttribute("Base", Format(CDbl(rs("importe")), "#.#0"))
                            Base = Format(CDbl(rs("importe")), "#.#0")
                            Retencion.SetAttribute("Impuesto", "001")
                            Impuesto = "001"
                            Retencion.SetAttribute("TipoFactor", "Tasa")
                            TipoFactor = "Tasa"
                            Importe = Math.Round(CDec(rs("importe") * 0.1), 2)

                            If Importe >= 2000 Then
                                Importe = Math.Round(CDec(rs("importe") * 0.1), 2)
                            End If

                            Retencion.SetAttribute("TasaOCuota", "0.100000")
                            TasaOCuota = "0.100000"
                            Retencion.SetAttribute("Importe", Format(CDbl(Importe), "#.#0"))

                            Dim DataControl As New DataControl
                            DataControl.RunSQLQuery("exec pCFD @cmd=43, @conceptoid='" & conceptoid.ToString & "', @base='" & Base.ToString & "', @impuesto='" & Impuesto.ToString & "', @tipofactor='" & TipoFactor.ToString & "', @tasaOcuota='" & TasaOCuota.ToString & "', @importe1='" & Importe.ToString & "'")
                            DataControl = Nothing

                            Retenciones.AppendChild(Retencion)

                            Retencion = CrearNodo("cfdi:Retencion")
                            '
                            '   Retenciones
                            '
                            '
                            Retencion.SetAttribute("Base", Format(CDbl(rs("importe")), "#.#0"))
                            Base = Format(CDbl(rs("importe")), "#.#0")
                            Retencion.SetAttribute("Impuesto", "002")
                            Impuesto = "002"
                            Retencion.SetAttribute("TipoFactor", "Tasa")
                            TipoFactor = "Tasa"

                            Importe = Math.Round((rs("iva") / 3) * 2, 2) 'Math.Round(CDec(rs("importe")), 2)

                            If Importe >= 2000 Then
                                Importe = Format(CDbl(rs("importe") * 0.106667), "#.#0")
                                Retencion.SetAttribute("TasaOCuota", "0.106667")
                                TasaOCuota = "0.106667"
                            Else
                                If rs("iva") >= 1 Then
                                    Importe = Math.Round((rs("iva") / 3) * 2, 2)
                                    Retencion.SetAttribute("TasaOCuota", "0.106666")
                                    TasaOCuota = "0.106666"
                                Else
                                    Importe = Math.Round((rs("iva") / 3) * 2, 2)
                                    Retencion.SetAttribute("TasaOCuota", "0.000000")
                                    TasaOCuota = "0.000000"
                                End If
                            End If

                            Retencion.SetAttribute("Importe", Math.Round(Importe, 2)) 'Format(CDbl(Importe), "#.#0"))

                            Dim objDat As New DataControl
                            objDat.RunSQLQuery("exec pCFD @cmd=43, @conceptoid='" & conceptoid.ToString & "', @base='" & Base.ToString & "', @impuesto='" & Impuesto.ToString & "', @tipofactor='" & TipoFactor.ToString & "', @tasaOcuota='" & TasaOCuota.ToString & "', @importe1='" & Importe.ToString & "'")
                            objDat = Nothing

                            Retenciones.AppendChild(Retencion)

                            Impuestos.AppendChild(Retenciones)

                            AplicarRetencion = True

                        ElseIf rs("tipoRetencion") = 5 Then ' Retención del 4% Carta Porte
                            '
                            Retenciones = CrearNodo("cfdi:Retenciones")
                            Retencion = CrearNodo("cfdi:Retencion")

                            Retencion.SetAttribute("Base", Format(CDbl(rs("importe")), "#.#0"))
                            Base = Format(CDbl(rs("importe")), "#.#0")
                            Retencion.SetAttribute("Impuesto", "002")
                            Impuesto = "002"
                            Retencion.SetAttribute("TipoFactor", "Tasa")
                            TipoFactor = "Tasa"
                            Retencion.SetAttribute("TasaOCuota", "0.040000")
                            Importe = Format(CDbl(rs("importe") * 0.04), "#.#0")
                            If Importe >= 2000 Then
                                Importe = Math.Round(rs("importe") * 0.04) + 0.01
                                Retencion.SetAttribute("TasaOCuota", "0.040000")
                                TasaOCuota = "0.040000"
                            Else
                                Retencion.SetAttribute("TasaOCuota", "0.040000")
                                TasaOCuota = "0.040000"
                            End If
                            Retencion.SetAttribute("Importe", Format(CDbl(rs("importe") * 0.04), "#.#0"))

                            Dim objDat As New DataControl
                            objDat.RunSQLQuery("exec pCFD @cmd=43, @conceptoid='" & conceptoid.ToString & "', @base='" & Base.ToString & "', @impuesto='" & Impuesto.ToString & "', @tipofactor='" & TipoFactor.ToString & "', @tasaOcuota='" & TasaOCuota.ToString & "', @importe1='" & Importe.ToString & "'")
                            objDat = Nothing

                            Retenciones.AppendChild(Retencion)

                            Impuestos.AppendChild(Retenciones)

                            AplicarRetencion = True

                        ElseIf rs("tipoRetencion") = 7 Then  ' Factura con Retención de 2/3 partes del IVA
                            '
                            '   IVA
                            '
                            Retenciones = CrearNodo("cfdi:Retenciones")
                            Retencion = CrearNodo("cfdi:Retencion")

                            Retencion.SetAttribute("Base", Format(CDbl(rs("importe")), "#.#0"))
                            Retencion.SetAttribute("Impuesto", "002")
                            Retencion.SetAttribute("TipoFactor", "Tasa")
                            Retencion.SetAttribute("TasaOCuota", " 0.040000")
                            Retencion.SetAttribute("Importe", Format(CDbl(rs("importe") * 0.04), "#.#0"))

                            Retenciones.AppendChild(Retencion)

                            Impuestos.AppendChild(Retenciones)
                            '
                            '   Retenciones
                            '
                            '
                            '
                            '
                            AplicarRetencion = True
                        End If

                    End If
                End If
                Concepto.AppendChild(Impuestos)
                'Aqui agregar CuentaPredial si es Arrendamiento

                If serieid.SelectedValue = 3 Then
                    CuentaPredial = CrearNodo("cfdi:CuentaPredial")
                    CuentaPredial.SetAttribute("Numero", rs("cuentaPredial"))

                    Concepto.AppendChild(CuentaPredial)
                End If

                If chkAduana.Checked = True Then
                    Aduanera = CrearNodo("cfdi:InformacionAduanera")
                    Aduanera.SetAttribute("NumeroPedimento", numeropedimento.Text)
                    Concepto.AppendChild(Aduanera)
                End If

                Conceptos.AppendChild(Concepto)
                IndentarNodo(Conceptos, 2)
                Concepto = Nothing
            End While
            '
            '
        Catch ex As Exception
            '
            Response.Write(ex.Message.ToString)
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try

        Nodo.AppendChild(Conceptos)
        ''
    End Sub
    Private Sub CrearNodoCfdiRelacionados(ByVal Nodo As XmlNode)
        Dim Complemento As XmlElement
        'Dim Relacionados As XmlElement

        Dim CfdiRelacionados As XmlElement
        Dim DocumentoRelacionado As XmlElement

        CfdiRelacionados = CrearNodo("cfdi:CfdiRelacionados")
        IndentarNodo(CfdiRelacionados, 1)

        CfdiRelacionados.SetAttribute("TipoRelacion", tiporelacionid.SelectedValue)
        IndentarNodo(CfdiRelacionados, 2)

        DocumentoRelacionado = CrearNodo("cfdi:CfdiRelacionado")
        DocumentoRelacionado.SetAttribute("UUID", txtFolioFiscal.Text)

        'IndentarNodo(DocumentoRelacionado, 2)

        CfdiRelacionados.AppendChild(DocumentoRelacionado)
        IndentarNodo(CfdiRelacionados, 1)
        'IndentarNodo(Complemento, 1)
        Nodo.AppendChild(CfdiRelacionados)
        'IndentarNodo(Conceptos, 1)
    End Sub

    Private Sub cfdnotimbrado()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=23, @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub cfdtimbrado()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=24, @uuid='" & UUID.ToString & "', @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub GuadarMetodoPago()
        Dim Objdata As New DataControl
        Objdata.RunSQLQuery("exec pCFD @cmd=25, @metodopagoid='" & metodopagoid.SelectedValue & "', @usocfdi='" & usoCFDIID.SelectedValue & "', @serieid='" & serieid.SelectedValue & "', @tipocambio='" & tipocambio.Text & "', @cfdid='" & Session("CFD").ToString & "'")
        Objdata = Nothing
    End Sub

    Private Sub obtienellave()
        Dim connX As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdX As New SqlCommand("exec pCFD @cmd=19, @clienteid='" & Session("clienteid").ToString & "', @cfdid='" & Session("CFD").ToString & "'", connX)
        Try

            connX.Open()

            Dim rs As SqlDataReader
            rs = cmdX.ExecuteReader()

            If rs.Read Then
                archivoLlavePrivada = Server.MapPath("~/portalcfd/llave") & "\" & rs("archivo_llave_privada")
                contrasenaLlavePrivada = rs("contrasena_llave_privada")
                archivoCertificado = Server.MapPath("~/portalcfd/certificados") & "\" & rs("archivo_certificado")
            End If

        Catch ex As Exception
            '
        Finally

            connX.Close()
            connX.Dispose()
            connX = Nothing

        End Try
    End Sub

    Private Function Parametros(ByVal codigoUsuarioProveedor As String, ByVal codigoUsuario As String, ByVal idSucursal As Integer, ByVal textoXml As String) As String
        Dim root As XmlNode
        Dim xmlParametros As New XmlDocument()

        If xmlParametros.ChildNodes.Count = 0 Then
            Dim declarationNode As XmlNode = xmlParametros.CreateXmlDeclaration("1.0", "UTF-8", String.Empty)

            xmlParametros.AppendChild(declarationNode)

            root = xmlParametros.CreateElement("Parametros")
            xmlParametros.AppendChild(root)
        Else
            root = xmlParametros.DocumentElement
            root.RemoveAll()
        End If

        Dim attribute As XmlAttribute = root.OwnerDocument.CreateAttribute("Version")
        attribute.Value = "1.0"
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("CodigoUsuarioProveedor")
        attribute.Value = codigoUsuarioProveedor
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("CodigoUsuario")
        attribute.Value = codigoUsuario
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("IdSucursal")
        attribute.Value = idSucursal.ToString()
        root.Attributes.Append(attribute)

        attribute = root.OwnerDocument.CreateAttribute("TextoXml")
        attribute.Value = textoXml
        root.Attributes.Append(attribute)

        Return xmlParametros.InnerXml
    End Function

    Private Sub generacbb()
        Dim CadenaCodigoBidimensional As String = ""
        Dim FinalSelloDigitalEmisor As String = ""
        UUID = ""
        Dim rfcE As String = ""
        Dim rfcR As String = ""
        Dim total As String = ""
        Dim sello As String = ""
        '
        '   Obtiene datos del cfdi para construir string del CBB
        '
        rfcE = GetXmlAttribute(Server.MapPath("cfd_storage") & "\iu_" & serie.Value & folio.Value & "_timbrado.xml", "Rfc", "cfdi:Emisor")
        rfcR = GetXmlAttribute(Server.MapPath("cfd_storage") & "\iu_" & serie.Value & folio.Value & "_timbrado.xml", "Rfc", "cfdi:Receptor")
        total = GetXmlAttribute(Server.MapPath("cfd_storage") & "\iu_" & serie.Value & folio.Value & "_timbrado.xml", "Total", "cfdi:Comprobante")
        UUID = GetXmlAttribute(Server.MapPath("cfd_storage") & "\iu_" & serie.Value & folio.Value & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
        sello = GetXmlAttribute(Server.MapPath("cfd_storage") & "\iu_" & serie.Value & folio.Value & "_timbrado.xml", "SelloCFD", "tfd:TimbreFiscalDigital")
        FinalSelloDigitalEmisor = Mid(sello, (Len(sello) - 7))
        '
        Dim totalDec As Decimal = CType(total, Decimal)
        '
        CadenaCodigoBidimensional = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx" & "?id=" & UUID & "&re=" & rfcE & "&rr=" & rfcR & "&tt=" & totalDec.ToString & "&fe=" & FinalSelloDigitalEmisor
        '
        '   Genera gráfico
        '
        Dim qrCodeEncoder As QRCodeEncoder = New QRCodeEncoder
        qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE
        qrCodeEncoder.QRCodeScale = 6
        qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.L
        'La versión "0" calcula automáticamente el tamaño
        qrCodeEncoder.QRCodeVersion = 0

        qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor)
        qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor)

        Dim CBidimensional As Drawing.Image
        CBidimensional = qrCodeEncoder.Encode(CadenaCodigoBidimensional, System.Text.Encoding.UTF8)
        CBidimensional.Save(Server.MapPath("~/portalCFD/cbb/") & serie.Value & folio.Value & ".png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Private Function TotalPartidas(ByVal cfdId As Long) As Long
        Dim Total As Long = 0
        Dim connP As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmdP As New SqlCommand("exec pCFD @cmd=15, @cfdid='" & cfdId.ToString & "'", connP)
        Try

            connP.Open()

            Dim rs As SqlDataReader
            rs = cmdP.ExecuteReader()

            If rs.Read Then
                Total = rs("total")
            End If

        Catch ex As Exception
            '
        Finally
            connP.Close()
            connP.Dispose()
            connP = Nothing
        End Try
        Return Total
    End Function

    Private Function Num2Text(ByVal value As Decimal) As String
        Select Case value
            Case 0 : Num2Text = "CERO"
            Case 1 : Num2Text = "UN"
            Case 2 : Num2Text = "DOS"
            Case 3 : Num2Text = "TRES"
            Case 4 : Num2Text = "CUATRO"
            Case 5 : Num2Text = "CINCO"
            Case 6 : Num2Text = "SEIS"
            Case 7 : Num2Text = "SIETE"
            Case 8 : Num2Text = "OCHO"
            Case 9 : Num2Text = "NUEVE"
            Case 10 : Num2Text = "DIEZ"
            Case 11 : Num2Text = "ONCE"
            Case 12 : Num2Text = "DOCE"
            Case 13 : Num2Text = "TRECE"
            Case 14 : Num2Text = "CATORCE"
            Case 15 : Num2Text = "QUINCE"
            Case Is < 20 : Num2Text = "DIECI" & Num2Text(value - 10)
            Case 20 : Num2Text = "VEINTE"
            Case Is < 30 : Num2Text = "VEINTI" & Num2Text(value - 20)
            Case 30 : Num2Text = "TREINTA"
            Case 40 : Num2Text = "CUARENTA"
            Case 50 : Num2Text = "CINCUENTA"
            Case 60 : Num2Text = "SESENTA"
            Case 70 : Num2Text = "SETENTA"
            Case 80 : Num2Text = "OCHENTA"
            Case 90 : Num2Text = "NOVENTA"
            Case Is < 100 : Num2Text = Num2Text(Int(value \ 10) * 10) & " Y " & Num2Text(value Mod 10)
            Case 100 : Num2Text = "CIEN"
            Case Is < 200 : Num2Text = "CIENTO " & Num2Text(value - 100)
            Case 200, 300, 400, 600, 800 : Num2Text = Num2Text(Int(value \ 100)) & "CIENTOS"
            Case 500 : Num2Text = "QUINIENTOS"
            Case 700 : Num2Text = "SETECIENTOS"
            Case 900 : Num2Text = "NOVECIENTOS"
            Case Is < 1000 : Num2Text = Num2Text(Int(value \ 100) * 100) & " " & Num2Text(value Mod 100)
            Case 1000 : Num2Text = "MIL"
            Case Is < 2000 : Num2Text = "MIL " & Num2Text(value Mod 1000)
            Case Is < 1000000 : Num2Text = Num2Text(Int(value \ 1000)) & " MIL"
                If value Mod 1000 Then Num2Text = Num2Text & " " & Num2Text(value Mod 1000)
            Case 1000000 : Num2Text = "UN MILLON"
            Case Is < 2000000 : Num2Text = "UN MILLON " & Num2Text(value Mod 1000000)
            Case Is < 1000000000000.0# : Num2Text = Num2Text(Int(value / 1000000)) & " MILLONES "
                If (value - Int(value / 1000000) * 1000000) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000) * 1000000)
            Case 1000000000000.0# : Num2Text = "UN BILLON"
            Case Is < 2000000000000.0# : Num2Text = "UN BILLON " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
            Case Else : Num2Text = Num2Text(Int(value / 1000000000000.0#)) & " BILLONES"
                If (value - Int(value / 1000000000000.0#) * 1000000000000.0#) Then Num2Text = Num2Text & " " & Num2Text(value - Int(value / 1000000000000.0#) * 1000000000000.0#)
        End Select
    End Function

    'Private Function CadenaOriginalComplemento() As String

    '    '
    '    '   Obtiene los valores del timbre de respuesta
    '    '
    '    Dim selloSAT As String = ""
    '    Dim noCertificadoSAT As String = ""
    '    Dim selloCFD As String = ""
    '    Dim fechaTimbrado As String = ""
    '    Dim UUID As String = ""
    '    Dim Version As String = ""
    '    '
    '    '
    '    'link_F18_timbrado
    '    Dim s_RutaRespuestaPAC As String = Server.MapPath("cfd_storage") & "\" & "link_timbre_" & serie.Value & folio.Value & ".xml"
    '    Dim respuestaPAC As New Timbrado()
    '    Dim objStreamReader As New StreamReader(s_RutaRespuestaPAC)
    '    Dim Xml As New XmlSerializer(respuestaPAC.[GetType]())
    '    respuestaPAC = DirectCast(Xml.Deserialize(objStreamReader), Timbrado)
    '    objStreamReader.Close()

    '    '
    '    'Crear el objeto timbre para asignar los valores de la respuesta PAC
    '    fechaTimbrado = respuestaPAC.Items(0).Informacion(0).Timbre(0).FechaTimbrado
    '    noCertificadoSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).noCertificadoSAT.ToString
    '    selloCFD = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloCFD.ToString
    '    selloSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloSAT.ToString
    '    UUID = respuestaPAC.Items(0).Informacion(0).Timbre(0).UUID.ToString
    '    Version = respuestaPAC.Items(0).Informacion(0).Timbre(0).version.ToString
    '    '
    '    Dim cadena As String = ""
    '    cadena = "||" & Version & "|" & UUID & "|" & fechaTimbrado & "|" & selloCFD & "|" & noCertificadoSAT & "||"
    '    Return cadena
    '    ''
    'End Function

    Private Function CadenaOriginalComplemento() As String

        ''
        ''   Obtiene los valores del timbre de respuesta
        ''
        'Dim selloSAT As String = ""
        'Dim noCertificadoSAT As String = ""
        'Dim selloCFD As String = ""
        'Dim fechaTimbrado As String = ""
        'Dim UUID As String = ""
        'Dim Version As String = ""
        ''
        ''
        ''link_F18_timbrado
        'Dim s_RutaRespuestaPAC As String = Server.MapPath("cfd_storage") & "\" & "iu_" & serie.Value & folio.Value & "_timbrado.xml"
        'Dim respuestaPAC As New Timbrado()
        'Dim objStreamReader As New StreamReader(s_RutaRespuestaPAC)
        'Dim Xml As New XmlSerializer(respuestaPAC.[GetType]())
        'respuestaPAC = DirectCast(Xml.Deserialize(objStreamReader), Timbrado)
        'objStreamReader.Close()

        ''
        ''Crear el objeto timbre para asignar los valores de la respuesta PAC
        'fechaTimbrado = respuestaPAC.Items(0).Informacion(0).Timbre(0).FechaTimbrado
        'noCertificadoSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).noCertificadoSAT.ToString
        'selloCFD = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloCFD.ToString
        'selloSAT = respuestaPAC.Items(0).Informacion(0).Timbre(0).selloSAT.ToString
        'UUID = respuestaPAC.Items(0).Informacion(0).Timbre(0).UUID.ToString
        'Version = respuestaPAC.Items(0).Informacion(0).Timbre(0).version.ToString
        ''
        'Dim cadena As String = ""
        'cadena = "||" & Version & "|" & UUID & "|" & fechaTimbrado & "|" & selloCFD & "|" & noCertificadoSAT & "||"
        'Return cadena
        '
        '
        '   Obtiene los valores del timbre de respuesta
        '
        Dim Version As String = ""
        Dim selloSAT As String = ""
        Dim UUID As String = ""
        Dim noCertificadoSAT As String = ""
        Dim selloCFD As String = ""
        Dim FechaTimbrado As String = ""
        Dim RfcProvCertif As String = ""

        Version = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "Version", "tfd:TimbreFiscalDigital")
        UUID = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
        FechaTimbrado = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
        RfcProvCertif = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
        selloCFD = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "SelloCFD", "tfd:TimbreFiscalDigital")
        noCertificadoSAT = GetXmlAttribute(Server.MapPath("~/portalcfd/cfd_storage") & "\" & "iu_" & serie.Value.ToString & folio.Value.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
        '
        'Dim FlujoReader As XmlTextReader = Nothing
        'Dim i As Integer
        '' leer del fichero e ignorar los nodos vacios
        'FlujoReader = New XmlTextReader()
        'FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        '' analizar el fichero y presentar cada nodo
        'While FlujoReader.Read()
        '    Select Case FlujoReader.NodeType
        '        Case XmlNodeType.Element
        '            If FlujoReader.Name = "tfd:TimbreFiscalDigital" Then
        '                For i = 0 To FlujoReader.AttributeCount - 1
        '                    FlujoReader.MoveToAttribute(i)
        '                    If FlujoReader.Name = "fechaTimbrado" Or FlujoReader.Name = "FechaTimbrado" Then
        '                        fechaTimbrado = FlujoReader.Value
        '                    ElseIf FlujoReader.Name = "UUID" Then
        '                        UUID = FlujoReader.Value
        '                    ElseIf FlujoReader.Name = "NoCertificadoSAT" Then
        '                        noCertificadoSAT = FlujoReader.Value
        '                    ElseIf FlujoReader.Name = "SelloCFD" Then
        '                        selloCFD = FlujoReader.Value
        '                    ElseIf FlujoReader.Name = "SelloSAT" Then
        '                        selloSAT = FlujoReader.Value
        '                    ElseIf FlujoReader.Name = "Version" Then
        '                        Version = FlujoReader.Value
        '                    ElseIf FlujoReader.Name = "RfcProvCertif" Then
        '                        RfcProvCertif = FlujoReader.Value
        '                    End If
        '                Next
        '            End If
        '    End Select
        'End While
        '
        '
        Dim cadena As String = ""
        cadena = "||" & Version & "|" & UUID & "|" & FechaTimbrado & "|" & RfcProvCertif & "|" & selloCFD & "|" & noCertificadoSAT & "||"
        Return cadena
        '
    End Function

#End Region

#Region "Manejo de PDF"

    Private Function GeneraPDF(ByVal cfdid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim numeroaprobacion As String = ""
        Dim anoAprobacion As String = ""
        Dim fechaHora As String = ""
        Dim noCertificado As String = ""
        Dim razonsocial As String = ""
        Dim callenum As String = ""
        Dim colonia As String = ""
        Dim ciudad As String = ""
        Dim rfc As String = ""
        Dim em_razonsocial As String = ""
        Dim em_callenum As String = ""
        Dim em_colonia As String = ""
        Dim em_ciudad As String = ""
        Dim em_rfc As String = ""
        Dim em_regimen As String = ""
        Dim importe As Decimal = 0
        Dim importetasacero As Decimal = 0
        Dim iva As Decimal = 0
        Dim total As Decimal = 0
        Dim CantidadTexto As String = ""
        Dim condiciones As String = ""
        Dim enviara As String = ""
        Dim instrucciones As String = ""
        Dim pedimento As String = ""
        Dim retencion As Decimal = 0
        Dim tipoid As Integer = 0
        Dim divisaid As Integer = 1
        Dim expedicionLinea1 As String = ""
        Dim expedicionLinea2 As String = ""
        Dim expedicionLinea3 As String = ""
        Dim porcentaje As Decimal = 0
        Dim plantillaid As Integer = 1
        Dim tipopago As String = ""
        Dim formapago As String = ""
        Dim numctapago As String = ""
        Dim serie As String = ""
        Dim folio As Integer = 0
        Dim usoCFDI As String = ""
        Dim tipoComprobante As String = ""
        Dim LugarExpedicion As String = ""
        Dim CuentaPredial As String = ""
        Dim tipocambio As Decimal = 0
        Dim tiporelacion As String = ""
        Dim TipoMoneda As String = ""

        Dim ImpuestoTrasladado As Decimal = 0
        Dim ImpuestoRetencion As Decimal = 0
        Dim importesinmaniobra As Decimal = 0
        Dim importemaniobra As Decimal = 0

        Dim ds As DataSet = New DataSet

        Try
            Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                serie = rs("serie")
                folio = rs("folio")
                tipoid = rs("tipoid")
                em_razonsocial = rs("em_razonsocial")
                em_callenum = rs("em_callenum")
                em_colonia = rs("em_colonia")
                em_ciudad = rs("em_ciudad")
                em_rfc = rs("em_rfc")
                em_regimen = rs("regimen")
                razonsocial = rs("razonsocial")
                callenum = rs("callenum")
                colonia = rs("colonia")
                ciudad = rs("ciudad")
                rfc = rs("rfc")
                importe = rs("importe")
                iva = rs("iva")
                total = rs("total")
                ieps_total = rs("ieps_total")
                divisaid = rs("divisaid")
                fechaHora = rs("fecha_factura").ToString
                condiciones = "Condiciones: " & rs("condiciones").ToString
                enviara = rs("enviara").ToString
                instrucciones = rs("instrucciones")
                If rs("aduana") = "" Or rs("numero_pedimento") = "" Then
                    pedimento = ""
                Else
                    pedimento = "Aduana: " & rs("aduana") & vbCrLf & "Fecha: " & rs("fecha_pedimento").ToString & vbCrLf & "Número: " & rs("numero_pedimento").ToString
                End If
                expedicionLinea1 = rs("expedicionLinea1")
                expedicionLinea2 = rs("expedicionLinea2")
                expedicionLinea3 = rs("expedicionLinea3")
                CuentaPredial = rs("cuentapredial")
                porcentaje = rs("porcentaje")
                plantillaid = rs("plantillaid")
                tipocontribuyenteid = rs("tipocontribuyenteid")
                tipopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
                usoCFDI = rs("usocfdi")
                importesinmaniobra = rs("importesinmaniobra")
                importemaniobra = rs("importemaniobra")
            End If
            rs.Close()
            '
        Catch ex As Exception
            '
            Response.Write(ex.ToString)
        Finally

            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try



        Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)



        If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
            If divisaid = 1 Then
                TipoMoneda = "MXN"
                CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
            Else
                TipoMoneda = "USD"
                CantidadTexto = "(  " + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD. )"
            End If
        Else
            TipoMoneda = "MXN"
            CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
        End If
        '
        '
        '
        If ckCartaPorte.Checked = True Then
            Dim reporte As New FormatosPDF.formato_cfdi_complemento_carta_porte

            reporte.ReportParameters("doc").Value = "I"
            reporte.ReportParameters("plantillaId").Value = plantillaid
            reporte.ReportParameters("cfdiId").Value = cfdid
            Select Case tipoid
                Case 1, 4, 7
                    reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                Case 2, 8
                    reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No.    " & serie.ToString & folio.ToString
                Case 5
                    reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                    reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO     EFECTOS FISCALES AL PAGO"
                Case 6
                    reporte.ReportParameters("txtDocumento").Value = "Honorarios No.    " & serie.ToString & folio.ToString
                Case Else
                    reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
            End Select
            reporte.ReportParameters("txtCondicionesPago").Value = condiciones
            reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
            reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
            reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
            LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "LugarExpedicion", "cfdi:Comprobante")
            reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
            reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
            reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
            reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
            reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
            reporte.ReportParameters("txtClienteCalleNum").Value = callenum
            reporte.ReportParameters("txtClienteColonia").Value = colonia
            reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
            reporte.ReportParameters("txtPACCertifico").Value = "RFC Prov. Certif.: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
            tipoComprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")
            reporte.ReportParameters("txtClienteRFC").Value = "Receptor RFC: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
            '
            If tipoComprobante.ToString <> "" Then

                'If tipoComprobante.ToString = "E" Then
                '    tiporelacion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoRelacion", "cfdi:CfdiRelacionados")
                '    reporte.ReportParameters("txtTipoRelacion").Value = "Tipo de Relacion: " & tiporelacion.ToString
                '    reporte.ReportParameters("txtUUIDNC").Value = "UUID:" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "cfdi:CfdiRelacionado")
                'End If

                Dim ObjData As New DataControl
                tipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & tipoComprobante.ToString & "'")
                ObjData = Nothing
            End If
            '
            reporte.ReportParameters("txtTipoComprobante").Value = tipoComprobante.ToString
            reporte.ReportParameters("txtMoneda").Value = TipoMoneda.ToString
            If TipoMoneda = "USD" Then
                reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoCambio", "cfdi:Comprobante")
            Else
                reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & ""
            End If
            reporte.ReportParameters("txtCuentaPredial").Value = "Cuenta Predial: " & ""

            reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
            reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
            '
            reporte.ReportParameters("txtInstrucciones").Value = instrucciones
            reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
            '
            reporte.ReportParameters("txtImporte").Value = FormatNumber(importesinmaniobra, 2).ToString
            reporte.ReportParameters("txtManiobra").Value = FormatNumber(importemaniobra, 2).ToString
            reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe, 2).ToString
            importetasacero = 0
            reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString

            reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
            reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
            'IEPS
            reporte.ReportParameters("txtDescuento").Value = FormatNumber(totaldescuento, 2).ToString
            '
            ImpuestoTrasladado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
            Try
                ImpuestoRetencion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosRetenidos", "cfdi:Impuestos")
            Catch ex As Exception
                ImpuestoRetencion = 0
            End Try
            reporte.ReportParameters("txtTotalImpuestoTrasladado").Value = FormatCurrency(ImpuestoTrasladado, 2).ToString
            reporte.ReportParameters("txtTotalImpuestoRetencion").Value = FormatCurrency(ImpuestoRetencion, 2).ToString

            reporte.ReportParameters("txtTotal").Value = FormatNumber(total, 2).ToString
            '
            reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
            reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
            reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
            If porcentaje > 0 Then
                reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
            End If
            '
            If tipoid = 5 Or tipoid = 10 Then
                retencion = FormatNumber((importesinmaniobra * 0.04), 2)
                reporte.ReportParameters("txtRetencion").Value = FormatNumber(retencion, 2).ToString
                reporte.ReportParameters("txtTotal").Value = FormatNumber(total - retencion, 2).ToString
                largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                If divisaid = 1 Then
                    CantidadTexto = "(  " + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                Else
                    CantidadTexto = "(  " + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
                End If
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
            End If
            '
            reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
            reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
            reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
            reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
            reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
            reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI
            '
            '
            '
            reporte.ReportParameters("cartaporteid").Value = _CartaPorteID
            '
            '
            '
            Dim Obj As New DataControl
            Dim dt As DataSet = Obj.FillDataSet("EXEC pAutotransportes @cmd=2, @id='" & cmbAutotransporte.SelectedValue & "'")
            For Each row As DataRow In dt.Tables(0).Rows

                reporte.ReportParameters("txtClaveSCT").Value = row("PermSCTId")
                reporte.ReportParameters("tctNumeroSCT").Value = row("NumPermisoSCT")
                reporte.ReportParameters("tctNombreSCT").Value = row("claveUnidadNom")
                reporte.ReportParameters("txtAseguradoraSCT").Value = row("NombreAseg")
                reporte.ReportParameters("txtPolizaSCT").Value = row("NumPolizaSeguro")

                Dim AseguradoraMedAmbiente As String
                AseguradoraMedAmbiente = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "AseguraMedAmbiente", "cartaporte20:Seguros")
                If AseguradoraMedAmbiente.Length < 1 Then
                    AseguradoraMedAmbiente = "NA"
                End If
                reporte.ReportParameters("txtAseguradoraMedAmbiente").Value = AseguradoraMedAmbiente

                Dim PolizaMedAmbiente As String
                PolizaMedAmbiente = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "PolizaMedAmbiente", "cartaporte20:Seguros")
                If PolizaMedAmbiente.Length < 1 Then
                    PolizaMedAmbiente = "NA"
                End If
                reporte.ReportParameters("txtPolizaMedAmbiente").Value = PolizaMedAmbiente

                reporte.ReportParameters("txtClaveAuto").Value = row("ConfigVehicularId")
                reporte.ReportParameters("txtDescAuto").Value = row("configVehicularNom")
                reporte.ReportParameters("txtPlacaAuto").Value = row("PlacaVM")
                reporte.ReportParameters("txtAnioAuto").Value = row("AnioModeloVM")

            Next
            '
            '
            'Totales
            '
            '
            Dim data As New DataControl
            Dim TotalMercancias As Double = data.RunSQLScalarQuery("select count(id) from tblcartaportemercancias where cartaporteid = " & _CartaPorteID)
            Dim PesoBrutoTotal As Double = data.RunSQLScalarQuery("select sum(PesoEnKg) from tblCartaPorteMercancias  where cartaporteid =" & _CartaPorteID)
            reporte.ReportParameters("txtTotalMercancias").Value = TotalMercancias
            reporte.ReportParameters("txtPesoBrutoTotal").Value = PesoBrutoTotal & ""

            reporte.ReportParameters("txtUnidadPeso").Value = cmbUnidadPeso.SelectedItem.Text
            reporte.ReportParameters("txtPesoNetoTotal").Value = txtPesoNetoTotal.Text
            Return reporte

            Exit Function
        End If
        '
        '
        Select Case tipoid
            Case 3, 6, 7      ' honorarios y arrendamiento
                Dim reporte As New Formatos.formato_cfdi_honorarios33iu
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 3
                        reporte.ReportParameters("txtDocumento").Value = "Arrendamiento No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO"
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Honorarios No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO"
                    Case 7
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO"
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "LugarExpedicion", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtPACCertifico").Value = "RFC Prov. Certif.: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                tipoComprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")
                reporte.ReportParameters("txtClienteRFC").Value = "Receptor RFC: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                '
                If tipoComprobante.ToString <> "" Then
                    Dim ObjData As New DataControl
                    tipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & tipoComprobante.ToString & "'")
                    ObjData = Nothing
                End If
                '
                reporte.ReportParameters("txtTipoComprobante").Value = tipoComprobante.ToString
                ''Faltan Datos ------
                reporte.ReportParameters("txtMoneda").Value = TipoMoneda.ToString

                If TipoMoneda = "USD" Then
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoCambio", "cfdi:Comprobante")
                Else
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & ""
                End If

                reporte.ReportParameters("txtCuentaPredial").Value = "Cuenta Predial: " & CuentaPredial.ToString

                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones

                ImpuestoTrasladado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
                Try
                    ImpuestoRetencion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosRetenidos", "cfdi:Impuestos")
                Catch ex As Exception
                    ImpuestoRetencion = 0
                End Try
                reporte.ReportParameters("txtTotalImpuestoTrasladado").Value = FormatCurrency(ImpuestoTrasladado, 2).ToString
                reporte.ReportParameters("txtTotalImpuestoRetencion").Value = FormatCurrency(ImpuestoRetencion, 2).ToString

                If tipocontribuyenteid = 1 Then
                    reporte.ReportParameters("txtImporte").Value = FormatNumber(importe, 2).ToString
                    reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                    reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtRetIva").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatNumber(importe + iva, 2).ToString
                    '
                    '   Ajusta cantidad con texto
                    '
                    total = FormatNumber((importe + iva), 2)
                    largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                    CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    '
                Else
                    If tipoid = 7 Then
                        reporte.ReportParameters("txtImporte").Value = FormatNumber(importe, 2).ToString
                        reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString
                        reporte.ReportParameters("txtRetIva").Value = FormatNumber((iva * 0.1), 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - ((iva * 0.1)), 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva) - ((iva * 0.1)), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '

                    Else
                        Dim TmpIva As Decimal = 0
                        reporte.ReportParameters("txtImporte").Value = FormatNumber(importe, 2).ToString
                        reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatNumber(importe * 0.1, 2).ToString
                        TmpIva = FormatNumber((iva / 3) * 2, 2).ToString
                        If TmpIva >= 2000 Then
                            TmpIva = importe * 0.106667
                            reporte.ReportParameters("txtRetIva").Value = FormatNumber(TmpIva, 2).ToString
                            reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                            total = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                        Else
                            reporte.ReportParameters("txtRetIva").Value = FormatNumber((iva / 3) * 2, 2).ToString
                            reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
                            total = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
                        End If

                        '
                        '   Ajusta cantidad con texto
                        '
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                    End If

                End If



                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString

                Return reporte
            Case 5     ' carta porte
                Dim reporte As New Formatos.formato_cfdi_carta_aporte33iu
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 5
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO"
                    Case Else
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "LugarExpedicion", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtPACCertifico").Value = "RFC Prov. Certif.: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                tipoComprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")
                reporte.ReportParameters("txtClienteRFC").Value = "Receptor RFC: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                '
                If tipoComprobante.ToString <> "" Then
                    Dim ObjData As New DataControl
                    tipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & tipoComprobante.ToString & "'")
                    ObjData = Nothing
                End If
                '
                reporte.ReportParameters("txtTipoComprobante").Value = tipoComprobante.ToString
                reporte.ReportParameters("txtMoneda").Value = TipoMoneda.ToString
                If TipoMoneda = "USD" Then
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoCambio", "cfdi:Comprobante")
                Else
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & ""
                End If
                reporte.ReportParameters("txtCuentaPredial").Value = "Cuenta Predial: " & ""

                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                reporte.ReportParameters("txtImporte").Value = FormatNumber(importesinmaniobra, 2).ToString
                reporte.ReportParameters("txtManiobra").Value = FormatNumber(importemaniobra, 2).ToString
                reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe, 2).ToString
                importetasacero = 0
                reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString

                reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                'IEPS
                reporte.ReportParameters("txtDescuento").Value = FormatNumber(totaldescuento, 2).ToString
                '
                ImpuestoTrasladado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
                Try
                    ImpuestoRetencion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosRetenidos", "cfdi:Impuestos")
                Catch ex As Exception
                    ImpuestoRetencion = 0
                End Try
                reporte.ReportParameters("txtTotalImpuestoTrasladado").Value = FormatCurrency(ImpuestoTrasladado, 2).ToString
                reporte.ReportParameters("txtTotalImpuestoRetencion").Value = FormatCurrency(ImpuestoRetencion, 2).ToString

                reporte.ReportParameters("txtTotal").Value = FormatNumber(total, 2).ToString
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                If tipoid = 5 Then
                    retencion = FormatNumber((importesinmaniobra * 0.04), 2)
                    reporte.ReportParameters("txtRetencion").Value = FormatNumber(retencion, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatNumber(total - retencion, 2).ToString
                    largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                    If divisaid = 1 Then
                        CantidadTexto = "(  " + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    Else
                        CantidadTexto = "(  " + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
                    End If
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                End If
                '
                '
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI
                '
                Return reporte

            Case Else
                Dim reporte As New Formatos.formato_cfdi_33iu
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 1, 4, 7
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                    Case 2, 8
                        reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No.    " & serie.ToString & folio.ToString
                    Case 5
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO     EFECTOS FISCALES AL PAGO"
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Honorarios No.    " & serie.ToString & folio.ToString
                    Case Else
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "LugarExpedicion", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtPACCertifico").Value = "RFC Prov. Certif.: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                tipoComprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")
                reporte.ReportParameters("txtClienteRFC").Value = "Receptor RFC: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                '
                If tipoComprobante.ToString <> "" Then

                    If tipoComprobante.ToString = "E" Then
                        tiporelacion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoRelacion", "cfdi:CfdiRelacionados")
                        reporte.ReportParameters("txtTipoRelacion").Value = "Tipo de Relacion: " & tiporelacion.ToString
                        reporte.ReportParameters("txtUUIDNC").Value = "UUID:" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "cfdi:CfdiRelacionado")
                    End If

                    Dim ObjData As New DataControl
                    tipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & tipoComprobante.ToString & "'")
                    ObjData = Nothing
                End If
                '
                reporte.ReportParameters("txtTipoComprobante").Value = tipoComprobante.ToString
                reporte.ReportParameters("txtMoneda").Value = TipoMoneda.ToString
                If TipoMoneda = "USD" Then
                    tipocambio = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoCambio", "cfdi:Comprobante")
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & FormatNumber(tipocambio, 2).ToString
                Else
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & ""
                End If
                reporte.ReportParameters("txtCuentaPredial").Value = "Cuenta Predial: " & ""

                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones

                If tipoComprobante = "E Egreso" Then
                    Dim uuidnc As String = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "cfdi:CfdiRelacionado")

                    Dim ObjNotaCredito As New DataControl
                    tipoid = ObjNotaCredito.RunSQLScalarQuery("select top 1 isnull(serieid,0) from tblCFD where isnull(timbrado,0)=1 and isnull(estatus,0)=1 and uuid='" & txtFolioFiscal.Text & "'")
                    ObjNotaCredito = Nothing
                Else
                    tipoid = 0
                End If

                'Retencion IVA,ISR
                If tipoid = 5 Then
                    retencion = FormatNumber((importe * 0.04), 2)
                    reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                    reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                    reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(retencion, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(total - retencion, 2).ToString
                    total = FormatNumber(total - retencion)
                    largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                    If divisaid = 1 Then
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    Else
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD. )"
                    End If
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString

                    reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                    'IEPS
                    reporte.ReportParameters("txtIEPS").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtDescuento").Value = FormatNumber(0, 2).ToString

                ElseIf tipoid = 3 Or tipoid = 6 Then
                    reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                    'IEPS
                    reporte.ReportParameters("txtIEPS").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtDescuento").Value = FormatNumber(0, 2).ToString

                    If tipocontribuyenteid = 1 Then
                        reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString
                        reporte.ReportParameters("txtRetIVA").Value = FormatNumber(0, 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatNumber(importe + iva, 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                    Else
                        If tipoid = 7 Then
                            reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                            reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                            reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString
                            reporte.ReportParameters("txtRetIVA").Value = FormatNumber((iva * 0.1), 2).ToString
                            reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - ((iva * 0.1)), 2).ToString
                            '
                            '   Ajusta cantidad con texto
                            '
                            total = FormatNumber((importe + iva) - ((iva * 0.1)), 2)
                            largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                            decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                            CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                            '
                            reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                        Else
                            Dim TmpIva As Decimal = 0
                            reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                            reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                            reporte.ReportParameters("txtRetISR").Value = FormatNumber(importe * 0.1, 2).ToString
                            TmpIva = FormatNumber((iva / 3) * 2, 2).ToString
                            If TmpIva >= 2000 Then
                                TmpIva = importe * 0.106667
                                reporte.ReportParameters("txtRetIVA").Value = FormatNumber(TmpIva, 2).ToString
                                reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                                total = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                            Else
                                reporte.ReportParameters("txtRetIVA").Value = FormatNumber((iva / 3) * 2, 2).ToString
                                reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
                                total = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                            End If

                            '
                            '   Ajusta cantidad con texto
                            '
                            largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                            decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                            CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                            '
                            reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                        End If
                    End If
                Else
                    'Calculo Normal...

                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                    '
                    reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe, 2).ToString
                    importetasacero = 0
                    reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString

                    reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                    reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                    'IEPS
                    reporte.ReportParameters("txtIEPS").Value = FormatNumber(ieps_total, 2).ToString
                    reporte.ReportParameters("txtDescuento").Value = FormatNumber(totaldescuento, 2).ToString

                    reporte.ReportParameters("txtTotal").Value = FormatNumber(total, 2).ToString


                    reporte.ReportParameters("txtRetIVA").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString

                End If

                ImpuestoTrasladado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
                Try
                    ImpuestoRetencion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosRetenidos", "cfdi:Impuestos")
                Catch ex As Exception
                    ImpuestoRetencion = 0
                End Try
                reporte.ReportParameters("txtTotalImpuestoTrasladado").Value = FormatCurrency(ImpuestoTrasladado, 2).ToString
                reporte.ReportParameters("txtTotalImpuestoRetencion").Value = FormatCurrency(ImpuestoRetencion, 2).ToString
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                '
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI
                '
                Return reporte
        End Select

    End Function

    Private Function GeneraPDF_PreImpreso(ByVal cfdid As Long) As Telerik.Reporting.Report
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Dim numeroaprobacion As String = ""
        Dim anoAprobacion As String = ""
        Dim fechaHora As String = ""
        Dim noCertificado As String = ""
        Dim razonsocial As String = ""
        Dim callenum As String = ""
        Dim colonia As String = ""
        Dim ciudad As String = ""
        Dim rfc As String = ""
        Dim em_razonsocial As String = ""
        Dim em_callenum As String = ""
        Dim em_colonia As String = ""
        Dim em_ciudad As String = ""
        Dim em_rfc As String = ""
        Dim em_regimen As String = ""
        Dim importe As Decimal = 0
        Dim importetasacero As Decimal = 0
        Dim iva As Decimal = 0
        Dim total As Decimal = 0
        Dim CantidadTexto As String = ""
        Dim condiciones As String = ""
        Dim enviara As String = ""
        Dim instrucciones As String = ""
        Dim pedimento As String = ""
        Dim retencion As Decimal = 0
        Dim tipoid As Integer = 0
        Dim divisaid As Integer = 1
        Dim expedicionLinea1 As String = ""
        Dim expedicionLinea2 As String = ""
        Dim expedicionLinea3 As String = ""
        Dim porcentaje As Decimal = 0
        Dim plantillaid As Integer = 1
        Dim tipopago As String = ""
        Dim formapago As String = ""
        Dim numctapago As String = ""
        Dim serie As String = ""
        Dim folio As Integer = 0
        Dim usoCFDI As String = ""
        Dim tipoComprobante As String = ""
        Dim LugarExpedicion As String = ""
        Dim CuentaPredial As String = ""
        Dim tipocambio As Decimal = 0
        Dim tiporelacion As String = ""
        Dim TipoMoneda As String = ""

        Dim ImpuestoTrasladado As Decimal = 0
        Dim ImpuestoRetencion As Decimal = 0
        Dim importesinmaniobra As Decimal = 0
        Dim importemaniobra As Decimal = 0

        Dim ds As DataSet = New DataSet

        Try
            Dim cmd As New SqlCommand("EXEC pCFD @cmd=18, @cfdid='" & cfdid.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                serie = rs("serie")
                folio = rs("folio")
                tipoid = rs("tipoid")
                em_razonsocial = rs("em_razonsocial")
                em_callenum = rs("em_callenum")
                em_colonia = rs("em_colonia")
                em_ciudad = rs("em_ciudad")
                em_rfc = rs("em_rfc")
                em_regimen = rs("regimen")
                razonsocial = rs("razonsocial")
                callenum = rs("callenum")
                colonia = rs("colonia")
                ciudad = rs("ciudad")
                rfc = rs("rfc")
                importe = rs("importe")
                iva = rs("iva")
                total = rs("total")
                ieps_total = rs("ieps_total")
                divisaid = rs("divisaid")
                fechaHora = rs("fecha_factura").ToString
                condiciones = "Condiciones: " & rs("condiciones").ToString
                enviara = rs("enviara").ToString
                instrucciones = rs("instrucciones")
                If rs("aduana") = "" Or rs("numero_pedimento") = "" Then
                    pedimento = ""
                Else
                    pedimento = "Aduana: " & rs("aduana") & vbCrLf & "Fecha: " & rs("fecha_pedimento").ToString & vbCrLf & "Número: " & rs("numero_pedimento").ToString
                End If
                expedicionLinea1 = rs("expedicionLinea1")
                expedicionLinea2 = rs("expedicionLinea2")
                expedicionLinea3 = rs("expedicionLinea3")
                CuentaPredial = rs("cuentapredial")
                porcentaje = rs("porcentaje")
                plantillaid = rs("plantillaid")
                tipocontribuyenteid = rs("tipocontribuyenteid")
                tipopago = rs("tipopago")
                formapago = rs("formapago")
                numctapago = rs("numctapago")
                usoCFDI = rs("usocfdi")
                importesinmaniobra = rs("importesinmaniobra")
                importemaniobra = rs("importemaniobra")
            End If
            rs.Close()
            '
        Catch ex As Exception
            '
            Response.Write(ex.ToString)
        Finally

            conn.Close()
            conn.Dispose()
            conn = Nothing
        End Try



        Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)



        If System.Configuration.ConfigurationManager.AppSettings("divisas") = 1 Then
            If divisaid = 1 Then
                TipoMoneda = "MXN"
                CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
            Else
                TipoMoneda = "USD"
                CantidadTexto = "(  " + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD. )"
            End If
        Else
            TipoMoneda = "MXN"
            CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
        End If

        If ckCartaPorte.Checked = True Then
            Dim reporte As New FormatosPDF.formato_cfdi_complemento_carta_porte_preimpreso
            reporte.ReportParameters("doc").Value = "I"
            reporte.ReportParameters("plantillaId").Value = plantillaid
            reporte.ReportParameters("cfdiId").Value = cfdid
            Select Case tipoid
                Case 1, 4, 7
                    reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                Case 2, 8
                    reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No.    " & serie.ToString & folio.ToString
                Case 5
                    reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                    reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO     EFECTOS FISCALES AL PAGO"
                Case 6
                    reporte.ReportParameters("txtDocumento").Value = "Honorarios No.    " & serie.ToString & folio.ToString
                Case Else
                    reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
            End Select
            reporte.ReportParameters("txtCondicionesPago").Value = condiciones
            reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
            reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
            reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
            LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "LugarExpedicion", "cfdi:Comprobante")
            reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
            reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
            reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
            reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
            reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
            reporte.ReportParameters("txtClienteCalleNum").Value = callenum
            reporte.ReportParameters("txtClienteColonia").Value = colonia
            reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
            reporte.ReportParameters("txtPACCertifico").Value = "RFC Prov. Certif.: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
            tipoComprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")
            reporte.ReportParameters("txtClienteRFC").Value = "Receptor RFC: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
            '
            If tipoComprobante.ToString <> "" Then

                'If tipoComprobante.ToString = "E" Then
                '    tiporelacion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoRelacion", "cfdi:CfdiRelacionados")
                '    reporte.ReportParameters("txtTipoRelacion").Value = "Tipo de Relacion: " & tiporelacion.ToString
                '    reporte.ReportParameters("txtUUIDNC").Value = "UUID:" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "cfdi:CfdiRelacionado")
                'End If

                Dim ObjData As New DataControl
                tipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & tipoComprobante.ToString & "'")
                ObjData = Nothing
            End If
            '
            reporte.ReportParameters("txtTipoComprobante").Value = tipoComprobante.ToString
            reporte.ReportParameters("txtMoneda").Value = TipoMoneda.ToString
            If TipoMoneda = "USD" Then
                reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoCambio", "cfdi:Comprobante")
            Else
                reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & ""
            End If
            reporte.ReportParameters("txtCuentaPredial").Value = "Cuenta Predial: " & ""

            reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
            reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
            '
            reporte.ReportParameters("txtInstrucciones").Value = instrucciones
            reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
            '
            reporte.ReportParameters("txtImporte").Value = FormatNumber(importesinmaniobra, 2).ToString
            reporte.ReportParameters("txtManiobra").Value = FormatNumber(importemaniobra, 2).ToString
            reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe, 2).ToString
            importetasacero = 0
            reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString

            reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
            reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
            'IEPS
            reporte.ReportParameters("txtDescuento").Value = FormatNumber(totaldescuento, 2).ToString
            '
            ImpuestoTrasladado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
            Try
                ImpuestoRetencion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosRetenidos", "cfdi:Impuestos")
            Catch ex As Exception
                ImpuestoRetencion = 0
            End Try
            reporte.ReportParameters("txtTotalImpuestoTrasladado").Value = FormatCurrency(ImpuestoTrasladado, 2).ToString
            reporte.ReportParameters("txtTotalImpuestoRetencion").Value = FormatCurrency(ImpuestoRetencion, 2).ToString

            reporte.ReportParameters("txtTotal").Value = FormatNumber(total, 2).ToString
            '
            reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
            reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
            reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
            If porcentaje > 0 Then
                reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
            End If
            '
            If tipoid = 5 Or tipoid = 10 Then
                retencion = FormatNumber((importesinmaniobra * 0.04), 2)
                reporte.ReportParameters("txtRetencion").Value = FormatNumber(retencion, 2).ToString
                reporte.ReportParameters("txtTotal").Value = FormatNumber(total - retencion, 2).ToString
                largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                If divisaid = 1 Then
                    CantidadTexto = "(  " + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                Else
                    CantidadTexto = "(  " + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
                End If
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
            End If
            '
            reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
            reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
            reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
            reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
            reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
            reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI
            '
            '
            '
            reporte.ReportParameters("cartaporteid").Value = _CartaPorteID
            '
            '
            '
            Dim Obj As New DataControl
            Dim dt As DataSet = Obj.FillDataSet("EXEC pAutotransportes @cmd=2, @id='" & cmbAutotransporte.SelectedValue & "'")
            For Each row As DataRow In dt.Tables(0).Rows

                reporte.ReportParameters("txtClaveSCT").Value = row("PermSCTId")
                reporte.ReportParameters("tctNumeroSCT").Value = row("NumPermisoSCT")
                reporte.ReportParameters("tctNombreSCT").Value = row("claveUnidadNom")
                reporte.ReportParameters("txtAseguradoraSCT").Value = row("NombreAseg")
                reporte.ReportParameters("txtPolizaSCT").Value = row("NumPolizaSeguro")

                Dim AseguradoraMedAmbiente As String
                AseguradoraMedAmbiente = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "AseguraMedAmbiente", "cartaporte20:Seguros")
                If AseguradoraMedAmbiente.Length < 1 Then
                    AseguradoraMedAmbiente = "NA"
                End If
                reporte.ReportParameters("txtAseguradoraMedAmbiente").Value = AseguradoraMedAmbiente

                Dim PolizaMedAmbiente As String
                PolizaMedAmbiente = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "PolizaMedAmbiente", "cartaporte20:Seguros")
                If PolizaMedAmbiente.Length < 1 Then
                    PolizaMedAmbiente = "NA"
                End If
                reporte.ReportParameters("txtPolizaMedAmbiente").Value = PolizaMedAmbiente

                reporte.ReportParameters("txtClaveAuto").Value = row("ConfigVehicularId")
                reporte.ReportParameters("txtDescAuto").Value = row("configVehicularNom")
                reporte.ReportParameters("txtPlacaAuto").Value = row("PlacaVM")
                reporte.ReportParameters("txtAnioAuto").Value = row("AnioModeloVM")

            Next
            '
            'Totales
            '
            '
            Dim Obj_des As New DataControl
            Dim TotalMercancias As Double = Obj_des.RunSQLScalarQuery("select sum(cantidad) from tblcartaportemercancias where cartaporteid = " & _CartaPorteID)
            Dim PesoBrutoTotal As Double = Obj_des.RunSQLScalarQuery("select sum(PesoEnKg) from tblCartaPorteMercancias  where cartaporteid =" & _CartaPorteID)
            reporte.ReportParameters("txtTotalMercancias").Value = TotalMercancias
            reporte.ReportParameters("txtPesoBrutoTotal").Value = PesoBrutoTotal

            reporte.ReportParameters("txtUnidadPeso").Value = cmbUnidadPeso.SelectedItem.Text
            reporte.ReportParameters("txtPesoNetoTotal").Value = txtPesoNetoTotal.Text

            Return reporte
            Exit Function
        End If
        '
        Select Case tipoid
            Case 3, 6, 7      ' honorarios y arrendamiento
                Dim reporte As New Formatos.formato_cbb_honorarios_preimpreso33ui
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 3
                        reporte.ReportParameters("txtDocumento").Value = "Arrendamiento No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO"
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Honorarios No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO"
                    Case 7
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO"
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "LugarExpedicion", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtPACCertifico").Value = "RFC Prov. Certif.: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                tipoComprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")
                reporte.ReportParameters("txtClienteRFC").Value = "Receptor RFC: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                '
                If tipoComprobante.ToString <> "" Then
                    Dim ObjData As New DataControl
                    tipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & tipoComprobante.ToString & "'")
                    ObjData = Nothing
                End If
                '
                reporte.ReportParameters("txtTipoComprobante").Value = tipoComprobante.ToString
                ''Faltan Datos ------
                reporte.ReportParameters("txtMoneda").Value = TipoMoneda.ToString

                If TipoMoneda = "USD" Then
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoCambio", "cfdi:Comprobante")
                Else
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & ""
                End If

                reporte.ReportParameters("txtCuentaPredial").Value = "Cuenta Predial: " & CuentaPredial.ToString

                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones

                ImpuestoTrasladado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
                Try
                    ImpuestoRetencion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosRetenidos", "cfdi:Impuestos")
                Catch ex As Exception
                    ImpuestoRetencion = 0
                End Try
                reporte.ReportParameters("txtTotalImpuestoTrasladado").Value = FormatCurrency(ImpuestoTrasladado, 2).ToString
                reporte.ReportParameters("txtTotalImpuestoRetencion").Value = FormatCurrency(ImpuestoRetencion, 2).ToString

                If tipocontribuyenteid = 1 Then
                    reporte.ReportParameters("txtImporte").Value = FormatNumber(importe, 2).ToString
                    reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                    reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtRetIva").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatNumber(importe + iva, 2).ToString
                    '
                    '   Ajusta cantidad con texto
                    '
                    total = FormatNumber((importe + iva), 2)
                    largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                    CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    '
                Else
                    If tipoid = 7 Then
                        reporte.ReportParameters("txtImporte").Value = FormatNumber(importe, 2).ToString
                        reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString
                        reporte.ReportParameters("txtRetIva").Value = FormatNumber((iva * 0.1), 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - ((iva * 0.1)), 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva) - ((iva * 0.1)), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '

                    Else
                        Dim TmpIva As Decimal = 0
                        reporte.ReportParameters("txtImporte").Value = FormatNumber(importe, 2).ToString
                        reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString
                        reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatNumber(importe * 0.1, 2).ToString
                        TmpIva = FormatNumber((iva / 3) * 2, 2).ToString
                        If TmpIva >= 2000 Then
                            TmpIva = importe * 0.106667
                            reporte.ReportParameters("txtRetIva").Value = FormatNumber(TmpIva, 2).ToString
                            reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                            total = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                        Else
                            reporte.ReportParameters("txtRetIva").Value = FormatNumber((iva / 3) * 2, 2).ToString
                            reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
                            total = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
                        End If

                        '
                        '   Ajusta cantidad con texto
                        '
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                    End If

                End If



                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString

                Return reporte
            Case 5     ' carta porte
                Dim reporte As New Formatos.formato_cbb_carta_aporte_preimpreso33ui
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 5
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO"
                    Case Else
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "LugarExpedicion", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtPACCertifico").Value = "RFC Prov. Certif.: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                tipoComprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")
                reporte.ReportParameters("txtClienteRFC").Value = "Receptor RFC: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                '
                If tipoComprobante.ToString <> "" Then
                    Dim ObjData As New DataControl
                    tipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & tipoComprobante.ToString & "'")
                    ObjData = Nothing
                End If
                '
                reporte.ReportParameters("txtTipoComprobante").Value = tipoComprobante.ToString
                reporte.ReportParameters("txtMoneda").Value = TipoMoneda.ToString
                If TipoMoneda = "USD" Then
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoCambio", "cfdi:Comprobante")
                Else
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & ""
                End If
                reporte.ReportParameters("txtCuentaPredial").Value = "Cuenta Predial: " & ""

                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones
                reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                '
                reporte.ReportParameters("txtImporte").Value = FormatNumber(importesinmaniobra, 2).ToString
                reporte.ReportParameters("txtManiobra").Value = FormatNumber(importemaniobra, 2).ToString
                reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe, 2).ToString
                importetasacero = 0
                reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString

                reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                'IEPS
                reporte.ReportParameters("txtDescuento").Value = FormatNumber(totaldescuento, 2).ToString
                '
                ImpuestoTrasladado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
                Try
                    ImpuestoRetencion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosRetenidos", "cfdi:Impuestos")
                Catch ex As Exception
                    ImpuestoRetencion = 0
                End Try
                reporte.ReportParameters("txtTotalImpuestoTrasladado").Value = FormatCurrency(ImpuestoTrasladado, 2).ToString
                reporte.ReportParameters("txtTotalImpuestoRetencion").Value = FormatCurrency(ImpuestoRetencion, 2).ToString

                reporte.ReportParameters("txtTotal").Value = FormatNumber(total, 2).ToString
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                If tipoid = 5 Then
                    retencion = FormatNumber((importesinmaniobra * 0.04), 2)
                    reporte.ReportParameters("txtRetencion").Value = FormatNumber(retencion, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatNumber(total - retencion, 2).ToString
                    largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                    If divisaid = 1 Then
                        CantidadTexto = "(  " + Num2Text((total - retencion - decimales)) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    Else
                        CantidadTexto = "(  " + Num2Text((total - retencion - decimales)) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD )"
                    End If
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                End If
                '
                '
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI
                '
                Return reporte

            Case Else
                Dim reporte As New Formatos.formato_cbb_preimpreso33ui
                reporte.ReportParameters("plantillaId").Value = plantillaid
                reporte.ReportParameters("cfdiId").Value = cfdid
                Select Case tipoid
                    Case 1, 4, 7
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                    Case 2, 8
                        reporte.ReportParameters("txtDocumento").Value = "Nota de Crédito No.    " & serie.ToString & folio.ToString
                    Case 5
                        reporte.ReportParameters("txtDocumento").Value = "Carta Porte No.    " & serie.ToString & folio.ToString
                        reporte.ReportParameters("txtLeyenda").Value = "IMPUESTO RETENIDO DE CONFORMIDAD CON LA LEY DEL IMPUESTO AL VALOR AGREGADO     EFECTOS FISCALES AL PAGO"
                    Case 6
                        reporte.ReportParameters("txtDocumento").Value = "Honorarios No.    " & serie.ToString & folio.ToString
                    Case Else
                        reporte.ReportParameters("txtDocumento").Value = "Factura No.    " & serie.ToString & folio.ToString
                End Select
                reporte.ReportParameters("txtCondicionesPago").Value = condiciones
                reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/portalcfd/cbb/" & serie.ToString & folio.ToString & ".png")
                reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/portalcfd/logos/" & Session("logo_formato"))
                reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Fecha", "cfdi:Comprobante")
                LugarExpedicion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "LugarExpedicion", "cfdi:Comprobante")
                reporte.ReportParameters("txtFechaCertificacion").Value = LugarExpedicion & "-" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "FechaTimbrado", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificado", "cfdi:Comprobante")
                reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "NoCertificadoSAT", "tfd:TimbreFiscalDigital")
                reporte.ReportParameters("txtClienteRazonSocial").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Nombre", "cfdi:Receptor")
                reporte.ReportParameters("txtClienteCalleNum").Value = callenum
                reporte.ReportParameters("txtClienteColonia").Value = colonia
                reporte.ReportParameters("txtClienteCiudadEstado").Value = ciudad
                reporte.ReportParameters("txtPACCertifico").Value = "RFC Prov. Certif.: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "RfcProvCertif", "tfd:TimbreFiscalDigital")
                tipoComprobante = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoDeComprobante", "cfdi:Comprobante")
                reporte.ReportParameters("txtClienteRFC").Value = "Receptor RFC: " & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Rfc", "cfdi:Receptor")
                '
                If tipoComprobante.ToString <> "" Then

                    If tipoComprobante.ToString = "E" Then
                        tiporelacion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoRelacion", "cfdi:CfdiRelacionados")
                        reporte.ReportParameters("txtTipoRelacion").Value = "Tipo de Relacion: " & tiporelacion.ToString
                        reporte.ReportParameters("txtUUIDNC").Value = "UUID:" & GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "cfdi:CfdiRelacionado")
                    End If

                    Dim ObjData As New DataControl
                    tipoComprobante = ObjData.RunSQLScalarQueryString("select top 1 codigo + ' ' + isnull(descripcion,'') from tblTipoDeComprobante where codigo='" & tipoComprobante.ToString & "'")
                    ObjData = Nothing
                End If
                '
                reporte.ReportParameters("txtTipoComprobante").Value = tipoComprobante.ToString
                reporte.ReportParameters("txtMoneda").Value = TipoMoneda.ToString
                If TipoMoneda = "USD" Then
                    tipocambio = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TipoCambio", "cfdi:Comprobante")
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & FormatNumber(tipocambio, 2).ToString
                Else
                    reporte.ReportParameters("txtTipoCambio").Value = "Tipo de Cambio: " & ""
                End If
                reporte.ReportParameters("txtCuentaPredial").Value = "Cuenta Predial: " & ""

                reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "Sello", "cfdi:Comprobante")
                reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "SelloSAT", "tfd:TimbreFiscalDigital")
                '
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones

                If tipoComprobante = "E Egreso" Then
                    Dim uuidnc As String = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "UUID", "cfdi:CfdiRelacionado")

                    Dim ObjNotaCredito As New DataControl
                    tipoid = ObjNotaCredito.RunSQLScalarQuery("select top 1 isnull(serieid,0) from tblCFD where isnull(timbrado,0)=1 and isnull(estatus,0)=1 and uuid='" & txtFolioFiscal.Text & "'")
                    ObjNotaCredito = Nothing
                Else
                    tipoid = 0
                End If

                'Retencion IVA,ISR
                If tipoid = 5 Then
                    retencion = FormatNumber((importe * 0.04), 2)
                    reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                    reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                    reporte.ReportParameters("txtRetIVA").Value = FormatCurrency(retencion, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatCurrency(0, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(total - retencion, 2).ToString
                    total = FormatNumber(total - retencion)
                    largo = Len(CStr(Format(CDbl(total - retencion), "#,###.00")))
                    decimales = Mid(CStr(Format(CDbl(total - retencion), "#,###.00")), largo - 2)
                    If divisaid = 1 Then
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                    Else
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " dólares " & Mid(decimales, Len(decimales) - 1) & "/100 USD. )"
                    End If
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString

                    reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                    'IEPS
                    reporte.ReportParameters("txtIEPS").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtDescuento").Value = FormatNumber(0, 2).ToString

                ElseIf tipoid = 3 Or tipoid = 6 Then
                    reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                    'IEPS
                    reporte.ReportParameters("txtIEPS").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtDescuento").Value = FormatNumber(0, 2).ToString

                    If tipocontribuyenteid = 1 Then
                        reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                        reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                        reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString
                        reporte.ReportParameters("txtRetIVA").Value = FormatNumber(0, 2).ToString
                        reporte.ReportParameters("txtTotal").Value = FormatNumber(importe + iva, 2).ToString
                        '
                        '   Ajusta cantidad con texto
                        '
                        total = FormatNumber((importe + iva), 2)
                        largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                        decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                        CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                        '
                        reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                    Else
                        If tipoid = 7 Then
                            reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                            reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                            reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString
                            reporte.ReportParameters("txtRetIVA").Value = FormatNumber((iva * 0.1), 2).ToString
                            reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - ((iva * 0.1)), 2).ToString
                            '
                            '   Ajusta cantidad con texto
                            '
                            total = FormatNumber((importe + iva) - ((iva * 0.1)), 2)
                            largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                            decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                            CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                            '
                            reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                        Else
                            Dim TmpIva As Decimal = 0
                            reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                            reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe + iva, 2).ToString
                            reporte.ReportParameters("txtRetISR").Value = FormatNumber(importe * 0.1, 2).ToString
                            TmpIva = FormatNumber((iva / 3) * 2, 2).ToString
                            If TmpIva >= 2000 Then
                                TmpIva = importe * 0.106667
                                reporte.ReportParameters("txtRetIVA").Value = FormatNumber(TmpIva, 2).ToString
                                reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                                total = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                            Else
                                reporte.ReportParameters("txtRetIVA").Value = FormatNumber((iva / 3) * 2, 2).ToString
                                reporte.ReportParameters("txtTotal").Value = FormatNumber((importe + iva) - (importe * 0.1) - ((iva / 3) * 2), 2).ToString
                                total = FormatNumber((importe + iva) - (importe * 0.1) - (TmpIva), 2).ToString
                            End If

                            '
                            '   Ajusta cantidad con texto
                            '
                            largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                            decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)
                            CantidadTexto = "(  " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"
                            '
                            reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString
                        End If
                    End If
                Else
                    'Calculo Normal...

                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto
                    '
                    reporte.ReportParameters("txtSubtotal").Value = FormatNumber(importe, 2).ToString
                    importetasacero = 0
                    reporte.ReportParameters("txtTasaCero").Value = FormatNumber(importetasacero, 2).ToString

                    reporte.ReportParameters("txtEtiquetaIVA").Value = "IVA 16%"
                    reporte.ReportParameters("txtIVA").Value = FormatNumber(iva, 2).ToString
                    'IEPS
                    reporte.ReportParameters("txtIEPS").Value = FormatNumber(ieps_total, 2).ToString
                    reporte.ReportParameters("txtDescuento").Value = FormatNumber(totaldescuento, 2).ToString

                    reporte.ReportParameters("txtTotal").Value = FormatNumber(total, 2).ToString


                    reporte.ReportParameters("txtRetIVA").Value = FormatNumber(0, 2).ToString
                    reporte.ReportParameters("txtRetISR").Value = FormatNumber(0, 2).ToString

                End If

                ImpuestoTrasladado = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosTrasladados", "cfdi:Impuestos")
                Try
                    ImpuestoRetencion = GetXmlAttribute(Server.MapPath("~/portalCFD/cfd_storage") & "\iu_" & serie.ToString & folio.ToString & "_timbrado.xml", "TotalImpuestosRetenidos", "cfdi:Impuestos")
                Catch ex As Exception
                    ImpuestoRetencion = 0
                End Try
                reporte.ReportParameters("txtTotalImpuestoTrasladado").Value = FormatCurrency(ImpuestoTrasladado, 2).ToString
                reporte.ReportParameters("txtTotalImpuestoRetencion").Value = FormatCurrency(ImpuestoRetencion, 2).ToString
                '
                reporte.ReportParameters("txtCadenaOriginal").Value = cadOrigComp
                reporte.ReportParameters("txtEmisorRazonSocial").Value = em_razonsocial
                reporte.ReportParameters("txtLugarExpedicion").Value = expedicionLinea1 & vbCrLf & expedicionLinea2 & vbCrLf & expedicionLinea3
                If porcentaje > 0 Then
                    reporte.ReportParameters("txtInteres").Value = porcentaje.ToString
                End If
                '
                '
                reporte.ReportParameters("txtRegimen").Value = em_regimen.ToString
                reporte.ReportParameters("txtFormaPago").Value = formapago.ToString
                reporte.ReportParameters("txtMetodoPago").Value = tipopago.ToString
                reporte.ReportParameters("txtNumCtaPago").Value = "Núm. cuenta: " & numctapago.ToString
                reporte.ReportParameters("txtInstrucciones").Value = instrucciones.ToString
                reporte.ReportParameters("txtUsoCFDI").Value = usoCFDI
                '
                Return reporte
        End Select

    End Function

#End Region

#Region "Manejo de PDF"

    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub

#End Region

#Region "Telerik Autocomplete"


#End Region

    Protected Sub chkAduana_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAduana.CheckedChanged
        panelInformacionAduanera.Visible = chkAduana.Checked
        valNombreAduana.Enabled = chkAduana.Checked
        valFechaPedimento.Enabled = chkAduana.Checked
        valNumeroPedimento.Enabled = chkAduana.Checked
    End Sub

    Protected Sub serieid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles serieid.SelectedIndexChanged
        If serieid.SelectedValue > 0 Then
            serieid.Enabled = False

            If serieid.SelectedValue = 4 Or serieid.SelectedValue = 8 Then
                panelDivisas.Visible = True
                valTipoCambio.Enabled = True
            End If

            If serieid.SelectedValue = 2 Or serieid.SelectedValue = 8 Then
                Label6.Visible = True
                tiporelacionid.Visible = True
                ValTipoRelecion.Enabled = True

                txtFolioFiscal.Visible = True
                ValFolioFiscal.Enabled = True
                Label7.Visible = True

                Dim ObjCat As New DataControl
                ObjCat.Catalogo(metodopagoid, "select codigo,descripcion from tblc_MetodoPago order by codigo", "PUE")
                ObjCat = Nothing

            End If
        End If
    End Sub

    Protected Sub btnCancelSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelSearch.Click
        gridResults.Visible = False
        itemsList.Visible = True
        txtSearchItem.Text = ""
        txtSearchItem.Focus()
        btnCancelSearch.Visible = False
    End Sub

    Protected Sub gridResults_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gridResults.ItemCommand
        Select Case e.CommandName
            Case "cmdAdd"
                If Session("CFD") = 0 Then
                    GetCFD()
                End If
                CargaTotales()
                InsertItem(e.CommandArgument, e.Item)
                DisplayItems()
                If ValidarConcepto = True Then
                    GetAplicarRetencion()
                End If
                Call CargaTotales()
                panelResume.Visible = True
                gridResults.Visible = False
                itemsList.Visible = True
                txtSearchItem.Text = ""
                txtSearchItem.Focus()
                btnCancelSearch.Visible = False
        End Select
    End Sub
    Private Sub GetAplicarRetencion()
        Dim ObjData As New DataControl
        AplicarRetencion = ObjData.RunSQLScalarQueryString("SELECT top 1 isnull(retencion,0) FROM tblCFD_Partidas WHERE cfdid='" & Session("CFD").ToString & "' order by id desc")
        ObjData = Nothing
    End Sub
    Protected Sub txtSearchItem_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearchItem.TextChanged
        gridResults.Visible = True
        itemsList.Visible = False
        Dim objdata As New DataControl
        gridResults.DataSource = objdata.FillDataSet("exec pCFD @cmd=30, @txtSearch='" & txtSearchItem.Text & "', @clienteid='" & cmbClient.SelectedValue & "'")
        gridResults.DataBind()

        If serieid.SelectedValue <> 3 Then
            gridResults.MasterTableView.GetColumn("predial").Visible = False
        End If

        If serieid.SelectedValue <> 5 Then
            gridResults.MasterTableView.GetColumn("maniobra").Visible = False
        End If

        objdata = Nothing
        txtSearchItem.Text = ""
        txtSearchItem.Focus()
        btnCancelSearch.Visible = True
    End Sub

    Protected Sub gridResults_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles gridResults.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim txtQuantity As RadNumericTextBox = DirectCast(e.Item.FindControl("txtQuantity"), RadNumericTextBox)
                Dim txtUnitaryPrice As RadNumericTextBox = DirectCast(e.Item.FindControl("txtUnitaryPrice"), RadNumericTextBox)

                If serieid.SelectedValue = 3 Then
                    Dim txtCuenta As System.Web.UI.WebControls.TextBox = DirectCast(e.Item.FindControl("txtCuenta"), System.Web.UI.WebControls.TextBox)
                    txtCuenta.Text = e.Item.DataItem("cuentapredial")
                End If

                txtQuantity.Text = "1"
                txtUnitaryPrice.Text = e.Item.DataItem("precio")
        End Select
    End Sub

    Protected Sub cmbClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbClient.SelectedIndexChanged
        Call CargaCliente(cmbClient.SelectedValue)
        Call ClearItems()
    End Sub

    Protected Sub btnCancelInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelInvoice.Click
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pCFD @cmd=31, @cfdid='" & Session("CFD").ToString & "'")
        ObjData = Nothing
        '
        Session("CFD") = 0
        '
        Response.Redirect("~/portalcfd/cfd.aspx")
        '
        ''
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Response.Redirect("~/portalcfd/cfd.aspx")
    End Sub

#Region "Eventos de los Objetos Carta Porte"
    Protected Sub btnAddMercancia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddMercancia.Click
        If FileMercancia.HasFile Then
            FileMercancia.SaveAs(Server.MapPath("~\portalcfd\files\cartaporte\") & FileMercancia.FileName)
            If File.Exists(Server.MapPath("~\portalcfd\files\cartaporte\") & FileMercancia.FileName) Then
                Dim List As List(Of String()) = LoadFileToArrays(Server.MapPath("~\portalcfd\files\cartaporte\") & FileMercancia.FileName)
                For Each item In List
                    Try
                        Dim BienesTransp As Integer
                        BienesTransp = item(0)

                        Dim Cantidad As String = item(1)
                        Dim ClaveUnidad As String = item(2)
                        Dim PesoEnKg As String = item(3)
                        Dim MaterialPeligroso As String = item(4)
                        Dim CveMaterialPeligroso As String = item(5)
                        Dim Embalaje As String = item(6)
                        Dim Descripcion As String = item(7)
                        SaveMercancia(item(0), "", Descripcion, Cantidad, ClaveUnidad, "", PesoEnKg, "", "MNX", MaterialPeligroso, CveMaterialPeligroso, Embalaje, "")
                    Catch ex As Exception

                    End Try
                Next
            Else

            End If
        Else
            Call SaveMercancia(cmbBienesTransp.SelectedValue, cmbBienesTransp.Text, txtDescripcion.Text.ToString, txtCantidad.Text.ToString, cmbClaveUnidad.SelectedValue, cmbClaveUnidad.Text, txtPesoEnKg.Text.ToString, txtValorMercancia.Text.ToString, cmbMoneda.SelectedValue, cmbMaterialPeligroso.SelectedValue, cmbCveMaterialPeligroso.SelectedValue, cmbEmbalaje.SelectedValue, cmbDescripEmbalaje.Text)
        End If
        Call tblMercancias_NeedData("on")
        Call ResetInputAddMercancia()
        Call verificaMedAmbiente()
    End Sub
    Private Function LoadFileToArrays(ByVal filePath As String) As List(Of String())
        Dim records As New List(Of String())()
        Dim myEncoding As Encoding = System.Text.Encoding.GetEncoding(1252) ' Windows-1252 

        For Each line As String In File.ReadAllLines(filePath, myEncoding)
            Dim values As New List(Of String)()
            For Each field As String In line.Split(New String() {ControlChars.Tab}, StringSplitOptions.None)
                values.Add(field)
            Next
            records.Add(values.ToArray())
        Next
        Return records
    End Function
    Private Sub SaveMercancia(ByVal BienesTransp As String, ByVal BienesTransp_D As String, ByVal Descripcion As String, ByVal Cantidad As String, ByVal ClaveUnidad As String, ByVal ClaveUnidad_D As String, ByVal PesoEnKg As String, ByVal ValorMercancia As String, ByVal Moneda As String, ByVal MaterialPeligroso As String, ByVal CveMaterialPeligroso As String, ByVal Embalaje As String, ByVal DescripEmbalaje As String)
        If Session("CFD") = 0 Then
            GetCFD()
        End If
        
        Dim ObjData As New DataControl
        ObjData.RunSQLQuery("exec pCartaPorteMercancias @cmd=1, @cartaporteid='" & _CartaPorteID & _
                            "', @BienesTransp='" & BienesTransp & _
                            "', @BienesTransp_D='" & BienesTransp_D & _
                            "', @Descripcion='" & Descripcion & _
                            "', @Cantidad='" & Cantidad & _
                            "', @ClaveUnidad='" & ClaveUnidad & _
                            "', @PesoEnKg='" & PesoEnKg & _
                            "', @ValorMercancia='" & ValorMercancia & _
                            "', @Moneda='" & Moneda & _
                            "', @MaterialPeligroso='" & MaterialPeligroso & _
                            "', @CveMaterialPeligroso='" & CveMaterialPeligroso & _
                            "', @DescripEmbalaje='" & DescripEmbalaje & _
                            "', @Embalaje='" & Embalaje & "' ")

    End Sub
    Private Sub SaveMercanciaDb()

    End Sub
    Protected Sub tblMercancias_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles tblMercancias.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                DeleteMercancia(e.CommandArgument)
        End Select
    End Sub
    Protected Sub tblMercancias_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles tblMercancias.NeedDataSource
        tblMercancias_NeedData("off")
    End Sub
    Protected Sub txtCodigoPostal_or_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigoPostal_or.TextChanged
        DomicilioOr_cmbCodigos("0", "0", "0")
    End Sub

    ''
    Protected Sub ckCartaPorte_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckCartaPorte.CheckedChanged
        If ckCartaPorte.Checked = True Then
            'ClearItems()
            panelCartaPorte.Visible = True
            '
            If GetEstateCartaPorte() = False Then
                _CartaPorteID = SaveCartaPorte()
            End If
            verificaCartaPorte()
        Else
            'ClearItems()
            panelCartaPorte.Visible = False
        End If

        '
    End Sub
    Protected Sub cmbBienesTransp_ItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cmbBienesTransp.ItemsRequested
        If e.Text.Length > 0 Then
            Dim ds As DataSet
            Dim ObjData As New DataControl
            ds = ObjData.FillDataSet("select top 20 clave as codigo, concat(descripcion, '. ', palabrasSimilares) as descripcion from dbo.tblClaveProdServCP where concat(descripcion, ', ', palabrasSimilares) like   '%" & e.Text & "%' order by descripcion")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbBienesTransp.DataSource = ds
                cmbBienesTransp.DataBind()
            Else
                ds = ObjData.FillDataSet("select top 20 clave as codigo, concat(clave, ' - ', descripcion) as descripcion from dbo.tblClaveProdServCP where concat(clave, ', ', descripcion) like   '%" & e.Text & "%' order by descripcion")
                If ds.Tables(0).Rows.Count > 0 Then
                    cmbBienesTransp.DataSource = ds
                    cmbBienesTransp.DataBind()
                End If
            End If
            ObjData = Nothing
        End If
    End Sub
    Protected Sub cmbClaveUnidad_ItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cmbClaveUnidad.ItemsRequested
        If e.Text.Length > 0 Then
            Dim ds As DataSet
            Dim ObjData As New DataControl
            ds = ObjData.FillDataSet("select top 15 codigo, concat(codigo, ' - ', nombre) as descripcion from dbo.tblClaveUnidad where concat(codigo, ' - ', nombre) like  '%" & e.Text & "%' order by nombre  ")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbClaveUnidad.DataSource = ds
                cmbClaveUnidad.DataBind()
            Else

            End If
            ObjData = Nothing
        End If
    End Sub
    Protected Sub cmbBienesTransp_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbBienesTransp.SelectedIndexChanged
        If valMateriaPeligroso(e.Value) > 0 Then
            cmbCveMaterialPeligroso.Text = ""
            cmbEmbalaje.SelectedValue = 0
            cmbDescripEmbalaje.Text = ""
            PanelMaterialPeligroso.Visible = True
        Else
            PanelMaterialPeligroso.Visible = False
        End If
        txtCantidad.Focus()

    End Sub
    Protected Sub cmbCveMaterialPeligroso_ItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs) Handles cmbCveMaterialPeligroso.ItemsRequested
        If e.Text.Length > 0 Then
            Dim ds As DataSet
            Dim ObjData As New DataControl
            ds = ObjData.FillDataSet("select clave, concat(clave, ' - ' , descripcion ) as descripcion from tblMaterialPeligroso where concat(clave, ' - ' , descripcion ) like  '%" & e.Text & "%' ")
            If ds.Tables(0).Rows.Count > 0 Then
                cmbCveMaterialPeligroso.DataSource = ds
                cmbCveMaterialPeligroso.DataBind()
            Else
                '
            End If
            ObjData = Nothing
        End If
    End Sub
    '
    '
    Protected Sub btnAddDestino_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDestino.Click
        If cmbDestinoCatalogo.SelectedValue <> 0 Then
            Dim FechaLlegada As String = ""
            Dim l As DateTime = txtFechaHoraProgLlegada.SelectedDate
            Dim OBJ As New DataControl
            FechaLlegada = l.Month.ToString + "-" + l.Day.ToString + "-" + l.Year.ToString + " " + l.Hour.ToString + ":" + l.Minute.ToString + ":" + l.Second.ToString + " "
            OBJ.RunSQLQuery("EXEC pCartaPorteDestinos @cmd=1, @cartaPorteId=" & _CartaPorteID & _
                                                           ", @clienteDireccionId=" & cmbDestinoCatalogo.SelectedValue & _
                                                           ", @fechaHoraLlegada='" & FechaLlegada & _
                                                           "', @distanciaRecorrida=" & txtDistanciaRecorrida_d.Text)
            destinoslist_NeedData("on")
            Call CleanInputDestino()
        End If
    End Sub    '
    Protected Sub Operadoreslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles Operadoreslist.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                DeleteOperador(e.CommandArgument)
        End Select
    End Sub
    Private Sub DeleteOperador(ByVal Operadorid As Long)
        Dim Obj As New DataControl
        Dim cmd As String = "EXEC pCartaPorteOperadores @cmd=5, @id =" & Operadorid
        Obj.RunSQLQuery(cmd)
        Operadoreslist_NeedData("on")
    End Sub
    Protected Sub Operadoreslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles Operadoreslist.NeedDataSource
        Operadoreslist_NeedData("off")
    End Sub
    Private Sub Operadoreslist_NeedData(ByVal state As String)
        If _CartaPorteID > 0 Then
            Dim Obj As New DataControl
            Dim result As DataSet
            Dim cmd As String = "EXEC pCartaPorteOperadores @cmd=2, @cartaPorteId=" & _CartaPorteID
            result = Obj.FillDataSet(cmd)
            If result.Tables(0).Rows.Count < 1 Then
                Dim dt As New DataTable
                Operadoreslist.DataSource = dt
            Else
                Operadoreslist.DataSource = Obj.FillDataSet(cmd)
            End If
            If state = "on" Then
                Operadoreslist.DataBind()
            End If
        End If
    End Sub
    Protected Sub btnAddOperador_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddOperador.Click
        If cmbOperador.SelectedValue <> 0 Then
            Dim OBJ As New DataControl
            OBJ.RunSQLQuery("EXEC pCartaPorteOperadores @cmd=1, @cartaPorteId=" & _CartaPorteID & _
                                                           ", @operadorid=" & cmbOperador.SelectedValue)
            Operadoreslist_NeedData("on")
            Call CleanInputOperador()
        End If
    End Sub

    Protected Sub destinoslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles destinoslist.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                DeleteDestino(e.CommandArgument)
        End Select
    End Sub
    Private Sub DeleteDestino(ByVal destinoid As Long)
        Dim Obj As New DataControl
        Dim cmd As String = "EXEC pCartaPorteDestinos @cmd=5, @id =" & destinoid
        Obj.RunSQLQuery(cmd)
        destinoslist_NeedData("on")
    End Sub
    Protected Sub destinoslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles destinoslist.NeedDataSource
        destinoslist_NeedData("off")
    End Sub
    Private Sub destinoslist_NeedData(ByVal state As String)
        If _CartaPorteID > 0 Then
            Dim Obj As New DataControl
            Dim result As DataSet
            Dim cmd As String = "EXEC pCartaPorteDestinos @cmd=2, @cartaPorteId=" & _CartaPorteID
            result = Obj.FillDataSet(cmd)
            If result.Tables(0).Rows.Count < 1 Then
                destinoslist.DataSource = New DataTable
            Else
                destinoslist.DataSource = Obj.FillDataSet(cmd)
            End If
            If state = "on" Then
                destinoslist.DataBind()
            End If
        End If
    End Sub
    Protected Sub remolqueslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles remolqueslist.NeedDataSource
        remolqueslist_NeedData("off")
    End Sub
    Protected Sub cmbMaterialPeligroso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMaterialPeligroso.SelectedIndexChanged
        If cmbMaterialPeligroso.SelectedValue = "No" Then
            cmbCveMaterialPeligroso.SelectedValue = ""
            cmbCveMaterialPeligroso.Text = ""
            cmbEmbalaje.SelectedValue = 0
            cmbCveMaterialPeligroso.Enabled = False
            cmbEmbalaje.Enabled = False
        Else
            cmbCveMaterialPeligroso.Enabled = True
            cmbEmbalaje.Enabled = True
        End If
    End Sub
#End Region

#Region "Funciones de Carta Porte"
    Private Sub verificaCartaPorte()
        'carta porte
        CargaCbmDirecciones_Cliente(cmbClient.SelectedValue)
        If GetEstateCartaPorte() = True Then
            ckCartaPorte.Checked = True
            panelCartaPorte.Visible = True
            Dim Obj As New DataControl
            Dim dt As DataSet = Obj.FillDataSet("EXEC pCartaPorte @cmd=3, @cfdid='" & Session("CFD") & "'")
            For Each row As DataRow In dt.Tables(0).Rows
                txtRFCRemitente.Text = row("RFCRemitente")
                txtNombreRemitente.Text = row("NombreRemitente")
                txtFechaHoraSalida.DbSelectedDate = row("FechaHoraSalida")

                txtRFCDestinatario.Text = row("RFCDestinatario")
                txtNombreDestinatario.Text = row("NombreDestinatario")
                txtFechaHoraProgLlegada.DbSelectedDate = row("FechaHoraProgLlegada")
                txtDistanciaRecorrida_d.Text = row("DistanciaRecorrida_d")

                cmbPermSCT.SelectedValue = row("PermSCT")
                txtNumPermisoSCT.Text = row("NumPermisoSCT")
                txtNombreAseg.Text = row("NombreAseg")
                txtNumPolizaSeguro.Text = row("NumPolizaSeguro")

                cmbConfigVehicular.Text = row("ConfigVehicular")
                txtPlacaVM.Text = row("PlacaVM")

                txtAnioModeloVM.Text = row("AnioModeloVM")

                txtRFCOperador.Text = row("RFCOperador")
                txtNumLicencia.Text = row("NumLicencia")
                txtNombreOperador.Text = row("NombreOperador")
                '
                txtCodigoPostal_or.Text = row("CodigoPostal_or")
                DomicilioOr_cmbCodigos(row("Municipio_or"), row("Colonia_or"), row("Localidad_or"))
                txtCalle_or.Text = row("Calle_or")
                txtNumeroExterior_or.Text = row("NumeroExterior_or")
                txtNumeroInterior_or.Text = row("NumeroInterior_or")
                txtReferencia_or.Text = row("Referencia_or")
                '
                txtCodigoPostal_des.Text = row("CodigoPostal_des")
                'DomicilioDes_cmbCodigos(row("Municipio_des"), row("Colonia_des"), row("Localidad_des"))
                txtCalle_des.Text = row("Calle_des")
                txtNumeroExterior_des.Text = row("NumeroExterior_des")
                txtNumeroInterior_des.Text = row("NumeroInterior_des")
                txtReferencia_des.Text = row("Referencia_des")
                '
                txtCodigoPostal_op.Text = row("CodigoPostal_op")
                'DomicilioOp_cmbCodigos(row("Municipio_op"), row("Colonia_op"), row("Localidad_op"))
                txtCalle_op.Text = row("Calle_op")
                txtNumeroExterior_op.Text = row("NumeroExterior_op")
                txtNumeroInterior_op.Text = row("NumeroInterior_op")
                txtReferencia_op.Text = row("Referencia_op")
            Next
            '_CartaPorteID = Obj.RunSQLScalarQuery("EXEC pCartaPorteMercancias @cmd=6, @cfdid='" & Session("CFD") & "'")
            verificaMedAmbiente()
            '
        End If
    End Sub
    Public Sub creaColumnasCache()
        pcache = New DataTable
        pcache.Columns.Add("id", GetType(Integer))
        pcache.Columns.Add("BienesTransp", GetType(String))
        pcache.Columns.Add("BienesTransp_D", GetType(String))
        pcache.Columns.Add("Descripcion", GetType(String))
        pcache.Columns.Add("Cantidad", GetType(String))
        pcache.Columns.Add("ClaveUnidad", GetType(String))
        pcache.Columns.Add("ClaveUnidad_D", GetType(String))
        pcache.Columns.Add("PesoEnKg", GetType(String))
        pcache.Columns.Add("ValorMercancia", GetType(String))
        pcache.Columns.Add("Moneda", GetType(String))
        pcache.Columns.Add("MaterialPeligroso", GetType(String))
        pcache.Columns.Add("CveMaterialPeligroso", GetType(String))
        pcache.Columns.Add("Embalaje", GetType(String))
        pcache.Columns.Add("DescripEmbalaje", GetType(String))

    End Sub
    Private Sub DomicilioOr_cmbCodigos(ByVal municipio As String, ByVal colonia As String, ByVal localidad As String)
        If txtCodigoPostal_or.Text.Length = 5 Then
            Dim code As String = codePais(txtCodigoPostal_or.Text)
            Dim ObjCat As New DataControl
            ObjCat.Catalogo(cmbColonia_or, "select c_Colonia as clave, NombreAsentamiento as nombre from tblColonia where c_CodigoPostal =" & txtCodigoPostal_or.Text, colonia)
            ObjCat.Catalogo(cmbEstado_or, " select top 1 a.clave, a.nombre from tblEstado a inner join tblCodigoPostal b on a.clave = b.clave where b.codigo ='" & txtCodigoPostal_or.Text & "'", code)
            ObjCat.Catalogo(txtLocalidad_or, "select localidad as clave, descripcion as nombre from tblLocalidad where estado ='" & code & "'", localidad)
            ObjCat.Catalogo(cmbMunicipio_or, "select ISNULL(b.municipio,'') as clave, ISNULL(b.descripcion,'') as descripcion from tblCodigoPostal a right join tblMunicipio b on a.clave = b.estado and a.c_Municipio = b.municipio where a.codigo = '" & txtCodigoPostal_or.Text & "'", municipio)
            ObjCat = Nothing
        Else
            Dim dt As New DataTable()
            cmbColonia_or.DataSource = dt
            cmbEstado_or.DataSource = dt
            txtLocalidad_or.DataSource = dt
            cmbMunicipio_or.DataSource = dt
        End If
    End Sub
    Public Sub tblMercancias_NeedData(ByVal state As String)
        tblMercancias.MasterTableView.NoMasterRecordsText = "No se han agregado mercancias"
        If _CartaPorteID = 0 Then
            tblMercancias.DataSource = pcache
        Else
            Dim Obj As New DataControl
            tblMercancias.DataSource = Obj.FillDataSet("EXEC pCartaPorteMercancias @cmd=2, @cartaporteid='" & _CartaPorteID & "'")
        End If
        If state = "on" Then
            tblMercancias.DataBind()
        End If
        tblMercancias.MasterTableView.NoMasterRecordsText = "No se han agregado mercancias"
    End Sub
    Private Sub remolqueslist_NeedData(ByVal state As String)
        Dim Obj As New DataControl
        Dim result As DataSet
        Dim cmd As String = "EXEC pAutotransportesRemolques @cmd=3, @autotransporteid=" & cmbAutotransporte.SelectedValue
        result = Obj.FillDataSet(cmd)
        If result.Tables(0).Rows.Count < 1 Or TieneRemolque(cmbConfigVehicular.SelectedValue, cmbAutotransporte.SelectedValue) = False Then
            Dim dt As New DataTable
            remolqueslist.DataSource = dt
        Else
            remolqueslist.DataSource = Obj.FillDataSet(cmd)
        End If
        If state = "on" Then
            remolqueslist.DataBind()
        End If
    End Sub
    Public Sub ResetInputAddMercancia()
        cmbBienesTransp.Text = ""
        txtDescripcion.Text = ""
        txtCantidad.Text = ""
        txtPesoEnKg.Text = ""
        cmbClaveUnidad.Text = ""
        txtValorMercancia.Text = ""
        cmbMoneda.SelectedValue = 0
        PanelMaterialPeligroso.Visible = False
        cmbCveMaterialPeligroso.Text = ""
        cmbCveMaterialPeligroso.SelectedValue = ""
        cmbEmbalaje.SelectedValue = 0

    End Sub
    Private Sub InitialComboPais()
        cmbPais_or.SelectedValue = "MEX"
    End Sub
    Function codePais(ByVal cp As String) As String
        Dim code As String = ""
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("select top 1 a.clave from tblEstado a inner join tblCodigoPostal b on a.clave = b.clave where b.codigo =" & cp, conn)
        conn.Open()
        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()
        If rs.Read Then
            code = rs("clave")
        End If
        conn.Close()
        conn.Dispose()
        Return code
    End Function
    Function valMateriaPeligroso(ByVal code As String) As Long
        cmbMaterialPeligroso.Enabled = False

        Dim materialpeligroso As Long = 0
        Dim indicador As String
        Dim Obj As New DataControl

        indicador = Obj.RunSQLScalarQueryString("select isnull(materialPeligroso,'') from tblClaveProdServCP where clave ='" & code & "'")

        If indicador = "0" Then
            cmbMaterialPeligroso.SelectedValue = "No"
            Return 0
        ElseIf indicador = "1" Then
            cmbMaterialPeligroso.SelectedValue = "Si"
            Return 1
        Else
            cmbMaterialPeligroso.Enabled = True
            Return 2
        End If
    End Function
    Public Sub DeleteMercancia(ByVal id As Integer)
        If _CartaPorteID = 0 Then
            If Request("id") Is Nothing Then
                Dim pAux As New DataTable
                pAux = pcache.Clone()
                For Each row As DataRow In pcache.Rows
                    If row("id") <> id Then
                        pAux.ImportRow(row)
                    End If
                Next
                pcache = New DataTable
                pcache = pAux.Copy()

            End If
        Else
            Dim Obj As New DataControl
            Obj.RunSQLQuery("EXEC pCartaPorteMercancias @cmd=5, @id='" & id & "'")
        End If
        tblMercancias_NeedData("on")
    End Sub
    Private Function GetEstateCartaPorte() As Boolean
        Dim Obj As New DataControl
        Dim cartaporte As Boolean = 0
        Dim cmdo As String = "EXEC pCartaPorte @cmd=7, @cfdid='" & Session("CFD") & "'"
        Dim result As DataSet = Obj.FillDataSet("EXEC pCartaPorte @cmd=7, @cfdid='" & Session("CFD") & "'")
        If result.Tables(0).Rows.Count > 0 Then
            For Each row As DataRow In result.Tables(0).Rows
                _CartaPorteID = row("id")
                If row("origenid") > 0 Then
                    cmbOrigenCatalogo.SelectedValue = row("origenid")
                    CargaDatos_OrigenCatalogo()
                End If
                If row("autotransporteid") > 0 Then
                    cmbAutotransporte.SelectedValue = row("autotransporteid")
                    cargaAutotrasporte(row("autotransporteid"))
                End If
                txtFechaHoraSalida.SelectedDate = row("FechaHoraSalida")
                txtPesoNetoTotal.Text = row("PesoNetoTotal")
                cmbUnidadPeso.SelectedValue = row("UnidadPeso")
            Next
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub CargaCbmDirecciones_Cliente(ByVal id As Integer)
        Dim ObjCat As New DataControl
        ObjCat.Catalogo(cmbDestinoCatalogo, "EXEC pMisClientesDirecciones @cmd=3, @clienteid='" & id & "'", 0)
    End Sub
    Private Sub CargaCmbDirecciones_Remitente()
        Dim ObjCat As New DataControl
        ObjCat.Catalogo(cmbOrigenCatalogo, "EXEC pOrigenDirecciones @cmd=3", 0)
    End Sub
    Private Sub CleanInputDestino()
        PanelDireccionDestino.Visible = False
        txtNombreDestinatario.Text = ""
        txtRFCDestinatario.Text = ""
        txtIDUbicacion_des.Text = ""
        txtPais_des.Text = ""
        txtCodigoPostal_des.Text = ""
        txtEstado_des.Text = ""
        txtMunicipio_des.Text = ""
        txtColonia_des.Text = ""
        txtCalle_des.Text = ""
        txtNumeroExterior_des.Text = ""
        txtNumeroInterior_des.Text = ""
        txtReferencia_des.Text = ""
        cmbDestinoCatalogo.SelectedValue = 0
        txtDistanciaRecorrida_d.Text = ""
        txtFechaHoraProgLlegada.SelectedDate = Nothing
    End Sub
    Private Sub CleanInputOperador()
        txtNombreOperador.Text = ""
        txtRFCOperador.Text = ""
        txtTipoFigura.Text = ""
        txtNumLicencia.Text = ""
        txtPais_op.Text = ""
        txtCodigoPostal_op.Text = ""
        txtEstado_op.Text = ""
        txtMunicipio_op.Text = ""
        txtColonia_op.Text = ""
        txtCalle_op.Text = ""
        txtNumeroExterior_op.Text = ""
        txtNumeroInterior_op.Text = ""
        txtReferencia_op.Text = ""
        cmbOperador.SelectedValue = 0
        PanelDireccionOperador.Visible = False
    End Sub
    Private Function TieneRemolque(ByVal ConfigVehicular As String, ByVal idCatRem As Long) As Boolean
        Dim remolque As String
        Dim Obj As New DataControl
        remolque = Obj.RunSQLScalarQueryString("EXEC pAutotransportes @cmd=7, @ConfigVehicularId='" & ConfigVehicular & "'")
        If remolque = "1" Then
            Return True
        ElseIf remolque = "0" Then
            Return False
        Else
            Dim numRemolques As Long = 0
            numRemolques = Obj.RunSQLScalarQuery("select isnull(count(id),0) from tblAutotransportesRemolques where autotransporteid='" & idCatRem & "'")
            If numRemolques > 0 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Private Function HayMercanciaPeligrosa(ByVal cartaporteid As Long) As Boolean
        Dim materiapeligroso As Boolean = False
        Dim Obj As New DataControl
        Dim result As Long
        result = Obj.RunSQLScalarQuery("select count(id) from tblCartaPorteMercancias where (MaterialPeligroso = 'Si') and (BorradorBit is null) and cartaporteid =" & cartaporteid)
        If result > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub verificaMedAmbiente()
        If HayMercanciaPeligrosa(_CartaPorteID) = True Then
            panelMedAmbiente.Visible = True
        Else
            panelMedAmbiente.Visible = False
        End If
    End Sub
#End Region

#Region "Manipulacion de datos DB CARTA PORTE"

    Function SaveCartaPorte() As Integer
        If Session("CFD") = 0 Then
            GetCFD()
        End If
        Dim CartaPorteID As Integer = 0
        Dim Obj As New DataControl
        Dim safdsa As String = "exec pCartaPorte @cmd=1, @cfdid='" & Session("CFD") & "'"
        CartaPorteID = Obj.RunSQLScalarQuery("exec pCartaPorte @cmd=1, @cfdid='" & Session("CFD") & "'")
        Return CartaPorteID
    End Function

    Private Sub updateCartaPorte()
        Dim FechaSalida As String = ""

        Dim s As DateTime = txtFechaHoraSalida.SelectedDate

        FechaSalida = s.Month.ToString + "-" + s.Day.ToString + "-" + s.Year.ToString + " " + s.Hour.ToString + ":" + s.Minute.ToString + ":" + s.Second.ToString + " "
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("exec pCartaPorte @cmd=4, @TranspInternac='No', " & _
                                                 "   @origenid='" & cmbOrigenCatalogo.SelectedValue & _
                                                 "', @autotransporteid='" & cmbAutotransporte.SelectedValue & _
                                                 "', @TotalDistRec='" & txtDistanciaRecorrida_d.Text & _
                                                 "', @FechaHoraSalida='" & FechaSalida & _
                                                 "', @UnidadPeso='" & cmbUnidadPeso.SelectedValue & _
                                                 "', @id='" & _CartaPorteID & "'", conn)
        conn.Open()
        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()
        conn.Close()
        conn.Dispose()
    End Sub

    Public Sub saveMercancia(ByVal CartaPorteID As Integer)
        For Each row In pcache.Rows
            Dim ObjData As New DataControl
            ObjData.RunSQLQuery("exec pCartaPorteMercancias @cmd=1, @cartaporteid='" & CartaPorteID & _
                                                                "', @BienesTransp='" & row("BienesTransp") & _
                                                                "', @BienesTransp_D='" & row("BienesTransp_D") & _
                                                                "', @Descripcion='" & row("Descripcion") & _
                                                                "', @Cantidad='" & row("Cantidad") & _
                                                                "', @ClaveUnidad='" & row("ClaveUnidad") & _
                                                                "', @PesoEnKg='" & row("PesoEnKg") & _
                                                                "', @ValorMercancia='" & row("ValorMercancia") & _
                                                                "', @Moneda='" & row("Moneda") & _
                                                                "', @MaterialPeligroso='" & row("MaterialPeligroso") & _
                                                                "', @CveMaterialPeligroso='" & row("CveMaterialPeligroso") & _
                                                                "', @DescripEmbalaje='" & row("DescripEmbalaje") & _
                                                                "', @Embalaje='" & row("Embalaje") & "' ")
            ObjData = Nothing
        Next
    End Sub

#End Region

#Region "Creacion de archivo xml CARTA PORTE"
    Public Sub CreaNodoCartaPorte(ByVal nodo As XmlNode)
        '
        Dim registroID As Integer = 0
        If tblMercancias.Items.Count < 1 Then
            valMercancias.IsValid = False
            Exit Sub
        Else
            If _CartaPorteID = 0 Then
                registroID = SaveCartaPorte()
            Else
                registroID = _CartaPorteID
            End If

            Call updateCartaPorte()
            Call SaveMercancia(registroID)
            '
            '
            'Crea nodo carta porte
            Dim cartaporte As XmlElement
            Dim complemento As XmlElement
            Dim ubicacion As XmlElement
            Dim uOrigen As XmlElement
            Dim uDestino As XmlElement
            Dim mercancias As XmlElement
            Dim AutoTrasporte As XmlElement
            Dim IdentificacionVehicular As XmlElement
            Dim figuraTasporte As XmlElement
            Dim Domicilio_or As XmlElement
            Dim Domicilio_des As XmlElement
            Dim Domicilio_op As XmlElement
            Dim Seguros As XmlElement
            Dim Remolques As XmlElement
            Dim Remolque As XmlElement
            Dim TiposFigura As XmlElement
            Dim Obj2 As New DataControl
            complemento = CrearNodo("cfdi:Complemento")
            IndentarNodo(complemento, 2)

            Dim DisTotalRec = Obj2.RunSQLScalarQuery("EXEC pCartaPorteDestinos @cmd=6, @cartaPorteId=" & _CartaPorteID)
            cartaporte = CrearNodoCartaPorte("cartaporte20:CartaPorte")
            cartaporte.SetAttribute("TranspInternac", "No")
            If DisTotalRec > 0 Then
                cartaporte.SetAttribute("TotalDistRec", DisTotalRec)
            End If

            cartaporte.SetAttribute("Version", "2.0")
            IndentarNodo(cartaporte, 3)

            ubicacion = CrearNodoCartaPorte("cartaporte20:Ubicaciones")
            uOrigen = CrearNodoCartaPorte("cartaporte20:Ubicacion")
            '
            'nodo carta porte origen 
            '
            uOrigen.SetAttribute("TipoUbicacion", "Origen")
            'IDUbicacion
            uOrigen.SetAttribute("FechaHoraSalidaLlegada", Format(txtFechaHoraSalida.SelectedDate, "yyyy-MM-ddTHH:mm:ss"))
            '
            '::::: UBICACION ORIGEN :::::::::::
            '
            Domicilio_or = CrearNodoCartaPorte("cartaporte20:Domicilio")

            Dim Obj_Origen As New DataControl
            Dim result_or As DataSet = Obj_Origen.FillDataSet("exec pOrigenDirecciones @cmd=2, @id=" & cmbOrigenCatalogo.SelectedValue)
            For Each row As DataRow In result_or.Tables(0).Rows
                uOrigen.SetAttribute("RFCRemitenteDestinatario", row("rfc_or"))
                uOrigen.SetAttribute("NombreRemitenteDestinatario", row("nombre_or"))
                Domicilio_or.SetAttribute("Pais", "MEX")
                Domicilio_or.SetAttribute("CodigoPostal", row("CodigoPostal_or"))
                Domicilio_or.SetAttribute("Estado", row("Estado_or"))
                Domicilio_or.SetAttribute("Calle", row("Calle_or"))
                If row("Municipio_or").ToString.Length <> 0 Then
                    Domicilio_or.SetAttribute("Municipio", row("Municipio_or"))
                End If
                If row("Colonia_or").ToString.Length <> 0 Then
                    Domicilio_or.SetAttribute("Colonia", row("Colonia_or"))
                End If
                If row("NumeroExterior_or").ToString.Length <> 0 Then
                    Domicilio_or.SetAttribute("NumeroExterior", row("NumeroExterior_or"))
                End If
                If row("NumeroInterior_or").ToString.Length > 0 Then
                    Domicilio_or.SetAttribute("NumeroInterior", row("NumeroInterior_or"))
                End If
                If row("Referencia_or").ToString.Length > 0 Then
                    Domicilio_or.SetAttribute("Referencia", row("Referencia_or"))
                End If
            Next
            '
            ' Agrega origen al xml
            '
            uOrigen.AppendChild(Domicilio_or)
            ubicacion.AppendChild(uOrigen)
            '
            '::::: UBICACION DESTINO :::::::::::
            '
            Dim Obj_desigen As New DataControl
            Dim result_des As DataSet = Obj_desigen.FillDataSet("exec pCartaPorteDestinos @cmd=2, @cartaPorteId=" & _CartaPorteID)
            For Each row As DataRow In result_des.Tables(0).Rows

                uDestino = CrearNodoCartaPorte("cartaporte20:Ubicacion")
                uDestino.SetAttribute("TipoUbicacion", "Destino")
                uDestino.SetAttribute("DistanciaRecorrida", row("distanciaRecorrida"))
                uDestino.SetAttribute("FechaHoraSalidaLlegada", Format(row("fechaHoraLlegada"), "yyyy-MM-ddTHH:mm:ss"))
                Domicilio_des = CrearNodoCartaPorte("cartaporte20:Domicilio")

                uDestino.SetAttribute("RFCRemitenteDestinatario", row("rfc_des"))
                uDestino.SetAttribute("NombreRemitenteDestinatario", row("nombre_des"))
                Domicilio_des.SetAttribute("Pais", "MEX")
                Domicilio_des.SetAttribute("CodigoPostal", row("CodigoPostal_des"))
                Domicilio_des.SetAttribute("Estado", row("Estado_des"))
                Domicilio_des.SetAttribute("Calle", row("Calle_des"))
                If row("Municipio_des").ToString.Length <> 0 Then
                    Domicilio_des.SetAttribute("Municipio", row("Municipio_des"))
                End If
                If row("Colonia_des").ToString.Length <> 0 Then
                    Domicilio_des.SetAttribute("Colonia", row("Colonia_des"))
                End If
                If row("NumeroExterior_des").ToString.Length <> 0 Then
                    Domicilio_des.SetAttribute("NumeroExterior", row("NumeroExterior_des"))
                End If
                If row("NumeroInterior_des").ToString.Length > 0 Then
                    Domicilio_des.SetAttribute("NumeroInterior", row("NumeroInterior_des"))
                End If
                If row("Referencia_des").ToString.Length > 0 Then
                    Domicilio_des.SetAttribute("Referencia", row("Referencia_des"))
                End If
                uDestino.AppendChild(Domicilio_des)
                ubicacion.AppendChild(uDestino)
            Next
            '
            'Nodo Mercacnias
            '
            Dim totalMercancias As Integer = 0
            Dim pesoBrutoTotal As Double = 0
            Dim materialPerligroso As Boolean = False
            mercancias = CrearNodoCartaPorte("cartaporte20:Mercancias")
            Dim Obj As New DataControl
            Dim dt As New DataSet
            dt = Obj.FillDataSet("EXEC pCartaPorteMercancias @cmd=2, @cartaporteid = '" & _CartaPorteID & "'")
            For Each row As DataRow In dt.Tables(0).Rows
                totalMercancias += 1
                Dim mercancia As XmlElement
                mercancia = CrearNodoCartaPorte("cartaporte20:Mercancia")
                mercancia.SetAttribute("BienesTransp", row("BienesTransp"))
                mercancia.SetAttribute("Cantidad", row("Cantidad"))
                mercancia.SetAttribute("ClaveUnidad", row("ClaveUnidad"))
                

                Dim valorMatPeligroso As String = ""
                valorMatPeligroso = Obj.RunSQLScalarQueryString("select isnull(materialPeligroso, '') from tblClaveProdServCP where clave =" & row("BienesTransp"))
                If valorMatPeligroso = "1" Then
                    mercancia.SetAttribute("MaterialPeligroso", "Sí")
                    mercancia.SetAttribute("CveMaterialPeligroso", row("CveMaterialPeligroso"))
                    mercancia.SetAttribute("Embalaje", row("Embalaje"))
                    Dim desEmbj As String = Obj.RunSQLScalarQueryString("select isnull(descripcion, 'No se encontro descripcion') from tblTipoEmbalaje where clave = '" & row("Embalaje") & "'")
                    mercancia.SetAttribute("DescripEmbalaje", desEmbj)
                    materialPerligroso = True
                ElseIf valorMatPeligroso = "0,1" Then
                    If row("MaterialPeligroso") = "Si" Then
                        mercancia.SetAttribute("MaterialPeligroso", "Sí")
                        mercancia.SetAttribute("CveMaterialPeligroso", row("CveMaterialPeligroso"))
                        mercancia.SetAttribute("Embalaje", row("Embalaje"))
                        Dim desEmbj As String = Obj.RunSQLScalarQueryString("select isnull(descripcion, 'No se encontro descripcion') from tblTipoEmbalaje where clave = '" & row("Embalaje") & "'")
                        mercancia.SetAttribute("DescripEmbalaje", desEmbj)
                        materialPerligroso = True
                    ElseIf row("MaterialPeligroso") = "No" Then
                        mercancia.SetAttribute("MaterialPeligroso", "No")
                    End If
                End If
                'If Not (valorMatPeligroso = "0" Or valorMatPeligroso.Length < 1) Then
                '    mercancia.SetAttribute("MaterialPeligroso", "No")
                'End If

                If row("Descripcion").ToString <> "" Then
                    mercancia.SetAttribute("Descripcion", row("Descripcion"))
                Else
                    mercancia.SetAttribute("Descripcion", Obj2.RunSQLScalarQueryString("select isnull(descripcion, '') from tblClaveProdServCP where clave = '" & row("BienesTransp") & "'"))
                End If
                If row("valorMercancia") > 0 Then
                    mercancia.SetAttribute("Moneda", "MXN")
                End If
                If row("PesoEnKg") > 0 Then
                    mercancia.SetAttribute("PesoEnKg", row("PesoEnKg"))
                    pesoBrutoTotal += row("PesoEnKg")
                End If
                If row("valorMercancia") > 0 Then
                    mercancia.SetAttribute("valorMercancia", row("valorMercancia"))
                End If
                mercancias.AppendChild(mercancia)
            Next
            mercancias.SetAttribute("PesoBrutoTotal", Format(pesoBrutoTotal, "###.000"))
            mercancias.SetAttribute("UnidadPeso", cmbUnidadPeso.SelectedValue)
            mercancias.SetAttribute("NumTotalMercancias", totalMercancias)
            If txtPesoNetoTotal.Text > 0 Then
                mercancias.SetAttribute("PesoNetoTotal", txtPesoNetoTotal.Text)
            End If
            '
            'Sub nodo autotrasporteFederal
            '
            AutoTrasporte = CrearNodoCartaPorte("cartaporte20:Autotransporte")
            AutoTrasporte.SetAttribute("PermSCT", cmbPermSCT.SelectedValue)
            AutoTrasporte.SetAttribute("NumPermisoSCT", txtNumPermisoSCT.Text.ToUpper)
            '
            'Identificacion Vahicular
            '
            IdentificacionVehicular = CrearNodoCartaPorte("cartaporte20:IdentificacionVehicular")
            IdentificacionVehicular.SetAttribute("ConfigVehicular", cmbConfigVehicular.SelectedValue)
            IdentificacionVehicular.SetAttribute("PlacaVM", txtPlacaVM.Text.ToUpper)
            IdentificacionVehicular.SetAttribute("AnioModeloVM", txtAnioModeloVM.Text)
            AutoTrasporte.AppendChild(IdentificacionVehicular)
            '
            'Seguros
            '
            Seguros = CrearNodoCartaPorte("cartaporte20:Seguros")
            Seguros.SetAttribute("AseguraRespCivil", txtNombreAseg.Text.ToUpper)
            Seguros.SetAttribute("PolizaRespCivil", txtNumPolizaSeguro.Text.ToUpper)
            AutoTrasporte.AppendChild(Seguros)
            If materialPerligroso Then
                Seguros.SetAttribute("PolizaMedAmbiente", txtPolizaMedAmbiente.Text.ToUpper)
                Seguros.SetAttribute("AseguraMedAmbiente", txtAseguraMedAmbiente.Text.ToUpper)
            End If
            '
            'Remolques
            '
            If TieneRemolque(cmbConfigVehicular.SelectedValue, cmbAutotransporte.SelectedValue) = True Then
                Remolques = CrearNodoCartaPorte("cartaporte20:Remolques")
                Dim obj_rem As New DataControl
                Dim result_rem As DataSet = obj_rem.FillDataSet("EXEC pAutotransportesRemolques @cmd=3, @autotransporteid=" & cmbAutotransporte.SelectedValue)
                For Each row As DataRow In result_rem.Tables(0).Rows
                    Remolque = CrearNodoCartaPorte("cartaporte20:Remolque")
                    Remolque.SetAttribute("SubTipoRem", row("SubTipoRem"))
                    Remolque.SetAttribute("Placa", row("Placa"))
                    Remolques.AppendChild(Remolque)
                Next
                AutoTrasporte.AppendChild(Remolques)
            End If
            '
            '
            mercancias.AppendChild(AutoTrasporte)
            '
            'Nodo operador
            '
            figuraTasporte = CrearNodoCartaPorte("cartaporte20:FiguraTransporte")

            Dim Obj_op As New DataControl
            Dim result_op As DataSet = Obj_desigen.FillDataSet("exec pCartaPorteOperadores @cmd=2, @cartaPorteId=" & _CartaPorteID)

            For Each row As DataRow In result_op.Tables(0).Rows

                TiposFigura = CrearNodoCartaPorte("cartaporte20:TiposFigura")
                Domicilio_op = CrearNodoCartaPorte("cartaporte20:Domicilio")

                TiposFigura.SetAttribute("TipoFigura", row("tipofigura"))
                TiposFigura.SetAttribute("RFCFigura", row("RFCOperador").ToString.ToUpper)

                If row("tipofigura") = "01" Then
                    TiposFigura.SetAttribute("NumLicencia", row("NumLicencia").ToString.ToUpper)
                End If

                If row("NombreOperador").ToString.Length > 0 Then
                    TiposFigura.SetAttribute("NombreFigura", row("NombreOperador").ToString.ToUpper)
                End If
                Domicilio_op.SetAttribute("Pais", row("Pais_op"))
                Domicilio_op.SetAttribute("CodigoPostal", row("CodigoPostal_op"))
                Domicilio_op.SetAttribute("Estado", row("Estado_op"))
                Domicilio_op.SetAttribute("Calle", row("Calle_op"))
                If row("Municipio_op").ToString.Length <> 0 Then
                    Domicilio_op.SetAttribute("Municipio", row("Municipio_op"))
                End If
                If row("Colonia_op").ToString.Length <> 0 Then
                    Domicilio_op.SetAttribute("Colonia", row("Colonia_op"))
                End If
                If row("NumeroExterior_op").ToString.Length <> 0 Then
                    Domicilio_op.SetAttribute("NumeroExterior", row("NumeroExterior_op"))
                End If
                If row("NumeroInterior_op").ToString.Length > 0 Then
                    Domicilio_op.SetAttribute("NumeroInterior", row("NumeroInterior_op"))
                End If
                If row("Referencia_op").ToString.Length > 0 Then
                    Domicilio_op.SetAttribute("Referencia", row("Referencia_op"))
                End If
                TiposFigura.AppendChild(Domicilio_op)
                figuraTasporte.AppendChild(TiposFigura)
            Next
            '
            'TiposFigura.AppendChild(PartesTransporte)
            '    
            IndentarNodo(ubicacion, 3)
            cartaporte.AppendChild(ubicacion)

            IndentarNodo(mercancias, 3)
            cartaporte.AppendChild(mercancias)

            IndentarNodo(figuraTasporte, 3)
            cartaporte.AppendChild(figuraTasporte)

            IndentarNodo(cartaporte, 2)
            complemento.AppendChild(cartaporte)
            IndentarNodo(complemento, 1)
            nodo.AppendChild(complemento)

        End If

    End Sub

    Private Function CrearNodoCartaPorte(ByVal Nombre As String) As XmlNode
        CrearNodoCartaPorte = m_xmlDOM.CreateNode(XmlNodeType.Element, Nombre, "http://www.sat.gob.mx/CartaPorte20")
    End Function


#End Region

#Region "Catalogos de carta porte"
    Protected Sub cmbAutotransporte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAutotransporte.SelectedIndexChanged
        If cmbAutotransporte.SelectedValue > 0 Then
            cargaAutotrasporte(cmbAutotransporte.SelectedValue)
        End If
        remolqueslist_NeedData("on")
    End Sub
    Protected Sub cmbOperador_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOperador.SelectedIndexChanged
        If cmbOperador.SelectedValue > 0 Then
            cargaOperador(cmbOperador.SelectedValue)
        End If
    End Sub
    Private Sub cargaAutotrasporte(ByVal id As Integer)
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pAutotransportes @cmd=2, @id=" & id)
        For Each row As DataRow In dt.Tables(0).Rows
            cmbPermSCT.SelectedValue = row("PermSCTId")
            txtNumPermisoSCT.Text = row("NumPermisoSCT")
            txtNombreAseg.Text = row("NombreAseg")
            txtAseguraMedAmbiente.Text = row("AseguraMedAmbiente")
            txtPolizaMedAmbiente.Text = row("PolizaMedAmbiente")
            txtNumPolizaSeguro.Text = row("NumPolizaSeguro")
            cmbConfigVehicular.SelectedValue = row("ConfigVehicularId")
            txtPlacaVM.Text = row("PlacaVM")
            txtAnioModeloVM.Text = row("AnioModeloVM")
            'If row("AseguraMedAmbiente").ToString.Length < 1 Then
            '    txtAseguraMedAmbiente.Text = row("NombreAseg")
            'End If
            'If row("PolizaMedAmbiente").ToString.Length < 1 Then
            '    txtPolizaMedAmbiente.Text = row("NumPolizaSeguro")
            'End If
        Next
    End Sub
    Private Sub CargaDireccion_RemitenteOrigen(ByVal id As Integer)
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pOrigenDirecciones @cmd=2, @id=" & id)

        For Each row As DataRow In dt.Tables(0).Rows
            txtCodigoPostal_or.Text = row("CodigoPostal_or")
            DomicilioOr_cmbCodigos(row("Municipio_or"), row("Colonia_or"), row("Localidad_or"))
            txtCalle_or.Text = row("Calle_or")
            txtNumeroExterior_or.Text = row("NumeroExterior_or")
            txtNumeroInterior_or.Text = row("NumeroInterior_or")
            txtReferencia_or.Text = row("Referencia_or")
        Next
    End Sub
    Private Sub CargaDireccion_ClienteDireccion(ByVal id As Integer)
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pMisClientesDirecciones @cmd=2, @id=" & id)

        For Each row As DataRow In dt.Tables(0).Rows
            txtCodigoPostal_des.Text = row("CodigoPostal_des")
            DomicilioOr_cmbCodigos(row("Municipio_des"), row("Colonia_des"), row("Localidad_des"))
            txtCalle_des.Text = row("Calle_des")
            txtNumeroExterior_des.Text = row("NumeroExterior_des")
            txtNumeroInterior_des.Text = row("NumeroInterior_des")
            txtReferencia_des.Text = row("Referencia_des")
        Next
    End Sub
    Private Sub cargaOperador(ByVal id As Integer)
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pOperadores @cmd=2, @id=" & id)
        For Each row As DataRow In dt.Tables(0).Rows
            PanelDireccionOperador.Visible = True
            txtRFCOperador.Text = row("RFCOperador")
            txtNumLicencia.Text = row("NumLicencia")
            txtNombreOperador.Text = row("NombreOperador")
            txtCodigoPostal_op.Text = row("CodigoPostal_op")
            txtCalle_op.Text = row("Calle_op")
            txtNumeroExterior_op.Text = row("NumeroExterior_op")
            txtNumeroInterior_op.Text = row("NumeroInterior_op")
            txtReferencia_op.Text = row("Referencia_op")
            txtPais_op.Text = row("Pais_op")
            txtEstado_op.Text = row("estado_nombre")
            txtMunicipio_op.Text = row("municipio_nombre")
            txtColonia_op.Text = row("colonia_nombre")
            txtTipoFigura.Text = row("tipoFigura_descripcion")
            If row("tipoFigura_descripcion") = "Operador" Then
                panelLicencia.Visible = True
            Else
                panelLicencia.Visible = False
            End If
        Next
    End Sub
    Private Sub DomiciliosPais()
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("select isnull(codigo,'') as clave, isnull(descripcion,'') as descripcion from tblPais ")
        cmbPais_or.DataSource = dt
        cmbPais_or.DataBind()
        cmbPais_or.SelectedValue = "MEX"
        txtPais_des.Text = "MEX"
    End Sub
    Protected Sub cmbOrigenCatalogo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbOrigenCatalogo.SelectedIndexChanged
        If cmbOrigenCatalogo.SelectedValue > 0 Then
            CargaDatos_OrigenCatalogo()
        End If
    End Sub
    Private Sub CargaDatos_OrigenCatalogo()
        Dim Obj As New DataControl
        Dim result As DataSet = Obj.FillDataSet("EXEC pOrigenDirecciones @cmd=2, @id=" & cmbOrigenCatalogo.SelectedValue)
        For Each row As DataRow In result.Tables(0).Rows
            '
            txtCodigoPostal_or.Text = row("CodigoPostal_or")
            DomicilioOr_cmbCodigos(row("Municipio_or"), row("Colonia_or"), row("Localidad_or"))
            txtCalle_or.Text = row("Calle_or")
            txtNumeroExterior_or.Text = row("NumeroExterior_or")
            txtNumeroInterior_or.Text = row("NumeroInterior_or")
            txtReferencia_or.Text = row("Referencia_or")
            txtRFCRemitente.Text = row("rfc_or")
            txtNombreRemitente.Text = row("nombre_or")
            txtIDUbicacion_or.Text = row("idOR")
            '
        Next
    End Sub
    Protected Sub cmbDestinoCatalogo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDestinoCatalogo.SelectedIndexChanged
        If cmbDestinoCatalogo.SelectedValue > 0 Then
            Call CargaDatos_DestinoCatalogo()
        Else
            Call CleanInputDestino()
        End If
    End Sub
    Private Sub CargaDatos_DestinoCatalogo()
        Dim Obj As New DataControl
        Dim result As DataSet = Obj.FillDataSet("EXEC pMisClientesDirecciones @cmd=2, @id=" & cmbDestinoCatalogo.SelectedValue)
        For Each row As DataRow In result.Tables(0).Rows
            '
            txtCodigoPostal_des.Text = row("CodigoPostal_des")
            txtCalle_des.Text = row("Calle_des")
            txtNumeroExterior_des.Text = row("NumeroExterior_des")
            txtNumeroInterior_des.Text = row("NumeroInterior_des")
            txtReferencia_des.Text = row("Referencia_des")
            txtRFCDestinatario.Text = row("rfc_des")
            txtNombreDestinatario.Text = row("nombre_des")
            txtIDUbicacion_des.Text = row("idDE")
            txtEstado_des.Text = row("estado_nombre")
            txtMunicipio_des.Text = row("municipio_nombre")
            txtColonia_des.Text = row("colonia_nombre")
            txtPais_des.Text = row("Pais_des")
            PanelDireccionDestino.Visible = True
            '
        Next
    End Sub
#End Region

    Protected Sub ckTernium_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ckTernium.CheckedChanged
        If ckTernium.Checked Then
            PanelTernium.Visible = True
            valTernium.ValidationGroup = ""
        Else
            PanelTernium.Visible = False
            valTernium.ValidationGroup = "off"
        End If
        DeternimaEstatusTernium("Checked")
    End Sub
    Public Sub AddendaTernium(ByVal path As String)
        If ckTernium.Checked Then
            Dim xml As New XmlDocument()
            xml.Load(path)

            Dim addenda As XmlNode
            Dim Ternium As XmlElement
            Dim viaje As XmlElement
            addenda = xml.CreateNode(XmlNodeType.Element, "cfdi:Addenda", URI_SAT)
            Ternium = xml.CreateElement("Ternium")
            Ternium.SetAttribute("version", "1.0")
            viaje = xml.CreateElement("viaje")
            viaje.InnerText = txtTernium.Text

            Ternium.AppendChild(viaje)
            addenda.AppendChild(Ternium)
            xml.DocumentElement.AppendChild(addenda)
            xml.Save(path)
        End If
        DeternimaEstatusTernium("Checked")
    End Sub
    Public Sub DeternimaEstatusTernium(ByVal opc As String)
        Dim Obj As New DataControl
        Select Case opc
            Case "Checked"
                If ckTernium.Checked Then
                    If Session("CFD") <> 0 Then
                        Obj.RunSQLQuery("update tblcfd set terniumbit = 1, viaje='" & txtTernium.Text & "' where id=" & Session("CFD"))
                    End If
                Else
                    If Session("CFD") <> 0 Then
                        Obj.RunSQLQuery("update tblcfd set terniumbit = null , viaje=null where id=" & Session("CFD"))
                    End If
                End If
            Case "Comprueba"
                Dim addid = Obj.RunSQLScalarQuery("select isnull(terniumbit,0) from tblcfd where id =" & Session("CFD"))
                If addid <> 0 Then
                    ckTernium.Checked = True
                    PanelTernium.Visible = True
                    txtTernium.Text = Obj.RunSQLScalarQueryString("select isnull(viaje,'') from tblcfd where id =" & Session("CFD"))
                End If
        End Select

    End Sub

    



End Class
