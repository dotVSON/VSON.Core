using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using VSON.Diff;

namespace VSON
{
    internal class Program
    {
        public static bool AttributesChanged(VsonComponent componentA, VsonComponent componentB)
        {
            if(componentA.NickName != componentB.NickName) { return false; }
            else if(componentA.Message != componentB.Message) { return false; }
            else if(componentA.Size != componentB.Size) { return false; }

            // Check IO Params

            return true;
        }

        public static VsonDiffState CompareChanges(VsonComponent componentA, VsonComponent componentB)
        {
            VsonDiffState state = VsonDiffState.None;

            // Check Class Type
            if(componentA.ComponentGuid != componentB.ComponentGuid)
            {
                return VsonDiffState.Undefined;
            }

            // Check Position
            if (componentA.Pivot != componentB.Pivot)
            {
                state = VsonDiffState.Moved;
            }

            // Check Attributes
            if (AttributesChanged(componentA, componentB) == false)
            {
                state = VsonDiffState.Modified;
            }

            return state;
        }


        static void Main(string[] args)
        {
            #region Inputs
            string
                jsonA = "{\r\n      \"ComponentType\": 2,\r\n      \"Type\": \"MathComponents.OperatorComponents.Component_VariableAddition\",\r\n      \"ComponentGuid\": \"a0d62394-a118-422d-abb3-6af115c75b25\",\r\n      \"InstanceGuid\": \"67570adc-9324-4f70-9b84-513ea6c70afa\",\r\n      \"Name\": \"Addition\",\r\n      \"NickName\": \"Addition\",\r\n      \"Message\": null,\r\n      \"Hidden\": false,\r\n      \"Locked\": false,\r\n      \"Pivot\": {\r\n        \"IsEmpty\": false,\r\n        \"X\": 653.0,\r\n        \"Y\": 347.0\r\n      },\r\n      \"Bounds\": {\r\n        \"Location\": {\r\n          \"IsEmpty\": false,\r\n          \"X\": 612.0,\r\n          \"Y\": 325.0\r\n        },\r\n        \"Size\": \"88, 44\",\r\n        \"X\": 612.0,\r\n        \"Y\": 325.0,\r\n        \"Width\": 88.0,\r\n        \"Height\": 44.0,\r\n        \"Left\": 612.0,\r\n        \"Top\": 325.0,\r\n        \"Right\": 710.0,\r\n        \"Bottom\": 369.0,\r\n        \"IsEmpty\": false\r\n      },\r\n      \"Size\": \"88, 44\",\r\n      \"InputParams\": [\r\n        {\r\n          \"ComponentType\": 1,\r\n          \"Type\": \"Grasshopper.Kernel.Parameters.Param_GenericObject\",\r\n          \"ComponentGuid\": \"8ec86459-bf01-4409-baee-174d0d2b13d0\",\r\n          \"InstanceGuid\": \"85fe4a6f-4de1-4fac-bc05-7a1dc73ed9bd\",\r\n          \"Name\": \"A\",\r\n          \"NickName\": \"A\",\r\n          \"Message\": null,\r\n          \"Hidden\": false,\r\n          \"Locked\": false,\r\n          \"Pivot\": {\r\n            \"IsEmpty\": false,\r\n            \"X\": 632.5,\r\n            \"Y\": 337.0\r\n          },\r\n          \"Bounds\": {\r\n            \"Location\": {\r\n              \"IsEmpty\": false,\r\n              \"X\": 624.0,\r\n              \"Y\": 327.0\r\n            },\r\n            \"Size\": \"14, 20\",\r\n            \"X\": 624.0,\r\n            \"Y\": 327.0,\r\n            \"Width\": 14.0,\r\n            \"Height\": 20.0,\r\n            \"Left\": 624.0,\r\n            \"Top\": 327.0,\r\n            \"Right\": 638.0,\r\n            \"Bottom\": 347.0,\r\n            \"IsEmpty\": false\r\n          },\r\n          \"Size\": \"14, 20\",\r\n          \"InputParams\": [],\r\n          \"OutputParams\": []\r\n        },\r\n        {\r\n          \"ComponentType\": 1,\r\n          \"Type\": \"Grasshopper.Kernel.Parameters.Param_GenericObject\",\r\n          \"ComponentGuid\": \"8ec86459-bf01-4409-baee-174d0d2b13d0\",\r\n          \"InstanceGuid\": \"51a3e053-27a8-48be-8cd8-e7a2b1a80f02\",\r\n          \"Name\": \"B\",\r\n          \"NickName\": \"B\",\r\n          \"Message\": null,\r\n          \"Hidden\": false,\r\n          \"Locked\": false,\r\n          \"Pivot\": {\r\n            \"IsEmpty\": false,\r\n            \"X\": 632.5,\r\n            \"Y\": 357.0\r\n          },\r\n          \"Bounds\": {\r\n            \"Location\": {\r\n              \"IsEmpty\": false,\r\n              \"X\": 624.0,\r\n              \"Y\": 347.0\r\n            },\r\n            \"Size\": \"14, 20\",\r\n            \"X\": 624.0,\r\n            \"Y\": 347.0,\r\n            \"Width\": 14.0,\r\n            \"Height\": 20.0,\r\n            \"Left\": 624.0,\r\n            \"Top\": 347.0,\r\n            \"Right\": 638.0,\r\n            \"Bottom\": 367.0,\r\n            \"IsEmpty\": false\r\n          },\r\n          \"Size\": \"14, 20\",\r\n          \"InputParams\": [],\r\n          \"OutputParams\": []\r\n        }\r\n      ],\r\n      \"OutputParams\": [\r\n        {\r\n          \"ComponentType\": 1,\r\n          \"Type\": \"Grasshopper.Kernel.Parameters.Param_GenericObject\",\r\n          \"ComponentGuid\": \"8ec86459-bf01-4409-baee-174d0d2b13d0\",\r\n          \"InstanceGuid\": \"7bf93a2f-2b05-450a-9e70-ed151cb52d09\",\r\n          \"Name\": \"Result\",\r\n          \"NickName\": \"Result\",\r\n          \"Message\": null,\r\n          \"Hidden\": false,\r\n          \"Locked\": false,\r\n          \"Pivot\": {\r\n            \"IsEmpty\": false,\r\n            \"X\": 688.0,\r\n            \"Y\": 347.0\r\n          },\r\n          \"Bounds\": {\r\n            \"Location\": {\r\n              \"IsEmpty\": false,\r\n              \"X\": 668.0,\r\n              \"Y\": 327.0\r\n            },\r\n            \"Size\": \"40, 40\",\r\n            \"X\": 668.0,\r\n            \"Y\": 327.0,\r\n            \"Width\": 40.0,\r\n            \"Height\": 40.0,\r\n            \"Left\": 668.0,\r\n            \"Top\": 327.0,\r\n            \"Right\": 708.0,\r\n            \"Bottom\": 367.0,\r\n            \"IsEmpty\": false\r\n          },\r\n          \"Size\": \"40, 40\",\r\n          \"InputParams\": [],\r\n          \"OutputParams\": []\r\n        }\r\n      ]\r\n    }",

                jsonB = "{\r\n      \"ComponentType\": 2,\r\n      \"Type\": \"MathComponents.OperatorComponents.Component_VariableAddition\",\r\n      \"ComponentGuid\": \"a0d62394-a118-422d-abb3-6af115c75b25\",\r\n      \"InstanceGuid\": \"67570adc-9324-4f70-9b84-513ea6c70afa\",\r\n      \"Name\": \"Addition\",\r\n      \"NickName\": \"Addition\",\r\n      \"Message\": null,\r\n      \"Hidden\": false,\r\n      \"Locked\": false,\r\n      \"Pivot\": {\r\n        \"IsEmpty\": false,\r\n        \"X\": 653.0,\r\n        \"Y\": 347.0\r\n      },\r\n      \"Bounds\": {\r\n        \"Location\": {\r\n          \"IsEmpty\": false,\r\n          \"X\": 622.0,\r\n          \"Y\": 325.0\r\n        },\r\n        \"Size\": \"88, 44\",\r\n        \"X\": 622.0,\r\n        \"Y\": 325.0,\r\n        \"Width\": 88.0,\r\n        \"Height\": 44.0,\r\n        \"Left\": 622.0,\r\n        \"Top\": 325.0,\r\n        \"Right\": 710.0,\r\n        \"Bottom\": 369.0,\r\n        \"IsEmpty\": false\r\n      },\r\n      \"Size\": \"88, 44\",\r\n      \"InputParams\": [\r\n        {\r\n          \"ComponentType\": 1,\r\n          \"Type\": \"Grasshopper.Kernel.Parameters.Param_GenericObject\",\r\n          \"ComponentGuid\": \"8ec86459-bf01-4409-baee-174d0d2b13d0\",\r\n          \"InstanceGuid\": \"85fe4a6f-4de1-4fac-bc05-7a1dc73ed9bd\",\r\n          \"Name\": \"A\",\r\n          \"NickName\": \"A\",\r\n          \"Message\": null,\r\n          \"Hidden\": false,\r\n          \"Locked\": false,\r\n          \"Pivot\": {\r\n            \"IsEmpty\": false,\r\n            \"X\": 632.5,\r\n            \"Y\": 337.0\r\n          },\r\n          \"Bounds\": {\r\n            \"Location\": {\r\n              \"IsEmpty\": false,\r\n              \"X\": 624.0,\r\n              \"Y\": 327.0\r\n            },\r\n            \"Size\": \"14, 20\",\r\n            \"X\": 624.0,\r\n            \"Y\": 327.0,\r\n            \"Width\": 14.0,\r\n            \"Height\": 20.0,\r\n            \"Left\": 624.0,\r\n            \"Top\": 327.0,\r\n            \"Right\": 638.0,\r\n            \"Bottom\": 347.0,\r\n            \"IsEmpty\": false\r\n          },\r\n          \"Size\": \"14, 20\",\r\n          \"InputParams\": [],\r\n          \"OutputParams\": []\r\n        },\r\n        {\r\n          \"ComponentType\": 1,\r\n          \"Type\": \"Grasshopper.Kernel.Parameters.Param_GenericObject\",\r\n          \"ComponentGuid\": \"8ec86459-bf01-4409-baee-174d0d2b13d0\",\r\n          \"InstanceGuid\": \"51a3e053-27a8-48be-8cd8-e7a2b1a80f02\",\r\n          \"Name\": \"B\",\r\n          \"NickName\": \"B\",\r\n          \"Message\": null,\r\n          \"Hidden\": false,\r\n          \"Locked\": false,\r\n          \"Pivot\": {\r\n            \"IsEmpty\": false,\r\n            \"X\": 632.5,\r\n            \"Y\": 357.0\r\n          },\r\n          \"Bounds\": {\r\n            \"Location\": {\r\n              \"IsEmpty\": false,\r\n              \"X\": 624.0,\r\n              \"Y\": 347.0\r\n            },\r\n            \"Size\": \"14, 20\",\r\n            \"X\": 624.0,\r\n            \"Y\": 347.0,\r\n            \"Width\": 14.0,\r\n            \"Height\": 20.0,\r\n            \"Left\": 624.0,\r\n            \"Top\": 347.0,\r\n            \"Right\": 638.0,\r\n            \"Bottom\": 367.0,\r\n            \"IsEmpty\": false\r\n          },\r\n          \"Size\": \"14, 20\",\r\n          \"InputParams\": [],\r\n          \"OutputParams\": []\r\n        }\r\n      ],\r\n      \"OutputParams\": [\r\n        {\r\n          \"ComponentType\": 1,\r\n          \"Type\": \"Grasshopper.Kernel.Parameters.Param_GenericObject\",\r\n          \"ComponentGuid\": \"8ec86459-bf01-4409-baee-174d0d2b13d0\",\r\n          \"InstanceGuid\": \"7bf93a2f-2b05-450a-9e70-ed151cb52d09\",\r\n          \"Name\": \"Result\",\r\n          \"NickName\": \"Result\",\r\n          \"Message\": null,\r\n          \"Hidden\": false,\r\n          \"Locked\": false,\r\n          \"Pivot\": {\r\n            \"IsEmpty\": false,\r\n            \"X\": 688.0,\r\n            \"Y\": 347.0\r\n          },\r\n          \"Bounds\": {\r\n            \"Location\": {\r\n              \"IsEmpty\": false,\r\n              \"X\": 668.0,\r\n              \"Y\": 327.0\r\n            },\r\n            \"Size\": \"40, 40\",\r\n            \"X\": 668.0,\r\n            \"Y\": 327.0,\r\n            \"Width\": 40.0,\r\n            \"Height\": 40.0,\r\n            \"Left\": 668.0,\r\n            \"Top\": 327.0,\r\n            \"Right\": 708.0,\r\n            \"Bottom\": 367.0,\r\n            \"IsEmpty\": false\r\n          },\r\n          \"Size\": \"40, 40\",\r\n          \"InputParams\": [],\r\n          \"OutputParams\": []\r\n        }\r\n      ]\r\n    }";

            #endregion Inputs

            /*VsonComponent componentA = VsonComponent.Deserialze<VsonComponent>(jsonA);
            VsonComponent componentB = VsonComponent.Deserialze<VsonComponent>(jsonB);

            componentA.Pivot = new System.Drawing.PointF(100, 100);
            componentA.Message = "Message";

            Console.WriteLine($"Comparing \"{componentA.Name}\" and \"{componentB.Name}\"");
            Console.WriteLine($"Changes: {CompareChanges(componentA, componentB)}");*/


            /*VsonDocument documentA = VsonDocument.DeserializeFromFile<VsonDocument>(@"A:\GitHub\VSON\.local\Trials\Sample_01_SerializeAdditionComponent.vson");
            VsonDocument documentB = VsonDocument.DeserializeFromFile<VsonDocument>(@"A:\GitHub\VSON\.local\Trials\Sample_02_SerializeAdditionComponent.vson");

            DocumentComparer comparer = new DocumentComparer(documentA, documentB);
            List<DiffChange> changes = comparer.Compare();

            void SetGreen()
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            void SetRed()
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            void SetYellow()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            void SetBlue()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            void SetDefault()
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            foreach (DiffChange change in changes)
            {
                if (change.State == VsonDiffState.None) SetDefault();
                else if (change.State == VsonDiffState.Added) SetGreen();
                else if (change.State == VsonDiffState.Removed) SetRed();
                else if (change.State == VsonDiffState.Modified) SetYellow();
                else if (change.State == VsonDiffState.Moved) SetBlue();

                string comment = $"{change.State} : {change.Attribute}";
                Console.WriteLine(comment);

                SetDefault();
            }*/

            VsonDocument documentA = VsonDocument.DeserializeFromFile<VsonDocument>(@"A:\GitHub\VSON\.local\Trials\Sample_01_SerializeAdditionComponent.vson");
            Console.Write(documentA.GetHashCode());

            Console.ReadKey();
        }
    }
}