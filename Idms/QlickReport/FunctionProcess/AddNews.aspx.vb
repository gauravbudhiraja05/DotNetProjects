Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.DBNull
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlTextArea

Partial Class FunctionProcess_AddNews
    Inherits System.Web.UI.Page
    Dim strcon As String = AppSettings("connectionString")
    Dim con As SqlConnection = New SqlConnection(strcon)
    Dim cmd1, cmd2, cmd3 As SqlCommand
    Dim dr As SqlDataReader
    Dim level1, level2, level3 As String
    Dim var1, var2, var3 As Integer
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.Open()
        errdept.Visible = False
        errclient.Visible = False
        errlob.Visible = False
        If dept.Text = "" Then
            errdept.Visible = True
            errdept.Text = "Should Not Be Blank"
        ElseIf client.Text = "" Then
            errclient.Visible = True
            errclient.Text = "Should Not Be Blank"
        ElseIf lob.Text = "" Then
            errlob.Visible = True
            errlob.Text = "Should Not Be Blank"
        Else
            level1 = dept.Text
            level2 = client.Text
            level3 = lob.Text

            cmd1 = New SqlCommand("select Max(MenuID) from nlvl_menu", con)
            var1 = Convert.ToInt32(cmd1.ExecuteScalar()) + 1
            Dim com11 As New SqlCommand("Insert_nlvl_menu_fp", con)
            com11.CommandType = CommandType.StoredProcedure
            With com11.Parameters
                com11.Parameters.AddWithValue("MenuID", var1)
                com11.Parameters.AddWithValue("CreatedBy", Session("userid"))
                com11.Parameters.AddWithValue("CreatedDate", FormatDateTime(System.DateTime.Now, DateFormat.ShortDate))
            End With
            com11.ExecuteNonQuery()
            com11.Dispose()
            Dim com1 As New SqlCommand("Insert_Menu", con)
            com1.CommandType = CommandType.StoredProcedure
            With com1.Parameters
                com1.Parameters.AddWithValue("@MenuID", var1)
                com1.Parameters.AddWithValue("@ParentID", 17)
                com1.Parameters.AddWithValue("@MenuDescription", level1)
                com1.Parameters.AddWithValue("@URLLink", "/FunctionProcess/IdmsDepartment.aspx")
                com1.Parameters.AddWithValue("@ActiveStatus", "A")
                com1.Parameters.AddWithValue("@Level", 1)
                com1.Parameters.AddWithValue("@LOB", 0)
                com1.Parameters.AddWithValue("@orderby", "2.00")
                com1.Parameters.AddWithValue("@limit", "OS")
            End With
            com1.ExecuteNonQuery()
            com1.Dispose()
            Dim cmdins1 As New SqlCommand("insert_NLVL_MENU_Rights", con)
            cmdins1.CommandType = CommandType.StoredProcedure
            With cmdins1.Parameters
                .AddWithValue("@LOB", "0")
                .AddWithValue("@MenuId", var1)
                .AddWithValue("@UserType", "3")
                .AddWithValue("@Access", "")
                .AddWithValue("@Currdate", System.DateTime.Now)
                .AddWithValue("@AssignBy", Session("userid"))
                .AddWithValue("@parentid", "17")
                .AddWithValue("@userid", Session("userid"))
            End With
            cmdins1.ExecuteNonQuery()
            cmdins1.Dispose()


            cmd2 = New SqlCommand("select Max(MenuID) from nlvl_menu", con)
            var2 = Convert.ToInt32(cmd2.ExecuteScalar()) + 1
            Dim com12 As New SqlCommand("Insert_nlvl_menu_fp", con)
            com12.CommandType = CommandType.StoredProcedure
            With com12.Parameters
                com12.Parameters.AddWithValue("MenuID", var2)
                com12.Parameters.AddWithValue("CreatedBy", Session("userid"))
                com12.Parameters.AddWithValue("CreatedDate", FormatDateTime(System.DateTime.Now, DateFormat.ShortDate))
            End With
            com12.ExecuteNonQuery()
            com12.Dispose()
            Dim com2 As New SqlCommand("Insert_Menu", con)
            com2.CommandType = CommandType.StoredProcedure
            With com2.Parameters
                com2.Parameters.AddWithValue("@MenuID", var2)
                com2.Parameters.AddWithValue("@ParentID", 17)
                com2.Parameters.AddWithValue("@MenuDescription", level2)
                com2.Parameters.AddWithValue("@URLLink", "/FunctionProcess/IdmsClient.aspx")
                com2.Parameters.AddWithValue("@ActiveStatus", "A")
                com2.Parameters.AddWithValue("@Level", 1)
                com2.Parameters.AddWithValue("@LOB", 0)
                com2.Parameters.AddWithValue("@orderby", "3.00")
                com2.Parameters.AddWithValue("@limit", "OS")
            End With
            com2.ExecuteNonQuery()
            com2.Dispose()
            Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", con)
            cmdins2.CommandType = CommandType.StoredProcedure
            With cmdins2.Parameters
                .AddWithValue("@LOB", "0")
                .AddWithValue("@MenuId", var2)
                .AddWithValue("@UserType", "3")
                .AddWithValue("@Access", "")
                .AddWithValue("@Currdate", System.DateTime.Now)
                .AddWithValue("@AssignBy", Session("userid"))
                .AddWithValue("@parentid", "17")
                .AddWithValue("@userid", Session("userid"))
            End With
            cmdins2.ExecuteNonQuery()
            cmdins2.Dispose()

            cmd3 = New SqlCommand("select Max(MenuID) from nlvl_menu", con)
            var3 = Convert.ToInt32(cmd3.ExecuteScalar()) + 1
            Dim com13 As New SqlCommand("Insert_nlvl_menu_fp", con)
            com13.CommandType = CommandType.StoredProcedure
            With com13.Parameters
                com13.Parameters.AddWithValue("MenuID", var3)
                com13.Parameters.AddWithValue("CreatedBy", Session("userid"))
                com13.Parameters.AddWithValue("CreatedDate", FormatDateTime(System.DateTime.Now, DateFormat.ShortDate))
            End With
            com13.ExecuteNonQuery()
            com13.Dispose()
            Dim com3 As New SqlCommand("Insert_Menu", con)
            com3.CommandType = CommandType.StoredProcedure
            With com3.Parameters
                com3.Parameters.AddWithValue("@MenuID", var3)
                com3.Parameters.AddWithValue("@ParentID", 17)
                com3.Parameters.AddWithValue("@MenuDescription", level3)
                com3.Parameters.AddWithValue("@URLLink", "/FunctionProcess/LOBMaster.aspx")
                com3.Parameters.AddWithValue("@ActiveStatus", "A")
                com3.Parameters.AddWithValue("@Level", 1)
                com3.Parameters.AddWithValue("@LOB", 0)
                com3.Parameters.AddWithValue("@orderby", "4.00")
                com3.Parameters.AddWithValue("@limit", "OS")
            End With
            com3.ExecuteNonQuery()
            com3.Dispose()
            Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", con)
            cmdins3.CommandType = CommandType.StoredProcedure
            With cmdins3.Parameters
                .AddWithValue("@LOB", "0")
                .AddWithValue("@MenuId", var3)
                .AddWithValue("@UserType", "3")
                .AddWithValue("@Access", "")
                .AddWithValue("@Currdate", System.DateTime.Now)
                .AddWithValue("@AssignBy", Session("userid"))
                .AddWithValue("@parentid", "17")
                .AddWithValue("@userid", Session("userid"))
            End With
            cmdins3.ExecuteNonQuery()
            cmdins3.Dispose()

            WARSShowMsg("Hierarchy has been added Successfully.")
            dept.Text = ""
            client.Text = ""
            lob.Text = ""
        End If

    End Sub
    Public Sub WARSShowMsg(ByVal strmsg As String)
        'alert function for message display
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        RegisterStartupScript("showmsg", str)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (IsPostBack = False) Then
            con.Open()
            Dim cmd As SqlCommand = New SqlCommand("Select count(MenuDescription) from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", con)
            Dim count As Integer
            count = Convert.ToInt32(cmd.ExecuteScalar())
            If (count <= 0) Then
                tableh1.Visible = True
                grdlob.Visible = False
            Else
                tableh1.Visible = False
                grdlob.Visible = True
                'Dim qry As String = "Select MenuId,MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'"
                'Dim da13 As SqlDataAdapter = New SqlDataAdapter(qry, con)
                'Dim ds As DataSet = New DataSet()
                'da13.Fill(ds)
                'dlreg.DataSource = ds
                'dlreg.DataBind()
                griddata()
            End If

            con.Close()
        End If
    End Sub

    Protected Sub griddata()
        Dim qry As String = "Select b.MenuID,MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'"
        Dim da13 As SqlDataAdapter = New SqlDataAdapter(qry, con)
        Dim ds As DataSet = New DataSet()
        da13.Fill(ds)
        grdlob.DataSource = ds
        grdlob.DataBind()
    End Sub

    Private Sub UpdateLobName(ByVal name, ByVal autoid)
        Dim com As New SqlCommand("update nlvl_menu set MenuDescription='" & name & "' where MenuID='" & autoid & "'", con)
        con.Open()
        If com.ExecuteNonQuery() <> -1 Then
            WARSShowMsg("level Updated Successfully.")
        End If
        con.Close()
        com.Dispose()
    End Sub

    Protected Sub grdlob_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles grdlob.RowCancelingEdit
        grdlob.EditIndex = -1
        griddata()
    End Sub

    Protected Sub grdlob_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles grdlob.RowEditing
        grdlob.EditIndex = e.NewEditIndex
        griddata()
    End Sub

    Protected Sub grdlob_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles grdlob.RowUpdating
        Dim row As GridViewRow = grdlob.Rows(e.RowIndex)
        Dim id As Integer = Convert.ToInt32(grdlob.DataKeys(row.RowIndex).Value.ToString())
        Dim txthead As TextBox = CType(row.FindControl("txtLOB"), TextBox)
        Dim a As String
        a = txthead.Text
        grdlob.EditIndex = -1
        con.Open()

        If (a = "") Then
            WARSShowMsg("Level Name can not be Blank")
            Exit Sub
        Else
            Dim com As New SqlCommand("Select b.MenuID,MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where MenuDescription='" & a & "' and b.MenuID!='" & id & "'", con)

            Dim rdr As SqlDataReader
            rdr = com.ExecuteReader
            If rdr.Read Then
                WARSShowMsg("This Lavel Name already exists")
                rdr.Close()
                con.Close()
                com.Dispose()
                Exit Sub
            End If
            rdr.Close()
            con.Close()
            com.Dispose()
            UpdateLobName(a, id)
            griddata()
            'Dim vr As String
            'vr = Request.QueryString("val")
            'Response.Redirect("../FunctionProcess/AddNews.aspx?val=" + vr)
        End If
    End Sub
End Class
