using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            OkButton.Text = tb_Name.Text;
            tb_Name.Text = "Noway out";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }
        public void CreateFileOrFolder()
        {


           
    }

        public void bt_add_Click(object sender, EventArgs e)
        {
            // Specify a name for your top-level folder.
            string folderName = @"C:\Users\h122817\Desktop\nay";

            // To create a string that specifies the path to a subfolder under your 
            // top-level folder, add a name for the subfolder to folderName.
            string pathString = System.IO.Path.Combine(folderName, "userdata");

            // You can extend the depth of your path if you want to.
            //pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

            // Create the subfolder. You can verify in File Explorer that you have this
            // structure in the C: drive.
            //    Local Disk (C:)
            //        Top-Level Folder
            //            SubFolder
            System.IO.Directory.CreateDirectory(pathString);

            // Create a file name for the file you want to create. 
            //string fileName = System.IO.Path.GetRandomFileName();

            // This example uses a random string for the name, but you also can specify
            // a particular name.
            string fileName = tb_Name.Text+".txt";

            // Use Combine again to add the file name to the path.
            pathString = System.IO.Path.Combine(pathString, fileName);

            // Verify the path that you have constructed.
            Console.WriteLine("Path to my file: {0}\n", pathString);

            // Check that the file doesn't already exist. If it doesn't exist, create
            // the file and write integers 0 - 99 to it.
            // DANGER: System.IO.File.Create will overwrite the file if it already exists.
            // This could happen even with random file names, although it is unlikely.
            if (!System.IO.File.Exists(pathString))
            {
               // using (System.IO.File fs = System.IO.File.Create(pathString))
                //{

                   // fs.Write(tb_Name.Text,0,0);
                System.IO.File.WriteAllText(pathString, tb_Name.Text+", ");
                System.IO.File.AppendAllText(pathString,tb_ContactNo.Text + ", ");
                System.IO.File.AppendAllText(pathString,tb_Age.Text+ ", ");
                System.IO.File.AppendAllText(pathString,tb_Aim.Text);

               

                //}
            }

            else
            {
                Console.WriteLine("File \"{0}\" already exists.", fileName);
                return;
            }

            // Read and display the data from your file.
            try
            {
                byte[] readBuffer = System.IO.File.ReadAllBytes(pathString);
                foreach (byte b in readBuffer)
                {
                    Console.Write(b + " ");
                }
                Console.WriteLine();
            }
            catch (System.IO.IOException e1)
            {
                Console.WriteLine(e1.Message);
            }

            // Keep the console window open in debug mode.
           // System.Console.WriteLine("Press any key to exit.");
            //System.Console.ReadKey();
        }

        public void bt_delete_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete(@"C:\Users\h122817\Desktop\nay\userdata\nay.txt");
        }
        // Sample output:

        // Path to my file: c:\Top-Level Folder\SubFolder\ttxvauxe.vv0

        //0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29
        //30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56
        // 57 58 59 60 61 62 63 64 65 66 67 68 69 70 71 72 73 74 75 76 77 78 79 80 81 82 8
        //3 84 85 86 87 88 89 90 91 92 93 94 95 96 97 98 99


    }
}
    



