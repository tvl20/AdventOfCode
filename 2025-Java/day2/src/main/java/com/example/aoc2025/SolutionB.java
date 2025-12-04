package com.example.aoc2025;

public class SolutionB {
    private final long[][] ranges;

    public SolutionB(long[][] ranges) {
        this.ranges = ranges;
    }

    public long solve() {
        long answer = 0;

        for (long[] range : ranges) {
            answer += calculateFaultyCodesBruteforce(range);
        }

        return answer;
    }

    public long calculateFaultyCodesBruteforce(long[] pair) {
        long smallest = Math.min(pair[0], pair[1]);
        long biggest = Math.max(pair[0], pair[1]);
        long faults = 0;

        for (long i = smallest; i <= biggest; i++) {
            String number = i + "";

            if (!isValidID(number)) faults += Long.parseLong(number);
        }

        return faults;
    }

    public boolean isValidID(String id) {
        for (int i = 1; i <= Math.ceilDiv(id.length(), 2); i++) { // only loop until half as the biggest pattern is half the string
            String pattern = id.substring(0, i);
            String changed = id.replaceAll(pattern, ""); //remove all occurrences of pattern
            if (changed.isEmpty()) return false; // if string is empty everything follows the same pattern
        }
        return true;
    }
}
