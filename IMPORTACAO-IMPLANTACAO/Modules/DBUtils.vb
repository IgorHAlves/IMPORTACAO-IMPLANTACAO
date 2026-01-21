Imports Npgsql

Module DBUtils

    Public Function BuscarLayout() As String
        Dim layoutValor As String = ""
        Dim connString As String = "Host=localhost;Username=root;Password=1234;Database=implantacao"

        Using conn As New NpgsqlConnection(connString)
            Try
                conn.Open()
                Dim sql As String = "SELECT layout_str FROM cblayout WHERE id_cblayout_int = 1"
                Using cmd As New NpgsqlCommand(sql, conn)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        layoutValor = result.ToString()
                    End If
                End Using
            Catch ex As Exception
                MsgBox("Erro ao buscar layout: " & ex.Message)
            End Try
        End Using

        Return layoutValor
    End Function

End Module
