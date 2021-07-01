Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Math
Imports System.Net.Sockets
Imports System.Reflection
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim con As String = AppSettings("ConnectionString")
    Dim co As New SqlConnection(con)
    Dim connection As New SqlConnection(con)
    Dim cmd As New SqlCommand
    Dim rdr As SqlDataReader
    Dim cmdnew As SqlCommand
    Public url = ""
    Dim typeofuser As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        typeofuser = Session("typeofuser")
        lbmsg.Text = typeofuser
        If (Session("userid") = "") Then
            Response.Redirect("../SessionExpired.aspx")
            Exit Sub
        End If
        Page.SmartNavigation = True
        If Page.IsPostBack = False Then
            If Session("useradmincheck") = "yes" Then
                useradmin.Visible = True
                useradmin1.Visible = False
            Else
                useradmin.Visible = False
                useradmin1.Visible = False
            End If
        End If

        If Page.IsPostBack = False Then
            Dim AllLinkButtons As LinkButton() = {Me.lb1, Me.lb2, Me.lb3, Me.lb4, Me.lb5, Me.lb6, Me.lb7, Me.lb8, Me.lb9}
            Dim cmdget As New SqlCommand
            '''''''''''''''''''' Main Menu Starts'''''''''''''''''''''''''''''
            cmdget = New SqlCommand("select distinct(a.MenuDescription) as menuname, a.URLLink as menureff,a.menuid  from nlvl_menu as a, nlvl_menu_rights as b where b.userid='" + Session("userid").ToString + "' and b.MenuId=a.MenuId and a.parentid='0' and b.usertype='1'  order by a.MenuDescription", connection)
            connection.Open()
            Dim ad As New SqlDataAdapter
            Dim ds As New DataSet
            ad.SelectCommand = cmdget
            ad.Fill(ds, "abc")
            Dim c As Integer
            c = ds.Tables("abc").Rows.Count
            For k = 0 To c - 1
                AllLinkButtons(k).Visible = True
                AllLinkButtons(k).Text = Trim(ds.Tables("abc").Rows(k)("menuname").ToString())
                AllLinkButtons(k).PostBackUrl = Trim(ds.Tables("abc").Rows(k)("menureff").ToString()) + "?val=" + Trim(ds.Tables("abc").Rows(k)("menuid").ToString())
                AllLinkButtons(k).ToolTip = Trim(ds.Tables("abc").Rows(k)("menuname").ToString())
            Next
            connection.Close()
            cmdget.Dispose()
        End If

        If (Session("userid") = "") Then
            Response.Redirect("../SessionExpired.aspx")
            Exit Sub
        Else
            username.Text = Session("username").ToString() + " " + "(" + Session("userid").ToString() + ")"
        End If
        menu.Nodes.Clear()
        If Trim(Request("val")).ToString() <> Nothing Then '<> not equal to sign
            Dim cmd As New SqlCommand

            If Trim(Request("val")).ToString() = "5" Or Trim(Request("val")).ToString() = "103" Or Trim(Request("val")).ToString() = "2" Then
                Me.LeftPlaceHolder.Visible = True
                divTree.Visible = False
            Else
                Me.LeftPlaceHolder.Visible = False
            End If

            connection.Open()
            cmd = New SqlCommand("select distinct menudescription,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "' and nlvl_menu_rights.UserId='" + Session("userid").ToString + "' order by orderby", connection)
            Dim dr1 As SqlDataReader
            Dim nodeaccount As System.Web.UI.WebControls.TreeNode
            dr1 = cmd.ExecuteReader
            While dr1.Read()
                nodeaccount = New TreeNode()
                nodeaccount.Text = dr1("menudescription")
                nodeaccount.NavigateUrl = "/QlickReport" + dr1("URLLink") + "?val=" + Request("val")
                nodeaccount.ToolTip = dr1("menudescription")
                menu.Nodes.Add(nodeaccount)
            End While
            connection.Close()
        End If
    End Sub

    Protected Sub logout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles logout.Click
        Session.Abandon()
        Response.Redirect("~/Default.aspx")
    End Sub

    Protected Sub hp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles hp.Click
        Response.Redirect("~/Misc/Home.aspx")
    End Sub

    Protected Sub useradmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles useradmin.Click
        connection.Open()
        cmdnew = New SqlCommand("select lobid,deptid,clientid,adminid,usertype,adminname from masteradmin where adminid='" & Session("userid") & "'", connection)
        rdr = cmdnew.ExecuteReader
        lbmsg.Text = "Admin"
        If rdr.Read Then
            Session("typeofuser") = "Admin"
            Session("userid") = rdr("adminid")
            Session("username") = rdr("adminname")
            Session("logintime") = System.DateTime.Now
            Session("usertype") = LCase(rdr("UserType"))
            Session("deptid") = rdr("deptid")
            Session("clientid") = rdr("clientid")
            Session("lobid") = rdr("lobID")
            'End If
            useradmin.Visible = False
            useradmin1.Visible = True
            Session("adminchk") = False
            Session("userchk") = True
            'Response.Redirect("../Misc/Home.aspx")
        End If
        rdr.Close()
        connection.Close()
        cmdnew.Dispose()
        Dim cmdget As SqlCommand
        Dim AllLinkButtons As LinkButton() = {Me.lb1, Me.lb2, Me.lb3, Me.lb4, Me.lb5, Me.lb6, Me.lb7, Me.lb8, Me.lb9}
        '''''''''''''''''''' Main Menu Starts'''''''''''''''''''''''''''''
        connection.Open()
        If (Session("typeofuser") = "Admin") Then

            cmdget = New SqlCommand("select distinct a.menuid, a.MenuDescription as menuname, a.URLLink as menureff  from nlvl_menu as a, nlvl_menu_rights as b where  b.userid='" + Session("userid").ToString + "' and b.MenuId=a.MenuId and a.parentid='0' and b.usertype='2' order by MenuDescription", connection)
        Else
            cmdget = New SqlCommand("select distinct(a.MenuDescription) as menuname, a.URLLink as menureff,a.menuid  from nlvl_menu as a, nlvl_menu_rights as b where b.userid='" + Session("userid").ToString + "' and b.MenuId=a.MenuId and a.parentid='0' and b.usertype='1'  order by a.MenuDescription", connection)
        End If
        'connection.Open()
        Dim ad As New SqlDataAdapter
        Dim ds As New DataSet
        ad.SelectCommand = cmdget
        ad.Fill(ds, "abc")
        Dim c As Integer
        c = ds.Tables("abc").Rows.Count

        For k = 0 To c - 1
            AllLinkButtons(k).Text = ""
            AllLinkButtons(k).PostBackUrl = ""
            AllLinkButtons(k).ToolTip = ""
        Next
        connection.Close()
        cmdget.Dispose()

        Dim ds1 As New DataSet
        connection.Open()
        If Session("typeofuser") = "Admin" Then

            cmdget = New SqlCommand("select distinct menudescription,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='0' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='2' order by nlvl_menu.orderby ", connection)
            'cmd = New SqlCommand("select distinct menudescription,URLLink from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "'", connection)
        Else
            cmdget = New SqlCommand("select distinct menudescription,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='0' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='1' order by nlvl_menu.orderby ", connection)
        End If
        'connection.Open()
        ad.SelectCommand = cmdget
        ad.Fill(ds1, "abc")
        c = ds1.Tables("abc").Rows.Count
        For k = 0 To c - 1
            AllLinkButtons(k).Visible = True
            AllLinkButtons(k).Text = Trim(ds.Tables("abc").Rows(k)("menuname").ToString())
            AllLinkButtons(k).PostBackUrl = Trim(ds.Tables("abc").Rows(k)("menureff").ToString()) + "?val=" + Trim(ds.Tables("abc").Rows(k)("menuid").ToString())
            AllLinkButtons(k).ToolTip = Trim(ds.Tables("abc").Rows(k)("menuname").ToString())
        Next
        connection.Close()
        cmdget.Dispose()
        '''''''''''''''''''' Left Pain Ends'''''''''''''''''''''''''''''''''''
        menu.Nodes.Clear()
        If Trim(Request("val")).ToString() <> Nothing Then '<> not equal to sign
            Dim cmd As New SqlCommand

            If Trim(Request("val")).ToString() = "5" Or Trim(Request("val")).ToString() = "103" Or Trim(Request("val")).ToString() = "2" Then
                Me.LeftPlaceHolder.Visible = True
                divTree.Visible = False
            Else
                Me.LeftPlaceHolder.Visible = False
            End If
            connection.Open()
            If Session("typeofuser") = "Admin" Then
                cmd = New SqlCommand("select distinct menudescription,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "' order by nlvl_menu.orderby ", connection)
            Else
                cmd = New SqlCommand("select distinct menudescription,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "' and nlvl_menu_rights.userid='" + Session("userid1") + "' order by nlvl_menu.orderby ", connection)
            End If
            Dim dr1 As SqlDataReader
            'connection.Open()
            Dim nodeaccount As System.Web.UI.WebControls.TreeNode

            dr1 = cmd.ExecuteReader
            While dr1.Read()

                nodeaccount = New TreeNode()
                nodeaccount.Text = dr1("menudescription")
                nodeaccount.NavigateUrl = "/QlickReport" + dr1("URLLink") + "?val=" + Request("val")
                nodeaccount.ToolTip = dr1("menudescription")
                menu.Nodes.Add(nodeaccount)

            End While
            connection.Close()
        End If
    End Sub

    Protected Sub useradmin1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles useradmin1.Click
        connection.Open()
        cmdnew = New SqlCommand("select userid,usertype,username,pwd,deptid,clientid,lobid from registration where userid='" & Session("userid") + "'", connection)
        'connection.Open()
        rdr = cmdnew.ExecuteReader
        If rdr.Read Then

            Session("typeofuser") = "User"
            Session("userid") = rdr("userid")

            Session("username") = rdr("username")
            Session("logintime") = System.DateTime.Now
            Session("usertype") = LCase(rdr("UserType"))
            Session("deptid") = rdr("deptid")
            Session("clientid") = rdr("clientid")
            Session("lobid") = rdr("lobID")
            'End If
            rdr.Close()
            'connection.Close()
            Session("adminchk") = True
            Session("userchk") = False
            'Response.Redirect("../Misc/Home.aspx")
        End If
        rdr.Close()
        useradmin1.Visible = False
        useradmin.Visible = True
        lbmsg.Text = "User"
        Dim cmdget As SqlCommand
        Dim AllLinkButtons As LinkButton() = {Me.lb1, Me.lb2, Me.lb3, Me.lb4, Me.lb5, Me.lb6, Me.lb7, Me.lb8, Me.lb9}
        '''''''''''''''''''' Main Menu Starts'''''''''''''''''''''''''''''
        If (Session("typeofuser") = "Admin") Then

            cmdget = New SqlCommand("select distinct a.menuid, a.MenuDescription as menuname, a.URLLink as menureff,a.orderby  from nlvl_menu as a, nlvl_menu_rights as b where  b.userid='" + Session("userid").ToString + "' and b.MenuId=a.MenuId and a.parentid='0' and b.usertype='2' order by MenuDescription", connection)
        Else
            cmdget = New SqlCommand("select distinct(a.MenuDescription) as menuname, a.URLLink as menureff,a.menuid,a.orderby  from nlvl_menu as a, nlvl_menu_rights as b where b.userid='" + Session("userid").ToString + "' and b.MenuId=a.MenuId and a.parentid='0' and b.usertype='1'  order by a.MenuDescription", connection)
        End If
        'connection.Open()
        Dim ad As New SqlDataAdapter
        Dim ds As New DataSet
        ad.SelectCommand = cmdget
        ad.Fill(ds, "abc")
        Dim c As Integer
        c = ds.Tables("abc").Rows.Count

        For k = 0 To c - 1
            AllLinkButtons(k).Text = ""
            AllLinkButtons(k).PostBackUrl = ""
            AllLinkButtons(k).ToolTip = ""
        Next
        connection.Close()
        cmdget.Dispose()
        Dim ds1 As New DataSet
        connection.Open()
        If Session("typeofuser") = "Admin" Then

            cmdget = New SqlCommand("select distinct menudescription,URLLink,orderby  from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='0' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='2' order by nlvl_menu.orderby ", connection)
            'cmd = New SqlCommand("select distinct menudescription,URLLink from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "'", connection)
        Else
            cmdget = New SqlCommand("select distinct menudescription,URLLink,orderby  from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='0' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='1' order by nlvl_menu.orderby ", connection)
        End If
        'connection.Open()
        ad.SelectCommand = cmdget
        ad.Fill(ds1, "abc")
        c = ds1.Tables("abc").Rows.Count
        For k = 0 To c - 1
            AllLinkButtons(k).Visible = True
            AllLinkButtons(k).Text = Trim(ds.Tables("abc").Rows(k)("menuname").ToString())
            AllLinkButtons(k).PostBackUrl = Trim(ds.Tables("abc").Rows(k)("menureff").ToString()) + "?val=" + Trim(ds.Tables("abc").Rows(k)("menuid").ToString())
            AllLinkButtons(k).ToolTip = Trim(ds.Tables("abc").Rows(k)("menuname").ToString())
        Next
        connection.Close()
        cmdget.Dispose()

        '''''''''''''''''''' Left Pain Ends'''''''''''''''''''''''''''''''''''
        menu.Nodes.Clear()
        connection.Open()
        If Trim(Request("val")).ToString() <> Nothing Then '<> not equal to sign
            Dim cmd As New SqlCommand
            If Trim(Request("val")).ToString() = "5" Or Trim(Request("val")).ToString() = "103" Or Trim(Request("val")).ToString() = "2" Then
                Me.LeftPlaceHolder.Visible = True
                divTree.Visible = False
            Else
                Me.LeftPlaceHolder.Visible = False
            End If
            If Session("typeofuser") = "Admin" Then
                cmd = New SqlCommand("select distinct menudescription,URLLink,orderby  from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "' order by nlvl_menu.orderby ", connection)
            Else
                cmd = New SqlCommand("select distinct menudescription,URLLink,orderby  from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "' and nlvl_menu_rights.userid='" + Session("userid1") + "' order by nlvl_menu.orderby ", connection)
            End If
            Dim dr1 As SqlDataReader
            'connection.Open()
            Dim nodeaccount As System.Web.UI.WebControls.TreeNode
            dr1 = cmd.ExecuteReader
            While dr1.Read()

                nodeaccount = New TreeNode()
                nodeaccount.Text = dr1("menudescription")
                nodeaccount.NavigateUrl = "/QlickReport" + dr1("URLLink") + "?val=" + Request("val")
                nodeaccount.ToolTip = dr1("menudescription")
                menu.Nodes.Add(nodeaccount)

            End While
        End If
        connection.Close()
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("~\Graphs\graph.aspx")
    End Sub

End Class

