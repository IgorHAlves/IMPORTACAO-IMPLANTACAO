Imports ClosedXML.Excel
Imports System.Data

Public Class Form1

    Private Sub btnBuscarArquivo_Click(sender As Object, e As EventArgs) Handles btnBuscarArquivo.Click

        OpenFileDialog1.Title = "Selecione um arquivo Excel"
        OpenFileDialog1.Filter = "Arquivos Excel (*.xlsx)|*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\DESENVOLVIMENTO"

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            lblCaminho.Text = OpenFileDialog1.FileName

            CarregarExcelNaGrid(OpenFileDialog1.FileName)
        End If

    End Sub


    Private Sub CarregarExcelNaGrid(caminho As String)

        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()

            Using workbook As New XLWorkbook(caminho)

                Dim planilha = workbook.Worksheet(1)
                Dim tabela As New DataTable

                Dim primeiraLinha = planilha.FirstRowUsed
                If primeiraLinha Is Nothing Then
                    MessageBox.Show("O Excel está vazio.")
                    Exit Sub
                End If

                Dim ultimaLinha = planilha.LastRowUsed
                Dim ultimaColuna = planilha.LastColumnUsed

                ' --- LER CABEÇALHO DO EXCEL ---
                Dim colunasExcel As New List(Of String)
                For coluna = 1 To ultimaColuna.ColumnNumber
                    colunasExcel.Add(planilha.Cell(primeiraLinha.RowNumber, coluna).Value.ToString.Trim())
                Next

                ' --- LAYOUT DO BANCO ---
                Dim colunasBanco As String() = DBUtils.BuscarLayout().Split(";"c).Select(Function(s) s.Trim()).ToArray()

                ' --- COMPARAR COLUNAS ---
                Dim colunasFaltando As New List(Of String)
                Dim colunasExtras As New List(Of String)

                ' Colunas que estão no banco mas não no Excel
                For Each c In colunasBanco
                    If Not colunasExcel.Contains(c) Then
                        colunasFaltando.Add(c)
                    End If
                Next

                ' Colunas que estão no Excel mas não no banco
                For Each c In colunasExcel
                    If Not colunasBanco.Contains(c) Then
                        colunasExtras.Add(c)
                    End If
                Next

                If colunasFaltando.Any() OrElse colunasExtras.Any() Then
                    Dim msg As String = "O Excel não corresponde ao layout esperado." & vbCrLf
                    If colunasFaltando.Any() Then
                        msg &= "Faltando no Excel: " & String.Join(", ", colunasFaltando) & vbCrLf
                    End If
                    If colunasExtras.Any() Then
                        msg &= "Extras no Excel: " & String.Join(", ", colunasExtras)
                    End If
                    MessageBox.Show(msg)
                    Exit Sub
                End If

                ' Colunas
                For Each celula In primeiraLinha.Cells(1, ultimaColuna.ColumnNumber)
                    tabela.Columns.Add(celula.Value.ToString)
                Next

                ' Linhas
                For linha = primeiraLinha.RowNumber + 1 To ultimaLinha.RowNumber
                    Dim novaLinha = tabela.NewRow

                    For coluna = 1 To ultimaColuna.ColumnNumber
                        novaLinha(coluna - 1) =
                            planilha.Cell(linha, coluna).Value.ToString
                    Next

                    tabela.Rows.Add(novaLinha)
                Next

                DataGridView1.DataSource = tabela

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao importar Excel: " & ex.Message)
        End Try

    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        Dim layout As String = DBUtils.BuscarLayout()
        MessageBox.Show(layout)
    End Sub
End Class
