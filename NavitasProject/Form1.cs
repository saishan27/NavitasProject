using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavitasProject
{
    public partial class Form1 : Form
    {
        Point dragPoint = Point.Empty;
        bool dragging = false;
        bool draggingCube = false;
        Point dragPointCube = Point.Empty;

        



        int J = -1; // Array Pointer which points the value of resPic Array tag
        int j = 0;
        int q = 0; // Cubicle Picturebox Array Index 
        int Q = -1; // Cubicle dragdrop TAG;
        PictureBox[] resPic = new PictureBox[50];

        CustomPictureBox[] cubPic = new CustomPictureBox[50];
        //Textbox to set custom properties to cubicle pixturebox
        TextBox C_Name, CubNo, EmpName, EmpSno, C_State, Other;
        //Labels for cubicle;
        Label[] l = new Label[6];
        //Edit menu selection
        Boolean IsCubicleOnEdit = false;


        public Form1()
        {
            InitializeComponent();
            pictureBox1.SendToBack();
            for (int i = 0; i < 50; i++)
            {
                resPic[i] = new PictureBox();
                cubPic[i] = new CustomPictureBox();
                //pictureBox1.Paint += new PaintEventHandler(panelArea_Paint);
            }
            
        }


        private void browseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files()|*.png| All Files(*.*)|*.*";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    pictureBox1.ImageLocation = imageLocation;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK);
            }

        }


        private void resPic_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragPoint = new Point(e.X, e.Y);

            if (sender is PictureBox)
            {
               J=(int)(((PictureBox)sender).Tag);
            }
            Control c = sender as Control;
            c.DoDragDrop(c, DragDropEffects.Move);

        }

        private void resPic_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
                resPic[J].Location = new Point(resPic[J].Location.X + e.X - dragPoint.X, resPic[J].Location.Y + e.Y - dragPoint.Y);
        }

        private void resPic_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            J = -1;
        }

        private void resPic_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip tt = new System.Windows.Forms.ToolTip();
            
            tt.SetToolTip(this.resPic[(int)(((PictureBox)sender).Tag)], "Tag No:"+(int)(((PictureBox)sender).Tag));
        }

        private void cubPic_MouseDown(object sender, MouseEventArgs e)
        {
            draggingCube = true;
            dragPointCube = new Point(e.X, e.Y);

            if (sender is PictureBox)
            {
                Q = (int)(((PictureBox)sender).Tag);
            }

        }

        private void cubPic_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingCube)
                cubPic[Q].Location = new Point(cubPic[Q].Location.X + e.X - dragPointCube.X, cubPic[Q].Location.Y + e.Y - dragPointCube.Y);
        }

        private void cubPic_MouseUp(object sender, MouseEventArgs e)
        {
            draggingCube = false;
            Q = -1;
        }

        private void cubPic_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip tt = new System.Windows.Forms.ToolTip();

            tt.SetToolTip(this.cubPic[(int)(((PictureBox)sender).Tag)], "Cubicle Tag No:" + (int)(((PictureBox)sender).Tag));
        }

        private void cubPic_DragDrop(object sender, DragEventArgs e)
        {
            Control c = e.Data.GetData(e.Data.GetFormats()[0]) as Control;
            if (c != null)  
            {
                if (sender is PictureBox)
                {
                    Q = (int)(((PictureBox)sender).Tag);
                }
                c.Location = this.cubPic[Q].PointToClient(new Point(e.X, e.Y));  
                this.cubPic[Q].Controls.Add(c);
                dragging = false;
            } 
        }

        private void cubPic_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Mobile Resource creator 
            resPic[j] = new PictureBox();
            resPic[j].Size = new Size(30, 30);  //I use this picturebox simply to debug and see if I can create a single picturebox, and that way I can tell if something goes wrong with my array of pictureboxes. Thus far however, neither are working.
            resPic[j].Image = Properties.Resources.computer;
            resPic[j].SizeMode = PictureBoxSizeMode.StretchImage;
            resPic[j].BackColor = Color.Transparent;
            //resPic[j].BackgroundImage = Properties.Resources.computer;
            resPic[j].BackgroundImageLayout = ImageLayout.Stretch;
            resPic[j].Location = new Point(0, 0 + (60 * j));

            resPic[j].MouseDown += new MouseEventHandler(resPic_MouseDown);

            resPic[j].MouseUp += new MouseEventHandler(resPic_MouseUp);

            resPic[j].MouseMove += new MouseEventHandler(resPic_MouseMove);

            resPic[j].MouseHover += new EventHandler(resPic_MouseHover);

            resPic[j].Tag = j;

            //pb.Anchor = AnchorStyles.Left;

            resPic[j].Visible = true;
            pictureBox1.Controls.Add(resPic[j]);
            j++;

            
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            resPic[j] = new PictureBox();
            resPic[j].Size = new Size(30, 30);  //I use this picturebox simply to debug and see if I can create a single picturebox, and that way I can tell if something goes wrong with my array of pictureboxes. Thus far however, neither are working.
            resPic[j].BackColor = Color.Transparent;                                    //resPic[j].Image = Properties.Resources._5572;//Printer jpg file
            resPic[j].Image = Properties.Resources._5572;
            resPic[j].SizeMode = PictureBoxSizeMode.StretchImage;
            resPic[j].BackgroundImageLayout = ImageLayout.Stretch;
            resPic[j].Location = new Point(0, 0 + (60 * j));

            resPic[j].MouseDown += new MouseEventHandler(resPic_MouseDown);

            resPic[j].MouseUp += new MouseEventHandler(resPic_MouseUp);

            resPic[j].MouseMove += new MouseEventHandler(resPic_MouseMove);

            resPic[j].MouseHover += new EventHandler(resPic_MouseHover);



            resPic[j].Tag = j;

            //pb.Anchor = AnchorStyles.Left;

            resPic[j].Visible = true;
            pictureBox1.Controls.Add(resPic[j]);
            j++;
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Mobile Resource creator 
            resPic[j] = new PictureBox();
            resPic[j].Size = new Size(30, 30);  //I use this picturebox simply to debug and see if I can create a single picturebox, and that way I can tell if something goes wrong with my array of pictureboxes. Thus far however, neither are working.
            resPic[j].BackColor = Color.Transparent;
            resPic[j].Image = Properties.Resources.mobile;
            resPic[j].SizeMode = PictureBoxSizeMode.StretchImage;
            resPic[j].BackgroundImageLayout = ImageLayout.Stretch;
            resPic[j].Location = new Point(0, 0 + (60 * j));

            resPic[j].MouseDown += new MouseEventHandler(resPic_MouseDown);

            resPic[j].MouseUp += new MouseEventHandler(resPic_MouseUp);

            resPic[j].MouseMove += new MouseEventHandler(resPic_MouseMove);
            
            resPic[j].MouseHover += new EventHandler(resPic_MouseHover);

            resPic[j].Tag = j;

            //pb.Anchor = AnchorStyles.Left;

            resPic[j].Visible = true;
            pictureBox1.Controls.Add(resPic[j]);
            j++;

        }

      

        private void toolStripButton4_Click(object sender, EventArgs e)
        {   

            resPic[j] = new PictureBox();
            resPic[j].Size = new Size(30, 30);  //I use this picturebox simply to debug and see if I can create a single picturebox, and that way I can tell if something goes wrong with my array of pictureboxes. Thus far however, neither are working.
            resPic[j].Image = Properties.Resources.keyboard;//keyboad jpg file
            resPic[j].BackColor = Color.Transparent;
            resPic[j].SizeMode = PictureBoxSizeMode.StretchImage;
            resPic[j].BackgroundImageLayout = ImageLayout.Stretch;
            resPic[j].Location = new Point(0, 0 + (60 * j));

            resPic[j].MouseDown += new MouseEventHandler(resPic_MouseDown);

            resPic[j].MouseUp += new MouseEventHandler(resPic_MouseUp);

            resPic[j].MouseMove += new MouseEventHandler(resPic_MouseMove);

            resPic[j].MouseHover += new EventHandler(resPic_MouseHover);

            resPic[j].Tag = j;

            //pb.Anchor = AnchorStyles.Left;

            resPic[j].Visible = true;
            pictureBox1.Controls.Add(resPic[j]);
            j++;
        }

       
        private void ClearAll_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < j; i++)
            {
                resPic[i].Dispose();

            }
            j = 0;
        }
        private void cubPic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Used to assign properties to the object created 
            if (!IsCubicleOnEdit)
            {
                IsCubicleOnEdit = true;
                ResetButton.Visible = true;
                SaveButton.Visible = true;
                CancelButton.Visible = true;

                //ArrowKeys used to resize the cubicle 
                UPArrow.Visible = true;
                LeftArrow.Visible = true;
                resetSize.Visible = true;
                RightArrow.Visible = true;
                DownArrow.Visible = true;



                l[0] = new Label { Text = "C_Name:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[0], 0, 0);
                C_Name = new TextBox { Dock = DockStyle.Fill };
                tableLayoutPanel1.Controls.Add(C_Name, 1, 0);



                l[1] = new Label { Text = "Cubicle No:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[1], 0, 1);
                CubNo = new TextBox { Dock = DockStyle.Fill };
                tableLayoutPanel1.Controls.Add(CubNo, 1, 1);

                l[2] = new Label { Text = "Employee Name:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[2], 0, 2);
                EmpName = new TextBox { AccessibleName = "EmpName", Dock = DockStyle.Fill };
                tableLayoutPanel1.Controls.Add(EmpName, 1, 2);

                l[3] = new Label { Text = "Employee Sno:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[3], 0, 3);
                EmpSno = new TextBox { Dock = DockStyle.Fill };
                tableLayoutPanel1.Controls.Add(EmpSno, 1, 3);

                l[4] = new Label { Text = "Cubicle State:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[4], 0, 4);
                C_State = new TextBox { AccessibleName = "cState", Dock = DockStyle.Fill };
                tableLayoutPanel1.Controls.Add(C_State, 1, 4);

                l[5] = new Label { Text = "Other:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[5], 0, 5);
                Other = new TextBox { AccessibleName = "Other", Dock = DockStyle.Fill };
                tableLayoutPanel1.Controls.Add(Other, 1, 5);

                //q is the tag used to assign
                Q = (int)(((PictureBox)sender).Tag);

            }
            
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {  /*
            # The cubicles are created using this method and they are created dynamically using array. the maximum size of the array is declared in the top 
            */


            
            cubPic[q] = new CustomPictureBox();
            cubPic[q].Size = new Size(100,100);  //I use this picturebox simply to debug and see if I can create a single picturebox, and that way I can tell if something goes wrong with my array of pictureboxes. Thus far however, neither are working.
            cubPic[q].Dock = DockStyle.Fill;
            cubPic[q].SizeMode = PictureBoxSizeMode.Normal;
            cubPic[q].BackColor = Color.LightSkyBlue;
            cubPic[q].BorderStyle = BorderStyle.Fixed3D;
            //resPic[q].BackgroundImage = Properties.Resources.computer;
            cubPic[q].BackgroundImageLayout = ImageLayout.Stretch;
            cubPic[q].Location = new Point(0, 0 + (60 * j));

            cubPic[q].MouseDown += new MouseEventHandler(cubPic_MouseDown);
            cubPic[q].MouseUp += new MouseEventHandler(cubPic_MouseUp);
            cubPic[q].MouseMove += new MouseEventHandler(cubPic_MouseMove);
            cubPic[q].MouseHover += new EventHandler(cubPic_MouseHover);
            cubPic[q].MouseDoubleClick += new MouseEventHandler(cubPic_MouseDoubleClick);

            cubPic[q].DragOver += new DragEventHandler(cubPic_DragOver);
            cubPic[q].DragDrop += new DragEventHandler(cubPic_DragDrop);

            //

            cubPic[q].Tag = q;
            this.cubPic[q].AllowDrop = true;

            cubPic[q].Anchor = AnchorStyles.Top;
            cubPic[q].SendToBack();
            cubPic[q].Visible = true;
            pictureBox1.Controls.Add(cubPic[q]);
            q++;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            cubPic[Q].EmpSno = EmpSno.Text;
            MessageBox.Show(cubPic[Q].EmpSno);
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            C_Name.Text = "";
            CubNo.Text = "";
            EmpName.Text = "";
            EmpSno.Text = "";
            C_State.Text = "";
            Other.Text = "";
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ResetButton.Visible = false;
            SaveButton.Visible = false;
            CancelButton.Visible = false;
                IsCubicleOnEdit = false;
           

            //ArrowKeys used to resize the cubicle 
            UPArrow.Visible = false;
            LeftArrow.Visible = false;
            resetSize.Visible = false;
            RightArrow.Visible = false;
            DownArrow.Visible = false;

            for(int i = 0; i < 6; i++)
            {
                l[i].Visible=false;
                l[i].Dispose();
            }

            C_Name.Dispose();
            CubNo.Dispose();
            EmpName.Dispose();
            EmpSno.Dispose();
            C_State.Dispose();
            Other.Dispose();

            Q = -1;



        }

    }




    /// <summary>
    /// Creating CustomPictureBox to add additional properties to the cubicle picturebox which can be accessed later for application
    /// </summary>
    public class CustomPictureBox : PictureBox
    {
        public CustomPictureBox() : base()
        { }

        public string CubNo
        {
            get;
            set;
        }


        public string EmpSno
        {
            get;
            set;
        }


        public string EmpName
        {
            get;
            set;
        }


        public string C_Name
        {
            get;
            set;
        }


        public string C_State
        {
            get;
            set;
        }

        public string Other
        {
            get;
            set;
        }
    }
}
