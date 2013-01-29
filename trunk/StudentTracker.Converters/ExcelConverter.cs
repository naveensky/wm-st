using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using StudentTracker.Models;

namespace StudentTracker.Converters {
    public class ExcelConverter {
        private const int DateCol = 1;
        private const int DurationCol = 2;
        private const int TeacherCol = 3;
        private const int CourseCol = 4;
        private const int TypeCol = 5;
        private const int DetailLabelCol = 8;
        private const int DetailValueCol = 9;

        public void GenerateExcel(string filePath, Student student) {

            var file = new System.IO.FileInfo(filePath);
            if (file.Directory != null && !file.Directory.Exists)
                file.Directory.Create();

            using (var excel = new ExcelPackage(file)) {
                var sheet = excel.Workbook.Worksheets.Add("Student Record");
                CreateHeaders(sheet);

                var rowIdx = 2;

                rowIdx = 2;
                sheet.Cells[rowIdx, DetailLabelCol].Value = "Name";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.Name;
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Email";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.Email;
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Mobile";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.Mobile;
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Course";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.Course.Name;
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Proposed Exam Date";
                sheet.Cells[rowIdx, DetailValueCol].Value = string.Format("{0:dd-MMM-yyyy}", student.ProposedExamDate);
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Actual Exam";
                sheet.Cells[rowIdx, DetailValueCol].Value = string.Format("{0:dd-MMM-yyyy}", student.ActualExamDate);
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "WM Prep Username";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.WmPrepUsername;
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Software Given";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.SoftwareGiven ? "Yes" : "No";
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Books Given";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.BooksGiven ? "Yes" : "No";
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Amount Paid";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.AmountPaid.HasValue ? student.AmountPaid.Value.ToString() : string.Empty;
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Amount Pending";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.AmountPending.HasValue ? student.AmountPending.Value.ToString() : string.Empty;
                sheet.Cells[++rowIdx, DetailLabelCol].Value = "Payment Date";
                sheet.Cells[rowIdx, DetailValueCol].Value = student.PaymentDate.HasValue ? student.PaymentDate.Value.ToString("dd MMM yyyy") : string.Empty;
                
                excel.Save();
            }
        }

        private void CreateHeaders(ExcelWorksheet sheet) {
            sheet.Cells["A1"].Value = "Date";
            sheet.Cells["B1"].Value = "Duration";
            sheet.Cells["C1"].Value = "Teacher";
            sheet.Cells["D1"].Value = "Course";
            sheet.Cells["E1"].Value = "Type";
            sheet.Cells["H1"].Value = "Student Details";
            sheet.Cells["A1:H1"].Style.Font.Bold = true;
        }

        public void GenerateLeadsExcel(string filePath, IEnumerable<Student> students) {

            var file = new System.IO.FileInfo(filePath);
            if (file.Directory != null && !file.Directory.Exists)
                file.Directory.Create();

            using (var excel = new ExcelPackage(file)) {
                var sheet = excel.Workbook.Worksheets.Add("Student Record");
                CreateLeadsHeaders(sheet);

                var rowIdx = 2;
                foreach (var student in students) {
                    sheet.Cells[rowIdx, 1].Value = student.Name;
                    sheet.Cells[rowIdx, 2].Value = student.Course.Name;
                    sheet.Cells[rowIdx, 3].Value = student.ProposedExamDate.HasValue ? student.ProposedExamDate.Value.ToString("dd MMM yyyy") : string.Empty;
                    sheet.Cells[rowIdx, 4].Value = student.ActualExamDate.HasValue ? student.ActualExamDate.Value.ToString("dd MMM yyyy") : string.Empty;
                    sheet.Cells[rowIdx, 5].Value = student.Score;
                    sheet.Cells[rowIdx, 6].Value = student.StudyCenter.Name;
                    ++rowIdx;
                }

                excel.Save();
            }
        }

        private void CreateLeadsHeaders(ExcelWorksheet sheet) {
            sheet.Cells["a1"].Value = "Student Name";
            sheet.Cells["b1"].Value = "Course Type";
            sheet.Cells["c1"].Value = "Proposed Date";
            sheet.Cells["d1"].Value = "Actual Date";
            sheet.Cells["e1"].Value = "Actual Score";
            sheet.Cells["f1"].Value = "Student Center";

        }
    }
}
