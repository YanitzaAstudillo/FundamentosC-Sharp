using Microsoft.ML;
using P_Sentiment.Models;
using System.Threading.Tasks;

namespace P_Sentiment.Services
{
    public class SentimentAnalyzer
    {
        private readonly MLContext _mlContext;
        private readonly PredictionEngine<SentimentData, SentimentPrediction> _predictor;
        private readonly TranslatorService _translator;

        public SentimentAnalyzer(ITransformer model, TranslatorService translator)
        {
            _mlContext = new MLContext();
            _predictor = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
            _translator = translator;
        }

        // Método async que traduce y predice
        public async Task PredictAsync(string text)
        {
            try
            {
                string translatedText = await _translator.Traducir(text);

                var result = _predictor.Predict(new SentimentData { Text = translatedText });

                Console.WriteLine($"Texto original: {text}");
                Console.WriteLine($"Texto traducido: {translatedText}");
                Console.WriteLine($"Prediccion: {(result.Prediction ? "Positivo" : "Negativo")}");
                Console.WriteLine($"Probabilidad: {result.Probability:P2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al predecir: {ex.Message}");
            }
        }
    }
}