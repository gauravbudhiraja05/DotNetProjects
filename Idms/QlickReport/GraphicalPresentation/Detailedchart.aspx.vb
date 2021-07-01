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
Partial Class GraphicalPresentation_Detailedchart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim region As String = ""
        If Me.Page.Request("region") IsNot Nothing Then
            region = DirectCast(Me.Page.Request("region"), String)
            Chart1.Titles(0).Text = region + " - chart"
        End If
        Dim xval As String = Session("xvalue")
        Dim yval As String = Session("yvalue")
        Dim xcol
        Dim ycol
        xcol = xval.Split(",")
        ycol = yval.Split(",")
        Dim myConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
        ' Define the database query	
        Dim mySelectQuery As String
        mySelectQuery = Session("drillquery")
        ' mySelectQuery += "FROM tabdaksh_test "
        mySelectQuery += "WHERE " + ycol(0) + "." + xcol(0) + "=" + "'" + region + "'"



        ' Create a database connection object using the connection string	
        Dim myConnection As New SqlConnection(myConnectionString)
        ' Create a database command on the connection using query	
        Dim myCommand As New SqlCommand(mySelectQuery, myConnection)
        ' Open the connection	
        myCommand.Connection.Open()
        ' Initializes a new instance of the OleDbDataAdapter class
        Dim reader As SqlDataReader
        reader = myCommand.ExecuteReader
        Chart1.Series("Sales").Points.DataBindXY(reader, xcol(0), reader, xcol(1))
        reader.Close()
        myCommand.Connection.Close()
    End Sub
End Class
