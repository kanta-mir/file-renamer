using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace File_Renamer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var folderPath = txtFolderPath.Text;
            var replaceThis = txtSourceText.Text;
            var withThis = txtTargetText.Text;

            DirectoryInfo d = new DirectoryInfo(folderPath);
            FileInfo[] infos = d.GetFiles();

            foreach (FileInfo f in infos)
            {
                try
                {
                    File.Move(f.FullName, f.FullName.Replace(replaceThis, withThis));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {f.FullName} " + ex.Message);
                }
            }

        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.RestoreDirectory = true;

            if (txtFolderPath.Text != "")
            {
                dialog.InitialDirectory = txtFolderPath.Text;
            }
            else
            {
                dialog.InitialDirectory = "C:";
            }


            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                txtFolderPath.Text = dialog.FileName;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            List<string> filepaths = new List<string>();
            foreach (var s in (string[])e.Data.GetData(DataFormats.FileDrop, false))
            {
                txtFolderPath.Text = s;
                if (Directory.Exists(s))
                {
                    //Add files from folder
                    filepaths.AddRange(Directory.GetFiles(s));
                }
                else
                {
                    //Add filepath
                    filepaths.Add(s);
                }
            }
        }

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}
