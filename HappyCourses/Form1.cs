using System;
using System.Windows.Forms;
using MouseManipulator;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Drawing.Imaging;
using IniFile;

namespace HappyCourses
{
    public partial class Form1 : Form
    {
        private Rectangle pattern_location = Rectangle.Empty;
        private Timer timer_check = null;
        ScheduledTask task = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialization();
        }

        private void Initialization()
        {
            Parameter._screen = Screen.FromControl(this);
            this.Location = new Point(Parameter._screen.WorkingArea.Width - this.Width,
                                      Parameter._screen.WorkingArea.Height - this.Height);
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;

            InitialDirectory();
            InitialPatternImage();
            InitialSetting();
            InitialTimer();
        }

        private void InitialDirectory()
        {
            Directory.CreateDirectory(Parameter._screenshotPath);
            Directory.CreateDirectory(Parameter._gifPath);
            Directory.CreateDirectory(Parameter._patternPath);
        }

        private void InitialSetting()
        {
            if (!File.Exists(Parameter._iniPath))
            {
                CreateDefaultIniFile();
            }
            else
            {
                Parameter._timer_check_interval_sec = Convert.ToInt32(IniHelper.ReadValue(Parameter._iniSectionSetting, Parameter._iniKeyTimeInterval, Parameter._iniPath));
                Parameter._location_Click1_X = Convert.ToInt32(IniHelper.ReadValue(Parameter._iniSectionClick1, Parameter._iniKeyTimeX, Parameter._iniPath));
                Parameter._location_Click1_Y = Convert.ToInt32(IniHelper.ReadValue(Parameter._iniSectionClick1, Parameter._iniKeyTimeY, Parameter._iniPath));
                Parameter._location_Click2_X = Convert.ToInt32(IniHelper.ReadValue(Parameter._iniSectionClick2, Parameter._iniKeyTimeX, Parameter._iniPath));
                Parameter._location_Click2_Y = Convert.ToInt32(IniHelper.ReadValue(Parameter._iniSectionClick2, Parameter._iniKeyTimeY, Parameter._iniPath));
            }
    }

        private void InitialTimer()
        {
            Timer timer_check = new Timer();
            timer_check.Interval = Parameter._timer_check_interval_sec * 1000;
            timer_check.Tick += new EventHandler(Timer_Tick);  
            timer_check.Enabled = true;
        }

        private void CreateDefaultIniFile()
        {
            bool result = true;
            result = IniHelper.WriteValue(Parameter._iniSectionSetting, Parameter._iniKeyTimeInterval, Parameter._timer_check_interval_sec.ToString(), Parameter._iniPath);
            result = IniHelper.WriteValue(Parameter._iniSectionClick1, Parameter._iniKeyTimeX, Parameter._location_Click1_X.ToString(), Parameter._iniPath);
            result = IniHelper.WriteValue(Parameter._iniSectionClick1, Parameter._iniKeyTimeY, Parameter._location_Click1_Y.ToString(), Parameter._iniPath);
            result = IniHelper.WriteValue(Parameter._iniSectionClick2, Parameter._iniKeyTimeX, Parameter._location_Click2_X.ToString(), Parameter._iniPath);
            result = IniHelper.WriteValue(Parameter._iniSectionClick2, Parameter._iniKeyTimeY, Parameter._location_Click2_Y.ToString(), Parameter._iniPath);
        }

        private void InitialPatternImage()
        {
            if (!File.Exists(Parameter._patternImagePath))
            {
                var bytes = Convert.FromBase64String(Parameter._patternImageInBase64);
                using (var imageFile = new FileStream(Parameter._patternImagePath, FileMode.Create))
                {
                    imageFile.Write(bytes, 0, bytes.Length);
                    imageFile.Flush();
                }
            }
        }

        private void Timer_Tick(object Sender, EventArgs e)
        {
            Operation();
        }

        private void Operation()
        {
            ScreenCapture sc = new ScreenCapture();
            sc.CaptureScreenToFile(Parameter._screenshotImagePath, ImageFormat.Png);

            Search4Pattern();

            if (pattern_location.Width != 0)
            {
                Cursor.Position = new Point(Parameter._location_Click1_X + pattern_location.X,
                                            Parameter._location_Click1_Y + +pattern_location.Y);
                VirtualMouse.LeftClick();

                System.Threading.Thread.Sleep(2000);

                Cursor.Position = new Point(Parameter._location_Click2_X + pattern_location.X,
                                            Parameter._location_Click2_Y + +pattern_location.Y);
                VirtualMouse.LeftClick();
            }

            try
            {
                File.Delete(Parameter._screenshotImagePath);
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            //if (CaptureScreen.CaptureFullScreen())
            //{

            //}
            //else
            //{
            //    MessageBox.Show("Capture screen failed.");
            //}
        }

        private void Search4Pattern()
        {
            Bitmap pattern = new Bitmap(Parameter._patternImagePath);
            Bitmap fullscreen = new Bitmap(Parameter._screenshotImagePath);
            pattern_location = BitmapOperation.SearchBitmap(pattern, fullscreen, 0.1); //tolerance:0~1

            pattern.Dispose();
            pattern = null;

            fullscreen.Dispose();
            fullscreen = null;
        }

        private void button_set_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker.Value.Date + dateTimePicker.Value.TimeOfDay;
            DateTime dateNow = DateTime.Now.AddMinutes(10);

            if (date <= dateNow)
            {
                MessageBox.Show("Time cannot be set!");
                return;
            }

            label_shutdown_info.Text = date.ToString("yyyy-MM-dd HH:mm");
            task = new ScheduledTask(date, ShutDown);
            task.ArrangeTask();
        }

        private void ShutDown()
        {
            var psi = new ProcessStartInfo("shutdown", "/s /t 10");
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            Process.Start(psi);
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            label_shutdown_info.Text = "not set";
            if (task != null)
                task.CancelTask();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //stop timer
            if (timer_check != null)
                timer_check.Stop();

            //prevent pc shutdown
            if (task != null)
                task.CancelTask();

            //delete screenshot
            try
            {
                File.Delete(Parameter._screenshotImagePath);
            }
            catch (IOException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            ChangePicture(pictureBox1);
        }

        private void ChangePicture(PictureBox pb)
        {
            int fCount = Directory.GetFiles(Parameter._gifPath, "*", SearchOption.AllDirectories).Length;
            Random random = new Random();
            int random_int = random.Next(0, fCount + 1);

            if (random_int == 0)
            {
                //pb.Image = Properties.Resources.reading;
                pb.Image = null;
            }
            else
            {
                string filename = String.Format("reading{0:D2}.gif", random_int);
                string filePath = Path.Combine(Parameter._gifPath, filename);
                if (File.Exists(filePath))
                {
                    pb.ImageLocation = filePath;
                }
            }
            
        }

        private void button_test_Click(object sender, EventArgs e)
        {
            Operation();
        }

    }
}
