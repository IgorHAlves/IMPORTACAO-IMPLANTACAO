Imports System.Data
Imports ClosedXML.Excel
Imports Npgsql

Public Class Form1
    Public Sub New()

        ' Esta chamada é requerida pelo designer.
        InitializeComponent()

        ' Adicione qualquer inicialização após a chamada InitializeComponent().

    End Sub

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
            pnlLoading.Visible = True
            pbImportacao.Minimum = 0
            pbImportacao.Maximum = 8
            pbImportacao.Value = 0
            lblStatus.Text = "Iniciando importação..."

            Await ImportacaoService.ImportarAsync(
            lblCaminho.Text,
            Sub(processo, texto)

                Me.Invoke(Sub()
                              pbImportacao.Value = processo + 1
                              lblStatus.Text = texto
                              lblStatus.Invalidate()
                              lblStatus.Update()
                          End Sub)

            End Sub
            )

            MessageBox.Show("Importação realizada com sucesso!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            btnImportar.Enabled = True
            pnlLoading.Visible = False
            pbImportacao.Value = 0
            lblStatus.Text = ""
        End Try
    End Sub

    Private Sub lblStatus_Click(sender As Object, e As EventArgs) Handles lblStatus.Click

    End Sub

    Private Sub pnlLoading_Paint(sender As Object, e As PaintEventArgs) Handles pnlLoading.Paint

    End Sub
End Class
