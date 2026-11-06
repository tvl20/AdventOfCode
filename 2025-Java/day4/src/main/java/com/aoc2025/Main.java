package com.aoc2025;

public class Main {
    static void main() {
        char[][] inputGrid = PuzzleInputReader.grid();

//        SolutionA solutionA = new SolutionA(inputGrid);
//        long resultA = solutionA.solve();
//        System.out.println("result of Solution A is " + resultA);

        SolutionB solutionB = new SolutionB(inputGrid);
        long resultB = solutionB.solve();
        System.out.println("result of Solution B is " + resultB);
    }
}
