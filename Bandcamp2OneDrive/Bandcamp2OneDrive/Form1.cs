using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Bandcamp2OneDriveLogic;

namespace Bandcamp2OneDrive
{
    public partial class Form1 : Form
    {
        private Configs _configs;
        private ArtistAlbum _artistAlbum;
        private string _inputFile;
        private string _genre;
        private Bandcamp2OneDriveConverter _converter;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _configs = Configs.GetConfigs();

                tbInputPath.Text = _configs.InputPath;
                tbOutputPath.Text = _configs.OutputPathRoot;
                cbGenres.Items.AddRange(_configs.Genres);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void cmdInputFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = _configs.InputPath;
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _inputFile = openFileDialog1.FileName;
                tbInputPath.Text = _inputFile;
            }
            _artistAlbum = ArtistAlbum.GetArtistAlbum(_inputFile);

            _genre = cbGenres.SelectedItem.ToString();

            _converter = new Bandcamp2OneDriveConverter(_configs, _genre, _inputFile, _artistAlbum.Artist, _artistAlbum.Album);

            tbOutputPath.Text = _converter.GetFullOutputPath();
        }

        private async void cmdGo_Click(object sender, EventArgs e)
        {
            await _converter.Convert4OneDrive(tbOutputPath.Text);

            MessageBox.Show("Done");
        }
    }
}
