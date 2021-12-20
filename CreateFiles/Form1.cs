using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace CreateFiles
{
    public partial class Form1 : Form
    {
        private List<MyFileControls.MyFile> files = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MyFileControls.Create(txtPath.Text, txtNome.Text, txtExtensao.Text);
                LerArquivosPath();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //txtPath.Text = AppDomain.CurrentDomain.BaseDirectory + @"Arquivos\";
            //LerArquivosPath();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", txtPath.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LerArquivosPath();
        }

        private void LerArquivosPath()
        {
            listView1.Items.Clear();

            files = new List<MyFileControls.MyFile>();

            files = MyFileControls.GetFilesDirectory(txtPath.Text);


            foreach (var file in files)
            {
                listView1.Items.Add(newItem(file));
            }
        }

        private ListViewItem newItem(MyFileControls.MyFile file)
        {
            ListViewItem item = new ListViewItem();
            item.ImageIndex = 0;
            item.Text = file.nome + "." + file.extensao;
            item.Name = file.nome;

            return item;           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var selecteds = listView1.SelectedItems;

                foreach (var selected in selecteds)
                {
                    var item = (ListViewItem)selected;
                    if (files.Exists(f => f.nome.Equals(item.Name))){
                        MyFileControls.DeleteFile(files.Find(f => f.nome.Equals(item.Name)));
                    }
                }

                LerArquivosPath();
                
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirArquivo();
        }

        private void AbrirArquivo()
        {
            var selecionados = listView1.SelectedItems;

            foreach(var selecionado in selecionados)
            {
                var item = (ListViewItem)selecionado;
                var myFile = files.Find(f => f.nome.Equals(item.Name));
                Process.Start("explorer", myFile.getPathComp());
            }
        }
    }
}