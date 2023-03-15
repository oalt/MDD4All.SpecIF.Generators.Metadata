//using MDD4All.SpecIF.DataModels;
using System.Collections.Generic;

namespace MDD4All.SpecIF.Generators.Metadata
{
    public class PrimitiveDataTypeCreator
    {
        /*private Dictionary<string, DataType> _primitiveDataTypes = new Dictionary<string, DataType>();

        public PrimitiveDataTypeCreator()
        {
        }

        public PrimitiveDataTypeCreator(List<DataType> baseDataTypes)
        {
            foreach(DataType dataType in baseDataTypes)
            {
                _primitiveDataTypes.Add(dataType.ID, dataType);
            }
        }


        public List<DataType> GetAllGeneratedPrimitiveDataTypes()
        {
            List<DataType> result = new List<DataType>();

            foreach(KeyValuePair<string, DataType> keyValuePair in _primitiveDataTypes)
            {
                result.Add(keyValuePair.Value);
            }

            return result;
        }

        public DataType CreatePrimitiveDataType(string typeIn, string minInclusiveIn, string maxInclusiveIn, string fractionDigitsIn)
        {
            DataType result = null;

            bool existingTypeMatchFound = false;

            string type = typeIn.Trim();

            int? minInclusive = null;

            if(!string.IsNullOrEmpty(minInclusiveIn))
            {
                int min = 0; 
                if(int.TryParse(minInclusiveIn, out min))
                {
                    minInclusive = min;
                }
            }

            int? maxInclusive = null;

            if (!string.IsNullOrEmpty(maxInclusiveIn))
            {
                int max = 0;
                if (int.TryParse(maxInclusiveIn, out max))
                {
                    maxInclusive = max;
                }
            }

            int? fractionDigits = null;

            if (!string.IsNullOrEmpty(fractionDigitsIn))
            {
                int digits = 0;
                if (int.TryParse(fractionDigitsIn, out digits))
                {
                    fractionDigits = digits;
                }
            }

            foreach (KeyValuePair<string, DataType> keyValuePair in _primitiveDataTypes)
            {
                DataType existingType = keyValuePair.Value;

                // compare existing type
                if (existingType.Type == type)
                {
                    if(existingType.Type.Equals("xs:string"))
                    {
                        if (existingType.MaxLength == maxInclusive)
                        {
                            existingTypeMatchFound = true;
                            result = existingType;
                            break;
                        }
                    }
                    else
                    {
                        if(existingType.MinInclusive == minInclusive &&
                           existingType.MaxInclusive == maxInclusive &&
                           existingType.FractionDigits == fractionDigits)
                        {
                            existingTypeMatchFound = true;
                            result = existingType;
                            break;
                        }
                    }
                }
                  
            }

            if(!existingTypeMatchFound)
            {
                // not matching type found, create a new one

                result = new DataType
                {
                    ID = CalculateDataTypeID(type, minInclusive, maxInclusive, fractionDigits),
                    Revision = "1",
                    Title = CalculateDataTypeTitle(type, minInclusive, maxInclusive, fractionDigits),
                    Type = typeIn
                };

                if(result.Type == "xs:string")
                {
                    result.MaxLength = maxInclusive;
                }
                else
                {

                    result.MinInclusive = minInclusive;
                    result.MaxInclusive = maxInclusive;
                    result.FractionDigits = fractionDigits;
                }

                _primitiveDataTypes.Add(result.ID, result);

            }

            return result;
        }

        private string CalculateDataTypeID(string type, int? minInclusive, int? maxInclusive, int? fractionDigits)
        {
            string result = "";

            result = "DT-";

            result += type.Trim().Replace("xs:", "");

            if(minInclusive != null)
            {
                result += "Min" + minInclusive;
            }

            if (maxInclusive != null)
            {
                result += "Max" + maxInclusive;
            }

            if (fractionDigits != null)
            {
                result += "FD" + fractionDigits;
            }

            return result;
        }

        private string CalculateDataTypeTitle(string type, int? minInclusive, int? maxInclusive, int? fractionDigits)
        {
            string result = "";

            result += type.Trim().Replace("xs:", "");

            if (minInclusive != null)
            {
                result += "Min" + minInclusive;
            }

            if (maxInclusive != null)
            {
                result += "Max" + maxInclusive;
            }

            if (fractionDigits != null)
            {
                result += "FD" + fractionDigits;
            }

            return result;
        }
        */
    }
}
