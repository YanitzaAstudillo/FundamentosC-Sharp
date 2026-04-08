using Microsoft.ML.Data;
using System;
using System.IO;

namespace P_Sentiment.Services
{
    public class ReportService
    {
        public string REPORT_PATH = $"C:/Users/Yera/Desktop/PREDICCION/P_Sentiment/Reports/";
        public void GenerateReport(BinaryClassificationMetrics metrics)
        {
            string fileName = $"reporte_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            var report = $@"
                ===== REPORTE DEL MODELO =====
                Fecha: {DateTime.Now}

                Accuracy: {metrics.Accuracy:P2}
                F1 Score: {metrics.F1Score:P2}
                Precision: {metrics.PositivePrecision:P2}
                Recall: {metrics.PositiveRecall:P2}
                AUC: {metrics.AreaUnderRocCurve:P2}

                ================================
                ";

            File.WriteAllText(REPORT_PATH + fileName, report);

            Console.WriteLine($"Reporte generado en: {REPORT_PATH}");
        }
    }
}