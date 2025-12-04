package com.example.aoc2025;

import java.util.List;

public class SolutionB {
    private final List<String> INPUT;

    private int currentRotation = 50;
    private int pointsAtZero = 0;

    public SolutionB(List<String> INPUT) {
        this.INPUT = INPUT;
    }

    public void solve() {
        for (String rotation : INPUT) {
            rotate(rotation);
        }

        System.out.println("Final rotation for solution B: ");
        System.out.println(pointsAtZero);
    }

    private void rotate(String rotation) {
        System.out.print(currentRotation);
        int degrees = removeFullTurns(rotation);

        if (rotation.startsWith("R")) {
            applyRotation(degrees);
        } else if (rotation.startsWith("L")) {
            applyRotation(-degrees);
        }
    }

    private void applyRotation(int rotation) {
        boolean startedAtZero = currentRotation == 0;
        currentRotation += rotation;

        if (currentRotation < 0) {
            System.out.println("rolling forward " + currentRotation + " as " + (currentRotation - rotation) + " was moved by " + rotation);
            currentRotation += 100;
            // only add a point if we passed 0, not if we landed on it or started from it
            if (!startedAtZero && currentRotation != 0) {
                pointsAtZero++;
            }
        } else if (currentRotation > 99) {
            System.out.println("rolling forward " + currentRotation + " as " + (currentRotation - rotation) + " was moved by " + rotation);
            currentRotation -= 100;
            // only add a point if we passed 0, not if we landed on it or started from it
            if (!startedAtZero && currentRotation != 0) {
                pointsAtZero++;
            }
        }

        // check if ending on zero if so add a point
        if (currentRotation == 0) {
            System.out.println(" ending on 0, numb " + pointsAtZero);
            pointsAtZero++;
        }
    }

    private int removeFullTurns(String input) {
        String degrees = input.substring(1);
        int degreesInt = Integer.parseInt(degrees);

        int fullRotations = degreesInt / 100;
        if (fullRotations > 0) {
            pointsAtZero += fullRotations;
            System.out.println("adding full rotations " + fullRotations);
        }

        // remove full rotations before returning
        return degreesInt % 100;
    }
}
