
// This file has been generated by the GUI designer. Do not modify.
namespace MyNesGTK
{
	public partial class Dialog_Palettes
	{
		private global::Gtk.Table table1;
		
		private global::Gtk.Button button16;
		
		private global::Gtk.Button button17;
		
		private global::Gtk.Button button18;
		
		private global::Gtk.Button button19;
		
		private global::Gtk.ComboBox combobox_paletteOf;
		
		private global::Gtk.ComboBox combobox_selection;
		
		private global::Gtk.HScale hscale_Brightness;
		
		private global::Gtk.HScale hscale_Contrast;
		
		private global::Gtk.HScale hscale_Gamma;
		
		private global::Gtk.HScale hscale_Hue;
		
		private global::Gtk.HScale hscale_Saturation;
		
		private global::Gtk.Image image1;
		
		private global::Gtk.Label label_Brightness;
		
		private global::Gtk.Label label_Contrast;
		
		private global::Gtk.Label label_Gamma;
		
		private global::Gtk.Label label_Hue;
		
		private global::Gtk.Label label_Saturation;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.Label label11;
		
		private global::Gtk.Label label13;
		
		private global::Gtk.Label label2;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.Label label4;
		
		private global::Gtk.Label label5;
		
		private global::Gtk.Label label7;
		
		private global::Gtk.Label label9;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MyNesGTK.Dialog_Palettes
			this.Name = "MyNesGTK.Dialog_Palettes";
			this.Title = global::Mono.Unix.Catalog.GetString ("Palettes Settings");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource ("MyNesGTK.resources.color_wheel.png");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			// Internal child MyNesGTK.Dialog_Palettes.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(12)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.button16 = new global::Gtk.Button ();
			this.button16.CanFocus = true;
			this.button16.Name = "button16";
			this.button16.UseUnderline = true;
			this.button16.Label = global::Mono.Unix.Catalog.GetString ("Load");
			this.table1.Add (this.button16);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.button16]));
			w2.TopAttach = ((uint)(8));
			w2.BottomAttach = ((uint)(9));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.button17 = new global::Gtk.Button ();
			this.button17.CanFocus = true;
			this.button17.Name = "button17";
			this.button17.UseUnderline = true;
			this.button17.Label = global::Mono.Unix.Catalog.GetString ("Save");
			this.table1.Add (this.button17);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.button17]));
			w3.TopAttach = ((uint)(9));
			w3.BottomAttach = ((uint)(10));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.button18 = new global::Gtk.Button ();
			this.button18.CanFocus = true;
			this.button18.Name = "button18";
			this.button18.UseUnderline = true;
			this.button18.Label = global::Mono.Unix.Catalog.GetString ("Flat all");
			this.table1.Add (this.button18);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.button18]));
			w4.TopAttach = ((uint)(10));
			w4.BottomAttach = ((uint)(11));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.button19 = new global::Gtk.Button ();
			this.button19.CanFocus = true;
			this.button19.Name = "button19";
			this.button19.UseUnderline = true;
			this.button19.Label = global::Mono.Unix.Catalog.GetString ("Save as .pal");
			this.table1.Add (this.button19);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.button19]));
			w5.TopAttach = ((uint)(11));
			w5.BottomAttach = ((uint)(12));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.combobox_paletteOf = global::Gtk.ComboBox.NewText ();
			this.combobox_paletteOf.AppendText (global::Mono.Unix.Catalog.GetString ("NTSC"));
			this.combobox_paletteOf.AppendText (global::Mono.Unix.Catalog.GetString ("PAL"));
			this.combobox_paletteOf.Name = "combobox_paletteOf";
			this.combobox_paletteOf.Active = 0;
			this.table1.Add (this.combobox_paletteOf);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.combobox_paletteOf]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.combobox_selection = global::Gtk.ComboBox.NewText ();
			this.combobox_selection.AppendText (global::Mono.Unix.Catalog.GetString ("AUTO"));
			this.combobox_selection.AppendText (global::Mono.Unix.Catalog.GetString ("Always NTSC"));
			this.combobox_selection.AppendText (global::Mono.Unix.Catalog.GetString ("Always PALB"));
			this.combobox_selection.Name = "combobox_selection";
			this.combobox_selection.Active = 0;
			this.table1.Add (this.combobox_selection);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.combobox_selection]));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hscale_Brightness = new global::Gtk.HScale (null);
			this.hscale_Brightness.CanFocus = true;
			this.hscale_Brightness.Name = "hscale_Brightness";
			this.hscale_Brightness.Adjustment.Lower = 500D;
			this.hscale_Brightness.Adjustment.Upper = 2000D;
			this.hscale_Brightness.Adjustment.PageIncrement = 10D;
			this.hscale_Brightness.Adjustment.StepIncrement = 1D;
			this.hscale_Brightness.Adjustment.Value = 1000D;
			this.hscale_Brightness.DrawValue = false;
			this.hscale_Brightness.Digits = 0;
			this.hscale_Brightness.ValuePos = ((global::Gtk.PositionType)(2));
			this.table1.Add (this.hscale_Brightness);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.hscale_Brightness]));
			w8.TopAttach = ((uint)(6));
			w8.BottomAttach = ((uint)(7));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hscale_Contrast = new global::Gtk.HScale (null);
			this.hscale_Contrast.CanFocus = true;
			this.hscale_Contrast.Name = "hscale_Contrast";
			this.hscale_Contrast.Adjustment.Lower = 500D;
			this.hscale_Contrast.Adjustment.Upper = 2000D;
			this.hscale_Contrast.Adjustment.PageIncrement = 10D;
			this.hscale_Contrast.Adjustment.StepIncrement = 1D;
			this.hscale_Contrast.Adjustment.Value = 1000D;
			this.hscale_Contrast.DrawValue = false;
			this.hscale_Contrast.Digits = 0;
			this.hscale_Contrast.ValuePos = ((global::Gtk.PositionType)(2));
			this.table1.Add (this.hscale_Contrast);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.hscale_Contrast]));
			w9.TopAttach = ((uint)(5));
			w9.BottomAttach = ((uint)(6));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hscale_Gamma = new global::Gtk.HScale (null);
			this.hscale_Gamma.CanFocus = true;
			this.hscale_Gamma.Name = "hscale_Gamma";
			this.hscale_Gamma.Adjustment.Lower = 1000D;
			this.hscale_Gamma.Adjustment.Upper = 2500D;
			this.hscale_Gamma.Adjustment.PageIncrement = 10D;
			this.hscale_Gamma.Adjustment.StepIncrement = 1D;
			this.hscale_Gamma.Adjustment.Value = 1800D;
			this.hscale_Gamma.DrawValue = false;
			this.hscale_Gamma.Digits = 0;
			this.hscale_Gamma.ValuePos = ((global::Gtk.PositionType)(2));
			this.table1.Add (this.hscale_Gamma);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1 [this.hscale_Gamma]));
			w10.TopAttach = ((uint)(7));
			w10.BottomAttach = ((uint)(8));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hscale_Hue = new global::Gtk.HScale (null);
			this.hscale_Hue.CanFocus = true;
			this.hscale_Hue.Name = "hscale_Hue";
			this.hscale_Hue.Adjustment.Lower = -1000D;
			this.hscale_Hue.Adjustment.Upper = 1000D;
			this.hscale_Hue.Adjustment.PageIncrement = 10D;
			this.hscale_Hue.Adjustment.StepIncrement = 1D;
			this.hscale_Hue.DrawValue = false;
			this.hscale_Hue.Digits = 0;
			this.hscale_Hue.ValuePos = ((global::Gtk.PositionType)(2));
			this.table1.Add (this.hscale_Hue);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1 [this.hscale_Hue]));
			w11.TopAttach = ((uint)(4));
			w11.BottomAttach = ((uint)(5));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hscale_Saturation = new global::Gtk.HScale (null);
			this.hscale_Saturation.CanFocus = true;
			this.hscale_Saturation.Name = "hscale_Saturation";
			this.hscale_Saturation.Adjustment.Upper = 5000D;
			this.hscale_Saturation.Adjustment.PageIncrement = 10D;
			this.hscale_Saturation.Adjustment.StepIncrement = 1D;
			this.hscale_Saturation.Adjustment.Value = 1000D;
			this.hscale_Saturation.DrawValue = false;
			this.hscale_Saturation.Digits = 0;
			this.hscale_Saturation.ValuePos = ((global::Gtk.PositionType)(2));
			this.table1.Add (this.hscale_Saturation);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1 [this.hscale_Saturation]));
			w12.TopAttach = ((uint)(3));
			w12.BottomAttach = ((uint)(4));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.image1 = new global::Gtk.Image ();
			this.image1.Name = "image1";
			this.table1.Add (this.image1);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1 [this.image1]));
			w13.TopAttach = ((uint)(3));
			w13.BottomAttach = ((uint)(12));
			w13.LeftAttach = ((uint)(3));
			w13.RightAttach = ((uint)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label_Brightness = new global::Gtk.Label ();
			this.label_Brightness.Name = "label_Brightness";
			this.label_Brightness.LabelProp = global::Mono.Unix.Catalog.GetString ("1");
			this.table1.Add (this.label_Brightness);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1 [this.label_Brightness]));
			w14.TopAttach = ((uint)(6));
			w14.BottomAttach = ((uint)(7));
			w14.LeftAttach = ((uint)(2));
			w14.RightAttach = ((uint)(3));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label_Contrast = new global::Gtk.Label ();
			this.label_Contrast.Name = "label_Contrast";
			this.label_Contrast.LabelProp = global::Mono.Unix.Catalog.GetString ("1");
			this.table1.Add (this.label_Contrast);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1 [this.label_Contrast]));
			w15.TopAttach = ((uint)(5));
			w15.BottomAttach = ((uint)(6));
			w15.LeftAttach = ((uint)(2));
			w15.RightAttach = ((uint)(3));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label_Gamma = new global::Gtk.Label ();
			this.label_Gamma.Name = "label_Gamma";
			this.label_Gamma.LabelProp = global::Mono.Unix.Catalog.GetString ("9");
			this.table1.Add (this.label_Gamma);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1 [this.label_Gamma]));
			w16.TopAttach = ((uint)(7));
			w16.BottomAttach = ((uint)(8));
			w16.LeftAttach = ((uint)(2));
			w16.RightAttach = ((uint)(3));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label_Hue = new global::Gtk.Label ();
			this.label_Hue.Name = "label_Hue";
			this.label_Hue.LabelProp = global::Mono.Unix.Catalog.GetString ("0");
			this.table1.Add (this.label_Hue);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1 [this.label_Hue]));
			w17.TopAttach = ((uint)(4));
			w17.BottomAttach = ((uint)(5));
			w17.LeftAttach = ((uint)(2));
			w17.RightAttach = ((uint)(3));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label_Saturation = new global::Gtk.Label ();
			this.label_Saturation.Name = "label_Saturation";
			this.label_Saturation.LabelProp = global::Mono.Unix.Catalog.GetString ("1");
			this.table1.Add (this.label_Saturation);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1 [this.label_Saturation]));
			w18.TopAttach = ((uint)(3));
			w18.BottomAttach = ((uint)(4));
			w18.LeftAttach = ((uint)(2));
			w18.RightAttach = ((uint)(3));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Edit palette of:");
			this.table1.Add (this.label1);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1 [this.label1]));
			w19.TopAttach = ((uint)(1));
			w19.BottomAttach = ((uint)(2));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label11 = new global::Gtk.Label ();
			this.label11.Name = "label11";
			this.label11.LabelProp = global::Mono.Unix.Catalog.GetString ("Gamma:");
			this.table1.Add (this.label11);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1 [this.label11]));
			w20.TopAttach = ((uint)(7));
			w20.BottomAttach = ((uint)(8));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label13 = new global::Gtk.Label ();
			this.label13.Name = "label13";
			this.label13.LabelProp = global::Mono.Unix.Catalog.GetString ("Preview:");
			this.table1.Add (this.label13);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1 [this.label13]));
			w21.TopAttach = ((uint)(2));
			w21.BottomAttach = ((uint)(3));
			w21.LeftAttach = ((uint)(3));
			w21.RightAttach = ((uint)(4));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Colors:");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w22.TopAttach = ((uint)(2));
			w22.BottomAttach = ((uint)(3));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Saturation:");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w23.TopAttach = ((uint)(3));
			w23.BottomAttach = ((uint)(4));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label ();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString ("Palette selection:");
			this.table1.Add (this.label4);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table1 [this.label4]));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label ();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString ("Hue:");
			this.table1.Add (this.label5);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table1 [this.label5]));
			w25.TopAttach = ((uint)(4));
			w25.BottomAttach = ((uint)(5));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Contrast:");
			this.table1.Add (this.label7);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table1 [this.label7]));
			w26.TopAttach = ((uint)(5));
			w26.BottomAttach = ((uint)(6));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label9 = new global::Gtk.Label ();
			this.label9.Name = "label9";
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString ("Brightness:");
			this.table1.Add (this.label9);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table1 [this.label9]));
			w27.TopAttach = ((uint)(6));
			w27.BottomAttach = ((uint)(7));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add (this.table1);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(w1 [this.table1]));
			w28.Position = 0;
			w28.Expand = false;
			w28.Fill = false;
			// Internal child MyNesGTK.Dialog_Palettes.ActionArea
			global::Gtk.HButtonBox w29 = this.ActionArea;
			w29.Name = "dialog1_ActionArea";
			w29.Spacing = 10;
			w29.BorderWidth = ((uint)(5));
			w29.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w30 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w29 [this.buttonCancel]));
			w30.Expand = false;
			w30.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w31 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w29 [this.buttonOk]));
			w31.Position = 1;
			w31.Expand = false;
			w31.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 809;
			this.DefaultHeight = 486;
			this.Show ();
			this.hscale_Saturation.ValueChanged += new global::System.EventHandler (this.OnHscaleSaturationValueChanged);
			this.hscale_Hue.ValueChanged += new global::System.EventHandler (this.OnHscaleHueValueChanged);
			this.hscale_Gamma.ValueChanged += new global::System.EventHandler (this.OnHscaleGammaValueChanged);
			this.hscale_Contrast.ValueChanged += new global::System.EventHandler (this.OnHscaleContrastValueChanged);
			this.hscale_Brightness.ValueChanged += new global::System.EventHandler (this.OnHscaleBrightnessValueChanged);
			this.combobox_paletteOf.Changed += new global::System.EventHandler (this.OnComboboxPaletteOfChanged);
			this.button19.Pressed += new global::System.EventHandler (this.OnButton19Pressed);
			this.button18.Pressed += new global::System.EventHandler (this.OnButton18Pressed);
			this.button17.Pressed += new global::System.EventHandler (this.OnButton17Pressed);
			this.button16.Pressed += new global::System.EventHandler (this.OnButton16Pressed);
		}
	}
}
