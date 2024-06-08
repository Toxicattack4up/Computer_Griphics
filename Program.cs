using System;
using System.Drawing;
using System.Windows.Forms;

public class Polygon
{
    public int Num { get; set; }
    public PointF[] Vertices { get; set; }

    public Polygon(int num)
    {
        Num = num;
        Vertices = new PointF[num];
    }
}

public class MainForm : Form
{
    private Polygon polygon;

    public MainForm()
    {
        this.Text = "GDI+ Example";
        this.ClientSize = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.DoubleBuffered = true;

        polygon = new Polygon(4)
        {
            Vertices = new PointF[]
            {
                new PointF(100, 200),
                new PointF(200, 100),
                new PointF(300, 200),
                new PointF(200, 300)
            }
        };

        this.Paint += new PaintEventHandler(OnPaint);
        this.KeyDown += new KeyEventHandler(OnKeyDown);
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(Color.Black);

        g.TranslateTransform(ClientSize.Width / 4, ClientSize.Height / 4);

        DrawPolygon(g, polygon);
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.D1:
                Resize(polygon, 1.1f, 1.0f); // Увеличение по горизонтали
                break;
            case Keys.D2:
                Resize(polygon, 0.9f, 1.0f); // Уменьшение по горизонтали
                break;
            case Keys.D3:
                Resize(polygon, 1.0f, 1.1f); // Увеличение по вертикали
                break;
            case Keys.D4:
                Resize(polygon, 1.0f, 0.9f); // Уменьшение по вертикали
                break;
        }
        Invalidate(); // Перерисовать окно
    }

    private void DrawPolygon(Graphics g, Polygon polygon)
    {
        using (Pen pen = new Pen(Color.Red, 3))
        {
            g.DrawPolygon(pen, polygon.Vertices);
        }
    }

    private void Resize(Polygon polygon, float kx, float ky)
    {
        for (int i = 0; i < polygon.Num; ++i)
        {
            polygon.Vertices[i].X *= kx;
            polygon.Vertices[i].Y *= ky;
        }
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
