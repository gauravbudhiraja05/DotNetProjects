Imports System
Imports System.Data
Imports System.Drawing
Imports Dundas.Charting.WebControl
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Imports System.Web.SessionState
Imports System.Collections
Imports Dundas.Charting.Utilities
Imports DundasUtilities.Charting.SixSigma
Imports System.ComponentModel
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Web.UI.WebControls.WebParts
Partial Class GraphicalPresentation_Regiongraph
    Inherits System.Web.UI.Page
    Dim myConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    ' Define the database query	
    Dim mySelectQuery As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim region As String = ""
        If Me.Page.Request("region") IsNot Nothing Then
            region = DirectCast(Me.Page.Request("region"), String)
            Chart1.Titles(0).Text = region + " - chart"
        End If
        mySelectQuery = "SELECT DM "
        mySelectQuery += "FROM tabdaksh_test "
        mySelectQuery += "WHERE (((tabdaksh_test.DM)='" + region + "'));"
        ' mySelectQuery = "select Lob from idmsgraphmaster where" '+ x + " from " + n + " order by " & sp(0) + ""
        ' Create a database connection object using the connection string	
        Dim myConnection As New SqlConnection(myConnectionString)
        ' Create a database command on the connection using query	
        Dim myCommand As New SqlCommand(mySelectQuery, myConnection)
        ' Open the connection	
        myCommand.Connection.Open()
        ' Initializes a new instance of the OleDbDataAdapter class
        Dim myDataAdapter As New SqlDataAdapter
        myDataAdapter.SelectCommand = myCommand
        ' Initializes a new instance of the DataSet class
        Dim myDataSet As New DataSet()
        ' Adds rows in the DataSet
        myDataAdapter.Fill(myDataSet, "Query")
        myCommand.Connection.Close()
    End Sub
End Class
