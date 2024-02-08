using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blood_bank.custom
{
    public partial class curvedtextboxcs : UserControl
    {
        private Color borderColor = Color.MediumSlateBlue;
        private int borderSize = 2;
        private bool underlinedStyle = false;
        private int borderRadius = 0;
        public curvedtextboxcs()
        {
            InitializeComponent();
        }

        public Color BorderColor { get => borderColor; set { borderColor = value; this.Invalidate(); } }
        public int BorderSize { get => borderSize; set{ borderSize = value;this.Invalidate();} }
        public bool UnderlinedStyle { get => underlinedStyle; set { underlinedStyle = value;this.Invalidate(); } }

        public int BorderRadius { get => borderRadius; set {  borderRadius = value;this.Invalidate();  } }

        public bool Multiline { get; private set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            if (borderRadius > 1)//rounded
            {
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize=borderSize>0 ? borderSize : 1;

                using (GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, borderRadius)) 
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize)) 
                using (Pen penBorderSmooth = new Pen(this.Parent.BackColor, smoothSize)) 
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(pathBorderSmooth);
                    if (borderRadius > 15) SetTextBoxRoundedRegion();
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;

                    if (underlinedStyle)
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.SmoothingMode = SmoothingMode.None;
                        graph.DrawLine(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                    else
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.DrawPath(penBorder,pathBorder);
                    }
                }

            }
            else//square
            {
                this.Region = new Region(this.ClientRectangle);
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;

                    if (underlinedStyle)
                        graph.DrawLine(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    else
                        graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }
        }

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxt;
            if(Multiline)
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderRadius - borderSize);
                textBox1.Region=new Region(pathTxt);
            }
            else
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderRadius*2);
                textBox1.Region = new Region(pathTxt);
            }
        }

        private GraphicsPath GetFigurePath(Rectangle rect,int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X,rect.Y, curveSize, curveSize,180,90);
            path.AddArc(rect.Right - curveSize, rect.Y,curveSize,curveSize,270,90);
            path.AddArc(rect.Right - curveSize, rect.Bottom-curveSize,curveSize,curveSize,0,90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateControlHeight();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }
        private void UpdateControlHeight()
        {
            if(textBox1.Multiline==false)
            {
                int txtHeight = TextRenderer.MeasureText("text", this.Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize=new Size(0,txtHeight);
                textBox1.Multiline = false;

                this.Height=textBox1.Height+this.Padding.Top+this.Padding.Bottom;
            }
        }
        private void curvedtextboxcs_Load(object sender, EventArgs e)
        {

        }
    }
}
