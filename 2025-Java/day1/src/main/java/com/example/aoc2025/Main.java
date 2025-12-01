package com.example.aoc2025;

import java.util.List;

public class Main {
    public static List<String> puzzleInput;

    static void main() {
        puzzleInput = PuzzleInputReader.lines();

//        SolutionA a = new SolutionA(puzzleInput);
//        a.Solve();

        SolutionB b = new SolutionB(puzzleInput);
        b.solve();
    }
}
