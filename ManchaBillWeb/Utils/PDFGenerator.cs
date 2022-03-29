using ManchaBillWeb.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using ManchaBillWeb.Services;

namespace ManchaBillWeb.Utils
{
    public class PDFGenerator
    {
        public static MemoryStream GenerateBillPDF(Bill bill,IParameterService parameterService)
        {
            string companyName = parameterService.GetParameterByName("COMPANY_NAME").Value;
            string companyPhone = parameterService.GetParameterByName("COMPANY_PHONE").Value;
            string companyAddress = parameterService.GetParameterByName("COMPANY_ADDRESS").Value;
            string companyCif = parameterService.GetParameterByName("COMPANY_CIF").Value;
            string companyEmail = parameterService.GetParameterByName("COMPANY_EMAIL").Value;


            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            MemoryStream PDFData = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);
            writer.CloseStream = false;

            var titleFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, BaseColor.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            var EmailFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLUE);
            var TotalFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            BaseColor TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            Rectangle pageSize = writer.PageSize;
            // Open the Document for writing
            pdfDoc.Open();
            //Add elements to the document here

            #region Top tableregi
            // Create the header table 
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 100f, 320f, 150f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            //headertable.DefaultCell.Border = Rectangle.BOX; //for testing           

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("./images/logo_company.png");
            logo.ScaleToFit(400, 60);

            {
                PdfPCell pdfCelllogo = new PdfPCell(logo);
                pdfCelllogo.Border = Rectangle.NO_BORDER;
                pdfCelllogo.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                pdfCelllogo.BorderWidthBottom = 1f;
                headertable.AddCell(pdfCelllogo);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                middlecell.BorderWidthBottom = 1f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase(companyName, titleFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase(companyAddress, bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase(companyPhone, bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase(companyEmail, EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                PdfPCell nextPostCell5 = new PdfPCell(new Phrase("CIF: " + companyCif, bodyFont));
                nextPostCell5.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell5);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                headertable.AddCell(nesthousing);
            }


            PdfPTable Invoicetable = new PdfPTable(3);
            Invoicetable.HorizontalAlignment = 0;
            Invoicetable.WidthPercentage = 100;
            Invoicetable.SetWidths(new float[] { 100f, 320f, 200f });  // then set the column's __relative__ widths
            Invoicetable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("Titular:", bodyFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase(bill.Client, titleFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                //PdfPCell nextPostCell3 = new PdfPCell(new Phrase("xxx Silver City, TX xxxx, US", bodyFont));
                //nextPostCell3.Border = Rectangle.NO_BORDER;
                //nested.AddCell(nextPostCell3);
                //PdfPCell nextPostCell4 = new PdfPCell(new Phrase("shivam@example.com", EmailFont));
                //nextPostCell4.Border = Rectangle.NO_BORDER;
                //nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                //middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //middlecell.BorderWidthBottom = 1f;
                Invoicetable.AddCell(middlecell);
            }


            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("Nº Factura: " + bill.SeriesBill.ToString() + bill.YearBill.ToString() + bill.NumberBill.ToString(), titleFontBlue)); ;
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Fecha de Emisión: " + bill.Date.ToShortDateString(), bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Fecha de Vencimiento: " + bill.Date.ToShortDateString(), bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }


            pdfDoc.Add(headertable);
            Invoicetable.PaddingTop = 10f;

            pdfDoc.Add(Invoicetable);
            #endregion

            #region Items Table
            //Create body table
            PdfPTable itemTable = new PdfPTable(7);

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 40, 5, 5, 10, 10, 10, 10 });  // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;

            PdfPCell cell1 = new PdfPCell(new Phrase("Descripción", boldTableFont));
            cell1.BackgroundColor = TabelHeaderBackGroundColor;
            cell1.HorizontalAlignment = 1;
            itemTable.AddCell(cell1);
            PdfPCell cell2 = new PdfPCell(new Phrase("Unidades", boldTableFont));
            cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell2);
            PdfPCell cell7 = new PdfPCell(new Phrase("Talla", boldTableFont));
            cell7.BackgroundColor = TabelHeaderBackGroundColor;
            cell7.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell7);
            PdfPCell cell3 = new PdfPCell(new Phrase("Precio Unitario", boldTableFont));
            cell3.BackgroundColor = TabelHeaderBackGroundColor;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell3);
            PdfPCell cell4 = new PdfPCell(new Phrase("IVA", boldTableFont));
            cell4.BackgroundColor = TabelHeaderBackGroundColor;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell4);
            PdfPCell cell5 = new PdfPCell(new Phrase("Descuento", boldTableFont));
            cell5.BackgroundColor = TabelHeaderBackGroundColor;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell5);
            PdfPCell cell6 = new PdfPCell(new Phrase("Total", boldTableFont));
            cell6.BackgroundColor = TabelHeaderBackGroundColor;
            cell6.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell6);
            foreach (BillLine line in bill.BillLines)
            {


                PdfPCell descriptionCell = new PdfPCell(new Phrase(line.Model.Item.LongDescription, bodyFont));
                descriptionCell.HorizontalAlignment = 1;
                descriptionCell.PaddingLeft = 10f;
                descriptionCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descriptionCell);

                PdfPCell unitsCell = new PdfPCell(new Phrase(line.Units.ToString(), bodyFont));
                unitsCell.HorizontalAlignment = 1;
                unitsCell.PaddingLeft = 10f;
                unitsCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(unitsCell);

                PdfPCell sizeCell = new PdfPCell(new Phrase(line.Model.Size.Description, bodyFont));
                sizeCell.HorizontalAlignment = 1;
                sizeCell.PaddingLeft = 10f;
                sizeCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(sizeCell);

                PdfPCell unitPrizeCell = new PdfPCell(new Phrase(line.UnitPrize.ToString() + "€", bodyFont));
                unitPrizeCell.HorizontalAlignment = 1;
                unitPrizeCell.PaddingLeft = 10f;
                unitPrizeCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(unitPrizeCell);

                PdfPCell taxCell = new PdfPCell(new Phrase(line.Tax.ToString() + "%", bodyFont));
                taxCell.HorizontalAlignment = 1;
                taxCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(taxCell);

                PdfPCell discountCell = new PdfPCell(new Phrase(line.Discount.ToString() + "%", bodyFont));
                discountCell.HorizontalAlignment = 1;
                discountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(discountCell);

                PdfPCell totalCell = new PdfPCell(new Phrase(line.Sum.ToString() + "€", bodyFont));
                totalCell.HorizontalAlignment = 1;
                totalCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalCell);

            }
            // Table footer
            PdfPCell totalAmtCell1 = new PdfPCell(new Phrase(""));
            totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell1);
            PdfPCell totalAmtCell2 = new PdfPCell(new Phrase(""));
            totalAmtCell2.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell2);
            PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(""));
            totalAmtCell3.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell3);
            PdfPCell totalAmtCell4 = new PdfPCell(new Phrase(""));
            totalAmtCell4.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell4);
            PdfPCell totalAmtCell5 = new PdfPCell(new Phrase(""));
            totalAmtCell5.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell5);
            PdfPCell totalAmtStrCell = new PdfPCell(new Phrase("Total", boldTableFont));
            totalAmtStrCell.Border = Rectangle.TOP_BORDER;   //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            totalAmtStrCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtStrCell);
            PdfPCell totalAmtCell = new PdfPCell(new Phrase(bill.Prize.ToString() + "€", boldTableFont));
            totalAmtCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtCell);


            pdfDoc.Add(itemTable);
            #endregion

            #region RESUMEtable
            // Create the header table 
            PdfPTable resumetable = new PdfPTable(1);
            resumetable.HorizontalAlignment = Element.ALIGN_RIGHT;
            resumetable.WidthPercentage = 100;
            resumetable.SetWidths(new float[] { 500f });  // then set the column's __relative__ widths
            PdfPCell c1 = new PdfPCell(new Phrase("Base Imponible: " + string.Format("{0:N2}", bill.Value) + "€", bodyFont));
            c1.Border = Rectangle.NO_BORDER;
            c1.HorizontalAlignment = Element.ALIGN_RIGHT;
            resumetable.AddCell(c1);
            PdfPCell c2 = new PdfPCell(new Phrase("IVA (" + bill.Tax + "%): " + string.Format("{0:N2}", (bill.Prize - bill.Value)) + "€", bodyFont));
            c2.Border = Rectangle.NO_BORDER;
            c2.HorizontalAlignment = Element.ALIGN_RIGHT;
            resumetable.AddCell(c2);
            PdfPCell c3 = new PdfPCell(new Phrase("Total: " + string.Format("{0:N2}", bill.Prize) + "€", TotalFont));
            c3.Border = Rectangle.NO_BORDER;
            c3.HorizontalAlignment = Element.ALIGN_RIGHT;
            resumetable.AddCell(c3);




            pdfDoc.Add(resumetable);
            Invoicetable.PaddingTop = 10f;

            #endregion

            PdfContentByte cb = new PdfContentByte(writer);


            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            cb = new PdfContentByte(writer);
            cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            //cb.ShowText("  ");
            //cb.NewlineShowText("");

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "En vista del cumplimiento de la normativa europea 2016/679 sobre Protección de datos (RGPD) le informamos que el tratamiento de los datos ", 40, pdfDoc.PageSize.GetBottom(50), 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "proporcionados por Ud. será responsabilidad de Manchasolutions con el objetivo de facturación, y que además se compromete a no ceder o ", 40, pdfDoc.PageSize.GetBottom(40), 0);//Just guessing that line two should be 20px down, will actually depend on the font
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "comunicar la información a terceros. Puede ejercer sus derechos de acceso, rectificación, cancelación o supresión del tratamiento a", 40, pdfDoc.PageSize.GetBottom(30), 0);//Just guessing that line two should be 20px down, will actually depend on the font
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "través de info@manchasolutions.com.", 40, pdfDoc.PageSize.GetBottom(20), 0);//Just guessing that line two should be 20px down, will actually depend on the font

            cb.EndText();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, pdfDoc.PageSize.GetBottom(60));
            cb.LineTo(pdfDoc.PageSize.Width - 40, pdfDoc.PageSize.GetBottom(60));
            cb.Stroke();

            pdfDoc.Close();

            PDFData.Seek(0, SeekOrigin.Begin);
            return PDFData;
        }
    }
}
