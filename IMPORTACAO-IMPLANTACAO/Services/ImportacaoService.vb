Imports System.Globalization
Imports ClosedXML.Excel
Imports Npgsql
Imports SixLabors.Fonts.Tables

Public Class ImportacaoService

    Public Shared Async Function ImportarAsync(caminhoArquivo As String) As Task

        Dim connString As String = "Host=localhost;Username=root;Password=1234;Database=implantacao"

        Using conn As New NpgsqlConnection(connString)
            Await conn.OpenAsync()

            Using trans As NpgsqlTransaction = conn.BeginTransaction()
                Try
                    Using workbook As New XLWorkbook(caminhoArquivo)
                        Dim planilha = workbook.Worksheet(1)
                        Dim primeiraLinha = planilha.FirstRowUsed
                        Dim ultimaLinha = planilha.LastRowUsed
                        Dim ultimaColuna = planilha.LastColumnUsed
                        Dim colunas As New List(Of String)

                        For c = 1 To ultimaColuna.ColumnNumber
                            colunas.Add(planilha.Cell(primeiraLinha.RowNumber, c).GetString().Trim())
                        Next

                        Dim sqlCreate =
                            "DROP TABLE IF EXISTS importacao_implantacao_tmp;" & vbCrLf &
                            "CREATE TABLE importacao_implantacao_tmp (" &
                            String.Join(", ", colunas.Select(Function(c) c & " TEXT")) & ");"

                        Using cmdCreate As New NpgsqlCommand(sqlCreate, conn, trans)
                            cmdCreate.ExecuteNonQuery()
                        End Using

                        Dim idxCPF = colunas.IndexOf("CPF") + 1
                        Dim idxTelefone = colunas.IndexOf("TELEFONE") + 1
                        Dim idxCelular = colunas.IndexOf("CELULAR") + 1
                        Dim idxEmail = colunas.IndexOf("EMAIL") + 1
                        Dim idxUF = colunas.IndexOf("UF") + 1
                        Dim idxValor = colunas.IndexOf("VALOR") + 1
                        Dim idxVencimento = colunas.IndexOf("VENCIMENTO") + 1
                        Dim idxParcela = colunas.IndexOf("PARCELA") + 1

                        For linha = primeiraLinha.RowNumber + 1 To ultimaLinha.RowNumber

                            Dim cpf = planilha.Cell(linha, idxCPF).GetString().Trim()
                            Dim telefone = planilha.Cell(linha, idxTelefone).GetString().Trim()
                            Dim celular = planilha.Cell(linha, idxCelular).GetString().Trim()
                            Dim email = planilha.Cell(linha, idxEmail).GetString().Trim()
                            Dim uf = planilha.Cell(linha, idxUF).GetString().Trim()
                            Dim valorStr = planilha.Cell(linha, idxValor).GetString().Trim().Replace(",", ".")
                            Dim vencimentoStr = planilha.Cell(linha, idxVencimento).GetString().Trim()
                            Dim parcela = planilha.Cell(linha, idxParcela).GetString().Trim()


                            If Not ValidationUtils.ValidarCPF(cpf) Then
                                If Not PerguntarIgnorar(linha, "CPF vazio ou não informado") Then
                                    Throw New Exception("Importação cancelada.")
                                End If
                                Continue For
                            End If

                            If Not String.IsNullOrEmpty(email) AndAlso Not ValidationUtils.ValidarEmail(email) Then
                                If Not PerguntarIgnorar(linha, $"E-mail inválido ({email})") Then Throw New Exception("Importação cancelada.")
                                Continue For
                            End If

                            If Not String.IsNullOrEmpty(telefone) AndAlso Not ValidationUtils.ValidarTelefone(telefone) Then
                                If Not PerguntarIgnorar(linha, $"Telefone inválido ({telefone})") Then Throw New Exception("Importação cancelada.")
                                Continue For
                            End If

                            If Not String.IsNullOrEmpty(celular) AndAlso Not ValidationUtils.ValidarCelular(celular) Then
                                If Not PerguntarIgnorar(linha, $"Celular inválido ({celular})") Then Throw New Exception("Importação cancelada.")
                                Continue For
                            End If

                            If Not ValidationUtils.ValidarValor(valorStr) Then
                                If Not PerguntarIgnorar(linha, $"Valor inválido ({valorStr})") Then Throw New Exception("Importação cancelada.")
                                Continue For
                            End If

                            If Not ValidationUtils.ValidarData(vencimentoStr) Then
                                If Not PerguntarIgnorar(linha, $"Vencimento inválido ({vencimentoStr})") Then Throw New Exception("Importação cancelada.")
                                Continue For
                            End If

                            Dim enderecoRandom As (logradouro As String, bairro As String, cidade As String, uf As String) = Nothing

                            If parcela = "1" Then

                                enderecoRandom = Await EnderecoUtils.BuscarEnderecoRandomAsync()

                                If String.IsNullOrWhiteSpace(uf) Then
                                    uf = enderecoRandom.uf
                                End If

                                uf = ValidationUtils.ObterUF(uf)

                                If String.IsNullOrWhiteSpace(planilha.Cell(linha, colunas.IndexOf("LOGRADOURO") + 1).GetString()) Then
                                    planilha.Cell(linha, colunas.IndexOf("LOGRADOURO") + 1).Value = enderecoRandom.logradouro
                                End If

                                If String.IsNullOrWhiteSpace(planilha.Cell(linha, colunas.IndexOf("BAIRRO") + 1).GetString()) Then
                                    planilha.Cell(linha, colunas.IndexOf("BAIRRO") + 1).Value = enderecoRandom.bairro
                                End If

                                If String.IsNullOrWhiteSpace(planilha.Cell(linha, colunas.IndexOf("CIDADE") + 1).GetString()) Then
                                    planilha.Cell(linha, colunas.IndexOf("CIDADE") + 1).Value = enderecoRandom.cidade
                                End If

                                If String.IsNullOrWhiteSpace(planilha.Cell(linha, colunas.IndexOf("UF") + 1).GetString()) Then
                                    planilha.Cell(linha, colunas.IndexOf("UF") + 1).Value = uf
                                End If

                            End If

                            Dim valores As New List(Of String)

                            For c = 1 To ultimaColuna.ColumnNumber
                                Dim v = planilha.Cell(linha, c).GetString().Trim().Replace("'", "''")
                                valores.Add($"'{v}'")
                            Next

                            Dim sqlInsert =
                                "INSERT INTO importacao_implantacao_tmp (" &
                                String.Join(", ", colunas) & ") VALUES (" &
                                String.Join(", ", valores) & ");"

                            Using cmdInsert As New NpgsqlCommand(sqlInsert, conn, trans)
                                cmdInsert.ExecuteNonQuery()
                            End Using

                        Next
                    End Using

                    For processo As Integer = 0 To 7
                        Using cmd As New NpgsqlCommand(
                            "SELECT f_importa_remessa_implantacao(@p, @t);",
                            conn,
                            trans
                        )
                            cmd.Parameters.AddWithValue("@p", processo)
                            cmd.Parameters.AddWithValue("@t", "importacao_implantacao")
                            cmd.ExecuteNonQuery()
                        End Using
                    Next

                    trans.Commit()

                Catch
                    trans.Rollback()
                    Throw
                End Try
            End Using
        End Using
    End Function

    Private Shared Function PerguntarIgnorar(linha As Integer, mensagem As String) As Boolean
        Dim r = MessageBox.Show(
            $"Linha {linha}: {mensagem}" & vbCrLf &
            "Deseja ignorar esta linha e continuar?",
            "Erro de validação",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        )

        Return r = DialogResult.Yes
    End Function

End Class
