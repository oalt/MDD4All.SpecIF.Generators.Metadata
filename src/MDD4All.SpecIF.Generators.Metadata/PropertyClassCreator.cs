//using MDD4All.SpecIF.DataModels;
//using MDD4All.SpecIF.DataModels.Manipulation;
using System.Collections.Generic;

namespace KMRD.KMSF.ExcelToSpecIfConverter
{
    public class PropertyClassCreator
    {
        //private PrimitiveDataTypeCreator _primitiveDataTypeCreator;

        //private Dictionary<string, DataType> _enumerations;

        //public PropertyClassCreator(PrimitiveDataTypeCreator primitiveDataTypeCreator,
        //                            Dictionary<string, DataType> enumerations)
        //{
        //    _primitiveDataTypeCreator = primitiveDataTypeCreator;
        //    _enumerations = enumerations;
        //}

        //public PropertyClass CreatePropertyClass(string id,
        //                                         string revision,
        //                                         string title, 
        //                                         string description,
        //                                         bool isMultiple,
        //                                         string unit,
        //                                         string defaultValue,
        //                                         string dtType,
        //                                         string dtMin,
        //                                         string dtMax,
        //                                         string dtDigits,
        //                                         string dtEnumerationTitle)
        //{
        //    PropertyClass result = null;

        //    DataType dataType = null;

        //    if (dtType == "Enumeration")
        //    {
        //        foreach(KeyValuePair<string, DataType> keyValuePair in _enumerations)
        //        {
        //            if(keyValuePair.Value.Title == dtEnumerationTitle)
        //            {
        //                dataType = keyValuePair.Value;
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dataType = _primitiveDataTypeCreator.CreatePrimitiveDataType(dtType, dtMin, dtMax, dtDigits);
        //    }

        //    if (dataType != null)
        //    {

        //        List<MultilanguageText> propertyClassDescription = new List<MultilanguageText>
        //        {
        //            new MultilanguageText
        //            {
        //                Text = description,
        //                Format = null,
        //                Language = null
        //            }
        //        };


        //        result = new PropertyClass
        //        {
        //            DataType = new Key(dataType.ID, dataType.Revision),
        //            Title = title,
        //            Description = propertyClassDescription,
        //            ID = id,
        //            Revision = revision,
        //            Unit = unit,
        //            Multiple = isMultiple

        //        };
        //        result.SetDefaultValue(defaultValue);

                
        //    }
        //    return result;
        //}
    }
}
