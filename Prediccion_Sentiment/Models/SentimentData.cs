using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_Sentiment.Models
{
    public class SentimentData
    {
        [LoadColumn(0)]
        public float Label { get; set; }

        [LoadColumn(5)]
        public string? Text { get; set; }
    }
}