package com.aoc2025;

import java.util.ArrayList;
import java.util.List;

public class SolutionA {
    private final List<String> banks;

    public SolutionA(List<String> banks) {
        this.banks = banks;
    }

    public long solve() {
        long answer = 0;

        for (String battery : banks) {
            List<Integer> batteryInts = new ArrayList<>();

            for (char c : battery.toCharArray()) {
                batteryInts.add(Character.getNumericValue(c));
            }

            answer += findHighestCombination(batteryInts);
        }

        return answer;
    }

    private int findHighestCombination(List<Integer> batteries) {
        String output = "";

        int highest = Integer.MIN_VALUE;
        int highestIndex = 0;

        // find the highest first number
        for (int i = 0; i < batteries.size() - 1; i++) { // we need at least one more at the end, don't take last as highest first number
            if (highest < batteries.get(i)) {
                highest = batteries.get(i);
                highestIndex = i;
            }
        }
        output += highest;

        // find the highest second number after the first for combination
        highest = Integer.MIN_VALUE;
        for (int i = highestIndex + 1; i < batteries.size(); i++) {
            if (highest < batteries.get(i)) {
                highest = batteries.get(i);
            }
        }
        output += highest;

        return Integer.parseInt(output);
    }
}
