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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.lblCaminho = New System.Windows.Forms.Label()
        Me.btnBuscarArquivo = New System.Windows.Forms.Button()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.btnImportar = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(22, 68)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(739, 307)
        Me.DataGridView1.TabIndex = 1
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'lblCaminho
        '
        Me.lblCaminho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCaminho.Location = New System.Drawing.Point(22, 385)
        Me.lblCaminho.Name = "lblCaminho"
        Me.lblCaminho.Size = New System.Drawing.Size(700, 23)
        Me.lblCaminho.TabIndex = 2
        Me.lblCaminho.Text = "(Nenhum arquivo selecionado)"
        '
        'btnBuscarArquivo
        '
        Me.btnBuscarArquivo.Location = New System.Drawing.Point(728, 385)
        Me.btnBuscarArquivo.Name = "btnBuscarArquivo"
        Me.btnBuscarArquivo.Size = New System.Drawing.Size(33, 23)
        Me.btnBuscarArquivo.TabIndex = 3
        Me.btnBuscarArquivo.Text = "..."
        Me.btnBuscarArquivo.UseVisualStyleBackColor = True
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.lblTitulo.Location = New System.Drawing.Point(263, 31)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(269, 21)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "Importação de Remessa Carteira X"
        '
        'btnImportar
        '
        Me.btnImportar.Location = New System.Drawing.Point(310, 415)
        Me.btnImportar.Name = "btnImportar"
        Me.btnImportar.Size = New System.Drawing.Size(147, 27)
        Me.btnImportar.TabIndex = 4
        Me.btnImportar.Text = "Importar"
        Me.btnImportar.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnImportar)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.btnBuscarArquivo)
        Me.Controls.Add(Me.lblCaminho)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Form1"
        Me.Text = "Importacao de remessa"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents lblCaminho As Label
    Friend WithEvents btnBuscarArquivo As Button
    Friend WithEvents lblTitulo As Label
    Friend WithEvents btnImportar As Button

End Class
