Imports System.Data
Imports ClosedXML.Excel
Imports Npgsql

Public Class Form1

    Private Sub btnBuscarArquivo_Click(sender As Object, e As EventArgs) Handles btnBuscarArquivo.Click
        OpenFileDialog1.Title = "Selecione um arquivo Excel"
        OpenFileDialog1.Filter = "Arquivos Excel (*.xlsx)|*.xlsx"
        OpenFileDialog1.InitialDirectory = "C:\DESENVOLVIMENTO"

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            lblCaminho.Text = OpenFileDialog1.FileName
            ExcelUtils.CarregarExcelNaGrid(DataGridView1, lblCaminho.Text)
        End If
    End Sub

    Private Async Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        If String.IsNullOrWhiteSpace(lblCaminho.Text) Then
            MessageBox.Show("Selecione um arquivo primeiro!")
            Exit Sub
        End If

        btnImportar.Enabled = False

        Try
            Await ImportacaoService.ImportarAsync(lblCaminho.Text)
            MessageBox.Show("Importação realizada com sucesso!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            btnImportar.Enabled = True
        End Try
    End Sub


End Class
