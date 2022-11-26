Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class FigurasTransporte
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl
            btnAdd1.Text = "Agregar Figura"
            btnGuardar.Text = Resources.Resource.btnSave
            btnCancelar.Text = Resources.Resource.btnCancel
            DomiciliosPais()
            objCat.Catalogo(cmbFiguraTransporte, "select isnull(clave,'') as  clave, isnull(descripcion,'') as nombre  from tblFiguraTransporte", 0)
            objCat = Nothing

        End If
    End Sub
    Private Sub GridList_ItemCommand(ByVal sender As Object, ByVal e As GridCommandEventArgs) Handles GridList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Edit(e.CommandArgument)
            Case "cmdDelete"
                Delete(e.CommandArgument)
        End Select
    End Sub
    Private Sub CargaCatalogos()
        Dim ObjCat As New DataControl
        ObjCat.Catalogo(cmbPais_op, "select isnull(codigo,'') as clave, isnull(descripcion,'') as descripcion from tblPais", 0)
        cmbPais_op.SelectedValue = "MEX"
    End Sub
    Private Sub Edit(ByVal id As Integer)

        panelRegistration.Visible = True
        btnGuardar.Text = "Actualizar"
        InsertOrUpdate.Value = 1
        registroID.Value = id

        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pOperadores @cmd=2, @id=" & id)

        For Each row As DataRow In dt.Tables(0).Rows
            txtRFCOperador.Text = row("RFCOperador")
            txtNumLicencia.Text = row("NumLicencia")
            txtNombreOperador.Text = row("NombreOperador")
            txtCodigoPostal_op.Text = row("CodigoPostal_op")
            DomicilioOp_cmbCodigos(row("Municipio_op"), row("Colonia_op"), row("Localidad_op"))
            txtCalle_op.Text = row("Calle_op")
            txtNumeroExterior_op.Text = row("NumeroExterior_op")
            txtNumeroInterior_op.Text = row("NumeroInterior_op")
            txtReferencia_op.Text = row("Referencia_op")
            cmbFiguraTransporte.SelectedValue = row("tipofigura")
        Next
    End Sub
    Private Sub Delete(ByVal id As Integer)
        Dim Obj As New DataControl
        Obj.RunSQLQuery("EXEC pOperadores @cmd=5, @id=" & id)
        GridList_NeedData("on")
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd1.Click
        clanInput()
        InsertOrUpdate.Value = 0
        registroID.Value = 0
        btnGuardar.Text = "Guardar"
        panelRegistration.Visible = True
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim Obj As New DataControl
        Dim cmd = 0
        If InsertOrUpdate.Value = 0 Then
            cmd = 1
        Else
            cmd = 4
        End If
        Obj.RunSQLQuery("EXEC pOperadores @cmd=" & cmd & ", @RFCOperador='" & txtRFCOperador.Text.ToUpper & _
                                                        "', @NumLicencia='" & txtNumLicencia.Text.ToUpper & _
                                                        "', @NombreOperador='" & txtNombreOperador.Text.ToUpper & _
                                                        "', @Calle_op='" & txtCalle_op.Text & _
                                                        "', @NumeroExterior_op='" & txtNumeroExterior_op.Text & _
                                                        "', @NumeroInterior_op='" & txtNumeroInterior_op.Text & _
                                                        "', @Colonia_op='" & cmbColonia_op.SelectedValue & _
                                                        "', @Localidad_op='" & txtLocalidad_op.Text.ToUpper & _
                                                        "', @Municipio_op='" & cmbMunicipio_op.SelectedValue & _
                                                        "', @Referencia_op='" & txtReferencia_op.Text.ToUpper & _
                                                        "', @Estado_op='" & cmbEstado_op.SelectedValue & _
                                                        "', @Pais_op='" & cmbPais_op.SelectedValue & _
                                                        "', @CodigoPostal_op='" & txtCodigoPostal_op.Text & _
                                                        "', @tipofigura='" & cmbFiguraTransporte.SelectedValue & _
                                                        "', @id=" & registroID.Value)
        panelRegistration.Visible = False

        clanInput()
        GridList_NeedData("on")
    End Sub
    Private Sub GridList_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles GridList.NeedDataSource
        If Not e.IsFromDetailTable Then
            GridList_NeedData("off")
            GridList.MasterTableView.NoMasterRecordsText = "No se encontraron registros"
        End If
    End Sub
    Private Sub GridList_NeedData(ByVal state As String)
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pOperadores @cmd=3")
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
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        InsertOrUpdate.Value = 0
        btnGuardar.Text = "Guardar"
        panelRegistration.Visible = False
        clanInput()
    End Sub

    Private Sub clanInput()
        txtRFCOperador.Text = ""
        txtNumLicencia.Text = ""
        txtNombreOperador.Text = ""
        cmbFiguraTransporte.SelectedValue = 0
        txtCodigoPostal_op.Text = ""
        cmbEstado_op.SelectedValue = 0
        cmbMunicipio_op.SelectedValue = 0
        cmbColonia_op.SelectedValue = 0
        txtCalle_op.Text = ""
        txtNumeroInterior_op.Text = ""
        txtNumeroExterior_op.Text = ""
        txtReferencia_op.Text = ""
    End Sub
    Protected Sub txtCodigoPostal_op_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigoPostal_op.TextChanged
        DomicilioOp_cmbCodigos("0", "0", "0")
    End Sub
    Private Sub DomicilioOp_cmbCodigos(ByVal municipio As String, ByVal colonia As String, ByVal localidad As String)
        Dim dt As New DataTable()
        cmbColonia_op.DataSource = dt
        cmbEstado_op.DataSource = dt
        txtLocalidad_op.DataSource = dt
        cmbMunicipio_op.DataSource = dt
        If txtCodigoPostal_op.Text.Length = 5 Then
            Dim code As String = codePais(txtCodigoPostal_op.Text)
            Dim ObjCat As New DataControl
            'Dim municipio As String = ObjCat.RunSQLScalarQueryString("")
            ObjCat.Catalogo(cmbColonia_op, "select c_Colonia as clave, NombreAsentamiento as nombre from tblColonia where c_CodigoPostal =" & txtCodigoPostal_op.Text, colonia)
            ObjCat.Catalogo(cmbEstado_op, " select top 1 a.clave, a.nombre from tblEstado a inner join tblCodigoPostal b on a.clave = b.clave where b.codigo ='" & txtCodigoPostal_op.Text & "'", code)
            ObjCat.Catalogo(txtLocalidad_op, " select localidad as clave, descripcion as nombre from tblLocalidad where estado ='" & cmbEstado_op.SelectedValue & "'", localidad)
            ObjCat.Catalogo(cmbMunicipio_op, "select ISNULL(b.municipio,'') as clave, ISNULL(b.descripcion,'') as descripcion from tblCodigoPostal a right join tblMunicipio b on a.clave = b.estado and a.c_Municipio = b.municipio where a.codigo = '" & txtCodigoPostal_op.Text & "'", municipio)
            ObjCat = Nothing
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
    Private Sub DomiciliosPais()
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("select isnull(codigo,'') as clave, isnull(descripcion,'') as descripcion from tblPais ")
        cmbPais_op.DataSource = dt
        cmbPais_op.SelectedValue = "MEX"
    End Sub

    Protected Sub cmbFiguraTransporte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiguraTransporte.SelectedIndexChanged
        If cmbFiguraTransporte.SelectedValue = "01" Then
            panelLicencia.Visible = True
        Else
            panelLicencia.Visible = False
        End If
    End Sub
End Class
