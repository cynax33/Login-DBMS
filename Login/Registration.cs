using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FaceRecognition;

namespace Login
{
    public partial class Registration : Form
    {
        int instance;

        userdbDataContext db = new userdbDataContext();

        public Registration()
        {
            InitializeComponent();
        }

        FaceRec facerec = new FaceRec();

        private void button1_Click(object sender, EventArgs e)
        {
            if(instance<1)
            {
                facerec.openCamera(pictureBox1, pictureBox2);
                instance += 1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            facerec.getPersonName(label4);
            textBox1.Text = label4.Text;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!isEmpty())
            {
                MessageBox.Show("Fields must not be empty!", "Warning");
            }
            else
            {
                string email = textBox5.Text;

                db.sp_register(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
                if(instance == 1)
                {
                    facerec.Save_IMAGE(email);
                }
                MessageBox.Show("Save Success");
            }
        }

        private bool isEmpty()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox7.Text))
            {
                return false;
            }
            else
            {
                return true;
            }               
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            facerec.isTrained = true;
        }
    }
}
