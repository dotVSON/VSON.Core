using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using VSON.Core.Diff;

namespace VSON.Core
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
            //Program.PrivateMethod();   
        }

        
    }
}