<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer.
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        DataGridView1 = New DataGridView()
        OpenFileDialog1 = New OpenFileDialog()
        lblCaminho = New Label()
        btnBuscarArquivo = New Button()
        lblTitulo = New Label()
        btnImportar = New Button()
        pnlLoading = New Panel()
        lblStatus = New Label()
        pbImportacao = New ProgressBar()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        pnlLoading.SuspendLayout()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(22, 68)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.Size = New Size(739, 278)
        DataGridView1.TabIndex = 1
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' lblCaminho
        ' 
        lblCaminho.BorderStyle = BorderStyle.FixedSingle
        lblCaminho.Location = New Point(22, 385)
        lblCaminho.Name = "lblCaminho"
        lblCaminho.Size = New Size(700, 23)
        lblCaminho.TabIndex = 2
        lblCaminho.Text = "(Nenhum arquivo selecionado)"
        ' 
        ' btnBuscarArquivo
        ' 
        btnBuscarArquivo.Location = New Point(728, 385)
        btnBuscarArquivo.Name = "btnBuscarArquivo"
        btnBuscarArquivo.Size = New Size(33, 23)
        btnBuscarArquivo.TabIndex = 3
        btnBuscarArquivo.Text = "..."
        btnBuscarArquivo.UseVisualStyleBackColor = True
        ' 
        ' lblTitulo
        ' 
        lblTitulo.AutoSize = True
        lblTitulo.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        lblTitulo.Location = New Point(263, 31)
        lblTitulo.Name = "lblTitulo"
        lblTitulo.Size = New Size(269, 21)
        lblTitulo.TabIndex = 0
        lblTitulo.Text = "Importação de Remessa Carteira X"
        ' 
        ' btnImportar
        ' 
        btnImportar.Location = New Point(310, 415)
        btnImportar.Name = "btnImportar"
        btnImportar.Size = New Size(147, 27)
        btnImportar.TabIndex = 4
        btnImportar.Text = "Importar"
        btnImportar.UseVisualStyleBackColor = True
        ' 
        ' pnlLoading
        ' 
        pnlLoading.Controls.Add(lblStatus)
        pnlLoading.Controls.Add(pbImportacao)
        pnlLoading.Location = New Point(22, 352)
        pnlLoading.Name = "pnlLoading"
        pnlLoading.Size = New Size(739, 27)
        pnlLoading.TabIndex = 5
        pnlLoading.Visible = False
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.Location = New Point(313, 7)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(41, 15)
        lblStatus.TabIndex = 2
        lblStatus.Text = "Label1"
        ' 
        ' pbImportacao
        ' 
        pbImportacao.Location = New Point(37, 5)
        pbImportacao.Maximum = 8
        pbImportacao.Name = "pbImportacao"
        pbImportacao.Size = New Size(699, 19)
        pbImportacao.TabIndex = 1
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(pnlLoading)
        Controls.Add(btnImportar)
        Controls.Add(lblTitulo)
        Controls.Add(btnBuscarArquivo)
        Controls.Add(lblCaminho)
        Controls.Add(DataGridView1)
        Name = "Form1"
        Text = "Importacao de remessa"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        pnlLoading.ResumeLayout(False)
        pnlLoading.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents lblCaminho As Label
    Friend WithEvents btnBuscarArquivo As Button
    Friend WithEvents lblTitulo As Label
    Friend WithEvents btnImportar As Button
    Friend WithEvents pnlLoading As Panel
    Friend WithEvents lblStatus As Label
    Friend WithEvents pbImportacao As ProgressBar

End Class
