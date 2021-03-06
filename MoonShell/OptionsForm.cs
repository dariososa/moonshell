﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MoonShell
{
    public partial class OptionsForm : Form
    {
        FontDialog _fontDialog = new FontDialog();
        ColorDialog _colorDialog = new ColorDialog();
        FontConverter _fontConverter = new FontConverter();

        Color _previewBackColor = Options.CurrentOptions.BackgroundColor;
        Color _previewForeColor = Options.CurrentOptions.ForegroundColor;
        Font _previewFont = Options.CurrentOptions.Font;

        public OptionsForm()
        {
            InitializeComponent();
            Options.ApplyTheme(this);
           
            _fontDialog.ShowEffects = true;
            _fontDialog.Font = Options.CurrentOptions.Font;

            string font = _fontConverter.ConvertToString(Options.CurrentOptions.Font);

            lblFont.Text = font;
            panelBackColor.BackColor = Options.CurrentOptions.BackgroundColor;
            panelForeColor.BackColor = Options.CurrentOptions.ForegroundColor;
            lblPreview.Font = Options.CurrentOptions.Font;
            lblPreview.ForeColor = Options.CurrentOptions.ForegroundColor;
            lblPreview.BackColor = Options.CurrentOptions.BackgroundColor;

            LoadPlaces();
        }

        private void LoadPlaces()
        {
            listPlaces.Items.AddRange(Options.CurrentOptions.Places.ToArray());

            if (!string.IsNullOrEmpty(Options.CurrentOptions.StartingDirectory))
            {
                listPlaces.Text = Options.CurrentOptions.StartingDirectory;
            }
        }

        private void ResetToDefault()
        {
            _previewFont = new Font("Consolas", 11F);
            _previewBackColor = Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            _previewForeColor = Color.Lime;

            lblFont.Text = _fontConverter.ConvertToInvariantString(_previewFont);
            panelBackColor.BackColor = _previewBackColor;
            panelForeColor.BackColor = _previewForeColor;
            lblPreview.Font = _previewFont;
            lblPreview.ForeColor = _previewForeColor;
            lblPreview.BackColor = _previewBackColor;
        }

        private void SaveOptions()
        {
            Options.CurrentOptions.Font = _previewFont;
            Options.CurrentOptions.ForegroundColor = _previewForeColor;
            Options.CurrentOptions.BackgroundColor = _previewBackColor;

            if (!string.IsNullOrEmpty(listPlaces.Text))
            {
                if (Directory.Exists(listPlaces.Text))
                {
                    Options.CurrentOptions.StartingDirectory = listPlaces.Text;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("This directory does not exist!", "MoonShell", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Options.CurrentOptions.StartingDirectory = string.Empty;
                this.Close();
            }
        }

        private void PreviewFont()
        {
            if (_fontDialog.ShowDialog() == DialogResult.OK)
            {
                lblFont.Text = _fontConverter.ConvertToInvariantString(_fontDialog.Font);
                lblPreview.Font = _fontDialog.Font;
                
                _previewFont = _fontDialog.Font;
            }
        }

        private void PreviewTextColor()
        {
            _colorDialog.Color = Options.CurrentOptions.ForegroundColor;
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelForeColor.BackColor = _colorDialog.Color;
                lblPreview.ForeColor = _colorDialog.Color;

                _previewForeColor = _colorDialog.Color;
            }
        }

        private void PreviewBackgroundColor()
        {
            _colorDialog.Color = Options.CurrentOptions.BackgroundColor;
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelBackColor.BackColor = _colorDialog.Color;
                lblPreview.BackColor = _colorDialog.Color;

                _previewBackColor = _colorDialog.Color;
            }
        }

        private void Options_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PreviewFont();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PreviewTextColor();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveOptions();
        }

        private void lblFont_Click(object sender, EventArgs e)
        {
            PreviewFont();
        }

        private void panelForeColor_Click(object sender, EventArgs e)
        {
            PreviewTextColor();
        }

        private void panelBackColor_Click(object sender, EventArgs e)
        {
            PreviewBackgroundColor();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PreviewBackgroundColor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetToDefault();
        }
    }
}
