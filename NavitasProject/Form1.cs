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


namespace NavitasProject
{
    public partial class Res : Form
    {
        Point dragPoint = Point.Empty;
        bool dragging = false;
        bool draggingCube = false;
        Point dragPointCube = Point.Empty;





        int J = -1; // Array Pointer which points the value of resPic Array tag
        int j = 0; //Resource Picturebox Array Index
        int q = 0; // Cubicle Picturebox Array Index 
        int Q = -1; // Cubicle dragdrop TAG;
        int S = -1; // Cubicle Resize 
        int P = -1; //Resource Resize
        ResourcePictureBox[] resPic = new ResourcePictureBox[50];

        CustomPictureBox[] cubPic = new CustomPictureBox[50];
        //Textbox to set custom properties to cubicle pixturebox
        TextBox C_Name, CubNo, EmpName, EmpSno, C_State, Other;
        TextBox OtherInfo, ResState,  AssCubNo, AssEmpNo, ResNo, ResType;
        TextBox DateOfIssue;
        //Labels for cubicle;
        Label[] l = new Label[7];
        //Edit menu selection
        Boolean IsCubicleOnEdit = false;
        Boolean IsResourceOnEdit = false;


        System.Windows.Forms.ToolTip tt = new System.Windows.Forms.ToolTip();
        //ToolTip Object

        public Res()
        {
            InitializeComponent();
            BaseMap.SendToBack();
            for (int i = 0; i < 50; i++)
            {
                resPic[i] = new ResourcePictureBox();
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
                    BaseMap.ImageLocation = imageLocation;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK);
            }

        }
        private  void createRes(string ResourceNo,string ResourceType,string AssignedEmpNo,string DateOfIssue,string AssignedCubNo,string ResourceState,string OtherInformation)
        {

            //Mobile Resource creator 
            resPic[j] = new ResourcePictureBox();
            resPic[j].Size = new Size(30, 30);  //I use this picturebox simply to debug and see if I can create a single picturebox, and that way I can tell if something goes wrong with my array of pictureboxes. Thus far however, neither are working.

            if (ResourceType == "computer")
            {
                resPic[j].Image = Properties.Resources.computer;
            }else if(ResourceType == "mobile")
            {
                resPic[j].Image = Properties.Resources.mobile;
            }else if(ResourceType == "keyboard")
            {
                resPic[j].Image = Properties.Resources.keyboard;
            }else if(ResourceType == "printer")
            {
                resPic[j].Image = Properties.Resources._5572;
            }

            resPic[j].DateOfIssue = DateOfIssue;
            resPic[j].AssCubNo = AssignedCubNo;
            resPic[j].AssEmpNo = AssignedEmpNo;
            resPic[j].ResNo = ResourceNo;
            resPic[j].ResType = ResourceType;
            resPic[j].ResState = ResourceState;
            resPic[j].OtherInfo = OtherInformation;
            

            resPic[j].SizeMode = PictureBoxSizeMode.StretchImage;
            resPic[j].BackColor = Color.Transparent;
            //resPic[j].BackgroundImage = Properties.Resources.computer;
            resPic[j].BackgroundImageLayout = ImageLayout.Stretch;
            resPic[j].Location = new Point(0, 0);

            resPic[j].MouseDown += new MouseEventHandler(resPic_MouseDown);

            resPic[j].MouseUp += new MouseEventHandler(resPic_MouseUp);

            resPic[j].MouseMove += new MouseEventHandler(resPic_MouseMove);

            resPic[j].MouseHover += new EventHandler(resPic_MouseHover);

            resPic[j].Tag = j;

            resPic[j].LocationChanged += new EventHandler(resPic_LC);
            //pb.Anchor = AnchorStyles.Left;

            resPic[j].Visible = true;
            //pictureBox1.Controls.Add(resPic[j]);
            ResourcesBay.Controls.Add(resPic[j]);
            j++;
        }

        private void createRes(string ResourceType)
        {

            //Mobile Resource creator 
            resPic[j] = new ResourcePictureBox();
            resPic[j].Size = new Size(30, 30);  //I use this picturebox simply to debug and see if I can create a single picturebox, and that way I can tell if something goes wrong with my array of pictureboxes. Thus far however, neither are working.

            if (ResourceType == "computer")
            {
                resPic[j].Image = Properties.Resources.computer;
            }
            else if (ResourceType == "mobile")
            {
                resPic[j].Image = Properties.Resources.mobile;
            }
            else if (ResourceType == "keyboard")
            {
                resPic[j].Image = Properties.Resources.keyboard;
            }
            else if (ResourceType == "printer")
            {
                resPic[j].Image = Properties.Resources._5572;
            }
            

            resPic[j].SizeMode = PictureBoxSizeMode.StretchImage;
            resPic[j].BackColor = Color.Transparent;
            //resPic[j].BackgroundImage = Properties.Resources.computer;
            resPic[j].BackgroundImageLayout = ImageLayout.Stretch;
            resPic[j].Location = new Point(0, 0);

            resPic[j].MouseDown += new MouseEventHandler(resPic_MouseDown);

            resPic[j].MouseUp += new MouseEventHandler(resPic_MouseUp);

            resPic[j].MouseMove += new MouseEventHandler(resPic_MouseMove);

            resPic[j].MouseHover += new EventHandler(resPic_MouseHover);

            resPic[j].Tag = j;

            resPic[j].LocationChanged += new EventHandler(resPic_LC);
            //pb.Anchor = AnchorStyles.Left;

            resPic[j].Visible = true;
            //pictureBox1.Controls.Add(resPic[j]);
            ResourcesBay.Controls.Add(resPic[j]);
            j++;
        }

        private void addResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String csvLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "CSV files(*.csv)|*.csv";
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    csvLocation = dialog.FileName;
                    string[] Lines = File.ReadAllLines(csvLocation);
                    string[] Fields;
                    Fields = Lines[0].Split(new char[] { ',' });
                    int Cols = Fields.GetLength(0);
                    DataTable dt = new DataTable();

                    //1st row must be column names; force lower case to ensure matching later on.
                    for(int i = 0; i < Cols; i++)
                    {
                        dt.Columns.Add(Fields[i].ToLower(), typeof(string));
                        
                    }
                    DataRow Row;

                    for (int i = 1; i < Lines.GetLength(0); i++)
                    {
                        Fields = Lines[i].Split(new char[] { ',' });
                        Row = dt.NewRow();
                        for (int f = 0; f < Cols; f++)
                            Row[f] = Fields[f];
                        dt.Rows.Add(Row);
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        //string file = row.Field<string>(0);
                        createRes(row.Field<string>(0), row.Field<string>(1), row.Field<string>(2), row.Field<string>(3), row.Field<string>(4), row.Field<string>(5), row.Field<string>(6));
                        

                    }
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
                J = (int)(((PictureBox)sender).Tag);
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
            
            tt.SetToolTip(this.resPic[(int)(((PictureBox)sender).Tag)], "Tag No:" + (int)(((PictureBox)sender).Tag));
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
            createRes("computer");


        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            createRes("printer");
        }


        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            createRes("mobile");

        }



        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            createRes("keyboard");
        }


        private void ClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < j; i++)
            {
                resPic[i].Dispose();

            }
        }

        private void UPArrow_Click(object sender, EventArgs e)
        {

            //cubPic[Q].Size.Height = (int)cubPic[Q].Size.Height + 1;
            Size temp = cubPic[S].Size;
            temp.Height += 10;
            cubPic[S].Size = temp;

        }

        private void DownArrow_Click(object sender, EventArgs e)
        {
            //cubPic[Q].Size.Height -= 1;
            Size temp = cubPic[S].Size;
            temp.Height -= 5;
            cubPic[S].Size = temp;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string inp = SearchBox.Text;

            for(int i = 0; i < j; i++)
            {
                if(inp == (string)resPic[i].ResNo)
                {
                    var obj = resPic[i].Parent;
                    string o = obj.Location.X + "," + obj.Location.Y;
                    obj.Focus();
                    
                    obj.BackColor = Color.LightSkyBlue;
                    
                }
            }
        }

        private void SearchCancelButton_Click(object sender, EventArgs e)
        {
            SearchBox.Text = "";
        }

        private void RightArrow_Click(object sender, EventArgs e)
        {
            //cubPic[Q].Size.Width += 1;
            Size temp = cubPic[S].Size;
            temp.Width += 10;
            cubPic[S].Size = temp;
        }

        private void LeftArrow_Click(object sender, EventArgs e)
        {
            //cubPic[Q].Size.Height += 1;
            Size temp = cubPic[S].Size;
            temp.Width -= 5;
            cubPic[S].Size = temp;
        }

        private void resetSize_Click(object sender, EventArgs e)
        {
            cubPic[S].Size = new Size(100, 100);
        }

        private void UPArrow_MouseDown(object sender, MouseEventArgs e)
        { /*
            Size temp = cubPic[S].Size;
            temp.Height += 2;
            cubPic[S].Size = temp;
            */
                }
                private void resPic_LC(object sender, EventArgs e)
        {
            if (!IsResourceOnEdit && !IsCubicleOnEdit )
            {
                IsResourceOnEdit = true;
                ResetButton.Visible = true;
                SaveButton.Visible = true;
                CancButton.Visible = true;

                P = (int)(((PictureBox)sender).Tag);

                


                l[0] = new Label { Text = "ResNo:", Anchor = AnchorStyles.Left, Tag = "resLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[0], 0, 0);
                ResNo = new TextBox { Dock = DockStyle.Fill, Text = resPic[P].ResNo };
                tableLayoutPanel1.Controls.Add(ResNo, 1, 0);

                

                l[1] = new Label { Text = "Assigned Cubicle No:", Anchor = AnchorStyles.Left, Tag = "resLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[1], 0, 1);
                AssCubNo = new TextBox { Dock = DockStyle.Fill, Text = resPic[P].AssCubNo };
                tableLayoutPanel1.Controls.Add(AssCubNo, 1, 1);

                l[2] = new Label { Text = "Assigned Emp Name:", Anchor = AnchorStyles.Left, Tag = "resLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[2], 0, 2);
                AssEmpNo = new TextBox { AccessibleName = "EmpName", Dock = DockStyle.Fill, Text = resPic[P].AssEmpNo };
                tableLayoutPanel1.Controls.Add(AssEmpNo, 1, 2);

                l[3] = new Label { Text = "DOI:", Anchor = AnchorStyles.Left, Tag = "resLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[3], 0, 3);
                DateOfIssue = new TextBox { Dock = DockStyle.Fill, Text = resPic[P].DateOfIssue };
                tableLayoutPanel1.Controls.Add(DateOfIssue, 1, 3);

                l[4] = new Label { Text = "Resource State :", Anchor = AnchorStyles.Left, Tag = "resLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[4], 0, 4);
                ResState = new TextBox { AccessibleName = "ResState", Dock = DockStyle.Fill, Text = resPic[P].ResState };
                tableLayoutPanel1.Controls.Add(ResState, 1, 4);

                l[5] = new Label { Text = "Res Type:", Anchor = AnchorStyles.Left, Tag = "resLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[5], 0, 5);
                ResType = new TextBox { AccessibleName = "OtherInfo", Dock = DockStyle.Fill, Text = resPic[P].ResType };
                tableLayoutPanel1.Controls.Add(ResType, 1, 5);

                l[6] = new Label { Text = "OtherInfo:", Anchor = AnchorStyles.Left, Tag = "resLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[6], 0, 6);
                OtherInfo = new TextBox { AccessibleName = "OtherInfo", Dock = DockStyle.Fill, Text = resPic[P].OtherInfo };
                tableLayoutPanel1.Controls.Add(OtherInfo, 1, 6);

            }
        }

        private void cubPic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Used to assign properties to the object created 
            if (!IsCubicleOnEdit && !IsResourceOnEdit)
            {
                IsCubicleOnEdit = true;
                ResetButton.Visible = true;
                SaveButton.Visible = true;
                CancButton.Visible = true;

                //ArrowKeys used to resize the cubicle 
                UPArrow.Visible = true;
                LeftArrow.Visible = true;
                resetSize.Visible = true;
                RightArrow.Visible = true;
                DownArrow.Visible = true;
                S = (int)(((PictureBox)sender).Tag);

                
                
                
                

                l[0] = new Label { Text = "C_Name:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[0], 0, 0);
                C_Name = new TextBox { Dock = DockStyle.Fill,Text= cubPic[S].C_Name };
                tableLayoutPanel1.Controls.Add(C_Name, 1, 0);



                l[1] = new Label { Text = "Cubicle No:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[1], 0, 1);
                CubNo = new TextBox { Dock = DockStyle.Fill, Text= cubPic[S].CubNo };
                tableLayoutPanel1.Controls.Add(CubNo, 1, 1);

                l[2] = new Label { Text = "Employee Name:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[2], 0, 2);
                EmpName = new TextBox { AccessibleName = "EmpName", Dock = DockStyle.Fill , Text = cubPic[S].EmpName };
                tableLayoutPanel1.Controls.Add(EmpName, 1, 2);

                l[3] = new Label { Text = "Employee Sno:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[3], 0, 3);
                EmpSno = new TextBox { Dock = DockStyle.Fill,Text= cubPic[S].EmpSno };
                tableLayoutPanel1.Controls.Add(EmpSno, 1, 3);

                l[4] = new Label { Text = "Cubicle State:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[4], 0, 4);
                C_State = new TextBox { AccessibleName = "cState", Dock = DockStyle.Fill,Text = cubPic[S].C_State };
                tableLayoutPanel1.Controls.Add(C_State, 1, 4);

                l[5] = new Label { Text = "Other:", Anchor = AnchorStyles.Left, Tag = "cubicleLabels", AutoSize = true };
                tableLayoutPanel1.Controls.Add(l[5], 0, 5);
                Other = new TextBox { AccessibleName = "Other", Dock = DockStyle.Fill,Text= cubPic[S].Other };
                tableLayoutPanel1.Controls.Add(Other, 1, 5);

                //q is the tag used to assign
                //Q = (int)(((PictureBox)sender).Tag);

            }

        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {  /*
            # The cubicles are created using this method and they are created dynamically using array. the maximum size of the array is declared in the top 
            */



                cubPic[q] = new CustomPictureBox();
            cubPic[q].Size = new Size(100, 100);  //I use this picturebox simply to debug and see if I can create a single picturebox, and that way I can tell if something goes wrong with my array of pictureboxes. Thus far however, neither are working.
            cubPic[q].Dock = DockStyle.Top;
            cubPic[q].SizeMode = PictureBoxSizeMode.Normal;
            cubPic[q].BackColor = Color.LightSkyBlue;
            cubPic[q].BorderStyle = BorderStyle.Fixed3D;
            cubPic[q].Anchor = AnchorStyles.Top;
            cubPic[q].BackgroundImageLayout = ImageLayout.Stretch;
            cubPic[q].Location = new Point(0, 0);

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
            BaseMap.Controls.Add(cubPic[q]);
            q++;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (IsCubicleOnEdit)
            {
                cubPic[S].EmpSno = EmpSno.Text;
                cubPic[S].EmpName = EmpName.Text;
                cubPic[S].CubNo = CubNo.Text;
                cubPic[S].C_Name = C_Name.Text;
                cubPic[S].C_State = C_State.Text;
                cubPic[S].Other = Other.Text;

                MessageBox.Show("Saved");
            }else if (IsResourceOnEdit)
            {
                
                resPic[P].AssEmpNo = AssEmpNo.Text;
                resPic[P].AssCubNo = AssCubNo.Text;
                resPic[P].ResNo = ResNo.Text;
                resPic[P].ResType = ResType.Text;
                resPic[P].ResState = ResState.Text;
                resPic[P].DateOfIssue = DateOfIssue.Text;
                resPic[P].OtherInfo = OtherInfo.Text;


                MessageBox.Show("Saved");

            }
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            if (IsCubicleOnEdit)
            {
                C_Name.Text = "";
                CubNo.Text = "";
                EmpName.Text = "";
                EmpSno.Text = "";
                C_State.Text = "";
                Other.Text = "";
            }else if (IsResourceOnEdit)
            {
                AssEmpNo.Text="";
                AssCubNo.Text = "";
                ResNo.Text = "";
                ResType.Text = "";
                ResState.Text = "";
                DateOfIssue.Text = "";
                OtherInfo.Text = "";
            }
        }

        private void CancButton_Click(object sender, EventArgs e)
        {
            if (IsCubicleOnEdit)
            {
                ResetButton.Visible = false;
                SaveButton.Visible = false;
                CancButton.Visible = false;
                IsCubicleOnEdit = false;


                //ArrowKeys used to resize the cubicle 
                UPArrow.Visible = false;
                LeftArrow.Visible = false;
                resetSize.Visible = false;
                RightArrow.Visible = false;
                DownArrow.Visible = false;

                for (int i = 0; i < 6; i++)
                {
                    l[i].Visible = false;
                    l[i].Dispose();
                }

                C_Name.Dispose();
                CubNo.Dispose();
                EmpName.Dispose();
                EmpSno.Dispose();
                C_State.Dispose();
                Other.Dispose();

                S = -1;
            }else if (IsResourceOnEdit)
            {
                IsResourceOnEdit = false;
                ResetButton.Visible = false;
                SaveButton.Visible = false;
                CancButton.Visible = false;

                for (int i = 0; i < 7; i++)
                {
                    l[i].Visible = false;
                    l[i].Dispose();
                }

                AssEmpNo.Dispose();
                AssCubNo.Dispose();
                ResNo.Dispose();
                ResType.Dispose();
                ResState.Dispose();
                DateOfIssue.Dispose();
                OtherInfo.Dispose();

                P = -1;
            }
            
        }

    }

    

    /// <summary>
    /// Creating CustomPictureBox to add additional properties to the cubicle picturebox which can be accessed later for application
    /// </summary>
    /// 

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
    public class ResourcePictureBox : PictureBox
    {
        public ResourcePictureBox() : base() { }

           
        public string ResNo
        {
            // Res Number which is unique for every resource bought by the company. This id links to company database of all resource bought
            get;
            set;
        }
        public string ResType
        {
            //Type of resource whether. That is wether its a computer or printer or mobile etc
            get;
            set;
        }
        public string AssCubNo
        {
            // Cubicle Number to which particular resource has been assigned
            get;
            set;
        }
        public string AssEmpNo
        {
            // Employee Number to which particular resource has been assigned
            get;
            set;
        }
        public String DateOfIssue
        {   //date of issue 
            get;
            set;
        }
        public string ResState
        {   //State of resource whether it is functional or not
            get;
            set;
        }
        public string OtherInfo
        {  //Anyother notes or information about the resource 
            get;
            set;
        }
}
    
}
