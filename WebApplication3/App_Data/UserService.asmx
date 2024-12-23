Imports System.Data.SqlClient

Public Class UserService
    Inherits System.Web.Services.WebService

    Private connectionString As String = "Data Source=SERVER_NAME;Initial Catalog=DB_Aplikasi;Integrated Security=True"

    <WebMethod()>
    Public Function GetAllUsers() As DataTable
        Dim dt As New DataTable()
        Using con As New SqlConnection(connectionString)
            Using cmd As New SqlCommand("GetAllUsers", con)
                cmd.CommandType = CommandType.StoredProcedure
                con.Open()
                dt.Load(cmd.ExecuteReader())
            End Using
        End Using
        Return dt
    End Function
End Class
