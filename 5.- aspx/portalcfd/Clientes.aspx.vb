Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI

Partial Class portalcfd_Clientes
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        LoadDatosCodigoPostal(txtZipCod)

        If Not IsPostBack Then

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblClientsListLegend.Text = Resources.Resource.lblClientsListLegend
            lblClientEditLegend.Text = Resources.Resource.lblClientEditLegend

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            'Dim TelerikRadComboBox As New FillRadComboBox
            'TelerikRadComboBox.FillRadComboBox(cmbState, "EXEC pCatalogos @cmd=1")

            'cmbState.Text = Resources.Resource.cmbEmptyMessage

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblSocialReason.Text = Resources.Resource.lblSocialReason
            lblContact.Text = Resources.Resource.lblContact
            lblContactEmail.Text = Resources.Resource.lblContactEmail
            lblContactPhone.Text = Resources.Resource.lblContactPhone
            lblStreet.Text = Resources.Resource.lblStreet
            lblExtNumber.Text = Resources.Resource.lblExtNumber
            lblIntNumber.Text = Resources.Resource.lblIntNumber
            lblColony.Text = Resources.Resource.lblColony
            lblCountry.Text = Resources.Resource.lblCountry
            lblState.Text = Resources.Resource.lblState
            lblTownship.Text = Resources.Resource.lblTownship
            lblZipCode.Text = Resources.Resource.lblZipCode
            lblRFC.Text = Resources.Resource.lblRFC
            'lblMetodoPago.Text = Resources.Resource.lblMetodoPago
            lblNumCtaPago.Text = Resources.Resource.lblNumCtaPago

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''

            RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator2.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator3.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator4.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator5.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator6.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator10.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator11.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator7.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator8.Text = Resources.Resource.validatorMessage
            RequiredFieldValidator9.Text = Resources.Resource.validatorMessage
            valTipoContribuyente.Text = Resources.Resource.validatorMessage

            RegularExpressionValidator1.Text = Resources.Resource.validatorMessageEmail
            valRFC.Text = Resources.Resource.validatorMessageRfc
            RegularExpressionValidator2.Text = Resources.Resource.validatorMessageRfc

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddClient.Text = Resources.Resource.btnAddClient
            btnSaveClient.Text = Resources.Resource.btnSave
            btnCancel.Text = Resources.Resource.btnCancel
            '
            '
            '
            Dim ObjData As New DataControl
            ObjData.Catalogo(tipoprecioid, "select id, nombre from tblTipoPrecio", 0)
            ObjData.Catalogo(condicionesid, "select id, nombre from tblCondiciones", 0)
            ObjData.Catalogo(tipoContribuyenteid, "select id, nombre from tblTipoContribuyente", 0)
            ObjData.Catalogo(formapagoid, "select id, nombre from tblFormaPago order by id", 0)
            ObjData.Catalogo(metodopagoid, "select codigo, descripcion from tblc_MetodoPago order by codigo", "PUE")
            ObjData.Catalogo(comprobanteid, "EXEC pCatalogos @cmd=8", 0)
            ObjData.Catalogo(fac_paisid, "EXEC pCatalogoPais @cmd=6", 0)
            ObjData.Catalogo(cmbState, "EXEC pCatalogos @cmd=1", 0)
            ObjData = Nothing
            DomiciliosPais()
        End If

    End Sub

    Private Sub LoadDatosCodigoPostal(ByVal autoCompleteBox As RadAutoCompleteBox)
        Dim ObjData As New DataControl
        autoCompleteBox.DataSource = ObtenerCodigoPostal()
        ObjData = Nothing
    End Sub

    Function ObtenerCodigoPostal() As DataSet
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCatalogos @cmd=6, @estadoid='" & cmbState.SelectedValue & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function

#End Region

#Region "Load List Of Clients"

    Function GetClients() As DataSet

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pMisClientes  @cmd=1, @clienteUnionId='" & Session("clienteid") & "', @txtSearch='" & txtSearch.Text & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function

#End Region

#Region "Telerik Grid Clients Loading Events"

    Protected Sub clientslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles clientslist.NeedDataSource

        If Not e.IsFromDetailTable Then

            clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
            clientslist.DataSource = GetClients()

        End If

    End Sub

#End Region

#Region "Telerik Grid Language Modification(Spanish)"

    Protected Sub clientslist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles clientslist.Init

        clientslist.PagerStyle.NextPagesToolTip = "Ver mas"
        clientslist.PagerStyle.NextPageToolTip = "Siguiente"
        clientslist.PagerStyle.PrevPagesToolTip = "Ver más"
        clientslist.PagerStyle.PrevPageToolTip = "Atrás"
        clientslist.PagerStyle.LastPageToolTip = "Última Página"
        clientslist.PagerStyle.FirstPageToolTip = "Primera Página"
        clientslist.PagerStyle.PagerTextFormat = "{4}    Página {0} de {1}, Registros {2} al {3} de {5}"
        clientslist.SortingSettings.SortToolTip = "Ordernar"
        clientslist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        clientslist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As Telerik.Web.UI.GridFilterMenu = clientslist.FilterMenu
        Dim i As Integer = 0

        While i < menu.Items.Count

            If menu.Items(i).Text = "NoFilter" Or menu.Items(i).Text = "Contains" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If

        End While

        Call ModificaIdiomaGrid()

    End Sub

    Private Sub ModificaIdiomaGrid()

        clientslist.GroupingSettings.CaseSensitive = False

        Dim Menu As Telerik.Web.UI.GridFilterMenu = clientslist.FilterMenu
        Dim item As Telerik.Web.UI.RadMenuItem

        For Each item In Menu.Items

            ''''''''''''''''''''''''''''''''''''''''''''''
            'Change The Text For The StartsWith Menu Item'
            ''''''''''''''''''''''''''''''''''''''''''''''

            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "Contains" Then
                item.Text = "Contiene"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If

        Next

    End Sub

#End Region

#Region "Telerik Grid Clients Editing & Deleting Events"

    Protected Sub clientslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles clientslist.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Clients" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ClientsDeleteConfirmationMessage & "');")

            End If

        End If

    End Sub

    Protected Sub clientslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles clientslist.ItemCommand

        Select Case e.CommandName

            Case "cmdEdit"
                EditClient(e.CommandArgument)

            Case "cmdDelete"
                DeleteClient(e.CommandArgument)

        End Select

    End Sub

    Private Sub DeleteClient(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisClientes @cmd='3', @clienteId ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelClientRegistration.Visible = False

            clientslist.DataSource = GetClients()
            clientslist.DataBind()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

    Private Sub EditClient(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisClientes @cmd='2', @clienteId='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtSocialReason.Text = rs("razonsocial")
                txtContact.Text = rs("contacto")
                txtContactEmail.Text = rs("email_contacto")
                txtContactPhone.Text = rs("telefono_contacto")
                txtStreet.Text = rs("fac_calle")
                txtExtNumber.Text = rs("fac_num_ext")
                txtIntNumber.Text = rs("fac_num_int")
                txtColony.Text = rs("fac_colonia")
                txtTownship.Text = rs("fac_municipio")
                txtZipCode.Text = rs("fac_cp")
                txtRFC.Text = rs("rfc")
                txtNumCtaPago.Text = rs("numctapago")
                '
                Dim ObjData As New DataControl
                ObjData.Catalogo(tipoprecioid, "select id, nombre from tblTipoPrecio", rs("tipoprecioid"))
                ObjData.Catalogo(condicionesid, "select id, nombre from tblCondiciones", rs("condicionesid"))
                ObjData.Catalogo(tipoContribuyenteid, "select id, nombre from tblTipoContribuyente", rs("tipoContribuyenteid"))
                ObjData.Catalogo(formapagoid, "select id, nombre from tblFormaPago", rs("formapagoid"))
                ObjData.Catalogo(metodopagoid, "select codigo, descripcion from tblc_MetodoPago order by codigo", rs("metodopagoid"))
                ObjData.Catalogo(comprobanteid, "EXEC pCatalogos @cmd=8", rs("usocfdi"))
                ObjData.Catalogo(fac_paisid, "EXEC pCatalogoPais @cmd=6", rs("fac_paisid"))
                ObjData.Catalogo(cmbState, "EXEC pCatalogos @cmd=1", rs("fac_estadoid"))
                ObjData = Nothing

                txtCuentaPredial.Text = rs("cuentapredial")

                If fac_paisid.SelectedValue <> 1 Then
                    txtStates.Text = rs("fac_estado")
                    cmbState.SelectedValue = 0
                    ValidarPais()
                Else
                    txtZipCod.Entries.Add(New AutoCompleteBoxEntry(rs("fac_cp"), rs("fac_cp")))
                    LoadDatosCodigoPostal(txtZipCod)
                    ValidarPais()
                End If

                panelClientRegistration.Visible = True

                InsertOrUpdate.Value = 1
                ClientsID.Value = id

                RadTabStrip1.Tabs(0).Enabled = True
                RadTabStrip1.Tabs(1).Enabled = True
                RadTabStrip1.Tabs(2).Enabled = True

                lblNombreValue.Text = rs("contacto")
                lblClienteValue.Text = rs("razonsocial")
                lblEmailValue.Text = rs("email_contacto")
                panel1.Visible = True


                cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
                cuentasList.DataSource = ObtenerCuentas()
                cuentasList.DataBind()
                GridList_NeedData("on")
            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

#End Region

#Region "Telerik Grid Clients Column Names (From Resource File)"

    Protected Sub clientslist_ItemCreated(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles clientslist.ItemCreated

        If TypeOf e.Item Is GridHeaderItem Then

            Dim header As GridHeaderItem = CType(e.Item, GridHeaderItem)

            If e.Item.OwnerTableView.Name = "Clients" Then

                header("razonsocial").Text = Resources.Resource.gridColumnNameSocialReason
                header("contacto").Text = Resources.Resource.gridColumnNameContact
                header("telefono_contacto").Text = Resources.Resource.gridColumnNameContactPhone
                header("rfc").Text = Resources.Resource.gridColumnNameRFC

            End If

        End If

    End Sub

#End Region

#Region "Display Client Data Panel"

    Protected Sub btnAddClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddClient.Click

        InsertOrUpdate.Value = 0

        txtSocialReason.Text = ""
        txtContact.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtStreet.Text = ""
        txtExtNumber.Text = ""
        txtIntNumber.Text = ""
        txtColony.Text = ""
        cmbState.SelectedValue = 0
        fac_paisid.SelectedValue = 0

        'txtCountry.Text = ""
        'txtStates.Text = ""
        'cmbState.Text = Resources.Resource.cmbEmptyMessage
        'cmbState.SelectedValue = 0
        txtTownship.Text = ""
        txtZipCode.Text = ""
        txtRFC.Text = ""
        comprobanteid.SelectedValue = 0
        condicionesid.SelectedValue = 0
        formapagoid.SelectedValue = 0
        tipoContribuyenteid.SelectedValue = 0
        metodopagoid.SelectedValue = 0
        tipoprecioid.SelectedValue = 0
        txtCuentaPredial.Text = ""
        txtNumCtaPago.Text = ""


        LoadDatosCodigoPostal(txtZipCod)
        txtZipCod.Entries.Clear()

        RadTabStrip1.Tabs(0).Enabled = True
        RadTabStrip1.Tabs(1).Enabled = False
        RadTabStrip1.Tabs(2).Enabled = False
        panelClientRegistration.Visible = True

    End Sub

#End Region

#Region "Save Client"

    Protected Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveClient.Click

        Dim fac_cp As String = ""

        If fac_paisid.SelectedValue <> 1 Then
            fac_cp = txtZipCode.Text
        Else
            fac_cp = Replace(txtZipCod.Text.Trim, ";", "")
        End If



        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            If InsertOrUpdate.Value = 0 Then

                Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=4, @clienteUnionId='" & Session("clienteid").ToString & "', @razonsocial='" & txtSocialReason.Text & "', @contacto='" & txtContact.Text & "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_estadoid='" & cmbState.SelectedValue & "', @fac_estado='" & txtStates.Text & "', @fac_paisid='" & fac_paisid.SelectedValue & "', @fac_cp='" & fac_cp.ToString & "', @fac_rfc='" & txtRFC.Text & "', @tipoprecioid='" & tipoprecioid.SelectedValue.ToString & "', @condicionesid='" & condicionesid.SelectedValue.ToString & "', @tipocontribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "',@metodopagoid='" & metodopagoid.SelectedValue & "', @usocfdi='" & comprobanteid.SelectedValue & "',@cuentapredial='" & txtCuentaPredial.Text & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panel1.Visible = False
                panelClientRegistration.Visible = False

                clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                clientslist.DataSource = GetClients()
                clientslist.DataBind()

                conn.Close()
                conn.Dispose()

            Else

                Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=5, @clienteid='" & ClientsID.Value & "', @razonsocial='" & txtSocialReason.Text & "', @contacto='" & txtContact.Text & "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_estadoid='" & cmbState.SelectedValue & "', @fac_estado='" & txtStates.Text & "', @fac_paisid='" & fac_paisid.SelectedValue & "', @fac_cp='" & fac_cp.ToString & "', @fac_rfc='" & txtRFC.Text & "', @tipoprecioid='" & tipoprecioid.SelectedValue.ToString & "', @condicionesid='" & condicionesid.SelectedValue.ToString & "', @tipocontribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @metodopagoid='" & metodopagoid.SelectedValue & "', @usocfdi='" & comprobanteid.SelectedValue & "',@cuentapredial='" & txtCuentaPredial.Text & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                panel1.Visible = False
                panelClientRegistration.Visible = False

                clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                clientslist.DataSource = GetClients()
                clientslist.DataBind()

                conn.Close()
                conn.Dispose()

            End If

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

    End Sub

#End Region

#Region "Cancel Client (Save/Edit)"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        InsertOrUpdate.Value = 0

        txtSocialReason.Text = ""
        txtContact.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtStreet.Text = ""
        txtExtNumber.Text = ""
        txtIntNumber.Text = ""
        txtColony.Text = ""
        cmbState.SelectedValue = 0
        fac_paisid.SelectedValue = 0
        'txtCountry.Text = ""
        'txtStates.Text = ""
        'cmbState.Text = Resources.Resource.cmbEmptyMessage
        'cmbState.SelectedValue = 0
        txtTownship.Text = ""
        txtZipCode.Text = ""
        txtRFC.Text = ""

        panel1.Visible = False
        panelClientRegistration.Visible = False

    End Sub

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
        clientslist.DataSource = GetClients()
        clientslist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click
        txtSearch.Text = ""
        clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
        clientslist.DataSource = GetClients()
        clientslist.DataBind()
    End Sub
    'fac_paisid
    Protected Sub paisid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles fac_paisid.SelectedIndexChanged
        ValidarPais()
    End Sub
    Public Sub ValidarPais()
        If fac_paisid.SelectedValue <> 1 Then
            cmbState.SelectedValue = 0
            cmbState.Visible = False
            RequiredFieldValidator6.Enabled = False
            txtStates.Visible = True
            txtStates.Enabled = True
            RequiredFieldValidator10.Enabled = True
            '
            txtZipCode.Visible = True
            RequiredFieldValidator5.Enabled = True

            RequiredFieldValidator11.Enabled = False
            txtZipCod.Visible = False
        Else
            'Mexico 
            'cmbState.SelectedValue = 0
            cmbState.Visible = True
            RequiredFieldValidator6.Enabled = True

            txtStates.Visible = False
            txtStates.Enabled = False
            RequiredFieldValidator10.Enabled = False
            '
            txtZipCode.Visible = False
            RequiredFieldValidator5.Enabled = False

            RequiredFieldValidator11.Enabled = True
            txtZipCod.Visible = True
        End If
    End Sub

    Protected Sub cmbState_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbState.SelectedIndexChanged
        LoadDatosCodigoPostal(txtZipCod)
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Dim objData As New DataControl
            Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
            Dim sql As String = ""
            Dim IdCuentas As Integer = 0

            If CuentaID.Value = 0 Then
                IdCuentas = objData.RunSQLScalarQuery("EXEC pCatalogoCuentas @cmd=1, @banconacional='" & txtBanco.Text & "', @bancoextranjero='" & txtBancoExtr.Text & "',@rfc='" & txtRFCBAK.Text & "', @numctapago='" & txtCuenta.Text & "', @predeterminadoBit='" & chkPredeterminado.Checked & "', @clienteid='" & ClientsID.Value & "'")
                clearItems()
            Else
                objData.RunSQLScalarQuery("EXEC pCatalogoCuentas @cmd=5, @banconacional='" & txtBanco.Text & "', @bancoextranjero='" & txtBancoExtr.Text & "', @rfc='" & txtRFCBAK.Text & "', @numctapago='" & txtCuenta.Text & "',@predeterminadoBit='" & chkPredeterminado.Checked & "', @clienteid='" & ClientsID.Value & "', @id='" & CuentaID.Value & "'")
                clearItems()
                objData = Nothing
            End If

            cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            cuentasList.DataSource = ObtenerCuentas()
            cuentasList.DataBind()
        End If
    End Sub
    Sub clearItems()
        txtBanco.Text = ""
        txtBancoExtr.Text = ""
        txtCuenta.Text = ""
        txtRFCBAK.Text = ""
        CuentaID.Value = 0
    End Sub
    Function ObtenerCuentas() As DataSet

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim qry As String = "EXEC pCatalogoCuentas @cmd=6, @clienteid='" & ClientsID.Value & "'"

        Dim cmd As New SqlDataAdapter(qry, conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()

        Catch ex As Exception

            Response.Write(ex.ToString)

        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function
    Private Sub DeleteCuenta(ByVal id As Integer)

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoCuentas @cmd=4, @id='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            cuentasList.DataSource = ObtenerCuentas()
            cuentasList.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub
    Private Sub CargarCuenta()


        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoCuentas @cmd=3, @id='" & CuentaID.Value & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtBanco.Text = rs("banconacional")
                txtBancoExtr.Text = rs("bancoextranjero")
                txtCuenta.Text = rs("numctapago")
                txtRFCBAK.Text = rs("rfc")
                chkPredeterminado.Checked = rs("predeterminadoBit")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub
    Protected Sub cuentasList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cuentasList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                CuentaID.Value = e.CommandArgument
                Call CargarCuenta()
                btnCancelar.Visible = True
            Case "cmdDelete"
                DeleteCuenta(e.CommandArgument)
        End Select
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        clearItems()
        btnCancelar.Visible = False
    End Sub
    Protected Sub cuentasList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cuentasList.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim imgPredeterminado As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgPredeterminado"), System.Web.UI.WebControls.Image)
                imgPredeterminado.Visible = e.Item.DataItem("predeterminadoBit")
        End Select
    End Sub
#Region "Direcciones"
    Protected Sub GridList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridList.NeedDataSource
        GridList_NeedData("off")
    End Sub
    Private Sub GridList_NeedData(ByVal state As String)
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pMisClientesDirecciones @cmd=3, @clienteid='" & ClientsID.Value & "'")
        GridList.DataSource = dt
        If state = "on" Then
            GridList.DataBind()
        End If
        GridList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
    End Sub
    Private Sub Paislist_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles GridList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
            lnkdel.Attributes.Add("onclick", "return confirm ('Va a borrar este Registro. ¿Desea continuar?');")
        End If
    End Sub
    Private Sub btnSaveItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveItem.Click
        Dim Obj As New DataControl
        Dim cmd = 0
        Dim id As Integer = 0
        If InsertOrUpdateItem.Value = 0 Then
            cmd = 1
        Else
            cmd = 4
        End If
        Try
            id = registroID.Value
        Catch ex As Exception
            id = 0
        End Try
        Obj.RunSQLQuery("EXEC pMisClientesDirecciones @cmd=" & cmd & ", @nombre='" & txtNombre.Text.ToUpper & _
                                                        "', @rfc_des='" & txtRFCDestinatario.Text.ToUpper & _
                                                        "', @nombre_des='" & txtNombreDestinatario.Text.ToUpper & _
                                                        "', @idDE='" & txtIDUbicacion_des.Text.ToUpper & _
                                                        "', @clienteid='" & ClientsID.Value & _
                                                        "', @Calle_des='" & txtCalle_des.Text & _
                                                        "', @NumeroExterior_des='" & txtNumeroExterior_des.Text & _
                                                        "', @NumeroInterior_des='" & txtNumeroInterior_des.Text & _
                                                        "', @Colonia_des='" & cmbColonia_des.SelectedValue & _
                                                        "', @Localidad_des='" & txtLocalidad_des.Text.ToUpper & _
                                                        "', @Municipio_des='" & cmbMunicipio_des.SelectedValue & _
                                                        "', @Referencia_des='" & txtReferencia_des.Text.ToUpper & _
                                                        "', @Estado_des='" & cmbEstado_des.SelectedValue & _
                                                        "', @Pais_des='" & cmbPais_des.SelectedValue & _
                                                        "', @CodigoPostal_des='" & txtCodigoPostal_des.Text & _
                                                        "', @id=" & id)
        panelRegistration.Visible = False

        clanInput()
        GridList_NeedData("on")
    End Sub
    Private Sub clanInput()
        txtNombre.Text = ""
        cmbEstado_des.SelectedValue = 0
        cmbMunicipio_des.SelectedValue = 0
        cmbMunicipio_des.SelectedValue = 0
        cmbColonia_des.SelectedValue = 0
        txtCodigoPostal_des.Text = ""
        txtCalle_des.Text = ""
        txtNumeroExterior_des.Text = ""
        txtNumeroInterior_des.Text = ""
        txtReferencia_des.Text = ""
        lblLegend.Text = "Nueva dirección"
        panelList.Visible = True
        InsertOrUpdateItem.Value = 0
        txtRFCDestinatario.Text = ""
        txtNombreDestinatario.Text = ""
        txtIDUbicacion_des.Text = "DE"
    End Sub
    Protected Sub txtCodigoPostal_des_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigoPostal_des.TextChanged
        DomicilioOr_cmbCodigos("0", "0", "0")
    End Sub
    Private Sub DomicilioOr_cmbCodigos(ByVal municipio As String, ByVal colonia As String, ByVal localidad As String)
        If txtCodigoPostal_des.Text.Length = 5 Then
            Dim code As String = codePais(txtCodigoPostal_des.Text)
            Dim ObjCat As New DataControl
            ObjCat.Catalogo(cmbColonia_des, "select c_Colonia as clave, NombreAsentamiento as nombre from tblColonia where c_CodigoPostal =" & txtCodigoPostal_des.Text, colonia)
            ObjCat.Catalogo(cmbEstado_des, " select top 1 a.clave, a.nombre from tblEstado a inner join tblCodigoPostal b on a.clave = b.clave where b.codigo ='" & txtCodigoPostal_des.Text & "'", code)
            ObjCat.Catalogo(txtLocalidad_des, " select localidad as clave, descripcion as nombre from tblLocalidad where estado ='" & code & "'", localidad)
            ObjCat.Catalogo(cmbMunicipio_des, "select ISNULL(b.municipio,'') as clave, ISNULL(b.descripcion,'') as descripcion from tblCodigoPostal a right join tblMunicipio b on a.clave = b.estado and a.c_Municipio = b.municipio where a.codigo = '" & txtCodigoPostal_des.Text & "'", municipio)
            ObjCat = Nothing
        Else
            Dim dt As New DataTable()
            cmbColonia_des.DataSource = dt
            cmbEstado_des.DataSource = dt
            txtLocalidad_des.DataSource = dt
            cmbMunicipio_des.DataSource = dt
        End If
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
    Private Sub btnCancelItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelItem.Click
        InsertOrUpdate.Value = 0
        btnGuardar.Text = "Guardar"
        panelRegistration.Visible = False
        clanInput()
    End Sub
    Private Sub GridList_ItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles GridList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Edit(e.CommandArgument)
            Case "cmdDelete"
                Delete(e.CommandArgument)
        End Select
    End Sub
    Private Sub Edit(ByVal id As Integer)
        panelList.Visible = False
        panelRegistration.Visible = True
        lblLegend.Text = "Edita dirección"
        btnGuardar.Text = "Actualizar"
        InsertOrUpdateItem.Value = 1
        registroID.Value = id

        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pMisClientesDirecciones @cmd=2, @id=" & id)

        For Each row As DataRow In dt.Tables(0).Rows
            txtNombre.Text = row("nombre")
            txtCodigoPostal_des.Text = row("CodigoPostal_des")
            DomicilioOr_cmbCodigos(row("Municipio_des"), row("Colonia_des"), row("Localidad_des"))
            txtCalle_des.Text = row("Calle_des")
            txtNumeroExterior_des.Text = row("NumeroExterior_des")
            txtNumeroInterior_des.Text = row("NumeroInterior_des")
            txtReferencia_des.Text = row("Referencia_des")
            txtRFCDestinatario.Text = row("rfc_des")
            txtNombreDestinatario.Text = row("nombre_des")
            txtIDUbicacion_des.Text = row("idDE")
        Next
    End Sub
    Private Sub Delete(ByVal id As Integer)
        Dim Obj As New DataControl
        Obj.RunSQLQuery("EXEC pMisClientesDirecciones @cmd=5, @id=" & id)
        GridList_NeedData("on")
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd1.Click
        clanInput()
        InsertOrUpdateItem.Value = 0
        registroID.Value = 0
        btnGuardar.Text = "Guardar"
        panelRegistration.Visible = True
        panelList.Visible = False
    End Sub
    Private Sub DomiciliosPais()
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("select isnull(codigo,'') as clave, isnull(descripcion,'') as descripcion from tblPais ")
        cmbPais_des.DataSource = dt
        cmbPais_des.SelectedValue = "MEX"
    End Sub
#End Region
End Class
