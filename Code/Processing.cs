using FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementAssistant.Code
{
    public class Processing
    {
        #region ########################### TRIMMER ###########################
        public Int32 _trendStrokeOut = 10;
        #endregion

        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------

        #region ########################### BASE METHODS ###########################

        public Boolean isTrendUp(List<String[]> candlesticks)
        {
            FileWorker fileWorker = new FileWorker(@"C:\Users\nicol\Desktop\logProcess.txt");
            Double origin = 0;
            Int32 xSeample = 0;
            Double yValue = 0;
            Double variation = 0;
            Double amplitude = 0;
            Double angle = 0;

            List<Double[]> candleDoubles = convertCandlesticks(candlesticks);
            candleDoubles.Reverse();

            origin = candleDoubles[0].Max();

            for (Int32 i = 0; i < candleDoubles.Count; i++)
            {
                if (i+1 == candleDoubles.Count) { break; } //End of seamples
                xSeample += 1;
                amplitude = candleDifference(candleDoubles[i], candleDoubles[i + 1]);
                yValue = GetMiddlePoint(candleDoubles[i], candleDoubles[i + 1]);
                angle = Math.Asin(yValue / Math.Sqrt(Math.Pow(yValue, 2) + Math.Pow(xSeample, 2)));
                Double angleGrad = (180 * angle / Math.PI);
                fileWorker.AppendToEndFile($"Seample: {xSeample}\n - Y: {yValue}\n - Amplitude: {amplitude}\n - Angle: {angle}\n - Angle grad: {angleGrad}");
            }
            return true;
        }
        #endregion

        #region ########################### UTILITY METHODS ###########################
        //Returns the difference between the highest and the lowest of two candles.
        //candle[0] - OPEN  candle[1] - HIGH  candle[2] - LOW  candle[3] - CLOSE
        public Double candleDifference(Double[] candle1, Double[] candle2)
        {
            Double maxCandle1 = candle1.Max();
            Double minCandle1 = candle1.Min();
            Double maxCandle2 = candle2.Max();
            Double minCandle2 = candle2.Min();

            Double bodyDifference = 0;

            Double max = Math.Max(maxCandle1, maxCandle2);
            Double min = Math.Min(minCandle1, minCandle2);

            bodyDifference = max - min;

            return bodyDifference;
        }
        //Return the MIDDLE POINT between the ighest and the lowest of two candles.
        private Double GetMiddlePoint(Double[] candle1, Double[] candle2)
        {
            Double maxCandle1 = candle1.Max();
            Double minCandle1 = candle1.Min();
            Double maxCandle2 = candle2.Max();
            Double minCandle2 = candle2.Min();

            Double middlePoint = 0;

            Double max = Math.Max(maxCandle1, maxCandle2);
            Double min = Math.Min(minCandle1, minCandle2);

            middlePoint = (max + min) / 2;

            return middlePoint;
        }
        //Returns a new List of Double array.
        public List<Double[]> convertCandlesticks(List<String[]> candlesticks)
        {
            List<Double[]> converted = new List<Double[]>();
            foreach (String[] candlestick in candlesticks)
            {
                Double[] convertedCandle = new double[candlestick.Length];
                for (Int32 i = 0; i < candlestick.Length; i++)
                {
                    convertedCandle[i] = Convert.ToDouble(candlestick[i]);
                }
                converted.Add(convertedCandle);
            }
            return converted;
        }
        #endregion
    }
}
