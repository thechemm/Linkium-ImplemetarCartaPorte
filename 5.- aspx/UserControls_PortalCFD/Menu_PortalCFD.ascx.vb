
Partial Class portalcfd_usercontrols_portalcfd_Menu_PortalCFD
    Inherits System.Web.UI.UserControl

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim fecha As DateTime
        lblUsuario.Text = "Usuario en sesión: <strong>" & Session("nombre").ToString & "</strong>"
        lblCertificado.Text = "Certificado Expira el: <strong>" & Session("fechaExpiracionCer").ToString & "</strong>"

        fecha = Session("fechaExpiracionCer").ToString

        If fecha <= Date.Today.AddDays(-30) Then
            lblCertificado.ForeColor = Drawing.Color.Red
        Else
            lblCertificado.ForeColor = Drawing.Color.Green
        End If

        If System.Configuration.ConfigurationManager.AppSettings("usuarios") = 1 And Session("admin") = 0 Then
            '
            '
            '   Permisos para el menu
            '
            If Session("perfilid") <> 1 Then
                RadMenu1.Items(5).Enabled = False
                RadMenu1.Items(6).Enabled = False
                RadMenu1.Items(7).Enabled = False
                RadMenu1.Items(8).Enabled = False
                '
                RadMenu1.Items(5).ToolTip = "Acceso restringido."
                RadMenu1.Items(6).ToolTip = "Acceso restringido."
                RadMenu1.Items(7).ToolTip = "Acceso restringido."
                RadMenu1.Items(8).ToolTip = "Acceso restringido."
            End If
        End If
        '
        '   Permisos por módulos contratados
        '
        If System.Configuration.ConfigurationManager.AppSettings("inventarios") = 0 Then
            'RadMenu1.Items(4).Items(1).Visible = False
        End If
        ''
    End Sub
    Protected Sub FoliosGrid_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles FoliosGrid.NeedDataSource
        Dim ObjData As New DataControl
        FoliosGrid.DataSource = ObjData.FillDataSet("exec pMisFolios @cmd=4")
        ObjData = Nothing
    End Sub
    Private Sub lnkConsultarFolios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConsultarFolios.Click
        RadWindow1.VisibleOnPageLoad = True
    End Sub
End Class
