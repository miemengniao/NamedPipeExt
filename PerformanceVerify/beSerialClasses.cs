using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GeneralUnitTests
{
    /// <summary>
    /// 
    /// </summary>
    public class BenchMarkTest
    {
        SmallClass sm = new SmallClass();
        MediumClass md = new MediumClass();
        ComplexClass complex = new ComplexClass();
        public BenchMarkTest()
        {
            sm.Id = 123421;
            md.Id = 34123;
            md.Name = (new StringBuilder()).Append('a', 1024 * 10).ToString();
            complex.Small = sm;
            complex.Medium = md;
            complex.Complex = new ComplexClass();
            complex.Complex.Complex = new ComplexClass();
            

        }

        /*
         *  Compare the result, Binary serialize have similar perfromance feature with Json on complex object. consider the benifit of Binary Seriliaze. 
         *  we should keep the Binary Serialize feature.
         // * Summary 

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Microsoft Windows NT 6.2.9200.0
Processor=Intel(R) Core(TM) i7-6700HQ CPU 2.60GHz, ProcessorCount=4
Frequency=2531252 ticks, Resolution=395.0614 ns, Timer=TSC
CLR=MS.NET 4.0.30319.42000, Arch=32-bit RELEASE
GC=Concurrent Workstation
JitModules=clrjit-v4.6.1586.0

Type=BenchMarkTest  Mode=Throughput

                Method |      Median |    StdDev |
---------------------- |------------ |---------- |
   SerialSmallByBinary |   8.7089 us | 0.4003 us |
     SerialSmallByJson |   3.0674 us | 0.1054 us |
  SerialMediumByBinary |  77.6600 us | 2.8556 us |
    SerialMediumByJson |  72.8592 us | 6.0663 us |
 SerialComplexByBinary | 164.3182 us | 7.8039 us |
   SerialComplexByJson | 164.0717 us | 5.4016 us |

         */

        #region Binary VS Json Serial

        [Benchmark]
        public void SerialSmallByBinary()
        {
            SerialByBinary(sm);
        }

       

        [Benchmark]
        public void SerialSmallByJson()
        {
            SerialByJson(sm);
        }

      

        [Benchmark]
        public void SerialMediumByBinary()
        {
            SerialByBinary(md);
        }

        [Benchmark]
        public void SerialMediumByJson()
        {
            SerialByJson(md);
        }

        [Benchmark]
        public void SerialComplexByBinary()
        {
            SerialByBinary(complex);
        }

        [Benchmark]
        public void SerialComplexByJson()
        {
            SerialByJson(complex);
        }

        private void SerialByJson(object sm)
        {
           var tmp= JsonConvert.SerializeObject(sm);
            JsonConvert.DeserializeObject(tmp);
        }
        private void SerialByBinary(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                bf.Deserialize(ms);
            }
        }
        #endregion
    }


    [Serializable]
   public class SmallClass
    {
        public int Id { get; set; }
    }

    [Serializable]
    public class MediumClass : SmallClass
    {
        public string Name { get; set; }
    }

    [Serializable]
    public class ComplexClass : MediumClass
    {

        public SmallClass Small { get; set; } = new SmallClass();
        public MediumClass Medium { get; set; } = new MediumClass();
        public ComplexClass Complex { get; set; }

        public int MyProperty1 { get; set; } = 2;
        public int MyProperty2 { get; set; } = 2;
        public int MyProperty3 { get; set; } = 2;
        public int MyProperty4 { get; set; } = 2;
        public int MyProperty5 { get; set; } = 2;
        public int MyProperty6 { get; set; } = 2;
        public int MyProperty7 { get; set; } = 2;
        public int MyProperty8 { get; set; } = 2;
        public int MyProperty9 { get; set; } = 2;

        public string strProp1 { get; set; } = "asdfasdfasdf";
        public string strProp2 { get; set; } = "asdfasdfasdf";

        public string strProp3 { get; set; } = "asdfasdfasdf";
        public string strProp4 { get; set; } = "asdfasdfasdf";
        public string strProp5 { get; set; } = "asdfasdfasdf";
        public string strProp6 { get; set; } = "asdfasdfasdf";
        public string strProp7 { get; set; } = "asdfasdfasdf";
        public string strProp8 { get; set; } = "asdfasdfasdf";

    }
}
