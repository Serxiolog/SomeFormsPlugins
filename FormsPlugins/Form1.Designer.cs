namespace FormsPlugins
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            ControlsStripMenuItem = new ToolStripMenuItem();
            ActionsToolStripMenuItem = new ToolStripMenuItem();
            ThesaurusToolStripMenuItem = new ToolStripMenuItem();
            AddElementToolStripMenuItem = new ToolStripMenuItem();
            UpdElementToolStripMenuItem = new ToolStripMenuItem();
            DelElementToolStripMenuItem = new ToolStripMenuItem();
            DocsToolStripMenuItem = new ToolStripMenuItem();
            SimpleDocToolStripMenuItem = new ToolStripMenuItem();
            TableDocToolStripMenuItem = new ToolStripMenuItem();
            ChartDocToolStripMenuItem = new ToolStripMenuItem();
            panelControl = new Panel();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { ControlsStripMenuItem, ActionsToolStripMenuItem, DocsToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(800, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "Меню";
            // 
            // ControlsStripMenuItem
            // 
            ControlsStripMenuItem.Name = "ControlsStripMenuItem";
            ControlsStripMenuItem.Size = new Size(90, 20);
            ControlsStripMenuItem.Text = "Компоненты";
            // 
            // ActionsToolStripMenuItem
            // 
            ActionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ThesaurusToolStripMenuItem, AddElementToolStripMenuItem, UpdElementToolStripMenuItem, DelElementToolStripMenuItem });
            ActionsToolStripMenuItem.Enabled = false;
            ActionsToolStripMenuItem.Name = "ActionsToolStripMenuItem";
            ActionsToolStripMenuItem.Size = new Size(70, 20);
            ActionsToolStripMenuItem.Text = "Действия";
            // 
            // ThesaurusToolStripMenuItem
            // 
            ThesaurusToolStripMenuItem.Name = "ThesaurusToolStripMenuItem";
            ThesaurusToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.I;
            ThesaurusToolStripMenuItem.Size = new Size(180, 22);
            ThesaurusToolStripMenuItem.Text = "Справочник";
            ThesaurusToolStripMenuItem.Click += ThesaurusToolStripMenuItem_Click;
            // 
            // AddElementToolStripMenuItem
            // 
            AddElementToolStripMenuItem.Name = "AddElementToolStripMenuItem";
            AddElementToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            AddElementToolStripMenuItem.Size = new Size(180, 22);
            AddElementToolStripMenuItem.Text = "Добавить";
            AddElementToolStripMenuItem.Click += AddElementToolStripMenuItem_Click;
            // 
            // UpdElementToolStripMenuItem
            // 
            UpdElementToolStripMenuItem.Name = "UpdElementToolStripMenuItem";
            UpdElementToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.U;
            UpdElementToolStripMenuItem.Size = new Size(180, 22);
            UpdElementToolStripMenuItem.Text = "Изменить";
            UpdElementToolStripMenuItem.Click += UpdElementToolStripMenuItem_Click;
            // 
            // DelElementToolStripMenuItem
            // 
            DelElementToolStripMenuItem.Name = "DelElementToolStripMenuItem";
            DelElementToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
            DelElementToolStripMenuItem.Size = new Size(180, 22);
            DelElementToolStripMenuItem.Text = "Удалить";
            DelElementToolStripMenuItem.Click += DelElementToolStripMenuItem_Click;
            // 
            // DocsToolStripMenuItem
            // 
            DocsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SimpleDocToolStripMenuItem, TableDocToolStripMenuItem, ChartDocToolStripMenuItem });
            DocsToolStripMenuItem.Enabled = false;
            DocsToolStripMenuItem.Name = "DocsToolStripMenuItem";
            DocsToolStripMenuItem.Size = new Size(82, 20);
            DocsToolStripMenuItem.Text = "Документы";
            // 
            // SimpleDocToolStripMenuItem
            // 
            SimpleDocToolStripMenuItem.Name = "SimpleDocToolStripMenuItem";
            SimpleDocToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            SimpleDocToolStripMenuItem.Size = new Size(233, 22);
            SimpleDocToolStripMenuItem.Text = "Простой документ";
            SimpleDocToolStripMenuItem.Click += SimpleDocToolStripMenuItem_Click;
            // 
            // TableDocToolStripMenuItem
            // 
            TableDocToolStripMenuItem.Name = "TableDocToolStripMenuItem";
            TableDocToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.T;
            TableDocToolStripMenuItem.Size = new Size(233, 22);
            TableDocToolStripMenuItem.Text = "Документ с таблицой";
            TableDocToolStripMenuItem.Click += TableDocToolStripMenuItem_Click;
            // 
            // ChartDocToolStripMenuItem
            // 
            ChartDocToolStripMenuItem.Name = "ChartDocToolStripMenuItem";
            ChartDocToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            ChartDocToolStripMenuItem.Size = new Size(233, 22);
            ChartDocToolStripMenuItem.Text = "Диаграмма";
            ChartDocToolStripMenuItem.Click += ChartDocToolStripMenuItem_Click;
            // 
            // panelControl
            // 
            panelControl.Dock = DockStyle.Fill;
            panelControl.Location = new Point(0, 24);
            panelControl.Name = "panelControl";
            panelControl.Size = new Size(800, 426);
            panelControl.TabIndex = 1;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelControl);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Главная форма";
            WindowState = FormWindowState.Maximized;
            KeyDown += FormMain_KeyDown;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ControlsStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DocsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SimpleDocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TableDocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChartDocToolStripMenuItem;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.ToolStripMenuItem ActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ThesaurusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UpdElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelElementToolStripMenuItem;
    }
}
