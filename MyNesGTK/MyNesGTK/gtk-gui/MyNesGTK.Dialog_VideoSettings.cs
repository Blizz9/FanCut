
// This file has been generated by the GUI designer. Do not modify.
namespace MyNesGTK
{
	public partial class Dialog_VideoSettings
	{
		private global::Gtk.Table table1;
		
		private global::Gtk.Button button107;
		
		private global::Gtk.CheckButton checkbutton_autoResizeToFitEmu;
		
		private global::Gtk.CheckButton checkbutton_hideLines;
		
		private global::Gtk.CheckButton checkbutton_keepAspectRatio;
		
		private global::Gtk.CheckButton checkbutton_showFPS;
		
		private global::Gtk.CheckButton checkbutton_showNotification;
		
		private global::Gtk.CheckButton checkbutton_startFullScreen;
		
		private global::Gtk.ComboBox combobox_fullscreenRes;
		
		private global::Gtk.Label label2;
		
		private global::Gtk.Label label3;
		
		private global::Gtk.SpinButton spinbutton_stretchMulti;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MyNesGTK.Dialog_VideoSettings
			this.Name = "MyNesGTK.Dialog_VideoSettings";
			this.Title = global::Mono.Unix.Catalog.GetString ("Video Settings");
			this.Icon = global::Gdk.Pixbuf.LoadFromResource ("MyNesGTK.resources.monitor.png");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.Modal = true;
			// Internal child MyNesGTK.Dialog_VideoSettings.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			w1.BorderWidth = ((uint)(2));
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table (((uint)(9)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.button107 = new global::Gtk.Button ();
			this.button107.CanFocus = true;
			this.button107.Name = "button107";
			this.button107.UseUnderline = true;
			this.button107.Label = global::Mono.Unix.Catalog.GetString ("Reset to defaults");
			this.table1.Add (this.button107);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1 [this.button107]));
			w2.TopAttach = ((uint)(8));
			w2.BottomAttach = ((uint)(9));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.checkbutton_autoResizeToFitEmu = new global::Gtk.CheckButton ();
			this.checkbutton_autoResizeToFitEmu.CanFocus = true;
			this.checkbutton_autoResizeToFitEmu.Name = "checkbutton_autoResizeToFitEmu";
			this.checkbutton_autoResizeToFitEmu.Label = global::Mono.Unix.Catalog.GetString ("Auto resize window to fit emulation size");
			this.checkbutton_autoResizeToFitEmu.Active = true;
			this.checkbutton_autoResizeToFitEmu.DrawIndicator = true;
			this.checkbutton_autoResizeToFitEmu.UseUnderline = true;
			this.table1.Add (this.checkbutton_autoResizeToFitEmu);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1 [this.checkbutton_autoResizeToFitEmu]));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.checkbutton_hideLines = new global::Gtk.CheckButton ();
			this.checkbutton_hideLines.CanFocus = true;
			this.checkbutton_hideLines.Name = "checkbutton_hideLines";
			this.checkbutton_hideLines.Label = global::Mono.Unix.Catalog.GetString ("Hide lines (8 for NTSC, 1 for PALB/DENDY)");
			this.checkbutton_hideLines.DrawIndicator = true;
			this.checkbutton_hideLines.UseUnderline = true;
			this.table1.Add (this.checkbutton_hideLines);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1 [this.checkbutton_hideLines]));
			w4.TopAttach = ((uint)(5));
			w4.BottomAttach = ((uint)(6));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.checkbutton_keepAspectRatio = new global::Gtk.CheckButton ();
			this.checkbutton_keepAspectRatio.CanFocus = true;
			this.checkbutton_keepAspectRatio.Name = "checkbutton_keepAspectRatio";
			this.checkbutton_keepAspectRatio.Label = global::Mono.Unix.Catalog.GetString ("Keep aspect ratio");
			this.checkbutton_keepAspectRatio.DrawIndicator = true;
			this.checkbutton_keepAspectRatio.UseUnderline = true;
			this.table1.Add (this.checkbutton_keepAspectRatio);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1 [this.checkbutton_keepAspectRatio]));
			w5.TopAttach = ((uint)(4));
			w5.BottomAttach = ((uint)(5));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.checkbutton_showFPS = new global::Gtk.CheckButton ();
			this.checkbutton_showFPS.CanFocus = true;
			this.checkbutton_showFPS.Name = "checkbutton_showFPS";
			this.checkbutton_showFPS.Label = global::Mono.Unix.Catalog.GetString ("Show FPS");
			this.checkbutton_showFPS.DrawIndicator = true;
			this.checkbutton_showFPS.UseUnderline = true;
			this.table1.Add (this.checkbutton_showFPS);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1 [this.checkbutton_showFPS]));
			w6.TopAttach = ((uint)(7));
			w6.BottomAttach = ((uint)(8));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.checkbutton_showNotification = new global::Gtk.CheckButton ();
			this.checkbutton_showNotification.CanFocus = true;
			this.checkbutton_showNotification.Name = "checkbutton_showNotification";
			this.checkbutton_showNotification.Label = global::Mono.Unix.Catalog.GetString ("Show notifications");
			this.checkbutton_showNotification.DrawIndicator = true;
			this.checkbutton_showNotification.UseUnderline = true;
			this.table1.Add (this.checkbutton_showNotification);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1 [this.checkbutton_showNotification]));
			w7.TopAttach = ((uint)(6));
			w7.BottomAttach = ((uint)(7));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.checkbutton_startFullScreen = new global::Gtk.CheckButton ();
			this.checkbutton_startFullScreen.CanFocus = true;
			this.checkbutton_startFullScreen.Name = "checkbutton_startFullScreen";
			this.checkbutton_startFullScreen.Label = global::Mono.Unix.Catalog.GetString ("Start in fullscreen");
			this.checkbutton_startFullScreen.DrawIndicator = true;
			this.checkbutton_startFullScreen.UseUnderline = true;
			this.table1.Add (this.checkbutton_startFullScreen);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1 [this.checkbutton_startFullScreen]));
			w8.TopAttach = ((uint)(2));
			w8.BottomAttach = ((uint)(3));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.combobox_fullscreenRes = global::Gtk.ComboBox.NewText ();
			this.combobox_fullscreenRes.Name = "combobox_fullscreenRes";
			this.table1.Add (this.combobox_fullscreenRes);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1 [this.combobox_fullscreenRes]));
			w9.TopAttach = ((uint)(3));
			w9.BottomAttach = ((uint)(4));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label ();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Stretch multiply: ");
			this.table1.Add (this.label2);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1 [this.label2]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label ();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString ("Fullscreen resolution:");
			this.table1.Add (this.label3);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1 [this.label3]));
			w11.TopAttach = ((uint)(3));
			w11.BottomAttach = ((uint)(4));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.spinbutton_stretchMulti = new global::Gtk.SpinButton (1D, 9D, 1D);
			this.spinbutton_stretchMulti.CanFocus = true;
			this.spinbutton_stretchMulti.Name = "spinbutton_stretchMulti";
			this.spinbutton_stretchMulti.Adjustment.PageIncrement = 1D;
			this.spinbutton_stretchMulti.ClimbRate = 1D;
			this.spinbutton_stretchMulti.Numeric = true;
			this.spinbutton_stretchMulti.Value = 1D;
			this.table1.Add (this.spinbutton_stretchMulti);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1 [this.spinbutton_stretchMulti]));
			w12.TopAttach = ((uint)(1));
			w12.BottomAttach = ((uint)(2));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add (this.table1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(w1 [this.table1]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Internal child MyNesGTK.Dialog_VideoSettings.ActionArea
			global::Gtk.HButtonBox w14 = this.ActionArea;
			w14.Name = "dialog1_ActionArea";
			w14.Spacing = 10;
			w14.BorderWidth = ((uint)(5));
			w14.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w15 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w14 [this.buttonCancel]));
			w15.Expand = false;
			w15.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w16 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w14 [this.buttonOk]));
			w16.Position = 1;
			w16.Expand = false;
			w16.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 438;
			this.DefaultHeight = 334;
			this.Show ();
			this.button107.Activated += new global::System.EventHandler (this.OnButton107Activated);
			this.button107.Pressed += new global::System.EventHandler (this.OnButton107Pressed);
		}
	}
}