package com.aoc2025;

import java.util.List;

public class Main {
    static void main() {
        List<String> input = PuzzleInputReader.lines();
        String[] ranges = input.getFirst().split(",");

        long[][] rangePairs = new long[ranges.length][];
        for (int i = 0; i < ranges.length; i++) {
            String[] pair = ranges[i].split("-");
            rangePairs[i] = new long[]{Long.parseLong(pair[0]), Long.parseLong(pair[1])};
        }

        SolutionA solutionA = new SolutionA(rangePairs);
        long result = solutionA.solve();
        System.out.println("result of Solution A is " + result);

        SolutionB solutionB = new SolutionB(rangePairs);
        long resultB = solutionB.solve();
        System.out.println("result of Solution B is " + resultB);
    }
}
