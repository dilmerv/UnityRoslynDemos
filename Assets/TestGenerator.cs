using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace ExampleSourceGenerator
{
    [Generator]
    public class ExampleSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            System.Console.WriteLine(System.DateTime.Now.ToString());

            var sourceBuilder = new StringBuilder(
            @"
            using System;
            namespace ExampleSourceGenerated
            {
                public static class ExampleSourceGenerated
                {
                    public static string GetTestText() 
                    {
                        return ""This is from source generator ");

            sourceBuilder.Append(System.DateTime.Now.ToString());

            sourceBuilder.Append(
                @""";
                    }
    }
}
");

            context.AddSource("exampleSourceGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context) { }
    }
}