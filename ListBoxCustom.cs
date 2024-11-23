using Microsoft.VisualBasic;
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
    public partial class ListBoxCustom : UserControl
    {
        private string? Template;
        private string? Pattern;
        private string? StartSymb;
        private string? EndSymb;
        private int? cnt;
        private Regex Regex = new(string.Empty);
        public ListBoxCustom()
        {
            InitializeComponent();
        }

        public void SetTemplate(string startSymb, string endSymb, string template)
        {
            string pattern = "\\" + startSymb + "([^" + endSymb + "]*)\\" + endSymb;
            Regex = new Regex(pattern);
            cnt = Regex.Count(template);
            if (template.StartsWith(startSymb) || template.EndsWith(endSymb))
            {
                throw new Exception("Первое и последнее слово не может быть шаблонным");
            }
            StartSymb = startSymb;
            EndSymb = endSymb;
            listBox1.Items.Clear();
            Pattern = pattern;
            Template = template;
        }

        public int SelectedIndex
        {
            get { return listBox1.SelectedIndex; }
            set { listBox1.SelectedIndex = value; }
        }
        
        public void Clear()
        {
            listBox1.Items.Clear();
        }
        public void Add<T>(T Object)
        {
            string txt = Template;
            for (int i = 0; i < cnt; i++)
            {
                string val = Regex.Match(txt, Pattern).Value;
                var tmp = typeof(T)?.GetProperty(val.Replace(StartSymb, "").Replace(EndSymb, ""))?.GetValue(Object);
                txt = txt.Replace(val, tmp.ToString());
            }
            listBox1.Items.Add(txt);
        }

        public T GetSelected<T>() where T : class, new()
        {
            Type objectType = typeof(T);
            T Object = new();
            string val = listBox1.Items?[SelectedIndex]?.ToString() ?? string.Empty;
            int last = -1;
            int leng = -1;
            int next = -1;
            string[] strings = Regex.Split(Template);
            for (int i = 0; i < strings.Length; i++)
            {
                if (i % 2 == 1)
                {
                    next = val.IndexOf(strings[i + 1]);
                    string str = val.Substring(last + leng, (next - last - leng));
                    var field = objectType.GetProperty(strings[i]);
                    field?.SetValue(Object, str);
                }
                else
                {
                    last = val.IndexOf(strings[i]);
                    leng = strings[i].Length;
                }
            }
            return Object;
        }
    }
}
