using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

var root = Path.GetFullPath(args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "../../../../../"));
var app = Path.Combine(root, "src", "OmenCoreApp");
var output = Path.Combine(root, "LOCALIZATION-ZH-CN-CSHARP-SYNTAX.csv");
var files = Directory.EnumerateFiles(app, "*.cs", SearchOption.AllDirectories)
    .Where(p => !p.Split(Path.DirectorySeparatorChar).Any(x => x is "obj" or "bin" or "runtimes"))
    .OrderBy(p => p, StringComparer.Ordinal).ToArray();
var rows = new List<string>();
var occurrence = new Dictionary<string, int>(StringComparer.Ordinal);
string Csv(string value) => "\"" + value.Replace("\"", "\"\"").Replace("\r", "\\r").Replace("\n", "\\n") + "\"";
foreach (var file in files)
{
    var text = File.ReadAllText(file);
    var tree = CSharpSyntaxTree.ParseText(text, path: file);
    var relative = Path.GetRelativePath(root, file).Replace('\\', '/');
    foreach (var node in tree.GetRoot().DescendantNodes().Where(n =>
        n is LiteralExpressionSyntax literal && literal.IsKind(SyntaxKind.StringLiteralExpression) || n is InterpolatedStringExpressionSyntax))
    {
        var value = node.ToString();
        if (!value.Any(char.IsLetter)) continue;
        var member = node.Ancestors().OfType<MemberDeclarationSyntax>().FirstOrDefault();
        var context = member switch
        {
            MethodDeclarationSyntax m => m.Identifier.ValueText,
            ConstructorDeclarationSyntax c => c.Identifier.ValueText,
            PropertyDeclarationSyntax p => p.Identifier.ValueText,
            FieldDeclarationSyntax f => f.Declaration.Variables.FirstOrDefault()?.Identifier.ValueText ?? "field",
            _ => "unknown"
        };
        var type = node.Ancestors().OfType<TypeDeclarationSyntax>().FirstOrDefault()?.Identifier.ValueText ?? "unknown";
        var invocation = node.Ancestors().OfType<InvocationExpressionSyntax>().FirstOrDefault()?.Expression.ToString() ?? "";
        var assignment = node.Ancestors().OfType<AssignmentExpressionSyntax>().FirstOrDefault()?.Left.ToString() ?? "";
        var category = "E";
        var reason = "仅语法候选，用户可见性和调用链待核实";
        if (invocation.Contains("MessageBox.Show", StringComparison.Ordinal)) { reason = "对话框参数，需确认具体用途和本地化"; }
        else if (invocation.StartsWith("_logging.", StringComparison.Ordinal) || invocation.StartsWith("_logger.", StringComparison.Ordinal)) { category = "C"; reason = "日志调用；仍需确认参数是否同时被其他路径使用"; }
        var key = string.Join("\0", relative, type, context, node.Kind().ToString(), value);
        occurrence.TryGetValue(key, out var count); occurrence[key] = ++count;
        var id = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(key + "\0" + count)))[..20].ToLowerInvariant();
        var line = node.GetLocation().GetLineSpan().StartLinePosition.Line + 1;
        rows.Add(string.Join(",", new[] { id, relative, line.ToString(), type, context, node.Kind().ToString(), value, invocation, assignment, category, reason }.Select(Csv)));
    }
}
using var writer = new StreamWriter(output, false, new UTF8Encoding(true));
writer.WriteLine("stable_id,path,line_at_scan,type,member,syntax,original,invocation,assignment,category,reason");
foreach (var row in rows) writer.WriteLine(row);
Console.WriteLine($"{files.Length} C# files, {rows.Count} syntax candidates: {Path.GetFileName(output)}");
