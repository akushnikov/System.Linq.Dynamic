using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Linq.Dynamic.Tests
{
    internal class Foo
    {
        public Foo()
        {
            Bars = new List<Bar>();
        }

        public int Integer { get; set; }

        public string String { get; set; }

        public decimal Decimal { get; set; }

        public ICollection<Bar> Bars { get; set; }
    }

    internal class Bar
    {
        public int Integer { get; set; }

        public string String { get; set; }

        public double Double { get; set; }
    }

    internal static class FooExtensions
    {
        public static Bar GetBar(this Foo foo, int integer)
        {
            return foo.Bars.SingleOrDefault(item => item.Integer == integer);
        }

        public static bool HasBar(this Foo foo, int integer)
        {
            return foo.Bars.Any(item => item.Integer == integer);
        }

        public static double? Get(this Foo foo, int integer)
        {
            var bar = foo.GetBar(integer);
            return bar != null ? bar.Double : (double?)null;
        }
    }

    internal class FooTypeProvider : DefaultDynamicLinqCustomTypeProvider
    {
        public override HashSet<Type> GetCustomTypes()
        {
            var types = base.GetCustomTypes();
            types.Add(typeof(FooExtensions));
            return types;
        }
    }


    [TestClass]
    public class ParseLambdaTests
    {
        private Foo GetFoo()
        {
            var foo = new Foo
            {
                Integer = 1,
                String = "5",
                Decimal = 1234.56m,
                Bars = new List<Bar>
                {
                    new Bar {Integer = 1, String = "Bar#1", Double = 1.1},
                    new Bar {Integer = 2, String = "Bar#2", Double = 2.2},
                    new Bar {Integer = 3, String = "Bar#3", Double = 3.3},
                    new Bar {Integer = 4, String = "Bar#4", Double = 4.4}
                }
            };
            return foo;
        }

        [TestInitialize]
        public void Initialize()
        {
            GlobalConfig.CustomTypeProvider = new FooTypeProvider();
        }

        [TestMethod]
        public void ParserTest()
        {
            var foo = GetFoo();
            var dynamicExpression = DynamicExpression.ParseLambda<Foo, bool>("$.HasBar(1)");
            var result = dynamicExpression.Compile().Invoke(foo);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DictionaryParamsTest()
        {
            var foo = GetFoo();
            var expression = DynamicExpression.ParseLambda<Foo, bool>("$.Integer = @integer and $.String = @str",
                new Dictionary<string, object> {{"@integer", 1}, {"@str", "5"}});
            var result = expression.Compile().Invoke(foo);
            Assert.IsTrue(result);
        }
    }
}