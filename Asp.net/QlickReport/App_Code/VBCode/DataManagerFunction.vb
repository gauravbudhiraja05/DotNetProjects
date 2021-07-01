Imports Microsoft.VisualBasic
Imports System.data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.IO.StreamReader
Imports System.Web.Configuration

Public Class DataManagerFunction
    Dim department As DropDownList
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim con As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim comdepart As New SqlCommand
    Dim comdepartgetvalue As New SqlCommand
    ''' <summary>
    ''' Function to show report
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <param name="columnvalue"></param>
    ''' <param name="wheredata"></param>
    ''' <param name="grpby"></param>
    ''' <param name="oderby"></param>
    ''' <param name="HavingCondition"></param>
    ''' <param name="txtformula"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function QueryRepShow(ByVal tablename As String, ByVal columnvalue As String, ByVal wheredata As String, ByVal grpby As String, ByVal oderby As String, ByVal HavingCondition As String, ByVal txtformula As String)
        ds1.Clear()
        Dim column1 As String = columnvalue.Replace("~", ",")
        Dim allcolumn As String = column1.Replace("$", ".")
        Dim colarr As Array = allcolumn.Split(",")
        Dim wherecond As String = ""
        Dim order As String = ""
        Dim group As String = ""
        Dim having As String = ""
        Dim strdiv As String = ""
        Dim querystring As String = ""
        Dim res As String = ""
        Dim wheretxt As String = ""

        Dim tabname = tablename.Replace("~", ",")
        If wheredata <> "" Then
            wherecond = " where " & wheredata
        Else
            wherecond = ""

        End If
        If grpby <> "" Then
            group = " group " & " by " & grpby
        Else
            group = ""

        End If
        If oderby <> "" Then
            oderby = " order " & " by " & oderby
        Else
            oderby = ""

        End If

        If HavingCondition <> "" Then
            having = " having " & HavingCondition
        Else
            having = ""
        End If
        If txtformula <> "" Then
            wheretxt = " where  " & txtformula
        Else
            wheretxt = ""
        End If
        Try

            Dim drgetvalue As SqlDataReader
            strdiv = strdiv & "<table width=90% border=1 bordercolor=darkgray cellspacing=0 cellpadding=0>"
            strdiv = strdiv & "<tr bgcolor=#66cccc>"
            Dim j As Integer = 0
            For j = 0 To colarr.Length - 1
                'For j = 0 To dr.FieldCount - 1
                'strdiv = strdiv & "<td width=10%>" & dr.GetName(j) & "</td>"
                'Format(System.DateTime.Now, "mm-dd-yyyy").Replace("/", "-")
                strdiv = strdiv & "<td width=10%>" & colarr(j) & "</td>"
            Next
            'dr.Close()
            'connection.Close()
            strdiv = strdiv & "</tr>"
           
            querystring = "select " & allcolumn & " from " & tabname & "  " & wherecond & " " & wheretxt & "" & group & " " & oderby & " " & having & ""

            comdepart = New SqlCommand(querystring, connection)
            connection.Open()
            drgetvalue = comdepart.ExecuteReader
            Dim i As Integer
            While drgetvalue.Read
                strdiv = strdiv & "<tr bgcolor=#f5f5f5>"
                For i = 0 To drgetvalue.FieldCount - 1
                    strdiv = strdiv & "<td> " & drgetvalue.GetValue(i) & "</td>"
                Next
                strdiv = strdiv & "</tr>"
            End While
            drgetvalue.Close()
            connection.Close()
            strdiv = strdiv & "<table>"
            res = strdiv & "*" & querystring

        Catch ex As Exception
            res = "2" & "*" & ex.Message
        End Try

        Return res

    End Function
    ''' <summary>
    ''' Function to save html report
    ''' </summary>
    ''' <param name="repname"></param>
    ''' <param name="dept"></param>
    ''' <param name="client"></param>
    ''' <param name="lob"></param>
    ''' <param name="queryname"></param>
    ''' <param name="path"></param>
    ''' <param name="savedby"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveHtmlRep(ByVal repname As String, ByVal dept As String, ByVal client As String, ByVal lob As String, ByVal queryname As String, ByVal path As String, ByVal savedby As String)

        comdepart = New SqlCommand("insert_Html", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        With comdepart.Parameters
            .AddWithValue("@Repname", repname)
            .AddWithValue("@Dept", dept)
            .AddWithValue("@Client", client)
            .AddWithValue("@Lob", lob)
            .AddWithValue("@Queryname", queryname)
            .AddWithValue("@Path", path)
            .AddWithValue("@Savedby", savedby)
            .AddWithValue("@Savedon", System.DateTime.Now)
        End With
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function
    ''' <summary>
    ''' Function to get condition whic applied on report
    ''' </summary>
    ''' <param name="recid"></param>
    ''' <param name="tabvalue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Tabname(ByVal recid As Integer, ByVal tabvalue As String)

        ds1.Clear()
        Dim tablename As String = ""
        Dim colname As String = ""
        Dim wheredata As String = ""
        Dim grouprby As String = ""
        Dim orderby As String = ""
        Dim havingcond As String = ""
        Dim txtFormula As String = ""


        comdepart = New SqlCommand("select_Tabname", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@recodid", SqlDbType.Int, 10)
        comdepart.Parameters("@recodid").Value = recid
        comdepart.Parameters.Add("@tabvalue", SqlDbType.NChar, 10)
        comdepart.Parameters("@tabvalue").Value = tabvalue
        connection.Open()
        da.SelectCommand = comdepart

        dr = comdepart.ExecuteReader
        If tabvalue = "rd" Then
            If dr.Read Then
                tablename = dr("TableName")
                colname = dr("ColName")
                wheredata = dr("WhereData")
                grouprby = dr("GroupBy")
                orderby = dr("OrderBy")
                havingcond = dr("HavingCondition")
                txtFormula = ""

            End If
        Else

            If dr.Read Then
                tablename = dr("TableName")
                colname = dr("ColName")
                grouprby = dr("GroupBy")
                orderby = dr("OrderBy")
                txtFormula = dr("txtFormula")
                havingcond = ""
                wheredata = ""
            End If
        End If
        dr.Close()
        connection.Close()
        Return tablename & "*" & colname & "*" & wheredata & "*" & grouprby & "*" & orderby & "*" & havingcond & "*" & txtFormula

    End Function
    ''' <summary>
    ''' Function to delete Data
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <param name="whereformula"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function rowdel(ByVal tablename As String, ByVal whereformula As String, ByRef strQuery As String)
        Dim recordsEffected As Integer
        Dim querystring As String = ""
        querystring = "delete  from " & tablename & " where  " & whereformula & " "
        strQuery = querystring
        comdepart = New SqlCommand(querystring, connection)
        connection.Open()
        recordsEffected = comdepart.ExecuteNonQuery()
        connection.Close()
        Return recordsEffected
    End Function
    Public Function rowdel(ByVal tablename As String, ByVal whereformula As String)
        Dim recordsEffected As Integer
        Dim querystring As String = ""
        querystring = "delete  from " & tablename & " where  " & whereformula & " "
        comdepart = New SqlCommand(querystring, connection)
        connection.Open()
        recordsEffected = comdepart.ExecuteNonQuery()
        connection.Close()
        Return recordsEffected
    End Function
    ''' <summary>
    ''' Function to save html report
    ''' </summary>
    ''' <param name="repName"></param>
    ''' <param name="Path"></param>
    ''' <param name="DepartmentID"></param>
    ''' <param name="ClientID"></param>
    ''' <param name="UnderLOB"></param>
    ''' <param name="SavedBy"></param>
    ''' <param name="SavedOn"></param>
    ''' <param name="Type"></param>
    ''' <param name="queryname"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insertHTMLReport(ByVal repName As String, ByVal Path As String, ByVal DepartmentID As Integer, ByVal ClientID As Integer, ByVal UnderLOB As Integer, ByVal SavedBy As String, ByVal SavedOn As String, ByVal Type As String, ByVal queryname As String) As String
        Dim res As String
        res = ""
        Try
            comdepart = New SqlCommand("sp_SaveHTMLReport", con)
            comdepart.CommandType = CommandType.StoredProcedure
            comdepart.Parameters.Add("@SavedFilename", SqlDbType.NVarChar, 200)
            comdepart.Parameters("@SavedFilename").Value = repName
            comdepart.Parameters.Add("@Path", SqlDbType.NVarChar, 1000)
            comdepart.Parameters("@Path").Value = Path
            comdepart.Parameters.Add("@DepartmentId", SqlDbType.Int, 9)
            comdepart.Parameters("@DepartmentId").Value = DepartmentID
            comdepart.Parameters.Add("@ClientId", SqlDbType.Int, 9)
            comdepart.Parameters("@ClientId").Value = ClientID
            comdepart.Parameters.Add("@LOBId", SqlDbType.Int, 9)
            comdepart.Parameters("@LOBId").Value = UnderLOB
            comdepart.Parameters.Add("@SavedBy", SqlDbType.NVarChar, 100)
            comdepart.Parameters("@SavedBy").Value = SavedBy
            comdepart.Parameters.Add("@SavedOn", SqlDbType.DateTime)
            comdepart.Parameters("@SavedOn").Value = SavedOn
            comdepart.Parameters.Add("@Type", SqlDbType.NVarChar, 20)
            comdepart.Parameters("@Type").Value = Type
            comdepart.Parameters.Add("@queryname", SqlDbType.NVarChar, 200)
            comdepart.Parameters("@queryname").Value = queryname
            con.Open()
            comdepart.ExecuteNonQuery()
            con.Close()
            res = "1"
        Catch ex As Exception
            res = ex.Message
        End Try
        Return res
    End Function
    ''' <summary>
    ''' Function to delete report 
    ''' </summary>
    ''' <param name="idvalue"></param>
    ''' <param name="tableid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Delrep(ByVal idvalue As String, ByVal tableid As Integer)
        
        Dim res As String = ""
        Try
            comdepart = New SqlCommand("sp_Delrep", con)
            comdepart.CommandType = CommandType.StoredProcedure
            comdepart.Parameters.Add("@tableid", SqlDbType.Int, 10)
            comdepart.Parameters("@tableid").Value = tableid
            comdepart.Parameters.Add("@idvalue", SqlDbType.NChar, 10)
            comdepart.Parameters("@idvalue").Value = idvalue
            con.Open()
            comdepart.ExecuteNonQuery()
            con.Close()
            res = "1"
            Return res
        Catch ex As Exception
            res = ex.Message
        End Try
        Return res
    End Function
    ''' <summary>
    ''' Function to uvarchive the report
    ''' </summary>
    ''' <param name="idvalue"></param>
    ''' <param name="tableid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Arvrep(ByVal idvalue As String, ByVal tableid As Integer)
        
        Dim res As String = ""
        Try
            comdepart = New SqlCommand("sp_Arcrep", con)
            comdepart.CommandType = CommandType.StoredProcedure
            comdepart.Parameters.Add("@tableid", SqlDbType.Int, 10)
            comdepart.Parameters("@tableid").Value = tableid
            comdepart.Parameters.Add("@idvalue", SqlDbType.NChar, 10)
            comdepart.Parameters("@idvalue").Value = idvalue
            con.Open()
            comdepart.ExecuteNonQuery()
            con.Close()
            res = "1"
            Return res
        Catch ex As Exception
            res = ex.Message
        End Try
        Return res
    End Function
    ''' <summary>
    ''' Function to show archive report
    ''' </summary>
    ''' <param name="Deptid"></param>
    ''' <param name="Clientid"></param>
    ''' <param name="Lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function showArcrepep(ByVal Deptid As Integer, ByVal Clientid As Integer, ByVal Lobid As Integer)

        Dim querystring As String = ""
        Dim ds As New DataSet


        comdepart = New SqlCommand("select_arcrep", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@Deptid", SqlDbType.Int, 10)
        comdepart.Parameters("@Deptid").Value = Deptid
        comdepart.Parameters.Add("@Clientid", SqlDbType.Int, 10)
        comdepart.Parameters("@Clientid").Value = Clientid
        comdepart.Parameters.Add("@Lobid", SqlDbType.Int, 10)
        comdepart.Parameters("@Lobid").Value = Lobid


        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()

        Return ds
    End Function
    ''' <summary>
    ''' Functuin to show report for local user
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function reportForlocal(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_arcreplocal", con)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@userid").Value = user
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        con.Close()
        Return ds
    End Function
    ''' <summary>
    ''' Functuin to show report for Nonlocal user
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function reportFornonlocal(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_arcrepNonlocal", con)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@userid").Value = user
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        con.Close()
        Return ds
    End Function

    Function chkuserrights(ByVal userid As String)

        comdepart = New SqlCommand("sp_chkuserrights", con)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@userid").Value = userid
        con.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        con.Close()
        Return ds
    End Function
    ''' <summary>
    ''' Function to check Html Report is already Exists.
    ''' </summary>
    ''' <param name="repname"></param>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckExistingHTMLReport(ByVal repname As String, ByVal deptid As Integer, ByVal clientid As Integer, ByVal lobid As Integer) As Boolean
        Dim stat As Boolean
        stat = False
        Dim rdr As SqlDataReader
        comdepart = New SqlCommand("sp_CheckHTMLReport", con)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@SavedFilename", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@SavedFilename").Value = repname
        comdepart.Parameters.Add("@DepartmentId", SqlDbType.Int, 9)
        comdepart.Parameters("@DepartmentId").Value = deptid
        comdepart.Parameters.Add("@ClientId", SqlDbType.Int, 9)
        comdepart.Parameters("@ClientId").Value = clientid
        comdepart.Parameters.Add("@LOBId", SqlDbType.Int, 9)
        comdepart.Parameters("@LOBId").Value = lobid
        con.Open()
        rdr = comdepart.ExecuteReader()
        Dim st As String = ""
        stat = rdr.HasRows
        con.Close()
        Return stat
    End Function

    Public Function trackDmMaster(ByVal actionBy As String, ByVal action As String, ByVal entity As String, ByVal entityName As String, ByVal deptId As String, ByVal clientId As String, ByVal lobId As String)

        Dim cmd As SqlCommand
        Dim autoId As Integer
        cmd = New SqlCommand("Sp_LogDataManagerWithAutoId", connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@AutoId", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 100)
        cmd.Parameters("@ActionBy").Value = actionBy
        cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
        cmd.Parameters("@Action").Value = action
        cmd.Parameters.Add("@Date", SqlDbType.DateTime, 8)
        cmd.Parameters("@Date").Value = System.DateTime.Now
        cmd.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
        cmd.Parameters("@Entity").Value = entity
        cmd.Parameters.Add("@EntityName", SqlDbType.NVarChar, 100)
        cmd.Parameters("@EntityName").Value = entityName
        cmd.Parameters.Add("@DeptId", SqlDbType.Int)
        cmd.Parameters("@DeptId").Value = deptId
        cmd.Parameters.Add("@ClientId", SqlDbType.Int)
        cmd.Parameters("@ClientId").Value = clientId
        cmd.Parameters.Add("@LobId", SqlDbType.Int)
        cmd.Parameters("@LobId").Value = lobId
        connection.Open()
        cmd.ExecuteNonQuery()
        autoId = cmd.Parameters(0).Value
        connection.Close()
        cmd.Dispose()
        Return autoId
    End Function
    Public Sub trackDmSlave(ByVal refId As Integer, ByVal attribute As String, ByVal value As String)
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim cmd As SqlCommand
        cmd = New SqlCommand("Sp_LogDataManagerSlave", connection)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@RefId", SqlDbType.Int).Value = refId
        cmd.Parameters.Add("@Attribute", SqlDbType.NVarChar, 100).Value = attribute
        cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 50).Value = value
        connection.Open()
        cmd.ExecuteNonQuery()
        connection.Close()
        cmd.Dispose()
    End Sub
End Class
