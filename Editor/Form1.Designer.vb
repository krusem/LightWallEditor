<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Editor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.PickColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.NewFrameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NextFrameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrevFrameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem, Me.ToolStripMenuItem3, Me.ToolStripMenuItem1, Me.PickColorToolStripMenuItem, Me.ToolStripMenuItem2, Me.NewFrameToolStripMenuItem, Me.NextFrameToolStripMenuItem, Me.PrevFrameToolStripMenuItem, Me.ToolStripMenuItem4})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(135, 176)
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(134, 22)
        Me.ToolStripMenuItem3.Text = "Clear"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(131, 6)
        '
        'PickColorToolStripMenuItem
        '
        Me.PickColorToolStripMenuItem.Name = "PickColorToolStripMenuItem"
        Me.PickColorToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.PickColorToolStripMenuItem.Text = "Pick Color"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(131, 6)
        '
        'NewFrameToolStripMenuItem
        '
        Me.NewFrameToolStripMenuItem.Name = "NewFrameToolStripMenuItem"
        Me.NewFrameToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.NewFrameToolStripMenuItem.Text = "New Frame"
        '
        'NextFrameToolStripMenuItem
        '
        Me.NextFrameToolStripMenuItem.Name = "NextFrameToolStripMenuItem"
        Me.NextFrameToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.NextFrameToolStripMenuItem.Text = "Next Frame"
        '
        'PrevFrameToolStripMenuItem
        '
        Me.PrevFrameToolStripMenuItem.Name = "PrevFrameToolStripMenuItem"
        Me.PrevFrameToolStripMenuItem.Size = New System.Drawing.Size(134, 22)
        Me.PrevFrameToolStripMenuItem.Text = "Prev Frame"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(131, 6)
        '
        'Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.Name = "Editor"
        Me.Text = "Light Display Editor"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PickColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NewFrameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NextFrameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrevFrameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator

End Class
