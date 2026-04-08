using Microsoft.ML;
using P_Sentiment.Services;
using P_Sentiment.Models;

public class Program
{
    public static void Main()
    {
        ExecutePipeline();
    }

    public static void ExecutePipeline()
    {
        var datasetPath = Path.Combine("Data", "archive", "training.1600000.processed.noemoticon.csv");

        var translator = new TranslatorService();

        var modelPath = Path.Combine(AppContext.BaseDirectory, "model.zip");
        ITransformer model;
        var mlContext = new MLContext();

        // Cargar modelo si existe
        if (File.Exists(modelPath))
        {
            using var stream = new FileStream(modelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            model = mlContext.Model.Load(stream, out var _);
        }
        else
        {
            var trainer = new SentimentTrainer();
            model = trainer.Train(datasetPath); //tambien guarda el modelo
            Console.WriteLine("Modelo entrenado y guardado.");
        }

        var analyzer = new SentimentAnalyzer(model, translator);

        Console.WriteLine("=== MODO INTERACTIVO ===");
        Console.WriteLine("Escribe 'salir' para terminar.");

        while (true)
        {
            Console.Write("\nTexto: ");
            string input = Console.ReadLine()!;

            if (input.ToLower() == "salir")
                break;

            // Ejecutar la predicción async de forma sincrónica
            analyzer.PredictAsync(input).GetAwaiter().GetResult();
        }
    }
}