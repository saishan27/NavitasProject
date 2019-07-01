
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