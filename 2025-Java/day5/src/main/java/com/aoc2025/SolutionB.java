package com.aoc2025;

import java.util.ArrayList;
import java.util.List;

public class SolutionB {
    private final List<Long[]> freshRangeIds;

    public SolutionB(List<String> ranges) {
        freshRangeIds = new ArrayList<>();

        for (String range : ranges) {
            String[] edges = range.split("-");
            freshRangeIds.add(new Long[]{Long.parseLong(edges[0]), Long.parseLong(edges[1])});
        }
    }

    public long solve() {
        List<Long[]> condensedList = new ArrayList<>();


        return -1;

    }

    private void compressRangeList() {

        // loop over everything to validate no collisions
        for (int i = 0; i < freshRangeIds.size(); i++) {
            // freshRangeIds[i] is the one being checked against the list for potential collisions

            // check for collision on the range
            for (Long[] potentialCollision : freshRangeIds) {
                boolean lowerCollision = collidesWithRange(potentialCollision, freshRangeIds.get(i)[0]);
                boolean upperCollision = collidesWithRange(potentialCollision, freshRangeIds.get(i)[1]);
                // check if freshRangeIds[i] is entirely encapsulated by potential collision
                boolean engulfedIn = lowerCollision && upperCollision;
                // check if freshRangeIds[i] is entirely encapsulating potential collision
            }
        }

        // if collision found
        // fix the collision
        // reset validation loop
    }

    private boolean collidesWithRange(Long[] range, long id) {
        if ((range[0] < id && range[1] > id) ||
                range[0] == id || range[1] == id) {
            return true;
        }

        return false;
    }
}
