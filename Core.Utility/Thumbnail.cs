using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
namespace Core.Utility
{
    public class Thumbnail
    {
        private string originalImagePath;
        private string thumbnailPath;
        private int width;
        private int height;
        private string mode;

        private Image originalImage;
        private int towidth;
        private int toheight;
        private int x;
        private int y;
        private int ow;
        private int oh;

        public Thumbnail(string originalImagePath, string thumbnailPath, Cut cut)
        {
            this.originalImagePath = originalImagePath;
            this.thumbnailPath = thumbnailPath;
            this.width = cut.width;
            this.height = cut.height;
            this.mode = cut.mode;
            MakeThumbnail(Image.FromFile(this.originalImagePath));
        }
        public Thumbnail(string picname, string originalImagePath, string thumbnailPath, Cut cut)
        {
            this.originalImagePath = originalImagePath + picname;
            this.thumbnailPath = thumbnailPath + picname;
            this.width = cut.width;
            this.height = cut.height;
            this.mode = cut.mode;
            MakeThumbnail(Image.FromFile(this.originalImagePath));
        }

        public Thumbnail(Stream s, string thumbnailPath, Cut cut)
        {
            this.thumbnailPath = thumbnailPath;
            this.width = cut.width;
            this.height = cut.height;
            this.mode = cut.mode;
            MakeThumbnail(Image.FromStream(s));
        }

        public void MakeThumbnail(Image im)
        {
            originalImage = im;
            towidth = width;
            toheight = height;

            x = 0;
            y = 0;
            ow = originalImage.Width;
            oh = originalImage.Height;
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）
                    drawing();
                    break;
                case "W"://指定宽，高按比例
                    if (ow > towidth)
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                        drawing();
                    }
                    else
                    {
                        im.Save(thumbnailPath);
                        //File.Copy(originalImagePath, thumbnailPath, true);
                    }
                    break;
                case "H"://指定高，宽按比例
                    if (oh > toheight)
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                        drawing();
                    }
                    else
                    {
                        im.Save(thumbnailPath);
                        //File.Copy(originalImagePath, thumbnailPath, true);
                    }
                    break;
                case "Cut"://指定高宽裁减（不变形）  
                    if (ow > towidth || oh > toheight)
                    {
                        if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                        {
                            oh = originalImage.Height;
                            ow = originalImage.Height * towidth / toheight;
                            y = 0;
                            x = (originalImage.Width - ow) / 2;
                        }
                        else
                        {
                            ow = originalImage.Width;
                            oh = originalImage.Width * height / towidth;
                            x = 0;
                            y = (originalImage.Height - oh) / 2;
                        }
                        drawing();
                    }
                    else
                    {
                        im.Save(thumbnailPath);
                        //File.Copy(originalImagePath, thumbnailPath, true);
                    }
                    break;
                default:
                    break;
            }
        }

        private void drawing()
        {
            //新建一个bmp图片 
            Image bitmap = new Bitmap(towidth, toheight);
            //新建一个画板 
            Graphics g = Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
            if (KiSaveAsJPEG(bitmap, thumbnailPath, 100))
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }
        private bool KiSaveAsJPEG(Image bmp, string FileName, int Qty)
        {
            try
            {
                EncoderParameter p;
                EncoderParameters ps;
                ps = new EncoderParameters(1);
                p = new EncoderParameter(Encoder.Quality, Qty);
                ps.Param[0] = p;
                bmp.Save(FileName, GetCodecInfo(ImageFormat.Jpeg), ps);
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 保存JPG时用
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns>得到指定mimeType的ImageCodecInfo</returns>
        private ImageCodecInfo GetCodecInfo(ImageFormat format)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.FormatID == format.Guid) return ici;
            }
            return null;
        }

    }
    public class Cut
    {
        public int width { get; set; }
        public int height { get; set; }
        public string mode { get; set; }
    }
}
