Imports Microsoft.VisualBasic

Imports System
Imports System.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls

Public Class MenuRight
    Dim department As DropDownList
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim comdepart As New SqlCommand
    ' Dim fun As Functions


    Public Function Bind_Menu()
        ds.Clear()
        comdepart = New SqlCommand("sp_GetMenu", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function Bind_Menuother(ByVal userid As String, ByVal utype As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetMenuOther", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        comdepart.Parameters("@userid").Value = userid
        comdepart.Parameters.Add("@utype", SqlDbType.VarChar, 10)
        comdepart.Parameters("@utype").Value = utype
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function


    Public Function Bind_LinkMenu(ByVal MenuId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetLinkMenu", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@MenuId", SqlDbType.NVarChar, 25)
        comdepart.Parameters("@MenuId").Value = MenuId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function

    Public Function Bind_SubLink1Menu(ByVal MenuId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetSubLink1Menu", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@MenuId", SqlDbType.VarChar, 25)
        comdepart.Parameters("@MenuId").Value = MenuId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function Bind_SubLink2Menu(ByVal MenuId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetSubLink2Menu", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@MenuId", SqlDbType.VarChar, 25)
        comdepart.Parameters("@MenuId").Value = MenuId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function Insert_MenuRights(ByVal MenuId As String, ByVal LinkMenuId() As String, ByVal SubLinkMenuId() As String, ByVal UserId As String, ByVal AssignedBy As String)

        Dim i As Integer
        Dim count As Integer = 0
        Dim count2 As Integer = 0
       
        If (SubLinkMenuId(0) <> "") And (LinkMenuId(0) <> "") Then
            InsertRights(MenuId, 0, UserId, AssignedBy)
            InsertRights(LinkMenuId(0), MenuId, UserId, AssignedBy)

            For i = 0 To SubLinkMenuId.Length - 1
                If (SubLinkMenuId(i) <> "") Then
                    count = count2 + 1
                Else
                    Exit For
                End If
            Next
            For i = 0 To count - 1
                InsertRights(SubLinkMenuId(i), LinkMenuId(0), UserId, AssignedBy)
            Next
        End If

        If (LinkMenuId(0) <> "" And SubLinkMenuId(0) = "") Then
            InsertRights(MenuId, 0, UserId, AssignedBy)

            For i = 0 To LinkMenuId.Length - 1
                If (LinkMenuId(i) <> "") Then
                    count = count + 1
                Else
                    Exit For

                End If
            Next
            For i = 0 To count - 1
                InsertRights(LinkMenuId(i), MenuId, UserId, AssignedBy)
            Next
        End If
        Return 1
    End Function
    Public Function InsertRights(ByVal menuId As String, ByVal ParentId As String, ByVal UserId As String, ByVal AssignedBy As String)



        connection.Open()
        comdepart = New SqlCommand("sp_InsertMenuRight", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@MenuId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@MenuId").Value = menuId

        comdepart.Parameters.Add("@ParentId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ParentId").Value = ParentId

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@Currdate", SqlDbType.DateTime)
        comdepart.Parameters("@Currdate").Value = System.DateTime.Now().ToString("d")

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy



        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function
    Public Function Bind_AssignedMenuRights(ByVal DeptId As String, ByVal ClientId As String, ByVal LobId As String, ByVal loggedId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_AssignedMenuRights11", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@deptId").Value = DeptId

        comdepart.Parameters.Add("@clientId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@clientId").Value = ClientId

        comdepart.Parameters.Add("@lobId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@lobId").Value = LobId

        comdepart.Parameters.Add("@loggedId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@loggedId").Value = loggedId

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function Bind_EditMenuRights(ByVal userid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_BindEditRights", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.VarChar, 25)
        comdepart.Parameters("@userid").Value = userid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function
    Public Function CancelMenuRights(ByVal userid As String, ByVal usertype As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_CancelMenuRights", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@userid", SqlDbType.VarChar, 25)
        comdepart.Parameters("@userid").Value = userid

        comdepart.Parameters.Add("@usertype", SqlDbType.VarChar, 25)
        comdepart.Parameters("@usertype").Value = usertype


        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return ds

    End Function
    Public Function CancelMenuRightsEdit(ByVal autoid As String, ByVal usertype As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_CancelMenuRightsEdit", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@autoid", SqlDbType.VarChar, 25)
        comdepart.Parameters("@autoid").Value = autoid

        comdepart.Parameters.Add("@type", SqlDbType.VarChar, 25)
        comdepart.Parameters("@type").Value = usertype

        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return ds

    End Function
    Public Function CancelMenuRightsRemaining(ByVal userid As String, ByVal usertype As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_CancelMenuRightsRemain", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.VarChar, 25)
        comdepart.Parameters("@userid").Value = userid

        comdepart.Parameters.Add("@usertype", SqlDbType.VarChar, 25)
        comdepart.Parameters("@usertype").Value = usertype
        connection.Open()

        comdepart.ExecuteNonQuery()
        connection.Close()
        Return ds

    End Function
    Public Function Update_MenuRights(ByVal MenuId As String, ByVal LinkMenuId() As String, ByVal SubLinkMenuId() As String, ByVal UserId As String, ByVal AssignedBy As String)

        Dim i As Integer
        Dim count As Integer = 0
        
        If (SubLinkMenuId(0) <> "") And (LinkMenuId(0) <> "") Then
            UpdateRights(MenuId, 0, UserId, AssignedBy)
            UpdateRights(LinkMenuId(0), MenuId, UserId, AssignedBy)

            For i = 0 To SubLinkMenuId.Length - 1
                UpdateRights(SubLinkMenuId(i), LinkMenuId(0), UserId, AssignedBy)
            Next
        End If
        If (LinkMenuId(0) <> "" And SubLinkMenuId(0) = "") Then
            UpdateRights(MenuId, 0, UserId, AssignedBy)

            For i = 0 To LinkMenuId.Length - 1

                If (LinkMenuId(i) <> "") Then
                    count = count + 1
                Else
                    Exit For

                End If
            Next
            For i = 0 To count - 1
                UpdateRights(LinkMenuId(i), MenuId, UserId, AssignedBy)
            Next
        End If
        Return 1
    End Function
    Public Function UpdateRights(ByVal menuId As String, ByVal ParentId As String, ByVal UserId As String, ByVal AssignedBy As String)



        connection.Open()
        comdepart = New SqlCommand("sp_UpdateMenuRight", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@MenuId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@MenuId").Value = menuId

        comdepart.Parameters.Add("@ParentId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ParentId").Value = ParentId

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@Currdate", SqlDbType.DateTime)
        comdepart.Parameters("@Currdate").Value = System.DateTime.Now().ToString("d")

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy



        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function

    Public Function Bind_MenuAdmin(ByVal UserId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetMenuAdmin", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function

    Public Function DeleteUserRights(ByVal menuid As String, ByVal usertype As String, ByVal userid As String)

        connection.Open()
        comdepart = New SqlCommand("sp_deleteRightsAfterPReset", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@Menuid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@MenuId").Value = menuid

        comdepart.Parameters.Add("@type", SqlDbType.VarChar, 50)
        comdepart.Parameters("@type").Value = usertype

        comdepart.Parameters.Add("@userid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@userid").Value = userid


        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function
    Public Function InsertUserRights(ByVal UserId As String, ByVal usertype As String, ByVal assignby As String)

        connection.Open()
        comdepart = New SqlCommand("sp_InsertRightsAfterPReset", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@usertype", SqlDbType.VarChar, 50)
        comdepart.Parameters("@usertype").Value = usertype

        comdepart.Parameters.Add("@assignby", SqlDbType.VarChar, 50)
        comdepart.Parameters("@assignby").Value = assignby

        comdepart.Parameters.Add("@Currdate", SqlDbType.DateTime)
        comdepart.Parameters("@Currdate").Value = System.DateTime.Now().ToString("d")

        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function
    Public Function Bind_LinkMenuAdmin(ByVal UserId As String, ByVal MenuId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetLinkMenuAdmin", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@MenuId", SqlDbType.NVarChar, 25)
        comdepart.Parameters("@MenuId").Value = MenuId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function





   
End Class
