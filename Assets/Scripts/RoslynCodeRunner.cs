using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class RoslynCodeRunner : MonoBehaviour
{
    [SerializeField]
    private string[] namespaces;

    [SerializeField]
    [TextArea(5, 12)]
    private string code;

    [SerializeField]
    private UnityEvent OnRunCodeCompleted;

    [SerializeField]
    private string[] resultVars;

    [SerializeField]
    [TextArea(5, 20)]
    private string resultInfo;

    public void RunCode()
    {
        Logger.Instance.LogInfo("Executing RunCode...");
        try
        {
            ScriptState<object> result = CSharpScript.RunAsync(code, SetDefaultImports()).Result;
            foreach(string var in resultVars)
            {
                resultInfo += $"{result.GetVariable(var).Name}: {result.GetVariable(var).Value}\n";
            }

            OnRunCodeCompleted?.Invoke();
        }
        catch(Exception e)
        {
            Logger.Instance.LogError(e.Message);
        }
    }

    private ScriptOptions SetDefaultImports()
    {
        return ScriptOptions.Default
            .WithImports(namespaces.Select(n => n.Replace("using", string.Empty).Trim()))
            .AddReferences(
            // Add any assemblies here required to run your code
            typeof(MonoBehaviour).Assembly,
            typeof(Debug).Assembly);
    }
}
