using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceRecognition;
using System.Windows.Forms;

namespace Login
{
    public partial class Login : Form
    {
        userdbDataContext db = new userdbDataContext();

        FaceRec facerec = new FaceRec();

        int instance;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int result = db.sp_login(textBox1.Text, textBox2.Text).Count();

            if(textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Fields must not be empty", "Warning");
            }
            else
            {
                if (result == 0)
                {
                    MessageBox.Show("Invalid username or password", "Log in");
                }
                else
                {
                    if (db.sp_type(textBox1.Text, textBox2.Text) == 0)
                    {
                        MessageBox.Show("Welcome Admin", "Log in");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Welcome Staff", "Log in");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (instance < 1)
            {
                facerec.openCamera(pictureBox1, pictureBox2);
                instance += 1;

                facerec.getPersonName(label3);

                facerec.isTrained = true;
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Registration rform = new Registration();
            this.Hide();
            rform.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label3 != null)
            {
                if (db.sp_faceLogin(label3.Text).Count() == 1)
                {
                    if(db.sp_typeFaceID(label3.Text) == 0)
                    {
                        MessageBox.Show("Welcome Admin", "Log in");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Welcome Staff", "Log in");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Face ID not found in database!", "Error");
                }
            }   
        }
    }
}
