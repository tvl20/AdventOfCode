package com.aoc2025;

import java.util.ArrayList;
import java.util.List;

public class SolutionA {
    private final List<Long[]> freshRangeIds;
    private final List<Long> ingredientIds;

    public SolutionA(List<String> ranges, List<String> ids) {
        freshRangeIds = new ArrayList<>();
        ingredientIds = new ArrayList<>();

        for (String range : ranges) {
            String[] edges = range.split("-");
            freshRangeIds.add(new Long[]{Long.parseLong(edges[0]), Long.parseLong(edges[1])});
        }

        for (String id : ids) {
            ingredientIds.add(Long.parseLong(id));
        }
    }

    public long solve() {
        long answer = 0;

        for (Long id : ingredientIds) {
            if (isFresh(id)) answer++;
        }

        return answer;
    }

    public boolean isFresh(long id) {
        for (Long[] range : freshRangeIds) {
            if ((range[0] < id && range[1] > id) ||
                    range[0] == id || range[1] == id) {
                return true;
            }
        }

        return false;
    }
}
