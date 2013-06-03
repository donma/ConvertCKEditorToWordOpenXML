using System;
using System.IO;
using System.Web;

namespace SimpleConvetCKEditorHTMLToOpenXML
{
    public partial class fileupload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            HttpPostedFile uploads = Context.Request.Files["upload"];
            string ckediotfuncnum = Context.Request["CKEditorFuncNum"];
            var t = Guid.NewGuid().ToString("N");

            //如果暫存資料夾不在就建立
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "uploaded\\"))
            {
                Directory.GetCreationTime(AppDomain.CurrentDomain.BaseDirectory + "uploaded\\");
            }

            //判斷圖片是否為.png or .jpg 
            //當然這方法並非很好
            //建議作法依然要打打開圖片做檢查會比較安全
            if (Path.GetExtension(uploads.FileName).ToLower() == ".jpg" || Path.GetExtension(uploads.FileName).ToLower() == ".png")
            {
                string url = "/uploaded/" + t + Path.GetExtension(uploads.FileName);
                File.WriteAllBytes(AppDomain.CurrentDomain.BaseDirectory + "uploaded\\" + t + Path.GetExtension(uploads.FileName), StreamToBytes(uploads.InputStream));
                Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + ckediotfuncnum + ",\"" + url + "\");</script>");

            }
            else
            {
                Response.Write("<script>alert('上傳格式錯誤');</script>");
                Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + ckediotfuncnum + ");</script>");
            }
        }

        /// <summary>
        /// 將Stream 轉成  Byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private byte[] StreamToBytes(Stream stream)
        {
            stream.Position = 0;
            var buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }
        

    }
}