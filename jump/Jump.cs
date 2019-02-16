using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JumpTool
{
    public static class Jump
    {
        static Type _jumpType;
        static Dictionary<string, MethodInfo> _consoleEntryPoints;
        static Dictionary<string, JumpToAttribute> _jumpAttributes;

        private static void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        private static void MapJumpTargets(Type targetType)
        {
            Dictionary<string, MethodInfo> jumpTargets = new Dictionary<string, MethodInfo>();
            Dictionary<string, JumpToAttribute> attrTargets = new Dictionary<string, JumpToAttribute>();

            var targetMethods = targetType.GetMethods()
                                            .Where(method => method.GetCustomAttributes<JumpToAttribute>()
                                                .Count() > 0);

            Log($"Found {targetMethods.Count()} methods with JumpToAttributes.");

            foreach (var method in targetMethods)
            {
                foreach (var attr in method.GetCustomAttributes<JumpToAttribute>())
                {
                    var key = attr.TargetName.ToLowerInvariant();

                    if(jumpTargets.ContainsKey(key))
                    {
                        throw new Exception($"There are multiple JumpToAttribute targets with the same name: '{key}' - Use unique names when specifying the JumpToAttribute on methods.");
                    }
                    else
                    {
                        jumpTargets.Add(key, method);
                        attrTargets.Add(key, attr);

                    }
                }
            }

            Log($"Jump :: Target Type: { targetType.FullName }");
            Log($"Jump :: Possible Options: \n\t- { string.Join("\n\t- ", jumpTargets.Keys)}");

            Jump._consoleEntryPoints = jumpTargets; // I precede with Jump to be explicit.
            Jump._jumpAttributes = attrTargets;
        }

        public static void PrintHelp()
        {
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"The following options are available to jump to: ");
            Console.WriteLine(new string('-', 50));

            foreach (var ep in Jump._consoleEntryPoints)
            {
                Console.WriteLine($"\t{ep.Key} - {ep.Value.ToString().PadRight(30, ' ')}\t{_jumpAttributes[ep.Key].ShortDescription}");
                Console.WriteLine();
            }

            Console.WriteLine($"");
        }
        
        private static void ExecuteJumpTarget(string[] args, Type targetType)
        {
            switch (args.Length)
            {
                case 0:
                    PrintHelp();
                    break;
                case 1:
                    if (Jump._consoleEntryPoints
                            .ContainsKey(args[0]))
                    {
                        Jump._consoleEntryPoints[args[0]]
                            .Invoke(null, null);
                    }
                    else
                    {
                        PrintHelp();
                        Console.WriteLine($"Could not find a matching jump target with name: {targetType.Name}");
                    }
                    break;
                default: // 1+ args
                    if (Jump._consoleEntryPoints
                            .ContainsKey(args[0]))
                    {
                        var targetMethodInfo        = Jump._consoleEntryPoints[args[0]];
                        var targetMethodParameters  = targetMethodInfo.GetParameters();
                        var targetArgs              = args.Skip(1).ToArray();
                        var targetParameterCount    = targetMethodParameters.Count();
                        var expectedArgCount        = targetParameterCount - targetMethodParameters.Where(p => p.HasDefaultValue).Count();

                        if (targetArgs.Count() < expectedArgCount)
                        {
                            throw new Exception("There is a mismatch in the number of parameters you have provided, and the number of parameters to bind to.\n" + 
                                $"You provided {targetArgs.Count()} argument(s), and the target method has {expectedArgCount} non-default parameters.");
                        }

                        var convertedArgs = new object[targetParameterCount];

                        // convert our args to what we need for the target method.
                        for(int i = 0; i < targetParameterCount; i++)
                        {
                            if(i < targetArgs.Length)
                            {
                                convertedArgs[i] = Convert.ChangeType(targetArgs[i], targetMethodParameters[i].ParameterType);
                            }
                            else if(i >= targetArgs.Length && targetMethodParameters[i].HasDefaultValue)
                            {
                                convertedArgs[i] = targetMethodParameters[i].DefaultValue;
                            }
                        }
                        
                        Jump._consoleEntryPoints[args[0]]
                            .Invoke(null, convertedArgs);
                    }
                    else
                    {
                        PrintHelp();
                        Console.WriteLine($"Could not find a matching jump target with name: {targetType.Name}");
                    }
                    break;
            }
        }

        public static void Start(string[] args, Type targetType)
        {
            Jump._jumpType = targetType;

            MapJumpTargets(targetType);
            ExecuteJumpTarget(args, targetType);
        }
    }
}
