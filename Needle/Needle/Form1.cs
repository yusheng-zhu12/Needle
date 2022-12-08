using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Needle
{
    public partial class Form1 : Form
    {

        //********************//
        private static Socket ServerSocket;
        private static Socket socketAccept;
        CancellationTokenSource cancellationTokenSource;
        ManualResetEvent manualReset = new ManualResetEvent(true);
        public Socket client1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public Socket client2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public Socket client3 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        public Socket client_receive;
        bool host = true;
        public int point = 1;
        public int your_point = 1;
        public int person_point = 1;
        private bool flag = true;
        //********************//

        int INTERVAL_LAUNCH = 10;
        int INTERVAL_ROTATE = 30;
        Bitmap bitmap;

        float LineWidth;//线宽

        float cx, cy;//圆心
        float offset_y;
        float r;//半径

        float Length;//线长

        float speed_launch = 10;//发射速度
        float speed_rotate = 5;//旋转速度

        Matrix matrix;
        PointF rotate_center;

        GraphicsPath graphicsPath = new GraphicsPath();//在圆上的针

        //CancellationTokenSource cancellationTokenSource;
        //CancellationToken cancellationToken;
        //ManualResetEvent resetEvent = new ManualResetEvent(true);

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void Launch_Click(object sender, EventArgs e)
        {
            //***************//
            byte[] msg = null;
            string strmsg = "lauch";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Broadcast, 5000);
            msg = System.Text.Encoding.UTF8.GetBytes(strmsg);
            client2.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            client2.SendTo(msg, iPEndPoint);//发送数据
            //***************//
            //Task.Run(() => LaunchLine());
        }

        private void LaunchLine()
        {
            Pen penbk = new Pen(Color.White, LineWidth);
            Pen pen1 = new Pen(Color.Black, LineWidth);
            float x1 = pictureBox1.Width / 2;
            float y1 = pictureBox1.Height;
            float x2 = pictureBox1.Width / 2;
            float y2 = pictureBox1.Height - Length;

            try
            {
                while (y2 > cy + r + LineWidth * 5)
                {

                    Thread.Sleep(INTERVAL_LAUNCH);
                    //Bitmap b = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.DrawLine(penbk, x1, y1, x2, y2);
                    y1 -= speed_launch;
                    y2 -= speed_launch;

                    var color = bitmap.GetPixel((int)x2 + 5, (int)(y2 - 5));
                    //var color = b.GetPixel((int)x2 + 5, (int)(y2 - 5));
                    int c1 = color.ToArgb();
                    int c2 = Color.Black.ToArgb();
                    int c3 = Color.White.ToArgb();
                    if (c1 == c2)
                    {
                        timerRotate.Stop();
                        return;
                    }

                    graphics.DrawLine(pen1, x1, y1, x2, y2);
                    //bitmap = b;
                    pictureBox1.Image = bitmap;
                }
                graphicsPath.AddLine(new PointF(x1, y1), new PointF(x2, y2));
                graphicsPath.CloseFigure();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        private void Stop_Click(object sender, EventArgs e)
        {
            timerRotate.Stop();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            //*******************************//
            if(host)
            {
                Start.Enabled = false;
                IPEndPoint iPEndPoint1 = new IPEndPoint(IPAddress.Any, 5000);
                client_receive = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                client_receive.Bind(iPEndPoint1);
                cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;
                manualReset.Set();
                Task.Run(() => Receive(client_receive, cancellationToken), cancellationToken);

                IPEndPoint iPEndPoint2 = new IPEndPoint(IPAddress.Broadcast, 5000);
                client2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                byte[] msg = null;
                string strmsg = "start";
                msg = System.Text.Encoding.UTF8.GetBytes(strmsg);
                client2.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                client2.SendTo(msg, iPEndPoint2);//发送数据
            }
            //*******************************//
        }

        private void timerLaunch_Tick(object sender, EventArgs e)
        {
            ;
        }

        private void timerRotate_Tick(object sender, EventArgs e)
        {
            try
            {
                Pen penbk = new Pen(Color.White, LineWidth);
                Pen pen1 = new Pen(Color.Black, LineWidth);
                //Bitmap b = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawPath(penbk, graphicsPath);
                graphicsPath.Transform(matrix);
                graphics.DrawPath(pen1, graphicsPath);
                //bitmap = b;
                pictureBox1.Image = bitmap;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void init_Click(object sender, EventArgs e)
        {
            //设置 颜色、线
            LineWidth = 8f;
            Length = 50f;
            Pen penbk = new Pen(Color.White, LineWidth);
            Pen pen1 = new Pen(Color.Black, LineWidth);

            timerLaunch.Interval = INTERVAL_LAUNCH;
            timerRotate.Interval = INTERVAL_ROTATE;

            //背景初始化
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);

            //画圆
            r = 40f;
            offset_y = -10f;
            cx = pictureBox1.Width / 2;
            cy = pictureBox1.Height / 2 + offset_y;
            SolidBrush brush = new SolidBrush(Color.Black);
            graphics.FillEllipse(brush, cx - r, cy - r, r * 2, r * 2);

            matrix = new Matrix();
            rotate_center = new PointF(cx, cy);
            matrix.RotateAt(speed_rotate, rotate_center);

            //画线
            PointF pt1 = new PointF(pictureBox1.Width / 2, pictureBox1.Height);
            PointF pt2 = new PointF(pictureBox1.Width / 2, pictureBox1.Height - Length);
            graphics.DrawLine(pen1, pt1, pt2);
            pictureBox1.Image = bitmap;
            //***************//
            if (host)
            {
                cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;
                manualReset.Set();
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 6000);
                client1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                client1.Bind(iPEndPoint);
                Task.Run(() => Receive_initial(client1, cancellationToken), cancellationToken);
            }
            else
            {
                cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = cancellationTokenSource.Token;
                manualReset.Set();
                IPEndPoint iPEndPoint1 = new IPEndPoint(IPAddress.Any, 6000);
                client1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                client1.Bind(iPEndPoint1);
                Task.Run(() => Send_initial(client1));
                IPEndPoint iPEndPoint2 = new IPEndPoint(IPAddress.Any, 5000);
                client2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                client2.Bind(iPEndPoint2);
                Task.Run(() => Receive(client2, cancellationToken), cancellationToken);
                IPEndPoint iPEndPoint3 = new IPEndPoint(IPAddress.Any, 7000);
                client3.Bind(iPEndPoint3);
                Task.Run(() => Receive_initial(client3, cancellationToken), cancellationToken);
            }
            //***************//
        }

        //**********************************//

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Send_initial(Socket socket)
        {
            byte[] msg = null;
            string strmsg = "join";
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Broadcast, 6000);
            msg = System.Text.Encoding.UTF8.GetBytes(strmsg);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
            socket.SendTo(msg, iPEndPoint);//发送数据
        }

        private void Receive_initial(Socket socket, CancellationToken cancellationToken)
        {
            if(host)
            {
                while (true)
                {

                    if (cancellationToken.IsCancellationRequested)//是否任务已被取消
                    {
                        break;
                    }

                    manualReset.WaitOne();//暂停/恢复线程

                    if (cancellationToken.IsCancellationRequested)//是否任务已被取消
                    {
                        break;
                    }

                    // Creates an IPEndPoint to capture the identity of the sending host.
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint senderRemote = (EndPoint)sender;

                    byte[] msg = new byte[1024];//接收数据
                    try
                    {
                        socket.ReceiveTimeout = 500;//设置等待时长
                        socket.ReceiveFrom(msg, ref senderRemote);// This call blocks for 500ms
                        string strmsg = System.Text.Encoding.UTF8.GetString(msg);
                        strmsg = strmsg.Replace("\0", "");
                        if (strmsg == "join")
                        {
                            person_point++;
                            byte[] msg1 = null;
                            byte[] msg2 = null;
                            string strmsg1 = person_point.ToString();
                            string strmsg2 = strmsg1;
                            IPEndPoint iPEndPoint1 = new IPEndPoint(IPAddress.Broadcast, 7000);
                            msg1 = System.Text.Encoding.UTF8.GetBytes(strmsg1);
                            msg2 = System.Text.Encoding.UTF8.GetBytes(strmsg2);
                            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                            socket.SendTo(msg1, iPEndPoint1);//发送数据
                            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, true);
                            socket.SendTo(msg2, iPEndPoint1);//发送数据
                        }
                    }
                    catch (SocketException e1)
                    {
                        //MessageBox.Show(e1.ToString());//超时异常
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        break;
                    }
                }

            }
            else
            {
                while (true)
                {
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 7000);
                    EndPoint senderRemote = (EndPoint)sender;
                    byte[] msg = new byte[1024];//接收数据
                    try
                    {
                        socket.ReceiveTimeout = 500;//设置等待时长
                        socket.ReceiveFrom(msg, ref senderRemote);// This call blocks for 500ms
                        string strmsg1 = System.Text.Encoding.UTF8.GetString(msg);
                        socket.ReceiveFrom(msg, ref senderRemote);// This call blocks for 500ms
                        string strmsg2 = System.Text.Encoding.UTF8.GetString(msg);
                        if (flag)
                        {
                            your_point = int.Parse(strmsg1);
                            flag = false;
                        }
                        person_point = int.Parse(strmsg2);
                        MessageBox.Show("Receive_initial");
                    }
                    catch
                    {

                    }
                }
            }

        }

        private void roomhost_Click(object sender, EventArgs e)
        {
            host = true;
        }

        private void gamer_Click(object sender, EventArgs e)
        {
            host = false;
            Start.Enabled = false;
        }

        private void Receive(Socket socket, CancellationToken cancellationToken)
        {
            while (true)
            {

                if (cancellationToken.IsCancellationRequested)//是否任务已被取消
                {
                    break;
                }

                manualReset.WaitOne();//暂停/恢复线程

                if (cancellationToken.IsCancellationRequested)//是否任务已被取消
                {
                    break;
                }

                // Creates an IPEndPoint to capture the identity of the sending host.
                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 5000);
                EndPoint senderRemote = (EndPoint)sender;

                byte[] msg = new byte[1024];//接收数据
                try
                {
                    socket.ReceiveTimeout = 500;//设置等待时长
                    socket.ReceiveFrom(msg, ref senderRemote);// This call blocks for 500ms
                    string strmsg = System.Text.Encoding.UTF8.GetString(msg);
                    strmsg = strmsg.Replace("\0", "");
                    if (strmsg == "start")
                    {
                        Start.Invoke((MethodInvoker)delegate { timerRotate.Start(); });
                    }
                    else if (strmsg == "lauch")
                    {
                        point++;
                        if (point % person_point == your_point) 
                            Launch.Enabled = true;
                        else
                        {
                            if ((point % person_point == 0) && (your_point == person_point)) 
                                Launch.Enabled = true;
                            else
                                Launch.Enabled = false;
                        }
                        Task.Run(() => LaunchLine());
                    }
                    else if (strmsg == "over")
                    {

                    }
                }
                catch (SocketException e1)
                {
                    //MessageBox.Show(e1.ToString());//超时异常
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    break;
                }
            }

        }

    }
}
