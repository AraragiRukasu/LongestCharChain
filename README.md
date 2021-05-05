# LongestCharChain

This a solution proposed to the following problem in C#.

Given a square matrix filled with characters return the longest character chain in which a character is repeated. You need to check horizontally, vertically and diagonally.

Input Example:

```
B, B, D, A, D, E, F
B, X, C, D, D, J, K
H, Y, I, 3, D, D, 3
R, 7, O, Ñ, G, D, 2
W, N, S, P, E, 0, D
A, 9, C, D, D, E, F
B, X, D, D, D, J, K
```

Output for this given input:

```
 D, D, D, D, D
```

## Running the project

### VS

Just open the solution on the root of the repo, set the only project contained as StartUp (default) and run it with Debugger using F5 or without with Ctrl + F5.

This will build the project, open a console window, run the project and output the result on the screen.

### Console

Open windows cmd on the LongestCharChain folder and run the following command.

```
dotnet run LongestCharChain.csproj
```

This will build and run the project outputting the result in screen and returning control to the console.

## Notes

The solution was implemented on C# being the language I feel more confortable on, but given the nature of the problem this may be easier to do on a more functional laguage that provides better tools to manage data in matrixes.
