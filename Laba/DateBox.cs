using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba
{
    public partial class DateBox : UserControl
    {
        private string? _template;
        private string? _example;
        private ToolTip toolTip;
        
        private string? date;
        public DateBox()
        {
            InitializeComponent();
            toolTip = toolTip1;
        }

        private event EventHandler? _changeValue;

        public event EventHandler ChangeValue
        {
            add => _changeValue += value;
            remove => _changeValue -= value;
        }

        public void SetExample(string Example)
        {
            _example = Example;
            toolTip.SetToolTip(textBox, _example);
        }
        public string Template
        {
            set
            {
                _template = value;
            }
        }

        public string Date
        {
            get
            {
                if (_template == null)
                {
                    throw new NullTemplateException();
                }
                if (Regex.IsMatch(textBox.Text, _template))
                {
                    return textBox.Text;
                }
                throw new InvalidArgumentException();
            }
            set
            {
                if (_template == null)
                {
                    textBox.Text = "";
                }
                else if (Regex.IsMatch(textBox.Text, _template))
                    textBox.Text = value;
                else
                    textBox.Text = "";
            }
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            _changeValue?.Invoke(sender, e);
        }

        private void textBox_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show(_example, this);
        }

        private void textBox_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(this);
        }
    }
}
