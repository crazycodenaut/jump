Early conceptual version.

## How it Works
Apply attributes like this:

```C# 
[JumpTo("aliasname", "Description goes here")]
````

to static methods in your Program class that you'd like to begin executing logic in. Then, when the end-user executes runs your program, like

` YouProgram.exe aliasname arg1 arg2 arg3 ` 

Then it will automatically jump to that static method, bind the arguments appropriately, and begin executing.

See the JumpTest code in the repository for additional examples. Keep in mind, however, that this is a conceptual release. 

### Simple Example (From JumpTest sample in repository)

This,

```C#
[JumpTo("Entrypoint2", "A second entry point.")]
public static void Ep2(int paramA, double paramB)
{
    WriteLine($"Episode 2: {paramA} {paramB}");
}
```

is executed by,
`JumpTest.exe entrypoint2 0 0.3` 

and will print:
`Episode 2: 0 0.3`

This example is straightforward but I think there is promise. In order to begin using it there is a single line of code required in your static Main method (or really, whatever method you have declared as your entry point).

```C# 
static void Main(string[] args)
{
    Start(args, typeof(Program));
}
```

In other words, just pass the args and the static class that you're keeping the method attributes in and the API takes care of the rest. 
