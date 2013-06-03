using System;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NotesFor.HtmlToOpenXml;

namespace SimpleConvetCKEditorHTMLToOpenXML
{
    public partial class demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConvertToWord_Click(object sender, EventArgs e)
        {
            var fileName = Guid.NewGuid().ToString("N") + ".docx";
            var filePathName = AppDomain.CurrentDomain.BaseDirectory + "words\\" +fileName;
            var html = CKEditorControl1.Text;

            using (MemoryStream generatedDocument = new MemoryStream())
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = package.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = package.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }

                    HtmlConverter converter = new HtmlConverter(mainPart);
                    Body body = mainPart.Document.Body;
                    //如果有插入圖片，這一行很重要
                    converter.BaseImageUrl = new Uri("http://localhost:16777");
                    var paragraphs = converter.Parse(html);
                    for (int i = 0; i < paragraphs.Count; i++)
                    {
                        body.Append(paragraphs[i]);
                    }

                    mainPart.Document.Save();
                }

                File.WriteAllBytes(filePathName, generatedDocument.ToArray());
                ltlMessage.Text = "已輸出至" + "words\\" + fileName;
            }

        }
    }
}