Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class TableTools_DataImportFromSql
    Inherits System.Web.UI.Page

    Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("Bmp10ConnectionString").ConnectionString)
    Dim con1 As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("IDMSConnectionString").ConnectionString)
    Dim cmd As SqlCommand
    Dim cmd1 As SqlCommand
    Dim dr As SqlDataReader
    Protected Sub Get_database_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Get_database.Click

        msg1.Visible = False
        msg2.Visible = False
        msg3.Visible = False
        Dim txt As String = TextBox1.Text
        Dim uname As String = txt2.Text
        Dim pass As String = txt3.Text
        If txt = "" Then
            msg1.Visible = True
        ElseIf uname = "" Then
            msg2.Visible = True
        ElseIf pass = "" Then
            msg3.Visible = True
        Else
            Dim con As SqlConnection = New SqlConnection("Data Source=" + txt + ";Initial Catalog=;Trusted_Connection=False;User Id=" + uname + "; password=" + pass + "")
            cmd = New SqlCommand("SELECT name FROM sys.sysdatabases", con)
            con.Open()
            dr = cmd.ExecuteReader()
            select_database.DataSource = dr
            select_database.DataTextField = "name"
            select_database.DataValueField = "name"
            select_database.DataBind()
        End If
    End Sub

    Protected Sub select_database_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles select_database.SelectedIndexChanged
        Label5.Visible = True
        Dim s As String = select_database.SelectedItem.Text
        Dim txt As String = TextBox1.Text
        Dim con As SqlConnection = New SqlConnection("Data Source=" + txt + "; Initial Catalog=;Trusted_Connection=False;User Id=sa;password=1234")
        cmd = New SqlCommand("use " + s + " select name from sys.Tables ", con)
        con.Open()
        dr = cmd.ExecuteReader()
        Dim table As String
        While (dr.Read())
            table = dr("name").ToString()
            table_list.Items.Add(table)
        End While
    End Sub
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub data_import_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles data_import.Click
        Dim dept, client, lob As String

        If (chdept.SelectedIndex = 0) Then
            aspnet_msgbox("Please Select Department First")
        End If
        dept = chdept.SelectedValue
        If (chclient.SelectedIndex = 0 Or chclient.SelectedValue = "--Select--") Then
            client = 0
        Else
            client = chclient.SelectedValue
        End If
        If (chlob.SelectedValue = "" Or chclient.SelectedValue = "--Select--") Then
            lob = 0
        Else
            lob = chlob.SelectedValue
        End If
        Dim tab As String
        Dim alltext As TextBox() = {Me.tx0, Me.tx1, Me.tx2, Me.tx3, Me.tx4}
        Dim txt_arr() As String = {"", "", "", "", ""}
        Dim lst_arr() As String = {"", "", "", "", ""}
        Dim txt As String = TextBox1.Text
        Dim k = 0
        Dim s As String = select_database.SelectedItem.Text
        For j = 0 To table_list.Items.Count - 1
            If (table_list.Items(j).Selected.Equals(True)) Then
                lst_arr(k) = table_list.Items(j).Text
                txt_arr(k) = alltext(k).Text
                tab = alltext(k).Text
                
                con = New SqlConnection("Data Source=" + txt + "; Initial Catalog=;Trusted_Connection=False;User Id=sa;password=1234")
                con.Open()
                cmd = New SqlCommand("SELECT * INTO  QlickReport.dbo." + txt_arr(k) + " FROM " + s + ".dbo." + lst_arr(k) + "", con)
                dr = cmd.ExecuteReader()
                con.Close()
                k = k + 1
                'getting all columns // CHANGES BY GAURAV BUDHIRAJA ON 4-AUG-2012
                Dim connew As SqlConnection
                Dim colname As String = ""
                connew = New SqlConnection("data source=DESKTOP-V32N4KG\SQLEXPRESS;initial catalog=QlickReport;User Id=sa;password=1234")
                Dim cmdnew = New SqlCommand("SELECT COLUMN_NAME 'All_Columns' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + tab + "'", connew)
                Dim drnew As SqlDataReader
                connew.Open()
                drnew = cmdnew.ExecuteReader()
                While (drnew.Read())
                    If (colname = "") Then
                        colname = drnew("All_Columns").ToString()
                    Else
                        colname = colname + "," + drnew("All_Columns").ToString()
                    End If
                End While
                drnew.Close()
                connew.Close()
                'ENTRY IN WARSLOBTABLEMASTER OF EACH NEWLY IMPORTED TABLE // CHANGES BY GAURAV BUDHIRAJA ON 4-AUG-2012
                Dim cmdin As New SqlCommand
                cmdin.CommandType = CommandType.StoredProcedure
                cmdin.CommandText = "insert_WARSLOBTableMaster"
                con1.Close()
                con1.Open()
                cmdin.Connection = con1
                With cmdin.Parameters
                    If chlob.SelectedValue = "" Or chlob.SelectedValue = "--Select--" Then
                        .AddWithValue("@LobId", "0")
                    Else
                        .AddWithValue("@LobId", chlob.SelectedValue)
                    End If
                    .AddWithValue("@TableName", tab)
                    .AddWithValue("@CreatedOn", System.DateTime.Today)
                    .AddWithValue("@CreatedBy", Session("userid"))
                    .AddWithValue("@LastModified", System.DateTime.Today)
                    .AddWithValue("@LastModifiedBy", Session("userid"))
                    .AddWithValue("@currdate", System.DateTime.Today)
                    .AddWithValue("@visiblecolumn", colname)
                    .AddWithValue("@DepartmentId", chdept.SelectedValue)
                    If chclient.SelectedValue = "--Select--" Then
                        .AddWithValue("@ClientId", "0")
                    Else
                        .AddWithValue("@ClientId", chclient.SelectedValue)
                    End If
                    .AddWithValue("@Local", "NonLocal")
                    'End If
                    .AddWithValue("@primcol", "Null")
                    .AddWithValue("@constraintname", "null")
                End With
                cmdin.ExecuteNonQuery()
                connew.Close()
                cmdin.Dispose()
            End If
        Next
        MsgBox("Data Imported Succesfully")
        table_list.Items.Clear()
        tx0.Visible = False
        tx1.Visible = False
        tx2.Visible = False
        tx3.Visible = False
        tx4.Visible = False
        Exit Sub
    End Sub

    Protected Sub clear_table_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles clear_table.Click
        table_list.Items.Clear()
        change.Visible = "False"
        tx0.Visible = False
        tx1.Visible = False
        tx2.Visible = False
        tx3.Visible = False
        tx4.Visible = False

    End Sub

    Protected Sub selected_table_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles selected_table.Click
        Dim j As Integer = 0
        Dim i As Integer = 0
        Dim count As Integer = 0
        Dim alltext As TextBox() = {Me.tx0, Me.tx1, Me.tx2, Me.tx3, Me.tx4}
        tx0.Enabled = True
        tx1.Enabled = True
        tx2.Enabled = True
        tx3.Enabled = True
        tx4.Enabled = True
        For j = 0 To table_list.Items.Count - 1
            If (table_list.Items(j).Selected.Equals(True)) Then
                count = count + 1
            End If
        Next
        If count <= 0 Then
            MsgBox("please select atleast one table")
        ElseIf count > 5 Then
            MsgBox("Only 5 tables allowed to select")
        Else
            change.Visible = True
            For j = 0 To table_list.Items.Count - 1
                If (table_list.Items(j).Selected.Equals(True)) Then
                    alltext(i).Text = table_list.Items(j).Text
                    i = i + 1
                End If
            Next
            For i = 0 To 4
                If (i < count) Then
                    alltext(i).Visible = True
                Else
                    alltext(i).Visible = False
                End If
            Next
        End If
    End Sub
    Protected Sub created_table_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles created_table.Click
        Dim txt_arr As String() = {"", "", "", "", ""}
        Dim lst_arr As String() = {"", "", "", "", ""}
        Dim k As Integer = 0
        Dim alltext As TextBox() = {Me.tx0, Me.tx1, Me.tx2, Me.tx3, Me.tx4}
        tx0.Enabled = True
        tx1.Enabled = True
        tx2.Enabled = True
        tx3.Enabled = True
        tx4.Enabled = True
        Lbl1.Visible = False
        Lbl2.Visible = False
        Lbl3.Visible = False
        Lbl4.Visible = False
        Lbl5.Visible = False
        Dim count1 As Integer = 0
        Dim da As SqlDataAdapter
        Dim cmd As SqlCommand
        Dim dt As DataTable = New DataTable()
        Dim txt As String = TextBox1.Text
        Dim i = 0
        Dim con As SqlConnection = New SqlConnection("Data Source=DESKTOP-V32N4KG\SQLEXPRESS; Initial Catalog=QlickReport;Trusted_Connection=False;User Id=sa;password=1234")
        con.Open()
        cmd = New SqlCommand("select COUNT(*) from sys.tables", con)
        Dim count = Convert.ToInt32(cmd.ExecuteScalar())
        Dim str_arr(count) As String
        cmd = New SqlCommand("select name from sys.Tables", con)
        con.Close()
        con.Open()
        dr = cmd.ExecuteReader()
        Dim tvalue As String = ""
        Dim t0value As String = ""
        Dim t1value As String = ""
        Dim t2value As String = ""
        Dim t3value As String = ""
        Dim t4value As String = ""
        For j = 0 To table_list.Items.Count - 1
            If (table_list.Items(j).Selected.Equals(True)) Then
                count1 = count1 + 1
            End If
        Next
        Dim tname As String = ""
        If (tx0.Visible.Equals(True)) Then
            t0value = tx0.Text
        End If
        If (tx1.Visible.Equals(True)) Then
            t1value = tx1.Text
        End If
        If (tx2.Visible.Equals(True)) Then
            t2value = tx2.Text
        End If
        If (tx3.Visible.Equals(True)) Then
            t3value = tx3.Text
        End If
        If (tx4.Visible.Equals(True)) Then
            t4value = tx4.Text
        End If
        While (dr.Read())
            tvalue = dr("name").ToString()

            If (t0value.Equals(tvalue)) Then
                Lbl1.Visible = True
            ElseIf (t1value.Equals(tvalue)) Then
                Lbl2.Visible = True
            ElseIf (t2value.Equals(tvalue)) Then
                Lbl3.Visible = True
            ElseIf (t3value.Equals(tvalue)) Then
                Lbl4.Visible = True
            ElseIf (t4value.Equals(tvalue)) Then
                Lbl5.Visible = True
            End If
        End While
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        con1.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", con1)
        dr = cmd.ExecuteReader
        If dr.Read Then
            Dim producttype As String = dr("ProductType")
            If (producttype = "Multiple User") Then
                dept_row.Visible = True
                client_row.Visible = True
                lob_row.Visible = True
                data_import.Visible = True
                dr.Close()
                cmd.Dispose()
                If (Session("typeofuser") = "Super Admin") Then
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", con1)
                Else
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", con1)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    deptname.Text = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    clientname.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    ddllobname.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If
                Dim typeofuser = Session("typeofuser")
                If (Me.IsPostBack = False) Then
                    If (typeofuser.Equals("Super Admin")) Then
                        Dim cmdgetdept As New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", con1)
                        Dim dsgetdept As New DataSet
                        Dim adpgetdept As New SqlDataAdapter
                        adpgetdept.SelectCommand = cmdgetdept
                        adpgetdept.Fill(dsgetdept)
                        chdept.DataSource = dsgetdept
                        chdept.DataTextField = "DepartmentName"
                        chdept.DataValueField = "autoid"
                        chdept.DataBind()
                        chdept.Items.Insert(0, "--Select--")
                    ElseIf (typeofuser.Equals("User")) Then
                        Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con1)
                        Dim dsgetdept As New DataSet
                        Dim adpgetdept As New SqlDataAdapter
                        adpgetdept.SelectCommand = cmdgetdept
                        adpgetdept.Fill(dsgetdept)
                        chdept.DataSource = dsgetdept
                        chdept.DataTextField = "DepartmentName"
                        chdept.DataValueField = "autoid"
                        chdept.DataBind()
                        chdept.Items.Insert(0, "--Select--")
                        adpgetdept.Dispose()
                        dsgetdept.Clear()
                        cmdgetdept.Dispose()
                    ElseIf (typeofuser.Equals("Admin")) Then
                        Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con1)
                        Dim dsgetdept As New DataSet
                        Dim adpgetdept As New SqlDataAdapter
                        adpgetdept.SelectCommand = cmdgetdept
                        adpgetdept.Fill(dsgetdept)
                        chdept.DataSource = dsgetdept
                        chdept.DataTextField = "DepartmentName"
                        chdept.DataValueField = "autoid"
                        chdept.DataBind()
                        chdept.Items.Insert(0, "--Select--")
                    End If
                End If
                con1.Close()
            Else
                data_import_singleuser.Visible = True
            End If

        End If
      

    End Sub

    Protected Sub chdept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chdept.SelectedIndexChanged
        Dim cmdnew As SqlCommand
        Dim drnew As SqlDataReader
        If (chdept.SelectedValue = "--Select--") Then
            chclient.Items.Clear()
            chlob.Items.Clear()
        Else
            'Dim connew As SqlConnection = New SqlConnection("Data Source=.; Initial Catalog=QlickReport;Integrated Security=true")
            con.Open()
            cmdnew = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + chdept.SelectedValue + "'", con)
            drnew = cmdnew.ExecuteReader()
            chclient.DataSource = drnew
            chclient.DataTextField = "ClientName"
            chclient.DataValueField = "autoid"
            chclient.DataBind()
            chclient.Items.Insert(0, "--Select--")
            con.Close()
            drnew.Close()
            cmdnew.Dispose()
        End If
        
    End Sub

    Protected Sub chclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chclient.SelectedIndexChanged
        Dim cmdnew As SqlCommand
        Dim drnew As SqlDataReader
        If (chclient.SelectedValue = "--Select--") Then
            chlob.Items.Clear()
        Else
            con.Open()
            cmdnew = New SqlCommand("select autoid,LOBName from WARSLobMaster where deptid='" + chdept.SelectedValue + "' and  clientid= '" + chclient.SelectedValue + "'", con)
            drnew = cmdnew.ExecuteReader()
            chlob.DataSource = drnew
            chlob.DataTextField = "LOBName"
            chlob.DataValueField = "AutoID"
            chlob.DataBind()
            chlob.Items.Insert(0, " --Select-- ")
            con.Close()
            drnew.Close()
            cmdnew.Dispose()
        End If
        'Dim connew As SqlConnection = New SqlConnection("Data Source=.; Initial Catalog=QlickReport;Integrated Security=true")
        
    End Sub

    Protected Sub data_import_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles data_import_singleuser.Click
        Dim tab As String
        Dim alltext As TextBox() = {Me.tx0, Me.tx1, Me.tx2, Me.tx3, Me.tx4}
        Dim txt_arr() As String = {"", "", "", "", ""}
        Dim lst_arr() As String = {"", "", "", "", ""}
        Dim txt As String = TextBox1.Text
        Dim k = 0
        Dim s As String = select_database.SelectedItem.Text
        For j = 0 To table_list.Items.Count - 1
            If (table_list.Items(j).Selected.Equals(True)) Then
                lst_arr(k) = table_list.Items(j).Text
                txt_arr(k) = alltext(k).Text
                tab = alltext(k).Text

                con = New SqlConnection("Data Source=" + txt + "; Initial Catalog=;Trusted_Connection=False;User Id=sa;password=1234")
                con.Open()
                cmd = New SqlCommand("SELECT * INTO  QlickReport.dbo." + txt_arr(k) + " FROM " + s + ".dbo." + lst_arr(k) + "", con)
                dr = cmd.ExecuteReader()
                con.Close()
                k = k + 1
                'getting all columns // CHANGES BY GAURAV BUDHIRAJA ON 4-AUG-2012
                Dim connew As SqlConnection
                Dim colname As String = ""
                connew = New SqlConnection("data source=.;initial catalog=QlickReport;integrated security=sspi")
                Dim cmdnew = New SqlCommand("SELECT COLUMN_NAME 'All_Columns' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + tab + "'", connew)
                Dim drnew As SqlDataReader
                connew.Open()
                drnew = cmdnew.ExecuteReader()
                While (drnew.Read())
                    If (colname = "") Then
                        colname = drnew("All_Columns").ToString()
                    Else
                        colname = colname + "," + drnew("All_Columns").ToString()
                    End If
                End While
                drnew.Close()
                connew.Close()
                'ENTRY IN WARSLOBTABLEMASTER OF EACH NEWLY IMPORTED TABLE // CHANGES BY GAURAV BUDHIRAJA ON 4-AUG-2012
                Dim cmdin As New SqlCommand
                cmdin.CommandType = CommandType.StoredProcedure
                cmdin.CommandText = "insert_WARSLOBTableMaster"
                con1.Close()
                con1.Open()
                cmdin.Connection = con1
                With cmdin.Parameters
                    If chlob.SelectedValue = "" Or chlob.SelectedValue = "--Select--" Then
                        .AddWithValue("@LobId", "0")
                    Else
                        .AddWithValue("@LobId", chlob.SelectedValue)
                    End If
                    .AddWithValue("@TableName", tab)
                    .AddWithValue("@CreatedOn", System.DateTime.Today)
                    .AddWithValue("@CreatedBy", Session("userid"))
                    .AddWithValue("@LastModified", System.DateTime.Today)
                    .AddWithValue("@LastModifiedBy", Session("userid"))
                    .AddWithValue("@currdate", System.DateTime.Today)
                    .AddWithValue("@visiblecolumn", colname)
                    .AddWithValue("@DepartmentId", "60")
                    If chclient.SelectedValue = "--Select--" Then
                        .AddWithValue("@ClientId", "0")
                    Else
                        .AddWithValue("@ClientId", chclient.SelectedValue)
                    End If
                    .AddWithValue("@Local", "NonLocal")
                    'End If
                    .AddWithValue("@primcol", "Null")
                    .AddWithValue("@constraintname", "null")
                End With
                cmdin.ExecuteNonQuery()
                connew.Close()
                cmdin.Dispose()
            End If
        Next
        MsgBox("Data Imported Succesfully")
        table_list.Items.Clear()
        tx0.Visible = False
        tx1.Visible = False
        tx2.Visible = False
        tx3.Visible = False
        tx4.Visible = False
        Exit Sub
    End Sub

    End Class
