Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Partial Class TreeviewPage
    Inherits System.Web.UI.Page
    Dim parentNodeNo As Integer = 0
    Dim superParentNo As Integer = 0
    Dim fourthNodeNo As Integer = 0
    Dim childNodeNo As Integer = 0
    Dim parentNode, superParent, fourthNode As System.Web.UI.WebControls.TreeNode
    Dim childNode As System.Web.UI.WebControls.TreeNode
    Dim counter As Integer = 0
    Dim con As String = AppSettings("connectionString")
    Dim objsqlcon As New SqlConnection(con)
    Dim connection As New SqlConnection(con)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = False

        If Me.IsPostBack = False Then
            If Trim(Request("val")).ToString() = "track" Then

                Dim cmd As New SqlCommand("select menudescription,URLLink from nlvl_menu where parentid=68", connection)
                Dim dr1 As SqlDataReader
                connection.Open()
                Dim nodeaccount As System.Web.UI.WebControls.TreeNode

                dr1 = cmd.ExecuteReader
                While dr1.Read

                    nodeaccount = New TreeNode()
                    nodeaccount.Text = dr1("menudescription")
                    nodeaccount.NavigateUrl = dr1("URLLink") + "=" + "Track" + dr1("menudescription") + ".aspx"
                    nodeaccount.Target = "Target"

                    ''menu.Target = ""
                    menu.Nodes.Add(nodeaccount)

                End While
                dr1.Close()
                connection.Close()
            Else
                CallAppropriateMenu()
                Try
                    'WRM()
                    Dim nodeWRM, nodeWRM1, nodeWRM2, nodeWRM3 As TreeNode

                    nodeWRM = New TreeNode
                    nodeWRM.Text = "WRM"
                    WRM.Nodes.Add(nodeWRM)
                    nodeWRM1 = New TreeNode
                    nodeWRM1.Text = "Add Report"
                    nodeWRM.ChildNodes.Add(nodeWRM1)
                    'nodeWRM.Nodes.Add(nodeWRM1)
                    nodeWRM1.Target = "frmFormTarget"
                    nodeWRM1.NavigateUrl = "/IDMS/Menu/ReportDesigner/WFMEntry.aspx"
                    nodeWRM2 = New TreeNode
                    nodeWRM2.Text = "Edit/View Report"
                    nodeWRM.ChildNodes.Add(nodeWRM2)
                    nodeWRM2.Target = "frmFormTarget"
                    nodeWRM2.NavigateUrl = "/IDMS/Menu/ReportDesigner/WRMlist.aspx"
                    nodeWRM3 = New TreeNode
                    nodeWRM3.Text = "HTML Reports"
                    nodeWRM.ChildNodes.Add(nodeWRM3)
                    nodeWRM3.Target = "frmFormTarget"
                    nodeWRM3.NavigateUrl = "/IDMS/Menu/ReportDesigner/WRMhtml.aspx"


                    Dim objds As New DataSet
                    Dim dadepartment As New SqlDataAdapter("select autoid,isnull(departmentname,'') as departmentname,isnull(message,'') as message from idmsdepartment order by departmentname", objsqlcon)
                    Dim daclient As New SqlDataAdapter("select autoid,DepartmentId,isnull(clientname,'') as clientname,isnull(message,'') as message from idmsclient order by clientname", objsqlcon)
                    Dim daLob As New SqlDataAdapter("select autoid,DepartmentId,ClientId,isnull(LOB,'') as LOB,isnull(description,'') as message from warslobmaster order by LOB", objsqlcon)
                    Dim daTable As New SqlDataAdapter("select * from warslobtablemaster order by tablename", objsqlcon)
                    dadepartment.Fill(objds, "dtdepartment")
                    daclient.Fill(objds, "dtclient")
                    daLob.Fill(objds, "dtLob")
                    daTable.Fill(objds, "dtTable")
                    objsqlcon.Close()

                    Dim nodebfi, nodedept, nodeclient, nodelob, nodetable As System.Web.UI.WebControls.TreeNode
                    Dim rowdept, rowclient, rowLob, rowTable As DataRow
                    nodebfi = New TreeNode
                    nodebfi.Text = "BFI"
                    BFI.Nodes.Add(nodebfi)
                    For Each rowdept In objds.Tables("dtdepartment").Rows
                        nodedept = New TreeNode
                        If rowdept("message") <> "" Then
                            nodedept.Text = rowdept("departmentname") & " (" & rowdept("message") & ")"
                        Else
                            nodedept.Text = rowdept("departmentname")
                        End If
                        nodedept.Value = rowdept("autoid")
                        nodebfi.ChildNodes.Add(nodedept)
                        For Each rowclient In objds.Tables("dtclient").Rows
                            If rowdept("autoid") = rowclient("DepartmentId") Then
                                nodeclient = New TreeNode
                                If rowclient("message") <> "" Then
                                    nodeclient.Text = rowclient("clientname") & " (" & rowclient("message") & ")"
                                Else
                                    nodeclient.Text = rowclient("clientname")
                                End If
                                nodeclient.Value = rowclient("autoid")
                                nodedept.ChildNodes.Add(nodeclient)
                                nodeclient.NavigateUrl = "/IDMS/FrameMenu/DispQuery.aspx?Department=" & nodedept.Value & "&Client=" & nodeclient.Value & "&LOB=0" & "&Table="
                                nodeclient.Target = "frmFormTarget"
                            End If
                            For Each rowLob In objds.Tables("dtLob").Rows
                                If rowdept("autoid") = rowLob("DepartmentId") And rowclient("autoid") = rowLob("ClientId") Then
                                    nodelob = New TreeNode
                                    If rowLob("message") <> "" Then
                                        nodelob.Text = rowLob("LOB") & " (" & rowLob("message") & ")"
                                    Else
                                        nodelob.Text = rowLob("LOB")
                                    End If
                                    nodelob.Value = rowLob("autoid")
                                    nodeclient.ChildNodes.Add(nodelob)
                                    nodelob.NavigateUrl = "/IDMS/FrameMenu/DispQuery.aspx?Department=" & nodedept.Value & "&Client=" & nodeclient.Value & "&LOB=" & nodelob.Value & "&Table="
                                    nodelob.Target = "frmFormTarget"
                                End If

                            Next
                        Next
                        nodedept.NavigateUrl = "/IDMS/FrameMenu/DispQuery.aspx?Department=" & nodedept.Value & "&Client=0&LOB=0" & "&Table="
                        nodedept.Target = "frmFormTarget"
                    Next
                    objds.Dispose()
                    dadepartment.Dispose()
                    daclient.Dispose()
                    objsqlcon.Close()
                    objsqlcon.Dispose()

                    Dim nodeQRC As TreeNode
                    nodeQRC = New TreeNode
                    nodeQRC.Text = "QRC"
                    QRC.Nodes.Add(nodeQRC)


                Catch ex As Exception
                    Dim strmsg As String
                    strmsg = Replace("Test" & ex.Message.ToString, "'", "")
                    strmsg = Replace(strmsg, vbCrLf, " ")
                    WARSShowMsg(strmsg)


                End Try



            End If
        End If

        '''''''''''changes




    End Sub
    Private Sub CallAppropriateMenu()
        If Trim(Request("Link")) <> "" Then
            IsurvAppropriateMenu(Session("usertype"), Request("Link"))

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
        Dim cmd As New SqlCommand(strtxt, objsqlcon)
        'select  * from nlvl_menu A where A.parentid =5 or A.parentid in(select menuid from nlvl_menu where parentid=5) order by orderby
        Dim dr As SqlDataReader
        objsqlcon.Open()
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
                    'slink = slink & "<tr><td WIDTH=12></td><td nowrap><a onClick='Toggle(this)'><img src='images/plus.gif' HEIGHT=9><font color=white>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    parentNode = New System.Web.UI.WebControls.TreeNode
                    parentNode.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    parentNode.NavigateUrl = dr("urllink")
                    parentNode.Target = "frmFormTarget"
                    menu.Nodes.Add(parentNode)
                    parentNodeNo = parentNodeNo + 1

                End If


            ElseIf Trim(dr("level")) = "2" Then
                'slink = slink & "<tr><td colspan=2><table border=0 width=100% cellspacing=1 cellpadding=1 align=center><tr><td WIDTH=12></td><td nowrap><img SRC='images/leaf.gif' HEIGHT=9>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2>" & dr("MenuDescription") & "</font><div></div></td></tr></table></td></tr>"
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
        'slink = slink & "</table>"

        dr.Close()
        objsqlcon.Close()

    End Function

    Public Function forLobtasking(ByVal strtxt As String)
        Dim cmd As New SqlCommand(strtxt, objsqlcon)
        'select  * from nlvl_menu A where A.parentid =5 or A.parentid in(select menuid from nlvl_menu where parentid=5) order by orderby
        Dim dr As SqlDataReader
        objsqlcon.Open()
        dr = cmd.ExecuteReader

        While dr.Read

            If Trim(dr("level")) = "1" Then
                parentNodeNo = 0
                ''''''''to add crt , support and sampark as superparents
                If chkParent(dr("menuid")) = True Then
                    'slink = slink & "<tr><td WIDTH=12></td><td nowrap><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    superParent = New System.Web.UI.WebControls.TreeNode
                    superParent.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    menu.Nodes.Add(superParent)
                    superParentNo = superParentNo + 1

                Else
                    'slink = slink & "<tr><td WIDTH=12></td><td nowrap><a onClick='Toggle(this)'><img src='images/plus.gif' HEIGHT=9><font color=white>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    superParent = New System.Web.UI.WebControls.TreeNode
                    superParent.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    superParent.NavigateUrl = dr("urllink")
                    superParent.Target = "frmFormTarget"
                    menu.Nodes.Add(superParent)
                    superParentNo = superParentNo + 1

                End If

                '''''''''''''''''''''''''to add crt , support and sampark as superparents end
            ElseIf Trim(dr("level")) = "2" Then
                If chkParent(dr("menuid")) = True Then
                    'slink = slink & "<tr><td WIDTH=12></td><td nowrap><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    parentNode = New System.Web.UI.WebControls.TreeNode
                    parentNode.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"

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
                    'slink = slink & "<tr><td WIDTH=12></td><td nowrap><a onClick='Toggle(this)'><img src='images/plus.gif' HEIGHT=9><font color=white>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2><b>" & dr("MenuDescription") & "</b></font></a><div>"
                    parentNode = New System.Web.UI.WebControls.TreeNode
                    parentNode.Text = "<font color=white face=verdana size=2><b>" + dr("MenuDescription") + "</b></font>"
                    parentNode.NavigateUrl = dr("urllink")
                    parentNode.Target = "frmFormTarget"
                    'menu.Nodes.Add(parentNode)
                    menu.Nodes(superParentNo - 1).ChildNodes.Add(parentNode)
                    parentNodeNo = parentNodeNo + 1


                End If


            ElseIf Trim(dr("level")) = "3" Then
                'slink = slink & "<tr><td colspan=2><table border=0 width=100% cellspacing=1 cellpadding=1 align=center><tr><td WIDTH=12></td><td nowrap><img SRC='images/leaf.gif' HEIGHT=9>&nbsp;<a href='" & dr("urllink") & "' target='frmFormTarget'><font color=white face=verdana size=2>" & dr("MenuDescription") & "</font><div></div></td></tr></table></td></tr>"
                childNode = New System.Web.UI.WebControls.TreeNode
                childNode.Text = "<font color=white face=verdana size=2>" + dr("MenuDescription") + "</font>"
                childNode.NavigateUrl = dr("urllink")
                childNode.Target = "frmFormTarget"
                menu.Nodes(superParentNo - 1).ChildNodes(parentNodeNo - 1).ChildNodes.Add(childNode)
                childNodeNo = childNodeNo + 1

                'ElseIf Trim(dr("level")) = "4" Then

                '    fourthNode = New Microsoft.Web.UI.WebControls.TreeNode
                '    fourthNode.Text = "<font color=white face=verdana size=2>" + dr("MenuDescription") + "</font>"
                '    fourthNode.NavigateUrl = dr("urllink")
                '    fourthNode.Target = "frmFormTarget"
                '    'menu.Nodes(childNodeNo - 1).Nodes.Add(fourthNode)
                '    menu.Nodes(superParentNo - 1).Nodes(parentNodeNo - 1).Nodes(childNodeNo - 1).Nodes.Add(fourthNode)
            End If

        End While
        'slink = slink & "</table>"

        dr.Close()
        objsqlcon.Close()

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

    
End Class
