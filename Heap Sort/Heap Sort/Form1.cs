using System.Runtime.InteropServices;

namespace Heap_Sort
{
    public partial class Form1 : Form
    {
        public static Array StartingArray,SortingArray,Next;
        public static List<Array> Arrays = new List<Array>();
        static Pen pen = new Pen(Color.Black);
        public Form1()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.KeyDown += Form1_KeyDown;
        }
        public static void Heapify(Array Arr,int Length,bool FirstPass) {
            int[] Temp = Arr.array,Copy;
            int Current = 0,Swap;
            for (int i = 0; i < Length; i++) {
                if ((i * 2 + 1) > Length-1)
                    break;
                if (Temp[i] < Temp[(i * 2) + 1])
                {
                    Current = (i * 2) + 1;
                    do
                    {
                        if (Temp[Current] > Temp[(Current - 1) / 2])
                        {
                            Swap = Temp[(Current - 1) / 2];
                            Temp[(Current - 1) / 2] = Temp[Current];
                            Temp[Current] = Swap;
                            if (!FirstPass)
                            {
                                Copy = new int[Temp.Length];
                                Copy = (int[])Temp.Clone();
                                Arrays.Add(new Array(Copy, Length, Current, (Current - 1) / 2));
                            }
                            Current = (Current - 1) / 2;
                        }
                        else break;
                    }
                    while (Current >= 1);
                    i = 0;
                }
                if ((i * 2 + 2) > Length-1)
                    break;
                if (Temp[i] < Temp[(i * 2) + 2])
                {
                    Current = (i * 2) + 2;
                    do
                    {
                        if (Temp[Current] > Temp[(Current - 2) / 2])
                        {
                            Swap = Temp[(Current - 2) / 2];
                            Temp[(Current - 2) / 2] = Temp[Current];
                            Temp[Current] = Swap; 
                            if (!FirstPass)
                            {
                                Copy = new int[Temp.Length];
                                Copy = (int[])Temp.Clone();
                                Arrays.Add(new Array(Copy, Length, Current, (Current - 2) / 2));
                            }
                            Current = (Current - 2) / 2;
                            
                        }
                        else break;
                    }
                    while (Current>=2);
                    i = 0;
                }
            }

        }
        public static void HeapSort(Array arr) {
            int[] temp = arr.array,copy;
            int swap;
            for (int i = arr.length - 1; i > 0; i--) {
                swap = temp[0];
                temp[0] = temp[i];
                temp[i] = swap;
                copy = new int[temp.Length];
                copy = (int[])temp.Clone();
                Arrays.Add(new Array(copy,i,0,i));
                Heapify(arr,i,false);
            
            }

        }
        ListView listView = new ListView();
        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetStyle(
               System.Windows.Forms.ControlStyles.UserPaint |
               System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
               System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
               true);

            listView.Width = 700;
            listView.Location =new Point(550,900);
            listView.Font = new Font(listView.Font.FontFamily, 25);
            int n = 10;
            int[] arr = new int[n], arr2 = new int[n];
            //Initialize array.
            for (int i = 0; i < n; i++) {
                arr[i] = i + 1;
            }
            arr2 = (int[]) arr.Clone();
            StartingArray = new Array(arr, arr.Length,0,0);
            Arrays.Add(StartingArray);
            SortingArray = new Array(arr2, arr.Length,0,0);
           

            Heapify(SortingArray,SortingArray.length,true);
            arr2 = (int[])SortingArray.array.Clone();
            Arrays.Add(new Array(arr2,arr2.Length,0,0));
            HeapSort(SortingArray);
            Next = Arrays.ElementAt(select + 1);

        }

        int select = 0;
        bool Highlight = false;
        Array Current;
        private void Form1_KeyDown(object sender, KeyEventArgs e) { 
        if(e.KeyCode == Keys.Space) {
                if (!Highlight)
                {
                    foreach (Label label in Current.labels)
                    {
                        this.Controls.Remove(label);
                    }
                    if (select < Arrays.Count - 1)
                        select++;
                    Highlight = true;
                }
                else
                {
                    Highlight = false;

                  
                }

            }
            this.Controls.Clear();
            Invalidate();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (!Highlight)
                {
                    foreach (Label label in Current.labels)
                    {
                        this.Controls.Remove(label);
                    }
                    if (select < Arrays.Count - 1)
                        select++;
                    Highlight = true;
                }
                else
                {
                    Highlight = false;

                }

            }
            this.Controls.Clear();
            Invalidate();

        }
        List<Label> labels = new List<Label>();
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            listView.Clear();
            if(select< Arrays.Count - 2)
                Next = Arrays.ElementAt(select + 1);
            Current = Arrays.ElementAt(select);
            pen.Width = 10;
            int radius = 100;
            int StartingX = this.Width / 2 - radius,StartingY=100;
            int LabelX = 0;
            Label label;
            int Levels,NumberOfNodes=0;
            if (Current.length > 7)
                Levels = 4;
            else if (Current.length > 3)
                Levels = 3;
            else if (Current.length > 1)
                Levels = 2;
            else
                Levels = 1;
            int shift = 0, nodeshift = 0,shiftStore=450;
            bool shiftSet = false;
            for (int i = 0;i <Levels ;i++) {
                NumberOfNodes = (int)Math.Pow(2, i);
                int Start = NumberOfNodes - 1;
                shiftSet = false;

                for (int j = Start; j < Start+NumberOfNodes; j++) {
                    if (j >= Current.length)
                        break;
                    if (Highlight)
                    {
                        if (j == Current.Node2)
                            pen.Color = Color.Green;
                        else if (j == Current.Node1)
                            pen.Color = Color.Blue;
                        else
                            pen.Color = Color.Black;
                    }
                    else
                    {

                        if (j == Next.Node1)
                            pen.Color = Color.Green;
                        else if (j == Next.Node2)
                            pen.Color = Color.Blue;
                        else
                            pen.Color = Color.Black;
                    }
                    label = new Label();
                    label.Text = Current.array[j].ToString();
                    label.Font = new Font(this.Font.FontFamily, 20);
                    label.Width = 50;
                    label.Height = 30;
                    switch (label.Text.Length)
                    {
                        case 1:
                            LabelX = 37;
                            break;
                        case 2:
                            LabelX = 25;
                            break;
                    }
                    if (i == 0) {
                        e.Graphics.DrawEllipse(pen, StartingX - shift + ((j - Start) * nodeshift), StartingY + (i * 100), radius, radius);
                        label.Location = new Point((StartingX + LabelX) - shift + ((j - Start) * nodeshift), StartingY + 50 - ((int)(label.Height / 1.5)) + (i * 100));
                    }
                    else if (j < (Start + Start + NumberOfNodes) / 2 && i != 0)
                    {
                        shift = shiftStore;
                        e.Graphics.DrawEllipse(pen, StartingX - shift + ((j - Start) * nodeshift), StartingY + (i * 200), radius, radius);
                        label.Location = new Point((StartingX + LabelX) - shift + ((j - Start) * nodeshift), StartingY + 50 - ((int)(label.Height / 1.5)) + (i * 200));
                    }
                    else if (j >= (Start + Start + NumberOfNodes) / 2 && i != 0)
                    {
                        shift = -400;
                        e.Graphics.DrawEllipse(pen, StartingX - shift + ((j - ((Start + Start + NumberOfNodes) / 2)) * nodeshift), StartingY + (i * 200), radius, radius);
                        label.Location = new Point((StartingX + LabelX) - shift + ((j - ((Start + Start + NumberOfNodes) / 2)) * nodeshift), StartingY + 50 - ((int)(label.Height / 1.5)) + (i * 200));

                    }
                    this.Controls.Add(label);
                    Current.labels[j] = new Label();
                    Current.labels[j].Text = label.Text;
                }
                if (i > 0)
                {
                    shift = shiftStore;
                    shift += 450 /(i+1);
                    nodeshift = 450 / (i + 1);
                    shiftStore = shift;
                }
            }
            for(int i=0; i<Current.array.Length; i++)
            {
                listView.Items.Add(Current.array[i].ToString());
                if (Highlight)
                {
                    if (i == Current.Node2)
                    {
                        listView.Items[i].ForeColor = Color.Green;
                        listView.Items[i].BackColor = Color.LightGreen;
                    }
                    else if (i == Current.Node1)
                    {
                        listView.Items[i].ForeColor = Color.Blue;
                        listView.Items[i].BackColor = Color.LightBlue;
                    }
                    else
                        listView.Items[i].ForeColor = Color.Black;
                }
                else
                {

                    if (i == Next.Node1)
                    {
                        listView.Items[i].ForeColor = Color.Green;
                        listView.Items[i].BackColor = Color.LightGreen;
                    }
                    else if (i == Next.Node2)
                    {
                        listView.Items[i].ForeColor = Color.Blue;
                        listView.Items[i].BackColor = Color.LightBlue;
                    }
                    else
                        listView.Items[i].ForeColor = Color.Black;
                }
            }
            this.Controls.Add(listView);

        }
    }
}