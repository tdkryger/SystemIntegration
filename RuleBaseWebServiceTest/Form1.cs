using LoanBroker.model;
using Newtonsoft.Json;
using RuleBaseWebServiceTest.RuleBaseInterface;
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
        private RuleBaseInterface.RuleBaseServiceSoapClient rbsc = new RuleBaseInterface.RuleBaseServiceSoapClient();

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
            //rbsc.AddABank(
            //    new RuleBaseInterface.Bank()
            //    {
            //        Id = (int)nudId.Value,
            //        Name = tbName.Text
            //    }
            //    );
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //rbsc.RemoveABank(
            //    new Bank()
            //    {
            //        Id = (int)nudId.Value,
            //        Name = tbName.Text
            //    }
            //    );
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            ArrayOfString banks = rbsc.GetBanks();

            foreach (string jSonRepOfBank in banks)
            {
                Bank b = JsonConvert.DeserializeObject<Bank>(jSonRepOfBank);
                listBox1.Items.Add(string.Format("ID: {0}, Name: {1}, Min. creditscore: {2}, Max creditscore: {3}, Min. amount: {4}, Max amount: {5}, Min. duration: {6}, Max duration: {7}", b.Id, b.Name, b.MinCreditScore, b.MaxCreditScore, b.MinAmount, b.MaxAmount, b.MinDuration, b.MaxDuration));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                Bank bank;
                bank = new Bank();

                bank.Id = i;
                bank.Name = string.Format("Bank number {0}", i);

                rbsc.AddABank(JsonConvert.SerializeObject(bank));

                //rbsc.AddABank(
                //new RuleBaseInterface.Bank()
                //{
                //    Id = i,
                //    Name = string.Format("Bank number {0}", i),
                //    MinCreditScore = (i + 1) * 32,
                //    MinAmount = (i + 1) * 230,
                //    MinDuration = (i + 1) * 2
                //}
                //);
            }
        }
    }
}
