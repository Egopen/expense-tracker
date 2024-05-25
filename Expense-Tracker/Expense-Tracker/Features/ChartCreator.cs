using System.Drawing.Imaging;
using System.Drawing;
using Expense_Tracker.ResponseJson;

namespace Expense_Tracker.Features
{
    public class ChartCreator
    {
        public static async Task GeneratePieChartAsBytes(GraficalInfoRes info, int width = 450, int height = 450)
        {
            using (Bitmap bitmap = new Bitmap(width, height))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                double total = info.CategoryInfo.Sum(cat=> cat.ExpenseSum);
                double currentAngle = 0;

                Color[] colors = GenerateColors(info.CategoryInfo.Count);

                for (int i = 0; i < info.CategoryInfo.Count; i++)
                {
                    double sweepAngle = (info.CategoryInfo[i].ExpenseSum / total) * 360;
                    using (Brush brush = new SolidBrush(colors[i]))
                    {
                        graphics.FillPie(brush, 0, 0, width, height, (float)currentAngle, (float)sweepAngle);
                    }
                    currentAngle += sweepAngle;
                    info.CategoryInfo[i].Color = colors[i].Name;
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    info.Image=ms.ToArray();
                }
            }
        }

        private static Color[] GenerateColors(int count)
        {
            Color[] colors = new Color[count];
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                colors[i] = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            }
            return colors;
        }
    }
}
