package com.aoc2025;

import java.util.ArrayList;
import java.util.List;

public class SolutionB {
    private final List<String> banks;

    public SolutionB(List<String> banks) {
        this.banks = banks;
    }

    public long solve() {
        long answer = 0;

        for (String battery : banks) {
            List<Integer> batteryInts = new ArrayList<>();

            for (char c : battery.toCharArray()) {
                batteryInts.add(Character.getNumericValue(c));
            }

            long highest = findHighestCombination(batteryInts);
            answer += highest;
        }

        return answer;
    }

    private long findHighestCombination(List<Integer> batteries) {
        StringBuilder output = new StringBuilder();

        int currentHighest = Integer.MIN_VALUE;
        int previousHighestIndex = -1;

        // find the next highest number 12 times
        for (int i = 11; i >= 0; i--) {
            // find the highest number
            // while starting after the previous added digit
            // while leaving at least i enough digits over at the end
            for (int j = previousHighestIndex + 1; j < batteries.size() - i; j++) {
                if (currentHighest < batteries.get(j)) {
                    currentHighest = batteries.get(j);
                    previousHighestIndex = j;
                }
            }
            output.append(currentHighest);
            currentHighest = Integer.MIN_VALUE;
        }

        return Long.parseLong(output.toString());
    }

}
