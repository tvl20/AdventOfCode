package com.aoc2025;

import java.util.List;

public class Main {
    static void main() {
        List<String> input = PuzzleInputReader.lines();

        SolutionA solutionA = new SolutionA(input);
        long resultA = solutionA.solve();
        System.out.println("result of Solution A is " + resultA);

        SolutionB solutionB = new SolutionB(input);
        long resultB = solutionB.solve();
        System.out.println("result of Solution B is " + resultB);
    }
}
