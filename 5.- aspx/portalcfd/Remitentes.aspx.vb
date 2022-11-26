Imports System.Data
Imports Telerik.Web.UI
Imports System.Data.SqlClient

Partial Class Remitentes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DomiciliosPais()
        End If
    End Sub
    Protected Sub GridList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles GridList.NeedDataSource
        GridList_NeedData("off")
    End Sub
    Private Sub GridList_NeedData(ByVal state As String)
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pOrigenDirecciones @cmd=3")
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
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim Obj As New DataControl
        Dim cmd = 0
        Dim id As Integer = 0
        If InsertOrUpdate.Value = 0 Then
            cmd = 1
        Else
            cmd = 4
        End If
        Try
            id = registroID.Value
        Catch ex As Exception
            id = 0
        End Try
        Obj.RunSQLQuery("EXEC pOrigenDirecciones @cmd=" & cmd & ", @nombre='" & txtNombre.Text.ToUpper & _
                                                        "', @rfc_or='" & txtRFCRemitente.Text.ToUpper & _
                                                        "', @nombre_or='" & txtNombreRemitente.Text.ToUpper & _
                                                        "', @idOR='" & txtIDUbicacion_or.Text.ToUpper & _
                                                        "', @Calle_or='" & txtCalle_or.Text & _
                                                        "', @NumeroExterior_or='" & txtNumeroExterior_or.Text & _
                                                        "', @NumeroInterior_or='" & txtNumeroInterior_or.Text & _
                                                        "', @Colonia_or='" & cmbColonia_or.SelectedValue & _
                                                        "', @Localidad_or='" & txtLocalidad_or.Text.ToUpper & _
                                                        "', @Municipio_or='" & cmbMunicipio_or.SelectedValue & _
                                                        "', @Referencia_or='" & txtReferencia_or.Text.ToUpper & _
                                                        "', @Estado_or='" & cmbEstado_or.SelectedValue & _
                                                        "', @Pais_or='" & cmbPais_or.SelectedValue & _
                                                        "', @CodigoPostal_or='" & txtCodigoPostal_or.Text & _
                                                        "', @id=" & id)
        panelRegistration.Visible = False

        clanInput()
        GridList_NeedData("on")
    End Sub
    Private Sub clanInput()
        txtNombre.Text = ""
        cmbEstado_or.SelectedValue = 0
        cmbMunicipio_or.SelectedValue = 0
        cmbMunicipio_or.SelectedValue = 0
        cmbColonia_or.SelectedValue = 0
        txtCodigoPostal_or.Text = ""
        txtCalle_or.Text = ""
        txtNumeroExterior_or.Text = ""
        txtNumeroInterior_or.Text = ""
        txtReferencia_or.Text = ""
        lblLegend.Text = "Nueva dirección"
        panelList.visible = True

        txtRFCRemitente.Text = ""
        txtNombreRemitente.Text = ""
        txtIDUbicacion_or.Text = "OR"
    End Sub
    Protected Sub txtCodigoPostal_or_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigoPostal_or.TextChanged
        DomicilioOr_cmbCodigos("0", "0", "0")
    End Sub
    Private Sub DomicilioOr_cmbCodigos(ByVal municipio As String, ByVal colonia As String, ByVal localidad As String)
        If txtCodigoPostal_or.Text.Length = 5 Then
            Dim code As String = codePais(txtCodigoPostal_or.Text)
            Dim ObjCat As New DataControl
            ObjCat.Catalogo(cmbColonia_or, "select c_Colonia as clave, NombreAsentamiento as nombre from tblColonia where c_CodigoPostal =" & txtCodigoPostal_or.Text, colonia)
            ObjCat.Catalogo(cmbEstado_or, " select top 1 a.clave, a.nombre from tblEstado a inner join tblCodigoPostal b on a.clave = b.clave where b.codigo ='" & txtCodigoPostal_or.Text & "'", code)
            ObjCat.Catalogo(txtLocalidad_or, " select localidad as clave, descripcion as nombre from tblLocalidad where estado ='" & code & "'", localidad)
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
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
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
        panelList.visible = False
        panelRegistration.Visible = True
        lblLegend.Text = "Edita dirección"
        btnGuardar.Text = "Actualizar"
        InsertOrUpdate.Value = 1
        registroID.Value = id

        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("EXEC pOrigenDirecciones @cmd=2, @id=" & id)

        For Each row As DataRow In dt.Tables(0).Rows
            txtNombre.Text = row("nombre")
            txtCodigoPostal_or.Text = row("CodigoPostal_or")
            DomicilioOr_cmbCodigos(row("Municipio_or"), row("Colonia_or"), row("Localidad_or"))
            txtCalle_or.Text = row("Calle_or")
            txtNumeroExterior_or.Text = row("NumeroExterior_or")
            txtNumeroInterior_or.Text = row("NumeroInterior_or")
            txtReferencia_or.Text = row("Referencia_or")
            txtRFCRemitente.Text = row("rfc_or")
            txtNombreRemitente.Text = row("nombre_or")
            txtIDUbicacion_or.Text = row("idOR")
        Next
    End Sub
    Private Sub Delete(ByVal id As Integer)
        Dim Obj As New DataControl
        Obj.RunSQLQuery("EXEC pOrigenDirecciones @cmd=5, @id=" & id)
        GridList_NeedData("on")
    End Sub
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd1.Click
        clanInput()
        InsertOrUpdate.Value = 0
        registroID.Value = 0
        btnGuardar.Text = "Guardar"
        panelRegistration.Visible = True
        panelList.Visible = False
    End Sub
    Private Sub DomiciliosPais()
        Dim Obj As New DataControl
        Dim dt As DataSet = Obj.FillDataSet("select isnull(codigo,'') as clave, isnull(descripcion,'') as descripcion from tblPais ")
        cmbPais_or.DataSource = dt
        cmbPais_or.SelectedValue = "MEX"
    End Sub
End Class
