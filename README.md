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
