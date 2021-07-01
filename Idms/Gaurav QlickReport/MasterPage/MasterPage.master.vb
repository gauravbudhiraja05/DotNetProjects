Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Math
Imports System.Net.Sockets
Imports System.Reflection
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim con As String = AppSettings("ConnectionString")
    Dim parentNodeNo As Integer = 0
    Dim superParentNo As Integer = 0
    Dim fourthNodeNo As Integer = 0
    Dim childNodeNo As Integer = 0
    Dim counter As Integer = 0
    Dim parentNode, superParent, fourthNode As System.Web.UI.WebControls.TreeNode
    Dim childNode As System.Web.UI.WebControls.TreeNode
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
        '' to bind url's at homepage
        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                treetable.Visible = True
            End If
        End If
        connection.Close()
        'Dim cmdnews = New SqlCommand("select * from manunews where startdate <= getdate() and enddate >= getdate()", connection)
        'connection.Close()
        'connection.Open()
        'Dim adnw As New SqlDataAdapter
        'Dim dsnw As New DataSet
        'adnw.SelectCommand = cmdnews
        'adnw.Fill(dsnw, "news")
        'connection.Close()
        'adnw.Dispose()
        'cmdnews.Dispose()
        'Dim cnw = dsnw.Tables("news").Rows.Count
        'Dim w = 0
        'Dim htbl As New HtmlTable
        'htbl.Style.Add("margin-left", "5px")
        'For w = 0 To cnw - 1
        '    Dim hrow As New HtmlTableRow
        '    Dim hcell As New HtmlTableCell
        '    Dim hcell1 As New HtmlTableCell
        '    Dim lnk As New HtmlAnchor
        '    Dim imln As New HtmlImage
        '    imln.Src = "../images/arrow.gif"
        '    imln.Alt = "Arrow"
        '    lnk.Target = "_new"
        '    lnk.InnerHtml = dsnw.Tables("news").Rows(w).Item("HeadLine")
        '    lnk.Style.Add("color", "Blue")
        '    Dim nur = dsnw.Tables("news").Rows(w).Item("NewsURL")
        '    If LCase(nur).contains("http://") = False Then
        '        nur = "http://" + nur
        '    End If
        '    lnk.HRef = nur
        '    lnk.Title = dsnw.Tables("news").Rows(w).Item("News")
        '    hcell1.Controls.Add(imln)
        '    hcell.Controls.Add(lnk)
        '    hrow.Controls.Add(hcell1)
        '    hrow.Controls.Add(hcell)
        '    htbl.Controls.Add(hrow)
        'Next
        'pnlURL.Controls.Add(htbl)


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
        'menu.Nodes.Clear()

        If (Page.IsPostBack = False) Then
            If Trim(Request("val")).ToString() <> Nothing Then '<> not equal to sign
                Dim cmd As New SqlCommand
                If Trim(Request("val")).ToString() = "5" Or Trim(Request("val")).ToString() = "103" Or Trim(Request("val")).ToString() = "2" Then
                    Me.LeftPlaceHolder.Visible = True
                    divTree.Visible = False
                ElseIf Trim(Request("val")).ToString() = "71" Then  '' To fill BLT Treeview
                    hidNode.Value = "BLT"
                    Dim nodeblt, nodemaster, nodeentry As System.Web.UI.WebControls.TreeNode
                    Dim rowblt, rowmaster, rowentry As DataRow                        '

                    Dim objds1 As New DataSet
                    Dim daLob1 As New SqlDataAdapter
                    If Session("typeofuser") = "Admin" Then

                        daLob1 = New SqlDataAdapter("select distinct menudescription,  nlvl_menu.menuid as menuid,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='2' order by nlvl_menu.orderby ", connection)
                        'cmd = New SqlCommand("select distinct menudescription,URLLink from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "'", connection)
                    ElseIf Session("typeofuser") = "Super Admin" Then
                        daLob1 = New SqlDataAdapter("select distinct menudescription, nlvl_menu.menuid as menuid,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='3' order by nlvl_menu.orderby ", connection)
                    Else

                        daLob1 = New SqlDataAdapter("select distinct menudescription,  nlvl_menu.menuid as menuid,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='1' order by nlvl_menu.orderby ", connection)
                    End If
                    daLob1.Fill(objds1, "dtclient1")
                    For Each rowblt In objds1.Tables("dtclient1").Rows
                        nodeblt = New TreeNode
                        nodeblt.Text = rowblt("menudescription")
                        nodeblt.ToolTip = rowblt("menudescription")

                        nodeblt.Value = rowblt("menuid")
                        BFI.Nodes.Add(nodeblt)
                        'nodeblt.NavigateUrl = rowblt("URLLink") & "?val=71"
                        Dim daclient1 As New SqlDataAdapter
                        If Session("typeofuser") = "Admin" Then

                            daclient1 = New SqlDataAdapter("select distinct menudescription, nlvl_menu.menuid as menuid,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid = '" + nodeblt.Value + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='2' order by nlvl_menu.orderby ", connection)
                            'cmd = New SqlCommand("select distinct menudescription,URLLink from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "'", connection)
                        ElseIf Session("typeofuser") = "Super Admin" Then
                            daclient1 = New SqlDataAdapter("select distinct menudescription, nlvl_menu.menuid as menuid,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid = '" + nodeblt.Value + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='3' order by nlvl_menu.orderby ", connection)
                        Else

                            daclient1 = New SqlDataAdapter("select distinct menudescription, nlvl_menu.menuid as menuid,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid = '" + nodeblt.Value + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='1' order by nlvl_menu.orderby ", connection)
                        End If

                        Dim dsClient1 As New DataSet()
                        connection.Open()

                        daclient1.Fill(dsClient1, "dtclient")
                        connection.Close()
                        For Each rowmaster In dsClient1.Tables("dtclient").Rows
                            'If rowdept("autoid") = rowclient("DeptId") Then
                            nodemaster = New TreeNode

                            nodemaster.Text = rowmaster("menudescription")
                            nodemaster.ToolTip = rowmaster("menudescription")

                            nodemaster.Value = rowmaster("menuid")
                            nodeblt.ChildNodes.Add(nodemaster)
                            ' nodemaster.NavigateUrl = rowmaster("URLLink") & "?val=71"
                            'nodeclient.Target = "frmFormTarget"
                            ' End If

                            If (Convert.ToInt32(rowmaster("menuid")) = 76 Or Convert.ToInt32(rowmaster("menuid")) = 77) Then
                                ' nodemaster.NavigateUrl = rowmaster("URLLink") & "&val=71"
                            Else
                                ' nodemaster.NavigateUrl = rowmaster("URLLink") & "?val=71"
                            End If

                            Dim daLob2 As New SqlDataAdapter
                            If Session("typeofuser") = "Admin" Then

                                daLob2 = New SqlDataAdapter("select distinct menudescription, nlvl_menu.menuid as menuid,URLLink from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid = '" + nodemaster.Value + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='2'", connection)
                                'cmd = New SqlCommand("select distinct menudescription,URLLink from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid ='" + Request("val") + "'", connection)
                            ElseIf Session("typeofuser") = "Super Admin" Then
                                daLob2 = New SqlDataAdapter("select distinct menudescription, nlvl_menu.menuid as menuid,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid = '" + nodemaster.Value + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='3'", connection)
                            Else

                                daLob2 = New SqlDataAdapter("select distinct menudescription, nlvl_menu.menuid as menuid,URLLink,orderby from nlvl_menu,nlvl_menu_rights where nlvl_menu.menuid=nlvl_menu_rights.menuid and nlvl_menu_rights.parentid = '" + nodemaster.Value + "' and nlvl_menu_rights.userid='" + Session("userid") + "' and nlvl_menu_rights.usertype='1'", connection)
                            End If

                            Dim dslob2 As New DataSet()
                            connection.Open()
                            daLob2.Fill(dslob2, "dtLob")
                            connection.Close()
                            For Each rowentry In dslob2.Tables("dtLob").Rows
                                ' If rowdept("autoid") = rowLob("DeptId") And rowclient("autoid") = rowLob("ClientId") Then
                                nodeentry = New TreeNode

                                nodeentry.Text = rowentry("menudescription")
                                nodeentry.ToolTip = rowentry("menudescription")
                                nodeentry.Value = rowentry("menuid")
                                nodemaster.ChildNodes.Add(nodeentry)
                                ' nodeentry.NavigateUrl = rowentry("URLLink") & "?val=71"

                                ' End If

                            Next
                            daLob2.Dispose()
                            dslob2.Dispose()

                        Next
                        daclient1.Dispose()
                        dsClient1.Dispose()
                        ' connection.Dispose()
                        ' nodeblt.NavigateUrl = ""

                    Next
                    objds1.Dispose()
                    daLob1.Dispose()

                    connection.Close()
                    'connection.Dispose()
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
            Else
                CallAppropriateMenu()
                Try

                    Dim dadepartment As SqlDataAdapter
                    Dim objds As New DataSet
                    If (Session("typeofuser") = "Super Admin") Then
                        dadepartment = New SqlDataAdapter("select autoid,isnull(departmentname,'') as departmentname,isnull(message,'') as message from idmsdepartment where SavedBy='" + Session("userid").ToString + "'  order by departmentname ", connection)
                    Else
                        dadepartment = New SqlDataAdapter("select autoid,isnull(departmentname,'') as departmentname,isnull(message,'') as message from idmsdepartment where SavedBy='" + Session("CreatedBy").ToString() + "'  order by departmentname ", connection)
                    End If

                    Dim daTable As New SqlDataAdapter("select * from warslobtablemaster order by tablename", connection)
                    dadepartment.Fill(objds, "dtdepartment")
                    daTable.Fill(objds, "dtTable")
                    connection.Close()

                    Dim nodebfi, nodedept, nodeclient, nodelob, nodetable As System.Web.UI.WebControls.TreeNode
                    Dim rowdept, rowclient, rowLob, rowTable As DataRow
                    nodebfi = New TreeNode
                    nodebfi.Text = "DASHBOARD"
                    nodebfi.ToolTip = "DASHBOARD"
                    hidNode.Value = "BFI"
                    BFI.Nodes.Add(nodebfi)
                    For Each rowdept In objds.Tables("dtdepartment").Rows
                        nodedept = New TreeNode
                        If rowdept("message") <> "" Then
                            nodedept.Text = rowdept("departmentname")
                            nodedept.ToolTip = rowdept("departmentname")
                        Else
                            nodedept.Text = rowdept("departmentname")
                            nodedept.ToolTip = rowdept("departmentname")
                        End If
                        nodedept.Value = rowdept("autoid")
                        nodebfi.ChildNodes.Add(nodedept)
                        ' url = "../Misc/DispQuery.aspx?Department=" & nodedept.Value & "&Client=&LOB=0" & "&Table="
                        'nodedept.NavigateUrl = "javascript:document.frames['frmTarget'].location.href='" & url & "';"

                        ' url = "../Misc/DispQuery.aspx?Department=" & nodedept.Value & "&Client=0" & "&LOB=0" & "&Table="
                        Dim daclient As New SqlDataAdapter("select autoid,DeptId,isnull(clientname,'') as clientname,isnull(message,'') as message from idmsclient where DeptId='" & nodedept.Value & "' order by clientname", connection)
                        Dim dsClient As New DataSet()
                        connection.Open()

                        daclient.Fill(dsClient, "dtclient")
                        connection.Close()
                        For Each rowclient In dsClient.Tables("dtclient").Rows
                            'If rowdept("autoid") = rowclient("DeptId") Then
                            nodeclient = New TreeNode
                            If rowclient("message") <> "" Then
                                nodeclient.Text = rowclient("clientname")
                                nodeclient.ToolTip = rowclient("clientname")
                            Else
                                nodeclient.Text = rowclient("clientname")
                                nodeclient.ToolTip = rowclient("clientname")
                            End If
                            nodeclient.Value = rowclient("autoid")
                            nodedept.ChildNodes.Add(nodeclient)
                            'url = "../Misc/DispQuery.aspx?Department=" & nodedept.Value & "&Client=" & nodeclient.Value & "&LOB=0" & "&Table="
                            'nodeclient.NavigateUrl = "javascript:document.frames['frmTarget'].location.href='" & url & "';"

                            ' End If
                            Dim daLob As New SqlDataAdapter("select autoid,DeptId,ClientId,isnull(LOBName,'') as LOB,isnull(description,'') as message from warslobmaster where DeptId='" & nodedept.Value & "' and ClientId='" & nodeclient.Value & "' order by LOB", connection)
                            Dim dslob As New DataSet()
                            connection.Open()
                            daLob.Fill(dslob, "dtLob")
                            connection.Close()
                            For Each rowLob In dslob.Tables("dtLob").Rows
                                ' If rowdept("autoid") = rowLob("DeptId") And rowclient("autoid") = rowLob("ClientId") Then
                                nodelob = New TreeNode
                                If rowLob("message") <> "" Then
                                    nodelob.Text = rowLob("LOB")
                                    nodelob.ToolTip = rowLob("LOB")
                                Else
                                    nodelob.Text = rowLob("LOB")
                                    nodelob.ToolTip = rowLob("LOB")
                                End If
                                nodelob.Value = rowLob("autoid")
                                nodeclient.ChildNodes.Add(nodelob)
                                ' url = "../Misc/DispQuery.aspx?Department=" & nodedept.Value & "&Client=" & nodeclient.Value & "&LOB=" & nodelob.Value & "&Table="
                                ' nodelob.NavigateUrl = "javascript:callme();"
                                ' End If

                            Next
                            daLob.Dispose()
                            dslob.Dispose()

                        Next
                        daclient.Dispose()
                        dsClient.Dispose()
                        ' connection.Dispose()

                        'nodedept.Target = "_frmTarget"
                        'nodedept.NavigateUrl = "../Misc/DispQuery.aspx?Department=" & nodedept.Value & "&Client=0&LOB=0" & "&Table="
                        ' url = "../Misc/DisplayQuery.aspx?Department=" & nodedept.Value & "&Client=0&LOB=0" & "&Table="
                        'nodedept.NavigateUrl = "javascript:callme();"

                    Next
                    objds.Dispose()
                    dadepartment.Dispose()

                    connection.Close()
                    connection.Dispose()

                Catch ex As Exception
                    Dim strmsg As String
                    strmsg = Replace(ex.Message.ToString, "'", "")
                    strmsg = Replace(strmsg, vbCrLf, " ")
                    WARSShowMsg(strmsg)
                End Try
            End If
        End If
    End Sub
    Private Sub CallAppropriateMenu()
        If Trim(Request("Link")) <> "" Then
            IsurvAppropriateMenu(Session("typeofuser"), Request("Link"))
        End If
    End Sub
    Public Function IsurvAppropriateMenu(ByVal UserType As String, ByVal MainLinkId As String)
        Try
            If Trim(UserType) <> "" Then
                Dim slink, COUNT
                COUNT = 0
                Dim str

                Select Case Trim(UserType)


                    Case "member"

                        str = "select a.* from nlvl_menu a, nlvl_menu_rights b where a.menuid=b.menuid and b.userid='" & Session("userid") & "' and b.parentid='" & Trim(MainLinkId) & "' union select * from nlvl_menu b where b.menuid in (select a.menuid from nlvl_menu_rights a, nlvl_menu_rights c where a.userid='" & Session("userid") & "' and c.Menuid=a.Parentid and c.parentid='" & Trim(MainLinkId) & "')"
                        createUsualMenu(str)

                    Case "superadmin"
                        If Trim(MainLinkId) = "8" Then

                            str = "select  * from nlvl_menu A where A.parentid =" & Trim(MainLinkId) & " or A.parentid in(select menuid from nlvl_menu  where parentid=" & Trim(MainLinkId) & ") or A.parentid in (select menuid from nlvl_menu where parentid in(select menuid from nlvl_menu where parentid=" & Trim(MainLinkId) & ")) order by orderby"
                            forLobtasking(str)
                        Else
                            str = "select  * from nlvl_menu A where A.parentid =" & Trim(MainLinkId) & " or A.parentid in(select menuid from nlvl_menu  where parentid=" & Trim(MainLinkId) & ") or A.parentid in (select menuid from nlvl_menu where parentid in(select menuid from nlvl_menu where parentid=" & Trim(MainLinkId) & ")) order by orderby"
                            createUsualMenu(str)
                        End If
                End Select
            Else
                Response.Redirect("/" & Session("projName") & "/")
            End If
        Catch ex As Exception
            'Response.Write(ex)ss
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try

    End Function
    Public Function createUsualMenu(ByVal strtxt As String)
        Dim cmd As New SqlCommand(strtxt, connection)
        ' '' '' ''select  * from nlvl_menu A where A.parentid =5 or A.parentid in(select menuid from nlvl_menu where parentid=5) order by orderby
        Dim dr As SqlDataReader
        connection.Open()
        dr = cmd.ExecuteReader

        While dr.Read

            If Trim(dr("level")) = "1" Then

                If chkParent(dr("menuid")) = True Then
                    'slink = slink & "<tr><td WIDTH=12></td><td nowrap><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    parentNode = New System.Web.UI.WebControls.TreeNode
                    parentNode.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    menu.Nodes.Add(parentNode)
                    parentNodeNo = parentNodeNo + 1

                Else
                    '' '' ''slink = slink & "<tr><td WIDTH=12></td><td nowrap><a onClick='Toggle(this)'><img src='images/plus.gif' HEIGHT=9><font color=white>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    parentNode = New System.Web.UI.WebControls.TreeNode
                    parentNode.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    parentNode.NavigateUrl = dr("urllink")
                    parentNode.Target = "frmFormTarget"
                    menu.Nodes.Add(parentNode)
                    parentNodeNo = parentNodeNo + 1

                End If


            ElseIf Trim(dr("level")) = "2" Then
                ' '' '' ''slink = slink & "<tr><td colspan=2><table border=0 width=100% cellspacing=1 cellpadding=1 align=center><tr><td WIDTH=12></td><td nowrap><img SRC='images/leaf.gif' HEIGHT=9>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2>" & dr("MenuDescription") & "</font><div></div></td></tr></table></td></tr>"
                childNode = New System.Web.UI.WebControls.TreeNode
                childNode.Text = "<font color=white face=verdana size=2>" + dr("MenuDescription") + "</font>"
                childNode.NavigateUrl = dr("urllink")
                childNode.Target = "frmFormTarget"

                menu.Nodes(parentNodeNo - 1).ChildNodes.Add(childNode)
                childNodeNo = childNodeNo + 1

            ElseIf Trim(dr("level")) = "3" Then

                fourthNode = New System.Web.UI.WebControls.TreeNode
                fourthNode.Text = "<font color=white face=verdana size=2>" + dr("MenuDescription") + "</font>"
                fourthNode.NavigateUrl = dr("urllink")
                fourthNode.Target = "frmFormTarget"
                menu.Nodes(parentNodeNo - 1).ChildNodes(childNodeNo - 1).ChildNodes.Add(fourthNode)
            End If
        End While
        '' '' ''slink = slink & "</table>"

        dr.Close()
        connection.Close()

    End Function

    Public Function forLobtasking(ByVal strtxt As String)
        Dim cmd As New SqlCommand(strtxt, connection)
        '' '' ''select  * from nlvl_menu A where A.parentid =5 or A.parentid in(select menuid from nlvl_menu where parentid=5) order by orderby
        Dim dr As SqlDataReader
        connection.Open()
        dr = cmd.ExecuteReader

        While dr.Read

            If Trim(dr("level")) = "1" Then
                parentNodeNo = 0
                ''''''''to add crt , support and sampark as superparents
                If chkParent(dr("menuid")) = True Then
                    '' '' ''slink = slink & "<tr><td WIDTH=12></td><td nowrap><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    superParent = New System.Web.UI.WebControls.TreeNode
                    superParent.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    superParent.ToolTip = dr("MenuDescription")
                    menu.Nodes.Add(superParent)
                    superParentNo = superParentNo + 1

                Else
                    '' '' ''slink = slink & "<tr><td WIDTH=12></td><td nowrap><a onClick='Toggle(this)'><img src='images/plus.gif' HEIGHT=9><font color=white>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    superParent = New System.Web.UI.WebControls.TreeNode
                    superParent.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    superParent.NavigateUrl = dr("urllink")
                    superParent.ToolTip = dr("MenuDescription")
                    superParent.Target = "frmFormTarget"
                    menu.Nodes.Add(superParent)
                    superParentNo = superParentNo + 1

                End If

                '''''''''''''''''''''''''to add crt , support and sampark as superparents end
            ElseIf Trim(dr("level")) = "2" Then
                If chkParent(dr("menuid")) = True Then
                    '' '' ''slink = slink & "<tr><td WIDTH=12></td><td nowrap><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    parentNode = New System.Web.UI.WebControls.TreeNode
                    parentNode.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    parentNode.ToolTip = dr("MenuDescription")
                    parentNode.NavigateUrl = dr("urllink")
                    parentNode.Target = "frmFormTarget"
                    menu.Nodes(superParentNo - 1).ChildNodes.Add(parentNode)
                    'menu.Nodes.Add(parentNode)
                    parentNodeNo = parentNodeNo + 1
                    counter = counter + 1
                    If counter = 5 Then
                        Dim str1 As String = "asdfsd"
                    End If
                Else
                    '' '' ''slink = slink & "<tr><td WIDTH=12></td><td nowrap><a onClick='Toggle(this)'><img src='images/plus.gif' HEIGHT=9><font color=white>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    parentNode = New System.Web.UI.WebControls.TreeNode
                    parentNode.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    parentNode.NavigateUrl = dr("urllink")
                    parentNode.ToolTip = dr("MenuDescription")
                    parentNode.Target = "frmFormTarget"
                    '' '' ''menu.Nodes.Add(parentNode)
                    menu.Nodes(superParentNo - 1).ChildNodes.Add(parentNode)
                    parentNodeNo = parentNodeNo + 1


                End If


            ElseIf Trim(dr("level")) = "3" Then
                ' '' '' ''slink = slink & "<tr><td colspan=2><table border=0 width=100% cellspacing=1 cellpadding=1 align=center><tr><td WIDTH=12></td><td nowrap><img SRC='images/leaf.gif' HEIGHT=9>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2>" & dr("MenuDescription") & "</font><div></div></td></tr></table></td></tr>"
                childNode = New System.Web.UI.WebControls.TreeNode
                childNode.Text = "<font color=white face=verdana size=2>" + dr("MenuDescription") + "</font>"
                childNode.NavigateUrl = dr("urllink")
                childNode.ToolTip = dr("MenuDescription")
                childNode.Target = "frmFormTarget"
                menu.Nodes(superParentNo - 1).ChildNodes(parentNodeNo - 1).ChildNodes.Add(childNode)
                childNodeNo = childNodeNo + 1


            End If

        End While
        'slink = slink & "</table>"

        dr.Close()
        connection.Close()

    End Function
    Private Function chkParent(ByVal Id)
        Dim conn As New SqlConnection(con)
        Dim com As New SqlCommand("select * from nlvl_menu where parentid=" & Id, conn)
        conn.Open()
        Dim rdr As SqlDataReader
        rdr = com.ExecuteReader
        If rdr.Read Then
            conn.Close()
            com.Dispose()
            rdr.Close()
            Return True
        End If
        conn.Close()
        com.Dispose()
        rdr.Close()
        Return False
    End Function
    Public Sub WARSShowMsg(ByVal strmsg As String)
        'alert function for message display
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"

        '  RegisterStartupScript("showmsg", str)
    End Sub
    Private Function IsurvReturnURLLink(ByVal Id)
        Dim comm As New SqlCommand("select url from Isurvlinkmaster where linkid=" & Id, connection)
        connection.Open()
        Dim rdrURL As SqlDataReader
        rdrURL = comm.ExecuteReader
        If rdrURL.Read Then
            Dim r As String
            r = rdrURL(0)
            connection.Close()
            comm.Dispose()
            rdrURL.Close()
            Return r
        End If
        connection.Close()
        comm.Dispose()
        rdrURL.Close()
        Return False
    End Function
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
    Protected Sub BFI_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BFI.SelectedNodeChanged
        Session("dp") = ""
        Session("cl") = ""
        Session("lb") = ""
        Dim bool As Boolean = False
        Dim depth = BFI.SelectedNode.Depth
        If hidNode.Value = "BFI" Then
            Dim lob = ""
            Dim cli = ""
            Dim dep = ""
            If depth = 3 Then
                lob = BFI.SelectedNode.Value
                cli = BFI.SelectedNode.Parent.Value
                dep = BFI.SelectedNode.Parent.Parent.Value
                Session("dp") = dep
                Session("cl") = cli
                Session("lb") = lob
            ElseIf depth = 2 Then
                cli = BFI.SelectedNode.Value
                dep = BFI.SelectedNode.Parent.Value
                Session("dp") = dep
                Session("cl") = cli
            ElseIf depth = 1 Then
                dep = BFI.SelectedNode.Value
                Session("dp") = dep
            End If
            If dep <> "" Then
                bool = True
                url = "../Misc/DisplayQuery.aspx?Department=" & dep & "&Client=" & cli & "&LOB=" & lob & "&Table="
            End If
        ElseIf hidNode.Value = "BLT" Then
            If depth = 2 Then
                Dim id = BFI.SelectedValue
                Dim cmd As New SqlCommand()
                cmd = New SqlCommand("select URLLink from nlvl_menu where menuid ='" + id + "'", connection)
                connection.Open()
                url = cmd.ExecuteScalar()
                connection.Close()
                cmd.Dispose()
                bool = True
            ElseIf depth = 1 Then
                Dim id = BFI.SelectedValue
                If id = "76" Or id = "77" Then
                    Dim cmd As New SqlCommand()
                    cmd = New SqlCommand("select URLLink from nlvl_menu where menuid ='" + id + "'", connection)
                    connection.Open()
                    url = cmd.ExecuteScalar()
                    connection.Close()
                    cmd.Dispose()
                    bool = True
                End If
            End If
        End If
        If bool = True Then
            Me.ContentPlaceHolder1.Visible = False

            Dim str = "<script language='Javascript'>"
            str = str + "callme();"
            str = str + "</script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Call", str)
        End If

    End Sub

   
End Class

