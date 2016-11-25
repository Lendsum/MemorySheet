using Lendsum.MemorySheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemorySheetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table();
            table.AddColumns(new Column[]
            {
                new Column("A"),
                new Column("B"),
                new Column("C", new SheetExpression<int>((A,B)=>DoSometing(A,B)))
            });

            table.GetCell("B", 1).SetExpression<int>((A) => DoSometing(A, A));
            int i = 0;
            while (true)
            {
                if (i > 1000000) i = 0;
                i++;

                table["A", 1] = i;

                Console.WriteLine(table["C", 1]);
            }
        }

        static int counter = 0;
        private static int DoSometing(int param1, int param2)
        {
            counter++;
            return param1 + param2;
        }
    }
}
