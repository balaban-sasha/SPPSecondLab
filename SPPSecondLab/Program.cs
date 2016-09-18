using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SPPSecondLab
{
    
        public class Program
    {
        public static void Main()
        {
            int deleagateNum = 10;
            Random random = new Random();
            WeakDelegate weakDelegate = new WeakDelegate(deleagateNum);

            object[] DataName;
            GC.Collect(0);
            
            for (int i = 0; i < weakDelegate.Count; i++)
            {
                int index = random.Next(weakDelegate.Count);
                
                DataName = weakDelegate[index].GetData;
            }
            weakDelegate.IsAlive(deleagateNum);
            Console.ReadKey();
        }

        static public void PrintArray(params object[] parametres)
        {
            foreach (var o in parametres)
                Console.WriteLine("{0} ", o);
        }
    }
    public class WeakDelegate
    {
        static Dictionary<int, WeakReference> weakDelegate;
        

        public WeakDelegate(int count)
        {
            weakDelegate = new Dictionary<int, WeakReference>();
            
            for (int i = 0; i < count; i++)
            {
                weakDelegate.Add(i, new WeakReference(new Data(i,i+1,i-1,"hallo"+i), false));
            }
        }
        public int Count
        {
            get { return weakDelegate.Count; }
        }
        public void IsAlive(int count)
        {
            for (int i = 0; i < count; i++)
                Console.WriteLine("{0}  ", weakDelegate[i].IsAlive);
        }
        
       public Data this[int index]
        {
            get
            {
                Data d = weakDelegate[index].Target as Data;
                if (d == null)
                {
                    Console.WriteLine("Regenerate object at {0}: Yes", index);
                    d = new Data(index,index+1,index+2,index-1);
                    weakDelegate[index].Target = d;
                }
                else
                {
                    Console.WriteLine("Regenerate object at {0}: No", index);
                }

                return d;
            }
        }
    }
    
    public class Data
    {
        private object[] data;

        public Data(params object[] tempData)
        {
            foreach (var o in tempData)
                data = tempData;
        }
        
        public object[] GetData
        {
            get { return data; }
        }
    }
}
