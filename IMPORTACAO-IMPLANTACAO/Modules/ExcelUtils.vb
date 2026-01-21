Imports ClosedXML.Excel
Imports System.Data

Module ExcelUtils

    ''' <summary>
    ''' Carrega um arquivo Excel em um DataGridView e valida o layout.
    ''' </summary>
    ''' <param name="dgv">DataGridView onde os dados serão exibidos</param>
    ''' <param name="caminho">Caminho do arquivo Excel</param>
    Public Sub CarregarExcelNaGrid(dgv As DataGridView, caminho As String)

        Try
            dgv.DataSource = Nothing
            dgv.Rows.Clear()
            dgv.Columns.Clear()

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

                ' --- Cabeçalho do Excel ---
                Dim colunasExcel As New List(Of String)
                For coluna = 1 To ultimaColuna.ColumnNumber
                    colunasExcel.Add(planilha.Cell(primeiraLinha.RowNumber, coluna).Value.ToString.Trim())
                Next

                ' --- Layout esperado do banco ---
                Dim colunasBanco As String() = DBUtils.BuscarLayout().Split(";"c).Select(Function(s) s.Trim()).ToArray()

                ' --- Verifica colunas faltando/extras ---
                Dim colunasFaltando As New List(Of String)
                Dim colunasExtras As New List(Of String)

                For Each c In colunasBanco
                    If Not colunasExcel.Contains(c) Then colunasFaltando.Add(c)
                Next
                For Each c In colunasExcel
                    If Not colunasBanco.Contains(c) Then colunasExtras.Add(c)
                Next

                If colunasFaltando.Any() OrElse colunasExtras.Any() Then
                    Dim msg As String = "O Excel não corresponde ao layout esperado." & vbCrLf
                    If colunasFaltando.Any() Then msg &= "Faltando no Excel: " & String.Join(", ", colunasFaltando) & vbCrLf
                    If colunasExtras.Any() Then msg &= "Extras no Excel: " & String.Join(", ", colunasExtras)
                    MessageBox.Show(msg)
                    Exit Sub
                End If

                ' --- Cria colunas na DataTable ---
                For Each celula In primeiraLinha.Cells(1, ultimaColuna.ColumnNumber)
                    tabela.Columns.Add(celula.Value.ToString)
                Next

                ' --- Preenche linhas na DataTable ---
                For linha = primeiraLinha.RowNumber + 1 To ultimaLinha.RowNumber
                    Dim novaLinha = tabela.NewRow

                    For coluna = 1 To ultimaColuna.ColumnNumber
                        novaLinha(coluna - 1) = planilha.Cell(linha, coluna).Value.ToString
                    Next

                    tabela.Rows.Add(novaLinha)
                Next

                ' --- Atribui à grid ---
                dgv.DataSource = tabela

            End Using

        Catch ex As Exception
            MessageBox.Show("Erro ao importar Excel: " & ex.Message)
        End Try

    End Sub

End Module
