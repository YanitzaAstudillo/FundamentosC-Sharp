using Microsoft.ML;
using Microsoft.ML.Data;
using P_Sentiment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Sentiment.Services
{
    public class SentimentTrainer
    {
        private readonly MLContext _mlContext;

        public SentimentTrainer() 
        {
            _mlContext = new MLContext(); // Instanciacion
        }

        public ITransformer Train(string datasetPath)
        {
            var tempPath = Path.Combine(Path.GetTempPath(), "dataset_utf8.csv");

            File.WriteAllText(
                tempPath,
                File.ReadAllText(datasetPath, Encoding.GetEncoding("latin1")),
                Encoding.UTF8
            );

            var data = _mlContext.Data.LoadFromTextFile<SentimentData>(
                tempPath,
                hasHeader: false,
                separatorChar: ',',
                allowQuoting: true
            );

            //Cache
            var cachedData = _mlContext.Data.Cache(data);

            //Shuffle
            var shuffled = _mlContext.Data.ShuffleRows(cachedData, seed: 1);

            //Split
            var split = _mlContext.Data.TrainTestSplit(shuffled, testFraction: 0.2);

            
            var pipeline = _mlContext.Transforms.Text.FeaturizeText(
                    outputColumnName: "Features",
                    options: new Microsoft.ML.Transforms.Text.TextFeaturizingEstimator.Options
                    {
                        WordFeatureExtractor = new Microsoft.ML.Transforms.Text.WordBagEstimator.Options
                        {
                            NgramLength = 2,
                            UseAllLengths = true
                        },
                        CharFeatureExtractor = new Microsoft.ML.Transforms.Text.WordBagEstimator.Options
                        {
                            NgramLength = 3,
                            UseAllLengths = false
                        }
                    },
                    inputColumnNames: nameof(SentimentData.Text))

                .Append(_mlContext.Transforms.CustomMapping<SentimentData, SentimentBinary>(
                    (input, output) =>
                    {
                        output.LabelBool = input.Label == 4;
                    }, contractName: "LabelMapping"))

                .Append(_mlContext.Transforms.CopyColumns(
                    outputColumnName: "Label",
                    inputColumnName: "LabelBool"))

                .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression());

            //Train
            var model = pipeline.Fit(split.TrainSet);

            _mlContext.Model.Save(model, data.Schema, "model.zip");

            //Evaluate
            var predictions = model.Transform(split.TestSet);
            var metrics = _mlContext.BinaryClassification.Evaluate(predictions);

            //servicio
            var reportService = new ReportService();
            reportService.GenerateReport(metrics);

            return model;
        }
    }
}
