using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba
{
    public partial class ComboBoxCustom : UserControl
    {
        public ComboBoxCustom()
        {
            InitializeComponent();
        }
        public event Action<string>? SelectedChanged;
        public void Add(string str)
        {
            comboBox.Items.Add(str);
        }

        public void Clear()
        {
            comboBox.Items.Clear();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedChanged?.Invoke(comboBox.SelectedItem?.ToString() ?? string.Empty);
        }

        public string selectedValue
        {
            get
            {
                return comboBox.SelectedItem?.ToString() ?? string.Empty;
            }
            set
            {
                if (comboBox.Items.Contains(value))
                {
                    comboBox.SelectedItem = value;
                }
            }
        }



    }
}
