using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace VSON.Core.Serialization
{
    public class Serializer
    {
        public static JsonSerializerSettings SerializationSettings
        { 
            get => new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            };
        }

        public static object Deserialize(string json)
        {
            PointF newPivot = new PointF(500, 500);
            JObject jObject = JsonConvert.DeserializeObject(json) as JObject;
            var type = jObject.Type;
          
            


            return null;
        }



        public static string Serialize(object instanceObject)
        {
            Dictionary<string, object> objectTable = new Dictionary<string, object>();
            
            PropertyInfo[] properties = instanceObject.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (true || property.CanWrite || property.SetMethod != null)
                {
                    var value = property.GetValue(instanceObject);
                    if (value != null)
                    {
                        string strVal = JsonConvert.SerializeObject(value, Formatting.Indented, SerializationSettings);
                        objectTable.Add(property.Name, strVal);
                    }
                    
                }
            }
            return JsonConvert.SerializeObject(objectTable, Formatting.Indented, SerializationSettings);

            /*JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            Table<string, Type, object> table = new Table<string, Type, object>();
            PropertyInfo[] properties = instanceObject.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.SetMethod != null && propertyInfo.SetMethod.IsPublic)
                {
                    var value = propertyInfo.GetValue(instanceObject);
                    string strVal = JsonConvert.SerializeObject(value, Formatting.Indented, settings);
                    table.Add(propertyInfo.Name, propertyInfo.PropertyType, strVal);
                }
            }
            return JsonConvert.SerializeObject(table, Formatting.Indented, settings);*/
        }

        /*public static Table<string, Type, object> Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Table<string, Type, object>>(json);

        }*/
    }
}
