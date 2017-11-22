﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using Parquet.Data;

namespace Parquet.Data.Concrete
{
   class Int96DataType : BasicPrimitiveDataType<BigInteger>
   {
      public Int96DataType() : base(DataType.Int96, Thrift.Type.INT96)
      {
      }

      public override bool IsMatch(Thrift.SchemaElement tse, ParquetOptions formatOptions)
      {
         return tse.Type == Thrift.Type.INT96 && !formatOptions.TreatBigIntegersAsDates;
      }

      protected override BigInteger ReadOne(BinaryReader reader)
      {
         byte[] data = reader.ReadBytes(12);
         var big = new BigInteger(data);
         return big;
      }

      protected override void WriteOne(BinaryWriter writer, BigInteger value)
      {
         byte[] data = value.ToByteArray();
         writer.Write(data);
      }
   }
}
