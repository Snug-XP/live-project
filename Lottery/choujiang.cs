using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class choujiang
    {

        int h_act_num = 5;
        int m_act_num = 3;
        int l_act_num = 2;
        int totalnum;
        List<T> pList = new List<T>();
        int[] array = new int[10];


        public List<T> GetpList<T>(List<T> totalpeople)
        {

            sort(totalpeople);
            return outputList;
        }
        public static List<T> GetRandomList<T>(List<T> inputList)
        {
        for (int i = 0; i < 10; i++)
        {

         }
    //Copy to a array
        T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);
            
            //Add range
            List<T> copyList = new List<T>();
            copyList.AddRange(copyArray);

            //Set outputList and random
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);

            while (copyList.Count > 0)
            {
               //Select an index and item
                int rdIndex = rd.Next(0, copyList.Count - 1);
                T remove = copyList[rdIndex];
    
                //remove it from copyList and add it to output
                copyList.Remove(remove);
                outputList.Add(remove);
             }
            return outputList;
}          
    }
}
