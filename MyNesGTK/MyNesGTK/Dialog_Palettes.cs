﻿//
//  Dialog_Palettes.cs
//
//  Author:
//       Ala Ibrahim Hadid <ahdsoftwares@hotmail.com>
//
//  Copyright (c) 2009 - 2015 Ala Ibrahim Hadid
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.IO;
using System.Collections.Generic;
using Gtk;
using Gdk;
using MyNes.Core;

namespace MyNesGTK
{
    public partial class Dialog_Palettes : Gtk.Dialog
    {
        public Dialog_Palettes()
        {
            this.Build();
            MyNesSDL.Settings.LoadSettings(System.IO.Path.Combine(MainClass.WorkingFolder, "SDLSettings.conf"));
            // load settings
            if (MyNesSDL.Settings.Palette_AutoSelect)
                combobox_selection.Active = 0;
            else
            {
                combobox_selection.Active = MyNesSDL.Settings.Palette_UseNTSCPalette ? 1 : 2;
            }
            NTSCPaletteGenerator.brightness = MyNesSDL.Settings.Palette_NTSC_brightness;
            NTSCPaletteGenerator.contrast = MyNesSDL.Settings.Palette_NTSC_contrast;
            NTSCPaletteGenerator.gamma = MyNesSDL.Settings.Palette_NTSC_gamma;
            NTSCPaletteGenerator.hue_tweak = MyNesSDL.Settings.Palette_NTSC_hue_tweak;
            NTSCPaletteGenerator.saturation = MyNesSDL.Settings.Palette_NTSC_saturation;

            PALBPaletteGenerator.brightness = MyNesSDL.Settings.Palette_PALB_brightness;
            PALBPaletteGenerator.contrast = MyNesSDL.Settings.Palette_PALB_contrast;
            PALBPaletteGenerator.gamma = MyNesSDL.Settings.Palette_PALB_gamma;
            PALBPaletteGenerator.hue_tweak = MyNesSDL.Settings.Palette_PALB_hue_tweak;
            PALBPaletteGenerator.saturation = MyNesSDL.Settings.Palette_PALB_saturation;

            hscale_Brightness.Value = (int)(MyNesSDL.Settings.Palette_NTSC_brightness * 1000);
            hscale_Contrast.Value = (int)(MyNesSDL.Settings.Palette_NTSC_contrast * 1000);
            hscale_Gamma.Value = (int)(MyNesSDL.Settings.Palette_NTSC_gamma * 1000);
            hscale_Hue.Value = (int)(MyNesSDL.Settings.Palette_NTSC_hue_tweak * 1000);
            hscale_Saturation.Value = (int)(MyNesSDL.Settings.Palette_NTSC_saturation * 1000);

            label_Brightness.Text = MyNesSDL.Settings.Palette_NTSC_brightness.ToString("F3");
            label_Contrast.Text = MyNesSDL.Settings.Palette_NTSC_contrast.ToString("F3");
            label_Gamma.Text = MyNesSDL.Settings.Palette_NTSC_gamma.ToString("F3");
            label_Hue.Text = MyNesSDL.Settings.Palette_NTSC_hue_tweak.ToString("F3");
            label_Saturation.Text = MyNesSDL.Settings.Palette_NTSC_saturation.ToString("F3");
            if (combobox_paletteOf.Active == 0)
            {
                int[] palette = NTSCPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
            else
            {
                int[] palette = PALBPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
        }

        unsafe void ShowPalette(int[] PaletteFormat)
        {
            Gdk.Window gr = image1.GdkWindow;
            int y = 0;
            int x = 0;
            int xoffset = 500;
            int yoffset = 70;
            int w = 16;
            int h = 10;
            for (int j = 0; j < PaletteFormat.Length; j++)
            {
                y = (j / 16) * h;
                x = (j % w) * w;
                List<byte> buff = new List<byte>();
                for (int i = 0; i < w * h; i++)
                {
                    int color = PaletteFormat[j];
                    buff.Add((byte)((color >> 16) & 0xFF));//Red
                    buff.Add((byte)((color >> 8) & 0xFF));//Green
                    buff.Add((byte)((color >> 0) & 0xFF));//Blue
                }
                gr.DrawRgbImage(image1.Style.BackgroundGC(StateType.Normal), xoffset + x, yoffset + y, w, h, 
                    RgbDither.Normal, buff.ToArray(), 0);
            }
        }

        public void SaveSettings()
        {
            MyNesSDL.Settings.Palette_NTSC_brightness = NTSCPaletteGenerator.brightness;
            MyNesSDL.Settings.Palette_NTSC_contrast = NTSCPaletteGenerator.contrast;
            MyNesSDL.Settings.Palette_NTSC_gamma = NTSCPaletteGenerator.gamma;
            MyNesSDL.Settings.Palette_NTSC_hue_tweak = NTSCPaletteGenerator.hue_tweak;
            MyNesSDL.Settings.Palette_NTSC_saturation = NTSCPaletteGenerator.saturation;

            MyNesSDL.Settings.Palette_PALB_brightness = PALBPaletteGenerator.brightness;
            MyNesSDL.Settings.Palette_PALB_contrast = PALBPaletteGenerator.contrast;
            MyNesSDL.Settings.Palette_PALB_gamma = PALBPaletteGenerator.gamma;
            MyNesSDL.Settings.Palette_PALB_hue_tweak = PALBPaletteGenerator.hue_tweak;
            MyNesSDL.Settings.Palette_PALB_saturation = PALBPaletteGenerator.saturation;

            int[] palette = (combobox_paletteOf.Active == 0) ? NTSCPaletteGenerator.GeneratePalette() :
                PALBPaletteGenerator.GeneratePalette();
            ShowPalette(palette);
          
            switch (combobox_selection.Active)
            {
                case 0:
                    MyNesSDL.Settings.Palette_UseNTSCPalette = false;
                    MyNesSDL.Settings.Palette_AutoSelect = true;
                    break;
                case 1:
                    MyNesSDL.Settings.Palette_UseNTSCPalette = true;
                    MyNesSDL.Settings.Palette_AutoSelect = false;
                    break;
                case 2:
                    MyNesSDL.Settings.Palette_UseNTSCPalette = false;
                    MyNesSDL.Settings.Palette_AutoSelect = false;
                    break;
            }
                
            MyNesSDL.Settings.SaveSettings();
        }

        protected void OnHscaleSaturationValueChanged(object sender, EventArgs e)
        {
            float v = (float)hscale_Saturation.Value;
            if (combobox_paletteOf.Active == 0)
            {
                NTSCPaletteGenerator.saturation = (v / 1000);
                label_Saturation.Text = NTSCPaletteGenerator.saturation.ToString("F3");
                int[] palette = NTSCPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
            else
            {
                PALBPaletteGenerator.saturation = (v / 1000);
                label_Saturation.Text = PALBPaletteGenerator.saturation.ToString("F3");
                int[] palette = PALBPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
        }

        protected void OnHscaleHueValueChanged(object sender, EventArgs e)
        {
            float v = (float)hscale_Hue.Value;
            if (combobox_paletteOf.Active == 0)
            {
                NTSCPaletteGenerator.hue_tweak = (v / 1000);
                label_Hue.Text = NTSCPaletteGenerator.hue_tweak.ToString("F3");
                int[] palette = NTSCPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
            else
            {
                PALBPaletteGenerator.hue_tweak = (v / 1000);
                label_Hue.Text = PALBPaletteGenerator.hue_tweak.ToString("F3");
                int[] palette = PALBPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
        }

        protected void OnHscaleContrastValueChanged(object sender, EventArgs e)
        {
            float v = (float)hscale_Contrast.Value;
            if (combobox_paletteOf.Active == 0)
            {
                NTSCPaletteGenerator.contrast = (v / 1000);
                label_Contrast.Text = NTSCPaletteGenerator.contrast.ToString("F3");
                int[] palette = NTSCPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
            else
            {
                PALBPaletteGenerator.contrast = (v / 1000);
                label_Contrast.Text = PALBPaletteGenerator.contrast.ToString("F3");
                int[] palette = PALBPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
        }

        protected void OnHscaleBrightnessValueChanged(object sender, EventArgs e)
        {
            float v = (float)hscale_Brightness.Value;
            if (combobox_paletteOf.Active == 0)
            {
                NTSCPaletteGenerator.brightness = (v / 1000);
                label_Brightness.Text = NTSCPaletteGenerator.brightness.ToString("F3");
                int[] palette = NTSCPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
            else
            {
                PALBPaletteGenerator.brightness = (v / 1000);
                label_Brightness.Text = PALBPaletteGenerator.brightness.ToString("F3");
                int[] palette = PALBPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
        }

        protected void OnHscaleGammaValueChanged(object sender, EventArgs e)
        {
            float v = (float)hscale_Gamma.Value;
            if (combobox_paletteOf.Active == 0)
            {
                NTSCPaletteGenerator.gamma = (v / 1000);
                label_Gamma.Text = NTSCPaletteGenerator.gamma.ToString("F3");
                int[] palette = NTSCPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
            else
            {
                PALBPaletteGenerator.gamma = (v / 1000);
                label_Gamma.Text = PALBPaletteGenerator.gamma.ToString("F3");
                int[] palette = PALBPaletteGenerator.GeneratePalette();
                ShowPalette(palette);
            }
        }

        protected void OnComboboxPaletteOfChanged(object sender, EventArgs e)
        {
            if (combobox_paletteOf.Active == 0)
                ShowPalette(NTSCPaletteGenerator.GeneratePalette());
            else
                ShowPalette(PALBPaletteGenerator.GeneratePalette());
        }
        // Load
        protected void OnButton16Pressed(object sender, EventArgs e)
        {
            FileChooserDialog openD = new FileChooserDialog("Open My Nes palette present file",
                                          this, FileChooserAction.Open,
                                          Stock.Cancel, ResponseType.Cancel,
                                          Stock.Open, ResponseType.Accept);
            FileFilter filter = new FileFilter();
            filter.Name = "My Nes Palette Present (*.mnpp)";
            filter.AddMimeType("mnpp");
            filter.AddPattern("*.mnpp");
            openD.AddFilter(filter);
            if (openD.Run() == (int)ResponseType.Accept)
            {
                string[] lines = File.ReadAllLines(openD.Filename);
                if (combobox_paletteOf.Active == 0)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] pars = lines[i].Split(new char[] { '=' });
                        switch (pars[0])
                        {
                            case "Brightness":
                                NTSCPaletteGenerator.brightness = float.Parse(pars[1]);
                                hscale_Brightness.Value = (int)(NTSCPaletteGenerator.brightness * 1000);
                                break;
                            case "Contrast":
                                NTSCPaletteGenerator.contrast = float.Parse(pars[1]);
                                hscale_Contrast.Value = (int)(NTSCPaletteGenerator.contrast * 1000);
                                break;
                            case "Gamma":
                                NTSCPaletteGenerator.gamma = float.Parse(pars[1]);
                                hscale_Gamma.Value = (int)(NTSCPaletteGenerator.gamma * 1000);
                                break;
                            case "Hue":
                                NTSCPaletteGenerator.hue_tweak = float.Parse(pars[1]);
                                hscale_Hue.Value = (int)(NTSCPaletteGenerator.hue_tweak * 1000);
                                break;
                            case "Saturation":
                                NTSCPaletteGenerator.saturation = float.Parse(pars[1]);
                                hscale_Saturation.Value = (int)(NTSCPaletteGenerator.saturation * 1000);
                                break;
                        }
                    }
                    label_Brightness.Text = NTSCPaletteGenerator.brightness.ToString("F3");
                    label_Contrast.Text = NTSCPaletteGenerator.contrast.ToString("F3");
                    label_Gamma.Text = NTSCPaletteGenerator.gamma.ToString("F3");
                    label_Hue.Text = NTSCPaletteGenerator.hue_tweak.ToString("F3");
                    label_Saturation.Text = NTSCPaletteGenerator.saturation.ToString("F3");

                    int[] palette = NTSCPaletteGenerator.GeneratePalette();
                    ShowPalette(palette);
                }
                else
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] pars = lines[i].Split(new char[] { '=' });
                        switch (pars[0])
                        {
                            case "Brightness":
                                PALBPaletteGenerator.brightness = float.Parse(pars[1]);
                                hscale_Brightness.Value = (int)(PALBPaletteGenerator.brightness * 1000);
                                break;
                            case "Contrast":
                                PALBPaletteGenerator.contrast = float.Parse(pars[1]);
                                hscale_Contrast.Value = (int)(PALBPaletteGenerator.contrast * 1000);
                                break;
                            case "Gamma":
                                PALBPaletteGenerator.gamma = float.Parse(pars[1]);
                                hscale_Gamma.Value = (int)(PALBPaletteGenerator.gamma * 1000);
                                break;
                            case "Hue":
                                PALBPaletteGenerator.hue_tweak = float.Parse(pars[1]);
                                hscale_Hue.Value = (int)(PALBPaletteGenerator.hue_tweak * 1000);
                                break;
                            case "Saturation":
                                PALBPaletteGenerator.saturation = float.Parse(pars[1]);
                                hscale_Saturation.Value = (int)(PALBPaletteGenerator.saturation * 1000);
                                break;
                        }
                    }
                    label_Brightness.Text = PALBPaletteGenerator.brightness.ToString("F3");
                    label_Contrast.Text = PALBPaletteGenerator.contrast.ToString("F3");
                    label_Gamma.Text = PALBPaletteGenerator.gamma.ToString("F3");
                    label_Hue.Text = PALBPaletteGenerator.hue_tweak.ToString("F3");
                    label_Saturation.Text = PALBPaletteGenerator.saturation.ToString("F3");
                    int[] palette = PALBPaletteGenerator.GeneratePalette();
                    ShowPalette(palette);
                }
            }
            openD.Destroy();
        }
        // Save
        protected void OnButton17Pressed(object sender, EventArgs e)
        {
            FileChooserDialog sav = new FileChooserDialog("Save My Nes palette present file",
                                        this, FileChooserAction.Save,
                                        Stock.Cancel, ResponseType.Cancel,
                                        Stock.Save, ResponseType.Accept);
            FileFilter filter = new FileFilter();
            filter.Name = "My Nes Palette Present (*.mnpp)";
            filter.AddMimeType("mnpp");
            filter.AddPattern("*.mnpp");
            sav.AddFilter(filter);
            sav.SetFilename("present.mnpp");
            if (sav.Run() == (int)ResponseType.Accept)
            {
                List<string> lines = new List<string>();
                lines.Add("Brightness=" + NTSCPaletteGenerator.brightness);
                lines.Add("Contrast=" + NTSCPaletteGenerator.contrast);
                lines.Add("Gamma=" + NTSCPaletteGenerator.gamma);
                lines.Add("Hue=" + NTSCPaletteGenerator.hue_tweak);
                lines.Add("Saturation=" + NTSCPaletteGenerator.saturation);
                File.WriteAllLines(sav.Filename, lines.ToArray());
            }
            sav.Destroy();
        }
        // Flat all
        protected void OnButton18Pressed(object sender, EventArgs e)
        {
            if (combobox_paletteOf.Active == 0)
            {
                NTSCPaletteGenerator.saturation = 1.0f;
                hscale_Saturation.Value = 1000;
                NTSCPaletteGenerator.hue_tweak = 0.0f;
                hscale_Hue.Value = 0;
                NTSCPaletteGenerator.contrast = 1.0f;
                hscale_Contrast.Value = 1000;
                NTSCPaletteGenerator.brightness = 1.0f;
                hscale_Brightness.Value = 1000;
                NTSCPaletteGenerator.gamma = 1.8f;
                hscale_Gamma.Value = 1800;

                label_Saturation.Text = NTSCPaletteGenerator.saturation.ToString("F3");
                label_Hue.Text = NTSCPaletteGenerator.hue_tweak.ToString("F3");
                label_Contrast.Text = NTSCPaletteGenerator.contrast.ToString("F3");
                label_Brightness.Text = NTSCPaletteGenerator.brightness.ToString("F3");
                label_Gamma.Text = NTSCPaletteGenerator.gamma.ToString("F3");

                int[] palette = NTSCPaletteGenerator.GeneratePalette();
                ShowPalette(palette);

            }
            else
            {
                PALBPaletteGenerator.saturation = 1.0f;
                hscale_Saturation.Value = 1000;
                PALBPaletteGenerator.hue_tweak = 0.0f;
                hscale_Hue.Value = 0;
                PALBPaletteGenerator.contrast = 1.0f;
                hscale_Contrast.Value = 1000;
                PALBPaletteGenerator.brightness = 1.0f;
                hscale_Brightness.Value = 1000;
                PALBPaletteGenerator.gamma = 1.8f;
                hscale_Gamma.Value = 1800;

                label_Saturation.Text = PALBPaletteGenerator.saturation.ToString("F3");
                label_Hue.Text = PALBPaletteGenerator.hue_tweak.ToString("F3");
                label_Contrast.Text = PALBPaletteGenerator.contrast.ToString("F3");
                label_Brightness.Text = PALBPaletteGenerator.brightness.ToString("F3");
                label_Gamma.Text = PALBPaletteGenerator.gamma.ToString("F3");

                int[] palette = PALBPaletteGenerator.GeneratePalette();
                ShowPalette(palette);

            }
        }
        // Save as .pal
        protected void OnButton19Pressed(object sender, EventArgs e)
        {
            FileChooserDialog sav = new FileChooserDialog("Save Palette file",
                                        this, FileChooserAction.Save,
                                        Stock.Cancel, ResponseType.Cancel,
                                        Stock.Save, ResponseType.Accept);
            FileFilter filter1 = new FileFilter();
            filter1.Name = "Palette file '64 indexes' (*.pal)";
            filter1.AddMimeType("pal");
            filter1.AddPattern("*.pal");
            sav.AddFilter(filter1);
            FileFilter filter2 = new FileFilter();
            filter2.Name = "Palette file '512 indexes' (*.pal)";
            filter2.AddMimeType("pal");
            filter2.AddPattern("*.pal");
            sav.AddFilter(filter2);
            sav.SetFilename("palette.pal");
            if (sav.Run() == (int)ResponseType.Accept)
            {
                //get pallete
                List<byte> palette = new List<byte>();
                int[] NesPalette = (combobox_paletteOf.Active == 0) ? NTSCPaletteGenerator.GeneratePalette() :
                    PALBPaletteGenerator.GeneratePalette();

                for (int i = 0; i < ((sav.Filter == filter1) ? 64 : 512); i++)
                {
                    int color = NesPalette[i];
                    palette.Add((byte)((color >> 16) & 0xFF));//Red
                    palette.Add((byte)((color >> 8) & 0xFF));//Green
                    palette.Add((byte)((color >> 0) & 0xFF));//Blue
                }

                Stream str = new FileStream(sav.Filename, FileMode.Create, FileAccess.Write);
                str.Write(palette.ToArray(), 0, palette.Count);
                str.Close();
            }
            sav.Destroy();
        }
    }
}

