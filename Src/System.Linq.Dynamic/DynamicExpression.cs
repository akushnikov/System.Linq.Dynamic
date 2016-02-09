using System.Collections.Generic;
using System.Linq.Expressions;


namespace System.Linq.Dynamic
{
    public static class DynamicExpression
    {
        //Commented Out as It's never used.
        //public static Expression Parse(Type resultType, string expression, params object[] values)
        //{
        //    ExpressionParser parser = new ExpressionParser(null, expression, values);
        //    return parser.Parse(resultType);
        //}

        public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
        {
            return ParseLambda(new [] { Expression.Parameter(itType, "") }, resultType, expression, values);
        }

        public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
        {
            ExpressionParser parser = new ExpressionParser(parameters, expression, values);
            return Expression.Lambda(parser.Parse(resultType), parameters);
        }

        /// <summary>
        /// Parses lambda expression from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Expression<Func<T, TResult>> ParseLambda<T, TResult>(string expression, params object[] values)
        {
            return (Expression<Func<T, TResult>>)ParseLambda(typeof(T), typeof(TResult), expression, values);
        }

        //Commented Out as It's never used.
        //public static Type CreateClass(params DynamicProperty[] properties)
        //{
        //    return ClassFactory.Instance.GetDynamicClass(properties);
        //}

        public static Type CreateClass(IEnumerable<DynamicProperty> properties)
        {
            return ClassFactory.Instance.GetDynamicClass(properties);
        }
    }

}
