namespace Laba
{
    partial class DateBox
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            textBox = new TextBox();
            toolTip1 = new ToolTip(components);
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Dock = DockStyle.Fill;
            textBox.Location = new Point(0, 0);
            textBox.Name = "textBox";
            textBox.Size = new Size(191, 31);
            textBox.TabIndex = 0;
            textBox.TextChanged += textBox_TextChanged;
            textBox.MouseLeave += textBox_MouseLeave;
            textBox.MouseHover += textBox_MouseHover;
            // 
            // DateBox
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBox);
            Name = "DateBox";
            Size = new Size(191, 32);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox;
        private ToolTip toolTip1;
    }
}
