package com.aoc2025;

public class SolutionA {
    private final char[][] grid;

    public SolutionA(char[][] grid) {
        this.grid = grid;
    }

    public long solve() {
        long answer = 0;

        for (int x = 0; x < grid.length; x++) {
            for (int y = 0; y < grid[x].length; y++) {
                if (getSafeCoordinate(x, y) == '@' &&
                        numbAdjacent(x, y) < 4) {
                    answer++;
                }
            }
        }

        return answer;
    }

    public int numbAdjacent(int x, int y) {
        int neighbours = 0;

        // count all '@' in a given 3x3 grid centered on the given coordinate
        for (int offsetX = -1; offsetX < 2; offsetX++) {
            for (int offsetY = -1; offsetY < 2; offsetY++) {
                if (getSafeCoordinate(x + offsetX, y + offsetY) == '@') {
                    neighbours++;
                }
            }
        }

        if (getSafeCoordinate(x, y) == '@')
            neighbours -= 1; // in case center was '@' it would have been counted, so remove it from count

        return neighbours;
    }

    public char getSafeCoordinate(int x, int y) {
        if (x < 0 || x >= grid.length) return '-';
        if (y < 0 || y >= grid[x].length) return '-';

        return grid[x][y];
    }
}
