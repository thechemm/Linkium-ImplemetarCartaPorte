Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class portalcfd_Autotransportes
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then
            Dim objCat As New DataControl

            objCat.Catalogo(cmbPermSCT, "select clave, descripcion from tblTipoPermiso where claveTransporte = '01'", 0)
            objCat.Catalogo(cmbConfigVehicular, "select clave, CONCAT(clave, ' - ', descripcion) as descripcion from tblConfigAutotransporte ", 0)
            objCat.Catalogo(cmbSubTipoRem, "select clave, descripcion from tblSubTipoRem", 0)
            btnAdd1.Text = "Agregar Autotrasporte"
            btnGuardar.Text = Resources.Resource.btnSave
            btnCancelar.Text = Resources.Resource.btnCancel
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
    Private Sub Edit(ByVal id As Integer)

        panelRegistration.Visible = True
        btnGuardar.Text = "Actualizar"
        InsertOrUpdate.Value = 1
        registroID.Value = id

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
        Next
        verificaRemolque()
    End Sub
    Private Sub Delete(ByVal id As Integer)
        Dim Obj As New DataControl
        Obj.RunSQLQuery("EXEC pAutotransportes @cmd=5, @id=" & id)
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
        Dim cmd As Long = 0
        If InsertOrUpdate.Value = 0 Then
            cmd = 1
        Else
            cmd = 4
        End If
        Dim sqlQuery As String = "EXEC pAutotransportes @cmd=" & cmd & ", @PermSCTId='" & cmbPermSCT.SelectedValue & _
                            "', @NumPermisoSCT='" & txtNumPermisoSCT.Text & _
                            "', @NombreAseg='" & txtNombreAseg.Text.ToUpper & _
                            "', @NumPolizaSeguro='" & txtNumPolizaSeguro.Text.ToUpper & _
                            "', @AseguraMedAmbiente='" & txtAseguraMedAmbiente.Text.ToUpper & _
                            "', @PolizaMedAmbiente='" & txtPolizaMedAmbiente.Text.ToUpper & _
                            "', @ConfigVehicularId='" & cmbConfigVehicular.SelectedValue & _
                            "', @PlacaVM='" & txtPlacaVM.Text.ToUpper & _
                            "', @AnioModeloVM='" & txtAnioModeloVM.Text & _
                            "', @id='" & registroID.Value & "'"
        Obj.RunSQLQuery(sqlQuery)
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
        Dim dt As DataSet = Obj.FillDataSet("EXEC pAutotransportes @cmd=3")
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
        cmbPermSCT.SelectedValue = 0
        txtNumPermisoSCT.Text = ""
        txtNombreAseg.Text = ""
        txtNumPolizaSeguro.Text = ""
        cmbConfigVehicular.SelectedValue = 0
        txtPlacaVM.Text = ""
        txtAnioModeloVM.Text = ""
        txtAseguraMedAmbiente.Text = ""
        txtPolizaMedAmbiente.Text = ""
    End Sub

    Protected Sub cmbConfigVehicular_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbConfigVehicular.SelectedIndexChanged
        verificaRemolque()
    End Sub
    Protected Sub remolqueslist_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles remolqueslist.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                DeleteRemolque(e.CommandArgument)
                CleanInputRemolque()
        End Select
    End Sub
    Private Sub DeleteRemolque(ByVal Remolqueid As Long)
        Dim Obj As New DataControl
        Dim cmd As String = "EXEC pAutotransportesRemolques @cmd=5, @id =" & Remolqueid
        Obj.RunSQLQuery(cmd)
        remolqueslist_NeedData("on")
    End Sub
    Protected Sub remolqueslist_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles remolqueslist.NeedDataSource
        remolqueslist_NeedData("off")
    End Sub
    Private Sub remolqueslist_NeedData(ByVal state As String)
        Dim Obj As New DataControl
        Dim result As DataSet
        Dim cmd As String = "EXEC pAutotransportesRemolques @cmd=3, @autotransporteid=" & registroID.Value
        result = Obj.FillDataSet(cmd)
        If result.Tables(0).Rows.Count < 1 Then
            Dim dt As New DataTable
            remolqueslist.DataSource = dt
        Else
            remolqueslist.DataSource = Obj.FillDataSet(cmd)
        End If
        If state = "on" Then
            remolqueslist.DataBind()
        End If
    End Sub

    Protected Sub btnAddRemolque_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRemolque.Click
        If cmbSubTipoRem.SelectedValue <> "0" Then
            Dim OBJ As New DataControl
            OBJ.RunSQLQuery("EXEC pAutotransportesRemolques @cmd=1, @autotransporteid='" & registroID.Value & _
                                                                "', @placa='" & txtPlaca.Text & _
                                                                "', @descripcion='" & cmbSubTipoRem.SelectedItem.Text & _
                                                                "', @SubTipoRem=" & cmbSubTipoRem.SelectedValue)
            remolqueslist_NeedData("on")
            Call CleanInputRemolque()
        End If
        CleanInputRemolque()
    End Sub
    Private Sub verificaRemolque()
        If cmbConfigVehicular.SelectedValue <> "0" Then
            Dim remolque As String
            Dim Obj As New DataControl
            remolque = Obj.RunSQLScalarQueryString("EXEC pAutotransportes @cmd=7, @ConfigVehicularId='" & cmbConfigVehicular.SelectedValue & "'")
            If remolque = "1" Then
                panelRemolque.Visible = True
            ElseIf remolque = "0" Then
                panelRemolque.Visible = False
            Else
                panelRemolque.Visible = True

            End If
        End If
        remolqueslist_NeedData("on")
    End Sub
    Private Sub CleanInputRemolque()
        cmbSubTipoRem.SelectedValue = 0
        txtPlaca.Text = ""
        If remolqueslist.Items.Count = 2 Then
            btnAddRemolque.Enabled = False
        Else
            btnAddRemolque.Enabled = True
        End If
    End Sub

End Class
