using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq;
using UnityEngine;

public class TestRoslyn : MonoBehaviour
{
    [SerializeField]
    private string[] namespaces;

    [SerializeField]
    [TextArea(5, 20)]
    private string code = "Pow(2,5)";

    [SerializeField]
    private string resultVar = "r";

    [SerializeField]
    private string resultInfo;

    // Start is called before the first frame update
    void Update()
    {
        ScriptState<object> result = null;

        if (namespaces.Length == 0)
        {
            result = CSharpScript.RunAsync(code).Result;
        }
        else
        {
            result = CSharpScript.RunAsync(code, SetImports()).Result;
        }
        resultInfo = result.GetVariable(resultVar).Value.ToString();
        Debug.Log(result);
    }

    private ScriptOptions SetImports()
    {
        return ScriptOptions.Default
                     .WithImports(namespaces.Select(n => n.Replace("using", string.Empty).Trim()));
    }
}
