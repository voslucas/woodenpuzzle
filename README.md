# woodenpuzzle

![Wooden puzzel](images/IMG_6089.jpg)

This is an Answer Set Programming (ASP) solution to a wooden puzzle as shown in the image above. ASP is a declartive programming language to model your problem and let a solver find solutions. We use Clingo as our solver 


# Run
To run the depth experiment, as described in our paper, you can run: `clingo.exe 0 -t4 basic.lp --const depth=2`

The `-t4` means 4 threads. de `0` means to find all solutions. 

# Structure

- `basic.lp` and `optimmized.lp` form two implementations in ASP.

- `experiment.py` and `process.py` are two Python scripts that executes our two implementations for different Clingo configuration, in different depths and process the results. 

- The `dotnet` folder contains an imperative programming solution written in `C#` 


