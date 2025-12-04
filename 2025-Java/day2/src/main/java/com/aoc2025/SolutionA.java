package com.aoc2025;

public class SolutionA {
    private final long[][] ranges;

    public SolutionA(long[][] ranges) {
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
            if ((number.length() & 1) != 0) continue; // if its an odd number of numbers long, skip as it's always valid
            String firstHalf = number.substring(0, number.length() / 2);
            String secondHalf = number.substring(number.length() / 2);
            if (firstHalf.equals(secondHalf)) faults += i;
        }

        return faults;
    }
}
