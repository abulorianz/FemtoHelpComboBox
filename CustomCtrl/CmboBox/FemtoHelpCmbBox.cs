using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

namespace FemtoHelpComboBox.CmboBox
{
    [DefaultEvent("OnSelectedIndexChanged")]
    public partial class FemtoHelpCmbBox : UserControl
    {
       
        //Fields
        private Color backColor = Color.WhiteSmoke;
        private Color iconColor = Color.MediumSlateBlue;
        private Color listBackColor = Color.FromArgb(230, 228, 245);
        private Color listTextColor = Color.DimGray;
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 1;
        //private int borderRadius = 0;

        //Items
        private ComboBox cmbList;
        private TextBox lblText;
        private Button btnIcon;
        //Events
        public event EventHandler OnSelectedIndexChanged;//Default event

        //Constructor
        public FemtoHelpCmbBox()
        {
            InitializeComponent();
            cmbList = new ComboBox();
            lblText = new TextBox();
            btnIcon = new Button();
            this.SuspendLayout();

            //ComboBox: Dropdown list
            cmbList.BackColor = listBackColor;
            cmbList.Font = new Font(this.Font.Name, 8F);
            cmbList.ForeColor = listTextColor;
            cmbList.FlatStyle = FlatStyle.Flat;
            cmbList.Dock = DockStyle.Fill;
            cmbList.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);//Default event
            cmbList.TextChanged += new EventHandler(ComboBox_TextChanged);//Refresh text


            //Button: Icon
            btnIcon.Dock = DockStyle.Right;
            btnIcon.FlatStyle = FlatStyle.Flat;
            btnIcon.FlatAppearance.BorderSize = 0;
            btnIcon.BackColor = backColor;
            btnIcon.Size = new Size(20, 20);
            btnIcon.Cursor = Cursors.Hand;
            btnIcon.Click += new EventHandler(Icon_Click);//Open dropdown list
            btnIcon.Paint += new PaintEventHandler(Icon_Paint);//Draw icon

            //Label: Text
            lblText.Dock = DockStyle.Fill;
            lblText.BorderStyle = BorderStyle.None;
            lblText.AutoSize = false;
            lblText.BackColor = backColor;
            lblText.TextAlign = HorizontalAlignment.Center;
            lblText.Padding = new Padding(8, 0, 0, 0);
            lblText.Font = new Font(this.Font.Name, 12F);
            lblText.Width -= borderSize;
            lblText.Height -= borderSize;
            //->Attach label events to user control event
            lblText.Click += new EventHandler(Surface_Click);//Select combo box
            lblText.MouseEnter += new EventHandler(Surface_MouseEnter);
            lblText.MouseLeave += new EventHandler(Surface_MouseLeave);

            //User Control
            this.Controls.Add(lblText);//2
            this.Controls.Add(btnIcon);//1
            this.Controls.Add(cmbList);//0
            this.MinimumSize = new Size(150, 22);
            this.Size = new Size(150, 20);
            this.ForeColor = Color.DimGray;
            this.Padding = new Padding(borderSize);//Border Size
            this.Font = new Font(this.Font.Name, 10F);
            base.BackColor = borderColor; //Border Color
            this.ResumeLayout();
            AdjustComboBoxDimensions();
        }
         
         //Private methods
         private void AdjustComboBoxDimensions()
         {
             cmbList.Width = lblText.Width;
             cmbList.Location = new Point()
             {
                 X = this.Width - this.Padding.Right - cmbList.Width,
                 Y = lblText.Bottom - cmbList.Height
             };
         }
         //Event methods

         //-> Default event
         private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
         {
             if (OnSelectedIndexChanged != null)
                 OnSelectedIndexChanged.Invoke(sender, e);
             //Refresh text
             lblText.Text = cmbList.Text;
         }
         //-> Items actions
         private void Icon_Click(object sender, EventArgs e)
         {
             //Open dropdown list
             cmbList.Select();
             cmbList.DroppedDown = true;
         }
         private void Surface_Click(object sender, EventArgs e)
         {
             //Attach label click to user control click
             this.OnClick(e);
             //Select combo box
             cmbList.Select();
             if (cmbList.DropDownStyle == ComboBoxStyle.DropDownList)
                 cmbList.DroppedDown = true;//Open dropdown list
         }
         private void ComboBox_TextChanged(object sender, EventArgs e)
         {
             //Refresh text
             lblText.Text = cmbList.Text;
         }

         //-> Draw icon
         private void Icon_Paint(object sender, PaintEventArgs e)
         {
             //Fields
             int iconWidht = 14;
             int iconHeight = 6;
             var rectIcon = new Rectangle((btnIcon.Width - iconWidht) / 2, (btnIcon.Height - iconHeight) / 2, iconWidht, iconHeight);
             Graphics graph = e.Graphics;

             //Draw arrow down icon
             using (GraphicsPath path = new GraphicsPath())
             using (Pen pen = new Pen(iconColor, 2))
             {
                 graph.SmoothingMode = SmoothingMode.AntiAlias;
                 path.AddLine(rectIcon.X, rectIcon.Y, rectIcon.X + (iconWidht / 2), rectIcon.Bottom);
                 path.AddLine(rectIcon.X + (iconWidht / 2), rectIcon.Bottom, rectIcon.Right, rectIcon.Y);
                 graph.DrawPath(pen, path);
             }
         }
        //Properties
        //-> Appearance

        [Category("FemtoHelp")]
        public new Color BackColor
         {
             get { return backColor; }
             set
             {
                 backColor = value;
                 lblText.BackColor = backColor;
                 btnIcon.BackColor = backColor;
             }
         }

        [Category("FemtoHelp")]
        public Color IconColor
         {
             get { return iconColor; }
             set
             {
                 iconColor = value;
                 btnIcon.Invalidate();//Redraw icon
             }
         }

        [Category("FemtoHelp")]
        public Color ListBackColor
         {
             get { return listBackColor; }
             set
             {
                 listBackColor = value;
                 cmbList.BackColor = listBackColor;
             }
         }

        [Category("FemtoHelp")]
        public Color ListTextColor
         {
             get { return listTextColor; }
             set
             {
                 listTextColor = value;
                 cmbList.ForeColor = listTextColor;
             }
         }

        [Category("FemtoHelp")]
        public Color BorderColor
         {
             get { return borderColor; }
             set
             {
                 borderColor = value;
                 base.BackColor = borderColor; //Border Color
             }
         }

        [Category("FemtoHelp")]
        public int BorderSize
         {
             get { return borderSize; }
             set
             {
                 borderSize = value;
                 this.Padding = new Padding(borderSize);//Border Size
                 AdjustComboBoxDimensions();
             }
         }

        [Category("FemtoHelp")]
        public override Color ForeColor
         {
             get { return base.ForeColor; }
             set
             {
                 base.ForeColor = value;
                 lblText.ForeColor = value;
             }
         }

        [Category("FemtoHelp")]
        public override Font Font
         {
             get { return base.Font; }
             set
             {
                 base.Font = value;
                 lblText.Font = value;
                 cmbList.Font = value;//Optional
             }
         }

        [Category("FemtoHelp")]
        public string Texts
         {
             get { return lblText.Text; }
             set { lblText.Text = value; }
         }

         [Category("FemtoHelp")]
         public ComboBoxStyle DropDownStyle
         {
             get { return cmbList.DropDownStyle; }
             set
             {
                 if (cmbList.DropDownStyle != ComboBoxStyle.Simple)
                     cmbList.DropDownStyle = value;
             }
         }
        /*[Category("FemtoHelpe")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    this.Invalidate();//Redraw control
                }
            }
        }*/


        //Properties
        //-> Data
        [Category("FemtoHelp - Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        [MergableProperty(false)]
        public ComboBox.ObjectCollection Items
        {
            get { return cmbList.Items; }
        }
        [Category("FemtoHelp - Data")]
        [AttributeProvider(typeof(IListSource))]
        [DefaultValue(null)]
        public object DataSource
        {
            get { return cmbList.DataSource; }
            set { cmbList.DataSource = value; }
        }

        [Category("FemtoHelp - Data")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(true)]
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get { return cmbList.AutoCompleteCustomSource; }
            set { cmbList.AutoCompleteCustomSource = value; }
        }
        [Category("FemtoHelp - Data")]
        [Browsable(true)]
        [DefaultValue(AutoCompleteSource.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteSource AutoCompleteSource
        {
            get { return cmbList.AutoCompleteSource; }
            set { cmbList.AutoCompleteSource = value; }
        }

        [Category("FemtoHelp - Data")]
        [Browsable(true)]
        [DefaultValue(AutoCompleteMode.None)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public AutoCompleteMode AutoCompleteMode
        {
            get { return cmbList.AutoCompleteMode; }
            set { cmbList.AutoCompleteMode = value; }
        }

        [Category("FemtoHelp - Data")]
        [Bindable(true)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get { return cmbList.SelectedItem; }
            set { cmbList.SelectedItem = value; }
        }

        [Category("FemtoHelp - Data")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get { return cmbList.SelectedIndex; }
            set { cmbList.SelectedIndex = value; }
        }

        [Category("FemtoHelp - Data")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string DisplayMember
        {
            get { return cmbList.DisplayMember; }
            set { cmbList.DisplayMember = value; }
        }

        [Category("FemtoHelp - Data")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ValueMember
        {
            get { return cmbList.ValueMember; }
            set { cmbList.ValueMember = value; }
        }
        //->Attach label events to user control event
        private void Surface_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void Surface_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }
        private void Surface_Enter(object sender, EventArgs e)
        {
            this.OnEnter(e);
        }
        private void Surface_KeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }
        private void Surface_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }
        private void Surface_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }
        private void Surface_Leave(object sender, EventArgs e)
        {
            this.OnLeave(e);
        }
        private void Surface_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }
        private void Surface_MouseHover(object sender, EventArgs e)
        {
            this.OnMouseHover(e);
        }
        private void Surface_MouseMove(object sender, MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        private void Surface_MouseUp(object sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }
        private void Surface_Scroll(object sender, ScrollEventArgs e)
        {
            this.OnScroll(e);
        }
        private void Surface_MouseClick(object sender, MouseEventArgs e)
        {
            this.OnMouseClick(e);
        }

        //Surface
        //::::+
        //Overridden methods
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AdjustComboBoxDimensions();
        }
    }
}




