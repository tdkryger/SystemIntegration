using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RuleBaseWebServiceTest
{
    public partial class Form1 : Form
    {
        private RuleBaseInterface.RuleBaseSoapClient rbsc = new RuleBaseInterface.RuleBaseSoapClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void nudId_ValueChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = (nudId.Value >= 0 && nudId.Value < 100 && !string.IsNullOrEmpty(tbName.Text));
            btnDelete.Enabled = (nudId.Value >= 0 && nudId.Value < 100 && !string.IsNullOrEmpty(tbName.Text));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            rbsc.AddABank(
                new RuleBaseInterface.Bank()
                {
                    Id = (int)nudId.Value,
                    Name = tbName.Text
                }
                );
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            rbsc.RemoveABank(
                new RuleBaseInterface.Bank()
                {
                    Id = (int)nudId.Value,
                    Name = tbName.Text
                }
                );
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            RuleBaseInterface.Bank[] banks = rbsc.GetBanks(10, 500,17, "123456-7890" );
            foreach (RuleBaseInterface.Bank b in banks)
            {
                listBox1.Items.Add(string.Format("ID: {0}, Name: {1}", b.Id, b.Name));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                rbsc.AddABank(
                new RuleBaseInterface.Bank()
                {
                    Id = i,
                    Name = string.Format("Bank number {0}", i)
                }
                );
            }
        }
    }
}
