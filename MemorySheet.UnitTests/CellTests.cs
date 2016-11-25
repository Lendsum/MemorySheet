using Lendsum.MemorySheet;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MemorySheet.UnitTests
{
    /// <summary>
    /// Tests about cell.
    /// </summary>
    [TestFixture]
    public class CellTests
    {
        int counter = 0;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            counter = 0;
        }

        /// <summary>
        /// Test of set expression
        /// </summary>
        [TestCase]
        public void ColumnsExpression()
        {
            Table table = new Table();
            table.AddColumns(new Column[]
            {
                new Column("A"),
                new Column("B"),
                new Column("C", new SheetExpression<int>((A,B)=>DoSometing(A,B)))
            });

            table["A", 1] = 1;
            Assert.AreEqual(1, table["A", 1]);
            table["B", 1] = 2;
            Assert.AreEqual(2, table["B", 1]);

            var c = table["C", 1];
            Assert.AreEqual(3, c);
            Assert.AreEqual(1, counter);

            // check the value is not resolved again.
            c = table["C", 1];
            Assert.AreEqual(3, c);
            Assert.AreEqual(1, counter);

            table["A", 1] = 10;
            c = table["C", 1];
            Assert.AreEqual(12, c);
            Assert.AreEqual(2, counter);
        }

        /// <summary>
        /// Cells the expression.
        /// </summary>
        [TestCase]
        public void CellExpression()
        {
            Table table = new Table();
            table.AddColumns(new Column[]
            {
                new Column("A"),
                new Column("B"),
                new Column("C", new SheetExpression<int>((A_1,B)=>DoSometing(A,B)))
            });

            table.GetCell("B", 1).SetExpression<int>((A) => DoSometing(A, A));
            Assert.AreEqual(0, counter);
            table["A", 1] = 5;
            Assert.AreEqual(0, counter);
            Assert.AreEqual(15, table["C", 1]);
            Assert.AreEqual(2, counter);
        }

        /// <summary>
        /// Cells the expression.
        /// </summary>
        [TestCase]
        public void DateTests()
        {
            Table table = new Table();
            DateTime ahora = DateTime.UtcNow;
            table.AddColumns(new Column[]
            {
                new Column("A", new SheetExpression<DateTime>((A_1N)=>A_1N.AddDays(1))),
            });

            
            table["A", 1] = ahora;
            
            Assert.AreEqual(ahora.AddDays(1), table["A", 2]);
        }

        private int DoSometing(int param1, int param2)
        {
            counter++;
            return param1 + param2;
        }
    }
}
