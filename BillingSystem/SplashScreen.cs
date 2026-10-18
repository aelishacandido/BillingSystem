using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BillingSystem.Utils;

namespace BillingSystem
{
    public partial class SplashScreen : Form
    {
        private const int TotalMs = 2500;
        private readonly System.Windows.Forms.Timer animTimer =
            new System.Windows.Forms.Timer { Interval = 25 };
        private DateTime _start;
        private float _progress = 0f;

        private readonly string[] _messages =
        {
            "Initializing system...",
            "Connecting to database...",
            "Loading modules...",
            "Ready!"
        };

        public SplashScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;   // removes the title bar
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(500, 300);     // drawing area is exactly 500 x 300
            this.DoubleBuffered = true;   // removes flicker
            this.Opacity = 0;             // start invisible, fade in
        }

        // Returns the preferred font if installed, otherwise Segoe UI
        private static Font MakeFont(string name, float size, FontStyle style = FontStyle.Regular)
        {
            using (var test = new Font(name, size, style))
            {
                if (string.Equals(test.Name, name, StringComparison.OrdinalIgnoreCase))
                    return new Font(name, size, style);
            }
            return new Font("Segoe UI", size, style);
        }
        private void SplashScreen_Load(object sender, EventArgs e)
        {
            this.BackColor = AppTheme.PrimaryColor;

            // Title
            lblAppName.AutoSize = false;
            lblAppName.BackColor = Color.Transparent;
            lblAppName.ForeColor = Color.White;
            lblAppName.Font = MakeFont("Bahnschrift", 26f, FontStyle.Bold);
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;
            lblAppName.Text = "BILLING SYSTEM";
            lblAppName.SetBounds(0, 105, 500, 50);

            // Tagline
            lblTagline.AutoSize = false;
            lblTagline.BackColor = Color.Transparent;
            lblTagline.ForeColor = AppTheme.AccentColor;
            lblTagline.Font = MakeFont("Segoe UI Light", 12f);
            lblTagline.TextAlign = ContentAlignment.MiddleCenter;
            lblTagline.Text = "Electric Billing Management System";
            lblTagline.SetBounds(0, 155, 500, 28);

            // Loading text
            lblLoading.AutoSize = false;
            lblLoading.BackColor = Color.Transparent;
            lblLoading.ForeColor = AppTheme.AccentColor;
            lblLoading.Font = MakeFont("Segoe UI Semilight", 9.5f, FontStyle.Italic);
            lblLoading.TextAlign = ContentAlignment.MiddleCenter;
            lblLoading.Text = _messages[0];
            lblLoading.SetBounds(0, 235, 500, 22);

            // Start animation + the 2.5 second splash timer
            _start = DateTime.Now;
            animTimer.Tick += AnimTimer_Tick;
            animTimer.Start();
            splashTimer.Start();
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            double elapsed = (DateTime.Now - _start).TotalMilliseconds;

            // Progress with ease-out (fast start, gentle finish)
            double t = Math.Min(1.0, elapsed / TotalMs);
            _progress = (float)(1 - Math.Pow(1 - t, 2));

            // Status message changes as progress grows
            int idx = _progress < 0.35f ? 0 : _progress < 0.65f ? 1 : _progress < 0.95f ? 2 : 3;
            if (lblLoading.Text != _messages[idx])
                lblLoading.Text = _messages[idx];

            // Fade in (first 400 ms) and fade out (last 300 ms)
            if (elapsed < 400)
                this.Opacity = elapsed / 400.0;
            else if (elapsed > TotalMs - 300)
                this.Opacity = Math.Max(0, (TotalMs - elapsed) / 300.0);
            else
                this.Opacity = 1;

            this.Invalidate();   // repaint progress bar
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = this.ClientRectangle;

            // Gradient background
            using (var bg = new LinearGradientBrush(rect,
                   AppTheme.PrimaryColor, AppTheme.SecondaryColor, 45f))
                g.FillRectangle(bg, rect);

            // Soft decorative circles
            using (var soft = new SolidBrush(Color.FromArgb(25, Color.White)))
            {
                g.FillEllipse(soft, -60, -80, 220, 220);
                g.FillEllipse(soft, 360, 170, 220, 220);
            }

            // Glow behind the logo
            using (var glow = new SolidBrush(Color.FromArgb(50, AppTheme.AccentColor)))
                g.FillEllipse(glow, 210, 20, 80, 80);

            // Lightning bolt logo
            PointF[] bolt =
            {
                new PointF(258, 28), new PointF(230, 66), new PointF(247, 66),
                new PointF(240, 94), new PointF(272, 52), new PointF(254, 52)
            };
            using (var white = new SolidBrush(Color.White))
                g.FillPolygon(white, bolt);

            // Progress bar (track + fill)
            var track = new Rectangle(60, 212, 380, 6);
            using (var trackBrush = new SolidBrush(Color.FromArgb(70, Color.White)))
                g.FillRectangle(trackBrush, track);
            int fillW = (int)(track.Width * _progress);
            if (fillW > 0)
                using (var fill = new SolidBrush(AppTheme.AccentColor))
                    g.FillRectangle(fill, track.X, track.Y, fillW, track.Height);

            // Thin border
            using (var pen = new Pen(AppTheme.AccentColor, 2))
                g.DrawRectangle(pen, 1, 1, Width - 3, Height - 3);
        }

        private void splashTimer_Tick(object sender, EventArgs e)
        {
            splashTimer.Stop();
            animTimer.Stop();

            this.Hide();
            var loginForm = new LoginForm();
            // Close the hidden splash when login closes so the app can exit
            loginForm.FormClosed += (s, args) => this.Close();
            loginForm.Show();
        }
    }
}