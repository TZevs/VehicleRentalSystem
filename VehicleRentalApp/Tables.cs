using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalApp
{
    internal class Tables
    {
        private string[] Headers;
        private Dictionary<int, Vehicle> Rows; 
        private int[] ColumnWidths;

        public Tables(string[] headers, Dictionary<int, Vehicle> rows)
        {
            Headers = headers;
            Rows = rows;
        }
        private void SetColumnWidths()
        {
            ColumnWidths = new int[Headers.Length];
            for (int i = 0; i < Headers.Length; i++)
            {
                ColumnWidths[i] = Headers[i].Length;
                foreach (var row in Rows)
                {
                    
                }
            }
        }
        public void PrintTable()
        {
            
        }
    }
}
