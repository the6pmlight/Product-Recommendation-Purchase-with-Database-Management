using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesDataModel
{
    public class DataEntry
    {
        [KeyType(count: 262111)]
        public uint CustomerID { get; set; }
        [KeyType(count: 262111)]
        public uint ProductID { get; set; }
        public float Label { get; set; }
    }
}
