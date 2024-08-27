# Update this file to run the experiments for the different configurations
CLINGPATH =  'c:/Users/voslu/.vscode/extensions/ffrankreiter.answer-set-programming-language-support-0.7.1/clingo_win.exe'

THREADCOUNT = 8

REPS= [2,3,4,5,6,7,8,9,10]

#CONFIGURATIONS = ['frumpy', 'jumpy','tweety','crafty', 'handy']
CONFIGURATIONS = ['frumpy']
LPFILES = ['basic.lp', 'optimization1.lp']
DEPTHS = [1,2,3,4,5,6,7,8,9,10,11,12]



# run a commandline tool
def run_commandline_tool(tool, args, output_file):
    import subprocess
    import sys

    # run the tool
    try:
        # run the tool and capture the output in a file
        with open(output_file, "w") as output:
            subprocess.run(tool + " " + " ".join(args), stdout=output, stderr=output, check=True)
    # do not catch an exit status code 0 and 30
    except subprocess.CalledProcessError as e:
        if e.returncode != 30:
            print(f"Error: {e}")
            sys.exit(1)


# loop through the configurations consistsing of REPS, CONFIGURATIONS, LPFILES, DEPTHS
for rep in REPS:
    for configuration in CONFIGURATIONS:
        for lpfile in LPFILES:
            for depth in DEPTHS:
                print(f"Running {configuration} with {lpfile} and depth {depth} for rep {rep}")
                fname = f"output_{rep}_{configuration}_{lpfile}_{depth}.json"
                run_commandline_tool(CLINGPATH, [lpfile, "0", f"--const depth={depth}", f"-t{THREADCOUNT}", "--outf=2", f"--configuration={configuration}"], f"results/{fname}")



