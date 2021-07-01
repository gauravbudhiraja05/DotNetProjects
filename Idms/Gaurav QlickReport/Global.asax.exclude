<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        Dim appName As String
        Dim DateTime As String = Session.SessionID
        appName = System.Configuration.ConfigurationSettings.AppSettings("ConnectionString")

        Dim booaa As Boolean
        Dim cmd As System.Data.SqlClient.SqlCommand
      
        Dim readquery As System.Data.SqlClient.SqlDataReader
        Dim con As New System.Data.SqlClient.SqlConnection(appName)
        cmd = New System.Data.SqlClient.SqlCommand("select name from sysobjects where xtype='u'", con)
        con.Open()
        readquery = cmd.ExecuteReader
        While readquery.Read()
            Dim pretabvle As String = readquery("name")
            If pretabvle.Contains("tabddlReport" & DateTime) Then
                booaa = False
                Exit While

            Else
                booaa = True
            End If
        End While
        readquery.Close()
        con.Close()

        If booaa = False Then

            cmd = New System.Data.SqlClient.SqlCommand("drop table tabddlReport" + DateTime + "", con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
          
            Exit Sub


        Else

        End If
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>