using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using MDD4All.SpecIF.DataModels;
using MDD4All.SpecIF.DataModels.Manipulation;
using MDD4All.SpecIF.DataProvider.Contracts;

namespace MDD4All.SpecIF.Generators.Metadata
{
    public class MetaDataGenerator
    {


        private ISpecIfMetadataReader _metadataReader;
        private ISpecIfDataReader _dataReader;


        public MetaDataGenerator(ISpecIfMetadataReader metadataReader, ISpecIfDataReader dataReader)
        {
            _metadataReader = metadataReader;
            _dataReader = dataReader;


        }


        public Dictionary<Key, ResourceClass> ResourceClasses { get; set; } = new Dictionary<Key, ResourceClass>();
        public Dictionary<Key, PropertyClass> PropertyClasses { get; set; } = new Dictionary<Key, PropertyClass>();
        public Dictionary<Key, StatementClass> StatementClasses { get; set; } = new Dictionary<Key, StatementClass>();
        public Dictionary<Key, DataType> DataTypes { get; set; } = new Dictionary<Key, DataType>();


        public void GenerateMetaData(Node hierarchy)
        {

            GenerateMetaDataRecursively(hierarchy);

        }

        private void GenerateMetaDataRecursively(Node currentNode)
        {
            Resource nodeAssignedResource = _dataReader.GetResourceByKey(currentNode.ResourceReference);
            ResourceClass resourceClass = _metadataReader.GetResourceClassByKey(nodeAssignedResource.Class);

            string resourceTyp = resourceClass.Title;

            switch (resourceTyp)
            {
                case "SpecIF:TermResourceClass":

                    GenerateResourceClass(nodeAssignedResource, resourceClass);


                    break;


                case "SpecIF:TermStatementClass":

                    //GenerateStatementClass()

                    break;


                case "SpecIF:TermPropertyClassString":



                    break;

                case "SpecIF:TermPropertyValue":



                    break;




            }

            foreach (Node child in currentNode.Nodes)
            {
                GenerateMetaDataRecursively(child);
            }
        }

        private void GenerateResourceClass(Resource resource, ResourceClass resourceClass)
        {
            ResourceClass result = new ResourceClass();



            string isHeading = resource.GetPropertyValue("SpecIF:isHeading", _metadataReader);
            string icon = resource.GetPropertyValue("SpecIF:Icon", _metadataReader);
            string identifier = resource.GetPropertyValue("dcterms:identifier", _metadataReader);
            string title = resource.GetPropertyValue("dcterms:title", _metadataReader);
            string description = resource.GetPropertyValue("dcterms:description", _metadataReader);

            //später
            string domain = resource.GetPropertyValue("SpecIF:Domain", _metadataReader);
            string lifeCycleStatus = resource.GetPropertyValue("SpecIF:LifecycleStatus", _metadataReader);


            result.ID = identifier;
            result.Title = title;
            result.Description = new List<MultilanguageText>()
            {
                new MultilanguageText(description)
            };
            result.Revision = ""; // revision fehlt noch
            //result.Replaces = "";//replaces fehlt noch
            result.Icon = icon;

            // bool.TryParse
            bool parseResult;
            if (bool.TryParse(isHeading, out parseResult))
            {

                result.isHeading = parseResult;

            }


            //result.Instantiation = ""; //instantiation fehlt noch


            //statement findet man durch datareader

            Key resourceKey = new Key(resource.ID, resource.Revision);

            List<Statement> statements = _dataReader.GetAllStatementsForResource(resourceKey);



            //result.ChangedAt = ""; // ChangedAt fehlt

            foreach (Statement statement in statements)
            {
                StatementClass statementClass = _metadataReader.GetStatementClassByKey(statement.Class);
                if (statementClass.Title == "SpecIF:hasProperty")
                {


                    Console.WriteLine(statementClass.Title);

                    if (statement.StatementSubject.ID == resource.ID)
                    {
                        Resource objectResource = _dataReader.GetResourceByKey(statement.StatementObject);

                        ResourceClass objectResourceClass = _metadataReader.GetResourceClassByKey(objectResource.Class);

                        if (objectResourceClass.Title == "SpecIF:TermPropertyClassString")
                        {
                            Console.WriteLine(objectResourceClass.Title);

                            Key propertyClassKey = GeneratePropertyClass(objectResource);

                            result.PropertyClasses.Add(propertyClassKey);

                        }

                    }
                }

            }


            // foreach statement check if class.title == "hasProperty"...
            //durch key von Statement vergleichen ob subjekt und objekt oder Zyklus
            //nach vergleich Objekt holen --> ist termpropertyClass --> könnte man generieren, aber in einer anderen methode
            //nur Propertyclass titel einmal generieren ansonsten bestehendes verwenden




            Key key = new Key(result.ID, result.Revision);
            ResourceClasses.Add(key, result);

        }


        private void GenerateStatementClass(Statement statement, StatementClass statementClass)
        {
            StatementClass result = new StatementClass();

            string isUndirected = statement.GetPropertyValue("SpecIF:isUndirected", _metadataReader);
            string icon = statement.GetPropertyValue("SpecIF:Icon", _metadataReader);
            string identifier = statement.GetPropertyValue("dcterms:identifier", _metadataReader);
            string title = statement.GetPropertyValue("dcterms:title", _metadataReader);
            string description = statement.GetPropertyValue("dcterms:description", _metadataReader);
            string origin = statement.GetPropertyValue("SpecIF:Origin", _metadataReader);
            string domain = statement.GetPropertyValue("SpecIF:Domain", _metadataReader);
            string lifecycleStatus = statement.GetPropertyValue("SpecIF:LifecycleStatus", _metadataReader);


            result.ID = identifier;
            result.Title = title;
            // result.isExtension = isExtension;
            //result.createdAt = createdAt;
            // result.dataTypes = dataTypes;

            Key key = new Key(result.ID, result.Revision);
            StatementClasses.Add(key, result);


        }


        private Key GeneratePropertyClass(Resource resource)
        {
            Key result = null;

            string identifier = resource.GetPropertyValue("dcterms:identifier", _metadataReader);
            string revision = null; // fehlt noch in der Ontologie
            string title = resource.GetPropertyValue("dcterms:titel", _metadataReader);
            string textFormat = resource.GetPropertyValue("SpecIF:TextFormat", _metadataReader);
            string stringDefault = resource.GetPropertyValue("SpecIF:StringDefault", _metadataReader);
            string stringMaxLength = resource.GetPropertyValue("SpecIF:StringMax", _metadataReader);
            string multiple = resource.GetPropertyValue("SpecIF:multiple", _metadataReader);
            //string unit = resource.GetPropertyValue()   unit fehlt noch        
            //string values fehlt auch noch
            string description = resource.GetPropertyValue("dcterms:description", _metadataReader);
            //später
            string domain = resource.GetPropertyValue("SpecIF:Domain", _metadataReader);
            string lifecycleStatus = resource.GetPropertyValue("SpecIF:LifecycleStatus", _metadataReader);


            result = new Key(identifier, revision);

            if (PropertyClasses.ContainsKey(result))
            {

            }
            else
            {
                PropertyClass newPropertyClass = new PropertyClass();

                newPropertyClass.ID = identifier;
                newPropertyClass.Revision = revision;
                newPropertyClass.Title = title;
                newPropertyClass.Format = textFormat;
                //newPropertyClass.stringDefault=stringDefault; fehlt noch
                //newPropertyClass.stringMaxLength=stringMaxLength; fehlt noch

                newPropertyClass.Description = new List<MultilanguageText>()
                {
                new MultilanguageText(description)
                };
                if (resource.GetPropertyValue("SpecIF:multiple", _metadataReader) == "true")
                {
                    newPropertyClass.Multiple = true;

                }
                else
                {
                    newPropertyClass.Multiple = false;
                }

                //newPropertyClass.Unit=unit;




                newPropertyClass.DataType = GenerateOrFindDataType();

                PropertyClasses.Add(result, newPropertyClass);


            }


            return result;
        }

        private Key GenerateOrFindDataType(List<MultilanguageText> description, List<EnumerationValue> enumaration,
                                           int fractionDigits, int maxInclusive, int maxLength, int minInclusive,
                                           bool multiple, string title, string type)
        {
            Key result = null;
            bool match = false;
            

            foreach (KeyValuePair<Key, DataType> dataTypeElement in DataTypes)
            {
                DataType currentDataType = dataTypeElement.Value;
                // als erstes den type miteinander Vergleichen
                if (currentDataType.Type == type)
                {
                    if (currentDataType.Type == "xs:string")
                    {

                        if (currentDataType.Title == title)
                        {

                            if (currentDataType.MaxLength==maxLength)
                            {
                                result = dataTypeElement.Key;
                                match = true;
                                break;
                            }
                            
                        }
                        else
                        {

                            if (currentDataType.MinInclusive == minInclusive &&
                           currentDataType.MaxInclusive == maxInclusive &&
                           currentDataType.FractionDigits == fractionDigits)
                            {
                                match = true;
                                result = dataTypeElement.Key;
                                break;
                            }

                        }



                    }
                    else if (currentDataType.Type == "xs:boolean")
                    {
                        match = true;
                        result = dataTypeElement.Key;
                        break;

                    }

                    else if (currentDataType.Type == "xs:integer")
                    {
                        if (currentDataType.Title == title)
                        {
                            if (currentDataType.MaxInclusive == maxInclusive)
                            {
                                if(currentDataType.MinInclusive == minInclusive)
                                {
                                    result = dataTypeElement.Key;
                                    match = true;
                                    break;
                                }
                            }

                        }
                    }
                    else if (currentDataType.Type == "xs:double")
                    {
                        if (currentDataType.Title == title)
                        {
                            if (currentDataType.MaxInclusive == maxInclusive)
                            {
                                if (currentDataType.MinInclusive == minInclusive)
                                {
                                    result = dataTypeElement.Key;
                                    match = true;
                                    break;
                                }
                            }

                        }
                    }
                    else if (currentDataType.Type == "xs:anyURI")
                    {
                        if (currentDataType.Title == title)
                        {
                            result = dataTypeElement.Key;
                            match = true;
                            break;

                        }

                    }
                    else if (currentDataType.Type == "xs:dateTime")
                    {
                        if (currentDataType.Title == title)
                        {
                            result = dataTypeElement.Key;
                            match = true;
                            break;

                        }
                    }
                    else if (currentDataType.Type == "xs:duration")
                    {
                        if (currentDataType.Title == title)
                        {
                            result = dataTypeElement.Key;
                            match = true;
                            break;
                            

                        }

                    }

                    //kompatibilität suchen von dem Datentyp den ich anlegen möchte
                    //wenn kompatibel, dann diesen Datentyp verwenden
                    //ansonsten einen neuen Datentyp anlegen


                }


            }

            if (match == false)
            {
                DataType dataType = new DataType();
                Key key = new Key(dataType.ID, dataType.Revision);

                dataType.Description = description;
                dataType.Enumeration = enumaration;
                dataType.FractionDigits = fractionDigits;
                dataType.MaxInclusive = maxInclusive;
                dataType.MaxLength = maxLength;
                dataType.Multiple = multiple;
                dataType.Title = title;
                dataType.Type = type;
                dataType.MinInclusive = minInclusive;

                DataTypes.Add(key, dataType);
                result = key;
            }
            return result;



        }


    }
}
