using System;
using System.Diagnostics;
using System.Linq;

//Is it faster to do `if(variable==180){otherVar = functionInputValue;}` or is it faster to do `otherVar = cos(variable);`?
//I want `variable` to randomly be one of two different values (180, 0), instead of always 180
//ChatGPT pretty crappily generated this code on Aug23, 2023. I fixed it up so it's comparing apples to apples now instead of dinosaurs to 4D-beings


class IfVsCosTest
{
    static void Main()
    {
        //TEST RESULTS (if(),cos()):  in seconds, (5.8,6.3),(5.6,6.2),(5.9,6.3),(5.7,6.2),(6.1,6.3),(5.9,6.4),(5.6,6.4),(6.0,6.3).
        //Tests conducted with empty print statements so I could read the results without them getting overwritten.
        //If statement was faster every single test with 500M items
        //Print("."): (6.7,7.4),(6.8,7.5)

        //50M items (full error text): in millisec, (769,838),(788,773),(743,799).
        //500M items (full error text): in seconds, (35.634,19.201)VSCode,(74.534,45.873)CmdLine (didn't run more tests bc takes forever). HAD TO DO ON COMMAND LINE BC VSCODE RAN OUT OF MEMORY
        //https://stackoverflow.com/questions/71614897/vscode-crashed-reason-oom-code-536870904
        //Testing Notes: Don't move mouse while testing bc scrolling over another app steals resources. Don't type anything either for same reason. Turn off videos/music
        //I ran out of RAM on my 16GB-RAM computer
        int iterations = 500_000_000; // Adjust the number of iterations as needed. '_' acts as a visual separator (like a comma, but for code)
        Random random = new Random();

        Stopwatch stopwatch = new Stopwatch();

        int[] variableOptions = new int[] { 0, 180 };
        double funcInput_nodeDistance = random.NextDouble();    //These two variables are supposed to represent function arguments, making it important that
        double funcInput_nodeAngle    = random.NextDouble();    //  they aren't literal constants but instead are variable constants
        double[] inputArray = new double[iterations]; //Declare input array
        for (int i = 0; i < iterations; i++)
        {
            if(i%1_000==0){inputArray[i] = random.NextDouble()*-1;} //Suppose 1 in one thousand angle entries is a typo (but still a number, meaning it will compile).
            else{inputArray[i] = variableOptions[random.Next(variableOptions.Length)];} //Fill input array with valid inputs (0 and 180)
		    //inputArrayElementI = randomlySelectElementFrom_variableOptions
        }

        double[] outputArray = new double[iterations];

        // Test the if-condition operation
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
                 if(inputArray[i] == 0){  outputArray[i] = funcInput_nodeDistance;}
            else if(inputArray[i] == 180){outputArray[i] = -1*funcInput_nodeDistance;}
            else{Console.Write("The only acceptable degree inputs are 0 degrees(back-facing) and 180 degrees(front-facing), not {0}.", funcInput_nodeAngle);}
            //Inputs that aren't 0 or 180 will break this.
        }
        stopwatch.Stop();
        long ifElapsedTime = stopwatch.ElapsedMilliseconds;



        // Test the cos() operation
        stopwatch.Reset();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            if(inputArray[i] < 0){Console.Write("You accidentally typed a negative # of degrees, which can be ambiguous and/or misleading at a glance. [0,double.MaxValue) is allowed.");}
            outputArray[i] = Math.Cos(inputArray[i]);
            //Inputs that aren't 0 or 180 will NOT break this. Negative #s won't break it either BUT are horrible for anybody who has to proofread more than 5 angles
        }
        stopwatch.Stop();
        long cosElapsedTime = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"\nif-condition took {ifElapsedTime} ms");
        Console.WriteLine($"cos() operation took {cosElapsedTime} ms");
    }
}
