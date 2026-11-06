package com.aoc2025;

import java.util.List;

public class Main {
    static void main() {
        List<List<String>> inputLists = PuzzleInputReader.multiList();

//        SolutionA solutionA = new SolutionA(inputLists.get(0), inputLists.get(1));
//        long result = solutionA.solve();
//        System.out.println("result of Solution A is " + result);

        SolutionB solutionB = new SolutionB(inputLists.get(0));
        long resultB = solutionB.solve();
        System.out.println("result of Solution B is " + resultB);
    }
}
