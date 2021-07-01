﻿Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class Content_QlickLicense
    Inherits System.Web.UI.Page
    ''' <remarks>Mohit Tyagi</remarks>
    Dim item As String
    Dim producttype As String
    Dim databasetype As String
    Dim database As String
    Dim conn As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(conn)
    Dim cmdnew As SqlCommand
    Sub LinkButton_Command(ByVal sender As Object, ByVal e As CommandEventArgs)
        item = e.CommandArgument.ToString()
        Session("item") = item
        Response.Redirect("Registration.aspx")
    End Sub
    Protected Sub b1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles b1.Click
        Dim i, cnt As Integer
        database = " "
        cnt = 0
        producttype = DropDownList1.SelectedItem.ToString()
        databasetype = drop1.SelectedItem.ToString()
        For i = 0 To CheckBoxList1.Items.Count - 1
            If CheckBoxList1.Items(i).Selected = True Then
                cnt = cnt + 1
                If (cnt > 1) Then
                    database = database + "," + CheckBoxList1.Items(i).Text
                Else
                    database = CheckBoxList1.Items(i).Text
                End If
            End If
        Next

        Session("producttype") = producttype
        Session("databasetype") = databasetype
        Session("database") = database
        txtHidden.Text = ""
        For i = 0 To CheckBoxList1.Items.Count - 1
            If CheckBoxList1.Items(i).Selected = True Then
                txtHidden.Text += CheckBoxList1.Items(i).Text
            End If
        Next
        If (DropDownList1.SelectedValue = "--Select Product--") Then
            lblmsg.Text = "Please Select Product Type."
            lblmsg.Visible = True
            lblmsg1.Visible = False


        ElseIf (drop1.SelectedValue = "--Select Database--") Then
            lblmsg.Visible = False
            lblmsg1.Visible = True
            lblmsg1.Text = "Please Select Database Type."


        ElseIf (CheckBoxList1.Items(0).Selected = False And CheckBoxList1.Items(1).Selected = False And CheckBoxList1.Items(2).Selected = False And CheckBoxList1.Items(3).Selected = False And CheckBoxList1.Items(4).Selected = False) Then
            lblmsg.Visible = False
            lblmsg1.Visible = False
            lblmsg2.Visible = True
            lblmsg2.Text = "Please Select at least one database."

        Else

            lblmsg.Visible = False
            lblmsg1.Visible = False



            If (drop1.SelectedValue = "Single Database") Then

                If (CheckBoxList1.Items(0).Selected = True And ((CheckBoxList1.Items(1).Selected = True) Or (CheckBoxList1.Items(2).Selected = True) Or (CheckBoxList1.Items(3).Selected = True) Or (CheckBoxList1.Items(4).Selected = True))) Then
                    lblmsg2.Visible = True
                    lblmsg2.Text = "Please Select only one database."

                ElseIf (CheckBoxList1.Items(1).Selected = True And ((CheckBoxList1.Items(0).Selected = True) Or (CheckBoxList1.Items(2).Selected = True) Or (CheckBoxList1.Items(3).Selected = True) Or (CheckBoxList1.Items(4).Selected = True))) Then
                    lblmsg2.Visible = True
                    lblmsg2.Text = "Please Select only one database."

                ElseIf (CheckBoxList1.Items(2).Selected = True And ((CheckBoxList1.Items(1).Selected = True) Or (CheckBoxList1.Items(0).Selected = True) Or (CheckBoxList1.Items(3).Selected = True) Or (CheckBoxList1.Items(4).Selected = True))) Then
                    lblmsg2.Visible = True
                    lblmsg2.Text = "Please Select only one database."

                ElseIf (CheckBoxList1.Items(3).Selected = True And ((CheckBoxList1.Items(1).Selected = True) Or (CheckBoxList1.Items(2).Selected = True) Or (CheckBoxList1.Items(0).Selected = True) Or (CheckBoxList1.Items(4).Selected = True))) Then
                    lblmsg2.Visible = True
                    lblmsg2.Text = "Please Select only one database."

                ElseIf (CheckBoxList1.Items(4).Selected = True And ((CheckBoxList1.Items(1).Selected = True) Or (CheckBoxList1.Items(2).Selected = True) Or (CheckBoxList1.Items(3).Selected = True) Or (CheckBoxList1.Items(0).Selected = True))) Then
                    lblmsg2.Visible = True
                    lblmsg2.Text = "Please Select only one database."

                Else
                    lblmsg2.Visible = False
                    If (DropDownList1.SelectedValue = "Single User" And drop1.SelectedValue = "Single Database") Then
                        If (txtHidden.Text = "Excel") Then
                            SinUserSingDBExc.Visible = True
                        ElseIf (txtHidden.Text = "MS-SQL") Then
                            SinUserSingDBSql.Visible = True
                        ElseIf (txtHidden.Text = "MySQL") Then
                            SinUserSingDBMySql.Visible = True
                        ElseIf (txtHidden.Text = "Oracle") Then
                            SinUserSingDBOra.Visible = True
                        ElseIf (txtHidden.Text = "PostgreSQL") Then
                            SinUserSingDBPogr.Visible = True
                        End If

                    ElseIf (DropDownList1.SelectedValue = "Multiple User" And drop1.SelectedValue = "Single Database") Then
                        If (txtHidden.Text = "Excel") Then
                            MulUserSingDBExc.Visible = True
                        ElseIf (txtHidden.Text = "MS-SQL") Then
                            MulUserSingDBSQL.Visible = True
                        ElseIf (txtHidden.Text = "MySQL") Then
                            MulUserSingDBMySQL.Visible = True
                        ElseIf (txtHidden.Text = "Oracle") Then
                            MulUserSingDBOra.Visible = True
                        ElseIf (txtHidden.Text = "PostgreSQL") Then
                            MulUserSingDBPogr.Visible = True
                        End If

                    End If
                End If
            Else
                i = 0
                For Each item As ListItem In CheckBoxList1.Items
                    If item.Selected Then
                        i = i + 1
                    End If
                Next
                If i > 1 Then
                    If (DropDownList1.SelectedValue = "Single User" And drop1.SelectedValue = "Multiple Database") Then
                        lblmsg2.Visible = False
                        SinUserMulDB.Visible = True
                    ElseIf (DropDownList1.SelectedValue = "Multiple User" And drop1.SelectedValue = "Multiple Database") Then
                        lblmsg2.Visible = False
                        MulUserMulDB.Visible = True
                    End If
                Else
                    lblmsg2.Visible = True
                    lblmsg2.Text = "Please Select more then one database."
                End If
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckBoxList1.Items(0).Value = False
        CheckBoxList1.Items(1).Value = False
        CheckBoxList1.Items(2).Value = False
        CheckBoxList1.Items(3).Value = False
        CheckBoxList1.Items(4).Value = False
        CheckBoxList1.Items(3).Enabled = False
        CheckBoxList1.Items(4).Enabled = False

        SinUserSingDBExc.Visible = False
        SinUserSingDBMySql.Visible = False
        SinUserSingDBSql.Visible = False
        SinUserSingDBOra.Visible = False
        SinUserSingDBPogr.Visible = False
        SinUserMulDB.Visible = False
        MulUserSingDBExc.Visible = False
        MulUserSingDBSQL.Visible = False
        MulUserSingDBMySQL.Visible = False
        MulUserSingDBOra.Visible = False
        MulUserSingDBPogr.Visible = False
        MulUserMulDB.Visible = False

        If (IsPostBack = False) Then
            drop1.SelectedIndex = 0
            DropDownList1.SelectedIndex = 0
            'CheckBoxList1.SelectedValue = ""
        End If
    End Sub
    Sub Check_Clicked(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim rdr As SqlDataReader
        Dim rdr1 As SqlDataReader
        If txtuserid.Value = "" Then
            lblmsg.Visible = True
            Me.lblmsg.Text = "UserId Field Should Not Be Blank"
            txtuserid.Focus()
            Exit Sub
        End If
        If txtpassword.Value = "" Then
            lblmsg.Visible = True
            Me.lblmsg.Text = "Password Field Should Not Be Blank"
            txtpassword.Focus()
            Exit Sub
        End If
        connection.Close()
        connection.Open()
        lblmsg.Visible = False

        cmdnew = New SqlCommand("select userid,usertype,status,CreatedBy,username,pwd,deptid,clientid,lobid from registration where userid='" & txtuserid.Value & "' and pwd='" + Crypto.Encrypt(Trim(txtpassword.Value)) + "'", connection)
        rdr = cmdnew.ExecuteReader
        If rdr.Read Then
            If rdr("usertype") = 1 Then
                Session("typeofuser") = "User"
                Session("CreatedBy") = rdr("CreatedBy")
                Session("userid") = rdr("userid")
                Session("userid1") = rdr("userid")
                Session("username") = rdr("username")
                Session("logintime") = System.DateTime.Now
                Session("usertype") = LCase(rdr("UserType"))
                Session("deptid") = rdr("deptid")
                Session("clientid") = rdr("clientid")
                Session("lobid") = rdr("lobID")
                rdr.Close()
            ElseIf rdr("usertype") = 3 Then
                Session("typeofuser") = "Super Admin"
                Session("CreatedBy") = "CreatedBy"
                Session("userid") = rdr("userid")
                Session("userid1") = rdr("userid")
                Session("username") = rdr("username")
                Session("logintime") = System.DateTime.Now
                Session("usertype") = LCase(rdr("UserType"))
                Session("deptid") = rdr("deptid")
                Session("clientid") = rdr("clientid")
                Session("lobid") = rdr("lobID")
            Else
                Session("typeofuser") = "Admin"
                Session("CreatedBy") = "CreatedBy"
                Session("userid") = rdr("userid")
                Session("userid1") = rdr("userid")
                Session("username") = rdr("username")
                Session("logintime") = System.DateTime.Now
                Session("usertype") = LCase(rdr("UserType"))
                Session("deptid") = rdr("deptid")
                Session("clientid") = rdr("clientid")
                Session("lobid") = rdr("lobID")
            End If
        End If
        rdr.Close()
        cmdnew.Dispose()

        cmdnew = New SqlCommand("select lobid,deptid,clientid,adminid,usertype,adminname from masteradmin where adminid='" & Session("userid") & "'", connection)
        'connection.Open()
        rdr = cmdnew.ExecuteReader
        If rdr.Read Then
            Session("useradmincheck") = "yes"
        Else
            Session("useradmincheck") = "no"
        End If
        rdr.Close()

        Dim com As New SqlCommand("select EndDate from InternetProductDemo where userid='" & Trim(txtuserid.Value) & "'", connection)
        Dim rdrexp As SqlDataReader
        rdrexp = com.ExecuteReader
        If rdrexp.Read Then
            Dim dt As Date = rdrexp("EndDate")
            Dim exp = (DateDiff(DateInterval.Day, System.DateTime.Now, dt))
            If exp < 0 Then
                lblmsg.Visible = "True"
                lblmsg.Text = "Your Password has been Expired"
                rdrexp.Close()
                com.Dispose()
            ElseIf exp >= 0 Then
                rdrexp.Close()
                com.Dispose()
                cmdnew = New SqlCommand("select * from Registration where UserID='" + txtuserid.Value + "' and Pwd='" + Crypto.Encrypt(Trim(txtpassword.Value)) + "'", connection)
                rdr = cmdnew.ExecuteReader()
                If rdr.Read Then
                    Session("userid") = rdr("userid")
                    Session("prefix") = rdr("Prefix")
                    Session("username") = rdr("UserName")
                    Session("pwd") = rdr("Pwd").ToString()
                    Response.Redirect("~/Misc/Home.aspx")
                Else
                    lblmsg.Visible = True
                    lblmsg.Text = "Sorry !!! You are not Authenticated Person,Contact to Administrator."
                    txtuserid.Focus()
                End If
                rdr.Close()
                cmdnew.Dispose()
            End If
        End If
    End Sub
End Class
